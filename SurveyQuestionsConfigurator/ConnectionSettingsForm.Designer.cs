namespace SurveyQuestionsConfigurator
{
    partial class ConnectionSettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionSettingsForm));
            this.lblServer = new System.Windows.Forms.Label();
            this.ServerTextBox = new System.Windows.Forms.TextBox();
            this.lblDatabaseName = new System.Windows.Forms.Label();
            this.DatabaseNameTextBox = new System.Windows.Forms.TextBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.UserNameTextBox = new System.Windows.Forms.TextBox();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.WindowsRadioButton = new System.Windows.Forms.RadioButton();
            this.MainDBGroupBox = new System.Windows.Forms.GroupBox();
            this.SQLRadioButton = new System.Windows.Forms.RadioButton();
            this.lblAuthenticationType = new System.Windows.Forms.Label();
            this.SQLGroupBox = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.MainDBGroupBox.SuspendLayout();
            this.SQLGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblServer
            // 
            resources.ApplyResources(this.lblServer, "lblServer");
            this.lblServer.Name = "lblServer";
            // 
            // ServerTextBox
            // 
            resources.ApplyResources(this.ServerTextBox, "ServerTextBox");
            this.ServerTextBox.Name = "ServerTextBox";
            // 
            // lblDatabaseName
            // 
            resources.ApplyResources(this.lblDatabaseName, "lblDatabaseName");
            this.lblDatabaseName.Name = "lblDatabaseName";
            // 
            // DatabaseNameTextBox
            // 
            resources.ApplyResources(this.DatabaseNameTextBox, "DatabaseNameTextBox");
            this.DatabaseNameTextBox.Name = "DatabaseNameTextBox";
            // 
            // lblUserName
            // 
            resources.ApplyResources(this.lblUserName, "lblUserName");
            this.lblUserName.Name = "lblUserName";
            // 
            // lblPassword
            // 
            resources.ApplyResources(this.lblPassword, "lblPassword");
            this.lblPassword.Name = "lblPassword";
            // 
            // UserNameTextBox
            // 
            resources.ApplyResources(this.UserNameTextBox, "UserNameTextBox");
            this.UserNameTextBox.Name = "UserNameTextBox";
            // 
            // PasswordTextBox
            // 
            resources.ApplyResources(this.PasswordTextBox, "PasswordTextBox");
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.UseSystemPasswordChar = true;
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnTestConnection
            // 
            resources.ApplyResources(this.btnTestConnection, "btnTestConnection");
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // WindowsRadioButton
            // 
            resources.ApplyResources(this.WindowsRadioButton, "WindowsRadioButton");
            this.WindowsRadioButton.Name = "WindowsRadioButton";
            this.WindowsRadioButton.TabStop = true;
            this.WindowsRadioButton.UseVisualStyleBackColor = true;
            this.WindowsRadioButton.CheckedChanged += new System.EventHandler(this.WindowsRadioButton_CheckedChanged);
            // 
            // MainDBGroupBox
            // 
            this.MainDBGroupBox.Controls.Add(this.SQLRadioButton);
            this.MainDBGroupBox.Controls.Add(this.lblAuthenticationType);
            this.MainDBGroupBox.Controls.Add(this.lblServer);
            this.MainDBGroupBox.Controls.Add(this.ServerTextBox);
            this.MainDBGroupBox.Controls.Add(this.DatabaseNameTextBox);
            this.MainDBGroupBox.Controls.Add(this.lblDatabaseName);
            this.MainDBGroupBox.Controls.Add(this.WindowsRadioButton);
            resources.ApplyResources(this.MainDBGroupBox, "MainDBGroupBox");
            this.MainDBGroupBox.Name = "MainDBGroupBox";
            this.MainDBGroupBox.TabStop = false;
            // 
            // SQLRadioButton
            // 
            resources.ApplyResources(this.SQLRadioButton, "SQLRadioButton");
            this.SQLRadioButton.Name = "SQLRadioButton";
            this.SQLRadioButton.TabStop = true;
            this.SQLRadioButton.UseVisualStyleBackColor = true;
            this.SQLRadioButton.CheckedChanged += new System.EventHandler(this.SQLRadioButton_CheckedChanged);
            // 
            // lblAuthenticationType
            // 
            resources.ApplyResources(this.lblAuthenticationType, "lblAuthenticationType");
            this.lblAuthenticationType.Name = "lblAuthenticationType";
            // 
            // SQLGroupBox
            // 
            this.SQLGroupBox.Controls.Add(this.PasswordTextBox);
            this.SQLGroupBox.Controls.Add(this.UserNameTextBox);
            this.SQLGroupBox.Controls.Add(this.lblUserName);
            this.SQLGroupBox.Controls.Add(this.lblPassword);
            resources.ApplyResources(this.SQLGroupBox, "SQLGroupBox");
            this.SQLGroupBox.Name = "SQLGroupBox";
            this.SQLGroupBox.TabStop = false;
            // 
            // ConnectionSettingsForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnTestConnection);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.SQLGroupBox);
            this.Controls.Add(this.MainDBGroupBox);
            this.Name = "ConnectionSettingsForm";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.MainDBGroupBox.ResumeLayout(false);
            this.MainDBGroupBox.PerformLayout();
            this.SQLGroupBox.ResumeLayout(false);
            this.SQLGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.TextBox ServerTextBox;
        private System.Windows.Forms.Label lblDatabaseName;
        private System.Windows.Forms.TextBox DatabaseNameTextBox;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox UserNameTextBox;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.GroupBox MainDBGroupBox;
        private System.Windows.Forms.Label lblAuthenticationType;
        private System.Windows.Forms.RadioButton WindowsRadioButton;
        private System.Windows.Forms.GroupBox SQLGroupBox;
        private System.Windows.Forms.RadioButton SQLRadioButton;
    }
}