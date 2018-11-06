using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

using MisaMinoNET;
using ScpDriverInterface;

namespace PPTMonitor {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }

        int playerID = 0;
        VAMemory PPT = new VAMemory("puyopuyotetris");

        bool EnsureGame() {
            if (PPT == null) {
                if (Process.GetProcessesByName("puyopuyotetris").Length != 0) {
                    PPT = new VAMemory("puyopuyotetris");
                } else {
                    return false;
                }

            } else if (Process.GetProcessesByName("puyopuyotetris").Length == 0) {
                PPT = null;
                return false;
            }

            return true;
        }

        void RehookProcess(object sender, EventArgs e) {
            EnsureGame();
        }

        void ResetGame() {
            ScanTimer.Enabled = false;

            foreach (var process in Process.GetProcessesByName("puyopuyotetris")) {
                process.Kill();
            }

            EnsureGame();

            Thread.Sleep(10000);
            
            EnsureGame();
            Process.Start("steam://rungameid/546050");
            ratingSafe = 0;
            currentRating = 0;

            Thread.Sleep(15000);

            EnsureGame();

            ScanTimer.Enabled = true;
        }

        ScpBus scp = new ScpBus();
        bool gamepadPluggedIn = false;
        X360Controller gamepad = new X360Controller();
        
        void GamepadDisconnect(object sender, EventArgs e) {
            scp.UnplugAll();
            scp = new ScpBus();
            gamepad = new X360Controller();
            gamepadPluggedIn = false;
        }

        void GamepadConnect(object sender, EventArgs e) {
            scp.UnplugAll();
            scp.PlugIn(1);
            gamepadPluggedIn = true;
        }

        int currentRating, numplayers, frames, globalFrames;

        int[][,] board = new int[2][,] {
            new int[10, 40], new int[10, 40]
        };

        //int[,] intendedBoard = new int[10, 40];
       
        bool inMatch = false;
        int menuStartFrames = 0;
        int ratingSafe = 0;

        List<Instruction> movements = new List<Instruction>();
        int state = 0;
        int piece = 0;
        int pieceUsed;
        int[] queue = new int[5];
        bool register = false;

        private void runLogic() {
            numplayers = GameHelper.getPlayerCount(PPT);
            playerID = GameHelper.FindPlayer(PPT);

            int temp = GameHelper.getRating(PPT);

            if (temp != currentRating) {
                ratingSafe = GameHelper.getMenuFrameCount(PPT);
            }

            currentRating = temp;

            for (int p = 0; p < 2; p++) {
                int boardAddress = GameHelper.boardAddress(PPT, p);
                for (int i = 0; i < 10; i++) {
                    int columnAddress = PPT.ReadInt32(new IntPtr(boardAddress + i * 0x08));
                    for (int j = 0; j < 40; j++) {
                        board[p][i, j] = PPT.ReadInt32(new IntPtr(columnAddress + j * 0x04));
                    }
                }
            }

            globalFrames = GameHelper.getMenuFrameCount(PPT);

            if (GameHelper.OutsideMenu(PPT) && GameHelper.CurrentMode(PPT) == 4 && numplayers < 2 && GameHelper.boardAddress(PPT, playerID) == 0x0 && ratingSafe + 1500 < GameHelper.getMenuFrameCount(PPT)) {
                ResetGame();                
                return;
            }

            if (GameHelper.boardAddress(PPT, playerID) != 0x0 && GameHelper.OutsideMenu(PPT) && GameHelper.getBigFrameCount(PPT) != 0x0) {
                if (numplayers < 2 && GameHelper.CurrentMode(PPT) == 4) {
                    ResetGame();
                    return;
                }

                int drop = GameHelper.getPieceDropped(PPT, playerID);
                int current = GameHelper.getCurrentPiece(PPT, playerID);

                int piecesAddress = GameHelper.piecesAddress(PPT, playerID);
                int i;

                int[] pieces = new int[5];
                for (i = 0; i < 5; i++) {
                    pieces[i] = PPT.ReadByte(new IntPtr(piecesAddress + i * 0x04));
                }

                if (GameHelper.getBigFrameCount(PPT) < 6) {
                    MisaMino.Reset();
                    register = false;
                    movements.Clear();
                    inputStarted = false;
                    softdrop = false;
                }

                if (drop != state && drop == 1) {
                    register = true;
                }

                if ((register && !pieces.SequenceEqual(queue) && current == queue[0]) || (current != piece && piece == 255)) {
                    movements = MisaMino.FindMove(
                        pieces, 
                        current, 
                        board[playerID], 
                        GameHelper.getCombo(PPT, playerID), 
                        GameHelper.getGarbageOverhead(PPT, playerID), 
                        ref pieceUsed
                    );
                    
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
        bool softdrop = false;

        private void processInput() {
            if (movements.Count > 0) {
                switch (movements[0]) {
                    case Instruction.NULL:
                        movements.RemoveAt(0);
                        inputStarted = false;
                        processInput();
                        break;

                    case Instruction.L:
                        if (!inputStarted) {
                            inputGoal = GameHelper.getPiecePositionX(PPT, playerID) - 1;
                            inputStarted = true;
                        }

                        if (inputStarted) {
                            if (GameHelper.getPiecePositionX(PPT, playerID) == inputGoal) {
                                movements.RemoveAt(0);
                                inputStarted = false;
                                processInput();
                                break;
                            } else {
                                gamepad.Buttons |= X360Buttons.Left;
                            }
                        }
                        break;

                    case Instruction.R:
                        if (!inputStarted) {
                            inputGoal = GameHelper.getPiecePositionX(PPT, playerID) + 1;
                            inputStarted = true;
                        }

                        if (inputStarted) {
                            if (GameHelper.getPiecePositionX(PPT, playerID) == inputGoal) {
                                movements.RemoveAt(0);
                                inputStarted = false;
                                processInput();
                                break;
                            } else {
                                gamepad.Buttons |= X360Buttons.Right;
                            }
                        }
                        break;

                    case Instruction.LL:
                        if (!inputStarted) {
                            inputGoal = InputHelper.FindInputGoalX(
                                board[playerID],
                                pieceUsed,
                                GameHelper.getPiecePositionX(PPT, playerID),
                                GameHelper.getPiecePositionY(PPT, playerID),
                                GameHelper.getPieceRotation(PPT, playerID),
                                -1
                            );
                            if (movements.Count > 1) {
                                if (movements[1] == Instruction.R) {
                                    inputGoal++;
                                    movements.RemoveAt(1);
                                }
                            }
                            inputStarted = true;
                        }

                        if (inputStarted) {
                            if (GameHelper.getPiecePositionX(PPT, playerID) == inputGoal) {
                                movements.RemoveAt(0);
                                inputStarted = false;
                                processInput();
                                break;
                            } else {
                                gamepad.Buttons |= X360Buttons.Left;
                            }
                        }
                        break;

                    case Instruction.RR:
                        if (!inputStarted) {
                            inputGoal = InputHelper.FindInputGoalX(
                                board[playerID],
                                pieceUsed,
                                GameHelper.getPiecePositionX(PPT, playerID),
                                GameHelper.getPiecePositionY(PPT, playerID),
                                GameHelper.getPieceRotation(PPT, playerID),
                                1
                            );
                            if (movements.Count > 1) {
                                if (movements[1] == Instruction.L) {
                                    inputGoal--;
                                    movements.RemoveAt(1);
                                }
                            }
                            inputStarted = true;
                        }

                        if (inputStarted) {
                            if (GameHelper.getPiecePositionX(PPT, playerID) == inputGoal) {
                                movements.RemoveAt(0);
                                inputStarted = false;
                                processInput();
                                break;
                            } else {
                                gamepad.Buttons |= X360Buttons.Right;
                            }
                        }
                        break;

                    case Instruction.D:
                        if (!inputStarted) {
                            inputGoal = GameHelper.getPiecePositionY(PPT, playerID) + 1;
                            inputStarted = true;
                        }

                        if (inputStarted) {
                            if (GameHelper.getPiecePositionY(PPT, playerID) == inputGoal) {
                                movements.RemoveAt(0);
                                inputStarted = false;
                                processInput();
                                break;
                            } else {
                                gamepad.Buttons |= X360Buttons.Down;
                            }
                        }
                        break;

                    case Instruction.DD:
                        if (!inputStarted) {
                            inputGoal = InputHelper.FindInputGoalY(
                                board[playerID],
                                pieceUsed,
                                GameHelper.getPiecePositionX(PPT, playerID),
                                GameHelper.getPiecePositionY(PPT, playerID),
                                GameHelper.getPieceRotation(PPT, playerID)
                            );
                            inputStarted = true;
                        }

                        if (inputStarted) {
                            if (GameHelper.getPiecePositionY(PPT, playerID) == inputGoal) {
                                softdrop = false;
                                movements.RemoveAt(0);
                                inputStarted = false;
                                processInput();
                                break;
                            } else {
                                softdrop = true;
                            }
                        }
                        break;

                    case Instruction.LSPIN:
                        if (!inputStarted) {
                            inputGoal = GameHelper.getPieceRotation(PPT, playerID) - 1;
                            if (inputGoal == -1) inputGoal = 3;
                            inputStarted = true;
                        }

                        if (inputStarted) {
                            if (GameHelper.getPieceRotation(PPT, playerID) == inputGoal) {
                                movements.RemoveAt(0);
                                inputStarted = false;
                                processInput();
                                break;
                            } else {
                                gamepad.Buttons |= X360Buttons.A;
                            }
                        }
                        break;

                    case Instruction.RSPIN:
                        if (!inputStarted) {
                            inputGoal = GameHelper.getPieceRotation(PPT, playerID) + 1;
                            if (inputGoal == 4) inputGoal = 0;
                            inputStarted = true;
                        }

                        if (inputStarted) {
                            if (GameHelper.getPieceRotation(PPT, playerID) == inputGoal) {
                                movements.RemoveAt(0);
                                inputStarted = false;
                                processInput();
                                break;
                            } else {
                                gamepad.Buttons |= X360Buttons.B;
                            }
                        }
                        break;

                    case Instruction.DROP:
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

                    case Instruction.HOLD:
                        if (!inputStarted) {
                            inputGoal = GameHelper.getHoldPointer(PPT, playerID);
                            inputStarted = true;
                        }

                        if (inputStarted) {
                            if (GameHelper.getHoldPointer(PPT, playerID) != inputGoal && GameHelper.getHoldPointer(PPT, playerID) > 0x08000000) {
                                movements.RemoveAt(0);
                                inputStarted = false;
                                processInput();
                                break;
                            } else {
                                gamepad.Buttons |= X360Buttons.LeftBumper;
                            }
                        }
                        break;
                }
            }
        }

        private void applyInputs() {
            int nextFrame = GameHelper.getFrameCount(PPT);
            int menuFrames = GameHelper.getMenuFrameCount(PPT);

            if (GameHelper.boardAddress(PPT, playerID) != 0x0 && GameHelper.OutsideMenu(PPT) && nextFrame > 0 && GameHelper.getBigFrameCount(PPT) != 0x0) {
                if (nextFrame % 2 == 0) {
                    if (nextFrame != frames) {
                        gamepad.Buttons = X360Buttons.None;
                        processInput();
                    }
                } else {
                    gamepad.Buttons = X360Buttons.None;
                }

                if (softdrop)
                    gamepad.Buttons |= X360Buttons.Down;

                frames = nextFrame;

            } else {
                int mode = GameHelper.CurrentMode(PPT);
                gamepad.Buttons = X360Buttons.None;

                if (menuFrames % 2 == 0) {
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
            }

            valueGamepadInputs.Text = gamepad.Buttons.ToString();
            scp.Report(1, gamepad.GetReport());
        }


        private void updateUI() {
            if (inMatch) {
                UIHelper.drawBoard(board1, board[playerID]);
                UIHelper.drawBoard(board2, board[1]);
            } else {
                board1.Image = board2.Image = null;
            }

            valueGamepadState.Text = gamepadPluggedIn? "Connected" : "Disconnected";
            valueGamepadInputs.Text = gamepad.Buttons.ToString();

            valueGameRunning.Text = (PPT == null)? "Closed" : "Running";
            valuePlayers.Text = numplayers.ToString();
            valueMatchFrames.Text = frames.ToString();
            valueGlobalFrames.Text = globalFrames.ToString();

            valueMisaMinoState.Text = inMatch? "Match" : "Menu";
            valueMisaMinoLevel.Enabled = valueMisaMinoStyle.Enabled = !inMatch;
            valueInstructions.Text = String.Join(", ", movements);
        }

        private void valueMisaMinoLevel_SelectedIndexChanged(object sender, EventArgs e) {
            MisaMino.updateLevel(valueMisaMinoLevel.SelectedIndex + 1);
        }

        private void valueMisaMinoStyle_SelectedIndexChanged(object sender, EventArgs e) {
            MisaMino.updateStyle(valueMisaMinoStyle.SelectedIndex + 1);
        }

        private void Loop(object sender, EventArgs e) {
            if (EnsureGame()) {
                runLogic();
                applyInputs();
            }

            updateUI();
        }

        void MainForm_Load(object sender, EventArgs e) {
            scp.UnplugAll();
            scp.PlugIn(1);
            gamepadPluggedIn = true;

            valueMisaMinoLevel.SelectedIndex = 9;
            valueMisaMinoStyle.SelectedIndex = 0;
        }

        void MainForm_Closing(object sender, EventArgs e) {
            scp.UnplugAll();
            gamepadPluggedIn = false;
        }
    }
}