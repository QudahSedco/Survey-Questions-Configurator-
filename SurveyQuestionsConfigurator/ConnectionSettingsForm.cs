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
    public partial class ConnectionSettingsForm : Form
    {
        private AppSetting mAppSetting;
        private QuestionService mQuestionService;
        private const string UNEXPECTED_ERROR_MESSAGE = "An unexpected error occurred";
        private const string DB_KEY = "SurveyDb";

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
                PopulateFields();
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                CustomMessageBox.Show(Resources.UnexpectedError, Resources.ErrorCaption, ButtonTypes.Ok, IconTypes.Error);
                Close();
            }
        }

        private void PopulateFields()
        {
            try
            {
                string tConnectionString = mAppSetting.GetConnectionString(DB_KEY);
                if (!string.IsNullOrEmpty(tConnectionString))
                {
                    var tBuilder = new SqlConnectionStringBuilder(tConnectionString);

                    ServerTextBox.Text = tBuilder.DataSource;
                    DatabaseNameTextBox.Text = tBuilder.InitialCatalog;
                    UserNameTextBox.Text = tBuilder.UserID;
                    PasswordTextBox.Text = tBuilder.Password;
                }
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                CustomMessageBox.Show(Resources.UnexpectedError, Resources.ErrorCaption, ButtonTypes.Ok, IconTypes.Error);
            }
        }

        private string BuildConnectionString()
        {
            try
            {
                var tBuilder = new SqlConnectionStringBuilder
                {
                    DataSource = ServerTextBox.Text.Trim(),
                    InitialCatalog = DatabaseNameTextBox.Text.Trim(),
                    UserID = UserNameTextBox.Text.Trim(),
                    Password = PasswordTextBox.Text
                };
                return tBuilder.ConnectionString;
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, "error building connection string");
                throw;
            }
        }

        private void btnSave_Click(object pSender, EventArgs pE)
        {
            try
            {
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

                string tConnectionString = BuildConnectionString();

                // Test before saving
                var tResult = mQuestionService.TestConnection(tConnectionString);

                // Save to App.config using your AppSetting class
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
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                CustomMessageBox.Show(Resources.UnexpectedError, Resources.ErrorCaption, ButtonTypes.Ok, IconTypes.Error);
            }
        }

        private void btnTestConnection_Click(object pSender, EventArgs pE)
        {
            try
            {
                string tConnectionString = BuildConnectionString();

                var tResult = mQuestionService.TestConnection(tConnectionString);

                if (!tResult.IsSuccess)
                {
                    CustomMessageBox.Show(Resources.DatabaseConnectionError, Resources.ErrorCaption, ButtonTypes.Ok, IconTypes.Error);
                    return;
                }
                CustomMessageBox.Show(Resources.ConnectionSuccess, Resources.SuccessCaption, ButtonTypes.Ok, IconTypes.Success);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                CustomMessageBox.Show(Resources.UnexpectedError, Resources.ErrorCaption, ButtonTypes.Ok, IconTypes.Error);
            }
        }
    }
}