using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using MisaMinoNET;
using PerfectClearNET;

namespace Zetris {
    public abstract class Bot<UI, B> where UI: IUI where B: Bot<UI, B>, new() {
        static B instance;
        public static B Instance => instance?? (instance = new B());

        public static uint PCThreadsMaximum => (uint)Environment.ProcessorCount;

        #region InputHelper
        protected static readonly int[][][,] piecedefs = new int[7][][,] {
            new int[4][,] { // S
                new int[,] {
                    {-1, 0, 0, -1},
                    {0, 0, -1, -1},
                    {-1, -1, -1, -1},
                    {-1, -1, -1, -1}
                },
                new int[,] {
                    {-1, 0, -1, -1},
                    {-1, 0, 0, -1},
                    {-1, -1, 0, -1},
                    {-1, -1, -1, -1}
                },
                new int[,] {
                    {-1, -1, -1, -1},
                    {-1, 0, 0, -1},
                    {0, 0, -1, -1},
                    {-1, -1, -1, -1}
                },
                new int[,] {
                    {0, -1, -1, -1},
                    {0, 0, -1, -1},
                    {-1, 0, -1, -1},
                    {-1, -1, -1, -1}
                }
            },
            new int[4][,] { // Z
                new int[,] {
                    {1, 1, -1, -1},
                    {-1, 1, 1, -1},
                    {-1, -1, -1, -1},
                    {-1, -1, -1, -1}
                },
                new int[,] {
                    {-1, -1, 1, -1},
                    {-1, 1, 1, -1},
                    {-1, 1, -1, -1},
                    {-1, -1, -1, -1}
                },
                new int[,] {
                    {-1, -1, -1, -1},
                    {1, 1, -1, -1},
                    {-1, 1, 1, -1},
                    {-1, -1, -1, -1}
                },
                new int[,] {
                    {-1, 1, -1, -1},
                    {1, 1, -1, -1},
                    {1, -1, -1, -1},
                    {-1, -1, -1, -1}
                }
            },
            new int[4][,] { // J
                new int[,] {
                    {2, -1, -1, -1},
                    {2, 2, 2, -1},
                    {-1, -1, -1, -1},
                    {-1, -1, -1, -1}
                },
                new int[,] {
                    {-1, 2, 2, -1},
                    {-1, 2, -1, -1},
                    {-1, 2, -1, -1},
                    {-1, -1, -1, -1}
                },
                new int[,] {
                    {-1, -1, -1, -1},
                    {2, 2, 2, -1},
                    {-1, -1, 2, -1},
                    {-1, -1, -1, -1}
                },
                new int[,] {
                    {-1, 2, -1, -1},
                    {-1, 2, -1, -1},
                    {2, 2, -1, -1},
                    {-1, -1, -1, -1}
                }
            },
            new int[4][,] { // L
                new int[,] {
                    {-1, -1, 3, -1},
                    {3, 3, 3, -1},
                    {-1, -1, -1, -1},
                    {-1, -1, -1, -1}
                },
                new int[,] {
                    {-1, 3, -1, -1},
                    {-1, 3, -1, -1},
                    {-1, 3, 3, -1},
                    {-1, -1, -1, -1}
                },
                new int[,] {
                    {-1, -1, -1, -1},
                    {3, 3, 3, -1},
                    {3, -1, -1, -1},
                    {-1, -1, -1, -1}
                },
                new int[,] {
                    {3, 3, -1, -1},
                    {-1, 3, -1, -1},
                    {-1, 3, -1, -1},
                    {-1, -1, -1, -1}
                }
            },
            new int[4][,] { // T
                new int[,] {
                    {-1, 4, -1, -1},
                    {4, 4, 4, -1},
                    {-1, -1, -1, -1},
                    {-1, -1, -1, -1}
                },
                new int[,] {
                    {-1, 4, -1, -1},
                    {-1, 4, 4, -1},
                    {-1, 4, -1, -1},
                    {-1, -1, -1, -1}
                },
                new int[,] {
                    {-1, -1, -1, -1},
                    {4, 4, 4, -1},
                    {-1, 4, -1, -1},
                    {-1, -1, -1, -1}
                },
                new int[,] {
                    {-1, 4, -1, -1},
                    {4, 4, -1, -1},
                    {-1, 4, -1, -1},
                    {-1, -1, -1, -1}
                }
            },
            new int[4][,] { // O
                new int[,] {
                    {-1, 5, 5, -1},
                    {-1, 5, 5, -1},
                    {-1, -1, -1, -1},
                    {-1, -1, -1, -1}
                },
                new int[,] {
                    {-1, 5, 5, -1},
                    {-1, 5, 5, -1},
                    {-1, -1, -1, -1},
                    {-1, -1, -1, -1}
                },
                new int[,] {
                    {-1, 5, 5, -1},
                    {-1, 5, 5, -1},
                    {-1, -1, -1, -1},
                    {-1, -1, -1, -1}
                },
                new int[,] {
                    {-1, 5, 5, -1},
                    {-1, 5, 5, -1},
                    {-1, -1, -1, -1},
                    {-1, -1, -1, -1}
                }
            },
            new int[4][,] { // I
                new int[,] {
                    {-1, -1, -1, -1},
                    {6, 6, 6, 6},
                    {-1, -1, -1, -1},
                    {-1, -1, -1, -1}
                },
                new int[,] {
                    {-1, -1, 6, -1},
                    {-1, -1, 6, -1},
                    {-1, -1, 6, -1},
                    {-1, -1, 6, -1}
                },
                new int[,] {
                    {-1, -1, -1, -1},
                    {-1, -1, -1, -1},
                    {6, 6, 6, 6},
                    {-1, -1, -1, -1}
                },
                new int[,] {
                    {-1, 6, -1, -1},
                    {-1, 6, -1, -1},
                    {-1, 6, -1, -1},
                    {-1, 6, -1, -1}
                }
            }
        };

