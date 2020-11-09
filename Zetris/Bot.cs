using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

using MisaMinoNET;
using PerfectClearNET;

namespace Zetris {
    public abstract class Bot<UI, B> where UI: IUI where B: Bot<UI, B>, new() {
        static B instance;
        public static B Instance => instance?? (instance = new B());

        public static uint PCThreadsMaximum => (uint)Environment.ProcessorCount;

        #region InputHelper
        protected static readonly int[][][,] pieces = new int[7][][,] {
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
                    if (pieces[piece][r][i, j] != -1) {
                        if (x + j < 0 || y - i < 0 || x + j > 9 || y - i > 39 || board[x + j, y - i] != 255) return false;
                        board[x + j, y - i] = pieces[piece][r][i, j];
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
        #endregion

        protected UI Window;

        protected int[,] board = new int[10, 40];

        protected int current;
        protected int? hold;
        protected int combo;
        protected List<int> queue = new List<int>();

        protected int misa_lasty;
        protected int b2b = 0;

        protected int[,] misaboard = new int[10, 40];
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
        protected abstract bool AllSpins();
        protected abstract MisaMinoParameters CurrentStyle();
        protected abstract bool C4W();
        protected abstract bool TSDOnly();
        protected abstract int Intelligence();
        protected abstract bool Allow180();
        protected abstract bool SRSPlus();
        protected abstract uint PCThreads();
        protected abstract bool GarbageBlocking();
        protected abstract bool Danger(); // set to true if bot is probably being used for cheating, in PUBLIC mode

        protected int[] getClippedQueue() => queue.Take(Math.Min(queue.Count, getPreviews())).ToArray();
        
        protected int getPerfectType() => Convert.ToInt32(getEnhancePerfect()) + Convert.ToInt32(getEnhancePerfect() && AllSpins()) * 2;

        protected bool isPCB2BEnding(int cleared, int piece, int r) => (cleared >= 4) || (AllSpins() && (
            piece == 0 ||
            piece == 1 ||
            (r != 2 && (
                piece == 2 ||
                piece == 3
            ))
        ));

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

        public void UpdateConfig() {
            if (!Started) return;

            MisaMinoParameters param = CurrentStyle();
            param.Parameters.strategy_4w = 400 * Convert.ToInt32(C4W());

            MisaMino.Configure(param, HoldAllowed(), AllSpins(), TSDOnly(), Intelligence(), Allow180(), SRSPlus());
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
