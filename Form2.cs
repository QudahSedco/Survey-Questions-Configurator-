using Serilog;
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
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            mQuestionRepository = new QuestionRepository();
            comboBox1.DataSource = Enum.GetValues(typeof(QuestionType));

            if (pQuestion != null) //means form is in edit mode
            {
                mEditingQuestion = pQuestion;
                comboBox1.SelectedItem = pQuestion.QuestionType;
                PopulateFields(pQuestion);
                btnUpdate.Visible = true;
                button1.Visible = false;
            }
            else
            {
                btnUpdate.Visible = false;
                button1.Visible = true;
                comboBox1.SelectedIndex = 0;
                UpdateStars(trackBarStars.Value);
            }
        }

        //fills the form with values from the question
        private void PopulateFields(Question pQuestion)
        {
            textBoxQuestionText.Text = pQuestion.QuestionText;
            numericUpDownQuestionOrder.Value = pQuestion.QuestionOrder;

            if (pQuestion is StarQuestion tStarQuestion)
            {
                trackBarStars.Value = tStarQuestion.NumberOfStars;
                lblNumberOfStars.Text = tStarQuestion.NumberOfStars.ToString();
                UpdateStars(tStarQuestion.NumberOfStars);
            }
            if (pQuestion is SmileyFacesQuestion tSmileyFacesQuestion)
            {
                trackBarSmileyFaces.Value = tSmileyFacesQuestion.NumberOfSmileyFaces;
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

        //create button creates a new question
        private void button1_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();

            if (String.IsNullOrWhiteSpace(textBoxQuestionText.Text))
            {
                errorProvider1.SetError(textBoxQuestionText, "Question text cant be empty");
                return;
            }
            if (textBoxQuestionText.Text.Length > 1000)
            {
                errorProvider1.SetError(textBoxQuestionText, "Question text cant be more than 1000 characters");
                return;
            }

            QuestionType tSelectedType = (QuestionType)comboBox1.SelectedItem;

            Question tQuestion = null;

            try
            {
                if (tSelectedType == QuestionType.Star)
                {
                    if (trackBarStars.Value < 1)
                    {
                        errorProvider1.SetError(trackBarStars, "Number of stars cannot be less than 1");
                        return;
                    }

                    tQuestion = new StarQuestion
                    {
                        QuestionText = textBoxQuestionText.Text,
                        QuestionOrder = (int)numericUpDownQuestionOrder.Value,
                        NumberOfStars = trackBarStars.Value
                    };
                }
                else if (tSelectedType == QuestionType.Smiley)
                {
                    tQuestion = new SmileyFacesQuestion
                    {
                        QuestionText = textBoxQuestionText.Text,
                        QuestionOrder = (int)numericUpDownQuestionOrder.Value,
                        NumberOfSmileyFaces = trackBarSmileyFaces.Value
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
                    if (String.IsNullOrWhiteSpace(textBoxStartCaption.Text))
                    {
                        errorProvider1.SetError(textBoxStartCaption, "Start caption cant be empty");
                        return;
                    }
                    if (String.IsNullOrWhiteSpace(textBoxEndCaption.Text))
                    {
                        errorProvider1.SetError(textBoxEndCaption, "End caption cant be empty");
                        return;
                    }

                    tQuestion = new SliderQuestion
                    {
                        QuestionText = textBoxQuestionText.Text,
                        QuestionOrder = (int)numericUpDownQuestionOrder.Value,
                        StartValueCaption = textBoxStartCaption.Text,
                        EndValueCaption = textBoxEndCaption.Text
                    };
                    ((SliderQuestion)tQuestion).SetRange((int)numericUpDownStartValue.Value, (int)numericUpDownEndValue.Value);
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

        //prints the stars according to the trackbar
        private void UpdateStars(int pValue)
        {
            label4.Text =
                new string('★', pValue) +
                new string('☆', 10 - pValue);
        }

        //prints smiley faces according to the trackbar
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
            UpdateStars(trackBarStars.Value);
            lblNumberOfStars.Text = trackBarStars.Value.ToString();
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
            UpdateSmileyFace(trackBarSmileyFaces.Value);
            lblFacesNumber.Text = trackBarSmileyFaces.Value.ToString();
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

        //shows the number of chars the user enterd for end caption text box
        private void textBoxEndCaption_TextChanged(object sender, EventArgs e)
        {
            lblCharNumberEndCaption.Text = textBoxEndCaption.Text.Length.ToString();
        }

        private void lblFacesNumber_Click(object sender, EventArgs e)
        {
        }

        private void label6_Click_1(object sender, EventArgs e)
        {
        }

        //update button handles update logic
        //if type didnt change calls update method
        //if type did change calls UpdateChildTableType
        //that deletes the old type record and inserts a new record according to the new type chosen
        private void button2_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (String.IsNullOrWhiteSpace(textBoxQuestionText.Text))
            {
                errorProvider1.SetError(textBoxQuestionText, "Question text cant be empty");
                return;
            }
            if (textBoxQuestionText.Text.Length > 1000)
            {
                errorProvider1.SetError(textBoxQuestionText, "Question text cant be more than 1000 characters");
                return;
            }
            mEditingQuestion.QuestionText = textBoxQuestionText.Text;
            mEditingQuestion.QuestionOrder = (int)numericUpDownQuestionOrder.Value;

            var tOldType = mEditingQuestion.QuestionType;
            var tNewType = (QuestionType)comboBox1.SelectedItem;
            try
            {
                if (tOldType == tNewType)//if type didnt change
                {
                    if (mEditingQuestion is StarQuestion tStarQuestion)
                    {
                        if (trackBarStars.Value < 1)
                        {
                            errorProvider1.SetError(trackBarStars, "Number of stars cannot be less than 1");
                            return;
                        }
                        tStarQuestion.NumberOfStars = trackBarStars.Value;
                        mQuestionRepository.UpdateQuestion(tStarQuestion);
                    }
                    else if (mEditingQuestion is SmileyFacesQuestion tSmileyQuestion)
                    {
                        tSmileyQuestion.NumberOfSmileyFaces = trackBarSmileyFaces.Value;
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
                        if (String.IsNullOrWhiteSpace(textBoxStartCaption.Text))
                        {
                            errorProvider1.SetError(textBoxStartCaption, "Start caption cant be empty");
                            return;
                        }
                        if (textBoxStartCaption.Text.Length > 100)
                        {
                            errorProvider1.SetError(textBoxStartCaption, "start caption cant be more than 100 characters");
                            return;
                        }
                        if (String.IsNullOrWhiteSpace(textBoxEndCaption.Text))
                        {
                            errorProvider1.SetError(textBoxEndCaption, "End caption cant be empty");
                            return;
                        }
                        if (textBoxEndCaption.Text.Length > 100)
                        {
                            errorProvider1.SetError(textBoxEndCaption, "end caption cant be more than 100 characters");
                            return;
                        }

                        tSliderQuestion.SetRange((int)numericUpDownStartValue.Value, (int)numericUpDownEndValue.Value);
                        tSliderQuestion.StartValueCaption = textBoxStartCaption.Text;
                        tSliderQuestion.EndValueCaption = textBoxEndCaption.Text;
                        mQuestionRepository.UpdateQuestion(tSliderQuestion);
                    }
                }
                //if type changed call UpdateChildTableType that takes the new question and the old type
                //deletes the old type record from database and inserts the new type in the correct table
                else
                {
                    if (tNewType == QuestionType.Star)
                    {
                        var tNewQuestion = new StarQuestion
                        {
                            Id = mEditingQuestion.Id,
                            QuestionOrder = mEditingQuestion.QuestionOrder,
                            QuestionText = mEditingQuestion.QuestionText,
                        };
                        if (trackBarStars.Value < 1)
                        {
                            errorProvider1.SetError(trackBarStars, "Number of stars cannot be less than 1");
                            return;
                        }
                        tNewQuestion.NumberOfStars = trackBarStars.Value;
                        mQuestionRepository.UpdateChildTableType(tNewQuestion, tOldType);
                        mEditingQuestion = tNewQuestion;
                    }
                    else if (tNewType == QuestionType.Smiley)
                    {
                        var tNewQuestion = new SmileyFacesQuestion
                        {
                            Id = mEditingQuestion.Id,
                            QuestionOrder = mEditingQuestion.QuestionOrder,
                            QuestionText = mEditingQuestion.QuestionText,
                        };
                        if (trackBarSmileyFaces.Value < 2 || trackBarSmileyFaces.Value > 5)
                        {
                            errorProvider1.SetError(trackBarSmileyFaces, "Number of stars cannot be less than 2 or more than 5");
                            return;
                        }
                        tNewQuestion.NumberOfSmileyFaces = trackBarSmileyFaces.Value;
                        mQuestionRepository.UpdateChildTableType(tNewQuestion, tOldType);
                        mEditingQuestion = tNewQuestion;
                    }
                    else if (tNewType == QuestionType.Slider)
                    {
                        if (numericUpDownStartValue.Value >= numericUpDownEndValue.Value)
                        {
                            errorProvider1.SetError(numericUpDownStartValue, "Slider start value must be less than slider end value");
                            return;
                        }

                        if (string.IsNullOrWhiteSpace(textBoxStartCaption.Text))
                        {
                            errorProvider1.SetError(textBoxStartCaption, "Start caption can't be empty");
                            return;
                        }

                        if (string.IsNullOrWhiteSpace(textBoxEndCaption.Text))
                        {
                            errorProvider1.SetError(textBoxEndCaption, "End caption can't be empty");
                            return;
                        }

                        var tNewQuestion = new SliderQuestion
                        {
                            Id = mEditingQuestion.Id,
                            QuestionOrder = mEditingQuestion.QuestionOrder,
                            QuestionText = mEditingQuestion.QuestionText,
                            StartValueCaption = textBoxStartCaption.Text,
                            EndValueCaption = textBoxEndCaption.Text
                        };

                        tNewQuestion.SetRange(
                            (int)numericUpDownStartValue.Value,
                            (int)numericUpDownEndValue.Value
                        );

                        mQuestionRepository.UpdateChildTableType(tNewQuestion, tOldType);
                        mEditingQuestion = tNewQuestion;
                    }
                }

                MessageBox.Show(this, $"{mEditingQuestion.QuestionType} Question updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to update question");

                MessageBox.Show(
                    this,
                    "Could not update the question. Please try again.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //shows the number of chars entered by the user into the question text textbox
        private void textBoxQuestionText_TextChanged(object sender, EventArgs e)
        {
            lblCharNumber.Text = textBoxQuestionText.Text.Length.ToString();
        }

        private void lblNumberOfCharacters_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
        }

        //shows the number of chars enterd by the user into the start caption text box
        private void textBoxStartCaption_TextChanged(object sender, EventArgs e)
        {
            lblCharNumberStartCaption.Text = textBoxStartCaption.Text.Length.ToString();
        }
    }
}