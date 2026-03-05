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

{    /// <summary>
     /// Service layer responsible for managing survey questions.
     /// Acts as an intermediary between the UI layer and the data repository,
     /// handling validation and business logic
     /// </summary>
    public class QuestionService
    {
        private QuestionRepository mDataRepository;

        public event Action QuestionsTableChanged;

        /// <summary>
        /// Initializes the QuestionService and sets up the question repository,
        /// including subscribing to the QuestionsTableChanged event.
        /// </summary>
        public QuestionService()
        {
            try
            {
                mDataRepository = new QuestionRepository();
                mDataRepository.QuestionsTableChanged += OnQuestionTableChanged;
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                throw;
            }
        }

        /// <summary>
        /// Invokes QuestionsTableChanged event
        /// </summary>
        private void OnQuestionTableChanged()
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
        /// Starts the SQL table dependency listener.
        /// </summary>
        /// <returns>
        /// Returns a result object indicating whether the operation succeeded or failed and failure type.
        /// </returns>
        public Result<bool> StartListening()
        {
            try
            {
                return mDataRepository.StartListening();
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                return Result<bool>.Failure(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Stops the SQL table dependency listener.
        /// </summary>
        public void StopListening()
        {
            try
            {
                mDataRepository.StopListening();
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                throw;
            }
        }

        /// <summary>
        /// Tests the database connection using the provided connection string.
        /// </summary>
        /// <param name="pConnectionString">The connection string to test.</param>
        /// <returns>A Result object indicating success or failure of the connection attempt.</returns>
        public Result<bool> TestConnection(string pConnectionString)
        {
            try
            {
                return mDataRepository.TestConnection(pConnectionString);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                return Result<bool>.Failure(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Retrieves all questions from the database.
        /// </summary>
        /// <returns>A Result object containing a list of questions on success, or failure status on error.</returns>
        public Result<List<Question>> GetAllQuestions()
        {
            try
            {
                return mDataRepository.GetAllQuestions();
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                return Result<List<Question>>.Failure(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Retrieves the detailed child record of the given question from the database.
        /// </summary>
        /// <param name="pChildQuestion">The parent or reference question to fetch details for.</param>
        /// <returns>A Result object containing the child question on success, or failure status on error.</returns>
        public Result<Question> GetChildQuestion(Question pChildQuestion)
        {
            try
            {
                return mDataRepository.GetChildQuestion(pChildQuestion);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                return Result<Question>.Failure(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Deletes the question with the specified ID from the database.
        /// </summary>
        /// <param name="pId">The ID of the question to delete.</param>
        /// <returns>A Result object indicating success or failure of the deletion.</returns>
        public Result<bool> DeleteQuestionById(int pId)
        {
            try
            {
                return mDataRepository.DeleteQuestionById(pId);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                return Result<bool>.Failure(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Adds a new question to the database after validating it.
        /// </summary>
        /// <param name="pQuestion">The question object to add.</param>
        /// <returns>A Result object indicating whether the addition succeeded or failed.</returns>
        public Result<bool> AddQuestion(Question pQuestion)
        {
            try
            {
                var tResult = ValidateQuestion(pQuestion);

                if (!tResult.IsSuccess)
                    return tResult;

                return mDataRepository.AddQuestion(pQuestion);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                return Result<bool>.Failure(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Updates an existing question in the database after validating it.
        /// </summary>
        /// <param name="pQuestion">The question object to update.</param>
        /// <returns>A Result object indicating whether the update succeeded or failed.</returns>
        public Result<bool> UpdateQuestion(Question pQuestion)
        {
            try
            {
                var tResult = ValidateQuestion(pQuestion);

                if (!tResult.IsSuccess)
                    return tResult;

                return mDataRepository.UpdateQuestion(pQuestion);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                return Result<bool>.Failure(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Updates the table type of a child question from the old type to the new type specified in the question entity.
        /// Validates the question before performing the update.
        /// </summary>
        /// <param name="pQuestion">The updated question object.</param>
        /// <param name="pQuestionOldType">The previous type of the question before update.</param>
        /// <returns>A Result object indicating whether the update succeeded or failed.</returns>
        public Result<bool> UpdateChildTableType(Question pQuestion, QuestionType pQuestionOldType)
        {
            try
            {
                var tResult = ValidateQuestion(pQuestion);

                if (!tResult.IsSuccess)
                    return tResult;

                return mDataRepository.UpdateChildTableType(pQuestion, pQuestionOldType);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                return Result<bool>.Failure(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Validates the properties of a question object based on its type.
        /// Checks for null, empty text, text length, valid order, enum type,
        /// and specific rules for Star, SmileyFaces, and Slider questions.
        /// </summary>
        /// <param name="pQuestion">The question object to validate.</param>
        /// <returns>
        /// A Result object with Success=true if valid, otherwise Failure with appropriate ResultStatus.
        /// </returns>
        private Result<bool> ValidateQuestion(Question pQuestion)
        {
            try
            {
                if (pQuestion == null)
                    return Result<bool>.Failure(ResultStatus.NullError);

                if (string.IsNullOrWhiteSpace(pQuestion.QuestionText))

                    return Result<bool>.Failure(ResultStatus.NullOrWhiteSpaceError);

                if (pQuestion.QuestionText.Length > 1000)

                    return Result<bool>.Failure(ResultStatus.LengthTooLongError);

                if (pQuestion.QuestionOrder < 1)
                    return Result<bool>.Failure(ResultStatus.ValidationError);

                if (!Enum.IsDefined(typeof(QuestionType), pQuestion.QuestionType))
                    return Result<bool>.Failure(ResultStatus.UnknownTypeError);

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
                            return Result<bool>.Failure(ResultStatus.NullOrWhiteSpaceError);

                        if (tSliderQuestion.StartValueCaption.Length > 100)
                            return Result<bool>.Failure(ResultStatus.LengthTooLongError);

                        if (string.IsNullOrWhiteSpace(tSliderQuestion.EndValueCaption))
                            return Result<bool>.Failure(ResultStatus.NullOrWhiteSpaceError);

                        if (tSliderQuestion.EndValueCaption.Length > 100)
                            return Result<bool>.Failure(ResultStatus.LengthTooLongError);
                        break;
                }
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                throw;
            }

            return Result<bool>.Success(true);
        }
    }
}