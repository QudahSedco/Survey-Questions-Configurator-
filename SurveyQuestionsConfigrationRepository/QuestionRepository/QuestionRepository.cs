using Serilog;
using Serilog.Formatting.Json;
using SurveyQuestionsConfigurator.Models;
using SurveyQuestionsConfiguratorModels;
using SurveyQuestionsConfiguratorModels.Result;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;
using static System.Net.Mime.MediaTypeNames;

namespace SurveyQuestionsConfigurator.Repositories
{
    // SqlTableDependency requires a class that is not abstract and has property names matching the columns in the Database
    // This class is created only to notify of any changes in the Questions table in the Database
    public class QuestionTableColumns
    {
        public int question_id { get; set; }
        public string question_text { get; set; }
        public int question_type { get; set; }
        public int question_order { get; set; }
    }

    public class QuestionRepository : IQuestionRepository
    {
        private SqlTableDependency<QuestionTableColumns> mSqlTableDependency;

        public event Action QuestionsTableChanged;

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
        private const string UNEXPECTED_ERROR_MESSAGE = "Unexpected error happend";

        //Paramaters
        private const string PARAM_ID = "@id";

        private const string PARAM_QUESTION_TEXT = "@QuestionText";
        private const string PARAM_QUESTION_ORDER = "@QuestionOrder";
        private const string PARAM_QUESTION_TYPE = "@QuestionType";
        private const string PARAM_NUMBER_OF_STARS = "@numberOfStars";
        private const string PARAM_NUMBER_OF_SMILEY_FACES = "@numberOfSmileyFaces";
        private const string PARAM_START_VALUE = "@startValue";
        private const string PARAM_END_VALUE = "@endValue";
        private const string PARAM_START_CAPTION = "@startCaption";
        private const string PARAM_END_CAPTION = "@endCaption";

        public QuestionRepository()
        {
        }

        //if any changes in Database happens trigger the event
        private void OnQuestionsTableChanged(Object pSender, EventArgs pE)
        {
            try
            {
                QuestionsTableChanged?.Invoke();
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, "Error Invoking Questions Table changed");
                throw;
            }
        }

        public Result<bool> StartListening()
        {
            try
            {
                mSqlTableDependency = new SqlTableDependency<QuestionTableColumns>(mConnectionString, QUESTIONS_TABLE);
                mSqlTableDependency.OnChanged += OnQuestionsTableChanged;
                mSqlTableDependency.Start();
                return Result<bool>.Success(true);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                return Result<bool>.Failure(ResultStatus.SqlTableDependencyError);
            }
        }

