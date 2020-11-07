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
                    if (pieces[piece][r][i, j] != -1) {
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
                    if (pieces[4][r][i, j] != -1) {
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

        List<Instruction> movements = new List<Instruction>();
        int state = 0;
        int piece = 0;
        int pieceUsed;
        bool spinUsed;
        int finalX, finalY, finalR;
        bool register = false;
        bool shouldHaveRegistered = false;
        int baseBoardHeight;
        int old_y;
        int atk = 0;

        bool startbreak = false;

        bool danger =>
#if PUBLIC
            GameHelper.Online.Call() || (GameHelper.LobbyPtr.Call() != 0);
#else
            false;
#endif

        void misaPrediction(int current, int[] q, int? hold, int combo, int cleared) {
            int garbage_left = 0;

            if (!GameHelper.InSwap.Call() || cleared == 0) 
                AddGarbage(
                    misaboard,
                    GameHelper.RNG.Call(playerID),
                    GameHelper.CalculateGarbage(playerID, atk, out garbage_left)
                );

            if (MisaMino.Running) MisaMino.Abort();

            misasolved = false;

            if (!danger)
                MisaMino.FindMove(
                    q,
                    current,
                    hold,
                    misa_lasty = 21 + Convert.ToInt32(!FitPieceWithConvert(misaboard, current, 4, 4, 0)),
                    misaboard,
                    combo,
                    Math.Max(
                        b2b,
                        Convert.ToInt32(FuckItJustDoB2B(misaboard, 25))
                    ),
                    garbage_left
                );
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

                int current = GameHelper.getCurrentPiece.Call(playerID);

                int[] pieces = GameHelper.getPieces.Call(playerID);

                bool startanim = GameHelper.getStartAnimation.Call() > 0x1000;

                if (startanim && !startbreak) {
                    MisaMino.Reset(); // this will abort as well
                    misasolved = false;
                    b2b = 1; // Hack that makes MisaMino start like a normal person
                    atk = 0;
                    register = false;
                    movements.Clear();
                    inputStarted = 0;
                    softdrop = false;
                    speedTick = 0;

                    PerfectClear.Abort();
                    pcsolved = false;
                    futurepcsolved = false;
                    pcbuffer = false;
                    cachedpc = new List<Operation>();
                    searchbufpc = false;

                    misaboard = (int[,])board.Clone();
                    pcboard = (int[,])board.Clone();

                    int[] q = pieces.Skip(1).Concat(GameHelper.getNextFromBags.Call(playerID)).Concat(GameHelper.getNextFromRNG(playerID, rngsearch_max, 0)).ToArray();
                    q = q.Take(Math.Min(q.Length, getPreviews())).ToArray();

                    if (!danger) {
                        MisaMino.FindMove(q, pieces[0], null, misa_lasty = 21, pcboard, 0, b2b, 0);

                        if (Preferences.PerfectClear) {
                            PerfectClear.Find(
                                pcboard, q, pieces[0],
                                null, Preferences.HoldAllowed, 6, GameHelper.InSwap.Call(), getPerfectType(), 0, false
                            );
                        }
                    }
                }

                startbreak = startanim;

                int? hold = GameHelper.getHold.Call(playerID);
                int combo = GameHelper.getCombo.Call(playerID);

                if (drop != state) {
                    if (drop == 1) {
                        register = !shouldHaveRegistered;
                        old_y = y;

                        int[,] clearedboard = (int[,])board.Clone();
                        ClearLines(clearedboard, out int cleared);

                        if (!BoardEquals(misaboard, clearedboard)) {
                            LogHelper.LogText("ARE");
                            LogHelper.LogBoard(misaboard, clearedboard);

                            misaboard = clearedboard;

                            int[] q = pieces.Skip(1).Concat(GameHelper.getNextFromBags.Call(playerID)).Concat(GameHelper.getNextFromRNG(playerID, rngsearch_max, atk)).ToArray();
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
                    if (PerfectClear.Running && !pcbuffer) PerfectClear.Abort();

                    if (!danger) {
                        if (Preferences.PerfectClear && pcsolved && BoardEquals(board, pcboard)) {
                            LogHelper.LogText("Detected PC");

                            pieceUsed = executingpc[0].Piece;
                            finalX = executingpc[0].X;
                            finalY = executingpc[0].Y + baseBoardHeight - 21; // if baseboardheight happens to be 22, need to +1 this
                            finalR = executingpc[0].R;
                            
                            movements = MisaMino.FindPath(
                                board,
                                baseBoardHeight,
                                pieceUsed,
                                finalX,
                                finalY,
                                finalR,
                                current != pieceUsed,
                                out spinUsed,
                                out pathSuccess
                            );

                            if (!pathSuccess)
                                LogHelper.LogText($"PC PATHFINDER FAILED! piece={pieceUsed}, x={finalX}, y={finalY} => {finalY}, r={finalR}");
                        }

                        if (!pathSuccess) {
                            LogHelper.LogText("Using Misa!");

                            bool misaok = BoardEquals(misaboard, board) && misasolved;
                            bool misasaved = false;

                            if (misaok && misa_lasty != baseBoardHeight) { // oops we spawned on wrong y pos
                                LogHelper.LogText($"Tryna save Misa... {misa_lasty} {baseBoardHeight}");

                                movements = MisaMino.FindPath(
                                    board,
                                    baseBoardHeight,
                                    MisaMino.LastSolution.PieceUsed,
                                    MisaMino.LastSolution.FinalX,
                                    MisaMino.LastSolution.FinalY,
                                    MisaMino.LastSolution.FinalR,
                                    current != MisaMino.LastSolution.PieceUsed,
                                    out spinUsed,
                                    out misaok
                                );

                                misasaved = misaok;

                                LogHelper.LogText($"misasaved {misasaved}");
                            }

                            if (!misaok) {
                                int[] q = pieces.Concat(GameHelper.getNextFromBags.Call(playerID)).ToArray();
                                q = q.Take(Math.Min(q.Length, getPreviews())).ToArray();

                                LogHelper.LogText("Rush");
                                LogHelper.LogBoard(misaboard, board);

                                MisaMino.FindMove(
                                    q,
                                    current,
                                    hold,
                                    baseBoardHeight,
                                    board,
                                    combo,
                                    Math.Max(
                                        GameHelper.getB2B.Call(playerID), // if pc finder interrupted we might have a wrong value. read from game mem here
                                        Convert.ToInt32(FuckItJustDoB2B(board, 25))
                                    ),  
                                    GameHelper.getGarbageDropping.Call(playerID)
                                );

                                Stopwatch misasearching = new Stopwatch();
                                misasearching.Start();

                                while (misasearching.ElapsedMilliseconds < 12) {}

                                MisaMino.Abort();
                            }

                            if (!misasaved) movements = MisaMino.LastSolution.Instructions;
                            pieceUsed = MisaMino.LastSolution.PieceUsed;
                            if (!misasaved) spinUsed = MisaMino.LastSolution.SpinUsed;
                            b2b = MisaMino.LastSolution.B2B;
                            atk = MisaMino.LastSolution.Attack;
                            finalX = MisaMino.LastSolution.FinalX;
                            finalY = MisaMino.LastSolution.FinalY;
                            finalR = MisaMino.LastSolution.FinalR;

                            Window?.SetConfidence($"{MisaMino.LastSolution.Nodes} ({MisaMino.LastSolution.Depth})");
                            Window?.SetThinkingTime(MisaMino.LastSolution.Time);

                            pcsolved = false;
                            futurepcsolved = false;
                            pcbuffer = false;
                            searchbufpc = false;

                        } else {
                            LogHelper.LogText("Using PC!");

                            cachedpc = executingpc.Skip(1).ToList();

                            bool prev = pcbuffer;
                            pcbuffer = cachedpc.Count != 0;

                            searchbufpc |= !prev && pcbuffer;

                            if (!pcbuffer) {
                                pcsolved = futurepcsolved;
                                searchbufpc = futurepcsolved = false;
                            }

                            Window?.SetConfidence($"[PC] {cachedpc.Count + 1}");
                            Window?.SetThinkingTime(PerfectClear.LastTime);
                        }

                        misasolved = false;

                        bool wasHold = movements.Count > 0 && movements[0] == Instruction.HOLD;

                        misaboard = (int[,])board.Clone();

                        if (ApplyPiece(misaboard, pieceUsed, finalX, finalY, finalR, baseBoardHeight, out clear, out _)) {
                            int start = Convert.ToInt32(wasHold && hold == null);

                            int[] q = pieces.Skip(start + 1).Concat(GameHelper.getNextFromBags.Call(playerID)).Concat(GameHelper.getNextFromRNG(playerID, rngsearch_max, atk)).ToArray();

                            int futureCurrent = pieces[start];
                            int? futureHold = wasHold? current : hold;
                            int futureCombo = combo + Convert.ToInt32(clear > 0);
                            if (pathSuccess && !pcsolved) b2b = Convert.ToInt32(isPCB2BEnding(clear, pieceUsed, finalR));

                            LogHelper.LogText("AOT");
                            misaPrediction(futureCurrent, q.Take(Math.Min(q.Length, getPreviews())).ToArray(), futureHold, futureCombo, clear);

                            pcboard = (int[,])misaboard.Clone();

                            if (Preferences.PerfectClear && movements.Count > 0 && (!pcsolved || searchbufpc) && !PerfectClear.Running) {
                                bool cancel = false;

                                int[,] bufboard = pcboard;
                                int[] bufq = q;
                                int bufcurrent = futureCurrent;
                                int? bufhold = futureHold;
                                int bufcombo = futureCombo;
                                bool bufb2b = b2b > 0;
                                
                                if (searchbufpc) {
                                    bufboard = new int[10, 40];

                                    for (int i = 0; i < 10; i++)
                                        for (int j = 0; j < 40; j++)
                                            bufboard[i, j] = 255;
                                    
                                    int[,] tempboard = (int[,])pcboard.Clone();

                                    for (int i = 0; i < cachedpc.Count; i++) {    // yes i copy pasted code, no i don't care, they're different enough to not generalize into a func
                                        bool bufwasHold = bufcurrent != cachedpc[i].Piece;

                                        if (cancel = !ApplyPiece(tempboard, cachedpc[i].Piece, cachedpc[i].X, cachedpc[i].Y, cachedpc[i].R, baseBoardHeight, out int bufclear, out _))
                                            break;

                                        if (i == cachedpc.Count - 1) // last piece always clears a line, so don't have to track b2b all the time
                                            bufb2b = isPCB2BEnding(bufclear, cachedpc[i].Piece, cachedpc[i].R);

                                        int bufstart = Convert.ToInt32(bufwasHold && bufhold == null);
                                            
                                        bufhold = bufwasHold? bufcurrent : bufhold;
                                        bufcurrent = bufq[bufstart];
                                        bufq = bufq.Skip(bufstart + 1).ToArray();

                                        bufcombo += Convert.ToInt32(bufclear > 0);
                                    }

                                    cancel |= !BoardEquals(bufboard, tempboard);
                                }
                                
                                if (!cancel) {
                                    PerfectClear.Find(
                                        bufboard, bufq.Take(Math.Min(bufq.Length, getPreviews())).ToArray(), bufcurrent,
                                        bufhold, Preferences.HoldAllowed, 6, GameHelper.InSwap.Call(), getPerfectType(), bufcombo, bufb2b
                                    );

                                    searchbufpc = false;
                                } else LogHelper.LogText("FUCK but less");
                            }
                        } else LogHelper.LogText("FUCK");
                    }

                    register = false;
                }

                state = drop;
                piece = current;

                if (!register)
                    queue = pieces.ToList();

                inMatch = true;

            } else {
                if (inMatch) {
                    inMatch = false;

                    menuStartFrames = globalFrames;

                    MisaMino.Abort();
                    PerfectClear.Abort();
                }
            }
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
