using SurveyQuestionsConfigurator.Models;
using SurveyQuestionsConfigurator.Repositories;
using SurveyQuestionsConfiguratorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace SurveyQuestionsConfiguratorServices
{
    public class QuestionService
    {
        private QuestionRepository mDataRepository;

        public event Action QuestionsTableChanged;

        public QuestionService()
        {
            mDataRepository = new QuestionRepository();
            mDataRepository.QuestionsTableChanged += OnQuesitonTableChanged;
        }

        private void OnQuesitonTableChanged()
        {
            QuestionsTableChanged?.Invoke();
        }

        public Result<bool> StartListening()
        {
            return mDataRepository.StartListening();
        }

        public void StopListenting()
        {
            mDataRepository.StopListening();
        }

        public Result<List<Question>> GetAllQuestions()
        {
            return mDataRepository.GetAllQuestions();
        }

        public Result<Question> GetChildQuestion(Question pChildQuestion)
        {
            return mDataRepository.GetChildQuestion(pChildQuestion);
        }

        public Result<bool> DeleteQuestionById(int pId)
        {
            return mDataRepository.DeleteQuestionById(pId);
        }

        public Result<bool> AddQuestion(Question pQuestion)
        {
            return mDataRepository.AddQuestion(pQuestion);
        }

        public Result<bool> UpdateQuestion(Question pQuestion)
        {
            return mDataRepository.UpdateQuestion(pQuestion);
        }

        public Result<bool> UpdateChildTableType(Question pQuestion, QuestionType pQuestionOldType)
        {
            return mDataRepository.UpdateChildTableType(pQuestion, pQuestionOldType);
        }
    }
}