using SurveyQuestionsConfigurator.Models;
using SurveyQuestionsConfigurator.Repositories;
using SurveyQuestionsConfiguratorModels;
using SurveyQuestionsConfiguratorModels.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Serilog;

namespace SurveyQuestionsConfiguratorServices
{
    public class QuestionService
    {
        private QuestionRepository mDataRepository;

        public event Action QuestionsTableChanged;

        private const string UNEXPECTED_ERROR_MESSAGE = "Unexpected error happend";

        public QuestionService()
        {
            try
            {
                mDataRepository = new QuestionRepository();
                mDataRepository.QuestionsTableChanged += OnQuestionTableChanged;
            }
            catch
            {
                throw;
            }
        }

        private void OnQuestionTableChanged()
        {
            try
            {
                QuestionsTableChanged?.Invoke();
            }
            catch
            {
                throw;
            }
        }

        public Result<bool> StartListening()
        {
            try
            {
                return mDataRepository.StartListening();
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                return Result<bool>.Failure(ResultStatus.UnexpectedError);
            }
        }

        public void StopListening()
        {
            try
            {
                mDataRepository.StopListening();
            }
            catch
            {
                throw;
            }
        }

        public Result<List<Question>> GetAllQuestions()
        {
            try
            {
                return mDataRepository.GetAllQuestions();
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                return Result<List<Question>>.Failure(ResultStatus.UnexpectedError);
            }
        }

        public Result<Question> GetChildQuestion(Question pChildQuestion)
        {
            try
            {
                return mDataRepository.GetChildQuestion(pChildQuestion);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                return Result<Question>.Failure(ResultStatus.UnexpectedError);
            }
        }

        public Result<bool> DeleteQuestionById(int pId)
        {
            try
            {
                return mDataRepository.DeleteQuestionById(pId);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                return Result<bool>.Failure(ResultStatus.UnexpectedError);
            }
        }

        public Result<bool> AddQuestion(Question pQuestion)
        {
            try
            {
                var tResult = ValidateQuestion(pQuestion);

                if (tResult.Status != ResultStatus.Success)
                    return tResult;

                return mDataRepository.AddQuestion(pQuestion);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                return Result<bool>.Failure(ResultStatus.UnexpectedError);
            }
        }

        public Result<bool> UpdateQuestion(Question pQuestion)
        {
            try
            {
                var tResult = ValidateQuestion(pQuestion);

                if (!tResult.IsSuccess)
                    return tResult;

                return mDataRepository.UpdateQuestion(pQuestion);
            }
            catch
            {
                return Result<bool>.Failure(ResultStatus.UnexpectedError);
            }
        }

        public Result<bool> UpdateChildTableType(Question pQuestion, QuestionType pQuestionOldType)
        {
            try
            {
                var tResult = ValidateQuestion(pQuestion);

                if (!tResult.IsSuccess)
                    return tResult;

                return mDataRepository.UpdateChildTableType(pQuestion, pQuestionOldType);
            }
            catch (Exception tE)
            {
                Log.Error(tE, UNEXPECTED_ERROR_MESSAGE);
                return Result<bool>.Failure(ResultStatus.UnexpectedError);
            }
        }

        //method used to validate the question
        //made this instead of copy pasting the code every time i want to validate
        private Result<bool> ValidateQuestion(Question pQuestion)
        {
            try
            {
                if (pQuestion == null)
                    return Result<bool>.Failure(ResultStatus.NullError);

                if (String.IsNullOrWhiteSpace(pQuestion.QuestionText))
                {
                    return Result<bool>.Failure(ResultStatus.NullOrWhiteSpaceError);
                }
                if (pQuestion.QuestionText.Length > 1000)
                {
                    return Result<bool>.Failure(ResultStatus.ValidationError);
                }
                if (pQuestion.QuestionOrder < 1)
                    return Result<bool>.Failure(ResultStatus.ValidationError);

                if (!Enum.IsDefined(typeof(QuestionType), pQuestion.QuestionType))
                    return Result<bool>.Failure(ResultStatus.ValidationError);

                switch (pQuestion)
                {
                    case StarQuestion tStarQuestion:
                        if (tStarQuestion.NumberOfStars < 1 || tStarQuestion.NumberOfStars > 10)
                            return Result<bool>.Failure(ResultStatus.OutOfRangeError);
                        break;

                    case SmileyFacesQuestion tSmileyFacesQuestion:

                        if (tSmileyFacesQuestion.NumberOfSmileyFaces < 2 || tSmileyFacesQuestion.NumberOfSmileyFaces > 5)
                            return Result<bool>.Failure(ResultStatus.OutOfRangeError);

                        break;

                    case SliderQuestion tSliderQuestion:

                        if (tSliderQuestion.StartValue < 0 || tSliderQuestion.StartValue > 99)
                            return Result<bool>.Failure(ResultStatus.OutOfRangeError);
                        if (tSliderQuestion.EndValue < 1 || tSliderQuestion.EndValue > 100)
                            return Result<bool>.Failure(ResultStatus.OutOfRangeError);
                        if (tSliderQuestion.StartValue >= tSliderQuestion.EndValue)
                            return Result<bool>.Failure(ResultStatus.ValidationError);
                        if (string.IsNullOrWhiteSpace(tSliderQuestion.StartValueCaption))
                            return Result<bool>.Failure(ResultStatus.ValidationError);
                        if (tSliderQuestion.StartValueCaption.Length > 100)
                            return Result<bool>.Failure(ResultStatus.ValidationError);
                        if (string.IsNullOrWhiteSpace(tSliderQuestion.EndValueCaption))
                            return Result<bool>.Failure(ResultStatus.ValidationError);
                        if (tSliderQuestion.EndValueCaption.Length > 100)
                            return Result<bool>.Failure(ResultStatus.ValidationError);
                        break;
                }

                return Result<bool>.Success(true);
            }
            catch
            {
                throw;
            }
        }
    }
}