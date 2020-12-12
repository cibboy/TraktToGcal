using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using TraktToGcal.Google;
using TraktToGcal.Trakt;

namespace TraktToGcal {
    public partial class MainForm : Form {
        private DefaultProperties properties;

        public MainForm() {
            // If default settings are missing, immediately show settings page.
            if (!Directory.Exists("settings") || !File.Exists("settings/settings.json") || !File.Exists("settings/credentials.json"))
                new Settings().ShowDialog();

            // Load default settings and credentials.
            properties = DefaultProperties.Load();

            InitializeComponent();
            CustomInitialize();
        }

        private void CustomInitialize() {
            // Compute date for following week.
            DateTime now = DateTime.Now;
            int d = (int)now.DayOfWeek;
            d = (8 - d) % 8;
            // Sunday is day 0, so we need to take care of it and map it onto Monday...
            if (d == 0)
                d++;
            now = now.AddDays(d);
            DatePicker.Value = now;

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

            // Movie property.
            MovieReleaseCheck.Checked = !properties.MovieDvdReleases;
            DvdReleaseCheck.Checked = properties.MovieDvdReleases;

            // Calendar property.
            if (properties.CalendarName != "") {
                CalendarCombo.Items.Add(properties.CalendarName);
                CalendarCombo.Text = properties.CalendarName;
            }

            // All-day events property.
            AllDayCheck.Checked = properties.CreateAllDayEvents;
        }

        private async void ProceedButton_Click(object sender, EventArgs e) {
            try {
                if (CalendarCombo.Text == "") {
                    MessageBox.Show("You must specify the calendar where to store the events!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // GUI operations.
                ProceedButton.Enabled = false;
                SearchButton.Enabled = false;
                StatusLabel.Text = "Working...";
                StatusProgress.Visible = true;

                // Compute list of exclusions.
                List<string> excludes = new List<string>();
                string[] exs = ExcludeTextBox.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string ex in exs)
                    excludes.Add(ex.ToLowerInvariant());

                // Get movie release type.
                bool dvd = DvdReleaseCheck.Checked;

                // Load full list of episodes.
                List<EpisodeEntry> episodes = await TraktAccess.GetEpisodes(properties, DatePicker.Value, Convert.ToInt32(DaysTextBox.Value));
                // Load full list of movies.
                List<MovieEntry> movies = await TraktAccess.GetMovies(properties, DatePicker.Value, Convert.ToInt32(DaysTextBox.Value), dvd);

                // Update Google Calendar.
                await CalendarUpdate.UpdateCalendarAsync(properties, CalendarCombo.Text.ToLowerInvariant(), episodes, excludes, IncludeSpecialsCheck.Checked, AllDayCheck.Checked);
                await CalendarUpdate.UpdateCalendarAsync(properties, CalendarCombo.Text.ToLowerInvariant(), movies);

                // GUI operations.
                ProceedButton.Enabled = true;
                SearchButton.Enabled = true;
                StatusLabel.Text = "Done.";
                StatusProgress.Visible = false;
            }
            catch (Exception ex) {
                ErrorDialog.Show("There's been an error:" + Environment.NewLine, "Error", ex);

                // GUI operations.
                ProceedButton.Enabled = true;
                SearchButton.Enabled = true;
                StatusLabel.Text = "There's been an error.";
                StatusProgress.Visible = false;
            }
        }

        private async void SearchButton_Click(object sender, EventArgs e) {
            try {
                // GUI operations.
                SearchButton.Enabled = false;
                ProceedButton.Enabled = false;
                StatusLabel.Text = "Working...";
                StatusProgress.Visible = true;

                // Get list of calendars.
                List<string> calendars = await CalendarUpdate.GetListOfCalendarsAsync(properties);

                // Add list to combo box.
                CalendarCombo.Items.Clear();
                CalendarCombo.Items.AddRange(calendars.ToArray());

                // GUI operations.
                SearchButton.Enabled = true;
                ProceedButton.Enabled = true;
                StatusLabel.Text = "Done.";
                StatusProgress.Visible = false;
            }
            catch (Exception ex) {
                ErrorDialog.Show("There's been an error:" + Environment.NewLine, "Error", ex);

                // GUI operations.
                SearchButton.Enabled = true;
                ProceedButton.Enabled = true;
                StatusLabel.Text = "There's been an error.";
                StatusProgress.Visible = false;
            }
        }

        private void SettingsButton_Click(object sender, EventArgs e) {
            if (new Settings().ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                // Reload properties for updated values (as of now, not really useful).
                properties = DefaultProperties.Load();
            }
        }

        public static async Task<bool> RunSilentAsync() {
            DefaultProperties properties = DefaultProperties.Load();

            try {
                // Compute date for following week.
                DateTime now = DateTime.Now;
                int d = (int)now.DayOfWeek;
                d = (8 - d) % 8;
                // Sunday is day 0, so we need to take care of it and map it onto Monday...
                if (d == 0)
                    d++;
                now = now.AddDays(d);

                // Load full list of episodes.
                List<EpisodeEntry> episodes = await TraktAccess.GetEpisodes(properties, now, properties.LookaheadDays);
                // Load full list of movies.
                List<MovieEntry> movies = await TraktAccess.GetMovies(properties, now, properties.LookaheadDays, properties.MovieDvdReleases);

                for (int i = 0; i < properties.Exclusions.Count; i++)
                    properties.Exclusions[i] = properties.Exclusions[i].ToLowerInvariant();

                // Update Google Calendar.
                await CalendarUpdate.UpdateCalendarAsync(properties, properties.CalendarName.ToLowerInvariant(), episodes, properties.Exclusions, properties.IncludeSpecials, properties.CreateAllDayEvents);
                await CalendarUpdate.UpdateCalendarAsync(properties, properties.CalendarName.ToLowerInvariant(), movies);

                return true;
            }
            catch (Exception ex) {
                Console.WriteLine("There's been an error: " + ex.Message);
                return false;
            }
        }
    }
}
