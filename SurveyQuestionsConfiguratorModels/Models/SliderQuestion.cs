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
            private set
            {
                if (value < 0 || value > 99)
                    throw new ArgumentOutOfRangeException(nameof(value), "Start value must be between 0 and 99.");

                mStartValue = value;
            }
        }

        public int EndValue
        {
            get => mEndValue;
            private set
            {
                if (value < 1 || value > 100)
                    throw new ArgumentOutOfRangeException(nameof(value), "End value must be between 1 and 100.");

                mEndValue = value;
            }
        }

        public string StartValueCaption
        {
            get => mStartValueCaption;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Start value caption cannot be empty.");

                if (value.Length > 100)
                    throw new ArgumentException("Start value caption cannot exceed 100 characters.");

                mStartValueCaption = value;
            }
        }

        public string EndValueCaption
        {
            get => mEndValueCaption;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("End value caption cannot be empty.");

                if (value.Length > 100)
                    throw new ArgumentException("End value caption cannot exceed 100 characters.");

                mEndValueCaption = value;
            }
        }

        //makes sure that the start and end value are in a valid range before calling the setters
        public void SetRange(int pStartValue, int pEndValue)
        {
            if (pStartValue >= pEndValue)
                throw new ArgumentException("Start value must be less than end value.");

            StartValue = pStartValue;
            EndValue = pEndValue;
        }
    }
}