        protected static void ClearLines(int[,] board, out int cleared) {
            cleared = 0;

            for (int i = 25; i >= 0; i--) {
                int fill = 0;
                for (int j = 0; j < 10; j++)
                    fill += Convert.ToInt32(board[j, i] != 255);

                if (fill == 10) {
                    cleared++;
                    for (int j = i; j < 26; j++) {
                        for (int k = 0; k < 10; k++) {
                            board[k, j] = board[k, j + 1];
                        }
                    }
                }
            }
        }

        protected static bool ApplyPiece(int[,] board, int piece, int x, int y, int r, int baseBoardHeight, out int c, out List<int[]> coords) {
            x--;
            y = baseBoardHeight + 3 - y;

            c = 0;
            coords = new List<int[]>();

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (piecedefs[piece][r][i, j] != -1) {
                        if (x + j < 0 || y - i < 0 || x + j > 9 || y - i > 39 || board[x + j, y - i] != 255) return false;
                        board[x + j, y - i] = piecedefs[piece][r][i, j];
                        coords.Add(new[] { x + j, y - i });
                    }

            ClearLines(board, out c);

            return true;
        }

        protected static bool BoardEquals(int[,] a, int[,] b) {
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 24; j++)
                    if ((a[i, j] == 255) != (b[i, j] == 255))
                        return false;

            return true;
        }

        static int FirstNonGarbageLine(int[,] a) {
            for (int i = 0; i < 24; i++) {
                bool garbage = false;

                for (int j = 0; j < 10; j++)
                    if (a[j, i] == 9) {
                        garbage = true;
                        break;
                    }

                if (!garbage)
                    return i;
            }

            return 24;
        }

        protected static bool BoardEquivalent(int[,] a, int[,] b, out int diff) {
            int g = FirstNonGarbageLine(a);
            int h = FirstNonGarbageLine(b);
            int top = 24 - Math.Max(g, h);

            diff = h - g;

            for (int i = 0; i < 10; i++)
                for (int j = 0; j < top; j++)
                    if ((a[i, j + g] == 255) != (b[i, j + h] == 255))
                        return false;

            return true;
        }

        protected static void AddGarbageLine(int[,] board, int col) {
            for (int i = 0; i < 10; i++) {
                for (int j = 30; j >= 0; j--) {
                    board[i, j + 1] = board[i, j];
                }
            }

            for (int i = 0; i < 10; i++) {
                board[i, 0] = 9;
            }

            board[col, 0] = 255;
        }

        protected static bool FuckItJustDoB2B(int[,] board, int minos) {
            int count = 0;

            for (int i = 25; i >= 0; i--)
                for (int j = 0; j < 10; j++)
                    if ((count += Convert.ToInt32(board[j, i] != 255)) > minos)
                        return false;

            return true;
        }

