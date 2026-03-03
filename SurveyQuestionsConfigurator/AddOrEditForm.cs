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
    /// <summary>
    /// Handles creation and editing of survey questions including Star, Smiley Faces, and Slider types.
    /// Provides input validation, localized display, and dynamic form updates based on question type.
    /// </summary>
    public partial class AddOrEditForm : Form
    {
        private QuestionService mQuestionService;
        private Question mEditingQuestion;
        private const string ARABIC_CULTURE_CODE = "ar";

        /// <summary>
        /// Initializes a new instance of the AddOrEditForm.
        /// If a Question object is provided, the form enters Edit mode and populates controls with the question data.
        /// Otherwise, the form enters Add mode with default values.
        /// </summary>
        /// <param name="pQuestion">
        /// The Question to edit. Pass null to create a new question.
        /// </param>
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
                this.ActiveControl = textBoxQuestionText;
                numericUpDownQuestionOrder.TabIndex = 1;
                comboBoxQuestionTypes.TabIndex = 2;
                numericUpDownStartValue.TabIndex = 3;
                numericUpDownEndValue.TabIndex = 4;
                textBoxStartCaption.TabIndex = 5;
                textBoxEndCaption.TabIndex = 6;

                mQuestionService = new QuestionService();

                PopulateTypeComboBox();

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
                Log.Error(tEx, tEx.Message);
                throw;
            }
        }

        /// <summary>
        /// Retrieves the localized string for a given enum value from the Resources file.
        /// Builds the resource key in the format "EnumType_EnumValue".
        /// If the resource is not found, the enum's name is returned as a fallback.
        /// </summary>
        /// <param name="value">The enum value to localize.</param>
        /// <returns>The localized string if found; otherwise, the enum's name.</returns>
        private string GetLocalizedEnum(Enum value)
        {
            try
            {
                // Build the resource key example QuestionType_Slider
                string resourceKey = $"{value.GetType().Name}_{value}";

                string localizedValue = Resources.ResourceManager.GetString(resourceKey);

                // Fallback to enum name if not found
                return string.IsNullOrEmpty(localizedValue) ? value.ToString() : localizedValue;
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                return value.ToString();
            }
        }

        /// <summary>
        /// Fills the form controls with values from the provided question object.
        /// Determines the specific question type (Star, SmileyFaces, or Slider)
        /// and populates type specific controls accordingly.
        /// </summary>
        /// <param name="pQuestion">The question object whose values will populate the form.</param>
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
                Log.Error(tEx, tEx.Message);
                throw;
            }
        }

        /// <summary>
        /// Creates a new question based on the current form inputs and adds it to the database.
        /// Validates input fields and creates the appropriate Question subtype.
        /// </summary>
        /// <param name="pSender">The sender of the event.</param>
        /// <param name="pE">The event arguments.</param>
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
                Log.Error(tEx, tEx.Message);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Updates the star label according to the given value sent from the trackbar and displays it visually.
        /// </summary>
        /// <param name="pValue">Number of stars to display (1–10)</param>
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
                Log.Error(tEx, tEx.Message);
                throw;
            }
        }

        /// <summary>
        /// Updates the smiley faces label according to the given value sent from the trackbar and displays it visually.
        /// </summary>
        /// <param name="pValue">Number of smiley faces to display</param>
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
                Log.Error(tEx, tEx.Message);
                throw;
            }
        }

        /// <summary>
        /// Handles scrolling of the stars trackbar and updates the stars label and number display.
        /// </summary>
        /// <param name="pSender">The trackbar control that triggered the event</param>
        /// <param name="pE">Event arguments</param>
        private void TrackBarStars_Scroll(object pSender, EventArgs pE)
        {
            try
            {
                UpdateStars(trackBarStars.Value);
                lblNumberOfStars.Text = trackBarStars.Value.ToString();
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Handles the change of the selected question type in the combo box.
        /// Shows the appropriate panel based on the selection.
        /// Hides all other panels and brings the selected one to the front.
        /// </summary>
        /// <param name="pSender">The sender object.</param>
        /// <param name="pE">Event arguments.</param>
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
                Log.Error(tEx, tEx.Message);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Handles scrolling of the smiley faces  and updates the stars label and number display.
        /// </summary>
        /// <param name="pSender">The sender object.</param>
        /// <param name="pE">Event arguments.</param>
        private void trackBarSmiley_Scroll(object pSender, EventArgs pE)
        {
            try
            {
                UpdateSmileyFace(trackBarSmileyFaces.Value);
                lblFacesNumber.Text = trackBarSmileyFaces.Value.ToString();
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Updates the character count label for the End Caption text box
        /// based on the current text length and the current UI culture.
        /// </summary>
        /// <param name="pSender">The sender object.</param>
        /// <param name="pE">Event arguments.</param>
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
                Log.Error(tEx, tEx.Message);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Updates the existing question if the type hasn't changed,
        /// or replaces it with a new type if the type has changed.
        /// Performs validation for each question type.
        /// </summary>
        /// <param name="pSender">The sender object.</param>
        /// <param name="pE">Event arguments.</param>
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

                Result<bool> tResult = Result<bool>.Failure(ResultStatus.UnknownTypeError);

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
                //IF type changed call UpdateChildTableType that takes the new question and the old type
                //Deletes the old type record from Database and inserts the new type in the correct table
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
                    this.Close();
                }
                else ShowErrorBox(tResult.Status);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Closes the form without saving any changes.
        /// </summary>
        /// <param name="pSender">The sender object.</param>
        /// <param name="pE">Event arguments.</param>
        private void btnCancel_Click(object pSender, EventArgs pE)
        {
            try
            {
                this.Close();
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Updates the character counter label as the user types in the question text box.
        /// based on the current text length and the current UI culture.
        /// </summary>
        /// <param name="pSender">The sender object.</param>
        /// <param name="pE">Event arguments.</param>
        private void textBoxQuestionText_TextChanged(object pSender, EventArgs pE)
        {
            try
            {
                if (Thread.CurrentThread.CurrentUICulture.Name.StartsWith(ARABIC_CULTURE_CODE))
                    lblCharNumber.Text = $"1000/{textBoxQuestionText.Text.Length.ToString()}";
                else
                    lblCharNumber.Text = $"{textBoxQuestionText.Text.Length.ToString()}/1000";
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Updates the character count label for the Start Caption text box
        /// based on the current text length and the current UI culture.
        /// </summary>
        /// <param name="pSender">The sender object.</param>
        /// <param name="pE">Event arguments.</param>
        private void textBoxStartCaption_TextChanged(object pSender, EventArgs pE)
        {
            try
            {
                if (Thread.CurrentThread.CurrentUICulture.Name.StartsWith(ARABIC_CULTURE_CODE))
                    lblCharNumberStartCaption.Text = $"100/{textBoxStartCaption.Text.Length.ToString()}";
                else
                    lblCharNumberStartCaption.Text = $"{textBoxStartCaption.Text.Length.ToString()}/100";
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                ShowErrorBox(ResultStatus.UnexpectedError);
            }
        }

        /// <summary>
        /// Shows a custom error message box based on a ResultStatus.
        /// Falls back to a standard MessageBox if the custom box fails.
        /// </summary>
        /// <param name="pStatus">The ResultStatus indicating the error type.</param>
        private void ShowErrorBox(ResultStatus pStatus)
        {
            try
            {
                CustomMessageBox.Show(Resources.ResourceManager.GetString(pStatus.ToString()), Resources.ErrorCaption, ButtonTypes.Ok, IconTypes.Error);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                MessageBox.Show(Resources.UnexpectedError, Resources.ErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Populates the QuestionType combo box with localized enum values.
        /// </summary>
        private void PopulateTypeComboBox()
        {
            try
            {
                comboBoxQuestionTypes.DataSource = Enum.GetValues(typeof(QuestionType))
             .Cast<QuestionType>()//returns an array of object values so we cast it
             .Select(q => new
             {
                 Value = q,
                 Text = GetLocalizedEnum(q)
             })
             .ToList();

                comboBoxQuestionTypes.DisplayMember = "Text";
                comboBoxQuestionTypes.ValueMember = "Value";
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                throw;
            }
        }
    }
}