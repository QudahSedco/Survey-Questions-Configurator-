using SurveyQuestionsConfigurator.Models;
using SurveyQuestionsConfigurator.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SurveyQuestionsConfigurator
{
    public partial class Form1 : Form
    {
        private QuestionRepository questionRepository;

        public Form1()
        {
            InitializeComponent();
            questionRepository = new QuestionRepository();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = Enum.GetValues(typeof(QuestionType));
            listBox1.DataSource = questionRepository.GetAllQuestions();
            //question text here is the question obj property
            listBox1.DisplayMember = "QuestionText";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QuestionType selectedType =
                (QuestionType)comboBox1.SelectedItem;

            if (selectedType != QuestionType.Star)
            {
                MessageBox.Show(
                    "Only Star questions are supported right now.",
                    "Not Implemented",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            var question = new StarQuestion
            {
                QuestionText = textBox1.Text,
                QuestionOrder = (int)numericUpDown1.Value,
                NumberOfStars = 5
            };

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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}