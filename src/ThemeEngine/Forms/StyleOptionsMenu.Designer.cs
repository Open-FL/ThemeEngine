namespace ThemeEngine.Forms
{
    partial class StyleOptionsMenu
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
            this.dgvOptions = new System.Windows.Forms.DataGridView();
            this.OptionName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OptionValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOptions)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvOptions
            // 
            this.dgvOptions.AllowUserToAddRows = false;
            this.dgvOptions.AllowUserToDeleteRows = false;
            this.dgvOptions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOptions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOptions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OptionName,
            this.OptionValue});
            this.dgvOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOptions.Location = new System.Drawing.Point(0, 0);
            this.dgvOptions.MultiSelect = false;
            this.dgvOptions.Name = "dgvOptions";
            this.dgvOptions.Size = new System.Drawing.Size(690, 490);
            this.dgvOptions.TabIndex = 0;
            // 
            // OptionName
            // 
            this.OptionName.HeaderText = "Option Name";
            this.OptionName.Name = "OptionName";
            this.OptionName.ReadOnly = true;
            // 
            // OptionValue
            // 
            this.OptionValue.HeaderText = "Option Value";
            this.OptionValue.Name = "OptionValue";
            // 
            // StyleOptionsMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 490);
            this.Controls.Add(this.dgvOptions);
            this.Name = "StyleOptionsMenu";
            this.Text = "StyleOptionsMenu";
            ((System.ComponentModel.ISupportInitialize)(this.dgvOptions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvOptions;
        private System.Windows.Forms.DataGridViewTextBoxColumn OptionName;
        private System.Windows.Forms.DataGridViewTextBoxColumn OptionValue;
    }
}