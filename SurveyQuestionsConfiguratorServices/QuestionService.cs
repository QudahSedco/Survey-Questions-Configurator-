using SurveyQuestionsConfigurator.Models;
using SurveyQuestionsConfigurator.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyQuestionsConfiguratorServices
{
    public class QuestionService
    {
        private QuestionRepository mDataRepository;

        public event Action OnQuestionsChanged;

        public QuestionService()
        {
            mDataRepository = new QuestionRepository();
            mDataRepository.EventOnQuestionsTableChanged += () => OnQuestionsChanged?.Invoke();
        }

        public void StartListenting()
        {
            mDataRepository.StartListening();
        }

        public void StopListenting()
        {
            mDataRepository.StopListening();
        }

        public List<Question> GetAllQuestions()
        {
            return mDataRepository.GetAllQuestions();
        }

        public Question GetChildQuestion(Question pChildQuestion)
        {
            return mDataRepository.GetChildQuestion(pChildQuestion);
        }

        public void DeleteQuestionById(int pId)
        {
            mDataRepository.DeleteQuestionById(pId);
        }

        public void AddQuestion(Question pQuestion)
        {
            mDataRepository.AddQuestion(pQuestion);
        }

        public void UpdateQuestion(Question pQuestion)
        {
            mDataRepository.UpdateQuestion(pQuestion);
        }

        public void UpdateChildTableType(Question pQuestion, QuestionType pQuestionOldType)
        {
            mDataRepository.UpdateChildTableType(pQuestion, pQuestionOldType);
        }
    }
}