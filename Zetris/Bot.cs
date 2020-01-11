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
        static Thread BotThread = null;

        const int rngsearch_max = 1000;

        static UI Window = null;
        static int playerID = 0;

        static void ResetGame() {
#if !PUBLIC
            if (GameHelper.InSwap.Call() || !Preferences.PuzzleLeague) return;

            Process.Start("steam://joinlobby/546050/109775241058543776/76561198802063829");

            Stopwatch resetting = new Stopwatch();
            resetting.Start();

            while (resetting.ElapsedMilliseconds < 7000) { }
#endif
        }

        static int gamepadIndex;
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
        static bool futurepcsolved = false;
        static bool pcbuffer = false;
        static List<Operation> cachedpc = new List<Operation>();
        static List<Operation> executingpc => pcbuffer? cachedpc : PerfectClear.LastSolution;
        static bool searchbufpc = false;

        static bool startbreak = false;

        static bool danger =>
#if PUBLIC
            GameHelper.Online.Call() || (GameHelper.LobbyPtr.Call() != 0);
#else
            false;
#endif

        static int getPreviews() => (Preferences.Previews > 18)? int.MaxValue : Preferences.Previews;

        static int getPerfectType() => Convert.ToInt32(Preferences.EnhancePerfect) + Convert.ToInt32(Preferences.EnhancePerfect && Preferences.AllSpins) * 2;

        static bool isPCB2BEnding(int cleared, int piece, int r) => (cleared >= 4) || (Preferences.AllSpins && (
            piece == 0 ||
            piece == 1 ||
            (r != 2 && (
                piece == 2 ||
                piece == 3
            ))
        ));

        static void misaPrediction(int current, int[] q, int? hold, int combo, int cleared) {
            int garbage_left = 0;

            if (!GameHelper.InSwap.Call() || cleared == 0) 
                InputHelper.AddGarbage(
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
                    21 + Convert.ToInt32(!InputHelper.FitPieceWithConvert(misaboard, current, 4, 4, 0)),
                    misaboard,
                    combo + Convert.ToInt32(cleared > 0),
                    Math.Max(
                        b2b,
                        Convert.ToInt32(InputHelper.FuckItJustDoB2B(misaboard, 25))
                    ),
                    garbage_left
                );
        }

        static void runLogic() {
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
                        MisaMino.FindMove(q, pieces[0], null, 21, pcboard, 0, b2b, 0);

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
                        InputHelper.ClearLines(clearedboard, out int cleared);

                        if (!InputHelper.BoardEquals(misaboard, clearedboard)) {
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
                        if (Preferences.PerfectClear && pcsolved && InputHelper.BoardEquals(board, pcboard)) {
                            LogHelper.LogText("Detected PC");

                            pieceUsed = executingpc[0].Piece;
                            finalX = executingpc[0].X;
                            finalY = executingpc[0].Y;
                            finalR = executingpc[0].R;
                            
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

                            if (!pathSuccess)
                                LogHelper.LogText($"PC PATHFINDER FAILED! piece={pieceUsed}, x={finalX}, y={finalY} => {finalY}, r={finalR}");
                        }

                        if (!pathSuccess) {
                            LogHelper.LogText("Using Misa!");

                            if (!InputHelper.BoardEquals(misaboard, board) || !misasolved) {
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
                                        Convert.ToInt32(InputHelper.FuckItJustDoB2B(board, 25))
                                    ),  
                                    GameHelper.getGarbageDropping.Call(playerID)
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

                        if (InputHelper.ApplyPiece(misaboard, pieceUsed, finalX, finalY, finalR, out clear)) {
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

                                        int bufclear;

                                        if (cancel = !InputHelper.ApplyPiece(tempboard, cachedpc[i].Piece, cachedpc[i].X, cachedpc[i].Y, cachedpc[i].R, out bufclear))
                                            break;

                                        if (i == cachedpc.Count - 1) // last piece always clears a line, so don't have to track b2b all the time
                                            bufb2b = isPCB2BEnding(bufclear, cachedpc[i].Piece, cachedpc[i].R);

                                        int bufstart = Convert.ToInt32(bufwasHold && bufhold == null);
                                            
                                        bufhold = bufwasHold? bufcurrent : bufhold;
                                        bufcurrent = bufq[bufstart];
                                        bufq = bufq.Skip(bufstart + 1).ToArray();

                                        bufcombo += Convert.ToInt32(bufclear > 0);
                                    }

                                    cancel |= !InputHelper.BoardEquals(bufboard, tempboard);
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
                    queue = (int[])pieces.Clone();

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

        static int clear = 0;
        static int b2b = 0;
        static int inputStarted = 0;
        static int inputGoal = -1;
        static bool softdrop = false;
        static int desiredX, desiredR;
        static bool desiredHold;

        static void processInput() {
            if (movements.Count > 0) {
                if (GameHelper.InSwap.Call() && GameHelper.SwapType.Call() == 0) {
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

        static X360Buttons previousInputs = X360Buttons.None;
        static decimal speedTick = 0;

        static int charindex = 0;

        static void applyInputs() {
            int nextFrame = GameHelper.getFrameCount.Call();

            bool addDown = false;

            if (GameHelper.boardAddress.Call(playerID) > 0x1000 && GameHelper.OutsideMenu.Call() && nextFrame > 0 && GameHelper.getPlayer1Base.Call() > 0x1000 && GameHelper.GameEnd.Call() != 16 && GameHelper.GameEnd.Call() != 36) {
                if (nextFrame != frames) {
                    gamepad.Buttons = X360Buttons.None;
                    processInput();
                }

                addDown = softdrop;
                frames = nextFrame;

            } else if (Preferences.SaveReplay && GameHelper.CanSaveReplay.Call() == 0 && GameHelper.MenuNavigation.Call(0) != 250 && GameHelper.OutsideMenu.Call()) {
                gamepad.Buttons = X360Buttons.None;
                if (globalFrames % 2 == 0) { 
                    if (GameHelper.MenuNavigation.Call(1) == 1) {                 // end of match
                        if (GameHelper.MenuNavigation.Call(2) != 0) {             // not default position
                            if (GameHelper.ConfirmingReplay.Call() == 1) {        // in replay confirm sub menu
                                gamepad.Buttons |= (GameHelper.ReplayMenuSelection.Call() == 1) ? X360Buttons.A : X360Buttons.Right;
                            } else {
                                gamepad.Buttons |= X360Buttons.A;
                            }
                        } else {
                            gamepad.Buttons |= X360Buttons.Up;
                        }
                    } else {
                        gamepad.Buttons |= X360Buttons.A;
                    }
                }

            } else if (Preferences.PuzzleLeague) {
                int mode = GameHelper.CurrentMode.Call();
                gamepad.Buttons = X360Buttons.None;

                if (globalFrames % 2 == 0) {
                    if (GameHelper.OutsideMenu.Call()) {
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
                        if (GameHelper.MenuHighlighted.Call() != 4) {
                            gamepad.Buttons |= X360Buttons.Down;
                        } else {
                            gamepad.Buttons |= X360Buttons.A;
                        }
                    }
                }

            } else if (GameHelper.InMultiplayer.Call()) {
                gamepad.Buttons = X360Buttons.None;

                if (globalFrames % 2 == 0 && GameHelper.OutsideMenu.Call()) {
                    if (!GameHelper.IsCharacterSelect.Call()) {
                        gamepad.Buttons |= X360Buttons.A;
                    }

                    else if (GameHelper.CharSelectIndex.Call(playerID) == 13)
                        gamepad.Buttons |= X360Buttons.A;

                    else if (GameHelper.CharacterSelectState.Call(playerID) > 1)
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

            MisaMino.Configure(param, Preferences.HoldAllowed, Preferences.AllSpins, Preferences.TSDOnly, Preferences.Intelligence);
        }

        public static void UpdatePriority() {
            if (BotThread != null)
                BotThread.Priority = Preferences.AccurateSync? ThreadPriority.Normal : ThreadPriority.AboveNormal;
        }

        static void Loop() {
            BotThread = Thread.CurrentThread;

            UpdateConfig();
            UpdatePriority();

            while (!Disposing) {
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

                updateUI();

                if (!Preferences.AccurateSync && !newFrame)
                    Thread.Sleep(10);
            }

            Disposed = true;
        }

        public static bool Started { get; private set; } = false;

        public static void Start(UI window, int gamepadindex) {
            if (Started) return;

            Started = true;

            MisaMino.Finished += (bool success) => misasolved = success;

            PerfectClear.Finished += (bool success) => {
                if (pcbuffer) futurepcsolved = success;
                else pcsolved = success;
            };

            Window = window;

            scp.UnplugAll();

            scp = new ScpBus();
            scp.PlugIn(gamepadIndex = gamepadindex);

            Task.Run(Loop);
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
