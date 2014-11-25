using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TraktToGcal.Google;
using TraktToGcal.Trakt;

namespace TraktToGcal {
    public partial class Settings : Form {
        private DefaultProperties properties;
        private Credentials credentials;

        public Settings() {
            // Load default settings and credentials.
            properties = DefaultProperties.Load();
            credentials = Credentials.Load();

            InitializeComponent();
            CustomInitialize();
        }

        private void CustomInitialize() {
            // Days lookahead property.
            if (properties.LookaheadDays < 1)
                DaysTextBox.Value = 7;
            else
                DaysTextBox.Value = properties.LookaheadDays;

            // Exclusions property.
            ExcludeTextBox.Text = "";
            foreach(string s in properties.Exclusions)
                ExcludeTextBox.Text += s + Environment.NewLine;

            // Include specials property.
            IncludeSpecialsCheck.Checked = properties.IncludeSpecials;

            // Calendar property.
            if (properties.CalendarName != "") {
                CalendarCombo.Items.Add(properties.CalendarName);
                CalendarCombo.Text = properties.CalendarName;
            }

            // All-day events property.
            AllDayCheck.Checked = properties.CreateAllDayEvents;

            // Credential properties.
            GoogleUserTextbox.Text = credentials.GoogleUser;
            TraktUserTextbox.Text = credentials.TraktUser;
            TraktAPIKeyTextbox.Text = credentials.TraktApiKey;
        }

        private async void SearchButton_Click(object sender, EventArgs e) {
            try {
                // GUI operations.
                SearchButton.Enabled = false;
                SaveButton.Enabled = false;
                CancelButton.Enabled = false;

                // Set google user in credentials, since it's going to be used for retrieval of calendar list.
                credentials.GoogleUser = GoogleUserTextbox.Text;
                // Get list of calendars.
                List<string> calendars = await CalendarUpdate.GetListOfCalendarsAsync(credentials);

                // Add list to combo box.
                CalendarCombo.Items.Clear();
                CalendarCombo.Items.AddRange(calendars.ToArray());

                // GUI operations.
                SearchButton.Enabled = true;
                SaveButton.Enabled = true;
                CancelButton.Enabled = true;
            }
            catch (Exception ex) {
                ErrorDialog.Show("There's been an error:" + Environment.NewLine, "Error", ex);

                // GUI operations.
                SearchButton.Enabled = true;
                SaveButton.Enabled = true;
                CancelButton.Enabled = true;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e) {
            // Days lookahead property.
            properties.LookaheadDays = (int)DaysTextBox.Value;

            // Exclusions property.
            properties.Exclusions.Clear();
            foreach (string s in ExcludeTextBox.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None))
                properties.Exclusions.Add(s);

            // Include specials property.
            properties.IncludeSpecials = IncludeSpecialsCheck.Checked;

            // Calendar property.
            properties.CalendarName = CalendarCombo.Text;

            // All-day events property.
            properties.CreateAllDayEvents = AllDayCheck.Checked;

            // Credential properties.
            credentials.GoogleUser = GoogleUserTextbox.Text;
            credentials.TraktUser = TraktUserTextbox.Text;
            credentials.TraktApiKey = TraktAPIKeyTextbox.Text;
            
            // Update hash only if update password is selected.
            if (UpdatePwdCheckbox.Enabled) {
                // Blank hash if password is not specified.
                if (TraktPwdTextbox.Text == "")
                    credentials.TraktHash = "";
                // Compute hash otherwise.
                else
                    credentials.TraktHash = Credentials.ComputeHash(credentials.TraktUser, TraktPwdTextbox.Text);
            }

            properties.Save();
            credentials.Save();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void UpdatePwdCheckbox_CheckedChanged(object sender, EventArgs e) {
            TraktPwdTextbox.Enabled = UpdatePwdCheckbox.Enabled;
        }
    }
}
