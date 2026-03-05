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
    /// <summary>
    /// Main application form for managing survey questions.
    /// Display all questions in a sortable DataGridView
    /// Add, edit, and delete questions using the AddOrEditForm
    /// Change application language (English/Arabic)
    /// Update database connection settings dynamically
    /// Handle UI updates and localization
    /// Respond to database changes via events
    /// </summary>

    public partial class MainForm : Form
    {
        private QuestionService mQuestionService;
        private Dictionary<string, bool> mSortColumnsDictionary;
        private List<Question> mQuestionsList;
        private List<Language> mLanguageList;
        private const string COL_QUESTION_TEXT = "QuestionText";
        private const string COL_QUESTION_ORDER = "QuestionOrder";
        private const string COL_QUESTION_TYPE = "QuestionType";

        private const string ENGLISH_LANGUAGE = "English";
        private const string ARABIC_LANGUAGE = "Arabic";

        /// <summary>
        /// Initializes the main form, sets up language options,
        /// configures form properties, initializes the question service,
        /// subscribes to the QuestionsTableChanged event, and
        /// sets up the initial sort direction for each data grid column.
        /// </summary>

        public MainForm()
        {
            try
            {
                InitializeComponent();
                mLanguageList = new List<Language>
{
    new Language { Key = ENGLISH_LANGUAGE, Display = Resources.English_Language },
    new Language { Key = ARABIC_LANGUAGE, Display = Resources.Arabic_language }
};

                LanguagesComboBox.DataSource = mLanguageList;
                LanguagesComboBox.DisplayMember = "Display";
                LanguagesComboBox.ValueMember = "Key";
                LanguagesComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                FormBorderStyle = FormBorderStyle.FixedDialog;
                MinimizeBox = true;
                StartPosition = FormStartPosition.CenterScreen;

                mQuestionService = new QuestionService();
                mQuestionService.QuestionsTableChanged += OnQuestionsChanged;
                // Localize the QuestionType column values on every cell render based on current culture
                dataGridViewMain.CellFormatting += DataGridViewMain_CellFormatting;
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
                Log.Error(tEx, tEx.Message);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Starts listening for changes in the questions database via QuestionService.
        /// Configures and adds DataGridView columns for displaying questions.
        /// Loads existing questions into the DataGridView.
        /// Initializes button states (Delete, Update) as disabled.
        /// </summary>
        /// <param name="pSender">The object that raised the event.</param>
        /// <param name="pE">Event arguments associated with the load event.</param>
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
                dataGridViewMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                //Creating the columns for the data grid view
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
                Log.Error(tEx, tEx.Message);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Event handler that responds to changes in the questions database.
        /// Ensures the method executes on the UI thread if invoked from a background thread.
        /// Calls LoadQuestions() to refresh the DataGridView with the latest questions.
        /// This method is subscribed to the QuestionService.QuestionsTableChanged event.
        /// </summary>
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
                Log.Error(tEx, tEx.Message);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Opens the AddOrEditForm in add mode to create a new question.
        /// Passes null to the form to indicate creation rather than editing.
        /// Refreshes the questions list after the form closes if a new question was added.
        /// </summary>
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
                Log.Error(tEx, tEx.Message);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Deletes the currently selected question from the database.
        /// Prompts the user for confirmation before deletion.
        /// Refreshes the questions list if the deletion is successful.
        /// </summary>
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
                Log.Error(tEx, tEx.Message);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Retrieves all questions from the database using the question service,
        /// binds them to the main data grid view, and disables the Delete and Update buttons
        /// until a question is selected.
        /// Shows an error box if the retrieval fails.
        /// </summary>
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
                Log.Error(tEx, tEx.Message);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Opens the AddOrEditForm in edit mode for the selected question.
        /// Retrieves the full child question type (Star, Smiley, or Slider) since
        /// the DataGridView holds only base Question objects. Reloads the questions
        /// after the form closes if changes were saved.
        /// </summary>
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
                Log.Error(tEx, tEx.Message);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Sorts the questions in the DataGridView based on the specified column.
        /// Uses the mSortColumnsDictionary<String ,bool> to determine the current sort direction
        /// for each column (true = ascending, false = descending) and toggles it
        /// after sorting updates the column header to display the correct sort glyph.
        /// </summary>
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
                //displays an arrow based on sort order
                dataGridViewMain.Columns[pColumnIndex].HeaderCell.SortGlyphDirection = tAsc ? SortOrder.Ascending : SortOrder.Descending;

                mSortColumnsDictionary[pColumnName] = !tAsc;
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Handles the event when a DataGridView column header is clicked.
        /// Retrieves the column's property name and index then calls SortQuestions to
        /// sort the grid based on that column.
        /// </summary>
        private void DataGridViewMain_ColumnHeaderMouseClick(object pSender, DataGridViewCellMouseEventArgs pE)
        {
            try
            {
                if (mQuestionsList == null || mQuestionsList.Count < 1)
                    return;

                string tPropertyName = dataGridViewMain.Columns[pE.ColumnIndex].DataPropertyName;

                SortQuestions(tPropertyName, pE.ColumnIndex);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Handles the event when a cell in the DataGridView is clicked.
        /// Enables or disables the Delete and Update buttons based on whether
        /// a row with a valid bound question is selected.
        /// </summary>
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
                Log.Error(tEx, tEx.Message);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Stops listening to database changes via SQL dependency when the form is closing.
        /// Ensures that background database notifications are properly disposed.
        /// </summary>
        private void FormMain_FormClosing(object pSender, FormClosingEventArgs pE)
        {
            try
            {
                mQuestionService.StopListening();
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
            }
        }

        /// <summary>
        /// Switches the application's language based on the selected culture.
        /// Updates all form controls and DataGridView column headers with localized resources.
        /// Suspends layout during updates to improve performance and prevent UI flicker.
        /// </summary>
        /// <param name="pCulture">The culture code to switch to (e.g., "en" or "ar").</param>
        private void SwitchLanguage(string pCulture)
        {
            try
            {
                // Temporarily suspends layout logic for this control and its children
                // Prevents the form from repainting after each control update, which avoids flicker or freezing
                this.SuspendLayout();

                Thread.CurrentThread.CurrentUICulture = new CultureInfo(pCulture);
                Thread.CurrentThread.CurrentCulture = new CultureInfo(pCulture);

                var tRes = new ComponentResourceManager(typeof(MainForm));

                mLanguageList[0].Display = Resources.English_Language;
                mLanguageList[1].Display = Resources.Arabic_language;

                LanguagesComboBox.Refresh();

                tRes.ApplyResources(this, "$this");//applies to form level properties only

                foreach (Control tControl in Controls) //applies to all controls and the contorls inside them
                    ApplyResourcesRecursive(tRes, tControl);

                // DataGridView columns are not controls (they do not inherit from Control),
                // so they are not localized by ApplyResources / ApplyResourcesRecursive.
                // Column headers must therefore be localized manually using the Resources file.

                dataGridViewMain.SuspendLayout();

                foreach (DataGridViewColumn col in dataGridViewMain.Columns)
                {
                    col.HeaderText = Resources.ResourceManager.GetString(col.DataPropertyName);
                }
                dataGridViewMain.ResumeLayout();
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
            finally
            {
                //All layout changes are applied at once, improving performance and preventing UI flicker
                this.ResumeLayout();
            }
        }

        /// <summary>
        /// Recursively applies localization resources to the specified control and all its child controls.
        /// Ensures that each control's properties are updated according to the current culture.
        /// </summary>
        /// <param name="pRes">The ComponentResourceManager used to apply the localized resources.</param>
        /// <param name="pControl">The control to which resources should be applied recursively.</param>
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
                Log.Error(tEx, tEx.Message);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Displays a custom error message box for the specified ResultStatus.
        /// Wraps the call to CustomMessageBox to simplify error handling in the form.
        /// </summary>
        /// <param name="pStatus">The ResultStatus value used to retrieve the localized error message.</param>
        private void ShowErrorBox(ResultStatus pStatus)
        {
            try
            {
                CustomMessageBox.Show(Resources.ResourceManager.GetString(pStatus.ToString()), Resources.ErrorCaption, ButtonTypes.Ok, IconTypes.Error);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                MessageBox.Show(Resources.UnexpectedError);
            }
        }

        /// <summary>
        /// Handles the language selection change in the combo box.
        /// Calls SwitchLanguage with the corresponding culture code based on the selected language.
        /// </summary>
        /// <param name="pSender">The source of the event.</param>
        /// <param name="pE">Event arguments.</param>
        ///
        private void LanguagesComboBox_SelectedIndexChanged(object pSender, EventArgs pE)
        {
            try
            {
                string tSelectedLang = LanguagesComboBox.SelectedValue.ToString();

                switch (tSelectedLang)
                {
                    case ENGLISH_LANGUAGE:
                        SwitchLanguage("en");
                        break;

                    case ARABIC_LANGUAGE:
                        SwitchLanguage("ar");
                        break;
                }
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Opens the connection settings form to allow the user to change the database connection string.
        /// If the user confirms changes, stops the current QuestionService, creates a new instance
        /// with the updated connection, subscribes to the QuestionsTableChanged event,
        /// starts listening for database changes, and reloads the questions.
        /// </summary>
        /// <param name="pSender">The source of the event.</param>
        /// <param name="pE">Event arguments.</param>
        private void btnChangeDataBase_Click(object pSender, EventArgs pE)
        {
            try
            {
                using (var tConnectionForm = new ConnectionSettingsForm())
                {
                    tConnectionForm.ShowDialog();

                    if (tConnectionForm.DialogResult == DialogResult.OK)
                    {
                        mQuestionService?.StopListening();
                        mQuestionService = new QuestionService();// Create new service so it reads the updated connection string
                        mQuestionService.QuestionsTableChanged += OnQuestionsChanged;
                        var tResult = mQuestionService.StartListening();
                        if (!tResult.IsSuccess)
                        {
                            ShowErrorBox(tResult.Status);
                        }
                        LoadQuestions();
                    }
                }
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event for the main DataGridView.
        /// Intercepts the QuestionType column and replaces the raw enum value
        /// with a localized display string based on the current UI culture.
        /// </summary>
        /// <param name="pSender">The source of the event.</param>
        /// <param name="pE">Event arguments containing the cell's column index and value to format.</param>
        private void DataGridViewMain_CellFormatting(object pSender, DataGridViewCellFormattingEventArgs pE)
        {
            try
            {
                if (dataGridViewMain.Columns[pE.ColumnIndex].DataPropertyName == COL_QUESTION_TYPE && pE.Value != null)
                {
                    switch ((QuestionType)pE.Value)
                    {
                        case QuestionType.Smiley: pE.Value = Resources.ResourceManager.GetString("QuestionType_Smiley"); break;
                        case QuestionType.Slider: pE.Value = Resources.ResourceManager.GetString("QuestionType_Slider"); break;
                        case QuestionType.Star: pE.Value = Resources.ResourceManager.GetString("QuestionType_Star"); break;
                    }
                    pE.FormattingApplied = true; // tells DataGridView not to apply further formatting
                }
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }
    }
}