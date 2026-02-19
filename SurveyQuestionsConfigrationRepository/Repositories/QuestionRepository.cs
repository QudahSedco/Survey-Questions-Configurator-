using Serilog;
using Serilog.Formatting.Json;
using SurveyQuestionsConfigurator.Models;
using SurveyQuestionsConfiguratorModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;
using static System.Net.Mime.MediaTypeNames;

namespace SurveyQuestionsConfigurator.Repositories
{
    // SqlTableDependency requires a class that is not abstract and has property names matching the columns in the database
    // This class is created only to notify of any changes in the Questions table in the database
    public class QuestionRow
    {
        public int question_id { get; set; }
        public string question_text { get; set; }
        public int question_type { get; set; }
        public int question_order { get; set; }
    }

    public class QuestionRepository : IQuestionRepository
    {
        private SqlTableDependency<QuestionRow> mSqlTableDependency;

        public event Action EventOnQuestionsTableChanged;

        private readonly string mConnectionString = ConfigurationManager.ConnectionStrings["SurveyDb"].ConnectionString;
        private const string QUESTIONS_TABLE = "Questions";
        private const string STAR_QUESTIONS_TABLE = "Star_Questions";
        private const string SMILEY_FACES_QUESTIONS_TABLE = "Smiley_Faces_Questions";
        private const string SLIDER_QUESTIONS_TABLE = "Slider_Questions";
        private const string COLUMN_QUESTION_ID = "question_id";
        private const string COLUMN_QUESTION_TEXT = "question_text";
        private const string COLUMN_QUESTION_ORDER = "question_order";
        private const string COLUMN_QUESTION_TYPE = "question_type";
        private const string COLUMN_NUMBER_OF_STARS = "number_of_stars";
        private const string COLUMN_NUMBER_OF_SMILEY_FACES = "number_of_smiley_faces";
        private const string COLUMN_START_VALUE = "start_value";
        private const string COLUMN_END_VALUE = "end_value";
        private const string COLUMN_START_VALUE_CAPTION = "start_value_caption";
        private const string COLUMN_END_VALUE_CAPTION = "end_value_caption";

        public QuestionRepository()
        {
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
                    .WriteTo.File(new JsonFormatter(), "logs/SurveyQuestionsConfigurator-.json", rollingInterval: RollingInterval.Day).CreateLogger();
        }

        //if any changes in database happens trigger the event
        private void TableDependencyChanged(Object sender, EventArgs e)
        {
            EventOnQuestionsTableChanged?.Invoke();
        }

        public Result<bool> StartListening()
        {
            try
            {
                mSqlTableDependency = new SqlTableDependency<QuestionRow>(mConnectionString, QUESTIONS_TABLE);
                mSqlTableDependency.OnChanged += TableDependencyChanged;
                mSqlTableDependency.Start();
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error starting SqlTableDependency");
                return Result<bool>.Failure("Failed to start SqlTableDependency");
            }
        }

        public void StopListening()
        {
            mSqlTableDependency?.Stop();
        }

