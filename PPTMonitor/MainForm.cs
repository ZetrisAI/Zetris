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

        private static string RatioType(int ratio) {
            if (ratio < 34) {
                return "T";
            } else if (ratio > 66) {
                return "P";
            }
            return "X";
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

            int scoreAddress = PPT.ReadInt32(new IntPtr(0x14057F048));

            if (scoreAddress == 0x0) {
                buttonResetBattle_Click(sender, e);
            } else {
                scoreAddress += 0x38;
            }

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

            valueScore1.Text = score[0].ToString();
            valueScore2.Text = score[1].ToString();
            valueScore3.Text = score[2].ToString();
            valueScore4.Text = score[3].ToString();

            valueSets1.Text = sets[0].ToString();
            valueSets2.Text = sets[1].ToString();
            valueSets3.Text = sets[2].ToString();
            valueSets4.Text = sets[3].ToString();

            valueTotal1.Text = total[0].ToString();
            valueTotal2.Text = total[1].ToString();
            valueTotal3.Text = total[2].ToString();
            valueTotal4.Text = total[3].ToString();

            int playerAddress = PPT.ReadInt32(new IntPtr(PPT.ReadInt32(new IntPtr(0x140473760)) + 0x20)) + 0xD8;
            valuePlayers.Text = PPT.ReadInt16(new IntPtr(playerAddress) - 0x24).ToString();

            valueP1Rating.Text = PPT.ReadInt16(new IntPtr(playerAddress) + 0x30).ToString();
            valueP2Rating.Text = PPT.ReadInt16(new IntPtr(playerAddress) + 0x80).ToString();
            valueP3Rating.Text = PPT.ReadInt16(new IntPtr(playerAddress) + 0xD0).ToString();
            valueP4Rating.Text = PPT.ReadInt16(new IntPtr(playerAddress) + 0x120).ToString();

            valueP1Name.Text = PPT.ReadStringUnicode(new IntPtr(playerAddress), 0x20);
            valueP2Name.Text = PPT.ReadStringUnicode(new IntPtr(playerAddress) + 0x50, 0x20);
            valueP3Name.Text = PPT.ReadStringUnicode(new IntPtr(playerAddress) + 0xA0, 0x20);
            valueP4Name.Text = PPT.ReadStringUnicode(new IntPtr(playerAddress) + 0xF0, 0x20);

            int leagueAddress = PPT.ReadInt32(new IntPtr(PPT.ReadInt32(new IntPtr(PPT.ReadInt32(new IntPtr(PPT.ReadInt32(new IntPtr(0x140473760)) + 0x68)) + 0x20)) + 0x970)) - 0x38;
            valueP1League.Text = PPT.ReadInt16(new IntPtr(leagueAddress)).ToString("X");
            valueP2League.Text = PPT.ReadInt16(new IntPtr(leagueAddress) + 0x140).ToString("X");
            valueP3League.Text = PPT.ReadInt16(new IntPtr(leagueAddress) + 0x280).ToString("X");
            valueP4League.Text = PPT.ReadInt16(new IntPtr(leagueAddress) + 0x3C0).ToString("X");

            valueP1Ratio.Text = RatioType(PPT.ReadInt16(new IntPtr(playerAddress) + 0x32));
            valueP2Ratio.Text = RatioType(PPT.ReadInt16(new IntPtr(playerAddress) + 0x82));
            valueP3Ratio.Text = RatioType(PPT.ReadInt16(new IntPtr(playerAddress) + 0xD2));
            valueP4Ratio.Text = RatioType(PPT.ReadInt16(new IntPtr(playerAddress) + 0x122));
        }

        private void buttonResetBattle_Click(object sender, EventArgs e) {
            for (int i = 0; i < 4; i++) {
                score[i] = 0;
                sets[i] = 0;
                total[i] = 0;
            }

            valueScore1.Text = score[0].ToString();
            valueScore2.Text = score[1].ToString();
            valueScore3.Text = score[2].ToString();
            valueScore4.Text = score[3].ToString();

            valueSets1.Text = sets[0].ToString();
            valueSets2.Text = sets[1].ToString();
            valueSets3.Text = sets[2].ToString();
            valueSets4.Text = sets[3].ToString();

            valueTotal1.Text = total[0].ToString();
            valueTotal2.Text = total[1].ToString();
            valueTotal3.Text = total[2].ToString();
            valueTotal4.Text = total[3].ToString();
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