namespace SurveyQuestionsConfigurator
{
    partial class DialogForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogForm));
            this.btnAdd = new System.Windows.Forms.Button();
            this.textBoxQuestionText = new System.Windows.Forms.TextBox();
            this.lblQuestionText = new System.Windows.Forms.Label();
            this.numericUpDownQuestionOrder = new System.Windows.Forms.NumericUpDown();
            this.lblQuestionOrder = new System.Windows.Forms.Label();
            this.comboBoxQuestionTypes = new System.Windows.Forms.ComboBox();
            this.lblNumberOfStarsText = new System.Windows.Forms.Label();
            this.trackBarStars = new System.Windows.Forms.TrackBar();
            this.lblStars = new System.Windows.Forms.Label();
            this.lblTypeOfQuestion = new System.Windows.Forms.Label();
            this.pnlStars = new System.Windows.Forms.Panel();
            this.lblNumberOfStars = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlSlider = new System.Windows.Forms.Panel();
            this.lblCharNumberEndCaption = new System.Windows.Forms.Label();
            this.lblCharNumberStartCaption = new System.Windows.Forms.Label();
            this.lblEndCaption = new System.Windows.Forms.Label();
            this.textBoxEndCaption = new System.Windows.Forms.TextBox();
            this.textBoxStartCaption = new System.Windows.Forms.TextBox();
            this.lblStartCaption = new System.Windows.Forms.Label();
            this.lblEndValue = new System.Windows.Forms.Label();
            this.numericUpDownEndValue = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownStartValue = new System.Windows.Forms.NumericUpDown();
            this.lblStartValue = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblCharNumber = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlBaseFields = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pnlSmileyFaces = new System.Windows.Forms.Panel();
            this.lblFacesNumber = new System.Windows.Forms.Label();
            this.trackBarSmileyFaces = new System.Windows.Forms.TrackBar();
            this.lblSmileyFaces = new System.Windows.Forms.Label();
            this.lblNumberofSmileyfaces = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuestionOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarStars)).BeginInit();
            this.pnlStars.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.pnlSlider.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEndValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartValue)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.pnlBaseFields.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnlSmileyFaces.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSmileyFaces)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // textBoxQuestionText
            // 
            resources.ApplyResources(this.textBoxQuestionText, "textBoxQuestionText");
            this.textBoxQuestionText.Name = "textBoxQuestionText";
            this.textBoxQuestionText.TextChanged += new System.EventHandler(this.textBoxQuestionText_TextChanged);
            // 
            // lblQuestionText
            // 
            resources.ApplyResources(this.lblQuestionText, "lblQuestionText");
            this.lblQuestionText.Name = "lblQuestionText";
            // 
            // numericUpDownQuestionOrder
            // 
            resources.ApplyResources(this.numericUpDownQuestionOrder, "numericUpDownQuestionOrder");
            this.numericUpDownQuestionOrder.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownQuestionOrder.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownQuestionOrder.Name = "numericUpDownQuestionOrder";
            this.numericUpDownQuestionOrder.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblQuestionOrder
            // 
            resources.ApplyResources(this.lblQuestionOrder, "lblQuestionOrder");
            this.lblQuestionOrder.Name = "lblQuestionOrder";
            // 
            // comboBoxQuestionTypes
            // 
            this.comboBoxQuestionTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBoxQuestionTypes, "comboBoxQuestionTypes");
            this.comboBoxQuestionTypes.FormattingEnabled = true;
            this.comboBoxQuestionTypes.Name = "comboBoxQuestionTypes";
            this.comboBoxQuestionTypes.SelectedIndexChanged += new System.EventHandler(this.ComboBoxQuestionType_SelectedIndexChanged);
            // 
            // lblNumberOfStarsText
            // 
            resources.ApplyResources(this.lblNumberOfStarsText, "lblNumberOfStarsText");
            this.lblNumberOfStarsText.Name = "lblNumberOfStarsText";
            // 
            // trackBarStars
            // 
            resources.ApplyResources(this.trackBarStars, "trackBarStars");
            this.trackBarStars.Name = "trackBarStars";
            this.trackBarStars.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarStars.Scroll += new System.EventHandler(this.TrackBarStars_Scroll);
            // 
            // lblStars
            // 
            resources.ApplyResources(this.lblStars, "lblStars");
            this.lblStars.Name = "lblStars";
            // 
            // lblTypeOfQuestion
            // 
            resources.ApplyResources(this.lblTypeOfQuestion, "lblTypeOfQuestion");
            this.lblTypeOfQuestion.Name = "lblTypeOfQuestion";
            // 
            // pnlStars
            // 
            this.pnlStars.BackColor = System.Drawing.SystemColors.Control;
            this.pnlStars.Controls.Add(this.lblNumberOfStars);
            this.pnlStars.Controls.Add(this.trackBarStars);
            this.pnlStars.Controls.Add(this.lblStars);
            this.pnlStars.Controls.Add(this.lblNumberOfStarsText);
            resources.ApplyResources(this.pnlStars, "pnlStars");
            this.pnlStars.Name = "pnlStars";
            // 
            // lblNumberOfStars
            // 
            resources.ApplyResources(this.lblNumberOfStars, "lblNumberOfStars");
            this.lblNumberOfStars.Name = "lblNumberOfStars";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // pnlSlider
            // 
            this.pnlSlider.BackColor = System.Drawing.SystemColors.Control;
            this.pnlSlider.Controls.Add(this.lblCharNumberEndCaption);
            this.pnlSlider.Controls.Add(this.lblCharNumberStartCaption);
            this.pnlSlider.Controls.Add(this.lblEndCaption);
            this.pnlSlider.Controls.Add(this.textBoxEndCaption);
            this.pnlSlider.Controls.Add(this.textBoxStartCaption);
            this.pnlSlider.Controls.Add(this.lblStartCaption);
            this.pnlSlider.Controls.Add(this.lblEndValue);
            this.pnlSlider.Controls.Add(this.numericUpDownEndValue);
            this.pnlSlider.Controls.Add(this.numericUpDownStartValue);
            this.pnlSlider.Controls.Add(this.lblStartValue);
            resources.ApplyResources(this.pnlSlider, "pnlSlider");
            this.pnlSlider.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pnlSlider.Name = "pnlSlider";
            // 
            // lblCharNumberEndCaption
            // 
            resources.ApplyResources(this.lblCharNumberEndCaption, "lblCharNumberEndCaption");
            this.lblCharNumberEndCaption.Name = "lblCharNumberEndCaption";
            // 
            // lblCharNumberStartCaption
            // 
            resources.ApplyResources(this.lblCharNumberStartCaption, "lblCharNumberStartCaption");
            this.lblCharNumberStartCaption.Name = "lblCharNumberStartCaption";
            // 
            // lblEndCaption
            // 
            resources.ApplyResources(this.lblEndCaption, "lblEndCaption");
            this.lblEndCaption.Name = "lblEndCaption";
            // 
            // textBoxEndCaption
            // 
            resources.ApplyResources(this.textBoxEndCaption, "textBoxEndCaption");
            this.textBoxEndCaption.Name = "textBoxEndCaption";
            this.textBoxEndCaption.TextChanged += new System.EventHandler(this.textBoxEndCaption_TextChanged);
            // 
            // textBoxStartCaption
            // 
            resources.ApplyResources(this.textBoxStartCaption, "textBoxStartCaption");
            this.textBoxStartCaption.Name = "textBoxStartCaption";
            this.textBoxStartCaption.TextChanged += new System.EventHandler(this.textBoxStartCaption_TextChanged);
            // 
            // lblStartCaption
            // 
            resources.ApplyResources(this.lblStartCaption, "lblStartCaption");
            this.lblStartCaption.Name = "lblStartCaption";
            // 
            // lblEndValue
            // 
            resources.ApplyResources(this.lblEndValue, "lblEndValue");
            this.lblEndValue.Name = "lblEndValue";
            // 
            // numericUpDownEndValue
            // 
            resources.ApplyResources(this.numericUpDownEndValue, "numericUpDownEndValue");
            this.numericUpDownEndValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownEndValue.Name = "numericUpDownEndValue";
            this.numericUpDownEndValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDownStartValue
            // 
            resources.ApplyResources(this.numericUpDownStartValue, "numericUpDownStartValue");
            this.numericUpDownStartValue.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numericUpDownStartValue.Name = "numericUpDownStartValue";
            this.numericUpDownStartValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblStartValue
            // 
            resources.ApplyResources(this.lblStartValue, "lblStartValue");
            this.lblStartValue.Name = "lblStartValue";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnUpdate, "btnUpdate");
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblCharNumber
            // 
            resources.ApplyResources(this.lblCharNumber, "lblCharNumber");
            this.lblCharNumber.Name = "lblCharNumber";
            // 
            // pnlMain
            // 
            resources.ApplyResources(this.pnlMain, "pnlMain");
            this.pnlMain.BackColor = System.Drawing.SystemColors.Control;
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.pnlSlider);
            this.pnlMain.Controls.Add(this.pnlStars);
            this.pnlMain.Controls.Add(this.pnlBaseFields);
            this.pnlMain.Controls.Add(this.btnCancel);
            this.pnlMain.Controls.Add(this.btnAdd);
            this.pnlMain.Controls.Add(this.btnUpdate);
            this.pnlMain.Controls.Add(this.pnlSmileyFaces);
            this.pnlMain.Name = "pnlMain";
            // 
            // pnlBaseFields
            // 
            this.pnlBaseFields.BackColor = System.Drawing.SystemColors.Control;
            this.pnlBaseFields.Controls.Add(this.textBoxQuestionText);
            this.pnlBaseFields.Controls.Add(this.groupBox1);
            resources.ApplyResources(this.pnlBaseFields, "pnlBaseFields");
            this.pnlBaseFields.Name = "pnlBaseFields";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblCharNumber);
            this.groupBox1.Controls.Add(this.comboBoxQuestionTypes);
            this.groupBox1.Controls.Add(this.lblTypeOfQuestion);
            this.groupBox1.Controls.Add(this.lblQuestionText);
            this.groupBox1.Controls.Add(this.numericUpDownQuestionOrder);
            this.groupBox1.Controls.Add(this.lblQuestionOrder);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // pnlSmileyFaces
            // 
            this.pnlSmileyFaces.BackColor = System.Drawing.SystemColors.Control;
            this.pnlSmileyFaces.Controls.Add(this.lblFacesNumber);
            this.pnlSmileyFaces.Controls.Add(this.trackBarSmileyFaces);
            this.pnlSmileyFaces.Controls.Add(this.lblSmileyFaces);
            this.pnlSmileyFaces.Controls.Add(this.lblNumberofSmileyfaces);
            resources.ApplyResources(this.pnlSmileyFaces, "pnlSmileyFaces");
            this.pnlSmileyFaces.Name = "pnlSmileyFaces";
            // 
            // lblFacesNumber
            // 
            resources.ApplyResources(this.lblFacesNumber, "lblFacesNumber");
            this.lblFacesNumber.Name = "lblFacesNumber";
            // 
            // trackBarSmileyFaces
            // 
            resources.ApplyResources(this.trackBarSmileyFaces, "trackBarSmileyFaces");
            this.trackBarSmileyFaces.Maximum = 5;
            this.trackBarSmileyFaces.Minimum = 2;
            this.trackBarSmileyFaces.Name = "trackBarSmileyFaces";
            this.trackBarSmileyFaces.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarSmileyFaces.Value = 2;
            this.trackBarSmileyFaces.Scroll += new System.EventHandler(this.trackBarSmiley_Scroll);
            // 
            // lblSmileyFaces
            // 
            resources.ApplyResources(this.lblSmileyFaces, "lblSmileyFaces");
            this.lblSmileyFaces.Name = "lblSmileyFaces";
            // 
            // lblNumberofSmileyfaces
            // 
            resources.ApplyResources(this.lblNumberofSmileyfaces, "lblNumberofSmileyfaces");
            this.lblNumberofSmileyfaces.Name = "lblNumberofSmileyfaces";
            // 
            // DialogForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Name = "DialogForm";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuestionOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarStars)).EndInit();
            this.pnlStars.ResumeLayout(false);
            this.pnlStars.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.pnlSlider.ResumeLayout(false);
            this.pnlSlider.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEndValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartValue)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlBaseFields.ResumeLayout(false);
            this.pnlBaseFields.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlSmileyFaces.ResumeLayout(false);
            this.pnlSmileyFaces.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSmileyFaces)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox textBoxQuestionText;
        private System.Windows.Forms.Label lblQuestionText;
        private System.Windows.Forms.NumericUpDown numericUpDownQuestionOrder;
        private System.Windows.Forms.Label lblQuestionOrder;
        private System.Windows.Forms.ComboBox comboBoxQuestionTypes;
        private System.Windows.Forms.Label lblNumberOfStarsText;
        private System.Windows.Forms.TrackBar trackBarStars;
        private System.Windows.Forms.Label lblStars;
        private System.Windows.Forms.Label lblTypeOfQuestion;
        private System.Windows.Forms.Panel pnlStars;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Panel pnlSlider;
        private System.Windows.Forms.Label lblStartValue;
        private System.Windows.Forms.NumericUpDown numericUpDownStartValue;
        private System.Windows.Forms.Label lblEndValue;
        private System.Windows.Forms.NumericUpDown numericUpDownEndValue;
        private System.Windows.Forms.Label lblStartCaption;
        private System.Windows.Forms.Label lblEndCaption;
        private System.Windows.Forms.TextBox textBoxEndCaption;
        private System.Windows.Forms.TextBox textBoxStartCaption;
        private System.Windows.Forms.Label lblNumberOfStars;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblCharNumber;
        private System.Windows.Forms.Label lblCharNumberStartCaption;
        private System.Windows.Forms.Label lblCharNumberEndCaption;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlBaseFields;
        private System.Windows.Forms.Panel pnlSmileyFaces;
        private System.Windows.Forms.Label lblFacesNumber;
        private System.Windows.Forms.TrackBar trackBarSmileyFaces;
        private System.Windows.Forms.Label lblSmileyFaces;
        private System.Windows.Forms.Label lblNumberofSmileyfaces;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}