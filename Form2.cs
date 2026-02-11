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
        private QuestionRepository mQuestionRepository;
        private Question mEditingQuestion;

        public Form2(Question pQuestion)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterParent;

            mQuestionRepository = new QuestionRepository();
            comboBox1.DataSource = Enum.GetValues(typeof(QuestionType));

            if (pQuestion != null)
            {
                mEditingQuestion = pQuestion;
                comboBox1.SelectedItem = pQuestion.QuestionType;
                Updatemode(pQuestion);
                btnUpdate.Visible = true;
                button1.Visible = false;
            }
            else
            {
                btnUpdate.Visible = false;
                button1.Visible = true;
                comboBox1.SelectedIndex = 0;
                UpdateStars(trackBar1.Value);
            }
        }

        private void Updatemode(Question pQuestion)
        {
            comboBox1.Enabled = false;

            textBoxQuestionText.Text = pQuestion.QuestionText;
            numericUpDownQuestionOrder.Value = pQuestion.QuestionOrder;

            if (pQuestion is StarQuestion tStarQuestion)
            {
                trackBar1.Value = tStarQuestion.NumberOfStars;
                lblNumberOfStars.Text = tStarQuestion.NumberOfStars.ToString();
                UpdateStars(tStarQuestion.NumberOfStars);
            }
            if (pQuestion is SmileyFacesQuestion tSmileyFacesQuestion)
            {
                trackBar2.Value = tSmileyFacesQuestion.NumberOfSmileyFaces;
                lblFacesNumber.Text = tSmileyFacesQuestion.NumberOfSmileyFaces.ToString();
                UpdateSmileyFace(tSmileyFacesQuestion.NumberOfSmileyFaces);
            }
            if (pQuestion is SliderQuestion tSliderQuestion)
            {
                numericUpDownStartValue.Value = tSliderQuestion.StartValue;
                numericUpDownEndValue.Value = tSliderQuestion.EndValue;
                textBoxStartCaption.Text = tSliderQuestion.StartValueCaption;
                textBoxEndCaption.Text = tSliderQuestion.EndValueCaption;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxQuestionText.Text == String.Empty)
            {
                errorProvider1.SetError(textBoxQuestionText, "Question text cant be empty");
                return;
            }

            QuestionType tSelectedType = (QuestionType)comboBox1.SelectedItem;

            Question tQuestion = null;

            try
            {
                if (tSelectedType == QuestionType.Star)
                {
                    if (trackBar1.Value < 1)
                    {
                        errorProvider1.SetError(trackBar1, "Number of stars cannot be less than 1");
                        return;
                    }

                    tQuestion = new StarQuestion
                    {
                        QuestionText = textBoxQuestionText.Text,
                        QuestionOrder = (int)numericUpDownQuestionOrder.Value,
                        NumberOfStars = trackBar1.Value
                    };
                }
                else if (tSelectedType == QuestionType.Smiley)
                {
                    tQuestion = new SmileyFacesQuestion
                    {
                        QuestionText = textBoxQuestionText.Text,
                        QuestionOrder = (int)numericUpDownQuestionOrder.Value,
                        NumberOfSmileyFaces = trackBar2.Value
                    };
                }
                else if (tSelectedType == QuestionType.Slider)
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

                    tQuestion = new SliderQuestion
                    {
                        QuestionText = textBoxQuestionText.Text,
                        QuestionOrder = (int)numericUpDownQuestionOrder.Value,
                        StartValue = (int)numericUpDownStartValue.Value,
                        EndValue = (int)numericUpDownEndValue.Value,
                        StartValueCaption = textBoxStartCaption.Text,
                        EndValueCaption = textBoxEndCaption.Text
                    };
                }

                mQuestionRepository.AddQuestion(tQuestion);

                MessageBox.Show(
                    $"{tQuestion.QuestionType} question added successfully.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                Close();
            }
            catch
            {
                MessageBox.Show(
                    "error happend while trying to save question please try again",
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

        private void UpdateStars(int pValue)
        {
            label4.Text =
                new string('★', pValue) +
                new string('☆', 10 - pValue);
        }

        private void UpdateSmileyFace(int pValue)
        {
            string str = "";

            for (int i = 0; i < pValue; i++)
            {
                str += ":) ";
            }

            lblSmileyFaces.Text = str;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            UpdateStars(trackBar1.Value);
            lblNumberOfStars.Text = trackBar1.Value.ToString();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
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

        private void label6_Click_1(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBoxQuestionText.Text == String.Empty)
            {
                errorProvider1.SetError(textBoxQuestionText, "Question text cant be empty");
                return;
            }

            if (mEditingQuestion is StarQuestion tStarQuestion)
            {
                tStarQuestion.QuestionText = textBoxQuestionText.Text;
                tStarQuestion.QuestionOrder = (int)numericUpDownQuestionOrder.Value;
                tStarQuestion.NumberOfStars = trackBar1.Value;

                mQuestionRepository.UpdateQuestion(tStarQuestion);
            }
            else if (mEditingQuestion is SmileyFacesQuestion tSmileyQuestion)
            {
                tSmileyQuestion.QuestionText = textBoxQuestionText.Text;
                tSmileyQuestion.QuestionOrder = (int)numericUpDownQuestionOrder.Value;
                tSmileyQuestion.NumberOfSmileyFaces = trackBar2.Value;

                mQuestionRepository.UpdateQuestion(tSmileyQuestion);
            }
            else if (mEditingQuestion is SliderQuestion tSliderQuestion)
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

                tSliderQuestion.QuestionText = textBoxQuestionText.Text;
                tSliderQuestion.QuestionOrder = (int)numericUpDownQuestionOrder.Value;
                tSliderQuestion.StartValue = (int)numericUpDownStartValue.Value;
                tSliderQuestion.EndValue = (int)numericUpDownEndValue.Value;
                tSliderQuestion.StartValueCaption = textBoxStartCaption.Text;
                tSliderQuestion.EndValueCaption = textBoxEndCaption.Text;

                mQuestionRepository.UpdateQuestion(tSliderQuestion);
            }

            MessageBox.Show($"{mEditingQuestion.QuestionType} question updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}