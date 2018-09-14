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

        private enum MatchType {
            PuzzleLeague,
            FreePlay
        }

        private enum League {
            Student,
            Beginner,
            Rookie,
            Amateur,
            Ace,
            Wizard,
            Professional,
            Elite,
            Virtuoso,
            Star,
            Superstar,
            Legend,
            Golden,
            Platinum,
            GrandMaster
        }

        private enum MatchState {
            Menu,
            Match,
            Finished
        }

        private static string RatioType(int ratio) {
            if (ratio < 34) {
                return "T";
            } else if (ratio > 66) {
                return "P";
            }
            return "X";
        }

        private struct Player {
            public string name;
            public int rating;
            public League league;
            public int playstyle;
            public int location;
            public int regional;
            public int worldwide;
        }

        static VAMemory PPT = new VAMemory("puyopuyotetris");

        static int startingRating, currentRating, wins = 0, losses = 0, numplayers;

        static MatchState match = MatchState.Finished;
        static int[] score = new int[4] {0, 0, 0, 0};
        static int[] sets = new int[4] {0, 0, 0, 0};
        static int[] total = new int[4] {0, 0, 0, 0};
       
        static int maxscore = 2;
        static List<int> history = new List<int>();

        static Player[] players = new Player[4];

        private void ScanTimer_Tick(object sender, EventArgs e) {
            int temp;

            int scoreAddress = PPT.ReadInt32(new IntPtr(0x14057F048));

            if (scoreAddress == 0x0) {
                if (match == MatchState.Finished)
                    match = MatchState.Menu;

                buttonResetBattle_Click(sender, e);
            } else {
                if (match == MatchState.Menu)
                    match = MatchState.Match;

                scoreAddress += 0x38;
            }

            maxscore = PPT.ReadInt32(new IntPtr(scoreAddress + 0x10));

            for (int i = 0; i < 4; i++) {
                temp = PPT.ReadInt32(new IntPtr(scoreAddress) + i * 4);
                if (temp > score[i]) {
                    total[i]++;
                    history.Add(i);
                    if (temp >= maxscore) {
                        sets[i]++;
                    }
                }
                score[i] = temp;
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
            int leagueAddress = PPT.ReadInt32(new IntPtr(PPT.ReadInt32(new IntPtr(PPT.ReadInt32(new IntPtr(PPT.ReadInt32(new IntPtr(0x140473760)) + 0x68)) + 0x20)) + 0x970)) - 0x38;

            numplayers = PPT.ReadInt16(new IntPtr(playerAddress) - 0x24);
            valuePlayers.Text = numplayers.ToString();

            for (int i = 0; i < 4; i++) {
                players[i].name = PPT.ReadStringUnicode(new IntPtr(playerAddress) + i * 0x50, 0x20);
                players[i].rating = PPT.ReadInt16(new IntPtr(playerAddress) + 0x30 + i * 0x50);

                temp = PPT.ReadInt16(new IntPtr(leagueAddress) + i * 0x140);

                if (temp != 0)
                    players[i].league = (League)(temp - 1);
                
                players[i].playstyle = PPT.ReadInt16(new IntPtr(playerAddress) + 0x32 + i * 0x50);
            }

            valueP1Rating.Text = players[0].rating.ToString();
            valueP2Rating.Text = players[1].rating.ToString();
            valueP3Rating.Text = players[2].rating.ToString();
            valueP4Rating.Text = players[3].rating.ToString();

            valueP1Name.Text = players[0].name;
            valueP2Name.Text = players[1].name;
            valueP3Name.Text = players[2].name;
            valueP4Name.Text = players[3].name;

            valueP1League.Text = ((int)players[0].league).ToString("X");
            valueP2League.Text = ((int)players[1].league).ToString("X");
            valueP3League.Text = ((int)players[2].league).ToString("X");
            valueP4League.Text = ((int)players[3].league).ToString("X");

            valueP1Ratio.Text = RatioType(players[0].playstyle);
            valueP2Ratio.Text = RatioType(players[1].playstyle);
            valueP3Ratio.Text = RatioType(players[2].playstyle);
            valueP4Ratio.Text = RatioType(players[3].playstyle);

            temp = PPT.ReadInt32(new IntPtr(0x140599FF0));

            if (temp != currentRating) {
                if (match == MatchState.Match) {
                    writeMatch(MatchType.PuzzleLeague, numplayers, maxscore, history, temp - currentRating, players);
                    match = MatchState.Finished;
                }

                if (temp > currentRating) {
                    wins++;
                    valueWins.Text = wins.ToString();
                } else if (temp < currentRating) {
                    losses++;
                    valueLosses.Text = losses.ToString();
                }
            }

            currentRating = temp;

            valueCurrentRating.Text = currentRating.ToString();
            valueRatingDifference.Text = (currentRating - startingRating).ToString();
        }

        private void writeMatch(MatchType type, int players, int bestof, List<int> matchLog, int ratingChange, Player[] matchPlayers) {
            log.Text += $"writeMatch called {type}, {players}, {bestof}, {{{string.Join(", ", matchLog.ToArray())}}}, {ratingChange}, {matchPlayers[1].name}";
        }

        private void buttonResetBattle_Click(object sender, EventArgs e) {
            for (int i = 0; i < 4; i++) {
                score[i] = 0;
                sets[i] = 0;
                total[i] = 0;
            }

            history.Clear();

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