using System;

namespace SurveyQuestionsConfigurator.Models
{
    public class SliderQuestion : Question
    {
        private int startValue;
        private int endValue;
        private string startValueCaption;
        private string endValueCaption;

        public SliderQuestion()
        {
            QuestionType = QuestionType.Slider;
        }

        public int StartValue
        {
            get => startValue;
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        "Start value must be between 0 and 100.");

                if (endValue != 0 && value >= endValue)
                    throw new ArgumentException(
                        "Start value must be less than end value.");

                startValue = value;
            }
        }

        public int EndValue
        {
            get => endValue;
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        "End value must be between 0 and 100.");

                if (value <= startValue)
                    throw new ArgumentException(
                        "End value must be greater than start value.");

                endValue = value;
            }
        }

        public string StartValueCaption
        {
            get => startValueCaption;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(
                        "Start value caption cannot be empty.");

                if (value.Length > 100)
                    throw new ArgumentException(
                        "Start value caption cannot exceed 100 characters.");

            }
        }

        public string EndValueCaption
        {
            get => endValueCaption;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(
                        "End value caption cannot be empty.");

                if (value.Length > 100)
                    throw new ArgumentException(
                        "End value caption cannot exceed 100 characters.");

                
            }
        }
    }
}
