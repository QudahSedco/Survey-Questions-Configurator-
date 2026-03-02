namespace SurveyQuestionsConfigurator
{
    partial class AddOrEditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddOrEditForm));
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
            this.BaseQuestionsGroupBox = new System.Windows.Forms.GroupBox();
            this.TypeGroupBox = new System.Windows.Forms.GroupBox();
            this.pnlSmileyFaces = new System.Windows.Forms.Panel();
            this.lblFacesNumber = new System.Windows.Forms.Label();
            this.trackBarSmileyFaces = new System.Windows.Forms.TrackBar();
            this.lblNumberofSmileyfaces = new System.Windows.Forms.Label();
            this.lblSmileyFaces = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuestionOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarStars)).BeginInit();
            this.pnlStars.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.pnlSlider.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEndValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartValue)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.BaseQuestionsGroupBox.SuspendLayout();
            this.TypeGroupBox.SuspendLayout();
            this.pnlSmileyFaces.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSmileyFaces)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.errorProvider.SetError(this.btnAdd, resources.GetString("btnAdd.Error"));
            this.errorProvider.SetIconAlignment(this.btnAdd, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnAdd.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.btnAdd, ((int)(resources.GetObject("btnAdd.IconPadding"))));
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // textBoxQuestionText
            // 
            resources.ApplyResources(this.textBoxQuestionText, "textBoxQuestionText");
            this.errorProvider.SetError(this.textBoxQuestionText, resources.GetString("textBoxQuestionText.Error"));
            this.errorProvider.SetIconAlignment(this.textBoxQuestionText, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxQuestionText.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.textBoxQuestionText, ((int)(resources.GetObject("textBoxQuestionText.IconPadding"))));
            this.textBoxQuestionText.Name = "textBoxQuestionText";
            this.textBoxQuestionText.TextChanged += new System.EventHandler(this.textBoxQuestionText_TextChanged);
            // 
            // lblQuestionText
            // 
            resources.ApplyResources(this.lblQuestionText, "lblQuestionText");
            this.errorProvider.SetError(this.lblQuestionText, resources.GetString("lblQuestionText.Error"));
            this.errorProvider.SetIconAlignment(this.lblQuestionText, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblQuestionText.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.lblQuestionText, ((int)(resources.GetObject("lblQuestionText.IconPadding"))));
            this.lblQuestionText.Name = "lblQuestionText";
            // 
            // numericUpDownQuestionOrder
            // 
            resources.ApplyResources(this.numericUpDownQuestionOrder, "numericUpDownQuestionOrder");
            this.errorProvider.SetError(this.numericUpDownQuestionOrder, resources.GetString("numericUpDownQuestionOrder.Error"));
            this.errorProvider.SetIconAlignment(this.numericUpDownQuestionOrder, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("numericUpDownQuestionOrder.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.numericUpDownQuestionOrder, ((int)(resources.GetObject("numericUpDownQuestionOrder.IconPadding"))));
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
            this.errorProvider.SetError(this.lblQuestionOrder, resources.GetString("lblQuestionOrder.Error"));
            this.errorProvider.SetIconAlignment(this.lblQuestionOrder, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblQuestionOrder.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.lblQuestionOrder, ((int)(resources.GetObject("lblQuestionOrder.IconPadding"))));
            this.lblQuestionOrder.Name = "lblQuestionOrder";
            // 
            // comboBoxQuestionTypes
            // 
            resources.ApplyResources(this.comboBoxQuestionTypes, "comboBoxQuestionTypes");
            this.comboBoxQuestionTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider.SetError(this.comboBoxQuestionTypes, resources.GetString("comboBoxQuestionTypes.Error"));
            this.comboBoxQuestionTypes.FormattingEnabled = true;
            this.errorProvider.SetIconAlignment(this.comboBoxQuestionTypes, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("comboBoxQuestionTypes.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.comboBoxQuestionTypes, ((int)(resources.GetObject("comboBoxQuestionTypes.IconPadding"))));
            this.comboBoxQuestionTypes.Name = "comboBoxQuestionTypes";
            this.comboBoxQuestionTypes.SelectedIndexChanged += new System.EventHandler(this.ComboBoxQuestionType_SelectedIndexChanged);
            // 
            // lblNumberOfStarsText
            // 
            resources.ApplyResources(this.lblNumberOfStarsText, "lblNumberOfStarsText");
            this.errorProvider.SetError(this.lblNumberOfStarsText, resources.GetString("lblNumberOfStarsText.Error"));
            this.errorProvider.SetIconAlignment(this.lblNumberOfStarsText, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblNumberOfStarsText.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.lblNumberOfStarsText, ((int)(resources.GetObject("lblNumberOfStarsText.IconPadding"))));
            this.lblNumberOfStarsText.Name = "lblNumberOfStarsText";
            // 
            // trackBarStars
            // 
            resources.ApplyResources(this.trackBarStars, "trackBarStars");
            this.errorProvider.SetError(this.trackBarStars, resources.GetString("trackBarStars.Error"));
            this.errorProvider.SetIconAlignment(this.trackBarStars, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("trackBarStars.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.trackBarStars, ((int)(resources.GetObject("trackBarStars.IconPadding"))));
            this.trackBarStars.Name = "trackBarStars";
            this.trackBarStars.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarStars.Scroll += new System.EventHandler(this.TrackBarStars_Scroll);
            // 
            // lblStars
            // 
            resources.ApplyResources(this.lblStars, "lblStars");
            this.errorProvider.SetError(this.lblStars, resources.GetString("lblStars.Error"));
            this.errorProvider.SetIconAlignment(this.lblStars, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblStars.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.lblStars, ((int)(resources.GetObject("lblStars.IconPadding"))));
            this.lblStars.Name = "lblStars";
            // 
            // lblTypeOfQuestion
            // 
            resources.ApplyResources(this.lblTypeOfQuestion, "lblTypeOfQuestion");
            this.errorProvider.SetError(this.lblTypeOfQuestion, resources.GetString("lblTypeOfQuestion.Error"));
            this.errorProvider.SetIconAlignment(this.lblTypeOfQuestion, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblTypeOfQuestion.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.lblTypeOfQuestion, ((int)(resources.GetObject("lblTypeOfQuestion.IconPadding"))));
            this.lblTypeOfQuestion.Name = "lblTypeOfQuestion";
            // 
            // pnlStars
            // 
            resources.ApplyResources(this.pnlStars, "pnlStars");
            this.pnlStars.BackColor = System.Drawing.SystemColors.Control;
            this.pnlStars.Controls.Add(this.lblNumberOfStars);
            this.pnlStars.Controls.Add(this.trackBarStars);
            this.pnlStars.Controls.Add(this.lblStars);
            this.pnlStars.Controls.Add(this.lblNumberOfStarsText);
            this.errorProvider.SetError(this.pnlStars, resources.GetString("pnlStars.Error"));
            this.errorProvider.SetIconAlignment(this.pnlStars, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("pnlStars.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.pnlStars, ((int)(resources.GetObject("pnlStars.IconPadding"))));
            this.pnlStars.Name = "pnlStars";
            // 
            // lblNumberOfStars
            // 
            resources.ApplyResources(this.lblNumberOfStars, "lblNumberOfStars");
            this.errorProvider.SetError(this.lblNumberOfStars, resources.GetString("lblNumberOfStars.Error"));
            this.errorProvider.SetIconAlignment(this.lblNumberOfStars, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblNumberOfStars.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.lblNumberOfStars, ((int)(resources.GetObject("lblNumberOfStars.IconPadding"))));
            this.lblNumberOfStars.Name = "lblNumberOfStars";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            resources.ApplyResources(this.errorProvider, "errorProvider");
            // 
            // pnlSlider
            // 
            resources.ApplyResources(this.pnlSlider, "pnlSlider");
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
            this.errorProvider.SetError(this.pnlSlider, resources.GetString("pnlSlider.Error"));
            this.pnlSlider.ForeColor = System.Drawing.SystemColors.ControlText;
            this.errorProvider.SetIconAlignment(this.pnlSlider, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("pnlSlider.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.pnlSlider, ((int)(resources.GetObject("pnlSlider.IconPadding"))));
            this.pnlSlider.Name = "pnlSlider";
            // 
            // lblCharNumberEndCaption
            // 
            resources.ApplyResources(this.lblCharNumberEndCaption, "lblCharNumberEndCaption");
            this.errorProvider.SetError(this.lblCharNumberEndCaption, resources.GetString("lblCharNumberEndCaption.Error"));
            this.errorProvider.SetIconAlignment(this.lblCharNumberEndCaption, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblCharNumberEndCaption.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.lblCharNumberEndCaption, ((int)(resources.GetObject("lblCharNumberEndCaption.IconPadding"))));
            this.lblCharNumberEndCaption.Name = "lblCharNumberEndCaption";
            // 
            // lblCharNumberStartCaption
            // 
            resources.ApplyResources(this.lblCharNumberStartCaption, "lblCharNumberStartCaption");
            this.errorProvider.SetError(this.lblCharNumberStartCaption, resources.GetString("lblCharNumberStartCaption.Error"));
            this.errorProvider.SetIconAlignment(this.lblCharNumberStartCaption, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblCharNumberStartCaption.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.lblCharNumberStartCaption, ((int)(resources.GetObject("lblCharNumberStartCaption.IconPadding"))));
            this.lblCharNumberStartCaption.Name = "lblCharNumberStartCaption";
            // 
            // lblEndCaption
            // 
            resources.ApplyResources(this.lblEndCaption, "lblEndCaption");
            this.errorProvider.SetError(this.lblEndCaption, resources.GetString("lblEndCaption.Error"));
            this.errorProvider.SetIconAlignment(this.lblEndCaption, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblEndCaption.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.lblEndCaption, ((int)(resources.GetObject("lblEndCaption.IconPadding"))));
            this.lblEndCaption.Name = "lblEndCaption";
            // 
            // textBoxEndCaption
            // 
            resources.ApplyResources(this.textBoxEndCaption, "textBoxEndCaption");
            this.errorProvider.SetError(this.textBoxEndCaption, resources.GetString("textBoxEndCaption.Error"));
            this.errorProvider.SetIconAlignment(this.textBoxEndCaption, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxEndCaption.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.textBoxEndCaption, ((int)(resources.GetObject("textBoxEndCaption.IconPadding"))));
            this.textBoxEndCaption.Name = "textBoxEndCaption";
            this.textBoxEndCaption.TextChanged += new System.EventHandler(this.textBoxEndCaption_TextChanged);
            // 
            // textBoxStartCaption
            // 
            resources.ApplyResources(this.textBoxStartCaption, "textBoxStartCaption");
            this.errorProvider.SetError(this.textBoxStartCaption, resources.GetString("textBoxStartCaption.Error"));
            this.errorProvider.SetIconAlignment(this.textBoxStartCaption, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxStartCaption.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.textBoxStartCaption, ((int)(resources.GetObject("textBoxStartCaption.IconPadding"))));
            this.textBoxStartCaption.Name = "textBoxStartCaption";
            this.textBoxStartCaption.TextChanged += new System.EventHandler(this.textBoxStartCaption_TextChanged);
            // 
            // lblStartCaption
            // 
            resources.ApplyResources(this.lblStartCaption, "lblStartCaption");
            this.errorProvider.SetError(this.lblStartCaption, resources.GetString("lblStartCaption.Error"));
            this.errorProvider.SetIconAlignment(this.lblStartCaption, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblStartCaption.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.lblStartCaption, ((int)(resources.GetObject("lblStartCaption.IconPadding"))));
            this.lblStartCaption.Name = "lblStartCaption";
            // 
            // lblEndValue
            // 
            resources.ApplyResources(this.lblEndValue, "lblEndValue");
            this.errorProvider.SetError(this.lblEndValue, resources.GetString("lblEndValue.Error"));
            this.errorProvider.SetIconAlignment(this.lblEndValue, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblEndValue.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.lblEndValue, ((int)(resources.GetObject("lblEndValue.IconPadding"))));
            this.lblEndValue.Name = "lblEndValue";
            // 
            // numericUpDownEndValue
            // 
            resources.ApplyResources(this.numericUpDownEndValue, "numericUpDownEndValue");
            this.errorProvider.SetError(this.numericUpDownEndValue, resources.GetString("numericUpDownEndValue.Error"));
            this.errorProvider.SetIconAlignment(this.numericUpDownEndValue, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("numericUpDownEndValue.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.numericUpDownEndValue, ((int)(resources.GetObject("numericUpDownEndValue.IconPadding"))));
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
            this.errorProvider.SetError(this.numericUpDownStartValue, resources.GetString("numericUpDownStartValue.Error"));
            this.errorProvider.SetIconAlignment(this.numericUpDownStartValue, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("numericUpDownStartValue.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.numericUpDownStartValue, ((int)(resources.GetObject("numericUpDownStartValue.IconPadding"))));
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
            this.errorProvider.SetError(this.lblStartValue, resources.GetString("lblStartValue.Error"));
            this.errorProvider.SetIconAlignment(this.lblStartValue, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblStartValue.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.lblStartValue, ((int)(resources.GetObject("lblStartValue.IconPadding"))));
            this.lblStartValue.Name = "lblStartValue";
            // 
            // btnUpdate
            // 
            resources.ApplyResources(this.btnUpdate, "btnUpdate");
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.errorProvider.SetError(this.btnUpdate, resources.GetString("btnUpdate.Error"));
            this.errorProvider.SetIconAlignment(this.btnUpdate, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnUpdate.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.btnUpdate, ((int)(resources.GetObject("btnUpdate.IconPadding"))));
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.errorProvider.SetError(this.btnCancel, resources.GetString("btnCancel.Error"));
            this.errorProvider.SetIconAlignment(this.btnCancel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnCancel.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.btnCancel, ((int)(resources.GetObject("btnCancel.IconPadding"))));
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblCharNumber
            // 
            resources.ApplyResources(this.lblCharNumber, "lblCharNumber");
            this.errorProvider.SetError(this.lblCharNumber, resources.GetString("lblCharNumber.Error"));
            this.errorProvider.SetIconAlignment(this.lblCharNumber, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblCharNumber.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.lblCharNumber, ((int)(resources.GetObject("lblCharNumber.IconPadding"))));
            this.lblCharNumber.Name = "lblCharNumber";
            // 
            // pnlMain
            // 
            resources.ApplyResources(this.pnlMain, "pnlMain");
            this.pnlMain.BackColor = System.Drawing.SystemColors.Control;
            this.pnlMain.Controls.Add(this.BaseQuestionsGroupBox);
            this.pnlMain.Controls.Add(this.TypeGroupBox);
            this.errorProvider.SetError(this.pnlMain, resources.GetString("pnlMain.Error"));
            this.errorProvider.SetIconAlignment(this.pnlMain, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("pnlMain.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.pnlMain, ((int)(resources.GetObject("pnlMain.IconPadding"))));
            this.pnlMain.Name = "pnlMain";
            // 
            // BaseQuestionsGroupBox
            // 
            resources.ApplyResources(this.BaseQuestionsGroupBox, "BaseQuestionsGroupBox");
            this.BaseQuestionsGroupBox.BackColor = System.Drawing.SystemColors.Control;
            this.BaseQuestionsGroupBox.Controls.Add(this.textBoxQuestionText);
            this.BaseQuestionsGroupBox.Controls.Add(this.lblCharNumber);
            this.BaseQuestionsGroupBox.Controls.Add(this.comboBoxQuestionTypes);
            this.BaseQuestionsGroupBox.Controls.Add(this.lblTypeOfQuestion);
            this.BaseQuestionsGroupBox.Controls.Add(this.lblQuestionText);
            this.BaseQuestionsGroupBox.Controls.Add(this.numericUpDownQuestionOrder);
            this.BaseQuestionsGroupBox.Controls.Add(this.lblQuestionOrder);
            this.errorProvider.SetError(this.BaseQuestionsGroupBox, resources.GetString("BaseQuestionsGroupBox.Error"));
            this.errorProvider.SetIconAlignment(this.BaseQuestionsGroupBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("BaseQuestionsGroupBox.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.BaseQuestionsGroupBox, ((int)(resources.GetObject("BaseQuestionsGroupBox.IconPadding"))));
            this.BaseQuestionsGroupBox.Name = "BaseQuestionsGroupBox";
            this.BaseQuestionsGroupBox.TabStop = false;
            // 
            // TypeGroupBox
            // 
            resources.ApplyResources(this.TypeGroupBox, "TypeGroupBox");
            this.TypeGroupBox.BackColor = System.Drawing.SystemColors.Control;
            this.TypeGroupBox.Controls.Add(this.pnlSmileyFaces);
            this.TypeGroupBox.Controls.Add(this.pnlStars);
            this.TypeGroupBox.Controls.Add(this.pnlSlider);
            this.errorProvider.SetError(this.TypeGroupBox, resources.GetString("TypeGroupBox.Error"));
            this.errorProvider.SetIconAlignment(this.TypeGroupBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("TypeGroupBox.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.TypeGroupBox, ((int)(resources.GetObject("TypeGroupBox.IconPadding"))));
            this.TypeGroupBox.Name = "TypeGroupBox";
            this.TypeGroupBox.TabStop = false;
            // 
            // pnlSmileyFaces
            // 
            resources.ApplyResources(this.pnlSmileyFaces, "pnlSmileyFaces");
            this.pnlSmileyFaces.BackColor = System.Drawing.SystemColors.Control;
            this.pnlSmileyFaces.Controls.Add(this.lblFacesNumber);
            this.pnlSmileyFaces.Controls.Add(this.trackBarSmileyFaces);
            this.pnlSmileyFaces.Controls.Add(this.lblNumberofSmileyfaces);
            this.pnlSmileyFaces.Controls.Add(this.lblSmileyFaces);
            this.errorProvider.SetError(this.pnlSmileyFaces, resources.GetString("pnlSmileyFaces.Error"));
            this.errorProvider.SetIconAlignment(this.pnlSmileyFaces, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("pnlSmileyFaces.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.pnlSmileyFaces, ((int)(resources.GetObject("pnlSmileyFaces.IconPadding"))));
            this.pnlSmileyFaces.Name = "pnlSmileyFaces";
            // 
            // lblFacesNumber
            // 
            resources.ApplyResources(this.lblFacesNumber, "lblFacesNumber");
            this.errorProvider.SetError(this.lblFacesNumber, resources.GetString("lblFacesNumber.Error"));
            this.errorProvider.SetIconAlignment(this.lblFacesNumber, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblFacesNumber.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.lblFacesNumber, ((int)(resources.GetObject("lblFacesNumber.IconPadding"))));
            this.lblFacesNumber.Name = "lblFacesNumber";
            // 
            // trackBarSmileyFaces
            // 
            resources.ApplyResources(this.trackBarSmileyFaces, "trackBarSmileyFaces");
            this.errorProvider.SetError(this.trackBarSmileyFaces, resources.GetString("trackBarSmileyFaces.Error"));
            this.errorProvider.SetIconAlignment(this.trackBarSmileyFaces, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("trackBarSmileyFaces.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.trackBarSmileyFaces, ((int)(resources.GetObject("trackBarSmileyFaces.IconPadding"))));
            this.trackBarSmileyFaces.Maximum = 5;
            this.trackBarSmileyFaces.Minimum = 2;
            this.trackBarSmileyFaces.Name = "trackBarSmileyFaces";
            this.trackBarSmileyFaces.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarSmileyFaces.Value = 2;
            this.trackBarSmileyFaces.Scroll += new System.EventHandler(this.trackBarSmiley_Scroll);
            // 
            // lblNumberofSmileyfaces
            // 
            resources.ApplyResources(this.lblNumberofSmileyfaces, "lblNumberofSmileyfaces");
            this.errorProvider.SetError(this.lblNumberofSmileyfaces, resources.GetString("lblNumberofSmileyfaces.Error"));
            this.errorProvider.SetIconAlignment(this.lblNumberofSmileyfaces, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblNumberofSmileyfaces.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.lblNumberofSmileyfaces, ((int)(resources.GetObject("lblNumberofSmileyfaces.IconPadding"))));
            this.lblNumberofSmileyfaces.Name = "lblNumberofSmileyfaces";
            // 
            // lblSmileyFaces
            // 
            resources.ApplyResources(this.lblSmileyFaces, "lblSmileyFaces");
            this.errorProvider.SetError(this.lblSmileyFaces, resources.GetString("lblSmileyFaces.Error"));
            this.errorProvider.SetIconAlignment(this.lblSmileyFaces, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblSmileyFaces.IconAlignment"))));
            this.errorProvider.SetIconPadding(this.lblSmileyFaces, ((int)(resources.GetObject("lblSmileyFaces.IconPadding"))));
            this.lblSmileyFaces.Name = "lblSmileyFaces";
            // 
            // AddOrEditForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.pnlMain);
            this.Name = "AddOrEditForm";
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
            this.BaseQuestionsGroupBox.ResumeLayout(false);
            this.BaseQuestionsGroupBox.PerformLayout();
            this.TypeGroupBox.ResumeLayout(false);
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
        private System.Windows.Forms.Panel pnlSmileyFaces;
        private System.Windows.Forms.Label lblFacesNumber;
        private System.Windows.Forms.TrackBar trackBarSmileyFaces;
        private System.Windows.Forms.Label lblSmileyFaces;
        private System.Windows.Forms.Label lblNumberofSmileyfaces;
        private System.Windows.Forms.GroupBox BaseQuestionsGroupBox;
        private System.Windows.Forms.GroupBox TypeGroupBox;
    }
}