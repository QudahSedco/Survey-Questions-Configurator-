using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyQuestionsConfigurator.Models
{
    internal class SmileyFacesQuestion : Question
    {
        private int mNumberOfSmileyFaces;

        public SmileyFacesQuestion() : base(QuestionType.Smiley)
        {
        }

        public int NumberOfSmileyFaces
        {
            get => mNumberOfSmileyFaces;
            set
            {
                if (value < 2 || value > 5)
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        "Number of smiley faces must be between 2 and 5.");

                mNumberOfSmileyFaces = value;
            }
        }
    }
}