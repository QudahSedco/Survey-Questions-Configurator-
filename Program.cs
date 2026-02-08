using SurveyQuestionsConfigurator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SurveyQuestionsConfigurator
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            StarQuestion q = new StarQuestion();
            q.QuestionText = "How satisfied are you?";
            q.QuestionOrder = 0;
            q.NumberOfStars = 3;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            

            


        }
    }
}
