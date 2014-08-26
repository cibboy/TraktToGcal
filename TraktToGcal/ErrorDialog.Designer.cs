namespace TraktToGcal {
    partial class ErrorDialog {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.Image = new System.Windows.Forms.PictureBox();
            this.MainText = new System.Windows.Forms.Label();
            this.DetailsButton = new System.Windows.Forms.Button();
            this.DetailsText = new System.Windows.Forms.TextBox();
            this.OKButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Image)).BeginInit();
            this.SuspendLayout();
            // 
            // Image
            // 
            this.Image.Location = new System.Drawing.Point(12, 12);
            this.Image.Name = "Image";
            this.Image.Size = new System.Drawing.Size(32, 32);
            this.Image.TabIndex = 0;
            this.Image.TabStop = false;
            // 
            // MainText
            // 
            this.MainText.AutoEllipsis = true;
            this.MainText.Location = new System.Drawing.Point(78, 12);
            this.MainText.Name = "MainText";
            this.MainText.Size = new System.Drawing.Size(308, 49);
            this.MainText.TabIndex = 1;
            // 
            // DetailsButton
            // 
            this.DetailsButton.Location = new System.Drawing.Point(12, 64);
            this.DetailsButton.Name = "DetailsButton";
            this.DetailsButton.Size = new System.Drawing.Size(75, 23);
            this.DetailsButton.TabIndex = 3;
            this.DetailsButton.Text = "<< Details";
            this.DetailsButton.UseVisualStyleBackColor = true;
            this.DetailsButton.Click += new System.EventHandler(this.DetailsButton_Click);
            // 
            // DetailsText
            // 
            this.DetailsText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DetailsText.BackColor = System.Drawing.SystemColors.Control;
            this.DetailsText.Location = new System.Drawing.Point(12, 93);
            this.DetailsText.Multiline = true;
            this.DetailsText.Name = "DetailsText";
            this.DetailsText.ReadOnly = true;
            this.DetailsText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DetailsText.Size = new System.Drawing.Size(374, 0);
            this.DetailsText.TabIndex = 4;
            this.DetailsText.Visible = false;
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.OKButton.Location = new System.Drawing.Point(311, 64);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 5;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            // 
            // ErrorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.OKButton;
            this.ClientSize = new System.Drawing.Size(398, 100);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.DetailsText);
            this.Controls.Add(this.DetailsButton);
            this.Controls.Add(this.MainText);
            this.Controls.Add(this.Image);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ErrorDialog";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MessageDialog";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.Image)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Image;
        private System.Windows.Forms.Label MainText;
        private System.Windows.Forms.Button DetailsButton;
        private System.Windows.Forms.TextBox DetailsText;
        private System.Windows.Forms.Button OKButton;
    }
}