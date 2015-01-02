using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TraktToGcal.Trakt {
    public partial class AuthCodeInputDialog : Form {
        public AuthCodeInputDialog() {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
