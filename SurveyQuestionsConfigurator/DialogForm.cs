using Serilog;
using SurveyQuestionsConfigurator.Models;
using SurveyQuestionsConfiguratorModels;
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
    public partial class DialogForm : Form
    {
        private QuestionService mQuestionService;
        private Question mEditingQuestion;

        public DialogForm(Question pQuestion)
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
                btnAdd.Visible = false;
                Text = "Edit";
            }
            else
            {
                btnUpdate.Visible = false;
                btnAdd.Visible = true;
                comboBoxQuestionTypes.SelectedIndex = 0;
                UpdateStars(trackBarStars.Value);
                Text = "Create";
            }
        }

        //Fills the form with values from the question passed from the main form
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
        private void btnAdd_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();

            if (String.IsNullOrWhiteSpace(textBoxQuestionText.Text))
            {
                errorProvider.SetError(textBoxQuestionText, "Question text cannot be empty or white space");
                return;
            }
            if (textBoxQuestionText.Text.Length > 1000)
            {
                errorProvider.SetError(textBoxQuestionText, "Question text cannot be more than 1000 characters");
                return;
            }

            QuestionType tSelectedType = (QuestionType)comboBoxQuestionTypes.SelectedItem;

            Question tQuestion = null;

            switch (tSelectedType)
            {
                case QuestionType.Star:
                    if (trackBarStars.Value < 1)
                    {
                        errorProvider.SetError(trackBarStars, "Number of stars cannot be less than 1");
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
                        errorProvider.SetError(numericUpDownStartValue, "Slider start value cannot be more or equal to slider end value");
                        return;
                    }
                    if (numericUpDownEndValue.Value <= numericUpDownStartValue.Value)
                    {
                        errorProvider.SetError(numericUpDownStartValue, "Slider end value cannot be less than slider start value");
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(textBoxStartCaption.Text))
                    {
                        errorProvider.SetError(textBoxStartCaption, "Start caption cannot be empty or white space");
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(textBoxEndCaption.Text))
                    {
                        errorProvider.SetError(textBoxEndCaption, "End caption cannot be empty or white space");
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

            var tResult = mQuestionService.AddQuestion(tQuestion);

            if (tResult.IsSuccess)
                Close();
            else
            {
                MessageBox.Show(
                    tResult.Error,
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
            string tStr = "";

            for (int i = 0; i < pValue; i++)
            {
                tStr += ":) ";
            }

            lblSmileyFaces.Text = tStr;
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

            QuestionType tSelected = (QuestionType)comboBoxQuestionTypes.SelectedItem;

            switch (tSelected)
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
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();
            if (String.IsNullOrWhiteSpace(textBoxQuestionText.Text))
            {
                errorProvider.SetError(textBoxQuestionText, "Question text cannot be empty or white space");
                return;
            }
            if (textBoxQuestionText.Text.Length > 1000)
            {
                errorProvider.SetError(textBoxQuestionText, "Question text cannot be more than 1000 characters");
                return;
            }
            mEditingQuestion.QuestionText = textBoxQuestionText.Text;
            mEditingQuestion.QuestionOrder = (int)numericUpDownQuestionOrder.Value;

            var tOldType = mEditingQuestion.QuestionType;
            var tNewType = (QuestionType)comboBoxQuestionTypes.SelectedItem;

            Result<bool> tResult = Result<bool>.Failure("Unknown question type");

            if (tOldType == tNewType)//if type didn't change
            {
                switch (mEditingQuestion)
                {
                    case StarQuestion tStarQuestion:
                        if (trackBarStars.Value < 1)
                        {
                            errorProvider.SetError(trackBarStars, "Number of stars cannot be less than 1");
                            return;
                        }
                        tStarQuestion.NumberOfStars = trackBarStars.Value;
                        tResult = mQuestionService.UpdateQuestion(tStarQuestion);
                        break;

                    case SmileyFacesQuestion tSmileyQuestion:
                        if (trackBarSmileyFaces.Value < 2 || trackBarSmileyFaces.Value > 5)
                        {
                            errorProvider.SetError(trackBarSmileyFaces, "Number of smiley faces cannot be less than 2 or more than 5");
                            return;
                        }
                        tSmileyQuestion.NumberOfSmileyFaces = trackBarSmileyFaces.Value;
                        tResult = mQuestionService.UpdateQuestion(tSmileyQuestion);
                        break;

                    case SliderQuestion tSliderQuestion:
                        if (numericUpDownStartValue.Value >= numericUpDownEndValue.Value)
                        {
                            errorProvider.SetError(numericUpDownStartValue, " slider start value cannot be more or equal to slider end value");
                            return;
                        }
                        if (numericUpDownEndValue.Value <= numericUpDownStartValue.Value)
                        {
                            errorProvider.SetError(numericUpDownStartValue, " slider end value cannot be less than slider start value");
                            return;
                        }
                        if (String.IsNullOrWhiteSpace(textBoxStartCaption.Text))
                        {
                            errorProvider.SetError(textBoxStartCaption, "Start caption cannot be empty or white space");
                            return;
                        }
                        if (String.IsNullOrWhiteSpace(textBoxEndCaption.Text))
                        {
                            errorProvider.SetError(textBoxEndCaption, "End caption cannot be empty or white space");
                            return;
                        }

                        tSliderQuestion.SetRange((int)numericUpDownStartValue.Value, (int)numericUpDownEndValue.Value);
                        tSliderQuestion.StartValueCaption = textBoxStartCaption.Text;
                        tSliderQuestion.EndValueCaption = textBoxEndCaption.Text;
                        tResult = mQuestionService.UpdateQuestion(tSliderQuestion);
                        break;
                }
            }
            //if type changed call UpdateChildTableType that takes the new question and the old type
            //deletes the old type record from Database and inserts the new type in the correct table
            else
            {
                Question tNewQuestion = null;

                switch (tNewType)
                {
                    case QuestionType.Star:
                        if (trackBarStars.Value < 1)
                        {
                            errorProvider.SetError(trackBarStars, "Number of stars cannot be less than 1");
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
                        if (trackBarSmileyFaces.Value < 2 || trackBarSmileyFaces.Value > 5)
                        {
                            errorProvider.SetError(trackBarSmileyFaces, "Number of smiley faces cannot be less than 2 or more than 5");
                            return;
                        }
                        tNewQuestion = new SmileyFacesQuestion
                        {
                            Id = mEditingQuestion.Id,
                            QuestionOrder = mEditingQuestion.QuestionOrder,
                            QuestionText = mEditingQuestion.QuestionText,
                            NumberOfSmileyFaces = trackBarSmileyFaces.Value
                        };

                        break;

                    case QuestionType.Slider:

                        if (numericUpDownStartValue.Value >= numericUpDownEndValue.Value)
                        {
                            errorProvider.SetError(numericUpDownStartValue, "Slider start value cannot be more or equal to slider end value");
                            return;
                        }
                        if (numericUpDownEndValue.Value <= numericUpDownStartValue.Value)
                        {
                            errorProvider.SetError(numericUpDownStartValue, "Slider end value cannot be less than slider start value");
                            return;
                        }
                        if (string.IsNullOrWhiteSpace(textBoxStartCaption.Text))
                        {
                            errorProvider.SetError(textBoxStartCaption, "Start caption cannot be empty or white space");
                            return;
                        }
                        if (string.IsNullOrWhiteSpace(textBoxEndCaption.Text))
                        {
                            errorProvider.SetError(textBoxEndCaption, "End caption cannot be empty or white space");
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

                tResult = mQuestionService.UpdateChildTableType(tNewQuestion, tOldType);

                if (tResult.IsSuccess)
                    mEditingQuestion = tNewQuestion;
            }

            if (tResult.IsSuccess)
                Close();
            else
                MessageBox.Show(
                    this,
                    tResult.Error,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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