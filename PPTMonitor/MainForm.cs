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

        static int[,] intendedBoard = new int[10, 40];

        private void MainForm_Load(object sender, EventArgs e) {
            scp.PlugIn(1);
            menuStartFrames = GameHelper.getMenuFrameCount(PPT);
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

        enum movement {
            NULL,
            L,
            R,
            LL,
            RR,
            D,
            DD,
            LSPIN,
            RSPIN,
            DROP,
            HOLD,
            SPIN2,
            REFRESH
        };

        List<movement> movements = new List<movement>();
        int state = 0;
        int piece = 0;
        int[] queue = new int[5];
        bool register = false;

        private void runLogic() {
            /*if (GameHelper.OutsideMenu(PPT) && GameHelper.CurrentMode(PPT) == 4 && numplayers < 2 && GameHelper.boardAddress(PPT, playerID) == 0x0 && ratingSafe + 1500 < GameHelper.getMenuFrameCount(PPT)) {
                Kill();                
                return;
            }*/

            if (GameHelper.boardAddress(PPT, playerID) != 0x0 && GameHelper.OutsideMenu(PPT) && GameHelper.getBigFrameCount(PPT) != 0x0) {
                /*if (numplayers < 2) {
                    Kill();
                    return;
                }*/

                int drop = GameHelper.getPieceDropped(PPT, playerID);
                int current = GameHelper.getCurrentPiece(PPT, playerID);

                int piecesAddress = GameHelper.piecesAddress(PPT, playerID);
                int i;

                int[] pieces = new int[5];
                for (i = 0; i < 5; i++) {
                    pieces[i] = PPT.ReadByte(new IntPtr(piecesAddress + i * 0x04));
                }

                if (GameHelper.getBigFrameCount(PPT) < 6) {
                    MisaMinoNET.MisaMino.Reset();
                    register = false;
                    movements.Clear();
                    inputStarted = false;
                }

                if (drop != state && drop == 1) {
                    register = true;
                }

                if ((register && !pieces.SequenceEqual(queue) && current == queue[0]) || (current != piece && piece == 255)) {
                    string[] solution = MisaMinoNET.MisaMino.FindMove(pieces, current, board[playerID], 0, 0).Split('|');

                    movements.Clear();
                    foreach (string mov in solution[0].Split(',')) {
                        movements.Add((movement)int.Parse(mov));
                    }
                    labelMisaMino.Text = String.Join(", ", movements);

                    i = 19;
                    foreach (string row in solution[1].Split(';')) {
                        int j = 0;
                        foreach (string col in row.Split(',')) {
                            if (col.Equals("0")) {
                                // Mirror for whatever reason. Blaming MisaMino.
                                intendedBoard[9 - j, i] = -1;
                            } else {
                                intendedBoard[9 - j, i] = 9;
                            }
                            j++;
                        }
                        i--;
                    }

                    UIHelper.drawBoard(board1, board[playerID]);
                    UIHelper.drawBoard(board2, intendedBoard);

                    register = false;
                }

                state = drop;
                piece = current;

                if (!register)
                    queue = (int[])pieces.Clone();

                inMatch = true;

            } else {
                if (inMatch) {
                    inMatch = false;

                    menuStartFrames = GameHelper.getMenuFrameCount(PPT);
                }
            }
        }

        bool inputStarted = false;
        int inputGoal = -1;

        private void processInput() {
            if (movements.Count > 0) {
                switch (movements[0]) {
                    case movement.NULL:
                        movements.RemoveAt(0);
                        inputStarted = false;
                        processInput();
                        break;

                    case movement.L:
                        if (!inputStarted) {
                            inputGoal = GameHelper.getPiecePosition(PPT, playerID) - 1;
                            inputStarted = true;
                        }

                        if (inputStarted) {
                            if (GameHelper.getPiecePosition(PPT, playerID) == inputGoal) {
                                movements.RemoveAt(0);
                                inputStarted = false;
                                processInput();
                                break;
                            } else {
                                gamepad.Buttons |= X360Buttons.Left;
                            }
                        }
                        break;

                    case movement.R:
                        if (!inputStarted) {
                            inputGoal = GameHelper.getPiecePosition(PPT, playerID) + 1;
                            inputStarted = true;
                        }

                        if (inputStarted) {
                            if (GameHelper.getPiecePosition(PPT, playerID) == inputGoal) {
                                movements.RemoveAt(0);
                                inputStarted = false;
                                processInput();
                                break;
                            } else {
                                gamepad.Buttons |= X360Buttons.Right;
                            }
                        }
                        break;

                    case movement.LL:
                        if (!inputStarted) {
                            inputGoal = GameHelper.getPiecePosition(PPT, playerID) + 1;
                            inputStarted = true;
                        }

                        if (inputStarted) {
                            if (GameHelper.getPiecePosition(PPT, playerID) == inputGoal) {
                                movements.RemoveAt(0);
                                inputStarted = false;
                                processInput();
                                break;
                            } else {
                                inputGoal = GameHelper.getPiecePosition(PPT, playerID);
                                gamepad.Buttons |= X360Buttons.Left;
                            }
                        }
                        break;

                    case movement.RR:
                        if (!inputStarted) {
                            inputGoal = GameHelper.getPiecePosition(PPT, playerID) - 1;
                            inputStarted = true;
                        }

                        if (inputStarted) {
                            if (GameHelper.getPiecePosition(PPT, playerID) == inputGoal) {
                                movements.RemoveAt(0);
                                inputStarted = false;
                                processInput();
                                break;
                            } else {
                                inputGoal = GameHelper.getPiecePosition(PPT, playerID);
                                gamepad.Buttons |= X360Buttons.Right;
                            }
                        }
                        break;

                    case movement.DROP:
                        if (!inputStarted) {
                            inputGoal = 1;
                            inputStarted = true;
                        }

                        if (inputStarted) {
                            if (GameHelper.getPieceDropped(PPT, playerID) == inputGoal) {
                                movements.RemoveAt(0);
                                inputStarted = false;
                                processInput();
                                break;
                            } else {
                                gamepad.Buttons |= X360Buttons.Up;
                            }
                        }
                        break;
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

                if (nextFrame % 2 == 0) {
                    processInput();
                }

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