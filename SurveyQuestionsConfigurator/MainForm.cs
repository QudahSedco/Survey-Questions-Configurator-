using Serilog;
using SurveyQuestionsConfigurator.Models;
using SurveyQuestionsConfiguratorModels;
using SurveyQuestionsConfiguratorModels.Result;
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
using System.Net.NetworkInformation;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using SortOrder = System.Windows.Forms.SortOrder;

namespace SurveyQuestionsConfigurator
{
    public partial class MainForm : Form
    {
        private QuestionService mQuestionService;
        private Dictionary<string, bool> mSortColumnsDictionary;
        private List<Question> mQuestionsList;
        private const string UNEXPECTED_ERROR_MESSAGE = "An unexpected error occurred";
        private const string COL_QUESTION_TEXT = "QuestionText";
        private const string COL_QUESTION_ORDER = "QuestionOrder";
        private const string COL_QUESTION_TYPE = "QuestionType";
        private const string EN_LANGUAGE = "English";
        private const string AR_LANGUAGE = "Arabic";

        public MainForm()
        {
            try
            {
                InitializeComponent();
                LanguagesComboBox.Items.Add("English");
                LanguagesComboBox.Items.Add("Arabic");
                LanguagesComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                LanguagesComboBox.SelectedIndex = 0;
                FormBorderStyle = FormBorderStyle.FixedDialog;
                MinimizeBox = true;
                StartPosition = FormStartPosition.CenterScreen;
                mQuestionService = new QuestionService();

                mQuestionService.QuestionsTableChanged += OnQuestionsChanged;

                //keeps track of how each column is sorted (true = ascending, false = descending)
                mSortColumnsDictionary = new Dictionary<string, bool>()
    {
        { COL_QUESTION_TEXT, true },
        { COL_QUESTION_ORDER, true },
        { COL_QUESTION_TYPE, true }
    };
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        private void FormMain_Load(object pSender, EventArgs pE)
        {
            try
            {
                var tResult = mQuestionService.StartListening();
                if (!tResult.IsSuccess)
                {
                    ShowErrorBox(tResult.Status);
                }

                dataGridViewMain.Font = new Font("Segoe UI", 13, FontStyle.Regular);
                dataGridViewMain.Columns.Clear();
                dataGridViewMain.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridViewMain.AutoGenerateColumns = false;
                dataGridViewMain.MultiSelect = false;

                //creating the columns for the data grid view
                DataGridViewTextBoxColumn tColText = new DataGridViewTextBoxColumn();
                tColText.HeaderText = "Question text";
                tColText.DataPropertyName = COL_QUESTION_TEXT;
                tColText.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                tColText.SortMode = DataGridViewColumnSortMode.Automatic;
                dataGridViewMain.Columns.Add(tColText);

                DataGridViewTextBoxColumn tColOrder = new DataGridViewTextBoxColumn();
                tColOrder.HeaderText = "Order";
                tColOrder.DataPropertyName = COL_QUESTION_ORDER;
                tColOrder.Width = 170;
                tColOrder.SortMode = DataGridViewColumnSortMode.Automatic;
                dataGridViewMain.Columns.Add(tColOrder);

                DataGridViewTextBoxColumn tColType = new DataGridViewTextBoxColumn();
                tColType.HeaderText = "Type";
                tColType.DataPropertyName = COL_QUESTION_TYPE;
                tColType.Width = 160;
                tColType.SortMode = DataGridViewColumnSortMode.Automatic;
                dataGridViewMain.Columns.Add(tColType);
                LoadQuestions();
                btnDelete.Enabled = false;
                btnUpdate.Enabled = false;
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        //Loads questions everytime a change happens in the database
        //The method is subscribed to Service event
        private void OnQuestionsChanged()
        {
            try
            {
                if (InvokeRequired)//Ensures this method runs on the UI thread since UI updates must happen there
                {
                    Invoke(new Action(OnQuestionsChanged));
                    return;
                }
                LoadQuestions();
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        //Passing null to add/edit form to open it in "Add New Question" mode instead of edit mode
        private void btnAdd_Click(object pSender, EventArgs pE)
        {
            try
            {
                Question tQuestion = null;
                using (var tForm = new AddOrEditForm(tQuestion))
                {
                    tForm.ShowDialog(this);
                    if (tForm.DialogResult == DialogResult.OK)
                    {
                        LoadQuestions();
                    }
                }
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        //Deletes selected question
        private void btnDelete_Click(object pSender, EventArgs pE)
        {
            try
            {
                if (dataGridViewMain.CurrentRow == null)
                    return;

                Question tSelectedQuestion = (Question)dataGridViewMain.CurrentRow.DataBoundItem;

                DialogResult tAnswer = CustomMessageBox.Show($"{Resources.ConfirmDelete}\n\n{tSelectedQuestion.QuestionText}",
     Resources.DeleteCaption, ButtonTypes.YesNo, IconTypes.Question);

                if (tAnswer == DialogResult.Yes)
                {
                    var tResult = mQuestionService.DeleteQuestionById(tSelectedQuestion.Id);

                    if (tResult.IsSuccess)
                        LoadQuestions();
                    else
                        ShowErrorBox(tResult.Status);
                }
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        //Loads questions from Database
        private void LoadQuestions()
        {
            try
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
                    ShowErrorBox(tResult.Status);
                    return;
                }
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        //Edit button passes the selected question object and opens add/edit form in edit mode
        private void btnEdit_Click(object pSender, EventArgs pE)
        {
            try
            {
                if (dataGridViewMain.CurrentRow == null) return;

                Question tSelectedQuestion = (Question)dataGridViewMain.CurrentRow.DataBoundItem;

                var tResult = mQuestionService.GetChildQuestion(tSelectedQuestion);
                if (tResult.IsSuccess)

                    tSelectedQuestion = tResult.Value;
                else
                {
                    ShowErrorBox(tResult.Status);
                    return;
                }

                using (var tForm = new AddOrEditForm(tSelectedQuestion))
                {
                    tForm.ShowDialog(this);
                    if (tForm.DialogResult == DialogResult.OK)
                    {
                        LoadQuestions();
                    }
                }
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        //sorts questions using the propertyname saved in a dictionary data structure<string,bool>
        //if its true then its ascending if false its descending

        private void SortQuestions(string pColumnName, int pColumnIndex)
        {
            try
            {
                if (mQuestionsList == null || mQuestionsList.Count < 1)
                    return;

                bool tAsc = mSortColumnsDictionary[pColumnName];// Gets the current sort direction for the given column from the dictionary

                switch (pColumnName)
                {
                    case COL_QUESTION_TEXT:
                        dataGridViewMain.DataSource = tAsc
                            ? mQuestionsList.OrderBy(q => q.QuestionText).ToList()
                            : mQuestionsList.OrderByDescending(q => q.QuestionText).ToList();
                        break;

                    case COL_QUESTION_ORDER:
                        dataGridViewMain.DataSource = tAsc
                            ? mQuestionsList.OrderBy(q => q.QuestionOrder).ToList()
                            : mQuestionsList.OrderByDescending(q => q.QuestionOrder).ToList();
                        break;

                    case COL_QUESTION_TYPE:
                        dataGridViewMain.DataSource = tAsc
                            ? mQuestionsList.OrderBy(q => q.QuestionType).ToList()
                            : mQuestionsList.OrderByDescending(q => q.QuestionType).ToList();
                        break;
                }
                dataGridViewMain.Columns[pColumnIndex]
           .HeaderCell.SortGlyphDirection =
               tAsc ? SortOrder.Ascending : SortOrder.Descending;

                mSortColumnsDictionary[pColumnName] = !tAsc;
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        //calls sort questions method on the column clicked on
        private void DataGridViewMain_ColumnHeaderMouseClick(object pSender, DataGridViewCellMouseEventArgs pE)
        {
            try
            {
                if (mQuestionsList == null || mQuestionsList.Count < 1)
                    return;

                string tPropertyName = dataGridViewMain.Columns[pE.ColumnIndex].DataPropertyName;
                bool tAscending = mSortColumnsDictionary[tPropertyName];

                SortQuestions(tPropertyName, pE.ColumnIndex);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        private void DataGridViewMain_CellContentClick(object pSender, DataGridViewCellMouseEventArgs pE)
        {
            try
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
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        //stop sqltable dependency once the form is closed
        private void FormMain_FormClosing(object pSender, FormClosingEventArgs pE)
        {
            try
            {
                mQuestionService.StopListening();
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, "Error while trying to stop sqldependency");
            }
        }

        //switches the langauge based on the user selection in the langauge combobox
        private void SwitchLanguage(string pCulture)
        {
            try
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(pCulture);
                Thread.CurrentThread.CurrentCulture = new CultureInfo(pCulture);

                var tRes = new ComponentResourceManager(typeof(MainForm));

                tRes.ApplyResources(this, "$this");//applies to form level properties only

                foreach (Control tControl in Controls) //applies to all controls and the contorls inside them
                    ApplyResourcesRecursive(tRes, tControl);

                // DataGridView columns are not controls (they do not inherit from Control),
                // so they are not localized by ApplyResources / ApplyResourcesRecursive.
                // Column headers must therefore be localized manually using the Resources file.
                foreach (DataGridViewColumn col in dataGridViewMain.Columns)
                {
                    col.HeaderText = Resources.ResourceManager.GetString(col.DataPropertyName);
                }
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        // Recursively applies localization resources to all child controls.
        private void ApplyResourcesRecursive(ComponentResourceManager pRes, Control pControl)
        {
            try
            {
                pRes.ApplyResources(pControl, pControl.Name);

                foreach (Control child in pControl.Controls)
                    ApplyResourcesRecursive(pRes, child);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        //shows custom error box made this method to write less code when throwing erros
        private void ShowErrorBox(ResultStatus pStatus)
        {
            try
            {
                CustomMessageBox.Show(Resources.ResourceManager.GetString(pStatus.ToString()), "Error", ButtonTypes.Ok, IconTypes.Error);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE + "while tryin to show custom message box");
                MessageBox.Show(UNEXPECTED_ERROR_MESSAGE);
            }
        }

        //changes languge based on the option that the user chooses
        private void LanguagesComboBox_SelectedIndexChanged(object pSender, EventArgs pE)
        {
            try
            {
                string tSelectedLang = LanguagesComboBox.SelectedItem.ToString();

                switch (tSelectedLang)
                {
                    case EN_LANGUAGE:
                        SwitchLanguage("en");
                        break;

                    case AR_LANGUAGE:
                        SwitchLanguage("ar");
                        break;
                }
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        //opens connection setting form and allows the user to change the database connection string
        //if successful starts listening for changes on the new database
        private void btnChangeDataBase_Click(object pSender, EventArgs pE)
        {
            try
            {
                using (var tConnectionForm = new ConnectionSettingsForm())
                {
                    tConnectionForm.ShowDialog();
                    // Create new service so it reads the updated connection string
                    if (tConnectionForm.DialogResult == DialogResult.OK)
                    {
                        mQuestionService?.StopListening();
                        mQuestionService = new QuestionService();
                        mQuestionService.QuestionsTableChanged += OnQuestionsChanged;
                        mQuestionService?.StartListening();
                        LoadQuestions();
                    }
                }
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }
    }
}