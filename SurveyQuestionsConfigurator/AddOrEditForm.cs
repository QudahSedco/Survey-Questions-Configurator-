using Serilog;
using SurveyQuestionsConfigurator.Models;
using SurveyQuestionsConfigurator.Properties;
using SurveyQuestionsConfiguratorModels;
using SurveyQuestionsConfiguratorModels.Result;
using SurveyQuestionsConfiguratorServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SurveyQuestionsConfigurator
{
    public partial class AddOrEditForm : Form
    {
        private QuestionService mQuestionService;
        private Question mEditingQuestion;

        private ComponentResourceManager mRes = new ComponentResourceManager(typeof(MainForm));
        private const string UNEXPECTED_ERROR_MESSAGE = "An unexpected error occurred";

        public AddOrEditForm(Question pQuestion)
        {
            try
            {
                InitializeComponent();
                StartPosition = FormStartPosition.CenterParent;
                FormBorderStyle = FormBorderStyle.FixedDialog;
                MaximizeBox = false;
                MinimizeBox = false;

                textBoxQuestionText.TabIndex = 0;
                textBoxQuestionText.Focus();

                numericUpDownQuestionOrder.TabIndex = 1;
                comboBoxQuestionTypes.TabIndex = 2;
                numericUpDownStartValue.TabIndex = 3;
                numericUpDownEndValue.TabIndex = 4;
                textBoxStartCaption.TabIndex = 5;
                textBoxEndCaption.TabIndex = 6;

                if (Thread.CurrentThread.CurrentUICulture.Name.StartsWith("ar"))
                {
                    this.RightToLeft = RightToLeft.Yes;
                    pnlStars.RightToLeft = RightToLeft.Yes;
                    pnlSmileyFaces.RightToLeft = RightToLeft.Yes;
                    pnlSlider.RightToLeft = RightToLeft.Yes;
                    lblSmileyFaces.RightToLeft = RightToLeft.Yes;
                    lblSmileyFaces.TextAlign = ContentAlignment.MiddleRight;
                    this.RightToLeftLayout = true;
                }

                mQuestionService = new QuestionService();

                comboBoxQuestionTypes.DataSource = Enum.GetValues(typeof(QuestionType))
             .Cast<QuestionType>()//returns an array of enum values so we cast it
             .Select(q => new
             {
                 Value = q,
                 Text = GetLocalizedDescription(q)
             })
             .ToList();

                comboBoxQuestionTypes.DisplayMember = "Text";
                comboBoxQuestionTypes.ValueMember = "Value";

                pnlSmileyFaces.Visible = true;

                if (pQuestion != null) //means form is in edit mode
                {
                    mEditingQuestion = pQuestion;
                    comboBoxQuestionTypes.SelectedValue = pQuestion.QuestionType;
                    PopulateFields(pQuestion);
                    btnUpdate.Visible = true;
                    btnAdd.Visible = false;
                    Text = Resources.Edit;
                }
                else
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                    comboBoxQuestionTypes.SelectedIndex = 0;
                    UpdateStars(trackBarStars.Value);
                    Text = Resources.Add;
                }
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        private string GetLocalizedDescription(Enum value)
        {
            try
            {
                // Build the resource key: QuestionType_Slider
                string resourceKey = $"{value.GetType().Name}_{value}";

                string localizedValue = Resources.ResourceManager.GetString(resourceKey);

                // Fallback to enum name if not found
                return string.IsNullOrEmpty(localizedValue) ? value.ToString() : localizedValue;
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                ShowErrorBox(ResultStatus.UnexpectedError);
                return value.ToString();
            }
        }

        //Fills the form with values from the question passed from the main form
        private void PopulateFields(Question pQuestion)
        {
            try
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
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        //create button creates a new question
        private void btnAdd_Click(object pSender, EventArgs pE)
        {
            try
            {
                {
                    errorProvider.Clear();

                    if (String.IsNullOrWhiteSpace(textBoxQuestionText.Text))
                    {
                        errorProvider.SetError(textBoxQuestionText, Resources.NullOrWhiteSpaceError);
                        return;
                    }
                    if (textBoxQuestionText.Text.Length > 1000)
                    {
                        errorProvider.SetError(textBoxQuestionText, Resources.QuestionLengthError);
                        return;
                    }

                    QuestionType tSelectedType = (QuestionType)comboBoxQuestionTypes.SelectedValue;

                    Question tQuestion = null;

                    switch (tSelectedType)
                    {
                        case QuestionType.Star:
                            if (trackBarStars.Value < 1)
                            {
                                errorProvider.SetError(trackBarStars, Resources.StarsNumberError);
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
                                errorProvider.SetError(numericUpDownStartValue, Resources.SliderStartValueError);
                                return;
                            }
                            if (numericUpDownEndValue.Value <= numericUpDownStartValue.Value)
                            {
                                errorProvider.SetError(numericUpDownStartValue, Resources.SliderEndValueError);
                                return;
                            }
                            if (string.IsNullOrWhiteSpace(textBoxStartCaption.Text))
                            {
                                errorProvider.SetError(textBoxStartCaption, Resources.StartCaptionEmptyError);
                                return;
                            }
                            if (string.IsNullOrWhiteSpace(textBoxEndCaption.Text))
                            {
                                errorProvider.SetError(textBoxEndCaption, Resources.EndCaptionEmptyError);
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
                    {
                        this.DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        ShowErrorBox(tResult.Status);
                    }
                }
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        //prints the stars according to the trackbar
        private void UpdateStars(int pValue)
        {
            try
            {
                lblStars.Text =
                    new string('★', pValue) +
                    new string('☆', 10 - pValue);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
            }
        }

        //prints smiley faces according to the trackbar
        private void UpdateSmileyFace(int pValue)
        {
            try
            {
                string tStr = "";

                for (int i = 0; i < pValue; i++)
                {
                    tStr += ":) ";
                }

                lblSmileyFaces.Text = tStr;
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
            }
        }

        private void TrackBarStars_Scroll(object pSender, EventArgs pE)
        {
            try
            {
                UpdateStars(trackBarStars.Value);
                lblNumberOfStars.Text = trackBarStars.Value.ToString();
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        private void ComboBoxQuestionType_SelectedIndexChanged(object pSender, EventArgs pE)
        {
            try
            {
                pnlStars.Visible = false;
                pnlSmileyFaces.Visible = false;
                pnlSlider.Visible = false;
                if (comboBoxQuestionTypes.SelectedValue == null) return;

                if (!(comboBoxQuestionTypes.SelectedValue is QuestionType tSelected))
                    return;

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
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        private void trackBarSmiley_Scroll(object pSender, EventArgs pE)
        {
            try
            {
                UpdateSmileyFace(trackBarSmileyFaces.Value);
                lblFacesNumber.Text = trackBarSmileyFaces.Value.ToString();
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        //shows the number of chars the user entered for end caption text box
        private void textBoxEndCaption_TextChanged(object pSender, EventArgs pE)
        {
            try
            {
                if (System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "ar")
                    lblCharNumberEndCaption.Text = $"100/{textBoxEndCaption.Text.Length.ToString()}";
                else
                    lblCharNumberEndCaption.Text = ($"{textBoxEndCaption.Text.Length.ToString()}/100");
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
            }
        }

        //update button handles update logic
        //if type didn't change calls update method
        //if type did change calls UpdateChildTableType
        //that deletes the old type record and inserts a new record according to the new type chosen
        private void btnUpdate_Click(object pSender, EventArgs pE)
        {
            try
            {
                errorProvider.Clear();
                if (String.IsNullOrWhiteSpace(textBoxQuestionText.Text))
                {
                    errorProvider.SetError(textBoxQuestionText, Resources.NullOrWhiteSpaceError);
                    return;
                }
                if (textBoxQuestionText.Text.Length > 1000)
                {
                    errorProvider.SetError(textBoxQuestionText, Resources.QuestionLengthError);
                    return;
                }
                mEditingQuestion.QuestionText = textBoxQuestionText.Text;
                mEditingQuestion.QuestionOrder = (int)numericUpDownQuestionOrder.Value;

                var tOldType = mEditingQuestion.QuestionType;
                var tNewType = (QuestionType)comboBoxQuestionTypes.SelectedValue;

                Result<bool> tResult = Result<bool>.Failure(ResultStatus.UnknownTypeError); // temp

                if (tOldType == tNewType)//if type didn't change
                {
                    switch (mEditingQuestion)
                    {
                        case StarQuestion tStarQuestion:
                            if (trackBarStars.Value < 1)
                            {
                                errorProvider.SetError(trackBarStars, Resources.StarsNumberError);
                                return;
                            }
                            tStarQuestion.NumberOfStars = trackBarStars.Value;
                            tResult = mQuestionService.UpdateQuestion(tStarQuestion);
                            break;

                        case SmileyFacesQuestion tSmileyQuestion:
                            if (trackBarSmileyFaces.Value < 2 || trackBarSmileyFaces.Value > 5)
                            {
                                errorProvider.SetError(trackBarSmileyFaces, Resources.SmileyFacesNumberError);
                                return;
                            }
                            tSmileyQuestion.NumberOfSmileyFaces = trackBarSmileyFaces.Value;
                            tResult = mQuestionService.UpdateQuestion(tSmileyQuestion);
                            break;

                        case SliderQuestion tSliderQuestion:
                            if (numericUpDownStartValue.Value >= numericUpDownEndValue.Value)
                            {
                                errorProvider.SetError(numericUpDownStartValue, Resources.SliderStartValueError);
                                return;
                            }
                            if (numericUpDownEndValue.Value <= numericUpDownStartValue.Value)
                            {
                                errorProvider.SetError(numericUpDownStartValue, Resources.SliderEndValueError);
                                return;
                            }
                            if (String.IsNullOrWhiteSpace(textBoxStartCaption.Text))
                            {
                                errorProvider.SetError(textBoxStartCaption, Resources.StartCaptionEmptyError);
                                return;
                            }
                            if (String.IsNullOrWhiteSpace(textBoxEndCaption.Text))
                            {
                                errorProvider.SetError(textBoxEndCaption, Resources.EndCaptionEmptyError);
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
                                errorProvider.SetError(trackBarStars, Resources.StarsNumberError);
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
                                errorProvider.SetError(trackBarSmileyFaces, Resources.SmileyFacesNumberError);
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
                                errorProvider.SetError(numericUpDownStartValue, Resources.SliderStartValueError);
                                return;
                            }
                            if (numericUpDownEndValue.Value <= numericUpDownStartValue.Value)
                            {
                                errorProvider.SetError(numericUpDownStartValue, Resources.SliderEndValueError);
                                return;
                            }
                            if (string.IsNullOrWhiteSpace(textBoxStartCaption.Text))
                            {
                                errorProvider.SetError(textBoxStartCaption, Resources.StartCaptionEmptyError);
                                return;
                            }
                            if (string.IsNullOrWhiteSpace(textBoxEndCaption.Text))
                            {
                                errorProvider.SetError(textBoxEndCaption, Resources.EndCaptionEmptyError);
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
                {
                    this.DialogResult = DialogResult.OK;
                    Close();
                }
                else ShowErrorBox(tResult.Status);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        private void btnCancel_Click(object pSender, EventArgs pE)
        {
            try
            {
                Close();
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        //shows the number of chars entered by the user into the question text textbox
        private void textBoxQuestionText_TextChanged(object pSender, EventArgs pE)
        {
            try
            {
                if (System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "ar")
                    lblCharNumber.Text = $"1000/{textBoxQuestionText.Text.Length.ToString()}";
                else
                    lblCharNumber.Text = $"{textBoxQuestionText.Text.Length.ToString()}/1000";
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        //shows the number of chars entered by the user into the start caption text box
        private void textBoxStartCaption_TextChanged(object pSender, EventArgs pE)
        {
            try
            {
                if (System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "ar")
                    lblCharNumberStartCaption.Text = $"100/{textBoxStartCaption.Text.Length.ToString()}";
                else
                    lblCharNumberStartCaption.Text = $"{textBoxStartCaption.Text.Length.ToString()}/100";
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        private void ShowErrorBox(ResultStatus pStatus)
        {
            try
            {
                CustomMessageBox.Show(Resources.ResourceManager.GetString(pStatus.ToString()), "Error", ButtonTypes.Ok, IconTypes.Error);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, UNEXPECTED_ERROR_MESSAGE);
            }
        }
    }
}