using System;

namespace SurveyQuestionsConfigurator.Models
{
    public abstract class Question
    {
        public int Id { get; set; }
        private string mQuestionText;
        private int mQuestionOrder;
        public QuestionType QuestionType { get; }

        protected Question(QuestionType questionType)
        {
            QuestionType = questionType;
        }

        public Question()
        { }

        public string QuestionText
        {
            get => mQuestionText;

            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(nameof(value), "Question text cannot be null or empty.");
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
            get { return $"{QuestionText} - {QuestionType}"; }
        }
    }
}