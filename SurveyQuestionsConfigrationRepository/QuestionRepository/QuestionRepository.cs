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
    /// <summary>
    /// A model used by SqlTableDependency to monitor changes in the Questions table.
    /// Property names must match the column names in the database.
    /// </summary>
    public class QuestionTableColumns
    {
        public int question_id { get; set; }
        public string question_text { get; set; }
        public int question_type { get; set; }
        public int question_order { get; set; }
    }

    /// <summary>
    /// Repository responsible for all database operations related to survey questions.
    /// Handles CRUD operations across the base Questions table and all child type tables,
    /// and listens for real time table changes using SqlTableDependency.
    /// </summary>
    public class QuestionRepository : IQuestionRepository
    {
        private SqlTableDependency<QuestionTableColumns> mSqlTableDependency;

        public event Action QuestionsTableChanged;

        private readonly string mConnectionString = ConfigurationManager.ConnectionStrings["SurveyDb"].ConnectionString;

        //Table names

        private const string QUESTIONS_TABLE = "Questions";
        private const string STAR_QUESTIONS_TABLE = "Star_Questions";
        private const string SMILEY_FACES_QUESTIONS_TABLE = "Smiley_Faces_Questions";
        private const string SLIDER_QUESTIONS_TABLE = "Slider_Questions";

        //Columns names

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

        /// <summary>
        /// Invokes the QuestionsTableChanged event when a change in the Questions table is detected
        /// </summary>
        /// <param name="pSender">The sender object from the table dependency event.</param>
        /// <param name="pE">Event arguments from the table dependency event.</param>
        private void OnQuestionsTableChanged(Object pSender, EventArgs pE)
        {
            try
            {
                QuestionsTableChanged?.Invoke();
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                throw;
            }
        }

        /// <summary>
        /// Starts listening for changes in the Questions table using SqlTableDependency of QuestionTableColumns.
        /// Invokes the QuestionsTableChanged event when changes are detected.
        /// </summary>
        /// <returns>Returns a Result object indicating success or a failure.</returns>

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

        /// <summary>
        /// Stops listening to the Questions table changes if a SqlTableDependency is active.
        /// </summary>
        public void StopListening()
        {
            try
            {
                mSqlTableDependency?.Stop();
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
            }
        }

        /// <summary>
        /// Tests whether the provided database connection string can successfully connect to the database.
        /// </summary>
        /// <param name="pConnectionString">The connection string to test.</param>
        /// <returns>Returns a Result object indicating success or failure.</returns>

        public Result<bool> TestConnection(String pConnectionString)
        {
            try
            {
                using (SqlConnection tConnection = new SqlConnection(pConnectionString))
                    tConnection.Open();
                return Result<bool>.Success(true);
            }
            catch (SqlException tEx)
            {
                Log.Error(tEx, tEx.Message);
                return Result<bool>.Failure(ResultStatus.DatabaseConnectionError);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                return Result<bool>.Failure(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Adds a new question to the database, including its corresponding child table (Star, SmileyFaces, or Slider),
        /// within a single transaction. Rolls back if any error occurs
        /// </summary>
        /// <param name="pQuestion">The question object to insert.</param>
        /// <returns>Returns a Result object indicating success or failure.</returns>
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
            catch (SqlException tEx)
            {
                Log.Error(tEx, tEx.Message);
                return Result<bool>.Failure(ResultStatus.DatabaseError);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                return Result<bool>.Failure(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Deletes a question from the database by its ID. Child records are automatically deleted via cascading.
        /// </summary>
        /// <param name="pId">The ID of the question to delete.</param>
        /// <returns>Returns a Result object indicating success or failure.</returns>
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
                Log.Error(tEx, tEx.Message);
                return Result<bool>.Failure(ResultStatus.DatabaseError);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                return Result<bool>.Failure(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Retrieves all questions from the database without including child type specific fields.
        /// The list is ordered by question order.
        /// </summary>
        /// <returns>Returns a Result object containing all questions or failure with an error code.</returns>
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
                Log.Error(tEx, tEx.Message);
                return Result<List<Question>>.Failure(ResultStatus.DatabaseError);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                return Result<List<Question>>.Failure(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Retrieves the type specific details of a given question (Star, SmileyFaces, or Slider).
        /// </summary>
        /// <param name="pQuestion">The question object whose child details are requested.</param>
        /// <returns>Returns a Result object with the fully populated child question or failure status.</returns>

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
                Log.Error(tEx, tEx.Message);
                return Result<Question>.Failure(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Updates a question and its child type record in a single transaction.
        /// Rolls back on failure.
        /// </summary>
        /// <param name="pQuestion">The question object to update.</param>
        /// <returns>Returns a Result object indicating success or failure.</returns>
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
                Log.Error(tEx, tEx.Message);
                return Result<bool>.Failure(ResultStatus.DatabaseError);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                return Result<bool>.Failure(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Updates a StarQuestion child record in the database.
        /// </summary>
        /// <param name="pStarQuestion">The StarQuestion object with updated data.</param>
        /// <param name="pSqlConnection">The active SQL connection.</param>
        /// <param name="pSqlTransaction">The active SQL transaction.</param>
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
                Log.Error(tEx, tEx.Message);
                throw;
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                throw;
            }
        }

        /// <summary>
        /// Updates a SmileyFacesQuestion child record in the database.
        /// </summary>
        /// <param name="pSmileyQuestion">The SmileyFacesQuestion object with updated data.</param>
        /// <param name="pSqlConnection">The active SQL connection.</param>
        /// <param name="pSqlTransaction">The active SQL transaction.</param>

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
                Log.Error(tEx, tEx.Message);
                throw;
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                throw;
            }
        }

        /// <summary>
        /// Updates a SliderQuestion child record in the database.
        /// </summary>
        /// <param name="pSliderQuestion">The SliderQuestion object with updated data.</param>
        /// <param name="pSqlConnection">The active SQL connection.</param>
        /// <param name="pSqlTransaction">The active SQL transaction.</param>

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
                Log.Error(tEx, tEx.Message);
                throw;
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                throw;
            }
        }

        /// <summary>
        /// Inserts a StarQuestion child record into the database.
        /// </summary>
        /// <param name="pQuestion">The StarQuestion object to insert.</param>
        /// <param name="pConnection">The active SQL connection.</param>
        /// <param name="pTransaction">The active SQL transaction.</param>

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
                Log.Error(tEx, tEx.Message);
                throw;
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                throw;
            }
        }

        /// <summary>
        /// Inserts a SmileyFacesQuestion child record into the database.
        /// </summary>
        /// <param name="pQuestion">The SmileyFacesQuestion object to insert.</param>
        /// <param name="pConnection">The active SQL connection.</param>
        /// <param name="pTransaction">The active SQL transaction.</param>

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
                Log.Error(tEx, tEx.Message);
                throw;
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                throw;
            }
        }

        /// <summary>
        /// Inserts a SliderQuestion child record into the database.
        /// </summary>
        /// <param name="pQuestion">The SliderQuestion object to insert.</param>
        /// <param name="pConnection">The active SQL connection.</param>
        /// <param name="pTransaction">The active SQL transaction.</param>

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
                Log.Error(tEx, tEx.Message);
                throw;
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                throw;
            }
        }

        /// <summary>
        /// Retrieves StarQuestion child data from the database.
        /// </summary>
        /// <param name="pQuestion">The StarQuestion object with the ID populated.</param>
        /// <returns>Returns a Result object with full StarQuestion data or failure status.</returns>

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
                Log.Error(tEx, tEx.Message);
                return Result<Question>.Failure(ResultStatus.DatabaseError);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                return Result<Question>.Failure(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Retrieves SmileyFacesQuestion child data from the database.
        /// </summary>
        /// <param name="pQuestion">The SmileyFacesQuestion object with the ID populated.</param>
        /// <returns>Returns a Result object with full SmileyFacesQuestion data or failure status.</returns>

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
                Log.Error(tEx, tEx.Message);
                return Result<Question>.Failure(ResultStatus.DatabaseError);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                return Result<Question>.Failure(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Retrieves SliderQuestion child data from the database.
        /// </summary>
        /// <param name="pQuestion">The SliderQuestion object with the ID populated.</param>
        /// <returns>Returns a Result object with full SliderQuestion data or failure status.</returns>
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
                Log.Error(tEx, tEx.Message);
                return Result<Question>.Failure(ResultStatus.DatabaseError);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                return Result<Question>.Failure(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Handles a question type change by deleting the old child type record,
        /// updating the base question, and inserting a new child record for the new type,
        /// all within a single transaction.
        /// </summary>
        /// <param name="pQuestion">The question with the updated type.</param>
        /// <param name="pOldQuestionType">The previous question type.</param>
        /// <returns>Returns a Result object indicating success or failure.</returns>

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
                    Log.Error(tEx, tEx.Message);
                    return Result<bool>.Failure(ResultStatus.DatabaseError);
                }
                catch (Exception tEx)
                {
                    Log.Error(tEx, tEx.Message);
                    return Result<bool>.Failure(ResultStatus.UnexpectedError);
                }
            }
        }

        /// <summary>
        /// Updates the base question fields in the Questions table.
        /// </summary>
        /// <param name="pQuestion">The question to update.</param>
        /// <param name="pConnection">The active SQL connection.</param>
        /// <param name="pTransaction">The active SQL transaction.</param>

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
                Log.Error(tEx, tEx.Message);
                throw;
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                throw;
            }
        }
    }
}