        //Inserts into the base table then retrieves the Id created by the database and uses it to create a child record
        public Result<bool> AddQuestion(Question pQuestion)
        {
            using (SqlConnection tConnection = new SqlConnection(mConnectionString))
            {
                try
                {
                    tConnection.Open();

                    using (SqlTransaction tTransaction = tConnection.BeginTransaction())
                    {
                        try
                        {
                            string tSql =
                                $@"INSERT INTO {QUESTIONS_TABLE} ({COLUMN_QUESTION_TEXT}, {COLUMN_QUESTION_ORDER}, {COLUMN_QUESTION_TYPE}) VALUES (@text, @order, @type);SELECT CAST(SCOPE_IDENTITY() AS INT);";

                            using (SqlCommand tCmd = new SqlCommand(tSql, tConnection, tTransaction))
                            {
                                tCmd.Parameters.AddWithValue("@text", pQuestion.QuestionText);
                                tCmd.Parameters.AddWithValue("@order", pQuestion.QuestionOrder);
                                tCmd.Parameters.AddWithValue("@type", (int)pQuestion.QuestionType);

                                pQuestion.Id = (int)tCmd.ExecuteScalar();
                            }

                            switch (pQuestion)
                            {
                                case StarQuestion tStarQuestion:
                                    AddStarQuestion(tStarQuestion, tConnection, tTransaction);
                                    break;

                                case SmileyFacesQuestion tSmileyFacesQuestion:
                                    AddSmileyFaceQuestion(tSmileyFacesQuestion, tConnection, tTransaction);
                                    break;

                                case SliderQuestion tSliderQuestion:
                                    AddSliderQuestion(tSliderQuestion, tConnection, tTransaction);
                                    break;

                                default:
                                    tTransaction.Rollback();
                                    return Result<bool>.Failure("Failed to add an unknown question type");
                            }

                            tTransaction.Commit();
                            return Result<bool>.Success(true);
                        }
                        catch (SqlException ex)
                        {
                            tTransaction.Rollback();
                            Log.Error(ex, "Could Not complete the create question transaction");
                            return Result<bool>.Failure("Failed to save question in database");
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Log.Error(ex, "Could Not connect to Database");
                    return Result<bool>.Failure("Failed to connect to the database");
                }
            }
        }

        public Result<bool> DeleteQuestionById(int pId)
        {
            if (pId <= 0)
                return Result<bool>.Failure("Failed  to delete Invalid Question ID");

            using (SqlConnection tConnection = new SqlConnection(mConnectionString))
            {
                String tSql = $"DELETE FROM {QUESTIONS_TABLE} WHERE {COLUMN_QUESTION_ID} = @id";

                using (SqlCommand tCmd = new SqlCommand(tSql, tConnection))
                {
                    try
                    {
                        tConnection.Open();
                        tCmd.Parameters.AddWithValue("@id", pId);
                        tCmd.ExecuteNonQuery();
                        return Result<bool>.Success(true);
                    }
                    catch (SqlException tEx)
                    {
                        Log.Error(tEx, "Error happened while trying to delete question with ID {QuestionId}", pId);
                        return Result<bool>.Failure($"Failed to delete the question with ID {pId}");
                    }
                }
            }
        }

        public Result<List<Question>> GetAllQuestions()
        {
            var tQuestionsList = new List<Question>();

            string tSql = $"SELECT {COLUMN_QUESTION_ID}, {COLUMN_QUESTION_TEXT}, {COLUMN_QUESTION_ORDER}, {COLUMN_QUESTION_TYPE} FROM {QUESTIONS_TABLE} ORDER BY {COLUMN_QUESTION_ORDER}";

            using (SqlConnection tConnection = new SqlConnection(mConnectionString))
            using (SqlCommand tCmd = new SqlCommand(tSql, tConnection))
            {
                try
                {
                    tConnection.Open();
                    try
                    {
                        using (SqlDataReader tReader = tCmd.ExecuteReader())
                        {
                            while (tReader.Read())
                            {
                                QuestionType tType = (QuestionType)tReader.GetInt32(3);

                                Question tQuestion;

                                switch (tType)
                                {
                                    case QuestionType.Smiley:
                                        tQuestion = new SmileyFacesQuestion();
                                        break;

                                    case QuestionType.Slider:
                                        tQuestion = new SliderQuestion();
                                        break;

                                    case QuestionType.Star:
                                        tQuestion = new StarQuestion();
                                        break;

                                    default:
                                        return Result<List<Question>>.Failure("Failed to get question Unknown question type");
                                }

                                tQuestion.Id = tReader.GetInt32(0);
                                tQuestion.QuestionText = tReader.GetString(1);
                                tQuestion.QuestionOrder = tReader.GetInt32(2);

                                tQuestionsList.Add(tQuestion);
                            }
                        }

                        return Result<List<Question>>.Success(tQuestionsList);
                    }
                    catch (Exception tEx)
                    {
                        Log.Error(tEx, "Error getting all questions from the DataBase");
                        return Result<List<Question>>.Failure("Failed to get all questions from the DataBase");
                    }
                }
                catch (SqlException tEx)
                {
                    Log.Error(tEx, "Error Connecting to the DataBase");
                    return Result<List<Question>>.Failure("Failed to connect to the DataBase");
                }
            }
        }

        public Result<Question> GetChildQuestion(Question pQuestion)
        {
            switch (pQuestion)
            {
                case StarQuestion tStarQuestion:
                    return GetStarQuestion(tStarQuestion);

                case SmileyFacesQuestion tSmileyFacesQuestion:
                    return GetSmileyQuestion(tSmileyFacesQuestion);

                case SliderQuestion tSliderQuestion:
                    return GetSliderQuestion(tSliderQuestion);

                default:
                    return Result<Question>.Failure("Failed to get child question Unknown question type");
            }
        }

        public Result<bool> UpdateQuestion(Question pQuestion)
        {
            using (SqlConnection tConnection = new SqlConnection(mConnectionString))
            {
                try
                {
                    tConnection.Open();

                    using (SqlTransaction tTransaction = tConnection.BeginTransaction())

                    {
                        try
                        {
                            UpdateBaseQuestion(pQuestion, tConnection, tTransaction);

                            switch (pQuestion)
                            {
                                case StarQuestion tStarQuestion:
                                    UpdateStarQuestion(tStarQuestion, tConnection, tTransaction);
                                    break;

                                case SmileyFacesQuestion tSmileyFaceQuestion:
                                    UpdateSmileyQuestion(tSmileyFaceQuestion, tConnection, tTransaction);
                                    break;

                                case SliderQuestion tSliderQuestion:
                                    UpdateSliderQuestion(tSliderQuestion, tConnection, tTransaction);
                                    break;

                                default:
                                    tTransaction.Rollback();
                                    return Result<bool>.Failure("Failed to update question unknown question type");
                            }

                            tTransaction.Commit();
                            return Result<bool>.Success(true);
                        }
                        catch (SqlException tEx)
                        {
                            tTransaction.Rollback();
                            Log.Error(tEx, "Error while Updating question with ID {questionId} in database", pQuestion.Id);
                            return Result<bool>.Failure($"Failed to update question with ID {pQuestion.Id}");
                        }
                    }
                }
                catch (SqlException tEx)
                {
                    Log.Error(tEx, "Error couldn't connect to DataBase");
                    return Result<bool>.Failure("Failed to connect to database");
                }
            }
        }

        private void UpdateStarQuestion(StarQuestion pStarQuestion, SqlConnection pSqlConnection, SqlTransaction pSqlTransaction)
        {
            string tSql = $"UPDATE {STAR_QUESTIONS_TABLE} SET {COLUMN_NUMBER_OF_STARS} = @numberOfStars WHERE {COLUMN_QUESTION_ID}=@id";
            using (SqlCommand tCmd = new SqlCommand(tSql, pSqlConnection, pSqlTransaction))
            {
                tCmd.Parameters.AddWithValue("@id", pStarQuestion.Id);
                tCmd.Parameters.AddWithValue("@numberOfStars", pStarQuestion.NumberOfStars);
                try
                {
                    tCmd.ExecuteNonQuery();
                }
                catch (SqlException tEx)
                {
                    Log.Error(tEx, "Error  while updating star question with id {id}", pStarQuestion.Id);
                    throw;
                }
            }
        }

        private void UpdateSmileyQuestion(SmileyFacesQuestion pSmileyQuestion, SqlConnection pSqlConnection, SqlTransaction pSqlTransaction)
        {
            string tSql = $"UPDATE {SMILEY_FACES_QUESTIONS_TABLE} SET {COLUMN_NUMBER_OF_SMILEY_FACES}=@numberOfSmileyFaces WHERE {COLUMN_QUESTION_ID} = @id";
            using (SqlCommand tCmd = new SqlCommand(tSql, pSqlConnection, pSqlTransaction))
            {
                tCmd.Parameters.AddWithValue("@id", pSmileyQuestion.Id);
                tCmd.Parameters.AddWithValue("@numberOfSmileyFaces", pSmileyQuestion.NumberOfSmileyFaces);
                try
                {
                    tCmd.ExecuteNonQuery();
                }
                catch (SqlException tEx)
                {
                    Log.Error(tEx, "Error while updating Smiley face question with id {id}", pSmileyQuestion.Id);
                    throw;
                }
            }
        }

        private void UpdateSliderQuestion(SliderQuestion pSliderQuestion, SqlConnection pSqlConnection, SqlTransaction pSqlTransaction)
        {
            string tSql = $"UPDATE {SLIDER_QUESTIONS_TABLE} SET {COLUMN_START_VALUE} = @startValue, {COLUMN_END_VALUE}=@endValue,{COLUMN_START_VALUE_CAPTION}=@startCaption,{COLUMN_END_VALUE_CAPTION}=@endCaption WHERE {COLUMN_QUESTION_ID}= @id";
            using (SqlCommand tCmd = new SqlCommand(tSql, pSqlConnection, pSqlTransaction))
            {
                tCmd.Parameters.AddWithValue("@id", pSliderQuestion.Id);
                tCmd.Parameters.AddWithValue("@startValue", pSliderQuestion.StartValue);
                tCmd.Parameters.AddWithValue("@endValue", pSliderQuestion.EndValue);
                tCmd.Parameters.AddWithValue("@startCaption", pSliderQuestion.StartValueCaption);
                tCmd.Parameters.AddWithValue("@endCaption", pSliderQuestion.EndValueCaption);
                try
                {
                    tCmd.ExecuteNonQuery();
                }
                catch (SqlException tEx)
                {
                    Log.Error(tEx, "Error  while updating Slider question with id {id}", pSliderQuestion.Id);
                    throw;
                }
            }
        }

        private void AddStarQuestion(StarQuestion pQuestion, SqlConnection pConnection, SqlTransaction pTransaction)
        {
            string tSql = $@"INSERT INTO {STAR_QUESTIONS_TABLE} ({COLUMN_QUESTION_ID}, {COLUMN_NUMBER_OF_STARS}) VALUES (@id, @stars)";

            using (SqlCommand tCmd = new SqlCommand(tSql, pConnection, pTransaction))
            {
                tCmd.Parameters.AddWithValue("@id", pQuestion.Id);
                tCmd.Parameters.AddWithValue("@stars", pQuestion.NumberOfStars);
                try
                {
                    tCmd.ExecuteNonQuery();
                }
                catch (SqlException tEx)
                {
                    Log.Error(tEx, "Error  while adding star question with id {id}", pQuestion.Id);
                    throw;
                }
            }
        }

        private void AddSmileyFaceQuestion(SmileyFacesQuestion pQuestion, SqlConnection pConnection, SqlTransaction pTransaction)
        {
            string tSql = $"INSERT INTO {SMILEY_FACES_QUESTIONS_TABLE} ({COLUMN_QUESTION_ID},{COLUMN_NUMBER_OF_SMILEY_FACES}) VALUES (@id,@NumberOfSmileyFaces)";

            using (SqlCommand tCmd = new SqlCommand(tSql, pConnection, pTransaction))
            {
                tCmd.Parameters.AddWithValue("@id", pQuestion.Id);
                tCmd.Parameters.AddWithValue("@NumberOfSmileyFaces", pQuestion.NumberOfSmileyFaces);
                try
                {
                    tCmd.ExecuteNonQuery();
                }
                catch (SqlException tEx)
                {
                    Log.Error(tEx, "Error  while adding smiley face question with id {id}", pQuestion.Id);
                    throw;
                }
            }
        }

        private void AddSliderQuestion(SliderQuestion pQuestion, SqlConnection pConnection, SqlTransaction pTransaction)
        {
            string tSql = $@"INSERT INTO {SLIDER_QUESTIONS_TABLE}({COLUMN_QUESTION_ID},{COLUMN_START_VALUE},{COLUMN_END_VALUE},{COLUMN_START_VALUE_CAPTION},{COLUMN_END_VALUE_CAPTION})
                 VALUES (@id,@startValue,@endValue,@startCaption,@endCaption)";
            using (SqlCommand tCmd = new SqlCommand(tSql, pConnection, pTransaction))
            {
                tCmd.Parameters.AddWithValue("@id", pQuestion.Id);
                tCmd.Parameters.AddWithValue("@startValue", pQuestion.StartValue);
                tCmd.Parameters.AddWithValue("@endValue", pQuestion.EndValue);
                tCmd.Parameters.AddWithValue("@startCaption", pQuestion.StartValueCaption);
                tCmd.Parameters.AddWithValue("@endCaption", pQuestion.EndValueCaption);
                try
                {
                    tCmd.ExecuteNonQuery();
                }
                catch (SqlException tEx)
                {
                    Log.Error(tEx, "Error  while adding slider question with id {id}", pQuestion.Id);
                    throw;
                }
            }
        }

        private Result<Question> GetStarQuestion(StarQuestion pQuestion)
        {
            using (SqlConnection tConnection = new SqlConnection(mConnectionString))
            {
                try
                {
                    tConnection.Open();

                    string tSql = $"SELECT {COLUMN_NUMBER_OF_STARS} FROM {STAR_QUESTIONS_TABLE} WHERE {COLUMN_QUESTION_ID} = @id";

                    using (SqlCommand tCmd = new SqlCommand(tSql, tConnection))
                    {
                        tCmd.Parameters.AddWithValue("@id", pQuestion.Id);

                        using (SqlDataReader tReader = tCmd.ExecuteReader())
                        {
                            if (tReader.Read())
                            {
                                pQuestion.NumberOfStars = tReader.GetInt32(0);
                            }
                        }
                    }
                    return Result<Question>.Success(pQuestion);
                }
                catch (SqlException tEx)
                {
                    Log.Error(tEx, "Error while retrieving StarQuestion with id {QuestionId} from Database", pQuestion.Id);
                    return Result<Question>.Failure($"Failed to retrieve star question with ID {pQuestion.Id}");
                }
            }
        }

        private Result<Question> GetSmileyQuestion(SmileyFacesQuestion pQuestion)

        {
            using (SqlConnection tConnection = new SqlConnection(mConnectionString))
            {
                try
                {
                    tConnection.Open();
                    String tSql = $"SELECT {COLUMN_NUMBER_OF_SMILEY_FACES} FROM {SMILEY_FACES_QUESTIONS_TABLE} WHERE {COLUMN_QUESTION_ID}=@id";

                    using (SqlCommand tCmd = new SqlCommand(tSql, tConnection))
                    {
                        tCmd.Parameters.AddWithValue("@id", pQuestion.Id);
                        using (SqlDataReader tReader = tCmd.ExecuteReader())
                        {
                            if (tReader.Read())
                            {
                                pQuestion.NumberOfSmileyFaces = tReader.GetInt32(0);
                            }
                        }
                    }
                    return Result<Question>.Success(pQuestion);
                }
                catch (SqlException tEx)
                {
                    Log.Error(tEx, "Error while retrieving Smiley question with id {QuestionId} from Database", pQuestion.Id);
                    return Result<Question>.Failure($"Fai to retrieve smiley question with ID {pQuestion.Id}");
                }
            }
        }

        private Result<Question> GetSliderQuestion(SliderQuestion pQuestion)
        {
            using (SqlConnection tConnection = new SqlConnection(mConnectionString))
            {
                try
                {
                    tConnection.Open();
                    String tSql = $"SELECT {COLUMN_START_VALUE},{COLUMN_END_VALUE},{COLUMN_START_VALUE_CAPTION},{COLUMN_END_VALUE_CAPTION} FROM {SLIDER_QUESTIONS_TABLE} WHERE {COLUMN_QUESTION_ID}=@id";

                    using (SqlCommand tCmd = new SqlCommand(tSql, tConnection))
                    {
                        tCmd.Parameters.AddWithValue("@id", pQuestion.Id);
                        using (SqlDataReader tReader = tCmd.ExecuteReader())
                        {
                            if (tReader.Read())
                            {
                                {
                                    pQuestion.SetRange(tReader.GetInt32(0), tReader.GetInt32(1));
                                    pQuestion.StartValueCaption = tReader.GetString(2);
                                    pQuestion.EndValueCaption = tReader.GetString(3);
                                }
                            }
                        }
                    }
                    return Result<Question>.Success(pQuestion);
                }
                catch (SqlException tEx)
                {
                    Log.Error(tEx, "Error while retrieving Slider Question with Id {QuestionId} from Database", pQuestion.Id);
                    return Result<Question>.Failure($"Failed to retrieve Slider question with ID {pQuestion.Id}");
                }
            }
        }

        //removes old child record updates the base record and inserts the new child type record
        public Result<bool> UpdateChildTableType(Question pQuestion, QuestionType pOldQuestionType)
        {
            using (SqlConnection tConnection = new SqlConnection(mConnectionString))
            {
                try
                {
                    tConnection.Open();

                    using (SqlTransaction tTransaction = tConnection.BeginTransaction())
                    {
                        try
                        {
                            string tSql;

                            switch (pOldQuestionType)
                            {
                                case QuestionType.Star:
                                    tSql = $"DELETE FROM {STAR_QUESTIONS_TABLE} WHERE {COLUMN_QUESTION_ID} = @id";
                                    break;

                                case QuestionType.Smiley:
                                    tSql = $"DELETE FROM {SMILEY_FACES_QUESTIONS_TABLE} WHERE {COLUMN_QUESTION_ID} = @id";
                                    break;

                                case QuestionType.Slider:
                                    tSql = $"DELETE FROM {SLIDER_QUESTIONS_TABLE} WHERE  {COLUMN_QUESTION_ID}  = @id";
                                    break;

                                default:
                                    return Result<bool>.Failure("Faild to delete question with unknown type");
                            }

                            using (SqlCommand tCmd = new SqlCommand(tSql, tConnection, tTransaction))
                            {
                                tCmd.Parameters.AddWithValue("@id", pQuestion.Id);
                                tCmd.ExecuteNonQuery();
                            }

                            UpdateBaseQuestion(pQuestion, tConnection, tTransaction);

                            switch (pQuestion)
                            {
                                case StarQuestion tStarQuestion:
                                    AddStarQuestion(tStarQuestion, tConnection, tTransaction);
                                    break;

                                case SmileyFacesQuestion tSmileyFacesQuestion:
                                    AddSmileyFaceQuestion(tSmileyFacesQuestion, tConnection, tTransaction);
                                    break;

                                case SliderQuestion tSliderQuestion:
                                    AddSliderQuestion(tSliderQuestion, tConnection, tTransaction);
                                    break;
                            }

                            tTransaction.Commit();
                            return Result<bool>.Success(true);
                        }
                        catch (SqlException tEx)
                        {
                            tTransaction.Rollback();
                            Log.Error(tEx, "Error updating question {QuestionId}", pQuestion.Id);
                            return Result<bool>.Failure($"Failed to update question with id {pQuestion.Id}");
                        }
                    }
                }
                catch (SqlException tEx)
                {
                    Log.Error(tEx, "Failed to connect to the DataBase");
                    return Result<bool>.Failure("Failed to connect to the DataBase");
                }
            }
        }

        private void UpdateBaseQuestion(Question pQuestion, SqlConnection pConnection, SqlTransaction pTransaction)
        {
            string tSql = $"UPDATE {QUESTIONS_TABLE} SET {COLUMN_QUESTION_TEXT} = @questionText,{COLUMN_QUESTION_ORDER} = @questionOrder,{COLUMN_QUESTION_TYPE} = @questionType WHERE {COLUMN_QUESTION_ID}=@id";
            using (SqlCommand tCmd = new SqlCommand(tSql, pConnection, pTransaction))
            {
                tCmd.Parameters.AddWithValue("@id", pQuestion.Id);
                tCmd.Parameters.AddWithValue("@questionText", pQuestion.QuestionText);
                tCmd.Parameters.AddWithValue("@questionOrder", pQuestion.QuestionOrder);
                tCmd.Parameters.AddWithValue("@questionType", (int)pQuestion.QuestionType);
                try
                {
                    tCmd.ExecuteNonQuery();
                }
                catch (SqlException tEx)
                {
                    Log.Error(tEx, "Error happened while updating  question with id {id}", pQuestion.Id);
                    throw;
                }
            }
        }
    }
}