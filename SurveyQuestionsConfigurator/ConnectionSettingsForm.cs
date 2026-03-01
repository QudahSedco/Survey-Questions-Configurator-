using Serilog;
using SurveyQuestionsConfiguratorServices;
using System;
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

        public ConnectionSettingsForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            // Start position: center of the parent form that opened it
            this.StartPosition = FormStartPosition.CenterParent;
            mAppSetting = new AppSetting();
            mQuestionService = new QuestionService();
            PopulateFields();
        }

        private void PasswordTextBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void PopulateFields()
        {
            string tConnectionString = mAppSetting.GetConnectionString("SurveyDb");
            if (!string.IsNullOrEmpty(tConnectionString))
            {
                var builder = new SqlConnectionStringBuilder(tConnectionString);

                ServerTextBox.Text = builder.DataSource;
                DatabaseNameTextBox.Text = builder.InitialCatalog;
                UserNameTextBox.Text = builder.UserID;
                PasswordTextBox.Text = builder.Password;
            }
        }

        private string BuildConnectionString()
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = ServerTextBox.Text.Trim(),         // Server textbox
                InitialCatalog = DatabaseNameTextBox.Text.Trim(),   // Database textbox
                UserID = UserNameTextBox.Text.Trim(),           // User textbox
                Password = PasswordTextBox.Text              // Password textbox
            };
            return builder.ConnectionString;

            // return "Server=.;Database=SurveyQuestionsConfiguratorDB;User Id=sa;Password=SqlTest;";
        }

        public static void SetConnectionString(string name, string value)
        {
            var settings = ConfigurationManager.ConnectionStrings[name];

            settings.ConnectionString = value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(ServerTextBox.Text))
                {
                    errorProvider.SetError(ServerTextBox, Resources.NullOrWhiteSpaceError);
                    return;
                }
                if (String.IsNullOrEmpty(DatabaseNameTextBox.Text))
                {
                    errorProvider.SetError(ServerTextBox, Resources.NullOrWhiteSpaceError);
                    return;
                }
                if (String.IsNullOrEmpty(UserNameTextBox.Text))
                {
                    errorProvider.SetError(ServerTextBox, Resources.NullOrWhiteSpaceError);
                    return;
                }
                if (String.IsNullOrEmpty(ServerTextBox.Text))
                {
                    errorProvider.SetError(PasswordTextBox, Resources.NullOrWhiteSpaceError);
                    return;
                }

                string cs = BuildConnectionString();

                // Test before saving
                var tResult = mQuestionService.TestConnection(cs);

                // Save to App.config using your AppSetting class
                if (!tResult.IsSuccess)
                {
                    CustomMessageBox.Show(Resources.DatabaseConnectionError, Resources.ErrorCaption, ButtonTypes.Ok, IconTypes.Error);
                    return;
                }
                mAppSetting.SaveConnection(cs, "SurveyDb");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unexpected Error");
                CustomMessageBox.Show(Resources.UnexpectedError, Resources.ErrorCaption, ButtonTypes.Ok, IconTypes.Error);
            }
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            string cs = BuildConnectionString();

            var tResult = mQuestionService.TestConnection(cs);

            if (!tResult.IsSuccess)
            {
                CustomMessageBox.Show(Resources.DatabaseConnectionError, Resources.ErrorCaption, ButtonTypes.Ok, IconTypes.Error);
                return;
            }
            CustomMessageBox.Show(Resources.ConnectionSuccess, Resources.SuccessCaption, ButtonTypes.Ok, IconTypes.Success);
        }
    }
}