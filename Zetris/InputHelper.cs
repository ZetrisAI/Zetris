using System;

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
                        try {
                            if (board[x + j, y - i] != 255) {
                                return false;
                            }
                        } catch {
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

        public static void ApplyPiece(int[,] board, int piece, int x, int y, int r, out int c) {
            x--;
            y = 24 - y;

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (pieces[piece][r][i, j] != -1)
                        board[x + j, y - i] = pieces[piece][r][i, j];

            ClearLines(board, out c);
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

        public static void AddGarbage(int[,] board, uint rng, int lines) {
            if (lines == 0) return;

            int col = RandomInt(ref rng, 10);

            for (int i = 0; i < lines; i++) {
                if (70 < RandomInt(ref rng, 99)) {
                    int newCol = RandomInt(ref rng, 9);
                    col = newCol + Convert.ToInt32(newCol >= col);
                }

                AddGarbageLine(board, col);
            }
        }
    }
}
