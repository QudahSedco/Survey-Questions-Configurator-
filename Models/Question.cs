using System;

namespace SurveyQuestionsConfigurator.Models
{
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

        //prop just to display the question text and question type in the list next to each other
        public string DisplayText
        {
            get { return $"{mQuestionText} - ({QuestionType})"; }
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