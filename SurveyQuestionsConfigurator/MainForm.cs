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

namespace SurveyQuestionsConfigurator
{
    public partial class MainForm : Form
    {
        private QuestionService mQuestionService;
        private Dictionary<string, bool> mSortColumnsDictionary;
        private List<Question> mQuestionsList;
        private const string UNEXPECTED_ERROR_MESSAGE = "An unexpected error occurred";

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
        { "QuestionText", true },
        { "QuestionOrder", true },
        { "QuestionType", true }
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

                dataGridViewMain.Font = new Font("Segoe UI", 13, FontStyle.Regular);
                dataGridViewMain.Columns.Clear();
                dataGridViewMain.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridViewMain.AutoGenerateColumns = false;
                dataGridViewMain.MultiSelect = false;

                // this is what shows in the grid

                //creating the columns for the data grid view
                DataGridViewTextBoxColumn tColText = new DataGridViewTextBoxColumn();
                tColText.Name = "Grid_QuestionText";
                tColText.HeaderText = Resources.QuestionText;
                tColText.DataPropertyName = "QuestionText";
                tColText.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                tColText.SortMode = DataGridViewColumnSortMode.Automatic;

                dataGridViewMain.Columns.Add(tColText);

                DataGridViewTextBoxColumn tColOrder = new DataGridViewTextBoxColumn();
                tColOrder.Name = "Grid_QuestionOrder";
                tColOrder.HeaderText = "Order";

                tColOrder.DataPropertyName = "QuestionOrder";
                tColOrder.Width = 170;
                tColOrder.SortMode = DataGridViewColumnSortMode.Automatic;
                dataGridViewMain.Columns.Add(tColOrder);

                DataGridViewTextBoxColumn tColType = new DataGridViewTextBoxColumn();

                tColType.Name = "Grid_QuestionType";
                tColType.HeaderText = "Type";
                tColType.DataPropertyName = "QuestionType";
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

        //Ensures this method runs on the UI thread since UI updates must happen there
        private void OnQuestionsChanged()
        {
            try
            {
                if (InvokeRequired)
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

        //Passing null to Form2 to open it in "Add New Question" mode instead of edit mode
        private void btnAdd_Click(object pSender, EventArgs pE)
        {
            try
            {
                Question tQuestion = null;
                using (var tForm = new AddOrEditForm(tQuestion))
                {
                    tForm.ShowDialog(this);
                }
                LoadQuestions();
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE + "while adding a question");
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

        //Edit button passes the selected object and opens dialog form
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

        private void SortQuestions(string pColumnName)
        {
            try
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
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        private void DataGridViewMain_ColumnHeaderMouseClick(object pSender, DataGridViewCellMouseEventArgs pE)
        {
            try
            {
                if (mQuestionsList == null || mQuestionsList.Count < 1)
                    return;

                string tPropertyName = dataGridViewMain.Columns[pE.ColumnIndex].DataPropertyName;
                bool tAscending = mSortColumnsDictionary[tPropertyName];

                SortQuestions(tPropertyName);
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

        private void SwitchLanguage(string culture)
        {
            try
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
                Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);

                var res = new ComponentResourceManager(typeof(MainForm));

                res.ApplyResources(this, "$this");//applies to form level properties only

                foreach (Control c in Controls)
                    ApplyResourcesRecursive(res, c);

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
        private void ApplyResourcesRecursive(ComponentResourceManager res, Control control)
        {
            try
            {
                res.ApplyResources(control, control.Name);

                foreach (Control child in control.Controls)
                    ApplyResourcesRecursive(res, child);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        private void ShowErrorBox(ResultStatus pStatus)
        {
            try
            {
                CustomMessageBox.Show(Resources.ResourceManager.GetString(pStatus.ToString()), "Error", ButtonTypes.Ok, IconTypes.Error);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                MessageBox.Show("Unexpected error happened");
            }
        }

        //changes languge based on the option that the user chooses
        private void LanguagesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string tSelectedLang = LanguagesComboBox.SelectedItem.ToString();

                switch (tSelectedLang)
                {
                    case "English":
                        SwitchLanguage("en");
                        break;

                    case "Arabic":
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

        private void button1_Click(object sender, EventArgs e)
        {
            using (var connectionForm = new ConnectionSettingsForm())
            {
                // Show it as a modal dialog
                connectionForm.ShowDialog();
                try
                {
                    mQuestionService?.StopListening();

                    // Create new service so it reads the updated connection string
                    mQuestionService = new QuestionService();
                    mQuestionService.QuestionsTableChanged += OnQuestionsChanged;
                    LoadQuestions();
                }
                catch (Exception ex)
                {
                    Log.Error(ex, UNEXPECTED_ERROR_MESSAGE);
                    ShowErrorBox(ResultStatus.UnexpectedError);
                }
            }
        }
    }
}