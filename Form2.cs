using SurveyQuestionsConfigurator.Models;
using SurveyQuestionsConfigurator.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SurveyQuestionsConfigurator
{
    public partial class Form2 : Form
    {
        private QuestionRepository questionRepository;

        public Form2()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;

            comboBox1.DataSource = Enum.GetValues(typeof(QuestionType));
            questionRepository = new QuestionRepository();

            comboBox1.SelectedIndex = 0;
            comboBox1_SelectedIndexChanged(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxQuestionText.Text == String.Empty)
            {
                errorProvider1.SetError(textBoxQuestionText, "Question text cant be empty");
                return;
            }

            QuestionType selectedType =
                (QuestionType)comboBox1.SelectedItem;
            Question question = null;

            try
            {
                if (selectedType == QuestionType.Star)
                {
                    question = new StarQuestion
                    {
                        QuestionText = textBoxQuestionText.Text,
                        QuestionOrder = (int)numericUpDownQuestionOrder.Value,
                        NumberOfStars = trackBar1.Value
                    };
                }
                else if (selectedType == QuestionType.Smiley)
                {
                    question = new SmileyFacesQuestion
                    {
                        QuestionText = textBoxQuestionText.Text,
                        QuestionOrder = (int)numericUpDownQuestionOrder.Value,
                        NumberOfSmileyFaces = trackBar2.Value
                    };
                }
                else if (selectedType == QuestionType.Slider)
                {
                    if (numericUpDownStartValue.Value >= numericUpDownEndValue.Value)
                    {
                        errorProvider1.SetError(numericUpDownStartValue, " slider start value cant be more than slider end value");
                        return;
                    }
                    if (numericUpDownEndValue.Value <= numericUpDownStartValue.Value)
                    {
                        errorProvider1.SetError(numericUpDownStartValue, " slider end value cant be less than slider start value");
                        return;
                    }
                    if (textBoxStartCaption.Text == String.Empty)
                    {
                        errorProvider1.SetError(textBoxStartCaption, "Start caption cant be empty");
                        return;
                    }
                    if (textBoxEndCaption.Text == String.Empty)
                    {
                        errorProvider1.SetError(textBoxEndCaption, "End caption cant be empty");
                        return;
                    }

                    question = new SliderQuestion
                    {
                        QuestionText = textBoxQuestionText.Text,
                        QuestionOrder = (int)numericUpDownQuestionOrder.Value,
                        StartValue = (int)numericUpDownStartValue.Value,
                        EndValue = (int)numericUpDownEndValue.Value,
                        StartValueCaption = textBoxStartCaption.Text,
                        EndValueCaption = textBoxEndCaption.Text
                    };
                }

                questionRepository.AddQuestion(question);

                MessageBox.Show(
                    $"{question.QuestionType} question added successfully.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "error happend while trying to save question",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void UpdateStars(int value)
        {
            label4.Text =
                new string('★', value) +
                new string('☆', 10 - value);
        }

        private void UpdateSmileyFace(int value)
        {
            string str = "";

            for (int i = 0; i < value; i++)
            {
                str += ":) ";
            }

            lblSmileyFaces.Text = str;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            UpdateStars(trackBar1.Value);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (panel1.Visible == true)
                UpdateStars(0);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;

            QuestionType selected = (QuestionType)comboBox1.SelectedItem;

            if (selected == QuestionType.Star)
            {
                panel1.Visible = true;
                panel1.BringToFront();
            }
            else if (selected == QuestionType.Smiley)
            {
                panel2.Visible = true;
                panel2.BringToFront();
            }
            else if (selected == QuestionType.Slider)
            {
                panel3.Visible = true;
                panel3.BringToFront();
            }
        }

        private void lblSmileyFaces_Click(object sender, EventArgs e)
        {
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            UpdateSmileyFace(trackBar2.Value);
            lblFacesNumber.Text = trackBar2.Value.ToString();
        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void label6_Click(object sender, EventArgs e)
        {
        }

        private void label7_Click(object sender, EventArgs e)
        {
        }

        private void textBoxEndCaption_TextChanged(object sender, EventArgs e)
        {
        }

        private void lblFacesNumber_Click(object sender, EventArgs e)
        {

        }
    }
}