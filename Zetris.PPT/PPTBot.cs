using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Microsoft.VisualBasic;

using MisaMinoNET;
using PerfectClearNET;
using ScpDriverInterface;

namespace Zetris.PPT {
    class PPTBot: Bot<UI, PPTBot> {
        const int rngsearch_max = 1000;
        Thread BotThread = null;

        #region InputHelper
        static bool FitPiece(int[,] board, int piece, int x, int y, int r) {
            for (int i = 0; i < 4; i++) {
                for (int j = 0; j < 4; j++) {
                    if (piecedefs[piece][r][i, j] != -1) {
                        if (x + j < 0 || 9 < x + j || y - i < 0 || 32 < y - i || board[x + j, y - i] != 255) {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        static void fixInput(int piece, ref int x, ref int y, int r) {
            switch (piece) {
                case 5: // O
                    switch (r) {
                        case 1:
                            y++; break;
                        case 2:
                            x--; y++; break;
                        case 3:
                            x--; break;
                    }
                    break;

                case 6: // I
                    switch (r) {
                        case 1:
                            x--; break;
                        case 2:
                            x--; y--; break;
                        case 3:
                            y--; break;
                    }
                    break;
            }

            x--;
            y = 24 - y;
        }

        static void fixOutput(int piece, ref int x, ref int y, int r) {
            x++;
            y = 24 - y;

            switch (piece) {
                case 5: // O
                    switch (r) {
                        case 1:
                            y--; break;
                        case 2:
                            x++; y--; break;
                        case 3:
                            x++; break;
                    }
                    break;

                case 6: // I
                    switch (r) {
                        case 1:
                            x++; break;
                        case 2:
                            x++; y++; break;
                        case 3:
                            y++; break;
                    }
                    break;
            }
        }

        static bool FitPieceWithConvert(int[,] board, int piece, int x, int y, int r) {
            fixInput(piece, ref x, ref y, r);

            return FitPiece(board, piece, x, y, r);
        }

        static int FindInputGoalX(int[,] board, int piece, int x, int y, int r, int d) {
            fixInput(piece, ref x, ref y, r);

            while (FitPiece(board, piece, x, y, r))
                x += d;
            x -= d;

            fixOutput(piece, ref x, ref y, r);
            return x;
        }

        static int FindInputGoalY(int[,] board, int piece, int x, int y, int r) {
            fixInput(piece, ref x, ref y, r);

            while (FitPiece(board, piece, x, y, r))
                y--;
            y++;

            fixOutput(piece, ref x, ref y, r);
            return y;
        }

        static int FixWall(int[,] board, int piece, int x, int y, int r) {
            fixInput(piece, ref x, ref y, r);

            int d = (x > 4) ? -1 : 1;
            while (!FitPiece(board, piece, x, y, r)) {
                x += d;
                if (x > 11) {
                    d = -1;
                }
                if (x < -2) {
                    return -1;
                }
            }

            fixOutput(piece, ref x, ref y, r);
            return x;
        }

        static int BoardHeight(int[,] board, int height) {
            int ret = 0;
            for (int i = 0; i < 10; i++) {
                for (int j = height - 1; j >= 0; j--) {
                    if (board[i, j] != 255) {
                        ret = Math.Max(ret, j + 1);
                        break;
                    }
                }
            }
            return ret;
        }

        static bool FixTspinMini(int[,] board, int x, int y, int r) {
            fixInput(4, ref x, ref y, r);

            for (int i = 0; i < 4; i++) {
                for (int j = 0; j < 4; j++) {
                    if (piecedefs[4][r][i, j] != -1) {
                        int col = x + j;
                        for (int row = y - i; row < 22; row++) {  // row < baseBoardHeight
                            if (board[col, row] != 255) {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        static void AddGarbage(int[,] board, uint rng, int lines) {
            if (lines == 0) return;

            int col = GameHelper.RandomInt(ref rng, 10);

            for (int i = 0; i < lines; i++) {
                if (70 < GameHelper.RandomInt(ref rng, 99)) {
                    int newCol = GameHelper.RandomInt(ref rng, 9);
                    col = newCol + Convert.ToInt32(newCol >= col);
                }

                AddGarbageLine(board, col);
            }
        }
        #endregion

        protected override int getPreviews() => (Preferences.Previews > 18) ? int.MaxValue : Preferences.Previews;
        protected override bool getPerfectClear() => Preferences.PerfectClear;
        protected override bool getEnhancePerfect() => Preferences.EnhancePerfect;
        protected override bool HoldAllowed() => Preferences.HoldAllowed;
        protected override bool AllSpins() => Preferences.AllSpins;
        protected override MisaMinoParameters CurrentStyle() => Preferences.CurrentStyle.Clone().Parameters;
        protected override bool C4W() => Preferences.C4W;
        protected override bool TSDOnly() => Preferences.TSDOnly;
        protected override int Intelligence() => Preferences.Intelligence;
        protected override bool Allow180() => false;
        protected override bool SRSPlus() => false;
        protected override uint PCThreads() => Preferences.PCThreads;
        protected override bool GarbageBlocking() => GameHelper.InSwap.Call();
        protected override int RushTime() => 12;

        protected override bool Danger() =>
#if PUBLIC
            GameHelper.Online.Call() || (GameHelper.LobbyPtr.Call() != 0);
#else
            false;
#endif

        static void ResetGame() {
#if !PUBLIC
            if (GameHelper.InSwap.Call() || !Preferences.PuzzleLeague) return;

            Process.Start("steam://joinlobby/546050/109775241058543776/76561198802063829");

            Stopwatch resetting = new Stopwatch();
            resetting.Start();

            while (resetting.ElapsedMilliseconds < 7000) {}
#endif
        }

        int gamepadIndex = 4;
        ScpBus scp = new ScpBus();
        X360Controller gamepad = new X360Controller();

        public bool SetGamepad(bool state) {
            scp.Unplug(gamepadIndex);
            scp = new ScpBus();
            gamepad = new X360Controller();

            return state? scp.PlugIn(gamepadIndex) : false;
        }

        int playerID, currentRating, numplayers, frames, globalFrames;

        bool inMatch = false;
        int menuStartFrames = 0;
        int ratingSafe = 0;
        
        int state = 0;
        int piece = 0;
        bool register = false;
        bool shouldHaveRegistered = false;
        int old_y;

        bool startbreak = false;

        void misaPrediction(int current, int[] q, int? hold, int combo, int cleared) {
            int garbage_left = GameHelper.getGarbageDropping.Call(playerID);

            if (!GameHelper.InSwap.Call() || cleared == 0) 
                AddGarbage(
                    misaboard,
                    GameHelper.RNG.Call(playerID),
                    GameHelper.CalculateGarbage(playerID, atk, out garbage_left)
                );

            MisaMinoAOT(current, q, hold, combo, garbage_left, 21 + Convert.ToInt32(!FitPieceWithConvert(misaboard, current, 4, 4, 0)));
        }

        void runLogic() {
            numplayers = GameHelper.PlayerCount.Call();
            playerID = GameHelper.FindPlayer.Call();

            if (GameHelper.InMultiplayer.Call())
                playerID = Preferences.Player;

            int temp = GameHelper.getRating.Call();

            if (temp != currentRating) {
                ratingSafe = globalFrames;
            }

            currentRating = temp;

            int y = GameHelper.getPiecePositionY.Call(playerID);
            baseBoardHeight = 25 - y;

            board = GameHelper.getBoard.Call(playerID);
            int[,] oboard = (int[,])board.Clone();

            if (GameHelper.OutsideMenu.Call() && GameHelper.CurrentMode.Call() == 4 && numplayers < 2 && GameHelper.boardAddress.Call(playerID) < 0x1000 && ratingSafe + 1500 < globalFrames && !(GameHelper.GameEnd.Call() != 16 || GameHelper.GameEnd.Call() != 36)) {
                ResetGame();
                return;
            }

            if (GameHelper.boardAddress.Call(playerID) > 0x1000 && GameHelper.OutsideMenu.Call() && GameHelper.getPlayer1Base.Call() > 0x1000) {
                if (numplayers < 2 && GameHelper.CurrentMode.Call() == 4 && GameHelper.Online.Call() && !(GameHelper.GameEnd.Call() != 16 || GameHelper.GameEnd.Call() != 36)) {
                    ResetGame();
                    return;
                }

                int drop = GameHelper.getPieceDropped.Call(playerID);

                int[] pieces = GameHelper.getPieces.Call(playerID);

                bool startanim = GameHelper.getStartAnimation.Call() > 0x1000;

                if (startanim && !startbreak) {
                    NewGame(() => {
                        atk = 0;
                        register = false;
                        movements.Clear();
                        inputStarted = 0;
                        softdrop = false;
                        speedTick = 0;

                        current = pieces[0];
                        queue = pieces.Skip(1).Concat(GameHelper.getNextFromBags.Call(playerID)).Concat(GameHelper.getNextFromRNG(playerID, rngsearch_max, 0)).ToList();
                    }, 21);
                }

                current = GameHelper.getCurrentPiece.Call(playerID);

                startbreak = startanim;

                int? hold = GameHelper.getHold.Call(playerID);
                int combo = GameHelper.getCombo.Call(playerID);

                if (drop != state) {
                    if (drop == 1) {
                        register = !shouldHaveRegistered;
                        old_y = y;

                        int[,] clearedboard = (int[,])board.Clone();
                        ClearLines(clearedboard, out int cleared);

                        if (!BoardEquivalent(misaboard, clearedboard, out _)) {
                            LogHelper.LogText("ARE");
                            LogHelper.LogBoard(misaboard, clearedboard);

                            misaboard = clearedboard;

                            int[] q = pieces.Skip(1).Concat(GameHelper.getNextFromBags.Call(playerID)).Concat(GameHelper.getNextFromRNG(playerID, rngsearch_max, atk)).ToArray();
                            q = q.Take(Math.Min(q.Length, getPreviews())).ToArray();

                            misaPrediction(pieces[0], q, hold, combo, cleared);
                        }
                        
                    } else if (drop == 0) shouldHaveRegistered = true;
                }

                bool itstimetomove = ((register && !pieces.SequenceEqual(queue.Take(pieces.Length)) && current == queue[0]) || (current != piece && piece == 255)) && y < Math.Max(6, old_y);

                if (itstimetomove) register = false;

                if (!register)
                    queue = pieces.Concat(GameHelper.getNextFromBags.Call(playerID)).Concat(GameHelper.getNextFromRNG(playerID, rngsearch_max, 0)).ToList();

                if (itstimetomove) {
                    shouldHaveRegistered = false;
                    inputStarted = 0;
                    softdrop = false;

                    b2b = GameHelper.getB2B.Call(playerID);
                    garbage = GameHelper.getGarbageDropping.Call(playerID);

                    if (MakeDecision(out bool wasHold, out _, out _)) {
                        int start = Convert.ToInt32(wasHold && hold == null);

                        int[] q = pieces.Skip(start + 1).Concat(GameHelper.getNextFromBags.Call(playerID)).Concat(GameHelper.getNextFromRNG(playerID, rngsearch_max, atk)).ToArray();

                        int futureCurrent = pieces[start];
                        int? futureHold = wasHold? current : hold;
                        int futureCombo = combo + Convert.ToInt32(clear > 0);

                        LogHelper.LogText("AOT");
                        misaPrediction(futureCurrent, q.Take(Math.Min(q.Length, getPreviews())).ToArray(), futureHold, futureCombo, clear);

                        pcboard = (int[,])misaboard.Clone();

                        if (movements.Count > 0)
                            PerfectClearAOT(futureCurrent, q, futureHold, futureCombo);

                    } else LogHelper.LogText("FUCK");

                    register = false;
                }

                state = drop;
                piece = current;

                inMatch = true;

            } else {
                if (inMatch) {
                    inMatch = false;

                    menuStartFrames = globalFrames;

                    EndGame();
                }
            }

            board = (int[,])oboard.Clone();
        }

        int clear = 0;
        int inputStarted = 0;
        int inputGoal = -1;
        bool softdrop = false;
        int desiredX, desiredR;
        bool desiredHold;

        void processInput() {
            if (movements.Count > 0) {
                if (GameHelper.InSwap.Call() && GameHelper.SwapType.Call() == 0) {
                    softdrop = false;
                    movements.Clear();
                    inputStarted = 0;
                    return;
                }

                int boardHeight = BoardHeight(board, baseBoardHeight);

                if (pieceUsed == 4 && inputStarted == 0 && boardHeight < 16) {
                    if (FixTspinMini(board, finalX, finalY + baseBoardHeight - 21, finalR)) { // Y is baseBoardHeight compensated
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
                            case Instruction.L: inputGoal = GameHelper.getPiecePositionX.Call(playerID) - 1; break;
                            case Instruction.R: inputGoal = GameHelper.getPiecePositionX.Call(playerID) + 1; break;
                            case Instruction.DROP: inputGoal = 1; break;
                            case Instruction.HOLD: inputGoal = (int)GameHelper.getHoldPointer.Call(playerID); break;

                            case Instruction.D:
                                inputGoal = Math.Min(
                                    GameHelper.getPiecePositionY.Call(playerID) + 1,
                                    FindInputGoalY(
                                        board,
                                        pieceUsed,
                                        GameHelper.getPiecePositionX.Call(playerID),
                                        GameHelper.getPiecePositionY.Call(playerID),
                                        GameHelper.getPieceRotation.Call(playerID)
                                    )
                                );
                                break;

                            case Instruction.LL:
                                inputGoal = FindInputGoalX(
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
                                inputGoal = FindInputGoalX(
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
                                inputGoal = FindInputGoalY(
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
                    int pieceX = GameHelper.getPiecePositionX.Call(playerID);
                    int pieceR = GameHelper.getPieceRotation.Call(playerID);

                    if (inputStarted == 0) {
                        desiredX = pieceX;
                        desiredR = pieceR;
                        desiredHold = false;

                        foreach (Instruction i in movements) {
                            switch (i) {
                                case Instruction.L: desiredX--; break;
                                case Instruction.R: desiredX++; break;

                                case Instruction.LL:
                                    desiredX = FindInputGoalX(
                                        board,
                                        pieceUsed,
                                        desiredX,
                                        GameHelper.getPiecePositionY.Call(playerID),
                                        desiredR,
                                        -1
                                    );
                                    break;

                                case Instruction.RR:
                                    desiredX = FindInputGoalX(
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

                                    desiredX = FixWall(
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

                                    desiredX = FixWall(
                                        board,
                                        pieceUsed,
                                        desiredX,
                                        GameHelper.getPiecePositionY.Call(playerID),
                                        desiredR
                                    );
                                    break;

                                case Instruction.HOLD: desiredHold = true; break;
                            }
                        }

                        inputStarted = 3;
                    }

                    if (GameHelper.getPieceDropped.Call(playerID) == 1) {
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

        X360Buttons previousInputs = X360Buttons.None;
        decimal speedTick = 0;

        int charindex = 0;

        void applyInputs() {
            int nextFrame = GameHelper.getFrameCount.Call();

            bool addDown = false;

            if (GameHelper.boardAddress.Call(playerID) > 0x1000 && GameHelper.OutsideMenu.Call() && nextFrame > 0 && GameHelper.getPlayer1Base.Call() > 0x1000 && GameHelper.GameEnd.Call() != 16 && GameHelper.GameEnd.Call() != 36) {
                if (nextFrame != frames) {
                    gamepad.Buttons = X360Buttons.None;
                    processInput();
                }

                addDown = softdrop;
                frames = nextFrame;

            } else {
                gamepad.Buttons = X360Buttons.None;

                #if !PUBLIC
                    if (Preferences.SpamA && GameHelper.GetMenu.Call() == 28)
                        gamepad.Buttons |= globalFrames % 2 == 0? X360Buttons.A : (X360Buttons.LeftBumper | X360Buttons.Down);

                    else
                #endif

                if (globalFrames % 2 == 0) {
                    if (Preferences.SaveReplay && GameHelper.CanSaveReplay.Call() == 0 && GameHelper.MenuNavigation.Call(0) != 250 && GameHelper.OutsideMenu.Call()) {
                        if (GameHelper.MenuNavigation.Call(1) != 1)        // end of match
                            gamepad.Buttons |= X360Buttons.A;

                        else if (GameHelper.MenuNavigation.Call(2) == 0)   // not default position
                            gamepad.Buttons |= X360Buttons.Up;

                        else if (GameHelper.ConfirmingReplay.Call() != 1)  // in replay confirm sub menu
                            gamepad.Buttons |= X360Buttons.A;

                        else gamepad.Buttons |= GameHelper.ReplayMenuSelection.Call() == 1? X360Buttons.A : X360Buttons.Right;

                    } else if (Preferences.PuzzleLeague) {
                        int mode = GameHelper.CurrentMode.Call();

                        if (GameHelper.OutsideMenu.Call())
                            gamepad.Buttons |= X360Buttons.A;

                        else if (mode == 4) {
                            if (menuStartFrames + 1150 < globalFrames)
                                menuStartFrames = globalFrames;

                            gamepad.Buttons |= menuStartFrames + 1030 < globalFrames? X360Buttons.B : X360Buttons.A;

                        } else if (mode == 1)
                            gamepad.Buttons |= X360Buttons.B;

                        else gamepad.Buttons |= GameHelper.MenuHighlighted.Call() == 4? X360Buttons.A : X360Buttons.Down;

                    } else if (GameHelper.InMultiplayer.Call() && GameHelper.OutsideMenu.Call()) {
                        if (!GameHelper.IsCharacterSelect.Call() || GameHelper.CharSelectIndex.Call(playerID) == 13) // Zed
                            gamepad.Buttons |= X360Buttons.A;

                        else if (GameHelper.CharacterSelectState.Call(playerID) > 1) // Picked not Zed on accident
                            gamepad.Buttons |= X360Buttons.B;

                        else gamepad.Buttons |= (charindex = ++charindex % 5) == 0? X360Buttons.Down : X360Buttons.Right;
                    }
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

                if (gamepad.Buttons.HasFlag(X360Buttons.RightBumper))
                    gamepad.Buttons = X360Buttons.RightBumper;

                if (manualtimer > 0) {
                    gamepad.Buttons = manualbtn;
                    manualtimer--;
                    Interaction.AppActivate("PuyoPuyoTetris");

                } else if (doingManualInput) gamepad.Buttons = X360Buttons.None;

                previousInputs = gamepad.Buttons;
            }

            scp.Report(gamepadIndex, gamepad.GetReport());
        }

        X360Buttons manualbtn;
        int manualtimer = 0;

        bool doingManualInput = false;
        StackPanel _btn;

        public void ManualInput(X360Buttons button, StackPanel aaaaaaa) {
            doingManualInput = true;
            (_btn = aaaaaaa).MaxHeight = double.PositiveInfinity;

            manualbtn = button;
            manualtimer = 3;
        }

        public void RestoreManual() {
            if (GameHelper.CheckProcess())
                Interaction.AppActivate("PuyoPuyoTetris");

            doingManualInput = false;

            Dispatcher.CurrentDispatcher.Invoke(() => {
                if (_btn != null) _btn.MaxHeight = 0;
            });
        }

        public void UpdatePriority() {
            if (BotThread != null)
                BotThread.Priority = Preferences.AccurateSync? ThreadPriority.Normal : ThreadPriority.AboveNormal;
        }

        protected override void BeforeLoop() {
            BotThread = Thread.CurrentThread;
            UpdatePriority();
        }

        protected override void LoopIteration() {
            bool newFrame = false;

            if (GameHelper.CheckProcess()) {
                GameHelper.TrustProcess = true;

                int prev = globalFrames;
                globalFrames = GameHelper.getMenuFrameCount();

                if (newFrame = globalFrames > prev) {
                    if (globalFrames != prev + 1)
                        LogHelper.LogText("Skipped " + (globalFrames - prev - 1) + " frames");

                    CachedMethod.InvalidateAll();

                    runLogic();
                    applyInputs();
                }

                GameHelper.TrustProcess = false;
            }

            Window?.SetActive(inMatch);

            if (!Preferences.AccurateSync && !newFrame)
                Thread.Sleep(10);
        }

        protected override void Starting() {
            scp.UnplugAll();

            scp = new ScpBus();
            scp.PlugIn(gamepadIndex);
        }

        public void Start(UI window, int gamepadindex) {
            gamepadIndex = gamepadindex;
            Start(window);
        }

        protected override void BeforeDispose() => scp.UnplugAll();
    }
}
