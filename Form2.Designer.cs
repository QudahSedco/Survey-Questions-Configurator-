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
            this.label3 = new System.Windows.Forms.Label();
            this.trackBarStars = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblMaxStart = new System.Windows.Forms.Label();
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
            this.lblNumberOfCharacters = new System.Windows.Forms.Label();
            this.lblCharNumber = new System.Windows.Forms.Label();
            this.lblMaxChar = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuestionOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarStars)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSmileyFaces)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEndValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartValue)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(479, 378);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 34);
            this.button1.TabIndex = 0;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxQuestionText
            // 
            this.textBoxQuestionText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxQuestionText.Location = new System.Drawing.Point(219, 13);
            this.textBoxQuestionText.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxQuestionText.MaxLength = 1000;
            this.textBoxQuestionText.Multiline = true;
            this.textBoxQuestionText.Name = "textBoxQuestionText";
            this.textBoxQuestionText.Size = new System.Drawing.Size(217, 85);
            this.textBoxQuestionText.TabIndex = 1;
            this.textBoxQuestionText.TextChanged += new System.EventHandler(this.textBoxQuestionText_TextChanged);
            // 
            // lblQuestionText
            // 
            this.lblQuestionText.AutoSize = true;
            this.lblQuestionText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestionText.Location = new System.Drawing.Point(59, 32);
            this.lblQuestionText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQuestionText.Name = "lblQuestionText";
            this.lblQuestionText.Size = new System.Drawing.Size(107, 20);
            this.lblQuestionText.TabIndex = 2;
            this.lblQuestionText.Text = "Question Text";
            this.lblQuestionText.Click += new System.EventHandler(this.label1_Click);
            // 
            // numericUpDownQuestionOrder
            // 
            this.numericUpDownQuestionOrder.Location = new System.Drawing.Point(222, 106);
            this.numericUpDownQuestionOrder.Margin = new System.Windows.Forms.Padding(4);
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
            this.numericUpDownQuestionOrder.Size = new System.Drawing.Size(219, 22);
            this.numericUpDownQuestionOrder.TabIndex = 3;
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
            this.lblQuestionOrder.Location = new System.Drawing.Point(52, 105);
            this.lblQuestionOrder.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQuestionOrder.Name = "lblQuestionOrder";
            this.lblQuestionOrder.Size = new System.Drawing.Size(117, 20);
            this.lblQuestionOrder.TabIndex = 4;
            this.lblQuestionOrder.Text = "Question Order";
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(222, 145);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(217, 28);
            this.comboBox1.TabIndex = 5;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 28);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Number of stars";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // trackBarStars
            // 
            this.trackBarStars.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarStars.Location = new System.Drawing.Point(261, 61);
            this.trackBarStars.Margin = new System.Windows.Forms.Padding(4);
            this.trackBarStars.Name = "trackBarStars";
            this.trackBarStars.Size = new System.Drawing.Size(161, 45);
            this.trackBarStars.TabIndex = 7;
            this.trackBarStars.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarStars.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(192, 20);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 33);
            this.label4.TabIndex = 8;
            // 
            // lblTypeOfQuestion
            // 
            this.lblTypeOfQuestion.AutoSize = true;
            this.lblTypeOfQuestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTypeOfQuestion.Location = new System.Drawing.Point(43, 153);
            this.lblTypeOfQuestion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTypeOfQuestion.Name = "lblTypeOfQuestion";
            this.lblTypeOfQuestion.Size = new System.Drawing.Size(126, 20);
            this.lblTypeOfQuestion.TabIndex = 9;
            this.lblTypeOfQuestion.Text = "Type of question";
            this.lblTypeOfQuestion.Click += new System.EventHandler(this.label5_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblNumberOfStars);
            this.panel1.Controls.Add(this.trackBarStars);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(14, 186);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(633, 123);
            this.panel1.TabIndex = 10;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // lblNumberOfStars
            // 
            this.lblNumberOfStars.AutoSize = true;
            this.lblNumberOfStars.Location = new System.Drawing.Point(458, 33);
            this.lblNumberOfStars.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNumberOfStars.Name = "lblNumberOfStars";
            this.lblNumberOfStars.Size = new System.Drawing.Size(14, 16);
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
            this.panel2.Controls.Add(this.lblFacesNumber);
            this.panel2.Controls.Add(this.trackBarSmileyFaces);
            this.panel2.Controls.Add(this.lblSmileyFaces);
            this.panel2.Controls.Add(this.lblNumberofSmileyfaces);
            this.panel2.Location = new System.Drawing.Point(36, 190);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(463, 123);
            this.panel2.TabIndex = 11;
            // 
            // lblFacesNumber
            // 
            this.lblFacesNumber.AutoSize = true;
            this.lblFacesNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFacesNumber.Location = new System.Drawing.Point(374, 31);
            this.lblFacesNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFacesNumber.Name = "lblFacesNumber";
            this.lblFacesNumber.Size = new System.Drawing.Size(14, 16);
            this.lblFacesNumber.TabIndex = 3;
            this.lblFacesNumber.Text = "2";
            this.lblFacesNumber.Click += new System.EventHandler(this.lblFacesNumber_Click);
            // 
            // trackBarSmileyFaces
            // 
            this.trackBarSmileyFaces.Location = new System.Drawing.Point(261, 64);
            this.trackBarSmileyFaces.Margin = new System.Windows.Forms.Padding(4);
            this.trackBarSmileyFaces.Maximum = 5;
            this.trackBarSmileyFaces.Minimum = 2;
            this.trackBarSmileyFaces.Name = "trackBarSmileyFaces";
            this.trackBarSmileyFaces.Size = new System.Drawing.Size(139, 45);
            this.trackBarSmileyFaces.TabIndex = 2;
            this.trackBarSmileyFaces.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarSmileyFaces.Value = 2;
            this.trackBarSmileyFaces.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // lblSmileyFaces
            // 
            this.lblSmileyFaces.AutoSize = true;
            this.lblSmileyFaces.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSmileyFaces.Location = new System.Drawing.Point(275, 27);
            this.lblSmileyFaces.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSmileyFaces.Name = "lblSmileyFaces";
            this.lblSmileyFaces.Size = new System.Drawing.Size(27, 20);
            this.lblSmileyFaces.TabIndex = 1;
            this.lblSmileyFaces.Text = ":):)";
            this.lblSmileyFaces.Click += new System.EventHandler(this.lblSmileyFaces_Click);
            // 
            // lblNumberofSmileyfaces
            // 
            this.lblNumberofSmileyfaces.AutoSize = true;
            this.lblNumberofSmileyfaces.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberofSmileyfaces.Location = new System.Drawing.Point(6, 32);
            this.lblNumberofSmileyfaces.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNumberofSmileyfaces.Name = "lblNumberofSmileyfaces";
            this.lblNumberofSmileyfaces.Size = new System.Drawing.Size(176, 20);
            this.lblNumberofSmileyfaces.TabIndex = 0;
            this.lblNumberofSmileyfaces.Text = "Number of Smiley faces";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblCharNumberEndCaption);
            this.panel3.Controls.Add(this.lblCharNumberStartCaption);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.lblMaxStart);
            this.panel3.Controls.Add(this.lblEndCaption);
            this.panel3.Controls.Add(this.textBoxEndCaption);
            this.panel3.Controls.Add(this.textBoxStartCaption);
            this.panel3.Controls.Add(this.lblStartCaption);
            this.panel3.Controls.Add(this.lblEndValue);
            this.panel3.Controls.Add(this.numericUpDownEndValue);
            this.panel3.Controls.Add(this.numericUpDownStartValue);
            this.panel3.Controls.Add(this.lblStartValue);
            this.panel3.Location = new System.Drawing.Point(47, 192);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(532, 178);
            this.panel3.TabIndex = 12;
            // 
            // lblCharNumberEndCaption
            // 
            this.lblCharNumberEndCaption.AutoSize = true;
            this.lblCharNumberEndCaption.Location = new System.Drawing.Point(412, 130);
            this.lblCharNumberEndCaption.Name = "lblCharNumberEndCaption";
            this.lblCharNumberEndCaption.Size = new System.Drawing.Size(0, 16);
            this.lblCharNumberEndCaption.TabIndex = 22;
            // 
            // lblCharNumberStartCaption
            // 
            this.lblCharNumberStartCaption.AutoSize = true;
            this.lblCharNumberStartCaption.Location = new System.Drawing.Point(412, 97);
            this.lblCharNumberStartCaption.Name = "lblCharNumberStartCaption";
            this.lblCharNumberStartCaption.Size = new System.Drawing.Size(0, 16);
            this.lblCharNumberStartCaption.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(445, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 16);
            this.label1.TabIndex = 20;
            this.label1.Text = "Max(100)";
            // 
            // lblMaxStart
            // 
            this.lblMaxStart.AutoSize = true;
            this.lblMaxStart.Location = new System.Drawing.Point(444, 92);
            this.lblMaxStart.Name = "lblMaxStart";
            this.lblMaxStart.Size = new System.Drawing.Size(61, 16);
            this.lblMaxStart.TabIndex = 19;
            this.lblMaxStart.Text = "Max(100)";
            // 
            // lblEndCaption
            // 
            this.lblEndCaption.AutoSize = true;
            this.lblEndCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndCaption.Location = new System.Drawing.Point(32, 130);
            this.lblEndCaption.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEndCaption.Name = "lblEndCaption";
            this.lblEndCaption.Size = new System.Drawing.Size(94, 20);
            this.lblEndCaption.TabIndex = 18;
            this.lblEndCaption.Text = "End caption";
            // 
            // textBoxEndCaption
            // 
            this.textBoxEndCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxEndCaption.Location = new System.Drawing.Point(175, 124);
            this.textBoxEndCaption.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxEndCaption.MaxLength = 100;
            this.textBoxEndCaption.Name = "textBoxEndCaption";
            this.textBoxEndCaption.Size = new System.Drawing.Size(217, 24);
            this.textBoxEndCaption.TabIndex = 17;
            this.textBoxEndCaption.TextChanged += new System.EventHandler(this.textBoxEndCaption_TextChanged);
            // 
            // textBoxStartCaption
            // 
            this.textBoxStartCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxStartCaption.Location = new System.Drawing.Point(175, 89);
            this.textBoxStartCaption.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxStartCaption.MaxLength = 100;
            this.textBoxStartCaption.Name = "textBoxStartCaption";
            this.textBoxStartCaption.Size = new System.Drawing.Size(217, 24);
            this.textBoxStartCaption.TabIndex = 13;
            this.textBoxStartCaption.TextChanged += new System.EventHandler(this.textBoxStartCaption_TextChanged);
            // 
            // lblStartCaption
            // 
            this.lblStartCaption.AutoSize = true;
            this.lblStartCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartCaption.Location = new System.Drawing.Point(23, 91);
            this.lblStartCaption.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStartCaption.Name = "lblStartCaption";
            this.lblStartCaption.Size = new System.Drawing.Size(100, 20);
            this.lblStartCaption.TabIndex = 13;
            this.lblStartCaption.Text = "Start caption";
            // 
            // lblEndValue
            // 
            this.lblEndValue.AutoSize = true;
            this.lblEndValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndValue.Location = new System.Drawing.Point(49, 55);
            this.lblEndValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEndValue.Name = "lblEndValue";
            this.lblEndValue.Size = new System.Drawing.Size(79, 20);
            this.lblEndValue.TabIndex = 16;
            this.lblEndValue.Text = "End value";
            this.lblEndValue.Click += new System.EventHandler(this.label7_Click);
            // 
            // numericUpDownEndValue
            // 
            this.numericUpDownEndValue.Location = new System.Drawing.Point(175, 55);
            this.numericUpDownEndValue.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownEndValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownEndValue.Name = "numericUpDownEndValue";
            this.numericUpDownEndValue.Size = new System.Drawing.Size(219, 22);
            this.numericUpDownEndValue.TabIndex = 15;
            this.numericUpDownEndValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDownStartValue
            // 
            this.numericUpDownStartValue.Location = new System.Drawing.Point(175, 13);
            this.numericUpDownStartValue.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownStartValue.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numericUpDownStartValue.Name = "numericUpDownStartValue";
            this.numericUpDownStartValue.Size = new System.Drawing.Size(219, 22);
            this.numericUpDownStartValue.TabIndex = 14;
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
            this.lblStartValue.Location = new System.Drawing.Point(42, 15);
            this.lblStartValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStartValue.Name = "lblStartValue";
            this.lblStartValue.Size = new System.Drawing.Size(85, 20);
            this.lblStartValue.TabIndex = 13;
            this.lblStartValue.Text = "Start value";
            this.lblStartValue.Click += new System.EventHandler(this.label6_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Location = new System.Drawing.Point(479, 378);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(100, 34);
            this.btnUpdate.TabIndex = 13;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(105, 378);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 34);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblNumberOfCharacters
            // 
            this.lblNumberOfCharacters.AutoSize = true;
            this.lblNumberOfCharacters.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.lblNumberOfCharacters.Location = new System.Drawing.Point(444, 16);
            this.lblNumberOfCharacters.Name = "lblNumberOfCharacters";
            this.lblNumberOfCharacters.Size = new System.Drawing.Size(135, 16);
            this.lblNumberOfCharacters.TabIndex = 15;
            this.lblNumberOfCharacters.Text = "Number of characters";
            this.lblNumberOfCharacters.Click += new System.EventHandler(this.lblNumberOfCharacters_Click);
            // 
            // lblCharNumber
            // 
            this.lblCharNumber.AutoSize = true;
            this.lblCharNumber.Location = new System.Drawing.Point(484, 36);
            this.lblCharNumber.Name = "lblCharNumber";
            this.lblCharNumber.Size = new System.Drawing.Size(0, 16);
            this.lblCharNumber.TabIndex = 16;
            // 
            // lblMaxChar
            // 
            this.lblMaxChar.AutoSize = true;
            this.lblMaxChar.Location = new System.Drawing.Point(528, 34);
            this.lblMaxChar.Name = "lblMaxChar";
            this.lblMaxChar.Size = new System.Drawing.Size(68, 16);
            this.lblMaxChar.TabIndex = 17;
            this.lblMaxChar.Text = "Max(1000)";
            this.lblMaxChar.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 466);
            this.Controls.Add(this.lblMaxChar);
            this.Controls.Add(this.lblCharNumber);
            this.Controls.Add(this.lblNumberOfCharacters);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblTypeOfQuestion);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.lblQuestionOrder);
            this.Controls.Add(this.numericUpDownQuestionOrder);
            this.Controls.Add(this.lblQuestionText);
            this.Controls.Add(this.textBoxQuestionText);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(4);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxQuestionText;
        private System.Windows.Forms.Label lblQuestionText;
        private System.Windows.Forms.NumericUpDown numericUpDownQuestionOrder;
        private System.Windows.Forms.Label lblQuestionOrder;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackBarStars;
        private System.Windows.Forms.Label label4;
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
        private System.Windows.Forms.Label lblNumberOfCharacters;
        private System.Windows.Forms.Label lblMaxChar;
        private System.Windows.Forms.Label lblCharNumberStartCaption;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMaxStart;
        private System.Windows.Forms.Label lblCharNumberEndCaption;
    }
}