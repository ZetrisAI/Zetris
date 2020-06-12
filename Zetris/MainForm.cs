using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

using Microsoft.VisualBasic;

using ScpDriverInterface;
using MisaMinoNET;

namespace Zetris {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }

        void FocusGame() {
            try {
                Interaction.AppActivate("PuyoPuyoTetris");
            } catch {}
        }

        ScpBus scp = new ScpBus();
        const int gamepadIndex = 4; //! gamepadID
        bool gamepadPluggedIn = false;
        X360Controller gamepad = new X360Controller();

        int getPreviews() => int.MaxValue;

        private void buttonGamepad_Click(object sender, EventArgs e) {
            scp.UnplugAll();
            scp = new ScpBus();
            gamepad = new X360Controller();

            if (!gamepadPluggedIn) scp.PlugIn(4);

            gamepadPluggedIn = !gamepadPluggedIn;
        }

        int frames, globalFrames;

        int[,] board = new int[10, 40];

        int state = 0;
        int piece = 0;
        int[] queue = new int[5];
        bool register = false;

        int currentRating, numplayers;

        bool inMatch = false;
        int menuStartFrames = 0;
        int ratingSafe = 0;

        List<Instruction> movements = new List<Instruction>();
        int pieceUsed;
        bool spinUsed;
        int finalX, finalY, finalR;
        bool shouldHaveRegistered = false;
        int baseBoardHeight;
        int old_y;
        int misa_lasty;
        int atk = 0;

        int[,] misaboard = new int[10, 40];
        bool misasolved = false;

        bool startbreak = false;

        int playerID;

        void runLogic() {
            numplayers = GameHelper.PlayerCount.Call();
            playerID = GameHelper.FindPlayer.Call();

            if (GameHelper.InMultiplayer.Call())
                playerID = 0; //! playerID

            int temp = GameHelper.getRating.Call();

            if (temp != currentRating) {
                ratingSafe = globalFrames;
            }

            currentRating = temp;

            int y = GameHelper.getPiecePositionY.Call(playerID);
            baseBoardHeight = 25 - y;

            board = GameHelper.getBoard.Call(playerID);

            if (GameHelper.boardAddress.Call(playerID) > 0x1000 && GameHelper.OutsideMenu.Call() && GameHelper.getPlayer1Base.Call() > 0x1000) {
                int drop = GameHelper.getPieceDropped.Call(playerID);

                int current = GameHelper.getCurrentPiece.Call(playerID);

                int[] pieces = GameHelper.getPieces.Call(playerID);

                int? hold = GameHelper.getHold.Call(playerID);
                int combo = GameHelper.getCombo.Call(playerID);

                if (drop != state) {
                    if (drop == 1) {
                        register = !shouldHaveRegistered;
                        old_y = y;
                        
                    } else if (drop == 0) shouldHaveRegistered = true;
                }

                if (((register && !pieces.SequenceEqual(queue) && current == queue[0]) || (current != piece && piece == 255)) && y < Math.Max(6, old_y)) {
                    checkManual.Checked = false;

                    shouldHaveRegistered = false;
                    inputStarted = 0;
                    softdrop = false;

                    bool pathSuccess = false;

                    GameHelper.Game.Suspend();
                    ScanTimer.Enabled = false;

                    int[] q = pieces.Concat(GameHelper.getNextFromBags.Call(playerID)).Concat(GameHelper.getNextFromRNG(playerID, 100, 0)).ToArray();

                    while (!pathSuccess) {
                        Dialog ask = new Dialog(
                            board,
                            current,
                            y,
                            hold?? 255,
                            q,
                            null,
                            GameHelper.BagIndex.Call(playerID),
                            InputHelper.FirstGarbage(GameHelper.RNG.Call(playerID))
                        );
                        ask.ShowDialog();

                        pieceUsed = ask.desiredHold? (hold?? q[0]) : current;

                        Console.WriteLine(ask.desiredX);
                        Console.WriteLine(ask.desiredY);
                        Console.WriteLine(ask.desiredR);
                        Console.WriteLine(ask.desiredHold);
                        Console.WriteLine($"current: {current}");
                        Console.WriteLine($"pieceUsed: {pieceUsed}");
                        Console.WriteLine($"current != pieceUsed: {current != pieceUsed}");

                        movements = MisaMino.FindPath(
                            board,
                            baseBoardHeight,
                            pieceUsed,
                            finalX = ask.desiredX,
                            finalY = ask.desiredY,
                            finalR = ask.desiredR,
                            current != pieceUsed,
                            ref spinUsed,
                            out pathSuccess
                        );

                        Console.WriteLine($"movements: {String.Join(", ", movements)}");
                        Console.WriteLine();

                        if (ask.desiredX == -100) pathSuccess = true;
                    }

                    ScanTimer.Enabled = true;
                    GameHelper.Game.Resume();
                    FocusGame();

                    register = false;
                }

                state = drop;
                piece = current;

                if (!register)
                    queue = (int[])pieces.Clone();
            }
        }

        int clear = 0;
        int b2b = 0;
        int inputStarted = 0;
        int inputGoal = -1;
        bool softdrop = false;
        int desiredX, desiredR;

        void processInput() {
            if (movements.Count > 0) {
                if (GameHelper.InSwap.Call() && GameHelper.SwapType.Call() == 0) {
                    softdrop = false;
                    movements.Clear();
                    inputStarted = 0;
                    return;
                }

                int boardHeight = InputHelper.boardHeight(board, baseBoardHeight);

                if (!movements.Contains(Instruction.HOLD) && pieceUsed == 4 && inputStarted == 0 && boardHeight < 16) {
                    if (InputHelper.FixTspinMini(board, baseBoardHeight, finalX, finalY, finalR)) {
                        desiredX = finalX;
                        desiredR = finalR;
                        inputStarted = 3;

                        if (clear > 0) b2b += -1;
                    }
                }

                if (((spinUsed || boardHeight >= 16 || movements.Contains(Instruction.HOLD) || movements.Contains(Instruction.D) || movements.Contains(Instruction.DD)) && inputStarted != 3) || inputStarted == 1 || inputStarted == 2) {
                    if (inputStarted == 0 || inputStarted == 2) {
                        switch (movements[0]) {
                            case Instruction.NULL: inputGoal = -1; break;
                            case Instruction.L: inputGoal = GameHelper.getPiecePositionX.Call(playerID) - 1; break;
                            case Instruction.R: inputGoal = GameHelper.getPiecePositionX.Call(playerID) + 1; break;
                            case Instruction.DROP: inputGoal = 1; break;
                            case Instruction.HOLD: inputGoal = (int)GameHelper.getHoldPointer.Call(playerID); break;

                            case Instruction.D:
                                inputGoal = Math.Min(
                                    GameHelper.getPiecePositionY.Call(playerID) + 1,
                                    InputHelper.FindInputGoalY(
                                        board,
                                        pieceUsed,
                                        GameHelper.getPiecePositionX.Call(playerID),
                                        GameHelper.getPiecePositionY.Call(playerID),
                                        GameHelper.getPieceRotation.Call(playerID)
                                    )
                                );
                                break;

                            case Instruction.LL:
                                inputGoal = InputHelper.FindInputGoalX(
                                    board,
                                    pieceUsed,
                                    GameHelper.getPiecePositionX.Call(playerID),
                                    GameHelper.getPiecePositionY.Call(playerID),
                                    GameHelper.getPieceRotation.Call(playerID),
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
                                    GameHelper.getPiecePositionX.Call(playerID),
                                    GameHelper.getPiecePositionY.Call(playerID),
                                    GameHelper.getPieceRotation.Call(playerID),
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
                                    GameHelper.getPiecePositionX.Call(playerID),
                                    GameHelper.getPiecePositionY.Call(playerID),
                                    GameHelper.getPieceRotation.Call(playerID)
                                );
                                break;

                            case Instruction.LSPIN:
                                inputGoal = GameHelper.getPieceRotation.Call(playerID) - 1;
                                if (inputGoal < 0) inputGoal = 3;
                                break;

                            case Instruction.RSPIN:
                                inputGoal = GameHelper.getPieceRotation.Call(playerID) + 1;
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
                        case Instruction.RR: inputCurrent = GameHelper.getPiecePositionX.Call(playerID); break;
                        case Instruction.D:
                        case Instruction.DD: inputCurrent = GameHelper.getPiecePositionY.Call(playerID); break;
                        case Instruction.LSPIN:
                        case Instruction.RSPIN: inputCurrent = GameHelper.getPieceRotation.Call(playerID); break;
                        case Instruction.DROP: inputCurrent = GameHelper.getPieceDropped.Call(playerID); break;
                        case Instruction.HOLD: inputCurrent = (GameHelper.getHoldPointer.Call(playerID) != inputGoal && GameHelper.getHoldPointer.Call(playerID) > 0x08000000) ? inputGoal : 0; break;
                    }

                    if (inputCurrent == inputGoal || (softdrop && inputCurrent >= inputGoal)) {
                        softdrop = false;
                        int next = (movements[0] == Instruction.HOLD)? 0 : 2;
                        movements.RemoveAt(0);
                        inputStarted = movements.Count == 0 ? 0 : next;
                        processInput();
                        return;

                    } else {
                        switch (movements[0]) {
                            case Instruction.L:
                            case Instruction.LL: gamepad.Buttons |= X360Buttons.Left; break;
                            case Instruction.R:
                            case Instruction.RR: gamepad.Buttons |= X360Buttons.Right; break;
                            case Instruction.D:
                            case Instruction.DD: softdrop = true; break;
                            case Instruction.LSPIN: gamepad.Buttons |= X360Buttons.A; break;
                            case Instruction.RSPIN: gamepad.Buttons |= X360Buttons.B; break;
                            case Instruction.DROP: gamepad.Buttons |= X360Buttons.Up; break;
                            case Instruction.HOLD: gamepad.Buttons |= X360Buttons.RightBumper; break;
                        }

                        if (((movements[0] == Instruction.LSPIN && !previousInputs.HasFlag(X360Buttons.A)) || (movements[0] == Instruction.RSPIN && !previousInputs.HasFlag(X360Buttons.B))) && movements.Count > 1 && movements[1] == Instruction.DROP)
                            gamepad.Buttons |= X360Buttons.Up;
                    }

                } else if (inputStarted != 1 && inputStarted != 2) { // Desire mode = faster due to rotation/movement mixing, but can't softdrop/spin
                    int pieceX = GameHelper.getPiecePositionX.Call(playerID);
                    int pieceR = GameHelper.getPieceRotation.Call(playerID);

                    if (inputStarted == 0) {
                        desiredX = pieceX;
                        desiredR = pieceR;

                        foreach (Instruction i in movements) {
                            switch (i) {
                                case Instruction.L: desiredX--; break;
                                case Instruction.R: desiredX++; break;

                                case Instruction.LL:
                                    desiredX = InputHelper.FindInputGoalX(
                                        board,
                                        pieceUsed,
                                        desiredX,
                                        GameHelper.getPiecePositionY.Call(playerID),
                                        desiredR,
                                        -1
                                    );
                                    break;

                                case Instruction.RR:
                                    desiredX = InputHelper.FindInputGoalX(
                                        board,
                                        pieceUsed,
                                        desiredX,
                                        GameHelper.getPiecePositionY.Call(playerID),
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
                                        GameHelper.getPiecePositionY.Call(playerID),
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
                                        GameHelper.getPiecePositionY.Call(playerID),
                                        desiredR
                                    );
                                    break;
                            }
                        }

                        inputStarted = 3;
                    }

                    if (GameHelper.getPieceDropped.Call(playerID) == 1) {
                        inputStarted = 0;
                        movements.Clear();
                        return;
                    }

                    bool nerd = desiredX == 5 && desiredR % 2 == 1 && pieceUsed == 6;

                    if (nerd) desiredR = 1;

                    if (desiredX == pieceX && desiredR == pieceR) {
                        gamepad.Buttons |= X360Buttons.Up;

                    } else {
                        if (desiredX != pieceX && !nerd)
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

                        if ((desiredX == pieceX || nerd) && desiredR != pieceR && (desiredR == 3 || desiredR - pieceR == 1) && !previousInputs.HasFlag(X360Buttons.A) && !previousInputs.HasFlag(X360Buttons.B)) {
                            gamepad.Buttons |= X360Buttons.Up;
                        }
                    }
                }
            }
        }

        X360Buttons previousInputs = X360Buttons.None;
        decimal speedTick = 0;

        int charindex = 0;

        void applyInputs() {
            if (!checkManual.Checked) {
                int nextFrame = GameHelper.getFrameCount.Call();

                bool addDown = false;

                if (GameHelper.boardAddress.Call(playerID) > 0x1000 && GameHelper.OutsideMenu.Call() && nextFrame > 0 && GameHelper.getPlayer1Base.Call() > 0x1000 && GameHelper.GameEnd.Call() != 16 && GameHelper.GameEnd.Call() != 36) {
                    if (nextFrame != frames) {
                        gamepad.Buttons = X360Buttons.None;
                        processInput();
                    }

                    addDown = softdrop;
                    frames = nextFrame;

                } else
                    gamepad.Buttons = X360Buttons.None;

                if (gamepad.Buttons.HasFlag(X360Buttons.RightBumper))
                    gamepad.Buttons = X360Buttons.RightBumper;

                gamepad.Buttons &= ~previousInputs;

                if (addDown)
                    gamepad.Buttons |= X360Buttons.Down;

                previousInputs = gamepad.Buttons;
            }

            scp.Report(gamepadIndex, gamepad.GetReport());
        }

        private void updateUI() {
            buttonGamepad.Text = gamepadPluggedIn? "Disconnect" : "Connect";
            valueGamepadInputs.Text = gamepad.Buttons.ToString();
        }
                
        Stopwatch timer = new Stopwatch();
        int framesSkipped = 0;
        readonly double waitTime = Math.Round(0.001 * Stopwatch.Frequency);

        private void Loop(object sender, EventArgs e) {
            for (int _ = 0; _ < 20; _++) {
                timer = new Stopwatch();
                timer.Start();

                bool actualFrame = false;

                if (GameHelper.CheckProcess()) {
                    GameHelper.TrustProcess = true;

                    int prev = globalFrames;
                    globalFrames = GameHelper.getMenuFrameCount();

                    if (actualFrame = globalFrames > prev) {
                        CachedMethod.InvalidateAll();

                        runLogic();
                        applyInputs();

                        framesSkipped += globalFrames - prev - 1;
                        if (prev < 60) framesSkipped = 0;
                    }

                    GameHelper.TrustProcess = false;
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

        private void buttonResume_Click(object sender, EventArgs e) {
            GameHelper.Game.Resume();
            FocusGame();
        }

        System.Threading.Timer btnTimer;

        private void buttonManual_Click(object sender, EventArgs e) {
            if (!(sender is Button btn)) return;
            if (!(btn.Tag is X360Buttons btns)) return;

            checkManual.Checked = true;

            gamepad.Buttons = btns;
            scp.Report(gamepadIndex, gamepad.GetReport());
            FocusGame();
            btn.Enabled = false;

            btnTimer?.Dispose();
            btnTimer = new System.Threading.Timer(
                new TimerCallback(i =>
                    Invoke(new Action(() => {
                        gamepad.Buttons = X360Buttons.None;
                        scp.Report(gamepadIndex, gamepad.GetReport());
                        FocusGame();
                        btn.Enabled = true;
                    }))
                ),
                null,
                50,
                Timeout.Infinite
            );
        }

        void MainForm_Load(object sender, EventArgs e) {
            scp.PlugIn(gamepadIndex);
            gamepadPluggedIn = true;
        }

        void MainForm_Closing(object sender, EventArgs e) {
            gamepadPluggedIn = false;
        }
    }
}