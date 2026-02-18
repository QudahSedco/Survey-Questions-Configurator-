using Serilog;
using SurveyQuestionsConfigurator.Models;
using SurveyQuestionsConfiguratorServices;
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
    public partial class FormMain : Form
    {
        private QuestionService mQuestionService;
        private Dictionary<string, bool> mSortColumnsDictionary;

        private List<Question> mQuestionsList;

        public FormMain()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = true;
            StartPosition = FormStartPosition.CenterScreen;
            mQuestionService = new QuestionService();

            mQuestionService.OnQuestionsChanged += OnQuestionsChanged;

            mSortColumnsDictionary = new Dictionary<string, bool>()
    {
        { "QuestionText", true },
        { "QuestionOrder", true },
        { "QuestionType", true }
    };
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                mQuestionService.StartListenting();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to start question listener");

                MessageBox.Show(
                    this,
                    "Live updates are unavailable.\nThe application will continue to work normally.",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }

            dataGridViewMain.Font = new Font("Segoe UI", 13, FontStyle.Regular);
            dataGridViewMain.Columns.Clear();
            dataGridViewMain.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dataGridViewMain.AutoGenerateColumns = false;
            dataGridViewMain.MultiSelect = false;

            DataGridViewTextBoxColumn colText = new DataGridViewTextBoxColumn();
            colText.HeaderText = "QuestionText";
            colText.DataPropertyName = "QuestionText";
            colText.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridViewMain.Columns.Add(colText);

            DataGridViewTextBoxColumn colOrder = new DataGridViewTextBoxColumn();
            colOrder.HeaderText = "QuestionOrder";
            colOrder.DataPropertyName = "QuestionOrder";
            colOrder.Width = 170;
            dataGridViewMain.Columns.Add(colOrder);

            DataGridViewTextBoxColumn colType = new DataGridViewTextBoxColumn();
            colType.HeaderText = "QuestionType";
            colType.DataPropertyName = "QuestionType";
            colType.Width = 160;
            dataGridViewMain.Columns.Add(colType);

            LoadQuestions();
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
        }

        private void OnQuestionsChanged()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(OnQuestionsChanged));
                return;
            }
            LoadQuestions();
        }

        // passing null object to form 2 to make it in add new question instead of edit mode
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Question tQuestion = null;
            using (var tForm = new Form2(tQuestion))
            {
                tForm.ShowDialog(this);
                LoadQuestions();
            }
        }

        //enables the edit and delete button if there is a selected question from the list

        //deletes selected question
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewMain.CurrentRow == null)
                return;

            Question tSelectedQuestion = (Question)dataGridViewMain.CurrentRow.DataBoundItem;

            try
            {
                DialogResult tAnswer = MessageBox.Show(this,
            $"Are you sure you want to delete the following Question?\n\n{tSelectedQuestion.QuestionText}",
            "Confirm Delete",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question

);
                if (tAnswer == DialogResult.Yes)
                {
                    mQuestionService.DeleteQuestionById(tSelectedQuestion.Id);
                    LoadQuestions();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"An error occurred while deleting the question please try again", "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //loads questions from database
        private void LoadQuestions()
        {
            try
            {
                mQuestionsList = mQuestionService.GetAllQuestions();

                dataGridViewMain.DataSource = mQuestionsList;

                btnDelete.Enabled = false;
                btnUpdate.Enabled = false;
            }
            catch
            {
                MessageBox.Show(this, "An error occurred while retrieving questions from database", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //edit button sends the selected obj and opens form 2 as dialog
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewMain.CurrentRow == null) return;

            Question tSelectedQuestion = (Question)dataGridViewMain.CurrentRow.DataBoundItem;
            try
            {
                tSelectedQuestion = mQuestionService.GetChildQuestion(tSelectedQuestion);
            }
            catch
            {
                MessageBox.Show(this, "An error occurred while retrieving question from database", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var tForm = new Form2(tSelectedQuestion))
            {
                tForm.ShowDialog(this);
                LoadQuestions();
            }
        }

        //sorts questions using the propertyname saved in a dictionary data structure<string,bool>
        //if its true then its ascending if false its descending

        private void SortQuestions(string pColumnName)
        {
            bool tAsc = mSortColumnsDictionary[pColumnName];

            switch (pColumnName)
            {
                case "QuestionText":
                    dataGridViewMain.DataSource = tAsc
                        ? mQuestionsList.OrderBy(q => q.QuestionText).ToList()
                        : mQuestionsList.OrderByDescending(q => q.QuestionText).ToList();
                    break;

                case "QuestionOrder":
                    dataGridViewMain.DataSource = tAsc
                        ? mQuestionsList.OrderBy(q => q.QuestionOrder).ToList()
                        : mQuestionsList.OrderByDescending(q => q.QuestionOrder).ToList();
                    break;

                case "QuestionType":
                    dataGridViewMain.DataSource = tAsc
                        ? mQuestionsList.OrderBy(q => q.QuestionType).ToList()
                        : mQuestionsList.OrderByDescending(q => q.QuestionType).ToList();
                    break;
            }

            mSortColumnsDictionary[pColumnName] = !tAsc;
        }

        private void dataGridViewMain_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string tPropertyName = dataGridViewMain.Columns[e.ColumnIndex].DataPropertyName;
            bool tAscending = mSortColumnsDictionary[tPropertyName];
            foreach (DataGridViewColumn column in dataGridViewMain.Columns)
            {
                column.HeaderText = column.DataPropertyName;
            }
            dataGridViewMain.Columns[e.ColumnIndex].HeaderText = tPropertyName + (tAscending ? " ↑" : " ↓");
            SortQuestions(tPropertyName);
        }

        private void dataGridViewMain_CellContentClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridViewMain.CurrentRow.DataBoundItem != null)
            {
                btnDelete.Enabled = true;
                btnUpdate.Enabled = true;
            }
            else
            {
                btnDelete.Enabled = false;
                btnUpdate.Enabled = false;
            }
        }
    }
}