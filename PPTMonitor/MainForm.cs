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

        int startingRating, currentRating;
        static int[] score = new int[4] {0, 0, 0, 0};

        static VAMemory PPT = new VAMemory("puyopuyotetris");

        private void ScanTimer_Tick(object sender, EventArgs e) {
            currentRating = PPT.ReadInt32(new IntPtr(0x140599FF0));
            valueCurrentRating.Text = currentRating.ToString();
            valueRatingDifference.Text = (currentRating - startingRating).ToString();

            int scoreAddress = PPT.ReadInt32(new IntPtr(0x14057F048)) + 0x38;
            value4PScore1.Text = value2PScore1.Text = PPT.ReadInt32(new IntPtr(scoreAddress)).ToString();
            value4PScore2.Text = value2PScore2.Text = PPT.ReadInt32(new IntPtr(scoreAddress + 0x4)).ToString();
            value4PScore3.Text                      = PPT.ReadInt32(new IntPtr(scoreAddress + 0x8)).ToString();
            value4PScore4.Text                      = PPT.ReadInt32(new IntPtr(scoreAddress + 0xC)).ToString();

            int playerAddress = PPT.ReadInt32(new IntPtr(PPT.ReadInt32(new IntPtr(0x140473760)) + 0x20)) + 0x108;
            value4PPlayer1Rating.Text = value2PPlayer1Rating.Text = PPT.ReadInt16(new IntPtr(playerAddress)).ToString();
            value4PPlayer2Rating.Text = value2PPlayer2Rating.Text = PPT.ReadInt16(new IntPtr(playerAddress) + 0x50).ToString();
            value4PPlayer3Rating.Text                             = PPT.ReadInt16(new IntPtr(playerAddress) + 0xA0).ToString();
            value4PPlayer4Rating.Text                             = PPT.ReadInt16(new IntPtr(playerAddress) + 0xF0).ToString();
        }

        private void buttonResetPuzzle_Click(object sender, EventArgs e) {
            startingRating = currentRating;

            valueStartingRating.Text = startingRating.ToString();
            valueCurrentRating.Text = currentRating.ToString();
            valueRatingDifference.Text = (currentRating - startingRating).ToString();
        }

        private void MainForm_Load(object sender, EventArgs e) {
            ScanTimer_Tick(null, EventArgs.Empty);
            buttonResetPuzzle_Click(null, EventArgs.Empty);
        }

        private void buttonRehook_Click(object sender, EventArgs e) {
            PPT = new VAMemory("puyopuyotetris");
            
        }
    }
}