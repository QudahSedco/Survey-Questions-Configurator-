using Serilog;
using SurveyQuestionsConfigurator.Models;
using SurveyQuestionsConfiguratorModels;
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
    public partial class MainForm : Form
    {
        private QuestionService mQuestionService;
        private Dictionary<string, bool> mSortColumnsDictionary;
        private List<Question> mQuestionsList;

        public MainForm()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = true;
            StartPosition = FormStartPosition.CenterScreen;
            mQuestionService = new QuestionService();

            mQuestionService.QuestionsTableChanged += OnQuestionsChanged;

            //keeps track of how each column is sorted (true = ascending, false = descending)
            mSortColumnsDictionary = new Dictionary<string, bool>()
    {
        { "QuestionText", true },
        { "QuestionOrder", true },
        { "QuestionType", true }
    };
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            var result = mQuestionService.StartListening();

            if (!result.IsSuccess)
            {
                Log.Error("Failed to start question listener using SqlTableDependency: {Error}", result.Error);
            }

            dataGridViewMain.Font = new Font("Segoe UI", 13, FontStyle.Regular);
            dataGridViewMain.Columns.Clear();
            dataGridViewMain.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewMain.AutoGenerateColumns = false;
            dataGridViewMain.MultiSelect = false;

            //creating the columns for the data grid view
            DataGridViewTextBoxColumn colText = new DataGridViewTextBoxColumn();
            colText.HeaderText = "QuestionText";
            colText.DataPropertyName = "QuestionText";
            colText.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colText.SortMode = DataGridViewColumnSortMode.Automatic;

            dataGridViewMain.Columns.Add(colText);

            DataGridViewTextBoxColumn colOrder = new DataGridViewTextBoxColumn();
            colOrder.HeaderText = "QuestionOrder";
            colOrder.DataPropertyName = "QuestionOrder";
            colOrder.Width = 170;
            colOrder.SortMode = DataGridViewColumnSortMode.Automatic;
            dataGridViewMain.Columns.Add(colOrder);

            DataGridViewTextBoxColumn colType = new DataGridViewTextBoxColumn();
            colType.HeaderText = "QuestionType";
            colType.DataPropertyName = "QuestionType";
            colType.Width = 160;
            colType.SortMode = DataGridViewColumnSortMode.Automatic;
            dataGridViewMain.Columns.Add(colType);
            LoadQuestions();
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
        }

        //Ensures this method runs on the UI thread since UI updates must happen there
        private void OnQuestionsChanged()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(OnQuestionsChanged));
                return;
            }
            LoadQuestions();
        }

        //Passing null to Form2 to open it in "Add New Question" mode instead of edit mode
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Question tQuestion = null;
            using (var tForm = new DialogForm(tQuestion))
            {
                tForm.ShowDialog(this);
                LoadQuestions();
            }
        }

        //Deletes selected question
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewMain.CurrentRow == null)
                return;

            Question tSelectedQuestion = (Question)dataGridViewMain.CurrentRow.DataBoundItem;

            DialogResult tAnswer = MessageBox.Show(this,
        $"Are you sure you want to delete the following Question?\n\n{tSelectedQuestion.QuestionText}",
        "Confirm Delete",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question

);
            if (tAnswer == DialogResult.Yes)
            {
                var tResult = mQuestionService.DeleteQuestionById(tSelectedQuestion.Id);

                if (tResult.IsSuccess)
                    LoadQuestions();
                else
                    MessageBox.Show(this, tResult.Error, "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Loads questions from Database
        private void LoadQuestions()
        {
            var tResult = mQuestionService.GetAllQuestions();

            if (tResult.IsSuccess)
            {
                mQuestionsList = tResult.Value;
                dataGridViewMain.DataSource = mQuestionsList;

                btnDelete.Enabled = false;
                btnUpdate.Enabled = false;
            }
            else
            {
                MessageBox.Show(this, tResult.Error, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        //Edit button sends the selected object and opens dialog form
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewMain.CurrentRow == null) return;

            Question tSelectedQuestion = (Question)dataGridViewMain.CurrentRow.DataBoundItem;

            var tResult = mQuestionService.GetChildQuestion(tSelectedQuestion);
            if (tResult.IsSuccess)

                tSelectedQuestion = tResult.Value;
            else
            {
                MessageBox.Show(this, tResult.Error, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var tForm = new DialogForm(tSelectedQuestion))
            {
                tForm.ShowDialog(this);
                LoadQuestions();
            }
        }

        //sorts questions using the propertyname saved in a dictionary data structure<string,bool>
        //if its true then its ascending if false its descending

        private void SortQuestions(string pColumnName)
        {
            if (mQuestionsList == null || mQuestionsList.Count < 1)
                return;

            bool tAsc = mSortColumnsDictionary[pColumnName];// Gets the current sort direction for the given column from the dictionary

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

        private void DataGridViewMain_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (mQuestionsList == null || mQuestionsList.Count < 1)
                return;

            string tPropertyName = dataGridViewMain.Columns[e.ColumnIndex].DataPropertyName;
            bool tAscending = mSortColumnsDictionary[tPropertyName];

            foreach (DataGridViewColumn column in dataGridViewMain.Columns)
            {
                column.HeaderText = column.DataPropertyName;
            }

            dataGridViewMain.Columns[e.ColumnIndex].HeaderText = tPropertyName + (tAscending ? " ↑" : " ↓");

            SortQuestions(tPropertyName);
        }

        private void DataGridViewMain_CellContentClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridViewMain.CurrentRow != null && dataGridViewMain.CurrentRow.DataBoundItem != null)
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

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            mQuestionService.StopListenting();
        }
    }
}