        protected static bool FitPiece(int[,] board, int piece, int x, int y, int r) {
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

        protected static void fixInput(int piece, ref int x, ref int y, int r) {
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

        protected static void fixOutput(int piece, ref int x, ref int y, int r) {
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

        protected static bool FitPieceWithConvert(int[,] board, int piece, int x, int y, int r) {
            fixInput(piece, ref x, ref y, r);

            return FitPiece(board, piece, x, y, r);
        }
        #endregion

        protected UI Window;

        protected int[,] board = new int[10, 40];

        protected int current;
        protected int? hold;
        protected int combo;
        protected List<int> queue = new List<int>();
        protected int garbage;

        protected int misa_lasty;
        protected int baseBoardHeight;
        protected int b2b = 0;

        protected int[,] misaboard = new int[10, 40];
        protected List<Instruction> movements = new List<Instruction>();
        protected int finalX;
        protected int finalY;
        protected int finalR;
        protected int pieceUsed;
        protected bool spinUsed;
        protected int atk;
        protected bool misasolved = false;

        protected int[,] pcboard = new int[10, 40];
        protected bool pcsolved = false;
        protected bool futurepcsolved = false;
        protected bool pcbuffer = false;
        protected List<Operation> cachedpc = new List<Operation>();
        protected List<Operation> executingpc => pcbuffer? cachedpc : PerfectClear.LastSolution;
        protected bool searchbufpc = false;

        protected abstract int getPreviews();
        protected abstract bool getPerfectClear();
        protected abstract bool getEnhancePerfect();
        protected abstract bool HoldAllowed();
        protected abstract AllowedSpins getAllowedSpins();
        protected abstract MisaMinoParameters CurrentStyle();
        protected abstract bool C4W();
        protected abstract bool TSDOnly();
        protected abstract int Intelligence();
        protected abstract bool Allow180();
        protected abstract TetrisGame getTetrisGame();
        protected abstract uint PCThreads();
        protected abstract bool GarbageBlocking();
        protected abstract int RushTime();

        protected abstract bool Danger(); // set to true if bot is probably being used for cheating, in PUBLIC mode

        protected int[] getClippedQueue() => queue.Take(Math.Min(queue.Count, getPreviews())).ToArray();
        
        protected int getPerfectType() {
            if (!getEnhancePerfect()) {
                return 0; // Fast
            }
            if (getAllowedSpins().IsAllSpins()) {
                return 3; // All-spin Attack (mini is zero attack)
                // TODO: Implement All-spin attack without T-immobiles
            }
            return 1; // T-spin Attack
        }

        protected bool isPCB2BEnding(int cleared, int piece, int r) {
            if (getTetrisGame() == TetrisGame.TETRIOS2) {
                return true;
            }
            return (cleared >= 4) || (getAllowedSpins().IsAllSpins() && (
                piece == 0 ||
                piece == 1 ||
                (r != 2 && (
                    piece == 2 ||
                    piece == 3
                ))
            ));
        }

        protected void NewGame(Action setup, int baseBoardHeight) {
            MisaMino.Reset(); // this will abort as well
            misasolved = false;
            b2b = 1; // Hack that makes MisaMino start like a normal person

            PerfectClear.Abort();
            pcsolved = false;
            futurepcsolved = false;
            pcbuffer = false;
            cachedpc = new List<Operation>();
            searchbufpc = false;

            setup?.Invoke();

            misaboard = (int[,])board.Clone();
            pcboard = (int[,])board.Clone();

            int[] q = getClippedQueue();
            LogHelper.LogText("QUEUE FOR START: " + string.Join(" ", q));

            if (!Danger()) {
                MisaMino.FindMove(q, current, null, misa_lasty = baseBoardHeight, misaboard, 0, b2b, 0);

                if (getPerfectClear()) {
                    PerfectClear.Find(
                        pcboard, q, current,
                        null, HoldAllowed(), 6, GarbageBlocking(), getPerfectType(), 0, false
                    );
                }
            }
        }

        protected bool MakeDecision(out bool wasHold, out int clear, out List<int[]> coords) {
            wasHold = false;
            clear = 0;
            coords = null;

            bool pathSuccess = false;

            movements = new List<Instruction>();
            pieceUsed = finalX = finalY = finalR = -10;
            atk = 0;

            if (MisaMino.Running) MisaMino.Abort();
            if (PerfectClear.Running && !pcbuffer) PerfectClear.Abort();

            if (Danger()) return false;

            if (getPerfectClear() && pcsolved && BoardEquals(board, pcboard)) {
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
                    LogHelper.LogText($"PC PATHFINDER FAILED! piece={pieceUsed}, x={finalX}, y={finalY}, r={finalR}");
            }

            if (!pathSuccess) {
                LogHelper.LogText("Using Misa!");

                bool equals = BoardEquals(misaboard, board);
                bool equivalent = BoardEquivalent(misaboard, board, out int diff);

                int tempY = MisaMino.LastSolution.FinalY;

                if (!equals && equivalent) {
                    LogHelper.LogText("GARBAGE FIX ATTEMPT");
                    tempY -= diff;
                }

                bool misaok = equivalent && misasolved;
                bool misasaved = false;

                if (misaok && (misa_lasty != baseBoardHeight || (!equals && equivalent))) { // oops wrong y pos
                    LogHelper.LogText($"Tryna save Misa... lasty={misa_lasty} baseH={baseBoardHeight} finalY={MisaMino.LastSolution.FinalY} tempY={tempY}");

                    movements = MisaMino.FindPath(
                        board,
                        baseBoardHeight,
                        MisaMino.LastSolution.PieceUsed,
                        MisaMino.LastSolution.FinalX,
                        tempY,
                        MisaMino.LastSolution.FinalR,
                        current != MisaMino.LastSolution.PieceUsed,
                        out spinUsed,
                        out misaok
                    );

                    misasaved = misaok;

                    LogHelper.LogText($"misasaved {misasaved}");
                }

                if (!misaok) {
                    LogHelper.LogText("Rush (SOMETHING JUST WENT REALLY WRONG)");
                    LogHelper.LogBoard(misaboard, board);

                    int[] q = getClippedQueue();

                    LogHelper.LogText("QUEUE FOR RUSH: " + string.Join(" ", q));

                    MisaMino.FindMove(
                        q,
                        current,
                        hold,
                        baseBoardHeight,
                        board,
                        combo,
                        Math.Max(
                            b2b, // ideally this should be read from game mem right before calling
                            Convert.ToInt32(FuckItJustDoB2B(board, 25))
                        ),
                        garbage
                    );

                    Stopwatch misasearching = new Stopwatch();
                    misasearching.Start();

                    while (misasearching.ElapsedMilliseconds < RushTime()) { }

                    MisaMino.Abort();
                }

                if (!misasaved) movements = MisaMino.LastSolution.Instructions;
                pieceUsed = MisaMino.LastSolution.PieceUsed;
                if (!misasaved) spinUsed = MisaMino.LastSolution.SpinUsed;
                b2b = MisaMino.LastSolution.B2B;
                atk = MisaMino.LastSolution.Attack;
                finalX = MisaMino.LastSolution.FinalX;
                finalY = tempY;
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

            wasHold = movements.Count > 0 && movements[0] == Instruction.HOLD;

            bool applied = ApplyPiece(board, pieceUsed, finalX, finalY, finalR, baseBoardHeight, out clear, out coords);
            LogHelper.LogText($"Piece applied? {applied}");

            misaboard = (int[,])board.Clone();
            pcboard = (int[,])board.Clone();

            if (pathSuccess && !pcsolved)  // pathSuccess here means that I had used PC finder to make the decision
                b2b = Convert.ToInt32(isPCB2BEnding(clear, pieceUsed, finalR));

            // Filter L->R and R->L
            if (movements.Count >= 2) {
                for (int i = movements.Count - 2; i >= 0; i--) {
                    if ((movements[i] == Instruction.L && movements[i + 1] == Instruction.R) || (movements[i] == Instruction.R && movements[i + 1] == Instruction.L)) {
                        movements.RemoveAt(i);
                        movements.RemoveAt(i);
                        i--;
                    }
                }
            }

            LogHelper.LogText($"Movements generated for piece {pieceUsed} ({finalX}, {finalY}, {finalR}) => {string.Join(", ", movements)}");

            return applied;
        }

        protected void MisaMinoAOT(int current, int[] q, int? hold, int combo, int garbage, int spawn_pos) {
            if (MisaMino.Running) MisaMino.Abort();

            misasolved = false;

            if (!Danger())
                MisaMino.FindMove(
                    q,
                    current,
                    hold,
                    misa_lasty = spawn_pos,
                    misaboard,
                    combo,
                    Math.Max(
                        b2b,
                        Convert.ToInt32(FuckItJustDoB2B(misaboard, 25))
                    ),
                    garbage
                );
        }

        protected void PerfectClearAOT(int current, int[] q, int? hold, int combo) {
            if (getPerfectClear() && (!pcsolved || searchbufpc) && !PerfectClear.Running) {
                bool cancel = false;

                int[,] bufboard = pcboard;
                int[] bufq = q;
                int bufcurrent = current;
                int? bufhold = hold;
                int bufcombo = combo;
                bool bufb2b = b2b > 0;

                if (searchbufpc) {
                    bufboard = new int[10, 40];

                    for (int i = 0; i < 10; i++)
                        for (int j = 0; j < 40; j++)
                            bufboard[i, j] = 255;

                    int[,] tempboard = (int[,])pcboard.Clone();

                    for (int i = 0; i < cachedpc.Count; i++) {    // yes i copy pasted code, no i don't care, they're different enough to not generalize into a func
                        bool bufwasHold = bufcurrent != cachedpc[i].Piece;

                        cancel = !ApplyPiece(
                            tempboard,
                            cachedpc[i].Piece,
                            cachedpc[i].X,
                            cachedpc[i].Y + baseBoardHeight - 21, // if baseboardheight happens to be 22, need to +1 this (TETR.IO)
                            cachedpc[i].R,
                            baseBoardHeight,
                            out int bufclear,
                            out _
                        );

                        if (cancel) break;

                        if (i == cachedpc.Count - 1) // last piece always clears a line, so don't have to track b2b all the time
                            bufb2b = isPCB2BEnding(bufclear, cachedpc[i].Piece, cachedpc[i].R);

                        int bufstart = Convert.ToInt32(bufwasHold && bufhold == null);

                        bufhold = bufwasHold ? bufcurrent : bufhold;
                        bufcurrent = bufq[bufstart];
                        bufq = bufq.Skip(bufstart + 1).ToArray();

                        bufcombo += Convert.ToInt32(bufclear > 0);
                    }

                    cancel |= !BoardEquals(bufboard, tempboard);
                }

                if (!cancel) {
                    PerfectClear.Find(
                        bufboard, bufq.Take(Math.Min(bufq.Length, getPreviews())).ToArray(), bufcurrent,
                        bufhold, HoldAllowed(), 6, GarbageBlocking(), getPerfectType(), bufcombo, bufb2b
                    );

                    searchbufpc = false;

                } else LogHelper.LogText("FUCK but less");
            }
        }

        protected void EndGame() {
            MisaMino.Abort();
            PerfectClear.Abort();
        }

        public void UpdateConfig() {
            if (!Started) return;

            MisaMinoParameters param = CurrentStyle();
            param.Parameters.strategy_4w = 400 * Convert.ToInt32(C4W());

            MisaMino.Configure(param, HoldAllowed(), getAllowedSpins().IsAllSpins(), TSDOnly(), Intelligence(), Allow180(), getTetrisGame().IsSRSPlus());
        }

        public void UpdatePCThreads() {
            if (!Started) return;

            PerfectClear.SetThreads(PCThreads());
        }

        protected virtual void BeforeLoop() {}
        protected abstract void LoopIteration();

        public bool Started { get; private set; } = false;

        protected virtual void Starting() {}

        public async void Start(UI window) {
            if (Started) return;

            Started = true;
            Window = window;

            Starting();

            MisaMino.Finished += success => misasolved = success;

            PerfectClear.Finished += success => {
                if (pcbuffer) futurepcsolved = success;
                else pcsolved = success;
            };

            await Task.Run(() => {
                UpdateConfig();
                UpdatePCThreads();
                BeforeLoop();

                while (!Disposing)
                    LoopIteration();

                Disposed = true;
            });
        }

        bool Disposing = false;
        public bool Disposed { get; private set; } = false;

        protected virtual void BeforeDispose() {}

        public void Dispose() {
            Disposing = true;

            BeforeDispose();

            while (!Disposed && Started) {
                MisaMino.Abort();
                PerfectClear.Abort();
            }
        }
    }
}
