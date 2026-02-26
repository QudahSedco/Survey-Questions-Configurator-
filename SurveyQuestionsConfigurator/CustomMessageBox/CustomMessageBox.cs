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
    public partial class CustomMessageBox : Form
    {
        public CustomMessageBox()
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

        public string MessageText
        {
            get => label1.Text;
            set => label1.Text = value;
        }

        public string TitleText
        {
            get => this.Text;
            set => this.Text = value;
        }

        private Image _image;

        public Image IconImage
        {
            get => _image;
            set
            {
                _image = value;
                pictureBox1.Image = _image;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox1.RightToLeft = RightToLeft.No;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        public static DialogResult Show(string message, string title, ButtonTypes buttonTypes, IconTypes iconTypes)
        {
            using (var box = new CustomMessageBox())
            {
                box.btnOK.Visible = false;
                box.BtnYes.Visible = false;
                box.btnNo.Visible = false;

                box.MessageText = message;
                box.TitleText = title;
                box.IconImage = HandleIcon(iconTypes);
                box.pictureBox1.RightToLeft = RightToLeft.No;

                switch (buttonTypes)
                {
                    case ButtonTypes.Ok:
                        box.btnOK.Visible = true;
                        break;

                    case ButtonTypes.YesNo:
                        box.BtnYes.Visible = true;
                        box.btnNo.Visible = true;
                        break;
                }

                return box.ShowDialog();
            }
        }

        private static Image HandleIcon(IconTypes pType)
        {
            switch (pType)
            {
                case IconTypes.Question:
                    return SystemIcons.Question.ToBitmap();

                case IconTypes.Error:
                    return SystemIcons.Error.ToBitmap();

                default:
                    return SystemIcons.Information.ToBitmap();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void BtnYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }
    }
}