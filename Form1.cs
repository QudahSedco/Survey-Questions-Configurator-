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
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = true;
            StartPosition = FormStartPosition.CenterScreen;
            questionRepository = new QuestionRepository();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadQuestions();
            //DisplayText text here is the question obj property that combines question text and question type
            listBox1.DisplayMember = "DisplayText";
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Question tQuestion = null;
            using (var tForm = new Form2(tQuestion))
            {
                tForm.ShowDialog(this);
                LoadQuestions();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                btnDelete.Enabled = true;
                btnUpdate.Enabled = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Question tSelectedQuestion = (Question)listBox1.SelectedItem;

            DialogResult answer = MessageBox.Show(this,
            $"Are you sure you want to delete the following message?\n\n{tSelectedQuestion.QuestionText}",
            "Confirm Delete",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
);
            // dont forget to put try  catch here
            try
            {
                if (answer == DialogResult.Yes)
                {
                    questionRepository.DeleteQuestionById(tSelectedQuestion.Id);
                    LoadQuestions();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    $"An error occurred while deleting the question.{ex}",
                    "Delete Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void LoadQuestions()
        {
            listBox1.DataSource = null;
            listBox1.DataSource = questionRepository.GetAllQuestions();
            listBox1.DisplayMember = "DisplayText";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Question tSelectedQuestion = (Question)listBox1.SelectedItem;

            tSelectedQuestion = questionRepository.GetChildQuestion(tSelectedQuestion);

            using (var tForm = new Form2(tSelectedQuestion))
            {
                tForm.ShowDialog(this);
                LoadQuestions();
            }
        }
    }
}