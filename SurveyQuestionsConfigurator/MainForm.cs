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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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

        private void FormMain_Load(object pSender, EventArgs pE)
        {
            var tResult = mQuestionService.StartListening();

            if (!tResult.IsSuccess)
            {
                Log.Error("Failed to start question listener using SqlTableDependency: {Error}", tResult.MessageKey);//temp
            }

            dataGridViewMain.Font = new Font("Segoe UI", 13, FontStyle.Regular);
            dataGridViewMain.Columns.Clear();
            dataGridViewMain.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewMain.AutoGenerateColumns = false;
            dataGridViewMain.MultiSelect = false;

            //creating the columns for the data grid view
            DataGridViewTextBoxColumn tColText = new DataGridViewTextBoxColumn();
            tColText.HeaderText = "QuestionText";
            tColText.DataPropertyName = "QuestionText";
            tColText.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            tColText.SortMode = DataGridViewColumnSortMode.Automatic;

            dataGridViewMain.Columns.Add(tColText);

            DataGridViewTextBoxColumn tColOrder = new DataGridViewTextBoxColumn();
            tColOrder.HeaderText = "QuestionOrder";
            tColOrder.DataPropertyName = "QuestionOrder";
            tColOrder.Width = 170;
            tColOrder.SortMode = DataGridViewColumnSortMode.Automatic;
            dataGridViewMain.Columns.Add(tColOrder);

            DataGridViewTextBoxColumn tColType = new DataGridViewTextBoxColumn();
            tColType.HeaderText = "QuestionType";
            tColType.DataPropertyName = "QuestionType";
            tColType.Width = 160;
            tColType.SortMode = DataGridViewColumnSortMode.Automatic;
            dataGridViewMain.Columns.Add(tColType);
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
        private void btnAdd_Click(object pSender, EventArgs pE)
        {
            Question tQuestion = null;
            using (var tForm = new DialogForm(tQuestion))
            {
                tForm.ShowDialog(this);
                LoadQuestions();
            }
        }

        //Deletes selected question
        private void btnDelete_Click(object pSender, EventArgs pE)
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
                    MessageBox.Show(this, tResult.MessageKey, "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error); // temp
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
                MessageBox.Show(this, tResult.MessageKey, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // temp
                return;
            }
        }

        //Edit button passes the selected object and opens dialog form
        private void btnEdit_Click(object pSender, EventArgs pE)
        {
            if (dataGridViewMain.CurrentRow == null) return;

            Question tSelectedQuestion = (Question)dataGridViewMain.CurrentRow.DataBoundItem;

            var tResult = mQuestionService.GetChildQuestion(tSelectedQuestion);
            if (tResult.IsSuccess)

                tSelectedQuestion = tResult.Value;
            else
            {
                MessageBox.Show(this, tResult.MessageKey, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // temp
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

        private void DataGridViewMain_ColumnHeaderMouseClick(object pSender, DataGridViewCellMouseEventArgs pE)
        {
            if (mQuestionsList == null || mQuestionsList.Count < 1)
                return;

            string tPropertyName = dataGridViewMain.Columns[pE.ColumnIndex].DataPropertyName;
            bool tAscending = mSortColumnsDictionary[tPropertyName];

            foreach (DataGridViewColumn tColumn in dataGridViewMain.Columns)
            {
                tColumn.HeaderText = tColumn.DataPropertyName;
            }

            dataGridViewMain.Columns[pE.ColumnIndex].HeaderText = tPropertyName + (tAscending ? " ↑" : " ↓");

            SortQuestions(tPropertyName);
        }

        private void DataGridViewMain_CellContentClick(object pSender, DataGridViewCellMouseEventArgs pE)
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

        private void FormMain_FormClosing(object pSender, FormClosingEventArgs pE)
        {
            mQuestionService.StopListening();
        }

        private void button1_click(object sender, EventArgs e)
        {
            if (Thread.CurrentThread.CurrentUICulture.Name.StartsWith("en"))
            {
                SwitchLanguage("ar");
            }
            else
            {
                SwitchLanguage("en");
            }
        }

        private void SwitchLanguage(string culture)
        {
            bool isArabic = culture.StartsWith("ar");
            foreach (DataGridViewColumn col in dataGridViewMain.Columns)
            {
                col.DefaultCellStyle.Alignment = isArabic
                    ? DataGridViewContentAlignment.MiddleRight
                    : DataGridViewContentAlignment.MiddleLeft;

                col.HeaderCell.Style.Alignment = isArabic
                    ? DataGridViewContentAlignment.MiddleRight
                    : DataGridViewContentAlignment.MiddleLeft;
            }
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
            Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);

            ApplyGridHeaders();

            foreach (DataGridViewColumn col in dataGridViewMain.Columns)
                col.DefaultCellStyle.Alignment = isArabic ? DataGridViewContentAlignment.MiddleRight : DataGridViewContentAlignment.MiddleLeft;

            var resources = new ComponentResourceManager(typeof(MainForm));
            ApplyResources(resources, this);
        }

        private void ApplyResources(ComponentResourceManager res, Control control)
        {
            res.ApplyResources(control, control.Name);

            foreach (Control child in control.Controls)
            {
                ApplyResources(res, child);
            }
        }

        private void ApplyGridHeaders()
        {
            if (Thread.CurrentThread.CurrentUICulture.Name.StartsWith("ar"))
            {
                dataGridViewMain.Columns[0].HeaderText = "نص السؤال";
                dataGridViewMain.Columns[1].HeaderText = "الترتيب";
                dataGridViewMain.Columns[2].HeaderText = "النوع";
                dataGridViewMain.Columns[0].DisplayIndex = 2;
                dataGridViewMain.Columns[1].DisplayIndex = 1;
                dataGridViewMain.Columns[2].DisplayIndex = 0;
            }
            else
            {
                dataGridViewMain.Columns[0].HeaderText = "Question Text";
                dataGridViewMain.Columns[1].HeaderText = "Order";
                dataGridViewMain.Columns[2].HeaderText = "Type";
            }
        }
    }
}