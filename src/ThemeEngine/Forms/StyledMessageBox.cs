using System;
using System.Drawing;
using System.Windows.Forms;

namespace ThemeEngine.Forms
{
    public partial class StyledMessageBox : Form
    {

        [Flags]
        public enum BorderButtons
        {

            Minimize = 1,
            Maximize = 2,
            Close = 4

        }

        public enum MessageBoxContent
        {

            Text,
            RichText

        }

        private StyledMessageBox(StyledMessageBoxSettings settings)
        {
            InitializeComponent();

            Text = settings.Title;
            if (!settings.Size.IsEmpty)
            {
                Size = settings.Size;
            }
            else if (settings.ContentStyle == MessageBoxContent.Text)
            {
                Size = new Size(settings.MessageBoxSideImage != null ? 380 : 280, 280);
            }

            Control contentContainer = CreateContentControl(settings.Content, settings.ContentStyle);

            MinimizeBox = (settings.ActivatedBorderButtons & BorderButtons.Minimize) != 0;
            MaximizeBox = (settings.ActivatedBorderButtons & BorderButtons.Maximize) != 0;

            if ((settings.ActivatedBorderButtons & BorderButtons.Close) == 0)
            {
                //TODO
            }

            FormBorderStyle = settings.BorderStyle;

            if (settings.MessageBoxIcon != null)
            {
                Icon = settings.MessageBoxIcon;
            }

            if (settings.MessageBoxSideImage != null)
            {
                pbImage.Image = settings.MessageBoxSideImage;
            }
            else
            {
                panelImage.Visible = false;
            }

            btnOK.Visible = settings.ActivatedDialogButtons == MessageBoxButtons.OK ||
                            settings.ActivatedDialogButtons == MessageBoxButtons.OKCancel;

            btnYes.Visible = settings.ActivatedDialogButtons == MessageBoxButtons.YesNo ||
                             settings.ActivatedDialogButtons == MessageBoxButtons.YesNoCancel;

            btnNo.Visible = settings.ActivatedDialogButtons == MessageBoxButtons.YesNo ||
                            settings.ActivatedDialogButtons == MessageBoxButtons.YesNoCancel;

            btnAbort.Visible = settings.ActivatedDialogButtons == MessageBoxButtons.AbortRetryIgnore;

            btnCancel.Visible = settings.ActivatedDialogButtons == MessageBoxButtons.RetryCancel ||
                                settings.ActivatedDialogButtons == MessageBoxButtons.OKCancel ||
                                settings.ActivatedDialogButtons == MessageBoxButtons.YesNoCancel;

            btnRetry.Visible = settings.ActivatedDialogButtons == MessageBoxButtons.RetryCancel ||
                               settings.ActivatedDialogButtons == MessageBoxButtons.AbortRetryIgnore;
            btnIgnore.Visible = settings.ActivatedDialogButtons == MessageBoxButtons.AbortRetryIgnore;

            StyleManager.RegisterControls(this);
        }

        private Control CreateContentControl(string content, MessageBoxContent contentStyle)
        {
            if (contentStyle == MessageBoxContent.RichText)
            {
                RichTextBox tb = new RichTextBox();
                tb.Parent = panelDialogContent;
                tb.Dock = DockStyle.Fill;
                tb.Text = content;
                return tb;
            }

            Label lbl = new Label();
            lbl.Parent = panelDialogContent;
            lbl.Dock = DockStyle.Fill;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.Padding = new Padding(50);
            lbl.Text = content;
            return lbl;
        }

        private void FinalizeDialog(DialogResult result)
        {
            DialogResult = result;
            Close();
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            FinalizeDialog(DialogResult.Abort);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FinalizeDialog(DialogResult.Cancel);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            FinalizeDialog(DialogResult.OK);
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            FinalizeDialog(DialogResult.No);
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            FinalizeDialog(DialogResult.Yes);
        }

        private void btnRetry_Click(object sender, EventArgs e)
        {
            FinalizeDialog(DialogResult.Retry);
        }

        private void btnIgnore_Click(object sender, EventArgs e)
        {
            FinalizeDialog(DialogResult.Ignore);
        }

        public static void Show(
            string title, string content, MessageBoxContent contentStyle = MessageBoxContent.Text)
        {
            StyledMessageBoxSettings settings = new StyledMessageBoxSettings
                                                {
                                                    Title = title,
                                                    ContentStyle = contentStyle,
                                                    Content = content
                                                };
            StyledMessageBox mbox = new StyledMessageBox(settings);
            mbox.ShowDialog();
        }

        public static DialogResult Show(
            string title, string content, MessageBoxButtons buttons, Icon messageBoxIcon = null,
            MessageBoxContent contentStyle = MessageBoxContent.Text, Image messageBoxImage = null,
            FormBorderStyle borderStyle = FormBorderStyle.FixedDialog, Size size = default,
            BorderButtons borderButtons = BorderButtons.Close)
        {
            StyledMessageBoxSettings settings = new StyledMessageBoxSettings
                                                {
                                                    Title = title,
                                                    ContentStyle = contentStyle,
                                                    Content = content,
                                                    Size = size,
                                                    ActivatedDialogButtons = buttons,
                                                    ActivatedBorderButtons = borderButtons,
                                                    BorderStyle = borderStyle,
                                                    MessageBoxSideImage = messageBoxImage,
                                                    MessageBoxIcon = messageBoxIcon
                                                };

            StyledMessageBox mbox = new StyledMessageBox(settings);
            return mbox.ShowDialog();
        }

        private class StyledMessageBoxSettings
        {

            public BorderButtons ActivatedBorderButtons = BorderButtons.Close;
            public MessageBoxButtons ActivatedDialogButtons = MessageBoxButtons.OK;
            public FormBorderStyle BorderStyle = FormBorderStyle.FixedDialog;
            public string Content;
            public MessageBoxContent ContentStyle = MessageBoxContent.Text;
            public Icon MessageBoxIcon;
            public Image MessageBoxSideImage;
            public Size Size;

            public string Title;

        }

    }
}