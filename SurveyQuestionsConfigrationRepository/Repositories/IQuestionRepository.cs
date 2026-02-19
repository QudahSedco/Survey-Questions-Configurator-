using SurveyQuestionsConfigurator.Models;
using SurveyQuestionsConfiguratorModels;
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
        Result<List<Question>> GetAllQuestions();

        Result<bool> AddQuestion(Question pQuestion);

        Result<bool> DeleteQuestionById(int pId);

        Result<bool> UpdateQuestion(Question pQuestion);

        Result<Question> GetChildQuestion(Question pQuestion);
    }
}