        //Called once the application closes
        public void StopListening()
        {
            try
            {
                mSqlTableDependency?.Stop();
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, "Error stopping sqlTableDependency");
            }
        }

        public Result<bool> TestConnection(String pConnectionString)
        {
            try
            {
                using (SqlConnection tConnection = new SqlConnection(pConnectionString))
                    tConnection.Open();
                return Result<bool>.Success(true);
            }
            catch (SqlException ex)
            {
                Log.Error(ex, "Error connecting to Database");
                return Result<bool>.Failure(ResultStatus.DatabaseConnectionError);
            }
            catch (Exception ex)
            {
                Log.Error(ex, UNEXPECTED_ERROR_MESSAGE);
                return Result<bool>.Failure(ResultStatus.UnexpectedError);
            }
        }

        //Inserts into the base table then retrieves the Id created by the Database and uses it to create a child record
        public Result<bool> AddQuestion(Question pQuestion)
        {
            try
            {
                using (SqlConnection tConnection = new SqlConnection(mConnectionString))
                {
                    tConnection.Open();

                    using (SqlTransaction tTransaction = tConnection.BeginTransaction())
                    {
                        try
                        {
                            string tSql =
                                $@"INSERT INTO {QUESTIONS_TABLE} ({COLUMN_QUESTION_TEXT}, {COLUMN_QUESTION_ORDER}, {COLUMN_QUESTION_TYPE}) VALUES ({PARAM_QUESTION_TEXT}, {PARAM_QUESTION_ORDER}, {PARAM_QUESTION_TYPE});SELECT CAST(SCOPE_IDENTITY() AS INT);";

                            using (SqlCommand tCmd = new SqlCommand(tSql, tConnection, tTransaction))
                            {
                                tCmd.Parameters.AddWithValue(PARAM_QUESTION_TEXT, pQuestion.QuestionText);
                                tCmd.Parameters.AddWithValue(PARAM_QUESTION_ORDER, pQuestion.QuestionOrder);
                                tCmd.Parameters.AddWithValue(PARAM_QUESTION_TYPE, (int)pQuestion.QuestionType);

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
                                    return Result<bool>.Failure(ResultStatus.UnknownTypeError);
                            }

                            tTransaction.Commit();
                            return Result<bool>.Success(true);
                        }
                        catch
                        {
                            tTransaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Log.Error(ex, "Error adding a question to the Database");
                return Result<bool>.Failure(ResultStatus.DatabaseError);
            }
            catch (Exception ex)
            {
                Log.Error(ex, UNEXPECTED_ERROR_MESSAGE);
                return Result<bool>.Failure(ResultStatus.UnexpectedError);
            }
        }

        public Result<bool> DeleteQuestionById(int pId)
        {
            try
            {
                using (SqlConnection tConnection = new SqlConnection(mConnectionString))
                {
                    tConnection.Open();
                    String tSql = $"DELETE FROM {QUESTIONS_TABLE} WHERE {COLUMN_QUESTION_ID} = {PARAM_ID}";

                    using (SqlCommand tCmd = new SqlCommand(tSql, tConnection))
                    {
                        tCmd.Parameters.AddWithValue(PARAM_ID, pId);
                        tCmd.ExecuteNonQuery();
                        return Result<bool>.Success(true);
                    }
                }
            }
            catch (SqlException tEx)
            {
                Log.Error(tEx, "Error happened while trying to delete question with ID {QuestionId}", pId);
                return Result<bool>.Failure(ResultStatus.DatabaseError);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                return Result<bool>.Failure(ResultStatus.UnexpectedError);
            }
        }

        public Result<List<Question>> GetAllQuestions()
        {
            try
            {
                var tQuestionsList = new List<Question>();

                string tSql = $"SELECT {COLUMN_QUESTION_ID}, {COLUMN_QUESTION_TEXT}, {COLUMN_QUESTION_ORDER}, {COLUMN_QUESTION_TYPE} FROM {QUESTIONS_TABLE} ORDER BY {COLUMN_QUESTION_ORDER}";

                using (SqlConnection tConnection = new SqlConnection(mConnectionString))
                using (SqlCommand tCmd = new SqlCommand(tSql, tConnection))
                {
                    tConnection.Open();

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
                                    return Result<List<Question>>.Failure(ResultStatus.UnknownTypeError);
                            }

                            tQuestion.Id = tReader.GetInt32(0);
                            tQuestion.QuestionText = tReader.GetString(1);
                            tQuestion.QuestionOrder = tReader.GetInt32(2);

                            tQuestionsList.Add(tQuestion);
                        }
                    }

                    return Result<List<Question>>.Success(tQuestionsList);
                }
            }
            catch (SqlException tEx)
            {
                Log.Error(tEx, "Error getting all questions");
                return Result<List<Question>>.Failure(ResultStatus.DatabaseError);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                return Result<List<Question>>.Failure(ResultStatus.UnexpectedError);
            }
        }

        public Result<Question> GetChildQuestion(Question pQuestion)
        {
            try
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
                        return Result<Question>.Failure(ResultStatus.UnknownTypeError);
                }
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                return Result<Question>.Failure(ResultStatus.UnexpectedError);
            }
        }

        //two try statments are used here the nested one is to rollback the database
        //and to make sure the variabletTrsansdacitpon is sdtill iun the scopr becuase the word Using
        public Result<bool> UpdateQuestion(Question pQuestion)
        {
            try
            {
                using (SqlConnection tConnection = new SqlConnection(mConnectionString))
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
                                    return Result<bool>.Failure(ResultStatus.UnknownTypeError);
                            }

                            tTransaction.Commit();
                            return Result<bool>.Success(true);
                        }
                        catch
                        {
                            tTransaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (SqlException tEx)
            {
                Log.Error(tEx, "Error while Updating question with ID {questionId} in Database", pQuestion.Id);
                return Result<bool>.Failure(ResultStatus.DatabaseError);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                return Result<bool>.Failure(ResultStatus.UnexpectedError);
            }
        }

        private void UpdateStarQuestion(StarQuestion pStarQuestion, SqlConnection pSqlConnection, SqlTransaction pSqlTransaction)
        {
            string tSql = $"UPDATE {STAR_QUESTIONS_TABLE} SET {COLUMN_NUMBER_OF_STARS} = {PARAM_NUMBER_OF_STARS} WHERE {COLUMN_QUESTION_ID}={PARAM_ID}";
            try
            {
                using (SqlCommand tCmd = new SqlCommand(tSql, pSqlConnection, pSqlTransaction))
                {
                    tCmd.Parameters.AddWithValue(PARAM_ID, pStarQuestion.Id);
                    tCmd.Parameters.AddWithValue(PARAM_NUMBER_OF_STARS, pStarQuestion.NumberOfStars);

                    tCmd.ExecuteNonQuery();
                }
            }
            catch (SqlException tEx)
            {
                Log.Error(tEx, "Error  while updating star question with id {id}", pStarQuestion.Id);
                throw;
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                throw;
            }
        }

        private void UpdateSmileyQuestion(SmileyFacesQuestion pSmileyQuestion, SqlConnection pSqlConnection, SqlTransaction pSqlTransaction)
        {
            string tSql = $"UPDATE {SMILEY_FACES_QUESTIONS_TABLE} SET {COLUMN_NUMBER_OF_SMILEY_FACES}={PARAM_NUMBER_OF_SMILEY_FACES} WHERE {COLUMN_QUESTION_ID} = {PARAM_ID}";
            try
            {
                using (SqlCommand tCmd = new SqlCommand(tSql, pSqlConnection, pSqlTransaction))
                {
                    tCmd.Parameters.AddWithValue(PARAM_ID, pSmileyQuestion.Id);
                    tCmd.Parameters.AddWithValue(PARAM_NUMBER_OF_SMILEY_FACES, pSmileyQuestion.NumberOfSmileyFaces);

                    tCmd.ExecuteNonQuery();
                }
            }
            catch (SqlException tEx)
            {
                Log.Error(tEx, "Error while updating Smiley face question with id {id}", pSmileyQuestion.Id);
                throw;
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                throw;
            }
        }

        private void UpdateSliderQuestion(SliderQuestion pSliderQuestion, SqlConnection pSqlConnection, SqlTransaction pSqlTransaction)
        {
            string tSql = $"UPDATE {SLIDER_QUESTIONS_TABLE} SET {COLUMN_START_VALUE} = {PARAM_START_VALUE}, {COLUMN_END_VALUE}={PARAM_END_VALUE},{COLUMN_START_VALUE_CAPTION}={PARAM_START_CAPTION},{COLUMN_END_VALUE_CAPTION}={PARAM_END_CAPTION} WHERE {COLUMN_QUESTION_ID}= {PARAM_ID}";
            try
            {
                using (SqlCommand tCmd = new SqlCommand(tSql, pSqlConnection, pSqlTransaction))
                {
                    tCmd.Parameters.AddWithValue(PARAM_ID, pSliderQuestion.Id);
                    tCmd.Parameters.AddWithValue(PARAM_START_VALUE, pSliderQuestion.StartValue);
                    tCmd.Parameters.AddWithValue(PARAM_END_VALUE, pSliderQuestion.EndValue);
                    tCmd.Parameters.AddWithValue(PARAM_START_CAPTION, pSliderQuestion.StartValueCaption);
                    tCmd.Parameters.AddWithValue(PARAM_END_CAPTION, pSliderQuestion.EndValueCaption);

                    tCmd.ExecuteNonQuery();
                }
            }
            catch (SqlException tEx)
            {
                Log.Error(tEx, "Error  while updating Slider question with id {id}", pSliderQuestion.Id);
                throw;
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                throw;
            }
        }

        private void AddStarQuestion(StarQuestion pQuestion, SqlConnection pConnection, SqlTransaction pTransaction)
        {
            string tSql = $@"INSERT INTO {STAR_QUESTIONS_TABLE} ({COLUMN_QUESTION_ID}, {COLUMN_NUMBER_OF_STARS}) VALUES ({PARAM_ID}, {PARAM_NUMBER_OF_STARS})";
            try
            {
                using (SqlCommand tCmd = new SqlCommand(tSql, pConnection, pTransaction))
                {
                    tCmd.Parameters.AddWithValue(PARAM_ID, pQuestion.Id);
                    tCmd.Parameters.AddWithValue(PARAM_NUMBER_OF_STARS, pQuestion.NumberOfStars);

                    tCmd.ExecuteNonQuery();
                }
            }
            catch (SqlException tEx)
            {
                Log.Error(tEx, "Error  while adding star question with id {id}", pQuestion.Id);
                throw;
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                throw;
            }
        }

        private void AddSmileyFaceQuestion(SmileyFacesQuestion pQuestion, SqlConnection pConnection, SqlTransaction pTransaction)
        {
            string tSql = $"INSERT INTO {SMILEY_FACES_QUESTIONS_TABLE} ({COLUMN_QUESTION_ID},{COLUMN_NUMBER_OF_SMILEY_FACES}) VALUES ({PARAM_ID},{PARAM_NUMBER_OF_SMILEY_FACES})";
            try
            {
                using (SqlCommand tCmd = new SqlCommand(tSql, pConnection, pTransaction))
                {
                    tCmd.Parameters.AddWithValue(PARAM_ID, pQuestion.Id);
                    tCmd.Parameters.AddWithValue(PARAM_NUMBER_OF_SMILEY_FACES, pQuestion.NumberOfSmileyFaces);

                    tCmd.ExecuteNonQuery();
                }
            }
            catch (SqlException tEx)
            {
                Log.Error(tEx, "Error  while adding smiley face question with id {id}", pQuestion.Id);
                throw;
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                throw;
            }
        }

        private void AddSliderQuestion(SliderQuestion pQuestion, SqlConnection pConnection, SqlTransaction pTransaction)
        {
            string tSql = $@"INSERT INTO {SLIDER_QUESTIONS_TABLE}({COLUMN_QUESTION_ID},{COLUMN_START_VALUE},{COLUMN_END_VALUE},{COLUMN_START_VALUE_CAPTION},{COLUMN_END_VALUE_CAPTION})
                 VALUES ({PARAM_ID},{PARAM_START_VALUE},{PARAM_END_VALUE},{PARAM_START_CAPTION},{PARAM_END_CAPTION})";
            try
            {
                using (SqlCommand tCmd = new SqlCommand(tSql, pConnection, pTransaction))
                {
                    tCmd.Parameters.AddWithValue(PARAM_ID, pQuestion.Id);
                    tCmd.Parameters.AddWithValue(PARAM_START_VALUE, pQuestion.StartValue);
                    tCmd.Parameters.AddWithValue(PARAM_END_VALUE, pQuestion.EndValue);
                    tCmd.Parameters.AddWithValue(PARAM_START_CAPTION, pQuestion.StartValueCaption);
                    tCmd.Parameters.AddWithValue(PARAM_END_CAPTION, pQuestion.EndValueCaption);

                    tCmd.ExecuteNonQuery();
                }
            }
            catch (SqlException tEx)
            {
                Log.Error(tEx, "Error  while adding slider question with id {id}", pQuestion.Id);
                throw;
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                throw;
            }
        }

        private Result<Question> GetStarQuestion(StarQuestion pQuestion)
        {
            try
            {
                using (SqlConnection tConnection = new SqlConnection(mConnectionString))

                {
                    tConnection.Open();
                    string tSql = $"SELECT {COLUMN_NUMBER_OF_STARS} FROM {STAR_QUESTIONS_TABLE} WHERE {COLUMN_QUESTION_ID} = {PARAM_ID}";

                    using (SqlCommand tCmd = new SqlCommand(tSql, tConnection))
                    {
                        tCmd.Parameters.AddWithValue(PARAM_ID, pQuestion.Id);

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
            }
            catch (SqlException tEx)
            {
                Log.Error(tEx, "Error while retrieving StarQuestion with id {QuestionId} from Database", pQuestion.Id);
                return Result<Question>.Failure(ResultStatus.DatabaseError);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                return Result<Question>.Failure(ResultStatus.UnexpectedError);
            }
        }

        private Result<Question> GetSmileyQuestion(SmileyFacesQuestion pQuestion)

        {
            try
            {
                using (SqlConnection tConnection = new SqlConnection(mConnectionString))
                {
                    tConnection.Open();
                    String tSql = $"SELECT {COLUMN_NUMBER_OF_SMILEY_FACES} FROM {SMILEY_FACES_QUESTIONS_TABLE} WHERE {COLUMN_QUESTION_ID}={PARAM_ID}";

                    using (SqlCommand tCmd = new SqlCommand(tSql, tConnection))
                    {
                        tCmd.Parameters.AddWithValue(PARAM_ID, pQuestion.Id);
                        using (SqlDataReader tReader = tCmd.ExecuteReader())
                        {
                            if (tReader.Read())
                            {
                                pQuestion.NumberOfSmileyFaces = tReader.GetInt32(0);
                            }
                        }
                    }
                }

                return Result<Question>.Success(pQuestion);
            }
            catch (SqlException tEx)
            {
                Log.Error(tEx, "Error while retrieving Smiley question with id {QuestionId} from Database", pQuestion.Id);
                return Result<Question>.Failure(ResultStatus.DatabaseError);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                return Result<Question>.Failure(ResultStatus.UnexpectedError);
            }
        }

        private Result<Question> GetSliderQuestion(SliderQuestion pQuestion)
        {
            try
            {
                using (SqlConnection tConnection = new SqlConnection(mConnectionString))
                {
                    tConnection.Open();
                    String tSql = $"SELECT {COLUMN_START_VALUE},{COLUMN_END_VALUE},{COLUMN_START_VALUE_CAPTION},{COLUMN_END_VALUE_CAPTION} FROM {SLIDER_QUESTIONS_TABLE} WHERE {COLUMN_QUESTION_ID}={PARAM_ID}";

                    using (SqlCommand tCmd = new SqlCommand(tSql, tConnection))
                    {
                        tCmd.Parameters.AddWithValue(PARAM_ID, pQuestion.Id);
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
            }
            catch (SqlException tEx)
            {
                Log.Error(tEx, "Error while retrieving Slider Question with Id {QuestionId} from Database", pQuestion.Id);
                return Result<Question>.Failure(ResultStatus.DatabaseError);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                return Result<Question>.Failure(ResultStatus.UnexpectedError);
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
                                    tSql = $"DELETE FROM {STAR_QUESTIONS_TABLE} WHERE {COLUMN_QUESTION_ID} = {PARAM_ID}";
                                    break;

                                case QuestionType.Smiley:
                                    tSql = $"DELETE FROM {SMILEY_FACES_QUESTIONS_TABLE} WHERE {COLUMN_QUESTION_ID} = {PARAM_ID}";
                                    break;

                                case QuestionType.Slider:
                                    tSql = $"DELETE FROM {SLIDER_QUESTIONS_TABLE} WHERE  {COLUMN_QUESTION_ID}  = {PARAM_ID}";
                                    break;

                                default:
                                    return Result<bool>.Failure(ResultStatus.UnknownTypeError);
                            }

                            using (SqlCommand tCmd = new SqlCommand(tSql, tConnection, tTransaction))
                            {
                                tCmd.Parameters.AddWithValue(PARAM_ID, pQuestion.Id);
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
                        catch
                        {
                            tTransaction.Rollback();
                            throw;
                        }
                    }
                }
                catch (SqlException tEx)
                {
                    Log.Error(tEx, "Error updating child table");
                    return Result<bool>.Failure(ResultStatus.DatabaseError);
                }
                catch (Exception tEx)
                {
                    Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                    return Result<bool>.Failure(ResultStatus.UnexpectedError);
                }
            }
        }

        private void UpdateBaseQuestion(Question pQuestion, SqlConnection pConnection, SqlTransaction pTransaction)
        {
            string tSql = $"UPDATE {QUESTIONS_TABLE} SET {COLUMN_QUESTION_TEXT} = {PARAM_QUESTION_TEXT},{COLUMN_QUESTION_ORDER} = {PARAM_QUESTION_ORDER},{COLUMN_QUESTION_TYPE} = {PARAM_QUESTION_TYPE} WHERE {COLUMN_QUESTION_ID}={PARAM_ID}";
            try
            {
                using (SqlCommand tCmd = new SqlCommand(tSql, pConnection, pTransaction))
                {
                    tCmd.Parameters.AddWithValue(PARAM_ID, pQuestion.Id);
                    tCmd.Parameters.AddWithValue(PARAM_QUESTION_TEXT, pQuestion.QuestionText);
                    tCmd.Parameters.AddWithValue(PARAM_QUESTION_ORDER, pQuestion.QuestionOrder);
                    tCmd.Parameters.AddWithValue(PARAM_QUESTION_TYPE, (int)pQuestion.QuestionType);

                    tCmd.ExecuteNonQuery();
                }
            }
            catch (SqlException tEx)
            {
                Log.Error(tEx, "Error happened while updating base question with id {id}", pQuestion.Id);
                throw;
            }
            catch
            {
                throw;
            }
        }
    }
}