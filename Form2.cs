using Serilog;
using SurveyQuestionsConfigurator.Models;
using SurveyQuestionsConfiguratorServices;
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
        private QuestionService mQuestionService;
        private Question mEditingQuestion;

        public Form2(Question pQuestion)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            mQuestionService = new QuestionService();
            comboBoxQuestionTypes.DataSource = Enum.GetValues(typeof(QuestionType));

            if (pQuestion != null) //means form is in edit mode
            {
                mEditingQuestion = pQuestion;
                comboBoxQuestionTypes.SelectedItem = pQuestion.QuestionType;
                PopulateFields(pQuestion);
                btnUpdate.Visible = true;
                button1.Visible = false;
                Text = "Edit";
            }
            else
            {
                btnUpdate.Visible = false;
                button1.Visible = true;
                comboBoxQuestionTypes.SelectedIndex = 0;
                UpdateStars(trackBarStars.Value);
                Text = "Create";
            }
        }

        //fills the form with values from the question
        private void PopulateFields(Question pQuestion)
        {
            textBoxQuestionText.Text = pQuestion.QuestionText;
            numericUpDownQuestionOrder.Value = pQuestion.QuestionOrder;

            switch (pQuestion)
            {
                case StarQuestion tStarQuestion:
                    trackBarStars.Value = tStarQuestion.NumberOfStars;
                    lblNumberOfStars.Text = tStarQuestion.NumberOfStars.ToString();
                    UpdateStars(tStarQuestion.NumberOfStars);
                    break;

                case SmileyFacesQuestion tSmileyFacesQuestion:
                    trackBarSmileyFaces.Value = tSmileyFacesQuestion.NumberOfSmileyFaces;
                    lblFacesNumber.Text = tSmileyFacesQuestion.NumberOfSmileyFaces.ToString();
                    UpdateSmileyFace(tSmileyFacesQuestion.NumberOfSmileyFaces);
                    break;

                case SliderQuestion tSliderQuestion:
                    numericUpDownStartValue.Value = tSliderQuestion.StartValue;
                    numericUpDownEndValue.Value = tSliderQuestion.EndValue;
                    textBoxStartCaption.Text = tSliderQuestion.StartValueCaption;
                    textBoxEndCaption.Text = tSliderQuestion.EndValueCaption;
                    break;
            }
        }

        //create button creates a new question
        private void buttonSave_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();

            if (String.IsNullOrWhiteSpace(textBoxQuestionText.Text))
            {
                errorProvider1.SetError(textBoxQuestionText, "Question text can't be empty");
                return;
            }
            if (textBoxQuestionText.Text.Length > 1000)
            {
                errorProvider1.SetError(textBoxQuestionText, "Question text can't be more than 1000 characters");
                return;
            }

            QuestionType tSelectedType = (QuestionType)comboBoxQuestionTypes.SelectedItem;

            Question tQuestion = null;

            try
            {
                switch (tSelectedType)
                {
                    case QuestionType.Star:
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
                        break;

                    case QuestionType.Smiley:

                        tQuestion = new SmileyFacesQuestion
                        {
                            QuestionText = textBoxQuestionText.Text,
                            QuestionOrder = (int)numericUpDownQuestionOrder.Value,
                            NumberOfSmileyFaces = trackBarSmileyFaces.Value
                        };
                        break;

                    case QuestionType.Slider:
                        if (numericUpDownStartValue.Value >= numericUpDownEndValue.Value)
                        {
                            errorProvider1.SetError(numericUpDownStartValue, "Slider start value can't be more or equal to slider end value");
                            return;
                        }
                        if (numericUpDownEndValue.Value <= numericUpDownStartValue.Value)
                        {
                            errorProvider1.SetError(numericUpDownStartValue, "Slider end value can't be less than slider start value");
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

                        tQuestion = new SliderQuestion
                        {
                            QuestionText = textBoxQuestionText.Text,
                            QuestionOrder = (int)numericUpDownQuestionOrder.Value,
                            StartValueCaption = textBoxStartCaption.Text,
                            EndValueCaption = textBoxEndCaption.Text
                        };
                        ((SliderQuestion)tQuestion).SetRange(
                            (int)numericUpDownStartValue.Value,
                            (int)numericUpDownEndValue.Value
                        );
                        break;
                }

                mQuestionService.AddQuestion(tQuestion);

                Close();
            }
            catch
            {
                MessageBox.Show(
                    "Error happened while trying to save question please try again",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        //prints the stars according to the trackbar
        private void UpdateStars(int pValue)
        {
            lblStars.Text =
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

        private void trackBarStars_Scroll(object sender, EventArgs e)
        {
            UpdateStars(trackBarStars.Value);
            lblNumberOfStars.Text = trackBarStars.Value.ToString();
        }

        private void comboBoxQuestionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlStars.Visible = false;
            pnlSmileyFaces.Visible = false;
            pnlSlider.Visible = false;

            QuestionType selected = (QuestionType)comboBoxQuestionTypes.SelectedItem;

            switch (selected)
            {
                case QuestionType.Star:
                    pnlStars.Visible = true;
                    pnlStars.BringToFront();
                    break;

                case QuestionType.Smiley:
                    pnlSmileyFaces.Visible = true;
                    pnlSmileyFaces.BringToFront();
                    break;

                case QuestionType.Slider:
                    pnlSlider.Visible = true;
                    pnlSlider.BringToFront();
                    break;
            }
        }

        private void trackBarSmiley_Scroll(object sender, EventArgs e)
        {
            UpdateSmileyFace(trackBarSmileyFaces.Value);
            lblFacesNumber.Text = trackBarSmileyFaces.Value.ToString();
        }

        //shows the number of chars the user entered for end caption text box
        private void textBoxEndCaption_TextChanged(object sender, EventArgs e)
        {
            lblCharNumberEndCaption.Text = ($"{textBoxEndCaption.Text.Length.ToString()}/100");
        }

        //update button handles update logic
        //if type didn't change calls update method
        //if type did change calls UpdateChildTableType
        //that deletes the old type record and inserts a new record according to the new type chosen
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (String.IsNullOrWhiteSpace(textBoxQuestionText.Text))
            {
                errorProvider1.SetError(textBoxQuestionText, "Question text can't be empty");
                return;
            }
            if (textBoxQuestionText.Text.Length > 1000)
            {
                errorProvider1.SetError(textBoxQuestionText, "Question text can't be more than 1000 characters");
                return;
            }
            mEditingQuestion.QuestionText = textBoxQuestionText.Text;
            mEditingQuestion.QuestionOrder = (int)numericUpDownQuestionOrder.Value;

            var tOldType = mEditingQuestion.QuestionType;
            var tNewType = (QuestionType)comboBoxQuestionTypes.SelectedItem;
            try
            {
                if (tOldType == tNewType)//if type didnt change
                {
                    switch (mEditingQuestion)
                    {
                        case StarQuestion tStarQuestion:
                            if (trackBarStars.Value < 1)
                            {
                                errorProvider1.SetError(trackBarStars, "Number of stars cannot be less than 1");
                                return;
                            }
                            tStarQuestion.NumberOfStars = trackBarStars.Value;
                            mQuestionService.UpdateQuestion(tStarQuestion);
                            break;

                        case SmileyFacesQuestion tSmileyQuestion:
                            if (trackBarSmileyFaces.Value < 2 || trackBarSmileyFaces.Value > 5)
                            {
                                errorProvider1.SetError(trackBarSmileyFaces, "Number of smiley faces cannot be less than 2 or more than 5");
                                return;
                            }
                            tSmileyQuestion.NumberOfSmileyFaces = trackBarSmileyFaces.Value;
                            mQuestionService.UpdateQuestion(tSmileyQuestion);
                            break;

                        case SliderQuestion tSliderQuestion:
                            if (numericUpDownStartValue.Value >= numericUpDownEndValue.Value)
                            {
                                errorProvider1.SetError(numericUpDownStartValue, " slider start value can't be more or equal to slider end value");
                                return;
                            }
                            if (numericUpDownEndValue.Value <= numericUpDownStartValue.Value)
                            {
                                errorProvider1.SetError(numericUpDownStartValue, " slider end value can't be less than slider start value");
                                return;
                            }
                            if (String.IsNullOrWhiteSpace(textBoxStartCaption.Text))
                            {
                                errorProvider1.SetError(textBoxStartCaption, "Start caption can't be empty");
                                return;
                            }
                            if (String.IsNullOrWhiteSpace(textBoxEndCaption.Text))
                            {
                                errorProvider1.SetError(textBoxEndCaption, "End caption can't be empty");
                                return;
                            }

                            tSliderQuestion.SetRange((int)numericUpDownStartValue.Value, (int)numericUpDownEndValue.Value);
                            tSliderQuestion.StartValueCaption = textBoxStartCaption.Text;
                            tSliderQuestion.EndValueCaption = textBoxEndCaption.Text;
                            mQuestionService.UpdateQuestion(tSliderQuestion);
                            break;
                    }
                }
                //if type changed call UpdateChildTableType that takes the new question and the old type
                //deletes the old type record from database and inserts the new type in the correct table
                else
                {
                    Question tNewQuestion = null;

                    switch (tNewType)
                    {
                        case QuestionType.Star:
                            if (trackBarStars.Value < 1)
                            {
                                errorProvider1.SetError(trackBarStars, "Number of stars cannot be less than 1");
                                return;
                            }
                            tNewQuestion = new StarQuestion
                            {
                                Id = mEditingQuestion.Id,
                                QuestionOrder = mEditingQuestion.QuestionOrder,
                                QuestionText = mEditingQuestion.QuestionText,
                                NumberOfStars = trackBarStars.Value
                            };

                            break;

                        case QuestionType.Smiley:
                            tNewQuestion = new SmileyFacesQuestion
                            {
                                Id = mEditingQuestion.Id,
                                QuestionOrder = mEditingQuestion.QuestionOrder,
                                QuestionText = mEditingQuestion.QuestionText,
                                NumberOfSmileyFaces = trackBarSmileyFaces.Value
                            };
                            if (trackBarSmileyFaces.Value < 2 || trackBarSmileyFaces.Value > 5)
                            {
                                errorProvider1.SetError(trackBarSmileyFaces, "Number of smiley faces cannot be less than 2 or more than 5");
                                return;
                            }

                            break;

                        case QuestionType.Slider:

                            if (numericUpDownStartValue.Value >= numericUpDownEndValue.Value)
                            {
                                errorProvider1.SetError(numericUpDownStartValue, "slider start value can't be more or equal to slider end value");
                                return;
                            }
                            if (numericUpDownEndValue.Value <= numericUpDownStartValue.Value)
                            {
                                errorProvider1.SetError(numericUpDownStartValue, " slider end value can't be less than slider start value");
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

                            tNewQuestion = new SliderQuestion
                            {
                                Id = mEditingQuestion.Id,
                                QuestionOrder = mEditingQuestion.QuestionOrder,
                                QuestionText = mEditingQuestion.QuestionText,
                                StartValueCaption = textBoxStartCaption.Text,
                                EndValueCaption = textBoxEndCaption.Text
                            };

                            ((SliderQuestion)tNewQuestion).SetRange(
                                (int)numericUpDownStartValue.Value,
                                (int)numericUpDownEndValue.Value
                            );

                            break;
                    }

                    mQuestionService.UpdateChildTableType(tNewQuestion, tOldType);
                    mEditingQuestion = tNewQuestion;
                }

                Close();
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
            Close();
        }

        //shows the number of chars entered by the user into the question text textbox
        private void textBoxQuestionText_TextChanged(object sender, EventArgs e)
        {
            lblCharNumber.Text = $"{textBoxQuestionText.Text.Length.ToString()}/1000";
        }

        //shows the number of chars entered by the user into the start caption text box
        private void textBoxStartCaption_TextChanged(object sender, EventArgs e)
        {
            lblCharNumberStartCaption.Text = $"{textBoxStartCaption.Text.Length.ToString()}/100";
        }
    }
}