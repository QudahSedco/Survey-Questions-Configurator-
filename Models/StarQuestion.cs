using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyQuestionsConfigurator.Models
{
    internal class StarQuestion : Question
    {
        private int mNumberStars;

        public StarQuestion()
        {
            QuestionType = QuestionType.Star;
        }

        public int NumberOfStars
        {
            get => mNumberStars;
            set
            {
                if (value < 1 || value > 10)
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        "Number of stars must be between 1 and 10.");

                mNumberStars = value;
            }
        }

    }
}
