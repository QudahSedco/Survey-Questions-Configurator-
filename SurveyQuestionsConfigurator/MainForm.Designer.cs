namespace SurveyQuestionsConfigurator
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.QuestionsListGroupBox = new System.Windows.Forms.GroupBox();
            this.dataGridViewMain = new System.Windows.Forms.DataGridView();
            this.btnChangeDatabase = new System.Windows.Forms.Button();
            this.LanguagesComboBox = new System.Windows.Forms.ComboBox();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlFooter.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.QuestionsListGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMain)).BeginInit();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            resources.ApplyResources(this.btnUpdate, "btnUpdate");
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btnDelete);
            this.pnlFooter.Controls.Add(this.btnUpdate);
            this.pnlFooter.Controls.Add(this.btnAdd);
            resources.ApplyResources(this.pnlFooter, "pnlFooter");
            this.pnlFooter.Name = "pnlFooter";
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // pnlMain
            // 
            resources.ApplyResources(this.pnlMain, "pnlMain");
            this.pnlMain.Controls.Add(this.QuestionsListGroupBox);
            this.pnlMain.Name = "pnlMain";
            // 
            // QuestionsListGroupBox
            // 
            resources.ApplyResources(this.QuestionsListGroupBox, "QuestionsListGroupBox");
            this.QuestionsListGroupBox.BackColor = System.Drawing.SystemColors.Menu;
            this.QuestionsListGroupBox.Controls.Add(this.dataGridViewMain);
            this.QuestionsListGroupBox.Name = "QuestionsListGroupBox";
            this.QuestionsListGroupBox.TabStop = false;
            // 
            // dataGridViewMain
            // 
            this.dataGridViewMain.AllowUserToAddRows = false;
            this.dataGridViewMain.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.dataGridViewMain, "dataGridViewMain");
            this.dataGridViewMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMain.Name = "dataGridViewMain";
            this.dataGridViewMain.ReadOnly = true;
            this.dataGridViewMain.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridViewMain_CellContentClick);
            this.dataGridViewMain.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridViewMain_ColumnHeaderMouseClick);
            // 
            // btnChangeDatabase
            // 
            resources.ApplyResources(this.btnChangeDatabase, "btnChangeDatabase");
            this.btnChangeDatabase.Name = "btnChangeDatabase";
            this.btnChangeDatabase.UseVisualStyleBackColor = true;
            this.btnChangeDatabase.Click += new System.EventHandler(this.btnChangeDataBase_Click);
            // 
            // LanguagesComboBox
            // 
            resources.ApplyResources(this.LanguagesComboBox, "LanguagesComboBox");
            this.LanguagesComboBox.FormattingEnabled = true;
            this.LanguagesComboBox.Name = "LanguagesComboBox";
            this.LanguagesComboBox.SelectedIndexChanged += new System.EventHandler(this.LanguagesComboBox_SelectedIndexChanged);
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.LanguagesComboBox);
            this.pnlHeader.Controls.Add(this.btnChangeDatabase);
            resources.ApplyResources(this.pnlHeader, "pnlHeader");
            this.pnlHeader.Name = "pnlHeader";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlMain);
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.pnlFooter.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.QuestionsListGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMain)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.DataGridView dataGridViewMain;
        private System.Windows.Forms.GroupBox QuestionsListGroupBox;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ComboBox LanguagesComboBox;
        private System.Windows.Forms.Button btnChangeDatabase;
        private System.Windows.Forms.Panel pnlHeader;
    }
}

