using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PPTMonitor {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }

        public static VAMemory PPT = new VAMemory("puyopuyotetris");

        private void ScanTimer_Tick(object sender, EventArgs e) {
            valueCurrentRating.Text = PPT.ReadInt32(new IntPtr(0x140599FF0)).ToString();

            int score = PPT.ReadInt32(new IntPtr(0x14057F048)) + 0x38;
            value4PScore1.Text = value2PScore1.Text = PPT.ReadInt32(new IntPtr(score)).ToString();
            value4PScore2.Text = value2PScore2.Text = PPT.ReadInt32(new IntPtr(score + 4)).ToString();
            value4PScore3.Text                      = PPT.ReadInt32(new IntPtr(score + 8)).ToString();
            value4PScore4.Text                      = PPT.ReadInt32(new IntPtr(score + 12)).ToString();

            int player = PPT.ReadInt32(new IntPtr(PPT.ReadInt32(new IntPtr(0x140473760)) + 0x20)) + 0x108;
            value4PPlayer1Rating.Text = value2PPlayer1Rating.Text = PPT.ReadInt16(new IntPtr(player)).ToString();
            value4PPlayer2Rating.Text = value2PPlayer2Rating.Text = PPT.ReadInt16(new IntPtr(player) + 0x50).ToString();
            value4PPlayer3Rating.Text                             = PPT.ReadInt16(new IntPtr(player) + 0xA0).ToString();
            value4PPlayer4Rating.Text                             = PPT.ReadInt16(new IntPtr(player) + 0xF0).ToString();
        }

        private void buttonRehook_Click(object sender, EventArgs e) {
            PPT = new VAMemory("puyopuyotetris");
        }
    }
}