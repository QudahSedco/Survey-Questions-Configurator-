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
            mDataRepository.QuestionsTableChanged += OnQuestionTableChanged;
        }

        private void OnQuestionTableChanged()
        {
            QuestionsTableChanged?.Invoke();
        }

        public Result<bool> StartListening()
        {
            return mDataRepository.StartListening();
        }

        public void StopListening()
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
            if (pId < 1)
                return Result<bool>.Failure($"Failed to delete question with invalid Id {pId}");
            return mDataRepository.DeleteQuestionById(pId);
        }

        public Result<bool> AddQuestion(Question pQuestion)
        {
            var tResult = ValidateQuestion(pQuestion);

            if (!tResult.IsSuccess)
                return tResult;

            return mDataRepository.AddQuestion(pQuestion);
        }

        public Result<bool> UpdateQuestion(Question pQuestion)
        {
            var tResult = ValidateQuestion(pQuestion);

            if (!tResult.IsSuccess)
                return tResult;

            return mDataRepository.UpdateQuestion(pQuestion);
        }

        public Result<bool> UpdateChildTableType(Question pQuestion, QuestionType pQuestionOldType)
        {
            var tResult = ValidateQuestion(pQuestion);

            if (!tResult.IsSuccess)
                return tResult;

            return mDataRepository.UpdateChildTableType(pQuestion, pQuestionOldType);
        }

        private Result<bool> ValidateQuestion(Question pQuestion)
        {
            if (pQuestion == null)
                return Result<bool>.Failure("Question cannot be null");

            if (String.IsNullOrWhiteSpace(pQuestion.QuestionText))
            {
                return Result<bool>.Failure("Question text cannot be empty or white space");
            }
            if (pQuestion.QuestionText.Length > 1000)
            {
                return Result<bool>.Failure("Question text cannot be more than 1000 characters");
            }

            switch (pQuestion)
            {
                case StarQuestion tStarQuestion:
                    if (tStarQuestion.NumberOfStars < 1 || tStarQuestion.NumberOfStars > 10)
                        return Result<bool>.Failure("Number of stars cannot be less than 1 or more than 10");
                    break;

                case SmileyFacesQuestion tSmileyFacesQuestion:
                    if (tSmileyFacesQuestion.NumberOfSmileyFaces < 2 || tSmileyFacesQuestion.NumberOfSmileyFaces > 5)
                        return Result<bool>.Failure("Number of smiley faces cannot be less than 2 or more than 5");
                    break;

                case SliderQuestion tSliderQuestion:
                    if (tSliderQuestion.StartValue >= tSliderQuestion.EndValue)
                        return Result<bool>.Failure("Slider start value cannot be more or equal to slider end value");
                    if (string.IsNullOrWhiteSpace(tSliderQuestion.StartValueCaption))
                        return Result<bool>.Failure("Start caption cannot be empty or white space");
                    if (string.IsNullOrWhiteSpace(tSliderQuestion.EndValueCaption))
                        return Result<bool>.Failure("End caption cannot be empty or white space");
                    break;
            }
            return Result<bool>.Success(true);
        }
    }
}