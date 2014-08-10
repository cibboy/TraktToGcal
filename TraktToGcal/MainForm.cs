using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TraktToGcal.Google;
using TraktToGcal.Trakt;

namespace TraktToGcal {
    public partial class MainForm : Form {
        public MainForm() {
            Bitmap b = new Bitmap("cal.png");
            this.Icon = Icon.FromHandle(b.GetHicon());

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
            if (Properties.Settings.Default.DefaultLookaheadDays < 1)
                DaysTextBox.Value = 7;
            else
                DaysTextBox.Value = Properties.Settings.Default.DefaultLookaheadDays;

            // Exclusions property.
            ExcludeTextBox.Text = Properties.Settings.Default.Exclusions.Replace(";", Environment.NewLine);

            // Calendar property.
            CalendarCombo.Text = Properties.Settings.Default.CalendarName;
        }

        private async void ProceedButton_Click(object sender, EventArgs e) {
            if (CalendarCombo.Text == "") {
                MessageBox.Show("You must specify the calendar where to store the events!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // GUI operations.
            ProceedButton.Enabled = false;
            StatusLabel.Text = "Working...";
            StatusProgress.Visible = true;

            // Compute list of exclusions.
            List<string> excludes = new List<string>();
            string[] exs = ExcludeTextBox.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string ex in exs)
                excludes.Add(ex.ToLowerInvariant());

            // Load full list of episodes.
            List<Entry> episodes = await TraktAccess.GetEpisodes(DatePicker.Value, Convert.ToInt32(DaysTextBox.Value));

            // Update Google Calendar.
            await CalendarUpdate.UpdateCalendarAsync(CalendarCombo.Text.ToLowerInvariant(), episodes, excludes);

            // GUI operations.
            ProceedButton.Enabled = true;
            StatusLabel.Text = "Done.";
            StatusProgress.Visible = false;
        }

        private async void SearchButton_Click(object sender, EventArgs e) {
            // GUI operations.
            SearchButton.Enabled = false;
            StatusLabel.Text = "Working...";
            StatusProgress.Visible = true;

            // Get list of calendars.
            List<string> calendars = await CalendarUpdate.GetListOfCalendarsAsync();

            // Add list to combo box.
            CalendarCombo.Items.Clear();
            CalendarCombo.Items.AddRange(calendars.ToArray());

            // GUI operations.
            SearchButton.Enabled = true;
            StatusLabel.Text = "Done.";
            StatusProgress.Visible = false;
        }
    }
}
