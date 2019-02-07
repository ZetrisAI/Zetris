using System;
using System.Windows.Forms;

namespace PPT_TAS {
    public partial class Dialog : Form {
        public Dialog() {
            InitializeComponent();
        }

        public int desiredX = 4, desiredR = 0;
        public bool desiredHold = false;

        private void valueX_ValueChanged(object sender, EventArgs e) {
            desiredX = Convert.ToInt32(valueX.Value);
        }

        private void valueR_ValueChanged(object sender, EventArgs e) {
            desiredR = Convert.ToInt32(valueR.Value);
        }

        private void valueHold_CheckedChanged(object sender, EventArgs e) {
            desiredHold = valueHold.Checked;
        }
    }
}
