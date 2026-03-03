using System;

namespace SurveyQuestionsConfigurator.Models
{
    /// <summary>
    /// Represents an abstract base class for a survey question.
    /// Contains common properties shared across all question types,
    /// including text, display order, and type.
    /// </summary>
    public abstract class Question
    {
        private int mId;
        private string mQuestionText;
        private int mQuestionOrder;
        public QuestionType QuestionType { get; }

        public Question(QuestionType questionType)
        {
            QuestionType = questionType;
        }

        public string QuestionText
        {
            get => mQuestionText;

            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(nameof(value), "Question text cannot be null or empty.");

                if (value.Length > 1000)
                    throw new ArgumentOutOfRangeException(nameof(value), "Question text cannot exceed 1000 characters");

                mQuestionText = value;
            }
        }

        public int QuestionOrder
        {
            get => mQuestionOrder;

            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException(nameof(value), "Value cannont be less than 1");

                mQuestionOrder = value;
            }
        }

        public int Id
        {
            get => mId;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Id must be a positive value.");

                mId = value;
            }
        }
    }
}