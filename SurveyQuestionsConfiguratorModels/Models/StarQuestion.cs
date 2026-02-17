using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyQuestionsConfigurator.Models
{
    public class StarQuestion : Question
    {
        private int mNumberStars;

        public StarQuestion() : base(QuestionType.Star)
        {
        }

        public int NumberOfStars
        {
            get => mNumberStars;
            set
            {
                if (value < 1 || value > 10)
                    throw new ArgumentOutOfRangeException(nameof(value), "Number of stars must be between 1 and 10.");

                mNumberStars = value;
            }
        }
    }
}