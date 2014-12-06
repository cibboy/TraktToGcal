namespace TraktToGcal {
    partial class Settings {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.DaysTextBox = new System.Windows.Forms.NumericUpDown();
            this.SaveButton = new System.Windows.Forms.Button();
            this.ExcludeTextBox = new System.Windows.Forms.TextBox();
            this.CalendarCombo = new System.Windows.Forms.ComboBox();
            this.SearchButton = new System.Windows.Forms.Button();
            this.TraktGroupBox = new System.Windows.Forms.GroupBox();
            this.IncludeSpecialsCheck = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.GcalGroupBox = new System.Windows.Forms.GroupBox();
            this.AllDayCheck = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TraktPwdTextbox = new System.Windows.Forms.TextBox();
            this.UpdatePwdCheckbox = new System.Windows.Forms.CheckBox();
            this.TraktAPIKeyTextbox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TraktUserTextbox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.GoogleUserTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DaysTextBox)).BeginInit();
            this.TraktGroupBox.SuspendLayout();
            this.GcalGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DaysTextBox
            // 
            this.DaysTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DaysTextBox.Location = new System.Drawing.Point(96, 19);
            this.DaysTextBox.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.DaysTextBox.Name = "DaysTextBox";
            this.DaysTextBox.Size = new System.Drawing.Size(84, 20);
            this.DaysTextBox.TabIndex = 1;
            this.DaysTextBox.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.Location = new System.Drawing.Point(204, 382);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 12;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // ExcludeTextBox
            // 
            this.ExcludeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ExcludeTextBox.Location = new System.Drawing.Point(96, 45);
            this.ExcludeTextBox.Multiline = true;
            this.ExcludeTextBox.Name = "ExcludeTextBox";
            this.ExcludeTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ExcludeTextBox.Size = new System.Drawing.Size(246, 63);
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
            this.TraktGroupBox.Controls.Add(this.DaysTextBox);
            this.TraktGroupBox.Controls.Add(this.ExcludeTextBox);
            this.TraktGroupBox.Location = new System.Drawing.Point(12, 12);
            this.TraktGroupBox.Name = "TraktGroupBox";
            this.TraktGroupBox.Size = new System.Drawing.Size(348, 138);
            this.TraktGroupBox.TabIndex = 8;
            this.TraktGroupBox.TabStop = false;
            this.TraktGroupBox.Text = "Trakt";
            // 
            // IncludeSpecialsCheck
            // 
            this.IncludeSpecialsCheck.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IncludeSpecialsCheck.AutoSize = true;
            this.IncludeSpecialsCheck.Location = new System.Drawing.Point(96, 114);
            this.IncludeSpecialsCheck.Name = "IncludeSpecialsCheck";
            this.IncludeSpecialsCheck.Size = new System.Drawing.Size(104, 17);
            this.IncludeSpecialsCheck.TabIndex = 3;
            this.IncludeSpecialsCheck.Text = "Include Specials";
            this.IncludeSpecialsCheck.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 101;
            this.label3.Text = "Exclusions:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 100;
            this.label2.Text = "# days:";
            // 
            // GcalGroupBox
            // 
            this.GcalGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GcalGroupBox.Controls.Add(this.AllDayCheck);
            this.GcalGroupBox.Controls.Add(this.label5);
            this.GcalGroupBox.Controls.Add(this.SearchButton);
            this.GcalGroupBox.Controls.Add(this.CalendarCombo);
            this.GcalGroupBox.Location = new System.Drawing.Point(12, 156);
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
            this.label5.TabIndex = 102;
            this.label5.Text = "Calendar:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.TraktPwdTextbox);
            this.groupBox1.Controls.Add(this.UpdatePwdCheckbox);
            this.groupBox1.Controls.Add(this.TraktAPIKeyTextbox);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.TraktUserTextbox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.GoogleUserTextbox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 239);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(348, 135);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Security";
            // 
            // TraktPwdTextbox
            // 
            this.TraktPwdTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TraktPwdTextbox.Enabled = false;
            this.TraktPwdTextbox.Location = new System.Drawing.Point(96, 79);
            this.TraktPwdTextbox.Name = "TraktPwdTextbox";
            this.TraktPwdTextbox.PasswordChar = '*';
            this.TraktPwdTextbox.Size = new System.Drawing.Size(179, 20);
            this.TraktPwdTextbox.TabIndex = 9;
            // 
            // UpdatePwdCheckbox
            // 
            this.UpdatePwdCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UpdatePwdCheckbox.AutoSize = true;
            this.UpdatePwdCheckbox.Location = new System.Drawing.Point(281, 81);
            this.UpdatePwdCheckbox.Name = "UpdatePwdCheckbox";
            this.UpdatePwdCheckbox.Size = new System.Drawing.Size(61, 17);
            this.UpdatePwdCheckbox.TabIndex = 10;
            this.UpdatePwdCheckbox.Text = "Update";
            this.UpdatePwdCheckbox.UseVisualStyleBackColor = true;
            this.UpdatePwdCheckbox.CheckedChanged += new System.EventHandler(this.UpdatePwdCheckbox_CheckedChanged);
            // 
            // TraktAPIKeyTextbox
            // 
            this.TraktAPIKeyTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TraktAPIKeyTextbox.Location = new System.Drawing.Point(96, 105);
            this.TraktAPIKeyTextbox.Name = "TraktAPIKeyTextbox";
            this.TraktAPIKeyTextbox.Size = new System.Drawing.Size(246, 20);
            this.TraktAPIKeyTextbox.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 106;
            this.label7.Text = "Trakt API key:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 105;
            this.label6.Text = "Trakt password:";
            // 
            // TraktUserTextbox
            // 
            this.TraktUserTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TraktUserTextbox.Location = new System.Drawing.Point(96, 53);
            this.TraktUserTextbox.Name = "TraktUserTextbox";
            this.TraktUserTextbox.Size = new System.Drawing.Size(246, 20);
            this.TraktUserTextbox.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 104;
            this.label4.Text = "Trakt user:";
            // 
            // GoogleUserTextbox
            // 
            this.GoogleUserTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GoogleUserTextbox.Location = new System.Drawing.Point(96, 27);
            this.GoogleUserTextbox.Name = "GoogleUserTextbox";
            this.GoogleUserTextbox.Size = new System.Drawing.Size(246, 20);
            this.GoogleUserTextbox.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 103;
            this.label1.Text = "Google user:";
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(285, 382);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 13;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 417);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.GcalGroupBox);
            this.Controls.Add(this.TraktGroupBox);
            this.Controls.Add(this.SaveButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(344, 299);
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.DaysTextBox)).EndInit();
            this.TraktGroupBox.ResumeLayout(false);
            this.TraktGroupBox.PerformLayout();
            this.GcalGroupBox.ResumeLayout(false);
            this.GcalGroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown DaysTextBox;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.TextBox ExcludeTextBox;
        private System.Windows.Forms.ComboBox CalendarCombo;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.GroupBox TraktGroupBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox GcalGroupBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox IncludeSpecialsCheck;
        private System.Windows.Forms.CheckBox AllDayCheck;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TraktUserTextbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox GoogleUserTextbox;
        private System.Windows.Forms.TextBox TraktAPIKeyTextbox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.CheckBox UpdatePwdCheckbox;
        private System.Windows.Forms.TextBox TraktPwdTextbox;
    }
}

