using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ScpDriverInterface;

namespace PPTMonitor {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }

        static int playerID = 0;

        static VAMemory PPT = new VAMemory("puyopuyotetris");
        static ScpBus scp = new ScpBus();
        static X360Controller gamepad = new X360Controller();

        static int currentRating, numplayers, frames;

        static int[][,] board = new int[2][,] {
            new int[10, 40], new int[10, 40]
        };

        int[] queue = new int[5];

        private void MainForm_Load(object sender, EventArgs e) {
            scp.PlugIn(1);
            menuStartFrames = GameHelper.getMenuFrameCount(PPT);

            MessageBox.Show(MisaMinoNET.MisaMino.test().ToString());
        }

        private void MainForm_FormClosing(object sender, EventArgs e) {
            scp.UnplugAll();
        }

        private void buttonRehook_Click(object sender, EventArgs e) {
            PPT = new VAMemory("puyopuyotetris");
        }

        private void updateUI() {
            // stub
        }

        private void updateGame() {
            int playerAddress = GameHelper.playerAddress(PPT);
            int leagueAddress = GameHelper.leagueAddress(PPT);
            int prefAddress = GameHelper.prefAddress(PPT);
            int charAddress = GameHelper.charAddress(PPT);
            
            numplayers = PPT.ReadInt16(new IntPtr(playerAddress) - 0x24);

            int temp = GameHelper.getRating(PPT);

            if (temp != currentRating) {
                ratingSafe = GameHelper.getMenuFrameCount(PPT);
            }

            currentRating = temp;
            
            for (int p = 0; p < 2; p++) {
                int boardAddress = GameHelper.boardAddress(PPT, playerID);
                for (int i = 0; i < 10; i++) {
                    int columnAddress = PPT.ReadInt32(new IntPtr(boardAddress + i * 0x08));
                    for (int j = 0; j < 40; j++) {
                        board[p][i, j] = PPT.ReadInt32(new IntPtr(columnAddress + j * 0x04));
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            scp.UnplugAll();
            scp = new ScpBus();
            gamepad = new X360Controller();
        }

        private void button2_Click(object sender, EventArgs e) {
            scp.PlugIn(1);
        }

        private int holdPiece = -1;
        private bool inMatch = false;
        private int menuStartFrames = 0;
        private int ratingSafe = 0;

        private void Kill() {
            ScanTimer.Enabled = false;

            foreach (var process in Process.GetProcessesByName("puyopuyotetris")) {
                process.Kill();
            }

            Thread.Sleep(10000);

            Process.Start("steam://rungameid/546050");
            ratingSafe = 0;
            currentRating = 0;

            Thread.Sleep(15000);

            buttonRehook_Click(this, EventArgs.Empty);

            ScanTimer.Enabled = true;
        }

        private void runLogic() {
            if (GameHelper.OutsideMenu(PPT) && GameHelper.CurrentMode(PPT) == 4 && numplayers < 2 && GameHelper.boardAddress(PPT, playerID) == 0x0 && ratingSafe + 1500 < GameHelper.getMenuFrameCount(PPT)) {
                Kill();                
                return;
            }

            if (GameHelper.boardAddress(PPT, playerID) != 0x0 && GameHelper.OutsideMenu(PPT) && GameHelper.getBigFrameCount(PPT) != 0x0) {
                if (numplayers < 2) {
                    Kill();
                    return;
                }

                int piecesAddress = GameHelper.piecesAddress(PPT, playerID);

                int[] pieces = new int[5];
                for (int i = 0; i < 5; i++) {
                    pieces[i] = PPT.ReadByte(new IntPtr(piecesAddress + i * 0x04));
                }
                
                if (queue[4] == -1) {
                    pieces[4] = -1;
                }

                if (GameHelper.getBigFrameCount(PPT) < 6) {
                    queue = (int[])pieces.Clone();
                    holdPiece = -1;

                } else if (!pieces.SequenceEqual(queue)) {
                    int current = GameHelper.getCurrentPiece(PPT, playerID);

                    if (current != -1 && current == queue[0]) {
                        queue = (int[])pieces.Clone();

                        /* solution = MisaMinoNET;

                        if (holdPiece == -1 && solution.pieceLeft != -1) {
                            for (int i = 0; i < 4; i++) {
                                queue[i] = queue[i + 1];
                            }
                            queue[4] = -1;
                        }
                        holdPiece = solution.pieceLeft;*/
                    }
                }

                inMatch = true;

            } else {
                queue = new int[5];
                holdPiece = -1;

                if (inMatch) {
                    inMatch = false;

                    menuStartFrames = GameHelper.getMenuFrameCount(PPT);
                }
            }
        }

        private void applyInputs() {
            gamepad.Buttons = X360Buttons.None;
            int nextFrame = GameHelper.getFrameCount(PPT);
            int menuFrames = GameHelper.getMenuFrameCount(PPT);

            if (GameHelper.boardAddress(PPT, playerID) != 0x0 && GameHelper.OutsideMenu(PPT) && nextFrame > 0 && GameHelper.getBigFrameCount(PPT) != 0x0) {
                int pieceX = GameHelper.getPiecePosition(PPT, playerID);
                int pieceR = GameHelper.getPieceRotation(PPT, playerID);

                /*if (nextFrame > frames && nextFrame % 2 == 0) {
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
                }*/

                frames = nextFrame;

            } else if (menuFrames % 2 == 0) {
                int mode = GameHelper.CurrentMode(PPT);

                if (GameHelper.OutsideMenu(PPT)) {
                    gamepad.Buttons |= X360Buttons.A;

                } else if (mode == 4) {
                    if (menuStartFrames + 1150 < menuFrames) {
                        menuStartFrames = menuFrames;
                    }

                    if (menuStartFrames + 1150 < menuFrames) {
                        menuStartFrames = menuFrames;
                    }

                    if (menuStartFrames + 1030 < menuFrames) {
                        gamepad.Buttons |= X360Buttons.B;
                    } else {
                        gamepad.Buttons |= X360Buttons.A;
                    }

                } else if (mode == 1) {
                    gamepad.Buttons |= X360Buttons.B;

                } else {
                    if (GameHelper.MenuHighlighted(PPT) != 4) {
                        gamepad.Buttons |= X360Buttons.Down;
                    } else {
                        gamepad.Buttons |= X360Buttons.A;
                    }
                }
            }

            labelInputs.Text = gamepad.Buttons.ToString();
            scp.Report(1, gamepad.GetReport());
        }

        private void AILoop(object sender, EventArgs e) {
            playerID = GameHelper.FindPlayer(PPT);

            updateGame();
            runLogic();
            applyInputs();
            updateUI();
        }
    }
}