using Serilog;
using Serilog.Formatting.Json;
using SurveyQuestionsConfigurator.Models;
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

namespace SurveyQuestionsConfigurator.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private String mConnectionString = ConfigurationManager.ConnectionStrings["SurveyDb"].ConnectionString;

        public QuestionRepository()
        {
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
                    .WriteTo.File(new JsonFormatter(), "logs/SurveyQuestionsConfigurator-.json", rollingInterval: RollingInterval.Day).CreateLogger();
        }

        //inserts into the base table then retirves the Id created by database and uses it to create a child record
        public void AddQuestion(Question pQuestion)
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
                                @"INSERT INTO Questions (question_text, question_order, question_type) VALUES (@text, @order, @type);SELECT CAST(SCOPE_IDENTITY() AS INT);";

                            using (SqlCommand tCmd = new SqlCommand(tSql, tConnection, tTransaction))
                            {
                                tCmd.Parameters.AddWithValue("@text", pQuestion.QuestionText);
                                tCmd.Parameters.AddWithValue("@order", pQuestion.QuestionOrder);
                                tCmd.Parameters.AddWithValue("@type", (int)pQuestion.QuestionType);

                                pQuestion.Id = (int)tCmd.ExecuteScalar();
                            }

                            if (pQuestion is StarQuestion starQuestion)
                            {
                                AddStarQuestion(starQuestion, tConnection, tTransaction);
                            }
                            else if (pQuestion is SmileyFacesQuestion smileyFacesQuestion)
                            {
                                AddSmileyFaceQuestion(smileyFacesQuestion, tConnection, tTransaction);
                            }
                            else if (pQuestion is SliderQuestion sliderQuestion)
                            {
                                AddSliderQuestion(sliderQuestion, tConnection, tTransaction);
                            }

                            tTransaction.Commit();
                        }
                        catch (SqlException ex)
                        {
                            tTransaction.Rollback();
                            Log.Error(ex, "Could Not complete the create question transaction");
                            throw;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Could Not connect to Database");
                    throw;
                }
            }
        }

        public void DeleteQuestionById(int pId)
        {
            using (SqlConnection tConnection = new SqlConnection(mConnectionString))
            {
                String tSql = "DELETE FROM Questions WHERE question_id = @id";

                using (SqlCommand tCmd = new SqlCommand(tSql, tConnection))
                {
                    try
                    {
                        tConnection.Open();
                        tCmd.Parameters.AddWithValue("@id", pId);
                        tCmd.ExecuteNonQuery();
                    }
                    catch (SqlException tEx)
                    {
                        Log.Error(tEx, "Error happened while trying to delete question with ID {QuestionId}", pId);
                        throw;
                    }
                }
            }
        }

        public List<Question> GetAllQuestions()
        {
            var tQuestionsList = new List<Question>();

            string tSql = "SELECT question_id, question_text, question_order, question_type FROM Questions ORDER BY question_order";

            using (SqlConnection tConnection = new SqlConnection(mConnectionString))
            using (SqlCommand tCmd = new SqlCommand(tSql, tConnection))
            {
                try
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
                                    throw new NotSupportedException($"Unknown question type '{tType}'");
                            }

                            tQuestion.Id = tReader.GetInt32(0);
                            tQuestion.QuestionText = tReader.GetString(1);
                            tQuestion.QuestionOrder = tReader.GetInt32(2);

                            tQuestionsList.Add(tQuestion);
                        }
                    }
                }
                catch (SqlException tEx)
                {
                    Log.Error(tEx, "Error getting all questions from DB");
                    throw;
                }
            }

            return tQuestionsList;
        }

        public Question GetChildQuestion(Question pQuestion)
        {
            if (pQuestion is StarQuestion tStarQuestion)
                return GetStarQuestion(tStarQuestion);
            else if (pQuestion is SmileyFacesQuestion tSmileyFaceQuestion)
                return GetSmileyQuestion(tSmileyFaceQuestion);
            else if (pQuestion is SliderQuestion tSliderQuestion)
                return GetSliderQuestion(tSliderQuestion);

            throw new NotSupportedException("Unknown question type");
        }

        public void UpdateQuestion(Question pQuestion)
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
                            string tSql = "UPDATE Questions SET question_text=@questionText,question_order=@questionOrder WHERE question_id=@questionId";
                            using (SqlCommand tCmd = new SqlCommand(tSql, tConnection, tTransaction))
                            {
                                UpdateBaseQuesiton(pQuestion, tConnection, tTransaction);
                            }

                            if (pQuestion is StarQuestion tStarQuestion)
                                UpdateStarQuestion(tStarQuestion, tConnection, tTransaction);
                            else if (pQuestion is SmileyFacesQuestion tSmileyFaceQuestion)
                                UpdateSmileyQuestion(tSmileyFaceQuestion, tConnection, tTransaction);
                            else if (pQuestion is SliderQuestion tSliderQuestion)
                            {
                                UpdateSliderQuestion(tSliderQuestion, tConnection, tTransaction);
                            }

                            tTransaction.Commit();
                        }
                        catch (SqlException tEx)
                        {
                            tTransaction.Rollback();
                            Log.Error(tEx, "Error while Updating question with ID {questionId} in database", pQuestion.Id);
                            throw;
                        }
                    }
                }
                catch (SqlException tEx)
                {
                    Log.Error(tEx, "Error couldnt connect to DB");
                    throw;
                }
            }
        }

        private void UpdateStarQuestion(StarQuestion pStarQuestion, SqlConnection pSqlConnection, SqlTransaction pSqlTransaction)
        {
            string tSql = "UPDATE Star_Questions SET number_of_stars = @numberOfStars WHERE question_id=@id";
            using (SqlCommand tCmd = new SqlCommand(tSql, pSqlConnection, pSqlTransaction))
            {
                tCmd.Parameters.AddWithValue("@id", pStarQuestion.Id);
                tCmd.Parameters.AddWithValue("@numberOfStars", pStarQuestion.NumberOfStars);
                tCmd.ExecuteNonQuery();
            }
        }

        private void UpdateSmileyQuestion(SmileyFacesQuestion pSmileyQuestion, SqlConnection pSqlConnection, SqlTransaction pSqlTransaction)
        {
            string tSql = "UPDATE Smiley_Faces_Questions SET number_of_smiley_faces=@numberOfSmileyFaces WHERE question_id=@id";
            using (SqlCommand tCmd = new SqlCommand(tSql, pSqlConnection, pSqlTransaction))
            {
                tCmd.Parameters.AddWithValue("@id", pSmileyQuestion.Id);
                tCmd.Parameters.AddWithValue("@numberOfSmileyFaces", pSmileyQuestion.NumberOfSmileyFaces);
                tCmd.ExecuteNonQuery();
            }
        }

        private void UpdateSliderQuestion(SliderQuestion pSliderQuestion, SqlConnection pSqlConnection, SqlTransaction pSqlTransaction)
        {
            string tSql = "UPDATE Slider_Questions SET start_value=@startValue,end_value=@endValue,start_value_caption=@startCaption,end_value_caption=@endCaption WHERE question_id=@id";
            using (SqlCommand tCmd = new SqlCommand(tSql, pSqlConnection, pSqlTransaction))
            {
                tCmd.Parameters.AddWithValue("@id", pSliderQuestion.Id);
                tCmd.Parameters.AddWithValue("@startValue", pSliderQuestion.StartValue);
                tCmd.Parameters.AddWithValue("@endValue", pSliderQuestion.EndValue);
                tCmd.Parameters.AddWithValue("@startCaption", pSliderQuestion.StartValueCaption);
                tCmd.Parameters.AddWithValue("@endCaption", pSliderQuestion.EndValueCaption);
                tCmd.ExecuteNonQuery();
            }
        }

        private void AddStarQuestion(StarQuestion pQuestion, SqlConnection pConnection, SqlTransaction pTransaction)
        {
            string tSql = @"INSERT INTO Star_Questions (question_id, number_of_stars) VALUES (@id, @stars)";

            using (SqlCommand tCmd = new SqlCommand(tSql, pConnection, pTransaction))
            {
                tCmd.Parameters.AddWithValue("@id", pQuestion.Id);
                tCmd.Parameters.AddWithValue("@stars", pQuestion.NumberOfStars);

                tCmd.ExecuteNonQuery();
            }
        }

        private void AddSmileyFaceQuestion(SmileyFacesQuestion pQuestion, SqlConnection pConnection, SqlTransaction pTransaction)
        {
            string tSql = "INSERT INTO Smiley_Faces_Questions(question_id,number_of_smiley_faces) VALUES (@id,@NumberOfSmileyFaces)";

            using (SqlCommand tCmd = new SqlCommand(tSql, pConnection, pTransaction))
            {
                tCmd.Parameters.AddWithValue("@id", pQuestion.Id);
                tCmd.Parameters.AddWithValue("@NumberOfSmileyFaces", pQuestion.NumberOfSmileyFaces);
                tCmd.ExecuteNonQuery();
            }
        }

        private void AddSliderQuestion(SliderQuestion pQuestion, SqlConnection pConnection, SqlTransaction pTransaction)
        {
            string tSql = @"INSERT INTO Slider_Questions(question_id,start_value,end_value,start_value_caption,end_value_caption)
                 VALUES (@id,@startValue,@endValue,@startCaption,@endCaption)";
            using (SqlCommand tCmd = new SqlCommand(tSql, pConnection, pTransaction))
            {
                tCmd.Parameters.AddWithValue("@id", pQuestion.Id);
                tCmd.Parameters.AddWithValue("@startValue", pQuestion.StartValue);
                tCmd.Parameters.AddWithValue("@endValue", pQuestion.EndValue);
                tCmd.Parameters.AddWithValue("@startCaption", pQuestion.StartValueCaption);
                tCmd.Parameters.AddWithValue("@endCaption", pQuestion.EndValueCaption);
                tCmd.ExecuteNonQuery();
            }
        }

        private StarQuestion GetStarQuestion(StarQuestion pQuestion)
        {
            using (SqlConnection tConnection = new SqlConnection(mConnectionString))
            {
                try
                {
                    tConnection.Open();

                    string tSql = "SELECT number_of_stars FROM Star_Questions WHERE question_id = @id";

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
                }
                catch (SqlException tEx)
                {
                    Log.Error(tEx, "Error while retrieving StarQuestion with id {QuestionId} from Database", pQuestion.Id);
                    throw;
                }
            }

            return pQuestion;
        }

        private SmileyFacesQuestion GetSmileyQuestion(SmileyFacesQuestion pQuestion)

        {
            using (SqlConnection tConnection = new SqlConnection(mConnectionString))
            {
                try
                {
                    tConnection.Open();
                    String tSql = "SELECT number_of_smiley_faces FROM Smiley_Faces_Questions WHERE question_id=@id";

                    using (SqlCommand tCmd = new SqlCommand(tSql, tConnection))
                    {
                        tCmd.Parameters.AddWithValue("@id", pQuestion.Id);
                        using (SqlDataReader tReader = tCmd.ExecuteReader())
                        {
                            if (tReader.Read())
                            {
                                {
                                    pQuestion.NumberOfSmileyFaces = tReader.GetInt32(0);
                                }
                            }
                        }
                    }
                }
                catch (SqlException tEx)
                {
                    Log.Error(tEx, "Error while retrieving Smiley question with id {QuestionId} from Database", pQuestion.Id);
                    throw;
                }
                return pQuestion;
            }
        }

        private SliderQuestion GetSliderQuestion(SliderQuestion pQuestion)
        {
            using (SqlConnection tConnection = new SqlConnection(mConnectionString))
            {
                try
                {
                    tConnection.Open();
                    String tSql = "SELECT start_value,end_value,start_value_caption,end_value_caption FROM Slider_Questions WHERE question_id=@id";

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
                }
                catch (SqlException tEx)
                {
                    Log.Error(tEx, "Error while retrieving Slider Question with Id {QuestionId} from Database", pQuestion.Id);
                    throw;
                }
                return pQuestion;
            }
        }

        //removes old child record updates the base record and inserts the new child type record
        public void UpdateChildTableType(Question pQuestion, QuestionType pOldQuestionType)
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
                                    tSql = "DELETE FROM Star_Questions WHERE question_id = @id";
                                    break;

                                case QuestionType.Smiley:
                                    tSql = "DELETE FROM Smiley_Faces_Questions WHERE question_id = @id";
                                    break;

                                case QuestionType.Slider:
                                    tSql = "DELETE FROM Slider_Questions WHERE question_id = @id";
                                    break;

                                default:
                                    throw new ArgumentOutOfRangeException(nameof(pOldQuestionType));
                            }

                            using (SqlCommand tCmd = new SqlCommand(tSql, tConnection, tTransaction))
                            {
                                tCmd.Parameters.AddWithValue("@id", pQuestion.Id);
                                tCmd.ExecuteNonQuery();
                            }

                            UpdateBaseQuesiton(pQuestion, tConnection, tTransaction);

                            if (pQuestion is StarQuestion starQuestion)
                            {
                                AddStarQuestion(starQuestion, tConnection, tTransaction);
                            }
                            else if (pQuestion is SmileyFacesQuestion smileyFacesQuestion)
                            {
                                AddSmileyFaceQuestion(smileyFacesQuestion, tConnection, tTransaction);
                            }
                            else if (pQuestion is SliderQuestion sliderQuestion)
                            {
                                AddSliderQuestion(sliderQuestion, tConnection, tTransaction);
                            }

                            tTransaction.Commit();
                        }
                        catch (SqlException tEx)
                        {
                            tTransaction.Rollback();
                            Log.Error(tEx, "Error updating question {QuestionId}", pQuestion.Id);
                            throw;
                        }
                    }
                }
                catch (SqlException tEx)
                {
                    Log.Error(tEx, "Failed to connect to database");
                    throw;
                }
            }
        }

        public void UpdateBaseQuesiton(Question pQuestion, SqlConnection pConnection, SqlTransaction pTransaction)
        {
            string tSql = "UPDATE Questions SET question_text=@questionText,question_order=@questionOrder,question_type = @questionType WHERE question_id=@questionId";
            using (SqlCommand tCmd = new SqlCommand(tSql, pConnection, pTransaction))
            {
                tCmd.Parameters.AddWithValue("@questionId", pQuestion.Id);
                tCmd.Parameters.AddWithValue("@questionText", pQuestion.QuestionText);
                tCmd.Parameters.AddWithValue("@questionOrder", pQuestion.QuestionOrder);
                tCmd.Parameters.AddWithValue("@questionType", (int)pQuestion.QuestionType);

                tCmd.ExecuteNonQuery();
            }
        }
    }
}