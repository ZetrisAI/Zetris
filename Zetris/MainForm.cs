using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

using MisaMinoNET;
using ScpDriverInterface;

namespace Zetris {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }

        int playerID = 0;
        ProcessMemory PPT = new ProcessMemory("puyopuyotetris", false);

        void ResetGame() {
            if (!GameHelper.InSwap(PPT)) return;

            ScanTimer.Enabled = false;

            foreach (var process in Process.GetProcessesByName("puyopuyotetris")) {
                process.Kill();
            }

            PPT.TrustProcess = false;

            PPT.CheckProcess();

            Thread.Sleep(10000);

            PPT.CheckProcess();
            Process.Start("steam://rungameid/546050");
            ratingSafe = 0;
            currentRating = 0;

            Thread.Sleep(15000);

            PPT.CheckProcess();

            PPT.TrustProcess = true;

            ScanTimer.Enabled = true;
        }

        ScpBus scp = new ScpBus();
        bool gamepadPluggedIn = false;
        X360Controller gamepad = new X360Controller();

        private void buttonGamepad_Click(object sender, EventArgs e) {
            scp.UnplugAll();
            scp = new ScpBus();
            gamepad = new X360Controller();

            if (!gamepadPluggedIn) scp.PlugIn(4);

            gamepadPluggedIn = !gamepadPluggedIn;
        }

        int currentRating, numplayers, frames, globalFrames;

        int[,] board = new int[10, 40];

        //int[,] intendedBoard = new int[10, 40];

        bool inMatch = false;
        int menuStartFrames = 0;
        int ratingSafe = 0;

        List<Instruction> movements = new List<Instruction>();
        int state = 0;
        int piece = 0;
        int pieceUsed;
        bool spinUsed;
        int[] queue = new int[5];
        bool register = false;
        int baseBoardHeight;

        private bool runLogic() {
            bool ret = false;

            numplayers = GameHelper.getPlayerCount(PPT);
            playerID = GameHelper.FindPlayer(PPT);

            if (GameHelper.InMultiplayer(PPT))
                playerID = 1 - playerID;

            int temp = GameHelper.getRating(PPT);

            if (temp != currentRating) {
                ratingSafe = GameHelper.getMenuFrameCount(PPT);
            }

            currentRating = temp;

            int y = GameHelper.getPiecePositionY(PPT, playerID);
            baseBoardHeight = 25 - y;

            int boardAddress = GameHelper.boardAddress(PPT, playerID);
            for (int i = 0; i < 10; i++) {
                int columnAddress = PPT.ReadInt32(new IntPtr(boardAddress + i * 0x08));
                for (int j = 0; j < (25 - y); j++) {
                    board[i, j] = PPT.ReadByte(new IntPtr(columnAddress + j * 0x04));
                }
            }

            if (GameHelper.OutsideMenu(PPT) && GameHelper.CurrentMode(PPT) == 4 && numplayers < 2 && GameHelper.boardAddress(PPT, playerID) == 0x0 && ratingSafe + 1500 < GameHelper.getMenuFrameCount(PPT)) {
                ResetGame();                
                return false;
            }

            if (GameHelper.boardAddress(PPT, playerID) != 0x0 && GameHelper.OutsideMenu(PPT) && GameHelper.getBigFrameCount(PPT) != 0x0) {
                if (numplayers < 2 && GameHelper.CurrentMode(PPT) == 4) {
                    ResetGame();
                    return false;
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
                    inputStarted = 0;
                    softdrop = false;
                }

                if (drop != state && drop == 1) {
                    register = true;
                }

                if (((register && !pieces.SequenceEqual(queue) && current == queue[0]) || (current != piece && piece == 255)) && y <= 5) {
                    movements = MisaMino.FindMove(
                        pieces, 
                        current,
                        baseBoardHeight,
                        board,
                        GameHelper.getCombo(PPT, playerID), 
                        GameHelper.getGarbageOverhead(PPT, playerID), 
                        ref pieceUsed,
                        ref spinUsed
                    );
                    ret = true;                    
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

            return ret;
        }

        int inputStarted = 0;
        int inputGoal = -1;
        bool softdrop = false;
        int desiredX, desiredR;
        bool desiredHold;

        private void processInput() {
            if (movements.Count > 0) {
                if (GameHelper.InSwap(PPT) && GameHelper.SwapType(PPT) == 0) {
                    softdrop = false;
                    movements.Clear();
                    inputStarted = 0;
                    return;
                }

                if (((spinUsed || InputHelper.boardHeight(board, baseBoardHeight) >= 15 || movements.Contains(Instruction.D) || movements.Contains(Instruction.DD)) && inputStarted != 3) || inputStarted == 1 || inputStarted == 2) {
                    if (inputStarted == 0 || inputStarted == 2) {
                        switch (movements[0]) {
                            case Instruction.NULL: inputGoal = -1; break;
                            case Instruction.L: inputGoal = GameHelper.getPiecePositionX(PPT, playerID) - 1; break;
                            case Instruction.R: inputGoal = GameHelper.getPiecePositionX(PPT, playerID) + 1; break;
                            case Instruction.D: inputGoal = GameHelper.getPiecePositionY(PPT, playerID) + 1; break;
                            case Instruction.DROP: inputGoal = 1; break;
                            case Instruction.HOLD: inputGoal = GameHelper.getHoldPointer(PPT, playerID); break;

                            case Instruction.LL:
                                inputGoal = InputHelper.FindInputGoalX(
                                    board,
                                    pieceUsed,
                                    GameHelper.getPiecePositionX(PPT, playerID),
                                    GameHelper.getPiecePositionY(PPT, playerID),
                                    GameHelper.getPieceRotation(PPT, playerID),
                                    -1
                                );

                                if (movements.Count > 1 && movements[1] == Instruction.R) {
                                    inputGoal++;
                                    movements.RemoveAt(1);
                                }
                                break;

                            case Instruction.RR:
                                inputGoal = InputHelper.FindInputGoalX(
                                    board,
                                    pieceUsed,
                                    GameHelper.getPiecePositionX(PPT, playerID),
                                    GameHelper.getPiecePositionY(PPT, playerID),
                                    GameHelper.getPieceRotation(PPT, playerID),
                                    1
                                );

                                if (movements.Count > 1 && movements[1] == Instruction.L) {
                                    inputGoal--;
                                    movements.RemoveAt(1);
                                }
                                break;

                            case Instruction.DD:
                                inputGoal = InputHelper.FindInputGoalY(
                                    board,
                                    pieceUsed,
                                    GameHelper.getPiecePositionX(PPT, playerID),
                                    GameHelper.getPiecePositionY(PPT, playerID),
                                    GameHelper.getPieceRotation(PPT, playerID)
                                );
                                break;

                            case Instruction.LSPIN:
                                inputGoal = GameHelper.getPieceRotation(PPT, playerID) - 1;
                                if (inputGoal < 0) inputGoal = 3;
                                break;
                            case Instruction.RSPIN:
                                inputGoal = GameHelper.getPieceRotation(PPT, playerID) + 1;
                                if (inputGoal > 3) inputGoal = 0;
                                break;
                        }

                        inputStarted = 1;
                    }

                    int inputCurrent = -1;
                    switch (movements[0]) {
                        case Instruction.NULL: inputCurrent = -1; break;
                        case Instruction.L:
                        case Instruction.R:
                        case Instruction.LL:
                        case Instruction.RR: inputCurrent = GameHelper.getPiecePositionX(PPT, playerID); break;
                        case Instruction.D:
                        case Instruction.DD: inputCurrent = GameHelper.getPiecePositionY(PPT, playerID); break;
                        case Instruction.LSPIN:
                        case Instruction.RSPIN: inputCurrent = GameHelper.getPieceRotation(PPT, playerID); break;
                        case Instruction.DROP: inputCurrent = GameHelper.getPieceDropped(PPT, playerID); break;
                        case Instruction.HOLD: inputCurrent = (GameHelper.getHoldPointer(PPT, playerID) != inputGoal && GameHelper.getHoldPointer(PPT, playerID) > 0x08000000) ? inputGoal : 0; break;
                    }

                    if (inputCurrent == inputGoal) {
                        softdrop = false;
                        movements.RemoveAt(0);
                        inputStarted = movements.Count == 0? 0 : 2;
                        processInput();
                        return;

                    } else switch (movements[0]) {
                        case Instruction.L:
                        case Instruction.LL: gamepad.Buttons |= X360Buttons.Left; break;
                        case Instruction.R:
                        case Instruction.RR: gamepad.Buttons |= X360Buttons.Right; break;
                        case Instruction.D:
                        case Instruction.DD: softdrop = true; break;
                        case Instruction.LSPIN: gamepad.Buttons |= X360Buttons.A; break;
                        case Instruction.RSPIN: gamepad.Buttons |= X360Buttons.B; break;
                        case Instruction.DROP: gamepad.Buttons |= X360Buttons.Up; break;
                        case Instruction.HOLD: gamepad.Buttons |= X360Buttons.LeftBumper; break;
                    }

                } else if (inputStarted != 1 && inputStarted != 2) { // Desire mode = faster due to rotation/movement mixing, but can't softdrop/spin
                    int pieceX = GameHelper.getPiecePositionX(PPT, playerID);
                    int pieceR = GameHelper.getPieceRotation(PPT, playerID);

                    if (inputStarted == 0) {
                        desiredX = pieceX;
                        desiredR = pieceR;
                        desiredHold = false;

                        foreach (Instruction i in movements) {
                            switch (i) {
                                case Instruction.L: desiredX--; break;
                                case Instruction.R: desiredX++; break;

                                case Instruction.LL:
                                    desiredX = InputHelper.FindInputGoalX(
                                        board,
                                        pieceUsed,
                                        desiredX,
                                        GameHelper.getPiecePositionY(PPT, playerID),
                                        desiredR,
                                        -1
                                    );
                                    break;

                                case Instruction.RR:
                                    desiredX = InputHelper.FindInputGoalX(
                                        board,
                                        pieceUsed,
                                        desiredX,
                                        GameHelper.getPiecePositionY(PPT, playerID),
                                        desiredR,
                                        1
                                    );
                                    break;

                                case Instruction.LSPIN:
                                    desiredR--;
                                    if (desiredR < 0) desiredR = 3;

                                    if (pieceUsed == 6) {
                                        switch (desiredR) {
                                            case 0: desiredX--; break;
                                            case 2: desiredX++; break;
                                        }
                                    }

                                    desiredX = InputHelper.FixWall(
                                        board,
                                        pieceUsed,
                                        desiredX,
                                        GameHelper.getPiecePositionY(PPT, playerID),
                                        desiredR
                                    );
                                    break;

                                case Instruction.RSPIN:
                                    desiredR++;
                                    if (desiredR > 3) desiredR = 0;

                                    if (pieceUsed == 6) {
                                        switch (desiredR) {
                                            case 1: desiredX++; break;
                                            case 3: desiredX--; break;
                                        }
                                    }

                                    desiredX = InputHelper.FixWall(
                                        board,
                                        pieceUsed,
                                        desiredX,
                                        GameHelper.getPiecePositionY(PPT, playerID),
                                        desiredR
                                    );
                                    break;

                                case Instruction.HOLD: desiredHold = true; break;
                            }
                        }

                        inputStarted = 3;
                    }

                    if (GameHelper.getPieceDropped(PPT, playerID) == 1) {
                        inputStarted = 0;
                        movements.Clear();
                        return;
                    }

                    if (desiredHold) {
                        gamepad.Buttons |= X360Buttons.RightBumper;
                    }

                    if (desiredX == pieceX && desiredR == pieceR) {
                        gamepad.Buttons |= X360Buttons.Up;
                    } else {
                        if (desiredX != pieceX)
                            if (desiredX < pieceX) {
                                gamepad.Buttons |= X360Buttons.Left;
                            } else {
                                gamepad.Buttons |= X360Buttons.Right;
                            }

                        if (desiredR != pieceR)
                            if (desiredR == 3) {
                                gamepad.Buttons |= X360Buttons.A;
                            } else {
                                gamepad.Buttons |= X360Buttons.B;
                            }
                    }
                }
            }
        }

        X360Buttons previousInputs = X360Buttons.None;

        private void applyInputs() {
            int nextFrame = GameHelper.getFrameCount(PPT);

            if (GameHelper.boardAddress(PPT, playerID) != 0x0 && GameHelper.OutsideMenu(PPT) && nextFrame > 0 && GameHelper.getBigFrameCount(PPT) != 0x0) {
                previousInputs = gamepad.Buttons;

                if (nextFrame != frames) {
                    gamepad.Buttons = X360Buttons.None;
                    processInput();
                }

                gamepad.Buttons &= ~previousInputs;
                
                if (softdrop)
                    gamepad.Buttons |= X360Buttons.Down;

                frames = nextFrame;

            } else if (valuePuzzleLeague.Checked) {
                int mode = GameHelper.CurrentMode(PPT);
                gamepad.Buttons = X360Buttons.None;

                if (globalFrames % 2 == 0) {
                    if (GameHelper.OutsideMenu(PPT)) {
                        gamepad.Buttons |= X360Buttons.A;

                    } else if (mode == 4) {
                        if (menuStartFrames + 1150 < globalFrames) {
                            menuStartFrames = globalFrames;
                        }

                        if (menuStartFrames + 1150 < globalFrames) {
                            menuStartFrames = globalFrames;
                        }

                        if (menuStartFrames + 1030 < globalFrames) {
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

            } else if (GameHelper.InMultiplayer(PPT)) {
                gamepad.Buttons = X360Buttons.None;

                if (globalFrames % 2 == 0) {
                    if (GameHelper.OutsideMenu(PPT)) {
                        gamepad.Buttons |= X360Buttons.A;
                    }
                }
            }
            
            scp.Report(4, gamepad.GetReport());
        }


        private void updateUI() {
            buttonGamepad.Text = gamepadPluggedIn? "Disconnect" : "Connect";
            valueGamepadInputs.Text = gamepad.Buttons.ToString();

            valueGameState.Text = PPT.CheckProcess()? (inMatch? "Active (Match)" : "Inactive (Menu)") : "Inactive (Closed)";
                
            valueInstructions.Text = String.Join(", ", movements);
            
            valueMisaMinoLevel.Enabled = valueMisaMinoStyle.Enabled = !inMatch;
        }

        private void valueMisaMino_SelectedIndexChanged(object sender, EventArgs e) {
            MisaMino.Configure(valueMisaMinoLevel.SelectedIndex + 1, valueMisaMinoStyle.SelectedIndex + 1);
        }

        bool checkboxEvents = true;

        private void valuePuzzleLeague_CheckedChanged(object sender, EventArgs e) {
            if (checkboxEvents && inMatch) {
                checkboxEvents = false;
                valuePuzzleLeague.Checked = !valuePuzzleLeague.Checked;
                checkboxEvents = true;
            }
        }

        Stopwatch timer = new Stopwatch();
        int framesSkipped = 0;
        //object locker = new object();
        double waitTime = Math.Round(0.001 * Stopwatch.Frequency);

        private void Loop(object sender, EventArgs e) {
            for (int _ = 0; _ < 20; _++) {
                timer = new Stopwatch();
                timer.Start();

                bool actualFrame = false, logicFrame = false;

                if (PPT.CheckProcess()) {
                    PPT.TrustProcess = true;

                    int prev = globalFrames;
                    globalFrames = GameHelper.getMenuFrameCount(PPT);

                    if (actualFrame = globalFrames > prev) {
                        logicFrame = runLogic();
                        applyInputs();

                        framesSkipped += globalFrames - prev - 1;
                        if (prev < 60) framesSkipped = 0;
                    }

                    PPT.TrustProcess = false;
                }

                updateUI();
                timer.Stop();

                if (actualFrame) {
                    valueFrametime.Text = $"{timer.ElapsedMilliseconds} ms";
                    valueSkipped.Text = framesSkipped.ToString();
                }

                Stopwatch wait = Stopwatch.StartNew();
                while (wait.ElapsedTicks < waitTime) {}
            }
        }

        void MainForm_Load(object sender, EventArgs e) {
            scp.UnplugAll();
            scp.PlugIn(4);
            gamepadPluggedIn = true;

            valueMisaMinoLevel.SelectedIndex = 4;
            valueMisaMinoStyle.SelectedIndex = 0;

            valueMisaMino_SelectedIndexChanged(sender, e);
        }

        void MainForm_Closing(object sender, EventArgs e) {
            scp.UnplugAll();
            gamepadPluggedIn = false;
        }
    }
}