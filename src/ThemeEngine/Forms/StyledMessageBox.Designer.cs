namespace ThemeEngine.Forms
{
    partial class StyledMessageBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelDialogButtons = new System.Windows.Forms.Panel();
            this.flpDialogButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnAbort = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnYes = new System.Windows.Forms.Button();
            this.btnNo = new System.Windows.Forms.Button();
            this.btnRetry = new System.Windows.Forms.Button();
            this.panelImage = new System.Windows.Forms.Panel();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.panelDialogContent = new System.Windows.Forms.Panel();
            this.btnIgnore = new System.Windows.Forms.Button();
            this.panelDialogButtons.SuspendLayout();
            this.flpDialogButtons.SuspendLayout();
            this.panelImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // panelDialogButtons
            // 
            this.panelDialogButtons.Controls.Add(this.flpDialogButtons);
            this.panelDialogButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelDialogButtons.Location = new System.Drawing.Point(0, 217);
            this.panelDialogButtons.Name = "panelDialogButtons";
            this.panelDialogButtons.Size = new System.Drawing.Size(587, 38);
            this.panelDialogButtons.TabIndex = 0;
            // 
            // flpDialogButtons
            // 
            this.flpDialogButtons.Controls.Add(this.btnAbort);
            this.flpDialogButtons.Controls.Add(this.btnCancel);
            this.flpDialogButtons.Controls.Add(this.btnOK);
            this.flpDialogButtons.Controls.Add(this.btnNo);
            this.flpDialogButtons.Controls.Add(this.btnYes);
            this.flpDialogButtons.Controls.Add(this.btnRetry);
            this.flpDialogButtons.Controls.Add(this.btnIgnore);
            this.flpDialogButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpDialogButtons.Location = new System.Drawing.Point(0, 0);
            this.flpDialogButtons.Name = "flpDialogButtons";
            this.flpDialogButtons.Padding = new System.Windows.Forms.Padding(5);
            this.flpDialogButtons.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flpDialogButtons.Size = new System.Drawing.Size(587, 38);
            this.flpDialogButtons.TabIndex = 0;
            this.flpDialogButtons.WrapContents = false;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(337, 8);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnAbort
            // 
            this.btnAbort.Location = new System.Drawing.Point(499, 8);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(75, 23);
            this.btnAbort.TabIndex = 1;
            this.btnAbort.Text = "Abort";
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(418, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnYes
            // 
            this.btnYes.Location = new System.Drawing.Point(175, 8);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(75, 23);
            this.btnYes.TabIndex = 3;
            this.btnYes.Text = "Yes";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // btnNo
            // 
            this.btnNo.Location = new System.Drawing.Point(256, 8);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(75, 23);
            this.btnNo.TabIndex = 4;
            this.btnNo.Text = "No";
            this.btnNo.UseVisualStyleBackColor = true;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // btnRetry
            // 
            this.btnRetry.Location = new System.Drawing.Point(94, 8);
            this.btnRetry.Name = "btnRetry";
            this.btnRetry.Size = new System.Drawing.Size(75, 23);
            this.btnRetry.TabIndex = 5;
            this.btnRetry.Text = "Retry";
            this.btnRetry.UseVisualStyleBackColor = true;
            this.btnRetry.Click += new System.EventHandler(this.btnRetry_Click);
            // 
            // panelImage
            // 
            this.panelImage.Controls.Add(this.pbImage);
            this.panelImage.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelImage.Location = new System.Drawing.Point(0, 0);
            this.panelImage.Name = "panelImage";
            this.panelImage.Size = new System.Drawing.Size(117, 217);
            this.panelImage.TabIndex = 1;
            // 
            // pbImage
            // 
            this.pbImage.Location = new System.Drawing.Point(9, 50);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(100, 100);
            this.pbImage.TabIndex = 0;
            this.pbImage.TabStop = false;
            // 
            // panelDialogContent
            // 
            this.panelDialogContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDialogContent.Location = new System.Drawing.Point(117, 0);
            this.panelDialogContent.Name = "panelDialogContent";
            this.panelDialogContent.Size = new System.Drawing.Size(470, 217);
            this.panelDialogContent.TabIndex = 2;
            // 
            // btnIgnore
            // 
            this.btnIgnore.Location = new System.Drawing.Point(13, 8);
            this.btnIgnore.Name = "btnIgnore";
            this.btnIgnore.Size = new System.Drawing.Size(75, 23);
            this.btnIgnore.TabIndex = 6;
            this.btnIgnore.Text = "Ignore";
            this.btnIgnore.UseVisualStyleBackColor = true;
            this.btnIgnore.Click += new System.EventHandler(this.btnIgnore_Click);
            // 
            // StyledMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 255);
            this.Controls.Add(this.panelDialogContent);
            this.Controls.Add(this.panelImage);
            this.Controls.Add(this.panelDialogButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StyledMessageBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StyledMessageBox";
            this.panelDialogButtons.ResumeLayout(false);
            this.flpDialogButtons.ResumeLayout(false);
            this.panelImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelDialogButtons;
        private System.Windows.Forms.FlowLayoutPanel flpDialogButtons;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.Button btnRetry;
        private System.Windows.Forms.Panel panelImage;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Panel panelDialogContent;
        private System.Windows.Forms.Button btnIgnore;
    }
}