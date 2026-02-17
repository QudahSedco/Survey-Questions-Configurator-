namespace SurveyQuestionsConfigurator
{
    partial class Form2
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
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxQuestionText = new System.Windows.Forms.TextBox();
            this.lblQuestionText = new System.Windows.Forms.Label();
            this.numericUpDownQuestionOrder = new System.Windows.Forms.NumericUpDown();
            this.lblQuestionOrder = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lblNumberOfStarsText = new System.Windows.Forms.Label();
            this.trackBarStars = new System.Windows.Forms.TrackBar();
            this.lblStars = new System.Windows.Forms.Label();
            this.lblTypeOfQuestion = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblNumberOfStars = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblFacesNumber = new System.Windows.Forms.Label();
            this.trackBarSmileyFaces = new System.Windows.Forms.TrackBar();
            this.lblSmileyFaces = new System.Windows.Forms.Label();
            this.lblNumberofSmileyfaces = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
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
            this.panel4 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuestionOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarStars)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSmileyFaces)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEndValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartValue)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.pnlBaseFields.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(663, 389);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 28);
            this.button1.TabIndex = 0;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.lblQuestionText.Click += new System.EventHandler(this.label1_Click);
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
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.comboBox1.Location = new System.Drawing.Point(210, 158);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(303, 28);
            this.comboBox1.TabIndex = 5;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // lblNumberOfStarsText
            // 
            this.lblNumberOfStarsText.AutoSize = true;
            this.lblNumberOfStarsText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfStarsText.Location = new System.Drawing.Point(27, 23);
            this.lblNumberOfStarsText.Name = "lblNumberOfStarsText";
            this.lblNumberOfStarsText.Size = new System.Drawing.Size(122, 20);
            this.lblNumberOfStarsText.TabIndex = 6;
            this.lblNumberOfStarsText.Text = "Number of stars";
            this.lblNumberOfStarsText.Click += new System.EventHandler(this.label3_Click);
            // 
            // trackBarStars
            // 
            this.trackBarStars.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarStars.Location = new System.Drawing.Point(214, 52);
            this.trackBarStars.Name = "trackBarStars";
            this.trackBarStars.Size = new System.Drawing.Size(299, 45);
            this.trackBarStars.TabIndex = 7;
            this.trackBarStars.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarStars.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // lblStars
            // 
            this.lblStars.AutoSize = true;
            this.lblStars.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStars.Location = new System.Drawing.Point(237, 7);
            this.lblStars.Name = "lblStars";
            this.lblStars.Size = new System.Drawing.Size(0, 42);
            this.lblStars.TabIndex = 8;
            // 
            // lblTypeOfQuestion
            // 
            this.lblTypeOfQuestion.AutoSize = true;
            this.lblTypeOfQuestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTypeOfQuestion.Location = new System.Drawing.Point(27, 160);
            this.lblTypeOfQuestion.Name = "lblTypeOfQuestion";
            this.lblTypeOfQuestion.Size = new System.Drawing.Size(130, 20);
            this.lblTypeOfQuestion.TabIndex = 9;
            this.lblTypeOfQuestion.Text = "Type of question:";
            this.lblTypeOfQuestion.Click += new System.EventHandler(this.label5_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.lblNumberOfStars);
            this.panel1.Controls.Add(this.trackBarStars);
            this.panel1.Controls.Add(this.lblStars);
            this.panel1.Controls.Add(this.lblNumberOfStarsText);
            this.panel1.Location = new System.Drawing.Point(24, 200);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(711, 100);
            this.panel1.TabIndex = 10;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // lblNumberOfStars
            // 
            this.lblNumberOfStars.AutoSize = true;
            this.lblNumberOfStars.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfStars.Location = new System.Drawing.Point(518, 17);
            this.lblNumberOfStars.Name = "lblNumberOfStars";
            this.lblNumberOfStars.Size = new System.Drawing.Size(18, 20);
            this.lblNumberOfStars.TabIndex = 9;
            this.lblNumberOfStars.Text = "0";
            this.lblNumberOfStars.Click += new System.EventHandler(this.label6_Click_1);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.lblFacesNumber);
            this.panel2.Controls.Add(this.trackBarSmileyFaces);
            this.panel2.Controls.Add(this.lblSmileyFaces);
            this.panel2.Controls.Add(this.lblNumberofSmileyfaces);
            this.panel2.Location = new System.Drawing.Point(13, 192);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(695, 100);
            this.panel2.TabIndex = 11;
            // 
            // lblFacesNumber
            // 
            this.lblFacesNumber.AutoSize = true;
            this.lblFacesNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFacesNumber.Location = new System.Drawing.Point(518, 14);
            this.lblFacesNumber.Name = "lblFacesNumber";
            this.lblFacesNumber.Size = new System.Drawing.Size(18, 20);
            this.lblFacesNumber.TabIndex = 3;
            this.lblFacesNumber.Text = "2";
            this.lblFacesNumber.Click += new System.EventHandler(this.lblFacesNumber_Click);
            // 
            // trackBarSmileyFaces
            // 
            this.trackBarSmileyFaces.Location = new System.Drawing.Point(214, 52);
            this.trackBarSmileyFaces.Maximum = 5;
            this.trackBarSmileyFaces.Minimum = 2;
            this.trackBarSmileyFaces.Name = "trackBarSmileyFaces";
            this.trackBarSmileyFaces.Size = new System.Drawing.Size(299, 45);
            this.trackBarSmileyFaces.TabIndex = 2;
            this.trackBarSmileyFaces.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarSmileyFaces.Value = 2;
            this.trackBarSmileyFaces.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // lblSmileyFaces
            // 
            this.lblSmileyFaces.AutoSize = true;
            this.lblSmileyFaces.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSmileyFaces.Location = new System.Drawing.Point(299, 13);
            this.lblSmileyFaces.Name = "lblSmileyFaces";
            this.lblSmileyFaces.Size = new System.Drawing.Size(41, 29);
            this.lblSmileyFaces.TabIndex = 1;
            this.lblSmileyFaces.Text = ":):)";
            this.lblSmileyFaces.Click += new System.EventHandler(this.lblSmileyFaces_Click);
            // 
            // lblNumberofSmileyfaces
            // 
            this.lblNumberofSmileyfaces.AutoSize = true;
            this.lblNumberofSmileyfaces.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberofSmileyfaces.Location = new System.Drawing.Point(27, 20);
            this.lblNumberofSmileyfaces.Name = "lblNumberofSmileyfaces";
            this.lblNumberofSmileyfaces.Size = new System.Drawing.Size(176, 20);
            this.lblNumberofSmileyfaces.TabIndex = 0;
            this.lblNumberofSmileyfaces.Text = "Number of Smiley faces";
            this.lblNumberofSmileyfaces.Click += new System.EventHandler(this.lblNumberofSmileyfaces_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.Controls.Add(this.lblCharNumberEndCaption);
            this.panel3.Controls.Add(this.lblCharNumberStartCaption);
            this.panel3.Controls.Add(this.lblEndCaption);
            this.panel3.Controls.Add(this.textBoxEndCaption);
            this.panel3.Controls.Add(this.textBoxStartCaption);
            this.panel3.Controls.Add(this.lblStartCaption);
            this.panel3.Controls.Add(this.lblEndValue);
            this.panel3.Controls.Add(this.numericUpDownEndValue);
            this.panel3.Controls.Add(this.numericUpDownStartValue);
            this.panel3.Controls.Add(this.lblStartValue);
            this.panel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel3.Location = new System.Drawing.Point(24, 200);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(711, 146);
            this.panel3.TabIndex = 12;
            // 
            // lblCharNumberEndCaption
            // 
            this.lblCharNumberEndCaption.AutoSize = true;
            this.lblCharNumberEndCaption.Location = new System.Drawing.Point(518, 100);
            this.lblCharNumberEndCaption.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCharNumberEndCaption.Name = "lblCharNumberEndCaption";
            this.lblCharNumberEndCaption.Size = new System.Drawing.Size(44, 18);
            this.lblCharNumberEndCaption.TabIndex = 22;
            this.lblCharNumberEndCaption.Text = "0/100";
            // 
            // lblCharNumberStartCaption
            // 
            this.lblCharNumberStartCaption.AutoSize = true;
            this.lblCharNumberStartCaption.Location = new System.Drawing.Point(518, 70);
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
            this.lblEndValue.Click += new System.EventHandler(this.label7_Click);
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
            this.numericUpDownStartValue.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
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
            this.lblStartValue.Click += new System.EventHandler(this.label6_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Location = new System.Drawing.Point(663, 389);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 28);
            this.btnUpdate.TabIndex = 13;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(571, 389);
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
            this.lblCharNumber.Location = new System.Drawing.Point(518, 8);
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
            this.pnlMain.Controls.Add(this.pnlBaseFields);
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Controls.Add(this.button1);
            this.pnlMain.Controls.Add(this.btnCancel);
            this.pnlMain.Controls.Add(this.btnUpdate);
            this.pnlMain.Controls.Add(this.panel3);
            this.pnlMain.Controls.Add(this.panel4);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(750, 430);
            this.pnlMain.TabIndex = 18;
            // 
            // pnlBaseFields
            // 
            this.pnlBaseFields.BackColor = System.Drawing.SystemColors.Control;
            this.pnlBaseFields.Controls.Add(this.lblQuestionText);
            this.pnlBaseFields.Controls.Add(this.lblTypeOfQuestion);
            this.pnlBaseFields.Controls.Add(this.comboBox1);
            this.pnlBaseFields.Controls.Add(this.lblQuestionOrder);
            this.pnlBaseFields.Controls.Add(this.lblCharNumber);
            this.pnlBaseFields.Controls.Add(this.numericUpDownQuestionOrder);
            this.pnlBaseFields.Controls.Add(this.textBoxQuestionText);
            this.pnlBaseFields.Location = new System.Drawing.Point(24, 12);
            this.pnlBaseFields.Name = "pnlBaseFields";
            this.pnlBaseFields.Size = new System.Drawing.Size(714, 189);
            this.pnlBaseFields.TabIndex = 18;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.Control;
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Location = new System.Drawing.Point(11, 12);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(727, 363);
            this.panel4.TabIndex = 19;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 430);
            this.Controls.Add(this.pnlMain);
            this.Name = "Form2";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuestionOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarStars)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSmileyFaces)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEndValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartValue)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlBaseFields.ResumeLayout(false);
            this.pnlBaseFields.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxQuestionText;
        private System.Windows.Forms.Label lblQuestionText;
        private System.Windows.Forms.NumericUpDown numericUpDownQuestionOrder;
        private System.Windows.Forms.Label lblQuestionOrder;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label lblNumberOfStarsText;
        private System.Windows.Forms.TrackBar trackBarStars;
        private System.Windows.Forms.Label lblStars;
        private System.Windows.Forms.Label lblTypeOfQuestion;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblNumberofSmileyfaces;
        private System.Windows.Forms.Label lblSmileyFaces;
        private System.Windows.Forms.TrackBar trackBarSmileyFaces;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblStartValue;
        private System.Windows.Forms.NumericUpDown numericUpDownStartValue;
        private System.Windows.Forms.Label lblEndValue;
        private System.Windows.Forms.NumericUpDown numericUpDownEndValue;
        private System.Windows.Forms.Label lblStartCaption;
        private System.Windows.Forms.Label lblEndCaption;
        private System.Windows.Forms.TextBox textBoxEndCaption;
        private System.Windows.Forms.TextBox textBoxStartCaption;
        private System.Windows.Forms.Label lblFacesNumber;
        private System.Windows.Forms.Label lblNumberOfStars;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblCharNumber;
        private System.Windows.Forms.Label lblCharNumberStartCaption;
        private System.Windows.Forms.Label lblCharNumberEndCaption;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlBaseFields;
        private System.Windows.Forms.Panel panel4;
    }
}