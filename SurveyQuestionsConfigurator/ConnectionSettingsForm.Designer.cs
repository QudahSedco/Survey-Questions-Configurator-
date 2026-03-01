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
            this.errorProvider.SetError(this.lblServer, resources.GetString("lblServer.Error"));
            this.errorProvider.SetIconAlignment(this.lblServer, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblServer.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.lblServer, ((int)(resources.GetObject("lblServer.IconPadding"))));
            this.lblServer.Name = "lblServer";
            // 
            // ServerTextBox
            // 
            resources.ApplyResources(this.ServerTextBox, "ServerTextBox");
            this.errorProvider.SetError(this.ServerTextBox, resources.GetString("ServerTextBox.Error"));
            this.errorProvider.SetIconAlignment(this.ServerTextBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("ServerTextBox.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.ServerTextBox, ((int)(resources.GetObject("ServerTextBox.IconPadding"))));
            this.ServerTextBox.Name = "ServerTextBox";
            // 
            // lblDatabaseName
            // 
            resources.ApplyResources(this.lblDatabaseName, "lblDatabaseName");
            this.errorProvider.SetError(this.lblDatabaseName, resources.GetString("lblDatabaseName.Error"));
            this.errorProvider.SetIconAlignment(this.lblDatabaseName, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblDatabaseName.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.lblDatabaseName, ((int)(resources.GetObject("lblDatabaseName.IconPadding"))));
            this.lblDatabaseName.Name = "lblDatabaseName";
            // 
            // DatabaseNameTextBox
            // 
            resources.ApplyResources(this.DatabaseNameTextBox, "DatabaseNameTextBox");
            this.errorProvider.SetError(this.DatabaseNameTextBox, resources.GetString("DatabaseNameTextBox.Error"));
            this.errorProvider.SetIconAlignment(this.DatabaseNameTextBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("DatabaseNameTextBox.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.DatabaseNameTextBox, ((int)(resources.GetObject("DatabaseNameTextBox.IconPadding"))));
            this.DatabaseNameTextBox.Name = "DatabaseNameTextBox";
            // 
            // lblUserName
            // 
            resources.ApplyResources(this.lblUserName, "lblUserName");
            this.errorProvider.SetError(this.lblUserName, resources.GetString("lblUserName.Error"));
            this.errorProvider.SetIconAlignment(this.lblUserName, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblUserName.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.lblUserName, ((int)(resources.GetObject("lblUserName.IconPadding"))));
            this.lblUserName.Name = "lblUserName";
            // 
            // lblPassword
            // 
            resources.ApplyResources(this.lblPassword, "lblPassword");
            this.errorProvider.SetError(this.lblPassword, resources.GetString("lblPassword.Error"));
            this.errorProvider.SetIconAlignment(this.lblPassword, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblPassword.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.lblPassword, ((int)(resources.GetObject("lblPassword.IconPadding"))));
            this.lblPassword.Name = "lblPassword";
            // 
            // UserNameTextBox
            // 
            resources.ApplyResources(this.UserNameTextBox, "UserNameTextBox");
            this.errorProvider.SetError(this.UserNameTextBox, resources.GetString("UserNameTextBox.Error"));
            this.errorProvider.SetIconAlignment(this.UserNameTextBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("UserNameTextBox.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.UserNameTextBox, ((int)(resources.GetObject("UserNameTextBox.IconPadding"))));
            this.UserNameTextBox.Name = "UserNameTextBox";
            // 
            // PasswordTextBox
            // 
            resources.ApplyResources(this.PasswordTextBox, "PasswordTextBox");
            this.errorProvider.SetError(this.PasswordTextBox, resources.GetString("PasswordTextBox.Error"));
            this.errorProvider.SetIconAlignment(this.PasswordTextBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("PasswordTextBox.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.PasswordTextBox, ((int)(resources.GetObject("PasswordTextBox.IconPadding"))));
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.UseSystemPasswordChar = true;
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.errorProvider.SetError(this.btnSave, resources.GetString("btnSave.Error"));
            this.errorProvider.SetIconAlignment(this.btnSave, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnSave.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.btnSave, ((int)(resources.GetObject("btnSave.IconPadding"))));
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnTestConnection
            // 
            resources.ApplyResources(this.btnTestConnection, "btnTestConnection");
            this.errorProvider.SetError(this.btnTestConnection, resources.GetString("btnTestConnection.Error"));
            this.errorProvider.SetIconAlignment(this.btnTestConnection, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnTestConnection.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.btnTestConnection, ((int)(resources.GetObject("btnTestConnection.IconPadding"))));
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            resources.ApplyResources(this.errorProvider, "errorProvider");
            // 
            // WindowsRadioButton
            // 
            resources.ApplyResources(this.WindowsRadioButton, "WindowsRadioButton");
            this.errorProvider.SetError(this.WindowsRadioButton, resources.GetString("WindowsRadioButton.Error"));
            this.errorProvider.SetIconAlignment(this.WindowsRadioButton, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("WindowsRadioButton.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.WindowsRadioButton, ((int)(resources.GetObject("WindowsRadioButton.IconPadding"))));
            this.WindowsRadioButton.Name = "WindowsRadioButton";
            this.WindowsRadioButton.TabStop = true;
            this.WindowsRadioButton.UseVisualStyleBackColor = true;
            this.WindowsRadioButton.CheckedChanged += new System.EventHandler(this.WindowsRadioButton_CheckedChanged);
            // 
            // MainDBGroupBox
            // 
            resources.ApplyResources(this.MainDBGroupBox, "MainDBGroupBox");
            this.MainDBGroupBox.Controls.Add(this.SQLRadioButton);
            this.MainDBGroupBox.Controls.Add(this.lblAuthenticationType);
            this.MainDBGroupBox.Controls.Add(this.lblServer);
            this.MainDBGroupBox.Controls.Add(this.ServerTextBox);
            this.MainDBGroupBox.Controls.Add(this.DatabaseNameTextBox);
            this.MainDBGroupBox.Controls.Add(this.lblDatabaseName);
            this.MainDBGroupBox.Controls.Add(this.WindowsRadioButton);
            this.errorProvider.SetError(this.MainDBGroupBox, resources.GetString("MainDBGroupBox.Error"));
            this.errorProvider.SetIconAlignment(this.MainDBGroupBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("MainDBGroupBox.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.MainDBGroupBox, ((int)(resources.GetObject("MainDBGroupBox.IconPadding"))));
            this.MainDBGroupBox.Name = "MainDBGroupBox";
            this.MainDBGroupBox.TabStop = false;
            // 
            // SQLRadioButton
            // 
            resources.ApplyResources(this.SQLRadioButton, "SQLRadioButton");
            this.errorProvider.SetError(this.SQLRadioButton, resources.GetString("SQLRadioButton.Error"));
            this.errorProvider.SetIconAlignment(this.SQLRadioButton, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("SQLRadioButton.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.SQLRadioButton, ((int)(resources.GetObject("SQLRadioButton.IconPadding"))));
            this.SQLRadioButton.Name = "SQLRadioButton";
            this.SQLRadioButton.TabStop = true;
            this.SQLRadioButton.UseVisualStyleBackColor = true;
            this.SQLRadioButton.CheckedChanged += new System.EventHandler(this.SQLRadioButton_CheckedChanged);
            // 
            // lblAuthenticationType
            // 
            resources.ApplyResources(this.lblAuthenticationType, "lblAuthenticationType");
            this.errorProvider.SetError(this.lblAuthenticationType, resources.GetString("lblAuthenticationType.Error"));
            this.errorProvider.SetIconAlignment(this.lblAuthenticationType, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblAuthenticationType.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.lblAuthenticationType, ((int)(resources.GetObject("lblAuthenticationType.IconPadding"))));
            this.lblAuthenticationType.Name = "lblAuthenticationType";
            // 
            // SQLGroupBox
            // 
            resources.ApplyResources(this.SQLGroupBox, "SQLGroupBox");
            this.SQLGroupBox.Controls.Add(this.PasswordTextBox);
            this.SQLGroupBox.Controls.Add(this.UserNameTextBox);
            this.SQLGroupBox.Controls.Add(this.lblUserName);
            this.SQLGroupBox.Controls.Add(this.lblPassword);
            this.errorProvider.SetError(this.SQLGroupBox, resources.GetString("SQLGroupBox.Error"));
            this.errorProvider.SetIconAlignment(this.SQLGroupBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("SQLGroupBox.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.SQLGroupBox, ((int)(resources.GetObject("SQLGroupBox.IconPadding"))));
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