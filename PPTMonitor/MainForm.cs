using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using ScpDriverInterface;

namespace PPTMonitor {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }

        static VAMemory PPT = new VAMemory("puyopuyotetris");
        static ScpBus scp = new ScpBus();
        static X360Controller gamepad = new X360Controller();

        static int currentRating, numplayers, maxscore = 2, frames;
        static GameHelper.Player[] players = new GameHelper.Player[2] {
            new GameHelper.Player(), new GameHelper.Player()
        };
        static int[] score = new int[2] { 0, 0 };
        static int[][,] board = new int[2][,] {
            new int[10, 40], new int[10, 40]
        };

        static int[,] intendedBoard = new int[10, 40];
        int[] queue = new int[5];
        LogicHelper.Solution solution = new LogicHelper.Solution();

        private void MainForm_Load(object sender, EventArgs e) {
            scp.PlugIn(1);
        }

        private void MainForm_FormClosing(object sender, EventArgs e) {
            scp.UnplugAll();
        }

        private void buttonRehook_Click(object sender, EventArgs e) {
            PPT = new VAMemory("puyopuyotetris");
        }

        private void valueP1Name_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start(players[0].steam);
        }

        private void valueP2Name_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start(players[1].steam);
        }

        private void updateUI() {
            valueScore1.Text = score[0].ToString();
            valueScore2.Text = score[1].ToString();

            valueP1Rating.Text = players[0].rating.ToString();
            valueP2Rating.Text = players[1].rating.ToString();

            valueP1Name.Text = players[0].name;
            valueP2Name.Text = players[1].name;

            valueP1League.Text = UIHelper.Leagues[players[0].league];
            valueP2League.Text = UIHelper.Leagues[players[1].league];

            valueP1Ratio.Text = UIHelper.getRatioType(players[0].playstyle);
            valueP2Ratio.Text = UIHelper.getRatioType(players[1].playstyle);

            valueP1Region.Text = UIHelper.Regions[players[0].region];
            valueP2Region.Text = UIHelper.Regions[players[1].region];

            if (players[0].region == 0) {
                valueP1Regional.Text = "-";
            } else {
                valueP1Regional.Text = players[0].regional.ToString();
            }
            if (players[1].region == 0) {
                valueP2Regional.Text = "-";
            } else {
                valueP2Regional.Text = players[1].regional.ToString();
            }

            valueP1Worldwide.Text = players[0].worldwide.ToString();
            valueP2Worldwide.Text = players[1].worldwide.ToString();

            valuePlayers.Text = numplayers.ToString();
            valueRating.Text = currentRating.ToString();

            UIHelper.updateImage(valueP1CharacterPref, "character", players[0].pref);
            UIHelper.updateImage(valueP2CharacterPref, "character", players[1].pref);

            UIHelper.updateImage(valueP1Character, "character", players[0].character);
            UIHelper.updateImage(valueP2Character, "character", players[1].character);

            UIHelper.updateImage(valueP1Gamemode, "gamemode", players[0].gamemode);
            UIHelper.updateImage(valueP2Gamemode, "gamemode", players[1].gamemode);

            UIHelper.updateVoice(valueP1Voice, players[0].voice);
            UIHelper.updateVoice(valueP2Voice, players[1].voice);

            UIHelper.drawBoard(board1, board[0]);
            //UIHelper.drawBoard(board2, board[1]);
            UIHelper.drawBoard(board2, intendedBoard);

            valueFramecount.Text = frames.ToString();

            valueIntendedPosition.Text = solution.desiredX.ToString();
            valueIntendedRotation.Text = solution.desiredR.ToString();
        }

        private void updateGame() {
            int temp;

            int scoreAddress = GameHelper.scoreAddress(PPT);
            int playerAddress = GameHelper.playerAddress(PPT);
            int leagueAddress = GameHelper.leagueAddress(PPT);
            int prefAddress = GameHelper.prefAddress(PPT);
            int charAddress = GameHelper.charAddress(PPT);

            maxscore = PPT.ReadInt32(new IntPtr(scoreAddress + 0x10));

            for (int i = 0; i < 2; i++)
                score[i] = PPT.ReadInt32(new IntPtr(scoreAddress + i * 4));

            numplayers = PPT.ReadInt16(new IntPtr(playerAddress) - 0x24);

            for (int i = 0; i < 2; i++) {
                players[i].name = PPT.ReadStringUnicode(new IntPtr(playerAddress) + i * 0x50, 0x20);
                players[i].rating = PPT.ReadInt16(new IntPtr(playerAddress) + 0x30 + i * 0x50);

                temp = PPT.ReadInt16(new IntPtr(leagueAddress) + i * 0x140);

                if (temp != 0)
                    players[i].league = temp - 1;

                players[i].playstyle = PPT.ReadInt16(new IntPtr(playerAddress) + 0x32 + i * 0x50);
                players[i].region = PPT.ReadByte(new IntPtr(leagueAddress) - 0x0C + i * 0x140);

                players[i].regional = PPT.ReadInt32(new IntPtr(playerAddress) + 0x28 + i * 0x50);
                players[i].worldwide = PPT.ReadInt32(new IntPtr(playerAddress) + 0x2C + i * 0x50);

                players[i].id = PPT.ReadInt32(new IntPtr(playerAddress) + 0x40 + i * 0x50);

                players[i].steam = $"https://steamcommunity.com/profiles/{(76561197960265728 + players[i].id).ToString()}";

                players[i].pref = PPT.ReadByte(new IntPtr(prefAddress + (i - 1) * 0x9558));

                players[i].character = PPT.ReadByte(new IntPtr(charAddress + 0x1c8 + i * 0x30));
                players[i].gamemode = PPT.ReadByte(new IntPtr(charAddress + 0x980 + i * 0x28));
                players[i].voice = (PPT.ReadByte(new IntPtr(charAddress + 0x594)) & (i + 1)) >> i;
            }

            for (int i = numplayers; i < 2; i++) {
                players[i] = new GameHelper.Player();
            }

            currentRating = GameHelper.getRating(PPT);
            
            int[] boardAddress = GameHelper.boardAddress(PPT);

            for (int p = 0; p < 2; p++) {
                for (int i = 0; i < 10; i++) {
                    int columnAddress = PPT.ReadInt32(new IntPtr(boardAddress[p] + i * 0x08));
                    for (int j = 0; j < 40; j++) {
                        board[p][i, j] = PPT.ReadInt32(new IntPtr(columnAddress + j * 0x04));
                    }
                }
            }

            int a = GameHelper.boardAddress(PPT)[0];
        }

        private void button1_Click(object sender, EventArgs e) {
            scp.UnplugAll();
            scp = new ScpBus();
            gamepad = new X360Controller();
        }

        private void button2_Click(object sender, EventArgs e) {
            scp.PlugIn(1);
        }

        int holdPiece = -1;

        private void runLogic() {
            if (GameHelper.EnsureMatch(PPT)) {
                int piecesAddress = GameHelper.piecesAddress(PPT);

                int[] pieces = new int[5];
                for (int i = 0; i < 4; i++) {
                    pieces[i] = PPT.ReadByte(new IntPtr(piecesAddress + i * 0x04));
                }

                if (!pieces.SequenceEqual(queue)) {
                    int current = GameHelper.getCurrentPiece(PPT);
                    valueCurrentPiece.Text = current.ToString(); // UI

                    if (current != -1 && current == queue[0]) {
                        queue = (int[])pieces.Clone();

                        solution = LogicHelper.findMove(board[0], current, queue.Take(1).ToArray(), holdPiece, ref labelDownstacking, ref labelTetrisable);
                        intendedBoard = solution.desiredBoard;
                        holdPiece = solution.pieceLeft;

                        labelHold.Text = holdPiece.ToString();
                        labelUseHold.Text = solution.useHold.ToString();
                    }
                }

                if (frames < 20) {
                    queue = (int[])pieces.Clone();
                    holdPiece = -1;
                }
            } else {
                queue = new int[5];
                holdPiece = -1;
            }
        }

        private void applyInputs() {
            gamepad.Buttons = X360Buttons.None;
            int nextFrame = GameHelper.getFrameCount(PPT);

            if (GameHelper.EnsureMatch(PPT) && nextFrame > 0) {
                int pieceX = GameHelper.getPiecePosition(PPT);
                int pieceR = GameHelper.getPieceRotation(PPT);

                valueCurrentPosition.Text = pieceX.ToString();
                valueCurrentRotation.Text = pieceR.ToString();

                if (nextFrame > frames) {
                    if (nextFrame % 2 == 0) {
                        if (solution.useHold) {
                            gamepad.Buttons |= X360Buttons.RightBumper;
                        }

                        if (solution.desiredX == pieceX && solution.desiredR == pieceR) {
                            gamepad.Buttons |= X360Buttons.Up;
                        } else {
                            if (solution.desiredX != pieceX)
                                if (solution.desiredX < pieceX) {
                                    gamepad.Buttons |= X360Buttons.Left;
                                } else {
                                    gamepad.Buttons |= X360Buttons.Right;
                                }

                            if (solution.desiredR != pieceR)
                                if (solution.desiredR == 3) {
                                    gamepad.Buttons |= X360Buttons.A;
                                } else {
                                    gamepad.Buttons |= X360Buttons.B;
                                }
                        }
                    }
                }

                frames = nextFrame;

            } else {
                if (GameHelper.getMenuFrameCount(PPT) % 2 == 0) {
                    gamepad.Buttons |= X360Buttons.A;
                }
            }

            scp.Report(1, gamepad.GetReport());
        }

        private void AILoop(object sender, EventArgs e) {
            updateGame();
            runLogic();
            applyInputs();
            updateUI();
        }
    }
}