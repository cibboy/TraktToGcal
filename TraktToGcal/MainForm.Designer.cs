namespace TraktToGcal {
    partial class MainForm {
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
            this.DatePicker = new System.Windows.Forms.DateTimePicker();
            this.DaysTextBox = new System.Windows.Forms.NumericUpDown();
            this.ProceedButton = new System.Windows.Forms.Button();
            this.ExcludeTextBox = new System.Windows.Forms.TextBox();
            this.CalendarCombo = new System.Windows.Forms.ComboBox();
            this.SearchButton = new System.Windows.Forms.Button();
            this.TraktGroupBox = new System.Windows.Forms.GroupBox();
            this.IncludeSpecialsCheck = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.GcalGroupBox = new System.Windows.Forms.GroupBox();
            this.AllDayCheck = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusProgress = new System.Windows.Forms.ToolStripProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.DaysTextBox)).BeginInit();
            this.TraktGroupBox.SuspendLayout();
            this.GcalGroupBox.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DatePicker
            // 
            this.DatePicker.Location = new System.Drawing.Point(96, 19);
            this.DatePicker.Name = "DatePicker";
            this.DatePicker.Size = new System.Drawing.Size(200, 20);
            this.DatePicker.TabIndex = 0;
            // 
            // DaysTextBox
            // 
            this.DaysTextBox.Location = new System.Drawing.Point(96, 45);
            this.DaysTextBox.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.DaysTextBox.Name = "DaysTextBox";
            this.DaysTextBox.Size = new System.Drawing.Size(84, 20);
            this.DaysTextBox.TabIndex = 1;
            // 
            // ProceedButton
            // 
            this.ProceedButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ProceedButton.Location = new System.Drawing.Point(279, 262);
            this.ProceedButton.Name = "ProceedButton";
            this.ProceedButton.Size = new System.Drawing.Size(75, 23);
            this.ProceedButton.TabIndex = 7;
            this.ProceedButton.Text = "Proceed";
            this.ProceedButton.UseVisualStyleBackColor = true;
            this.ProceedButton.Click += new System.EventHandler(this.ProceedButton_Click);
            // 
            // ExcludeTextBox
            // 
            this.ExcludeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ExcludeTextBox.Location = new System.Drawing.Point(96, 71);
            this.ExcludeTextBox.Multiline = true;
            this.ExcludeTextBox.Name = "ExcludeTextBox";
            this.ExcludeTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ExcludeTextBox.Size = new System.Drawing.Size(246, 61);
            this.ExcludeTextBox.TabIndex = 2;
            // 
            // CalendarCombo
            // 
            this.CalendarCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CalendarCombo.FormattingEnabled = true;
            this.CalendarCombo.Location = new System.Drawing.Point(96, 27);
            this.CalendarCombo.Name = "CalendarCombo";
            this.CalendarCombo.Size = new System.Drawing.Size(165, 21);
            this.CalendarCombo.TabIndex = 4;
            // 
            // SearchButton
            // 
            this.SearchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchButton.Location = new System.Drawing.Point(267, 25);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(75, 23);
            this.SearchButton.TabIndex = 5;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // TraktGroupBox
            // 
            this.TraktGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TraktGroupBox.Controls.Add(this.IncludeSpecialsCheck);
            this.TraktGroupBox.Controls.Add(this.label3);
            this.TraktGroupBox.Controls.Add(this.label2);
            this.TraktGroupBox.Controls.Add(this.label1);
            this.TraktGroupBox.Controls.Add(this.DatePicker);
            this.TraktGroupBox.Controls.Add(this.DaysTextBox);
            this.TraktGroupBox.Controls.Add(this.ExcludeTextBox);
            this.TraktGroupBox.Location = new System.Drawing.Point(12, 12);
            this.TraktGroupBox.Name = "TraktGroupBox";
            this.TraktGroupBox.Size = new System.Drawing.Size(348, 161);
            this.TraktGroupBox.TabIndex = 8;
            this.TraktGroupBox.TabStop = false;
            this.TraktGroupBox.Text = "Trakt";
            // 
            // IncludeSpecialsCheck
            // 
            this.IncludeSpecialsCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.IncludeSpecialsCheck.AutoSize = true;
            this.IncludeSpecialsCheck.Location = new System.Drawing.Point(96, 138);
            this.IncludeSpecialsCheck.Name = "IncludeSpecialsCheck";
            this.IncludeSpecialsCheck.Size = new System.Drawing.Size(104, 17);
            this.IncludeSpecialsCheck.TabIndex = 3;
            this.IncludeSpecialsCheck.Text = "Include Specials";
            this.IncludeSpecialsCheck.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Exclusions:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "# days:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Starting on:";
            // 
            // GcalGroupBox
            // 
            this.GcalGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GcalGroupBox.Controls.Add(this.AllDayCheck);
            this.GcalGroupBox.Controls.Add(this.label5);
            this.GcalGroupBox.Controls.Add(this.SearchButton);
            this.GcalGroupBox.Controls.Add(this.CalendarCombo);
            this.GcalGroupBox.Location = new System.Drawing.Point(12, 179);
            this.GcalGroupBox.Name = "GcalGroupBox";
            this.GcalGroupBox.Size = new System.Drawing.Size(348, 77);
            this.GcalGroupBox.TabIndex = 9;
            this.GcalGroupBox.TabStop = false;
            this.GcalGroupBox.Text = "Google Calendar";
            // 
            // AllDayCheck
            // 
            this.AllDayCheck.AutoSize = true;
            this.AllDayCheck.Location = new System.Drawing.Point(96, 54);
            this.AllDayCheck.Name = "AllDayCheck";
            this.AllDayCheck.Size = new System.Drawing.Size(125, 17);
            this.AllDayCheck.TabIndex = 6;
            this.AllDayCheck.Text = "Create all-day events";
            this.AllDayCheck.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Calendar:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel,
            this.toolStripStatusLabel2,
            this.StatusProgress});
            this.statusStrip1.Location = new System.Drawing.Point(0, 295);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(372, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(42, 17);
            this.StatusLabel.Text = "Ready.";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(315, 17);
            this.toolStripStatusLabel2.Spring = true;
            // 
            // StatusProgress
            // 
            this.StatusProgress.Name = "StatusProgress";
            this.StatusProgress.Size = new System.Drawing.Size(100, 16);
            this.StatusProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.StatusProgress.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 317);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.GcalGroupBox);
            this.Controls.Add(this.TraktGroupBox);
            this.Controls.Add(this.ProceedButton);
            this.MinimumSize = new System.Drawing.Size(344, 299);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trakt To Google Calendar";
            ((System.ComponentModel.ISupportInitialize)(this.DaysTextBox)).EndInit();
            this.TraktGroupBox.ResumeLayout(false);
            this.TraktGroupBox.PerformLayout();
            this.GcalGroupBox.ResumeLayout(false);
            this.GcalGroupBox.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker DatePicker;
        private System.Windows.Forms.NumericUpDown DaysTextBox;
        private System.Windows.Forms.Button ProceedButton;
        private System.Windows.Forms.TextBox ExcludeTextBox;
        private System.Windows.Forms.ComboBox CalendarCombo;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.GroupBox TraktGroupBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox GcalGroupBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripProgressBar StatusProgress;
        private System.Windows.Forms.CheckBox IncludeSpecialsCheck;
        private System.Windows.Forms.CheckBox AllDayCheck;
    }
}

