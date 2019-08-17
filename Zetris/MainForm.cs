using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

using MisaMinoNET;
using PerfectClearNET;
using ScpDriverInterface;

namespace Zetris {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }

        int playerID = 0;
        ProcessMemory PPT = new ProcessMemory("puyopuyotetris", false);

        void ResetGame() {
            if (!GameHelper.InSwap(PPT) || !valuePuzzleLeague.Checked) return;

            Process.Start("steam://joinlobby/546050/109775241058543776/76561198802063829");
        }

        int gamepadIndex = 4;
        ScpBus scp = new ScpBus();
        bool gamepadPluggedIn = false;
        X360Controller gamepad = new X360Controller();

        private void buttonGamepad_Click(object sender, EventArgs e) {
            scp.Unplug(gamepadIndex);
            scp = new ScpBus();
            gamepad = new X360Controller();

            if (!gamepadPluggedIn) scp.PlugIn(gamepadIndex);

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
        int finalX, finalY, finalR;
        int[] queue = new int[5];
        bool register = false;
        bool shouldHaveRegistered = false;
        int baseBoardHeight;
        int old_y;

        int[,] pcboard;
        bool pcsolved = false;

        private bool runLogic() {
            bool ret = false;

            numplayers = GameHelper.getPlayerCount(PPT);
            playerID = GameHelper.FindPlayer(PPT);

            if (GameHelper.InMultiplayer(PPT))
                playerID = valueMPPlayer.SelectedIndex;
            
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
                for (int j = 0; j < 28; j++) {
                    board[i, j] = PPT.ReadByte(new IntPtr(columnAddress + j * 0x04));
                }
            }

            if (GameHelper.OutsideMenu(PPT) && GameHelper.CurrentMode(PPT) == 4 && numplayers < 2 && GameHelper.boardAddress(PPT, playerID) == 0x0 && ratingSafe + 1500 < GameHelper.getMenuFrameCount(PPT)) {
                ResetGame();                
                return false;
            }

            if (GameHelper.boardAddress(PPT, playerID) != 0x0 && GameHelper.OutsideMenu(PPT) && GameHelper.getBigFrameCount(PPT) > 1 && !GameHelper.IsReplay(PPT)) {
                if (numplayers < 2 && GameHelper.CurrentMode(PPT) == 4 && GameHelper.Online(PPT)) {
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
                    speedTick = 0;

                    PerfectClear.Abort();
                    pcsolved = false;

                    if (valueFinderEnable.Checked) {
                        pcboard = (int[,])board.Clone();
                        PerfectClear.Find(
                            pcboard, pieces.Skip(1).Concat(GameHelper.getNextFromBags(PPT, playerID)).ToArray(), pieces[0], 
                            null, valueMisaMinoStyle.SelectedIndex != 3, GameHelper.InSwap(PPT), 0
                        );
                    }
                }

                if (drop != state) {
                    if (drop == 1) {
                        register = !shouldHaveRegistered;
                        old_y = y;
                    } else if (drop == 0) shouldHaveRegistered = true;
                }

                if (((register && !pieces.SequenceEqual(queue) && current == queue[0]) || (current != piece && piece == 255)) && y < Math.Max(6, old_y)) {
                    shouldHaveRegistered = false;
                    inputStarted = 0;
                    softdrop = false;

                    int? hold = GameHelper.getHold(PPT, playerID);
                    int combo = GameHelper.getCombo(PPT, playerID);

                    bool pathSuccess = false;

                    if (PerfectClear.Running) PerfectClear.Abort();

                    if (valueFinderEnable.Checked && pcsolved && InputHelper.BoardEquals(board, pcboard)) {
                        pieceUsed = PerfectClear.LastSolution[0].Piece;
                        finalX = PerfectClear.LastSolution[0].X;
                        int misaY = finalY = PerfectClear.LastSolution[0].Y;
                        finalR = PerfectClear.LastSolution[0].R;

                        do {
                            movements = MisaMino.FindPath(
                                board,
                                baseBoardHeight,
                                pieceUsed,
                                finalX,
                                misaY,
                                finalR,
                                current != pieceUsed,
                                ref spinUsed,
                                out pathSuccess
                            );
                        } while (!(pathSuccess || --misaY < 3));
                    }

                    if (!pathSuccess) {
                        movements = MisaMino.FindMove(
                            pieces,
                            current,
                            hold,
                            baseBoardHeight,
                            board,
                            combo,
                            GameHelper.getGarbageOverhead(PPT, playerID),
                            ref pieceUsed,
                            ref spinUsed,
                            ref finalX,
                            ref finalY,
                            ref finalR
                        );

                        pcsolved = false;

                    } else {
                        PerfectClear.LastSolution = PerfectClear.LastSolution.Skip(1).ToList();

                        if (PerfectClear.LastSolution.Count == 0)
                            pcsolved = false;
                    }

                    if (valueFinderEnable.Checked) {
                        pcboard = (int[,])board.Clone();

                        bool fuck = false;
                        try {
                            InputHelper.ApplyPiece(pcboard, pieceUsed, finalX, finalY, finalR);
                        } catch {
                            fuck = true;
                        }

                        if (movements.Count > 0 && !pcsolved && !fuck) {
                            int start = Convert.ToInt32(hold == null && movements[0] == Instruction.HOLD);

                            PerfectClear.Find(
                                pcboard, pieces.Skip(start + 1).Concat(GameHelper.getNextFromBags(PPT, playerID)).ToArray(), pieces[start],
                                (movements[0] == Instruction.HOLD) ? current : hold, valueMisaMinoStyle.SelectedIndex != 3, GameHelper.InSwap(PPT), combo
                            );
                        }
                    }

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

                int boardHeight = InputHelper.boardHeight(board, baseBoardHeight);

                if (pieceUsed == 4 && inputStarted == 0 && boardHeight < 16) {
                    if (InputHelper.FixTspinMini(board, baseBoardHeight, finalX, finalY, finalR)) {
                        desiredX = finalX;
                        desiredR = finalR;
                        desiredHold = movements.Contains(Instruction.HOLD);
                        inputStarted = 3;
                    }
                }

                if (((spinUsed || boardHeight >= 16 || movements.Contains(Instruction.D) || movements.Contains(Instruction.DD)) && inputStarted != 3) || inputStarted == 1 || inputStarted == 2) {
                    if (inputStarted == 0 || inputStarted == 2) {
                        switch (movements[0]) {
                            case Instruction.NULL: inputGoal = -1; break;
                            case Instruction.L: inputGoal = GameHelper.getPiecePositionX(PPT, playerID) - 1; break;
                            case Instruction.R: inputGoal = GameHelper.getPiecePositionX(PPT, playerID) + 1; break;
                            case Instruction.DROP: inputGoal = 1; break;
                            case Instruction.HOLD: inputGoal = GameHelper.getHoldPointer(PPT, playerID); break;

                            case Instruction.D:
                                inputGoal = Math.Min(
                                    GameHelper.getPiecePositionY(PPT, playerID) + 1,
                                    InputHelper.FindInputGoalY(
                                        board,
                                        pieceUsed,
                                        GameHelper.getPiecePositionX(PPT, playerID),
                                        GameHelper.getPiecePositionY(PPT, playerID),
                                        GameHelper.getPieceRotation(PPT, playerID)
                                    )
                                );
                                break;

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

                    if (inputCurrent == inputGoal || (softdrop && inputCurrent >= inputGoal)) {
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

                        if (desiredX == pieceX && desiredR != pieceR && (desiredR == 3 || desiredR - pieceR == 1) && !previousInputs.HasFlag(X360Buttons.A) && !previousInputs.HasFlag(X360Buttons.B)) {
                            gamepad.Buttons |= X360Buttons.Up;
                        }
                    }
                }
            }
        }

        X360Buttons previousInputs = X360Buttons.None;
        decimal speedIncrement = 1;
        decimal speedTick = 0;

        int charindex = 0;

        private void applyInputs() {
            int nextFrame = GameHelper.getFrameCount(PPT);
            
            bool addDown = false;

            if (GameHelper.boardAddress(PPT, playerID) != 0x0 && GameHelper.OutsideMenu(PPT) && nextFrame > 0 && GameHelper.getBigFrameCount(PPT) != 0x0) {
                if (nextFrame != frames) {
                    gamepad.Buttons = X360Buttons.None;
                    processInput();
                }

                addDown = softdrop;
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
                        if (GameHelper.InMultiplayer(PPT)) {
                            if (GameHelper.CharSelectIndex(PPT, playerID) == 13) {
                                gamepad.Buttons |= X360Buttons.A;
                            } else {
                                gamepad.Buttons |= ((charindex = ++charindex % 5) == 0) ? X360Buttons.Down : X360Buttons.Right;
                            }
                        } else gamepad.Buttons |= X360Buttons.A;
                    }
                }
            }
            
            speedTick += speedIncrement;

            if (speedTick < 1 && inMatch) {
                gamepad.Buttons = X360Buttons.None;

            } else {
                if (inMatch) speedTick += -1;
                gamepad.Buttons &= ~previousInputs;

                if (addDown)
                    gamepad.Buttons |= X360Buttons.Down;

                previousInputs = gamepad.Buttons;
            }

            scp.Report(gamepadIndex, gamepad.GetReport());
        }


        private void updateUI() {
            buttonGamepad.Text = gamepadPluggedIn? Properties.Strings.GamepadConnected : Properties.Strings.GamepadDisconnected;
            valueGamepadInputs.Text = gamepad.Buttons.ToString();

            valueGameState.Text = PPT.CheckProcess()? (inMatch? Properties.Strings.GameStateMatch : Properties.Strings.GameStateMenu) : Properties.Strings.GameStateClosed;
                
            valueInstructions.Text = String.Join(", ", movements);
            
            valueMisaMinoStyle.Enabled = valueMPPlayer.Enabled = !inMatch;

            valueFinderSolved.Text = (valueFinderEnable.Checked && inMatch && pcsolved) ? $"{PerfectClear.LastSolution.Count}" : "...";
        }

        private void valueMisaMino_SelectedIndexChanged(object sender, EventArgs e) {
            MisaMino.Configure(valueMisaMinoStyle.SelectedIndex + 1, valueMisaMino4w.Checked);
        }

        bool checkboxEvents = true;

        private void valuePuzzleLeague_CheckedChanged(object sender, EventArgs e) {
            if (checkboxEvents && inMatch) {
                checkboxEvents = false;
                valuePuzzleLeague.Checked = !valuePuzzleLeague.Checked;
                checkboxEvents = true;
            }
        }

        private void valueFinderEnable_CheckedChanged(object sender, EventArgs e) {
            if (checkboxEvents && inMatch) {
                checkboxEvents = false;
                valueFinderEnable.Checked = !valueFinderEnable.Checked;
                checkboxEvents = true;
            }
        }

        private void valueMisaMino4w_CheckedChanged(object sender, EventArgs e) {
            if (inMatch) {
                if (checkboxEvents) {
                    checkboxEvents = false;
                    valueMisaMino4w.Checked = !valueMisaMino4w.Checked;
                    checkboxEvents = true;
                }
            } else valueMisaMino_SelectedIndexChanged(sender, e);
        }

        private void valueSpeed_MouseWheel(object sender, MouseEventArgs e) {
            if (!inMatch) {
                speedIncrement += (decimal)((e.Delta < 0) ? -0.01 : 0.01);
                speedIncrement = Math.Min(1, speedIncrement);
                speedIncrement = Math.Max(0.1M, speedIncrement);
                valueSpeed.Text = $"{Math.Round(speedIncrement * 100)}%";
            }
        }

        Stopwatch timer = new Stopwatch();
        int framesSkipped = 0;
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
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 2 && args[1] == "--gamepadIndex" && int.TryParse(args[2], out int index) && 1 <= index && index <= 4) {
                gamepadIndex = index;
                this.Text = args[2];
            }

            scp.UnplugAll();

            scp = new ScpBus();
            scp.PlugIn(gamepadIndex);
            gamepadPluggedIn = true;
            
            valueMisaMinoStyle.SelectedIndex = 0;

            valueMisaMino_SelectedIndexChanged(sender, e);

            valueMPPlayer.SelectedIndex = 1;

            PerfectClear.Finished += (bool success) => {
                pcsolved = success;
            };
        }

        void MainForm_Closing(object sender, EventArgs e) {
            scp.UnplugAll();
            gamepadPluggedIn = false;
        }
    }
}