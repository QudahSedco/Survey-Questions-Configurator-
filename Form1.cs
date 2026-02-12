using Serilog;
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

        private enum SortingMode
        {
            Alphabetical,
            QuestionOrder,
            QuestionType
        }

        private SortingMode mSortingMode;
        private List<Question> mQuestionsList;

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
                MessageBox.Show(this, $"An error occurred while deleting the question.{ex}", "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //loads questions from database
        private void LoadQuestions()
        {
            try
            {
                listBox1.DataSource = null;

                mQuestionsList = questionRepository.GetAllQuestions();

                listBox1.DataSource = mQuestionsList;

                listBox1.DisplayMember = "DisplayText";
            }
            catch
            {
                MessageBox.Show(this, "An error occured while retriving questions from database", "database error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void button1_Click_1(object sender, EventArgs e)
        {
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        //sort alphabitcially
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            mSortingMode = SortingMode.Alphabetical;
            FilterQuestions();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            mSortingMode = SortingMode.QuestionOrder;
            FilterQuestions();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            mSortingMode = SortingMode.QuestionType;
            FilterQuestions();
        }

        private void FilterQuestions()
        {
            if (mSortingMode == SortingMode.Alphabetical)
                listBox1.DataSource = mQuestionsList.OrderBy(q => q.QuestionText).ToList();
            if (mSortingMode == SortingMode.QuestionOrder)
                listBox1.DataSource = mQuestionsList.OrderBy(q => q.QuestionOrder).ToList();
            if (mSortingMode == SortingMode.QuestionType)
                listBox1.DataSource = mQuestionsList.OrderBy(q => q.QuestionType).ToList();
        }
    }
}