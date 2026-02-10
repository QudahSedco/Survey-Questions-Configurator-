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
using System.Windows.Forms;

namespace SurveyQuestionsConfigurator.Repositories
{
    internal class QuestionRepository : IQuestionRepository
    {
        private String ConnectionString = ConfigurationManager.ConnectionStrings["SurveyDb"].ConnectionString;

        public void AddQuestion(Question pQuestion)
        {
            using (SqlConnection tConnection = new SqlConnection(ConnectionString))
            {
                tConnection.Open();

                using (SqlTransaction transaction = tConnection.BeginTransaction())
                {
                    try
                    {
                        string tbBaseSql =
                            @"INSERT INTO Questions (question_text, question_order, question_type)
                      VALUES (@text, @order, @type);
                      SELECT CAST(SCOPE_IDENTITY() AS INT);";

                        using (SqlCommand tCmd = new SqlCommand(tbBaseSql, tConnection, transaction))
                        {
                            tCmd.Parameters.AddWithValue("@text", pQuestion.QuestionText);
                            tCmd.Parameters.AddWithValue("@order", pQuestion.QuestionOrder);
                            tCmd.Parameters.AddWithValue("@type", (int)pQuestion.QuestionType);

                            pQuestion.Id = (int)tCmd.ExecuteScalar();
                        }

                        if (pQuestion is StarQuestion starQuestion)
                        {
                            AddStarQuestion(starQuestion, tConnection, transaction);
                        }
                        else if (pQuestion is SmileyFacesQuestion smileyFacesQuestion)
                        {
                            AddSmileyFaceQuestion(smileyFacesQuestion, tConnection, transaction);
                        }
                        else if (pQuestion is SliderQuestion sliderQuestion)
                        {
                            AddSliderQuestion(sliderQuestion, tConnection, transaction);
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void DeleteQuestionById(int pId)
        {
            using (SqlConnection tConnection = new SqlConnection(ConnectionString))
            {
                String tSql = "DELETE FROM Questions WHERE question_id = @id";

                using (SqlCommand tCmd = new SqlCommand(tSql, tConnection))
                {
                    tConnection.Open();
                    tCmd.Parameters.AddWithValue("@id", pId);
                    tCmd.ExecuteNonQuery();
                }
            }
        }

        public List<Question> GetAllQuestions()
        {
            var tList = new List<Question>();

            string tSql = "SELECT question_id, question_text, question_order, question_type FROM Questions ORDER BY question_order";

            using (SqlConnection tConnection = new SqlConnection(ConnectionString))
            using (SqlCommand tCmd = new SqlCommand(tSql, tConnection))
            {
                tConnection.Open();

                using (SqlDataReader reader = tCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        QuestionType type = (QuestionType)reader.GetInt32(3);

                        Question question;

                        switch (type)
                        {
                            case QuestionType.Smiley:
                                question = new SmileyFacesQuestion();
                                break;

                            case QuestionType.Slider:
                                question = new SliderQuestion();
                                break;

                            case QuestionType.Star:
                                question = new StarQuestion();
                                break;

                            default:
                                throw new NotSupportedException(
                                    $"Unknown question type: {type}");
                        }

                        question.Id = reader.GetInt32(0);
                        question.QuestionText = reader.GetString(1);
                        question.QuestionOrder = reader.GetInt32(2);

                        tList.Add(question);
                    }
                }
            }

            return tList;
        }

        public Question GetChildQuestion(Question pQuestion)
        {
            if (pQuestion is StarQuestion starQuestion)
                return GetStarQuestion(starQuestion);
            else if (pQuestion is SmileyFacesQuestion smileyFaceQuestion)
                return GetSmileyQuestion(smileyFaceQuestion);
            else if (pQuestion is SliderQuestion sliderQuestion)
                return GetSliderQuestion(sliderQuestion);

            return null;
        }

        public void UpdateQuestion(Question pQuestion)
        {
            using (SqlConnection tConnection = new SqlConnection(ConnectionString))
            {
                tConnection.Open();
                using (SqlTransaction tTransaction = tConnection.BeginTransaction())
                {
                    try
                    {
                        string tSql = "UPDATE Questions SET question_text=@questionText,question_order=@questionOrder WHERE question_id=@questionId";
                        using (SqlCommand tCommand = new SqlCommand(tSql, tConnection, tTransaction))
                        {
                            tCommand.Parameters.AddWithValue("@questionId", pQuestion.Id);
                            tCommand.Parameters.AddWithValue("@questionText", pQuestion.QuestionText);
                            tCommand.Parameters.AddWithValue("@questionOrder", pQuestion.QuestionOrder);
                            tCommand.ExecuteNonQuery();
                        }

                        if (pQuestion is StarQuestion starQuestion)
                            UpdateStarQuestion(starQuestion, tConnection, tTransaction);
                        else if (pQuestion is SmileyFacesQuestion SmileyFaceQuestion)
                            UpdateSmileyQuestion(SmileyFaceQuestion, tConnection, tTransaction);
                        else if (pQuestion is SliderQuestion sliderQuestion)
                        {
                            UpdateSliderQuestion(sliderQuestion, tConnection, tTransaction);
                        }

                        tTransaction.Commit();
                    }
                    catch
                    {
                        tTransaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private void UpdateStarQuestion(StarQuestion starQuestion, SqlConnection pSqlConnection, SqlTransaction pSqlTransaction)
        {
            string tSql = "UPDATE Star_Questions SET number_of_stars = @numberOfStars WHERE question_id=@id";
            using (SqlCommand tSqlComman = new SqlCommand(tSql, pSqlConnection, pSqlTransaction))
            {
                tSqlComman.Parameters.AddWithValue("@id", starQuestion.Id);
                tSqlComman.Parameters.AddWithValue("@numberOfStars", starQuestion.NumberOfStars);
                tSqlComman.ExecuteNonQuery();
            }
        }

        private void UpdateSmileyQuestion(SmileyFacesQuestion smileyQuestion, SqlConnection pSqlConnection, SqlTransaction pSqlTransaction)
        {
            string tSql = "UPDATE Smiley_Faces_Questions SET number_of_smiley_faces=@numberOfSmileyFaces WHERE questions_id=@id";
            using (SqlCommand tSqlComman = new SqlCommand(tSql, pSqlConnection, pSqlTransaction))
            {
                tSqlComman.Parameters.AddWithValue("@id", smileyQuestion.Id);
                tSqlComman.Parameters.AddWithValue("@numberOfSmileyFaces", smileyQuestion.NumberOfSmileyFaces);
                tSqlComman.ExecuteNonQuery();
            }
        }

        private void UpdateSliderQuestion(SliderQuestion sliderQuestion, SqlConnection pSqlConnection, SqlTransaction pSqlTransaction)
        {
            string tSql = "UPDATE Slider_Questions SET start_value=@startValue,end_value=@endValue,start_value_caption=@startCaption,end_value_caption=@endCaption WHERE questions_id=@id";
            using (SqlCommand tSqlComman = new SqlCommand(tSql, pSqlConnection, pSqlTransaction))
            {
                tSqlComman.Parameters.AddWithValue("@id", sliderQuestion.Id);
                tSqlComman.Parameters.AddWithValue("@startValue", sliderQuestion.StartValue);
                tSqlComman.Parameters.AddWithValue("@endValue", sliderQuestion.EndValue);
                tSqlComman.Parameters.AddWithValue("@startCaption", sliderQuestion.StartValueCaption);
                tSqlComman.Parameters.AddWithValue("@endCaption", sliderQuestion.EndValueCaption);
                tSqlComman.ExecuteNonQuery();
            }
        }

        private void AddStarQuestion(StarQuestion pQuestion, SqlConnection pConnection, SqlTransaction pTransaction)
        {
            string tSql = @"INSERT INTO Star_Questions (question_id, number_of_stars) VALUES (@id, @stars)";

            using (SqlCommand cmd = new SqlCommand(tSql, pConnection, pTransaction))
            {
                cmd.Parameters.AddWithValue("@id", pQuestion.Id);
                cmd.Parameters.AddWithValue("@stars", pQuestion.NumberOfStars);

                cmd.ExecuteNonQuery();
            }
        }

        private void AddSmileyFaceQuestion(SmileyFacesQuestion pQuestion, SqlConnection pConnection, SqlTransaction pTransaction)
        {
            string tSql = "INSERT INTO Smiley_Faces_Questions(questions_id,number_of_smiley_faces) VALUES (@id,@NumberOfSmileyFaces)";

            using (SqlCommand tCmd = new SqlCommand(tSql, pConnection, pTransaction))
            {
                tCmd.Parameters.AddWithValue("@id", pQuestion.Id);
                tCmd.Parameters.AddWithValue("@NumberOfSmileyFaces", pQuestion.NumberOfSmileyFaces);
                tCmd.ExecuteNonQuery();
            }
        }

        private void AddSliderQuestion(SliderQuestion pQuestion, SqlConnection pConnection, SqlTransaction pTransaction)
        {
            string tSql = @"INSERT INTO Slider_Questions(questions_id,start_value,end_value,start_value_caption,end_value_caption)
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
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = "SELECT number_of_stars FROM Star_Questions WHERE question_id = @id";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", pQuestion.Id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            pQuestion.NumberOfStars = reader.GetInt32(0);
                        }
                    }
                }
            }

            return pQuestion;
        }

        private SmileyFacesQuestion GetSmileyQuestion(SmileyFacesQuestion pQuestion)

        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                String sql = "SELECT number_of_smiley_faces FROM Smiley_Faces_Questions WHERE questions_id=@id";

                using (SqlCommand tComman = new SqlCommand(sql, conn))
                {
                    tComman.Parameters.AddWithValue("@id", pQuestion.Id);
                    using (SqlDataReader reader = tComman.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            {
                                pQuestion.NumberOfSmileyFaces = reader.GetInt32(0);
                            }
                        }
                    }
                }
                return pQuestion;
            }
        }

        private SliderQuestion GetSliderQuestion(SliderQuestion pQuestion)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                String sql = "SELECT start_value,end_value,start_value_caption,end_value_caption FROM Slider_Questions WHERE questions_id=@id";

                using (SqlCommand tComman = new SqlCommand(sql, conn))
                {
                    tComman.Parameters.AddWithValue("@id", pQuestion.Id);
                    using (SqlDataReader reader = tComman.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            {
                                pQuestion.StartValue = reader.GetInt32(0);
                                pQuestion.EndValue = reader.GetInt32(1);
                                pQuestion.StartValueCaption = reader.GetString(2);
                                pQuestion.EndValueCaption = reader.GetString(3);
                            }
                        }
                    }
                }
                return pQuestion;
            }
        }
    }
}