using SurveyQuestionsConfigurator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SurveyQuestionsConfigurator.Repositories
{
    internal interface IQuestionRepository
    {
        List<Question> GetAllQuestions();

        void AddQuestion(Question pQuestion);

        void DeleteQuestionById(int pId);

        void UpdateQuestion(Question pQuestion);

        Question GetChildQuestion(Question pQuestion);
    }
}