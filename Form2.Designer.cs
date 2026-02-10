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
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownQuestionOrder = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.lblSmileyFaces = new System.Windows.Forms.Label();
            this.lblNumberofSmileyfaces = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblStartValue = new System.Windows.Forms.Label();
            this.numericUpDownStartValue = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownEndValue = new System.Windows.Forms.NumericUpDown();
            this.lblEndValue = new System.Windows.Forms.Label();
            this.lblStartCaption = new System.Windows.Forms.Label();
            this.textBoxStartCaption = new System.Windows.Forms.TextBox();
            this.textBoxEndCaption = new System.Windows.Forms.TextBox();
            this.lblEndCaption = new System.Windows.Forms.Label();
            this.lblFacesNumber = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuestionOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEndValue)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(247, 315);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxQuestionText
            // 
            this.textBoxQuestionText.Location = new System.Drawing.Point(243, 59);
            this.textBoxQuestionText.Name = "textBoxQuestionText";
            this.textBoxQuestionText.Size = new System.Drawing.Size(164, 20);
            this.textBoxQuestionText.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(164, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Question Text";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // numericUpDownQuestionOrder
            // 
            this.numericUpDownQuestionOrder.Location = new System.Drawing.Point(243, 99);
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
            this.numericUpDownQuestionOrder.Size = new System.Drawing.Size(164, 20);
            this.numericUpDownQuestionOrder.TabIndex = 3;
            this.numericUpDownQuestionOrder.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(159, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Question Order";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(243, 142);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(164, 21);
            this.comboBox1.TabIndex = 5;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Number of stars";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar1.Location = new System.Drawing.Point(100, 52);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(121, 45);
            this.trackBar1.TabIndex = 7;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(92, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 25);
            this.label4.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(151, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Type of question";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.trackBar1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(142, 174);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(281, 100);
            this.panel1.TabIndex = 10;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblFacesNumber);
            this.panel2.Controls.Add(this.trackBar2);
            this.panel2.Controls.Add(this.lblSmileyFaces);
            this.panel2.Controls.Add(this.lblNumberofSmileyfaces);
            this.panel2.Location = new System.Drawing.Point(139, 177);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(281, 100);
            this.panel2.TabIndex = 11;
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(100, 52);
            this.trackBar2.Maximum = 5;
            this.trackBar2.Minimum = 2;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(104, 45);
            this.trackBar2.TabIndex = 2;
            this.trackBar2.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBar2.Value = 2;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // lblSmileyFaces
            // 
            this.lblSmileyFaces.AutoSize = true;
            this.lblSmileyFaces.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSmileyFaces.Location = new System.Drawing.Point(131, 22);
            this.lblSmileyFaces.Name = "lblSmileyFaces";
            this.lblSmileyFaces.Size = new System.Drawing.Size(27, 20);
            this.lblSmileyFaces.TabIndex = 1;
            this.lblSmileyFaces.Text = ":):)";
            this.lblSmileyFaces.Click += new System.EventHandler(this.lblSmileyFaces_Click);
            // 
            // lblNumberofSmileyfaces
            // 
            this.lblNumberofSmileyfaces.AutoSize = true;
            this.lblNumberofSmileyfaces.Location = new System.Drawing.Point(3, 24);
            this.lblNumberofSmileyfaces.Name = "lblNumberofSmileyfaces";
            this.lblNumberofSmileyfaces.Size = new System.Drawing.Size(118, 13);
            this.lblNumberofSmileyfaces.TabIndex = 0;
            this.lblNumberofSmileyfaces.Text = "Number of Smiley faces";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblEndCaption);
            this.panel3.Controls.Add(this.textBoxEndCaption);
            this.panel3.Controls.Add(this.textBoxStartCaption);
            this.panel3.Controls.Add(this.lblStartCaption);
            this.panel3.Controls.Add(this.lblEndValue);
            this.panel3.Controls.Add(this.numericUpDownEndValue);
            this.panel3.Controls.Add(this.numericUpDownStartValue);
            this.panel3.Controls.Add(this.lblStartValue);
            this.panel3.Location = new System.Drawing.Point(155, 169);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(284, 145);
            this.panel3.TabIndex = 12;
            // 
            // lblStartValue
            // 
            this.lblStartValue.AutoSize = true;
            this.lblStartValue.Location = new System.Drawing.Point(10, 12);
            this.lblStartValue.Name = "lblStartValue";
            this.lblStartValue.Size = new System.Drawing.Size(58, 13);
            this.lblStartValue.TabIndex = 13;
            this.lblStartValue.Text = "Start value";
            this.lblStartValue.Click += new System.EventHandler(this.label6_Click);
            // 
            // numericUpDownStartValue
            // 
            this.numericUpDownStartValue.Location = new System.Drawing.Point(89, 10);
            this.numericUpDownStartValue.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownStartValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownStartValue.Name = "numericUpDownStartValue";
            this.numericUpDownStartValue.Size = new System.Drawing.Size(164, 20);
            this.numericUpDownStartValue.TabIndex = 14;
            this.numericUpDownStartValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownStartValue.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // numericUpDownEndValue
            // 
            this.numericUpDownEndValue.Location = new System.Drawing.Point(88, 44);
            this.numericUpDownEndValue.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownEndValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownEndValue.Name = "numericUpDownEndValue";
            this.numericUpDownEndValue.Size = new System.Drawing.Size(164, 20);
            this.numericUpDownEndValue.TabIndex = 15;
            this.numericUpDownEndValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblEndValue
            // 
            this.lblEndValue.AutoSize = true;
            this.lblEndValue.Location = new System.Drawing.Point(10, 46);
            this.lblEndValue.Name = "lblEndValue";
            this.lblEndValue.Size = new System.Drawing.Size(55, 13);
            this.lblEndValue.TabIndex = 16;
            this.lblEndValue.Text = "End value";
            this.lblEndValue.Click += new System.EventHandler(this.label7_Click);
            // 
            // lblStartCaption
            // 
            this.lblStartCaption.AutoSize = true;
            this.lblStartCaption.Location = new System.Drawing.Point(10, 75);
            this.lblStartCaption.Name = "lblStartCaption";
            this.lblStartCaption.Size = new System.Drawing.Size(67, 13);
            this.lblStartCaption.TabIndex = 13;
            this.lblStartCaption.Text = "Start caption";
            // 
            // textBoxStartCaption
            // 
            this.textBoxStartCaption.Location = new System.Drawing.Point(89, 75);
            this.textBoxStartCaption.Name = "textBoxStartCaption";
            this.textBoxStartCaption.Size = new System.Drawing.Size(164, 20);
            this.textBoxStartCaption.TabIndex = 13;
            // 
            // textBoxEndCaption
            // 
            this.textBoxEndCaption.Location = new System.Drawing.Point(89, 104);
            this.textBoxEndCaption.Name = "textBoxEndCaption";
            this.textBoxEndCaption.Size = new System.Drawing.Size(164, 20);
            this.textBoxEndCaption.TabIndex = 17;
            this.textBoxEndCaption.TextChanged += new System.EventHandler(this.textBoxEndCaption_TextChanged);
            // 
            // lblEndCaption
            // 
            this.lblEndCaption.AutoSize = true;
            this.lblEndCaption.Location = new System.Drawing.Point(13, 107);
            this.lblEndCaption.Name = "lblEndCaption";
            this.lblEndCaption.Size = new System.Drawing.Size(64, 13);
            this.lblEndCaption.TabIndex = 18;
            this.lblEndCaption.Text = "End caption";
            // 
            // lblFacesNumber
            // 
            this.lblFacesNumber.AutoSize = true;
            this.lblFacesNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFacesNumber.Location = new System.Drawing.Point(220, 25);
            this.lblFacesNumber.Name = "lblFacesNumber";
            this.lblFacesNumber.Size = new System.Drawing.Size(14, 16);
            this.lblFacesNumber.TabIndex = 3;
            this.lblFacesNumber.Text = "2";
            this.lblFacesNumber.Click += new System.EventHandler(this.lblFacesNumber_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 539);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDownQuestionOrder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxQuestionText);
            this.Controls.Add(this.button1);
            this.Name = "Form2";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuestionOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEndValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxQuestionText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownQuestionOrder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblNumberofSmileyfaces;
        private System.Windows.Forms.Label lblSmileyFaces;
        private System.Windows.Forms.TrackBar trackBar2;
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
    }
}