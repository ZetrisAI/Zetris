using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MisaMinoNET;
using PerfectClearNET;
using ScpDriverInterface;

namespace Zetris {
    public static class Bot {
        const int rngsearch_max = 1000;

        static UI Window = null;
        static int playerID = 0;

        public static string[] Args;

        static void ResetGame() {
#if !PUBLIC
            if (GameHelper.InSwap() || !Preferences.Auto) return;

            Process.Start("steam://joinlobby/546050/109775241058543776/76561198802063829");

            Stopwatch resetting = new Stopwatch();
            resetting.Start();

            while (resetting.ElapsedMilliseconds < 7000) { }
#endif
        }

        static int gamepadIndex = 4;
        static ScpBus scp = new ScpBus();
        static X360Controller gamepad = new X360Controller();

        public static void SetGamepad(bool state) {
            scp.Unplug(gamepadIndex);
            scp = new ScpBus();
            gamepad = new X360Controller();

            if (state) scp.PlugIn(gamepadIndex);
        }

        static int currentRating, numplayers, frames, globalFrames;

        static int[,] board = new int[10, 40];

        static bool inMatch = false;
        static int menuStartFrames = 0;
        static int ratingSafe = 0;

        static List<Instruction> movements = new List<Instruction>();
        static int state = 0;
        static int piece = 0;
        static int pieceUsed;
        static bool spinUsed;
        static int finalX, finalY, finalR;
        static int[] queue = new int[5];
        static bool register = false;
        static bool shouldHaveRegistered = false;
        static int baseBoardHeight;
        static int old_y;
        static int atk = 0;

        static int[,] misaboard = new int[10, 40];
        static bool misasolved = false;

        static int[,] pcboard = new int[10, 40];
        static bool pcsolved = false;

        static int startbreak = 0;

        static bool danger =>
#if PUBLIC
            GameHelper.Online() || (GameHelper.LobbyPtr() != 0);
#else
            false;
#endif

        static int getPreviews() => (Preferences.Previews > 18)? int.MaxValue : Preferences.Previews;

        static void misaPrediction(int current, int[] q, int? hold, int combo, int cleared) {
            if (!GameHelper.InSwap() || cleared == 0) 
                InputHelper.AddGarbage(
                    misaboard,
                    GameHelper.RNG(playerID),
                    GameHelper.CalculateGarbage(playerID, atk, out int garbage_left)
                );

            if (MisaMino.Running) MisaMino.Abort();

            misasolved = false;

            if (!danger)
                MisaMino.FindMove(
                    q,
                    current,
                    hold,
                    21 + Convert.ToInt32(!InputHelper.FitPieceWithConvert(misaboard, current, 4, 4, 0)),
                    misaboard,
                    combo + Convert.ToInt32(cleared > 0),
                    b2b,
                    0 // garbage_left TODO Zetris-21 stress test this
                );
        }

