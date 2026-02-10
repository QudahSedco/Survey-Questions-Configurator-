using System;

namespace SurveyQuestionsConfigurator.Models
{
    public class SliderQuestion : Question
    {
        private int mStartValue;
        private int mEndValue;
        private string mStartValueCaption;
        private string mEndValueCaption;

        public SliderQuestion() : base(QuestionType.Slider)
        {
        }

        public int StartValue
        {
            get => mStartValue;
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        "Start value must be between 0 and 100.");

                if (mEndValue != 0 && value >= mEndValue)
                    throw new ArgumentException(
                        "Start value must be less than end value.");

                mStartValue = value;
            }
        }

        public int EndValue
        {
            get => mEndValue;
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        "End value must be between 0 and 100.");

                if (value <= mStartValue)
                    throw new ArgumentException(
                        "End value must be greater than start value.");

                mEndValue = value;
            }
        }

        public string StartValueCaption
        {
            get => mStartValueCaption;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(
                        "Start value caption cannot be empty.");

                if (value.Length > 100)
                    throw new ArgumentException(
                        "Start value caption cannot exceed 100 characters.");
                mStartValueCaption = value;
            }
        }

        public string EndValueCaption
        {
            get => mEndValueCaption;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(
                        "End value caption cannot be empty.");

                if (value.Length > 100)
                    throw new ArgumentException(
                        "End value caption cannot exceed 100 characters.");
                mEndValueCaption = value;
            }
        }
    }
}