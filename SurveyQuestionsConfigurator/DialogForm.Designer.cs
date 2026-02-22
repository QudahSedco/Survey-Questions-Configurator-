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
            this.pnlSmileyFaces.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSmileyFaces)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(508, 323);
            this.btnAdd.Name = "button1";
            this.btnAdd.Size = new System.Drawing.Size(75, 28);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Save";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // textBoxQuestionText
            // 
            this.textBoxQuestionText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxQuestionText.Location = new System.Drawing.Point(210, 8);
            this.textBoxQuestionText.MaxLength = 1000;
            this.textBoxQuestionText.Multiline = true;
            this.textBoxQuestionText.Name = "textBoxQuestionText";
            this.textBoxQuestionText.Size = new System.Drawing.Size(303, 109);
            this.textBoxQuestionText.TabIndex = 1;
            this.textBoxQuestionText.TextChanged += new System.EventHandler(this.textBoxQuestionText_TextChanged);
            // 
            // lblQuestionText
            // 
            this.lblQuestionText.AutoSize = true;
            this.lblQuestionText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestionText.Location = new System.Drawing.Point(27, 51);
            this.lblQuestionText.Name = "lblQuestionText";
            this.lblQuestionText.Size = new System.Drawing.Size(103, 20);
            this.lblQuestionText.TabIndex = 2;
            this.lblQuestionText.Text = "Question text";
            // 
            // numericUpDownQuestionOrder
            // 
            this.numericUpDownQuestionOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownQuestionOrder.Location = new System.Drawing.Point(210, 127);
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
            this.numericUpDownQuestionOrder.Size = new System.Drawing.Size(303, 26);
            this.numericUpDownQuestionOrder.TabIndex = 3;
            this.numericUpDownQuestionOrder.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownQuestionOrder.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblQuestionOrder
            // 
            this.lblQuestionOrder.AutoSize = true;
            this.lblQuestionOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestionOrder.Location = new System.Drawing.Point(27, 129);
            this.lblQuestionOrder.Name = "lblQuestionOrder";
            this.lblQuestionOrder.Size = new System.Drawing.Size(118, 20);
            this.lblQuestionOrder.TabIndex = 4;
            this.lblQuestionOrder.Text = "Question order:";
            // 
            // comboBoxQuestionTypes
            // 
            this.comboBoxQuestionTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxQuestionTypes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxQuestionTypes.FormattingEnabled = true;
            this.comboBoxQuestionTypes.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.comboBoxQuestionTypes.Location = new System.Drawing.Point(210, 158);
            this.comboBoxQuestionTypes.Name = "comboBoxQuestionTypes";
            this.comboBoxQuestionTypes.Size = new System.Drawing.Size(303, 28);
            this.comboBoxQuestionTypes.TabIndex = 5;
            this.comboBoxQuestionTypes.SelectedIndexChanged += new System.EventHandler(this.comboBoxQuestionType_SelectedIndexChanged);
            // 
            // lblNumberOfStarsText
            // 
            this.lblNumberOfStarsText.AutoSize = true;
            this.lblNumberOfStarsText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfStarsText.Location = new System.Drawing.Point(27, 13);
            this.lblNumberOfStarsText.Name = "lblNumberOfStarsText";
            this.lblNumberOfStarsText.Size = new System.Drawing.Size(122, 20);
            this.lblNumberOfStarsText.TabIndex = 6;
            this.lblNumberOfStarsText.Text = "Number of stars";
            // 
            // trackBarStars
            // 
            this.trackBarStars.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarStars.Location = new System.Drawing.Point(210, 32);
            this.trackBarStars.Name = "trackBarStars";
            this.trackBarStars.Size = new System.Drawing.Size(303, 45);
            this.trackBarStars.TabIndex = 7;
            this.trackBarStars.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarStars.Scroll += new System.EventHandler(this.trackBarStars_Scroll);
            // 
            // lblStars
            // 
            this.lblStars.AutoSize = true;
            this.lblStars.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStars.Location = new System.Drawing.Point(254, 4);
            this.lblStars.Name = "lblStars";
            this.lblStars.Size = new System.Drawing.Size(217, 37);
            this.lblStars.TabIndex = 8;
            this.lblStars.Text = "☆☆☆☆☆☆☆☆☆☆";
            // 
            // lblTypeOfQuestion
            // 
            this.lblTypeOfQuestion.AutoSize = true;
            this.lblTypeOfQuestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTypeOfQuestion.Location = new System.Drawing.Point(27, 161);
            this.lblTypeOfQuestion.Name = "lblTypeOfQuestion";
            this.lblTypeOfQuestion.Size = new System.Drawing.Size(130, 20);
            this.lblTypeOfQuestion.TabIndex = 9;
            this.lblTypeOfQuestion.Text = "Type of question:";
            // 
            // pnlStars
            // 
            this.pnlStars.BackColor = System.Drawing.SystemColors.Control;
            this.pnlStars.Controls.Add(this.lblNumberOfStars);
            this.pnlStars.Controls.Add(this.trackBarStars);
            this.pnlStars.Controls.Add(this.lblStars);
            this.pnlStars.Controls.Add(this.lblNumberOfStarsText);
            this.pnlStars.Location = new System.Drawing.Point(-11, 187);
            this.pnlStars.Name = "pnlStars";
            this.pnlStars.Size = new System.Drawing.Size(612, 100);
            this.pnlStars.TabIndex = 10;
            // 
            // lblNumberOfStars
            // 
            this.lblNumberOfStars.AutoSize = true;
            this.lblNumberOfStars.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfStars.Location = new System.Drawing.Point(518, 6);
            this.lblNumberOfStars.Name = "lblNumberOfStars";
            this.lblNumberOfStars.Size = new System.Drawing.Size(18, 20);
            this.lblNumberOfStars.TabIndex = 9;
            this.lblNumberOfStars.Text = "0";
            // 
            // errorProvider1
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
            this.pnlSlider.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSlider.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pnlSlider.Location = new System.Drawing.Point(-11, 188);
            this.pnlSlider.Name = "pnlSlider";
            this.pnlSlider.Size = new System.Drawing.Size(612, 129);
            this.pnlSlider.TabIndex = 12;
            // 
            // lblCharNumberEndCaption
            // 
            this.lblCharNumberEndCaption.AutoSize = true;
            this.lblCharNumberEndCaption.Location = new System.Drawing.Point(527, 100);
            this.lblCharNumberEndCaption.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCharNumberEndCaption.Name = "lblCharNumberEndCaption";
            this.lblCharNumberEndCaption.Size = new System.Drawing.Size(44, 18);
            this.lblCharNumberEndCaption.TabIndex = 22;
            this.lblCharNumberEndCaption.Text = "0/100";
            // 
            // lblCharNumberStartCaption
            // 
            this.lblCharNumberStartCaption.AutoSize = true;
            this.lblCharNumberStartCaption.Location = new System.Drawing.Point(527, 70);
            this.lblCharNumberStartCaption.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCharNumberStartCaption.Name = "lblCharNumberStartCaption";
            this.lblCharNumberStartCaption.Size = new System.Drawing.Size(44, 18);
            this.lblCharNumberStartCaption.TabIndex = 21;
            this.lblCharNumberStartCaption.Text = "0/100";
            // 
            // lblEndCaption
            // 
            this.lblEndCaption.AutoSize = true;
            this.lblEndCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndCaption.Location = new System.Drawing.Point(27, 102);
            this.lblEndCaption.Name = "lblEndCaption";
            this.lblEndCaption.Size = new System.Drawing.Size(98, 20);
            this.lblEndCaption.TabIndex = 18;
            this.lblEndCaption.Text = "End caption:";
            // 
            // textBoxEndCaption
            // 
            this.textBoxEndCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxEndCaption.Location = new System.Drawing.Point(210, 100);
            this.textBoxEndCaption.MaxLength = 100;
            this.textBoxEndCaption.Name = "textBoxEndCaption";
            this.textBoxEndCaption.Size = new System.Drawing.Size(303, 24);
            this.textBoxEndCaption.TabIndex = 17;
            this.textBoxEndCaption.TextChanged += new System.EventHandler(this.textBoxEndCaption_TextChanged);
            // 
            // textBoxStartCaption
            // 
            this.textBoxStartCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxStartCaption.Location = new System.Drawing.Point(210, 70);
            this.textBoxStartCaption.MaxLength = 100;
            this.textBoxStartCaption.Name = "textBoxStartCaption";
            this.textBoxStartCaption.Size = new System.Drawing.Size(303, 24);
            this.textBoxStartCaption.TabIndex = 13;
            this.textBoxStartCaption.TextChanged += new System.EventHandler(this.textBoxStartCaption_TextChanged);
            // 
            // lblStartCaption
            // 
            this.lblStartCaption.AutoSize = true;
            this.lblStartCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartCaption.Location = new System.Drawing.Point(27, 72);
            this.lblStartCaption.Name = "lblStartCaption";
            this.lblStartCaption.Size = new System.Drawing.Size(104, 20);
            this.lblStartCaption.TabIndex = 13;
            this.lblStartCaption.Text = "Start caption:";
            // 
            // lblEndValue
            // 
            this.lblEndValue.AutoSize = true;
            this.lblEndValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndValue.Location = new System.Drawing.Point(27, 40);
            this.lblEndValue.Name = "lblEndValue";
            this.lblEndValue.Size = new System.Drawing.Size(83, 20);
            this.lblEndValue.TabIndex = 16;
            this.lblEndValue.Text = "End value:";
            // 
            // numericUpDownEndValue
            // 
            this.numericUpDownEndValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownEndValue.Location = new System.Drawing.Point(210, 38);
            this.numericUpDownEndValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownEndValue.Name = "numericUpDownEndValue";
            this.numericUpDownEndValue.Size = new System.Drawing.Size(303, 26);
            this.numericUpDownEndValue.TabIndex = 15;
            this.numericUpDownEndValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownEndValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDownStartValue
            // 
            this.numericUpDownStartValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownStartValue.Location = new System.Drawing.Point(210, 6);
            this.numericUpDownStartValue.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numericUpDownStartValue.Name = "numericUpDownStartValue";
            this.numericUpDownStartValue.Size = new System.Drawing.Size(303, 26);
            this.numericUpDownStartValue.TabIndex = 14;
            this.numericUpDownStartValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownStartValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblStartValue
            // 
            this.lblStartValue.AutoSize = true;
            this.lblStartValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartValue.Location = new System.Drawing.Point(27, 8);
            this.lblStartValue.Name = "lblStartValue";
            this.lblStartValue.Size = new System.Drawing.Size(89, 20);
            this.lblStartValue.TabIndex = 13;
            this.lblStartValue.Text = "Start value:";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Location = new System.Drawing.Point(507, 323);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 28);
            this.btnUpdate.TabIndex = 13;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(427, 323);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 28);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblCharNumber
            // 
            this.lblCharNumber.AutoSize = true;
            this.lblCharNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCharNumber.Location = new System.Drawing.Point(527, 8);
            this.lblCharNumber.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCharNumber.Name = "lblCharNumber";
            this.lblCharNumber.Size = new System.Drawing.Size(58, 20);
            this.lblCharNumber.TabIndex = 16;
            this.lblCharNumber.Text = "0/1000";
            // 
            // pnlMain
            // 
            this.pnlMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlMain.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.pnlSlider);
            this.pnlMain.Controls.Add(this.pnlStars);
            this.pnlMain.Controls.Add(this.pnlBaseFields);
            this.pnlMain.Controls.Add(this.btnCancel);
            this.pnlMain.Controls.Add(this.btnAdd);
            this.pnlMain.Controls.Add(this.btnUpdate);
            this.pnlMain.Controls.Add(this.pnlSmileyFaces);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(602, 362);
            this.pnlMain.TabIndex = 18;
            // 
            // pnlBaseFields
            // 
            this.pnlBaseFields.BackColor = System.Drawing.SystemColors.Control;
            this.pnlBaseFields.Controls.Add(this.lblQuestionText);
            this.pnlBaseFields.Controls.Add(this.lblTypeOfQuestion);
            this.pnlBaseFields.Controls.Add(this.comboBoxQuestionTypes);
            this.pnlBaseFields.Controls.Add(this.lblQuestionOrder);
            this.pnlBaseFields.Controls.Add(this.lblCharNumber);
            this.pnlBaseFields.Controls.Add(this.numericUpDownQuestionOrder);
            this.pnlBaseFields.Controls.Add(this.textBoxQuestionText);
            this.pnlBaseFields.Location = new System.Drawing.Point(-11, 0);
            this.pnlBaseFields.Name = "pnlBaseFields";
            this.pnlBaseFields.Size = new System.Drawing.Size(612, 189);
            this.pnlBaseFields.TabIndex = 18;
            // 
            // pnlSmileyFaces
            // 
            this.pnlSmileyFaces.BackColor = System.Drawing.SystemColors.Control;
            this.pnlSmileyFaces.Controls.Add(this.lblFacesNumber);
            this.pnlSmileyFaces.Controls.Add(this.trackBarSmileyFaces);
            this.pnlSmileyFaces.Controls.Add(this.lblSmileyFaces);
            this.pnlSmileyFaces.Controls.Add(this.lblNumberofSmileyfaces);
            this.pnlSmileyFaces.Location = new System.Drawing.Point(-11, 187);
            this.pnlSmileyFaces.Name = "pnlSmileyFaces";
            this.pnlSmileyFaces.Size = new System.Drawing.Size(603, 100);
            this.pnlSmileyFaces.TabIndex = 11;
            // 
            // lblFacesNumber
            // 
            this.lblFacesNumber.AutoSize = true;
            this.lblFacesNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFacesNumber.Location = new System.Drawing.Point(518, 4);
            this.lblFacesNumber.Name = "lblFacesNumber";
            this.lblFacesNumber.Size = new System.Drawing.Size(18, 20);
            this.lblFacesNumber.TabIndex = 3;
            this.lblFacesNumber.Text = "2";
            // 
            // trackBarSmileyFaces
            // 
            this.trackBarSmileyFaces.Location = new System.Drawing.Point(210, 39);
            this.trackBarSmileyFaces.Maximum = 5;
            this.trackBarSmileyFaces.Minimum = 2;
            this.trackBarSmileyFaces.Name = "trackBarSmileyFaces";
            this.trackBarSmileyFaces.Size = new System.Drawing.Size(303, 45);
            this.trackBarSmileyFaces.TabIndex = 2;
            this.trackBarSmileyFaces.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarSmileyFaces.Value = 2;
            this.trackBarSmileyFaces.Scroll += new System.EventHandler(this.trackBarSmiley_Scroll);
            // 
            // lblSmileyFaces
            // 
            this.lblSmileyFaces.AutoSize = true;
            this.lblSmileyFaces.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSmileyFaces.Location = new System.Drawing.Point(307, 5);
            this.lblSmileyFaces.Name = "lblSmileyFaces";
            this.lblSmileyFaces.Size = new System.Drawing.Size(41, 29);
            this.lblSmileyFaces.TabIndex = 1;
            this.lblSmileyFaces.Text = ":):)";
            // 
            // lblNumberofSmileyfaces
            // 
            this.lblNumberofSmileyfaces.AutoSize = true;
            this.lblNumberofSmileyfaces.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberofSmileyfaces.Location = new System.Drawing.Point(27, 9);
            this.lblNumberofSmileyfaces.Name = "lblNumberofSmileyfaces";
            this.lblNumberofSmileyfaces.Size = new System.Drawing.Size(176, 20);
            this.lblNumberofSmileyfaces.TabIndex = 0;
            this.lblNumberofSmileyfaces.Text = "Number of Smiley faces";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 362);
            this.Controls.Add(this.pnlMain);
            this.Name = "Form2";
            this.Text = "Form2";
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
    }
}