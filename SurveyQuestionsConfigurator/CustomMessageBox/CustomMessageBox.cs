using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;

namespace SurveyQuestionsConfigurator
{
    /// <summary>
    /// Custom form  used to display messages to the user.
    /// Supports OK and Yes/No button layouts and optional icons
    /// This dialog is displayed centered over its parent form and
    /// is intended to replace the standard MessageBox with a
    /// consistent application look and feel and supports localization.
    /// </summary>
    public partial class CustomMessageBox : Form
    {
        /// <summary>
        /// Configures the dialog appearance, disables resizing and
        /// taskbar visibility, applies default font settings, and
        /// enables automatic sizing based on content.
        /// </summary>
        public CustomMessageBox()
        {
            try
            {
                InitializeComponent();
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.ShowIcon = false;
                this.ShowInTaskbar = false;
                this.StartPosition = FormStartPosition.CenterParent;
                this.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
                this.AutoSize = true;
                this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                this.Padding = new Padding(10);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                throw;
            }
        }

        /// <summary>
        /// Displays a modal custom message box with the specified message,
        /// title, buttons, and icon.
        /// The dialog blocks the calling window until the user responds
        /// and returns the selected DialogResult.
        /// </summary>
        /// <param name="pMessage">The message text to display in the dialog.</param>
        /// <param name="pTitle">The title text shown in the dialog's title bar.</param>
        /// <param name="pButtonTypes">Determines which buttons are shown</param>
        /// <param name="pIconTypes">Determines which icon is displayed in the dialog.</param>
        /// <returns></returns>
        public static DialogResult Show(string pMessage, string pTitle, ButtonTypes pButtonTypes, IconTypes pIconTypes)
        {
            try
            {
                using (var tForm = new CustomMessageBox())
                {
                    tForm.btnOK.Visible = false;
                    tForm.BtnYes.Visible = false;
                    tForm.btnNo.Visible = false;
                    tForm.lblText.Text = pMessage;
                    tForm.Text = pTitle;
                    tForm.IconPictureBox.Image = HandleIcon(pIconTypes);
                    tForm.IconPictureBox.RightToLeft = RightToLeft.No;
                    tForm.StartPosition = FormStartPosition.CenterParent;

                    switch (pButtonTypes)
                    {
                        case ButtonTypes.Ok:
                            tForm.btnOK.Visible = true;
                            break;

                        case ButtonTypes.YesNo:
                            tForm.BtnYes.Visible = true;
                            tForm.btnNo.Visible = true;
                            break;
                    }

                    return tForm.ShowDialog();
                }
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                throw;
            }
        }

        /// <summary>
        /// Returns the appropriate icon image based on the specified icon type.
        /// Uses standard system icons to ensure consistency with the operating system.
        /// </summary>
        /// <param name="pType">The type of icon to display.</param>
        /// <returns>A bitmap image representing the requested icon.</returns>
        private static Image HandleIcon(IconTypes pType)
        {
            try
            {
                switch (pType)
                {
                    case IconTypes.Question:
                        return SystemIcons.Question.ToBitmap();

                    case IconTypes.Error:
                        return SystemIcons.Error.ToBitmap();

                    case IconTypes.Success:
                        return SystemIcons.Information.ToBitmap();

                    default:
                        return SystemIcons.Information.ToBitmap();
                }
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                throw;
            }
        }

        private void btnOK_Click(object pSender, EventArgs pE)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void BtnYes_Click(object pSender, EventArgs pE)
        {
            this.DialogResult = DialogResult.Yes;
        }

        private void btnNo_Click(object pSender, EventArgs pE)
        {
            this.DialogResult = DialogResult.No;
        }
    }
}