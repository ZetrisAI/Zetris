using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Zetris {
    public partial class Dialog : Form {
        public Dialog() {
            InitializeComponent();
        }

        public int desiredX = 4, desiredR = 0;
        public bool desiredHold = false;

        private void numericUpDown1_ValueChanged(object sender, EventArgs e) {
            desiredX = Convert.ToInt32(numericUpDown1.Value);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            desiredHold = checkBox1.Checked;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e) {

            desiredR = Convert.ToInt32(numericUpDown2.Value);
        }
    }
}
