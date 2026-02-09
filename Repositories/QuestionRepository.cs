using SurveyQuestionsConfigurator.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
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
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string baseSql =
                            @"INSERT INTO Questions (question_text, question_order, question_type)
                      VALUES (@text, @order, @type);
                      SELECT CAST(SCOPE_IDENTITY() AS INT);";

                        using (SqlCommand cmd = new SqlCommand(baseSql, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@text", pQuestion.QuestionText);
                            cmd.Parameters.AddWithValue("@order", pQuestion.QuestionOrder);
                            cmd.Parameters.AddWithValue("@type", (int)pQuestion.QuestionType);

                            pQuestion.Id = (int)cmd.ExecuteScalar();
                        }

                        if (pQuestion is StarQuestion starQuestion)
                        {
                            AddStarQuestion(starQuestion, connection, transaction);
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
            throw new NotImplementedException();
        }

        public List<Question> GetAllQuestions()
        {
            var list = new List<Question>();

            string sql = @"
        SELECT
            question_id,
            question_text,
            question_order,
            question_type
        FROM Questions
        ORDER BY question_order";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        QuestionType type =
                            (QuestionType)reader.GetInt32(3);

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

                        list.Add(question);
                    }
                }
            }

            return list;
        }

        public Question GetQuestionByID(int pId)
        {
            throw new NotImplementedException();
        }

        public void UpdateQuestion(Question pQuestion)
        {
            throw new NotImplementedException();
        }

        private void AddStarQuestion(StarQuestion pQuestion, SqlConnection connection, SqlTransaction transaction)
        {
            string sql = @"INSERT INTO Star_Questions (question_id, number_of_stars) VALUES (@id, @stars)";

            using (SqlCommand cmd = new SqlCommand(sql, connection, transaction))
            {
                cmd.Parameters.AddWithValue("@id", pQuestion.Id);
                cmd.Parameters.AddWithValue("@stars", pQuestion.NumberOfStars);

                cmd.ExecuteNonQuery();
            }
        }
    }
}