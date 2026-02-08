using System;

namespace SurveyQuestionsConfigurator.Models
{
    public abstract class Question
    {
        private string questionText;
        private int questionOrder;
        public QuestionType QuestionType { get; protected set;}

        public string QuestionText
        {
            get => questionText;

            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(nameof(value), "Question text cannot be null or empty.");
                questionText = value;
            }
        }

        public int QuestionOrder
        {
            get => questionOrder;
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException(nameof(value), "Value cannont be less than 1");
                questionOrder = value;
            }
        }
    }
}