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

        static VAMemory PPT = new VAMemory("puyopuyotetris");

        static int startingRating, currentRating, wins = 0, losses = 0;

        static int[] score = new int[4] {0, 0, 0, 0};
        static int[] sets = new int[4] {0, 0, 0, 0};
        static int[] total = new int[4] {0, 0, 0, 0};
        static int maxscore = 2;

        private void ScanTimer_Tick(object sender, EventArgs e) {
            int value = PPT.ReadInt32(new IntPtr(0x140599FF0));
            if (value > currentRating) {
                wins++;
                valueWins.Text = wins.ToString();
            } else if (value < currentRating) {
                losses++;
                valueLosses.Text = losses.ToString();
            }
            currentRating = value;

            valueCurrentRating.Text = currentRating.ToString();
            valueRatingDifference.Text = (currentRating - startingRating).ToString();

            int scoreAddress = PPT.ReadInt32(new IntPtr(0x14057F048)) + 0x38;
            maxscore = PPT.ReadInt32(new IntPtr(scoreAddress + 0x10));

            for (int i = 0; i < 4; i++) {
                value = PPT.ReadInt32(new IntPtr(scoreAddress) + i * 4);
                if (value > score[i]) {
                    total[i]++;
                    if (value >= maxscore) {
                        sets[i]++;
                    }
                }
                score[i] = value;
            }

            value4PScore1.Text = value2PScore1.Text = score[0].ToString();
            value4PScore2.Text = value2PScore2.Text = score[1].ToString();
            value4PScore3.Text                      = score[2].ToString();
            value4PScore4.Text                      = score[3].ToString();

            value4PSets1.Text = value2PSets1.Text = sets[0].ToString();
            value4PSets2.Text = value2PSets2.Text = sets[1].ToString();
            value4PSets3.Text                     = sets[2].ToString();
            value4PSets4.Text                     = sets[3].ToString();

            value4PTotal1.Text = value2PTotal1.Text = total[0].ToString();
            value4PTotal2.Text = value2PTotal2.Text = total[1].ToString();
            value4PTotal3.Text                      = total[2].ToString();
            value4PTotal4.Text                      = total[3].ToString();

            int playerAddress = PPT.ReadInt32(new IntPtr(PPT.ReadInt32(new IntPtr(0x140473760)) + 0x20)) + 0xD8;
            value4PPlayer1Rating.Text = value2PPlayer1Rating.Text = PPT.ReadInt16(new IntPtr(playerAddress) + 0x30).ToString();
            value4PPlayer2Rating.Text = value2PPlayer2Rating.Text = PPT.ReadInt16(new IntPtr(playerAddress) + 0x80).ToString();
            value4PPlayer3Rating.Text                             = PPT.ReadInt16(new IntPtr(playerAddress) + 0xD0).ToString();
            value4PPlayer4Rating.Text                             = PPT.ReadInt16(new IntPtr(playerAddress) + 0x120).ToString();

            label4PPlayer1Rating.Text = label2PPlayer1Rating.Text = PPT.ReadStringUnicode(new IntPtr(playerAddress), 0x20);
            label4PPlayer2Rating.Text = label2PPlayer2Rating.Text = PPT.ReadStringUnicode(new IntPtr(playerAddress) + 0x50, 0x20);
            label4PPlayer3Rating.Text                             = PPT.ReadStringUnicode(new IntPtr(playerAddress) + 0xA0, 0x20);
            label4PPlayer4Rating.Text                             = PPT.ReadStringUnicode(new IntPtr(playerAddress) + 0xF0, 0x20);
        }

        private void buttonResetPuzzle_Click(object sender, EventArgs e) {
            startingRating = currentRating;

            valueStartingRating.Text = startingRating.ToString();
            valueCurrentRating.Text = currentRating.ToString();
            valueRatingDifference.Text = (currentRating - startingRating).ToString();

            wins = losses = 0;
            valueWins.Text = wins.ToString();
            valueLosses.Text = losses.ToString();
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