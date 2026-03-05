using Serilog;
using SurveyQuestionsConfiguratorServices;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace SurveyQuestionsConfigurator
{
    /// <summary>
    /// Form that allows the user to view, test, and update the database connection
    /// </summary>
    public partial class ConnectionSettingsForm : Form
    {
        private AppSetting mAppSetting;
        private QuestionService mQuestionService;
        private const string DB_KEY = "SurveyDb";

        /// <summary>
        /// Initializes a new instance of the ConnectionSettingsForm class,
        /// sets up the UI, loads existing connection settings
        /// </summary>
        public ConnectionSettingsForm()
        {
            try
            {
                InitializeComponent();
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.MinimizeBox = false;
                this.MaximizeBox = false;
                this.StartPosition = FormStartPosition.CenterParent;
                mAppSetting = new AppSetting();
                mQuestionService = new QuestionService();
                WindowsRadioButton.Checked = true;
                SQLGroupBox.Visible = false;
                this.ActiveControl = ServerTextBox;

                PopulateFields();
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                throw;
            }
        }

        /// <summary>
        /// Populates the form fields with values read from the connection string
        /// stored in the configuration file.
        /// </summary>
        private void PopulateFields()
        {
            try
            {
                string tConnectionString = mAppSetting.GetConnectionString(DB_KEY);

                if (!string.IsNullOrEmpty(tConnectionString))
                {
                    var tBuilder = new SqlConnectionStringBuilder(tConnectionString);//Builds the SQL connection string dynamically

                    ServerTextBox.Text = tBuilder.DataSource;
                    DatabaseNameTextBox.Text = tBuilder.InitialCatalog;

                    bool isWindowsAuth = tBuilder.IntegratedSecurity;
                    WindowsRadioButton.Checked = isWindowsAuth;
                    SQLRadioButton.Checked = !isWindowsAuth;

                    UserNameTextBox.Text = tBuilder.UserID;
                    PasswordTextBox.Text = tBuilder.Password;
                }
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                CustomMessageBox.Show(Resources.UnexpectedError, Resources.ErrorCaption, ButtonTypes.Ok, IconTypes.Error);
            }
        }

        /// <summary>
        /// Builds a SQL Server connection string based on the values
        /// entered in the form fields and the selected authentication type.
        /// </summary>
        /// <returns>A valid connection string based on user input.</returns>
        private string BuildConnectionString()
        {
            try
            {
                var tBuilder = new SqlConnectionStringBuilder// Builds the SQL connection string dynamically
                {
                    DataSource = ServerTextBox.Text.Trim(),
                    InitialCatalog = DatabaseNameTextBox.Text.Trim(),
                    ConnectTimeout = 5
                };
                if (WindowsRadioButton.Checked)
                {
                    tBuilder.IntegratedSecurity = true;
                }
                else
                {
                    tBuilder.IntegratedSecurity = false;
                    tBuilder.UserID = UserNameTextBox.Text.Trim();
                    tBuilder.Password = PasswordTextBox.Text;
                }
                return tBuilder.ConnectionString;
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                throw;
            }
        }

        /// <summary>
        /// Validates user input, tests the database connection, and saves
        /// the connection string to the configuration file if successful.
        /// </summary>
        private async void btnSave_Click(object pSender, EventArgs pE)
        {
            try
            {
                btnTestConnection.Enabled = false;
                btnSave.Enabled = false;
                this.UseWaitCursor = true;
                SetUIEnabled(false); //disables UI elements

                errorProvider.Clear();
                if (String.IsNullOrEmpty(ServerTextBox.Text))
                {
                    errorProvider.SetError(ServerTextBox, Resources.NullOrWhiteSpaceError);
                    return;
                }
                if (String.IsNullOrEmpty(DatabaseNameTextBox.Text))
                {
                    errorProvider.SetError(DatabaseNameTextBox, Resources.NullOrWhiteSpaceError);
                    return;
                }
                if (SQLRadioButton.Checked)
                {
                    if (String.IsNullOrEmpty(UserNameTextBox.Text))
                    {
                        errorProvider.SetError(UserNameTextBox, Resources.NullOrWhiteSpaceError);
                        return;
                    }
                    if (String.IsNullOrEmpty(PasswordTextBox.Text))
                    {
                        errorProvider.SetError(PasswordTextBox, Resources.NullOrWhiteSpaceError);
                        return;
                    }
                }

                string tConnectionString = BuildConnectionString();

                var tResult = await Task.Run(() => mQuestionService.TestConnection(tConnectionString));

                if (!tResult.IsSuccess)
                {
                    CustomMessageBox.Show(Resources.DatabaseConnectionError, Resources.ErrorCaption, ButtonTypes.Ok, IconTypes.Error);
                    return;
                }

                mAppSetting.SaveConnection(tConnectionString, DB_KEY);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                CustomMessageBox.Show(Resources.UnexpectedError, Resources.ErrorCaption, ButtonTypes.Ok, IconTypes.Error);
            }
            finally
            {
                btnTestConnection.Enabled = true;
                btnSave.Enabled = true;
                this.UseWaitCursor = false;
                SetUIEnabled(true);//enables UI elements
            }
        }

        /// <summary>
        /// Validates user input,Tests the database connection using the currently entered connection details
        /// and shows a success or error message to the user.
        /// </summary>
        private async void btnTestConnection_Click(object pSender, EventArgs pE)
        {
            try
            {
                btnTestConnection.Enabled = false;
                btnSave.Enabled = false;
                this.UseWaitCursor = true;
                SetUIEnabled(false);//disables UI elements

                errorProvider.Clear();
                if (String.IsNullOrEmpty(ServerTextBox.Text))
                {
                    errorProvider.SetError(ServerTextBox, Resources.NullOrWhiteSpaceError);
                    return;
                }
                if (String.IsNullOrEmpty(DatabaseNameTextBox.Text))
                {
                    errorProvider.SetError(DatabaseNameTextBox, Resources.NullOrWhiteSpaceError);
                    return;
                }
                if (SQLRadioButton.Checked)
                {
                    if (String.IsNullOrEmpty(UserNameTextBox.Text))
                    {
                        errorProvider.SetError(UserNameTextBox, Resources.NullOrWhiteSpaceError);
                        return;
                    }
                    if (String.IsNullOrEmpty(PasswordTextBox.Text))
                    {
                        errorProvider.SetError(PasswordTextBox, Resources.NullOrWhiteSpaceError);
                        return;
                    }
                }
                string tConnectionString = BuildConnectionString();

                var tResult = await Task.Run(() => mQuestionService.TestConnection(tConnectionString));

                if (!tResult.IsSuccess)
                {
                    CustomMessageBox.Show(Resources.DatabaseConnectionError, Resources.ErrorCaption, ButtonTypes.Ok, IconTypes.Error);
                    return;
                }
                CustomMessageBox.Show(Resources.ConnectionSuccess, Resources.SuccessCaption, ButtonTypes.Ok, IconTypes.Success);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                CustomMessageBox.Show(Resources.UnexpectedError, Resources.ErrorCaption, ButtonTypes.Ok, IconTypes.Error);
            }
            finally
            {
                btnTestConnection.Enabled = true;
                btnSave.Enabled = true;
                this.UseWaitCursor = false;
                SetUIEnabled(true);//enables UI elements
            }
        }

        /// <summary>
        /// Hides SQL authentication fields and clears username/password if selected.
        /// </summary>
        private void WindowsRadioButton_CheckedChanged(object pSender, EventArgs pE)
        {
            try
            {
                if (WindowsRadioButton.Checked)
                {
                    SQLGroupBox.Visible = false;
                    UserNameTextBox.Clear();
                    PasswordTextBox.Clear();
                }
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                CustomMessageBox.Show(Resources.UnexpectedError, Resources.ErrorCaption, ButtonTypes.Ok, IconTypes.Error);
            }
        }

        /// <summary>
        /// Shows SQL authentication fields when selected.
        /// </summary>
        private void SQLRadioButton_CheckedChanged(object pSender, EventArgs pE)
        {
            try
            {
                if (SQLRadioButton.Checked)
                {
                    SQLGroupBox.Visible = true;
                }
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                CustomMessageBox.Show(Resources.UnexpectedError, Resources.ErrorCaption, ButtonTypes.Ok, IconTypes.Error);
            }
        }

        /// <summary>
        /// Enables or disables all interactive UI controls on the form.
        /// Used to prevent user interaction while testing connection string.
        /// </summary>
        /// <param name="pEnabled">True to enable all controls, false to disable them.</param>
        private void SetUIEnabled(bool pEnabled)
        {
            ServerTextBox.Enabled = pEnabled;
            DatabaseNameTextBox.Enabled = pEnabled;
            UserNameTextBox.Enabled = pEnabled;
            PasswordTextBox.Enabled = pEnabled;
            WindowsRadioButton.Enabled = pEnabled;
            SQLRadioButton.Enabled = pEnabled;
            btnSave.Enabled = pEnabled;
            btnTestConnection.Enabled = pEnabled;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}