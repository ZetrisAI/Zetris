using System;
using System.Collections.Generic;
using System.Linq;

namespace Zetris {
    static class InputHelper {
        private static readonly int[][][,] pieces = new int[7][][,] {
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

        private static bool FitPiece(int[,] board, int piece, int x, int y, int r) {
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

        private static void fixInput(int piece, ref int x, ref int y, int r) {
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

        private static void fixOutput(int piece, ref int x, ref int y, int r) {
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

        public static bool FitPieceWithConvert(int[,] board, int piece, int x, int y, int r) {
            fixInput(piece, ref x, ref y, r);

            return FitPiece(board, piece, x, y, r);
        }

        public static int FindInputGoalX(int[,] board, int piece, int x, int y, int r, int d) {
            fixInput(piece, ref x, ref y, r);

            while (FitPiece(board, piece, x, y, r))
                x += d;
            x -= d;

            fixOutput(piece, ref x, ref y, r);
            return x;
        }

        public static int FindInputGoalY(int[,] board, int piece, int x, int y, int r) {
            fixInput(piece, ref x, ref y, r);

            while (FitPiece(board, piece, x, y, r))
                y--;
            y++;

            fixOutput(piece, ref x, ref y, r);
            return y;
        }

        public static int FixWall(int[,] board, int piece, int x, int y, int r) {
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

        public static int boardHeight(int[,] board, int height) {
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

        public static bool FixTspinMini(int[,] board, int height, int x, int y, int r) {
            fixInput(4, ref x, ref y, r);

            for (int i = 0; i < 4; i++) {
                for (int j = 0; j < 4; j++) {
                    if (pieces[4][r][i, j] != -1) {
                        int col = x + j;
                        for (int row = y - i; row < height; row++) {
                            if (board[col, row] != 255) {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        public static void ClearLines(int[,] board, out int cleared) {
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

        public static bool ApplyPiece(int[,] board, int piece, int x, int y, int r, out int c, out List<int[]> coords) {
            x--;
            y = 24 - y;

            c = 0;

            coords = new List<int[]>();

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (pieces[piece][r][i, j] != -1) {
                        if (x + j < 0 || y - i < 0 || x + j > 9 || y - i > 39 || board[x + j, y - i] != 255) return false;
                        board[x + j, y - i] = pieces[piece][r][i, j];
                        coords.Add(new [] {x + j, y - i});
                    }

            ClearLines(board, out c);

            return true;
        }

        public static bool BoardEquals(int[,] a, int[,] b) {
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 24; j++)
                    if ((a[i, j] == 255) != (b[i, j] == 255))
                        return false;

            return true;
        }

        public static uint NextRNG(uint rng) => rng * 0x5D588B65 + 0x269EC3;
        public static int RandomInt(ref uint rng, int count) => (int)((((rng = NextRNG(rng)) >> 16) * count) >> 16);

        public static void AddGarbageLine(int[,] board, int col) {
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

        public static void AddGarbage(int[,] board, int[] garbage, out int[] leftover) {
            leftover = garbage;

            if (garbage.Length == 0) return;

            int m = Math.Min(7, garbage.Length);

            for (int i = 0; i < m; i++)
                AddGarbageLine(board, garbage[i]);

            leftover = garbage.Skip(m).ToArray();
        }

        public static bool FuckItJustDoB2B(int[,] board, int minos) {
            int count = 0;

            for (int i = 25; i >= 0; i--) 
                for (int j = 0; j < 10; j++)
                    if ((count += Convert.ToInt32(board[j, i] != 255)) > minos)
                        return false;
            
            return true;
        }
    }
}
