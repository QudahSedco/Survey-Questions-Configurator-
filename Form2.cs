using SurveyQuestionsConfigurator.Models;
using SurveyQuestionsConfigurator.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SurveyQuestionsConfigurator
{
    public partial class Form2 : Form
    {
        private QuestionRepository questionRepository;

        public Form2()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
            comboBox1.DataSource = Enum.GetValues(typeof(QuestionType));
            questionRepository = new QuestionRepository();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QuestionType selectedType =
                (QuestionType)comboBox1.SelectedItem;
            Question question = null;

            if (selectedType == QuestionType.Star)
            {
                question = new StarQuestion
                {
                    QuestionText = textBoxQuestionText.Text,
                    QuestionOrder = (int)numericUpDownQuestionOrder.Value,
                    NumberOfStars = trackBar1.Value
                };
            }
            else if (selectedType == QuestionType.Smiley)
            {
                question = new SmileyFacesQuestion
                {
                    QuestionText = textBoxQuestionText.Text,
                    QuestionOrder = (int)numericUpDownQuestionOrder.Value,
                    NumberOfSmileyFaces = 5
                };
            }

            try
            {
                questionRepository.AddQuestion(question);

                MessageBox.Show(
                    "Star question added successfully.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "An error occurred while saving the question.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private const int MaxStars = 10;

        private void UpdateStars(int value)
        {
            label4.Text =
                new string('★', value) +
                new string('☆', MaxStars - value);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            UpdateStars(trackBar1.Value);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel1.Visible = (QuestionType)comboBox1.SelectedItem == QuestionType.Star;
        }
    }
}