        static bool runLogic() {
            bool ret = false;

            numplayers = GameHelper.getPlayerCount();
            playerID = GameHelper.FindPlayer();

            if (GameHelper.InMultiplayer())
                playerID = Preferences.Player;

            int temp = GameHelper.getRating();

            if (temp != currentRating) {
                ratingSafe = GameHelper.getMenuFrameCount();
            }

            currentRating = temp;

            int y = GameHelper.getPiecePositionY(playerID);
            baseBoardHeight = 25 - y;

            board = GameHelper.getBoard(playerID);

            if (GameHelper.OutsideMenu() && GameHelper.CurrentMode() == 4 && numplayers < 2 && GameHelper.boardAddress(playerID) == 0x0 && ratingSafe + 1500 < GameHelper.getMenuFrameCount()) {
                ResetGame();
                return false;
            }

            if (GameHelper.boardAddress(playerID) != 0x0 && GameHelper.OutsideMenu() && GameHelper.getBigFrameCount() > 1) {
                if (numplayers < 2 && GameHelper.CurrentMode() == 4 && GameHelper.Online()) {
                    ResetGame();
                    return false;
                }

                int drop = GameHelper.getPieceDropped(playerID);

                int current = GameHelper.getCurrentPiece(playerID);

                int[] pieces = GameHelper.getPieces(playerID);

                if (GameHelper.getBigFrameCount() < 15) startbreak = 0;
                else if (GameHelper.getBigFrameCount() < 40) {
                    if (++startbreak == 10) {
                        MisaMino.Reset(); // this will abort as well
                        misasolved = false;
                        b2b = 0;
                        atk = 0;
                        register = false;
                        movements.Clear();
                        inputStarted = 0;
                        softdrop = false;
                        speedTick = 0;

                        PerfectClear.Abort();
                        pcsolved = false;

                        misaboard = (int[,])board.Clone();
                        pcboard = (int[,])board.Clone();

                        int[] q = pieces.Skip(1).Concat(GameHelper.getNextFromBags(playerID)).Concat(GameHelper.getNextFromRNG(playerID, rngsearch_max, 0)).ToArray();
                        q = q.Take(Math.Min(q.Length, getPreviews())).ToArray();

                        if (!danger) {
                            LogHelper.LogText("Start");
                            MisaMino.FindMove(q, pieces[0], null, 21, pcboard, 0, b2b, 0);

                            if (Preferences.PerfectClear) {
                                PerfectClear.Find(
                                    pcboard, q, pieces[0],
                                    null, Preferences.HoldAllowed, 6, GameHelper.InSwap(), 0
                                );
                            }
                        }
                    }
                }

                int? hold = GameHelper.getHold(playerID);
                int combo = GameHelper.getCombo(playerID);

                if (drop != state) {
                    if (drop == 1) {
                        register = !shouldHaveRegistered;
                        old_y = y;

                        int[,] clearedboard = (int[,])board.Clone();
                        InputHelper.ClearLines(clearedboard, out int cleared);

                        if (!InputHelper.BoardEquals(misaboard, clearedboard)) {
                            LogHelper.LogText("ARE");
                            LogHelper.LogBoard(misaboard, clearedboard);

                            misaboard = clearedboard;

                            int[] q = pieces.Skip(1).Concat(GameHelper.getNextFromBags(playerID)).Concat(GameHelper.getNextFromRNG(playerID, rngsearch_max, atk)).ToArray();
                            q = q.Take(Math.Min(q.Length, getPreviews())).ToArray();

                            misaPrediction(pieces[0], q, hold, combo, cleared);
                        }

                    } else if (drop == 0) shouldHaveRegistered = true;
                }

                if (((register && !pieces.SequenceEqual(queue) && current == queue[0]) || (current != piece && piece == 255)) && y < Math.Max(6, old_y)) {
                    shouldHaveRegistered = false;
                    inputStarted = 0;
                    softdrop = false;

                    bool pathSuccess = false;

                    if (MisaMino.Running) MisaMino.Abort();
                    if (PerfectClear.Running) PerfectClear.Abort();

                    if (!danger) {
                        if (Preferences.PerfectClear && pcsolved && InputHelper.BoardEquals(board, pcboard)) {
                            LogHelper.LogText("Detected PC");

                            pieceUsed = PerfectClear.LastSolution[0].Piece;
                            finalX = PerfectClear.LastSolution[0].X;
                            finalY = PerfectClear.LastSolution[0].Y;
                            finalR = PerfectClear.LastSolution[0].R;

                            do {
                                movements = MisaMino.FindPath(
                                    board,
                                    baseBoardHeight,
                                    pieceUsed,
                                    finalX,
                                    finalY,
                                    finalR,
                                    current != pieceUsed,
                                    ref spinUsed,
                                    out pathSuccess
                                );
                            } while (!(pathSuccess || --finalY < 3));
                        }

                        if (!pathSuccess) {
                            LogHelper.LogText("Using Misa!");

                            if (!InputHelper.BoardEquals(misaboard, board) || !misasolved) {
                                int[] q = pieces.Concat(GameHelper.getNextFromBags(playerID)).ToArray();
                                q = q.Take(Math.Min(q.Length, getPreviews())).ToArray();

                                LogHelper.LogText("Rush");
                                MisaMino.FindMove(
                                    q,
                                    current,
                                    hold,
                                    baseBoardHeight,
                                    board,
                                    combo,
                                    b2b,
                                    0
                                );

                                Stopwatch misasearching = new Stopwatch();
                                misasearching.Start();

                                while (misasearching.ElapsedMilliseconds < 12) {}

                                MisaMino.Abort();
                            }

                            movements = MisaMino.LastSolution.Instructions;
                            pieceUsed = MisaMino.LastSolution.PieceUsed;
                            spinUsed = MisaMino.LastSolution.SpinUsed;
                            b2b = MisaMino.LastSolution.B2B;
                            atk = MisaMino.LastSolution.Attack;
                            finalX = MisaMino.LastSolution.FinalX;
                            finalY = MisaMino.LastSolution.FinalY;
                            finalR = MisaMino.LastSolution.FinalR;

                            Window?.SetConfidence($"{MisaMino.LastSolution.Nodes} ({MisaMino.LastSolution.Depth})");
                            Window?.SetThinkingTime(MisaMino.LastSolution.Time);

                            pcsolved = false;

                        } else {
                            LogHelper.LogText("Using PC!");
                            PerfectClear.LastSolution = PerfectClear.LastSolution.Skip(1).ToList();

                            if (PerfectClear.LastSolution.Count == 0)
                                pcsolved = false;

                            Window?.SetConfidence($"[PC] {PerfectClear.LastSolution.Count + 1}");
                            Window?.SetThinkingTime(PerfectClear.LastTime);
                        }

                        misasolved = false;

                        bool wasHold = movements.Count > 0 && movements[0] == Instruction.HOLD;

                        misaboard = (int[,])board.Clone();

                        bool fuck = false;
                        try {
                            InputHelper.ApplyPiece(misaboard, pieceUsed, finalX, finalY, finalR, out clear);
                        } catch {
                            fuck = true;

                            LogHelper.LogText("FUCK");
                        }

                        if (!fuck) {
                            int start = Convert.ToInt32(wasHold && hold == null);

                            int[] q = pieces.Skip(start + 1).Concat(GameHelper.getNextFromBags(playerID)).Concat(GameHelper.getNextFromRNG(playerID, rngsearch_max, atk)).ToArray();
                            q = q.Take(Math.Min(q.Length, getPreviews())).ToArray();

                            int futureCurrent = pieces[start];
                            int? futureHold = wasHold? current : hold;
                            int futureCombo = combo + Convert.ToInt32(clear > 0);

                            LogHelper.LogText("AOT");
                            misaPrediction(futureCurrent, q, futureHold, futureCombo, clear);

                            pcboard = (int[,])misaboard.Clone();

                            if (Preferences.PerfectClear && movements.Count > 0 && !pcsolved)
                                PerfectClear.Find(
                                    pcboard, q, futureCurrent,
                                    futureHold, Preferences.HoldAllowed, 6, GameHelper.InSwap(), futureCombo
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

                    menuStartFrames = GameHelper.getMenuFrameCount();

                    MisaMino.Abort();
                    PerfectClear.Abort();
                }
            }

            return ret;
        }

        static int clear = 0;
        static int b2b = 0;
        static int inputStarted = 0;
        static int inputGoal = -1;
        static bool softdrop = false;
        static int desiredX, desiredR;
        static bool desiredHold;

        static void processInput() {
            if (movements.Count > 0) {
                if (GameHelper.InSwap() && GameHelper.SwapType() == 0) {
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

                        if (clear > 0) b2b += -1;
                    }
                }

                if (((spinUsed || boardHeight >= 16 || movements.Contains(Instruction.D) || movements.Contains(Instruction.DD)) && inputStarted != 3) || inputStarted == 1 || inputStarted == 2) {
                    if (inputStarted == 0 || inputStarted == 2) {
                        switch (movements[0]) {
                            case Instruction.NULL: inputGoal = -1; break;
                            case Instruction.L: inputGoal = GameHelper.getPiecePositionX(playerID) - 1; break;
                            case Instruction.R: inputGoal = GameHelper.getPiecePositionX(playerID) + 1; break;
                            case Instruction.DROP: inputGoal = 1; break;
                            case Instruction.HOLD: inputGoal = GameHelper.getHoldPointer(playerID); break;

                            case Instruction.D:
                                inputGoal = Math.Min(
                                    GameHelper.getPiecePositionY(playerID) + 1,
                                    InputHelper.FindInputGoalY(
                                        board,
                                        pieceUsed,
                                        GameHelper.getPiecePositionX(playerID),
                                        GameHelper.getPiecePositionY(playerID),
                                        GameHelper.getPieceRotation(playerID)
                                    )
                                );
                                break;

                            case Instruction.LL:
                                inputGoal = InputHelper.FindInputGoalX(
                                    board,
                                    pieceUsed,
                                    GameHelper.getPiecePositionX(playerID),
                                    GameHelper.getPiecePositionY(playerID),
                                    GameHelper.getPieceRotation(playerID),
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
                                    GameHelper.getPiecePositionX(playerID),
                                    GameHelper.getPiecePositionY(playerID),
                                    GameHelper.getPieceRotation(playerID),
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
                                    GameHelper.getPiecePositionX(playerID),
                                    GameHelper.getPiecePositionY(playerID),
                                    GameHelper.getPieceRotation(playerID)
                                );
                                break;

                            case Instruction.LSPIN:
                                inputGoal = GameHelper.getPieceRotation(playerID) - 1;
                                if (inputGoal < 0) inputGoal = 3;
                                break;
                            case Instruction.RSPIN:
                                inputGoal = GameHelper.getPieceRotation(playerID) + 1;
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
                        case Instruction.RR: inputCurrent = GameHelper.getPiecePositionX(playerID); break;
                        case Instruction.D:
                        case Instruction.DD: inputCurrent = GameHelper.getPiecePositionY(playerID); break;
                        case Instruction.LSPIN:
                        case Instruction.RSPIN: inputCurrent = GameHelper.getPieceRotation(playerID); break;
                        case Instruction.DROP: inputCurrent = GameHelper.getPieceDropped(playerID); break;
                        case Instruction.HOLD: inputCurrent = (GameHelper.getHoldPointer(playerID) != inputGoal && GameHelper.getHoldPointer(playerID) > 0x08000000) ? inputGoal : 0; break;
                    }

                    if (inputCurrent == inputGoal || (softdrop && inputCurrent >= inputGoal)) {
                        softdrop = false;
                        movements.RemoveAt(0);
                        inputStarted = movements.Count == 0 ? 0 : 2;
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
                            case Instruction.HOLD: gamepad.Buttons |= X360Buttons.LeftBumper; break;
                        }

                        if (((movements[0] == Instruction.LSPIN && !previousInputs.HasFlag(X360Buttons.A)) || (movements[0] == Instruction.RSPIN && !previousInputs.HasFlag(X360Buttons.B))) && movements.Count > 1 && movements[1] == Instruction.DROP)
                            gamepad.Buttons |= X360Buttons.Up;
                    }

                } else if (inputStarted != 1 && inputStarted != 2) { // Desire mode = faster due to rotation/movement mixing, but can't softdrop/spin
                    int pieceX = GameHelper.getPiecePositionX(playerID);
                    int pieceR = GameHelper.getPieceRotation(playerID);

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
                                        GameHelper.getPiecePositionY(playerID),
                                        desiredR,
                                        -1
                                    );
                                    break;

                                case Instruction.RR:
                                    desiredX = InputHelper.FindInputGoalX(
                                        board,
                                        pieceUsed,
                                        desiredX,
                                        GameHelper.getPiecePositionY(playerID),
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
                                        GameHelper.getPiecePositionY(playerID),
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
                                        GameHelper.getPiecePositionY(playerID),
                                        desiredR
                                    );
                                    break;

                                case Instruction.HOLD: desiredHold = true; break;
                            }
                        }

                        inputStarted = 3;
                    }

                    if (GameHelper.getPieceDropped(playerID) == 1) {
                        inputStarted = 0;
                        movements.Clear();
                        return;
                    }

                    if (desiredHold) {
                        gamepad.Buttons |= X360Buttons.RightBumper;
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

        static X360Buttons previousInputs = X360Buttons.None;
        static decimal speedTick = 0;

        static int charindex = 0;

        static void applyInputs() {
            int nextFrame = GameHelper.getFrameCount();

            bool addDown = false;

            if (GameHelper.boardAddress(playerID) != 0x0 && GameHelper.OutsideMenu() && nextFrame > 0 && GameHelper.getBigFrameCount() != 0x0) {
                if (nextFrame != frames) {
                    gamepad.Buttons = X360Buttons.None;
                    processInput();
                }

                addDown = softdrop;
                frames = nextFrame;

            } else if (Preferences.Auto) {
                int mode = GameHelper.CurrentMode();
                gamepad.Buttons = X360Buttons.None;

                if (globalFrames % 2 == 0) {
                    if (GameHelper.OutsideMenu()) {
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
                        if (GameHelper.MenuHighlighted() != 4) {
                            gamepad.Buttons |= X360Buttons.Down;
                        } else {
                            gamepad.Buttons |= X360Buttons.A;
                        }
                    }
                }

            } else if (GameHelper.InMultiplayer()) {
                gamepad.Buttons = X360Buttons.None;

                if (globalFrames % 2 == 0 && GameHelper.OutsideMenu()) {
                    if (!GameHelper.IsCharacterSelect()) {
                        bool yes = true;
                        if (Preferences.SaveReplay && GameHelper.CanSaveReplay() == 0) {
                            if (GameHelper.MenuNavigation(0) == 25 && GameHelper.MenuNavigation(1) == 1) {  //end of match
                                if (GameHelper.MenuNavigation(2) != 0) {                                    //not default position
                                    if (GameHelper.ConfirmingReplay() == 1) {                               //in replay confrim sub menu
                                        gamepad.Buttons |= (GameHelper.ReplayMenuSelection() == 1) ? X360Buttons.A : X360Buttons.Right;
                                    }
                                }
                                else
                                {
                                    gamepad.Buttons |= X360Buttons.Up;
                                    yes = false;
                                }
                            }
                        }

                        if (yes) {
                            gamepad.Buttons |= X360Buttons.A;
                        }
                    }

                    else if (GameHelper.CharSelectIndex(playerID) == 13)
                        gamepad.Buttons |= X360Buttons.A;

                    else if (GameHelper.CharacterSelectState(playerID) > 1)
                        gamepad.Buttons |= X360Buttons.B;

                    else gamepad.Buttons |= ((charindex = ++charindex % 5) == 0) ? X360Buttons.Down : X360Buttons.Right;
                }
            }

            speedTick += Preferences.Speed / 100M;

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

        static void updateUI() {
            Window?.SetActive(inMatch);
        }

        public static void UpdateConfig() {
            if (!Started) return;

            MisaMinoParameters param = Preferences.CurrentStyle.Clone().Parameters;
            param.Parameters.strategy_4w = 400 * Convert.ToInt32(Preferences.C4W);

            MisaMino.Configure(param, Preferences.HoldAllowed, Preferences.TSDOnly);
        }

        static void Loop() {
            Thread.CurrentThread.Priority = ThreadPriority.AboveNormal;

            while (!Disposing) {
                bool newFrame = false;

                if (GameHelper.CheckProcess()) {
                    GameHelper.TrustProcess = true;

                    int prev = globalFrames;
                    globalFrames = GameHelper.getMenuFrameCount();

                    if (newFrame = globalFrames > prev) {
                        if (globalFrames != prev + 1)
                            LogHelper.LogText("Skipped " + (globalFrames - prev - 1) + " frames");

                        runLogic();
                        applyInputs();
                    }

                    GameHelper.TrustProcess = false;
                }

                updateUI();

                if (!newFrame)
                    Thread.Sleep(10);
            }

            Disposed = true;
        }

        public static bool Started { get; private set; } = false;

        public static void Start(UI window) {
            if (Started) return;

            MisaMino.Finished += (bool success) => {
                misasolved = success;
            };

            PerfectClear.Finished += (bool success) => {
                pcsolved = success;
            };

            Window = window;

            if (Args.Length > 1 && Args[0] == "--gamepadIndex" && int.TryParse(Args[1], out int index) && 1 <= index && index <= 4) {
                gamepadIndex = index;
                Window?.SetGamepadIndex(gamepadIndex);
            }

            scp.UnplugAll();

            scp = new ScpBus();
            scp.PlugIn(gamepadIndex);

            Started = true;

            UpdateConfig();

            Task.Run(() => Loop());
        }

        static bool Disposing = false;
        public static bool Disposed { get; private set; } = false;

        public static void Dispose() {
            Disposing = true;

            while (!Disposed && Started) {
                MisaMino.Abort();
                PerfectClear.Abort();
            }

            scp.UnplugAll();
        }
    }
}
