using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TraktToGcal {
    public partial class ErrorDialog : Form {
        private bool detailsOpen;

        private ErrorDialog() {
            detailsOpen = false;

            InitializeComponent();
        }

        public static void Show(string preText, string caption, Exception e) {
            ErrorDialog d = new ErrorDialog();

            d.Text = caption;
            d.MainText.Text = preText + e.Message;
            d.Image.Image = SystemIcons.Error.ToBitmap();
            d.DetailsText.Text = "-- Error Message --" + Environment.NewLine + e.Message + Environment.NewLine + Environment.NewLine + "-- StackTrace --" + Environment.NewLine + e.StackTrace;

            d.ShowDialog();
        }

        private void DetailsButton_Click(object sender, EventArgs e) {
            if (detailsOpen) {
                DetailsText.Visible = false;
                this.Height -= 100;
                DetailsButton.Text = "Details >>";
            }
            else {
                this.Height += 100;
                DetailsText.Visible = true;
                DetailsButton.Text = "<< Details";
            }

            detailsOpen = !detailsOpen;
        }
    }
}
