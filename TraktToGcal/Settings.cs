using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TraktToGcal.Google;
using TraktToGcal.Trakt;

namespace TraktToGcal {
    public partial class Settings : Form {
        private DefaultProperties properties;
        private TraktAuthorization traktAuth;

        public Settings() {
            // Load default settings and credentials.
            properties = DefaultProperties.Load();
            traktAuth = TraktAuthorization.Load();

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
            GoogleUserTextbox.Text = properties.GoogleUser;
            TraktUserTextbox.Text = properties.TraktUser;
            TraktClientIDTextbox.Text = traktAuth.ClientID;
            TraktClientSecretTextbox.Text = traktAuth.ClientSecret;
        }

        private async void SearchButton_Click(object sender, EventArgs e) {
            try {
                // GUI operations.
                SearchButton.Enabled = false;
                SaveButton.Enabled = false;
                CancelButton.Enabled = false;

                // Set google user in credentials, since it's going to be used for retrieval of calendar list.
                properties.GoogleUser = GoogleUserTextbox.Text;
                // Get list of calendars.
                List<string> calendars = await CalendarUpdate.GetListOfCalendarsAsync(properties);

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
            foreach (string s in ExcludeTextBox.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
                properties.Exclusions.Add(s);

            // Include specials property.
            properties.IncludeSpecials = IncludeSpecialsCheck.Checked;

            // Calendar property.
            properties.CalendarName = CalendarCombo.Text;

            // All-day events property.
            properties.CreateAllDayEvents = AllDayCheck.Checked;

            // Credential properties.
            properties.GoogleUser = GoogleUserTextbox.Text;
            properties.TraktUser = TraktUserTextbox.Text;
            traktAuth.ClientID = TraktClientIDTextbox.Text;
            traktAuth.ClientSecret = TraktClientSecretTextbox.Text;

            properties.Save();
            traktAuth.Save();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void ResetTraktAuthButton_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Are you sure you want to reset your trakt authorization codes? The operation is not reversible and future requests will require a new authorization!",
                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                traktAuth.Authorization = "";
                traktAuth.AccessToken = "";
                traktAuth.RefreshToken = "";
                traktAuth.Expiration = DateTime.UtcNow;

                traktAuth.Save();

                MessageBox.Show("Reset complete.", "Reset complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
