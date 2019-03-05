using System.Drawing;
using System.Windows.Forms;

namespace PPT_TAS {
    public class Renderer {
        public static readonly (int, int)[][][] pieces = new (int, int)[7][][] {
            new (int, int)[4][] { // S
                new (int, int)[4] {(1, 0), (2, 0), (0, 1), (1, 1)},
                new (int, int)[4] {(1, 0), (1, 1), (2, 1), (2, 2)},
                new (int, int)[4] {(1, 1), (2, 1), (0, 2), (1, 2)},
                new (int, int)[4] {(0, 0), (0, 1), (1, 1), (1, 2)}
            },
            new (int, int)[4][] { // Z
                new (int, int)[4] {(0, 0), (1, 0), (1, 1), (2, 1)},
                new (int, int)[4] {(2, 0), (1, 1), (2, 1), (1, 2)},
                new (int, int)[4] {(0, 1), (1, 1), (1, 2), (2, 2)},
                new (int, int)[4] {(1, 0), (0, 1), (1, 1), (0, 2)}
            },
            new (int, int)[4][] { // J
                new (int, int)[4] {(0, 0), (0, 1), (1, 1), (2, 1)},
                new (int, int)[4] {(1, 0), (2, 0), (1, 1), (1, 2)},
                new (int, int)[4] {(0, 1), (1, 1), (2, 1), (2, 2)},
                new (int, int)[4] {(1, 0), (1, 1), (0, 2), (1, 2)}
            },
            new (int, int)[4][] { // L
                new (int, int)[4] {(2, 0), (0, 1), (1, 1), (2, 1)},
                new (int, int)[4] {(1, 0), (1, 1), (1, 2), (2, 2)},
                new (int, int)[4] {(0, 1), (1, 1), (2, 1), (0, 2)},
                new (int, int)[4] {(0, 0), (1, 0), (1, 1), (1, 2)}
            },
            new (int, int)[4][] { // T
                new (int, int)[4] {(1, 0), (0, 1), (1, 1), (2, 1)},
                new (int, int)[4] {(1, 0), (1, 1), (2, 1), (1, 2)},
                new (int, int)[4] {(0, 1), (1, 1), (2, 1), (1, 2)},
                new (int, int)[4] {(1, 0), (0, 1), (1, 1), (1, 2)}
            },
            new (int, int)[4][] { // O
                new (int, int)[4] {(1, 0), (2, 0), (1, 1), (2, 1)},
                new (int, int)[4] {(1, 0), (2, 0), (1, 1), (2, 1)},
                new (int, int)[4] {(1, 0), (2, 0), (1, 1), (2, 1)},
                new (int, int)[4] {(1, 0), (2, 0), (1, 1), (2, 1)}
            },
            new (int, int)[4][] { // I
                new (int, int)[4] {(0, 1), (1, 1), (2, 1), (3, 1)},
                new (int, int)[4] {(2, 0), (2, 1), (2, 2), (2, 3)},
                new (int, int)[4] {(0, 2), (1, 2), (2, 2), (3, 2)},
                new (int, int)[4] {(1, 0), (1, 1), (1, 2), (1, 3)}
            }
        };

        readonly SolidBrush[] BackgroundColors = new SolidBrush[] {
            new SolidBrush(Color.FromArgb(255, Color.FromArgb(0x1A1A1A))),
            new SolidBrush(Color.FromArgb(255, Color.FromArgb(0x272727))),
            new SolidBrush(Color.FromArgb(255, Color.FromArgb(0x343434))),
            new SolidBrush(Color.FromArgb(255, Color.FromArgb(0x414141))),
            new SolidBrush(Color.FromArgb(255, Color.FromArgb(0x1A090A))),
            new SolidBrush(Color.FromArgb(255, Color.FromArgb(0x270D0E))),
            new SolidBrush(Color.FromArgb(255, Color.FromArgb(0x341314))),
            new SolidBrush(Color.FromArgb(255, Color.FromArgb(0x41181A)))
        };
        readonly Pen SeparatorPen = new Pen(Color.FromArgb(255, Color.FromArgb(0x343434)));

        PictureBox canvasBoard, canvasQueue, canvasHold;
        Size pxBoard, pxQueue, pxHold;

        public int[,] board;
        public int[] queue;
        public int x, r, current, y, hold, cleared, bag;
        public bool useHold;

        private int ghostY() {
            int c = useHold? ((hold == 255)? queue[0] : hold) : current;

            for (int i = y; i <= 25; i++) {
                foreach ((int, int) offset in pieces[c][r]) {
                    try {
                        if (board[x - 1 + offset.Item1, 24 - i - offset.Item2] != 255) {
                            return i - 1;
                        }
                    } catch {
                        return i - 1;
                    }
                }
            }

            return 24;
        }
        
        public void DrawBackground() {
            canvasBoard.BackgroundImage = new Bitmap(canvasBoard.Width, canvasBoard.Height);

            using (Graphics gfx = Graphics.FromImage(canvasBoard.BackgroundImage)) {
                for (int i = 0; i < 10; i++) {
                    for (int j = 0; j < 24; j++) {
                        Rectangle mino = new Rectangle(new Point(i * pxBoard.Width, (23 - j) * pxBoard.Height), pxBoard);

                        gfx.FillRectangle(BackgroundColors[(j + cleared) % 4 + ((cleared + j >= 40) ? 4 : 0)], mino);
                        gfx.DrawImage(Properties.Resources.Grid, mino);

                        if (board[i, j] != 255)
                            gfx.DrawImage((Image)Properties.Resources.ResourceManager.GetObject($"Mino_{board[i, j]}"), mino);
                    }
                }

                gfx.Flush();
            }
        }

        public void DrawExtras() {
            canvasQueue.Image = new Bitmap(canvasQueue.Width, canvasQueue.Height);

            using (Graphics gfx = Graphics.FromImage(canvasQueue.Image)) {
                int i = (useHold && hold == 255)? 1 : 0;
                for (int h = 0; h < canvasQueue.Image.Height; h += pxQueue.Height) {
                    if ((12 - bag) % 7 == i % 7 && h != 0) {
                        gfx.DrawLine(SeparatorPen, pxQueue.Width / 20, h + pxQueue.Height / 4, pxQueue.Width - (pxQueue.Width / 20) - 1, h + pxQueue.Height / 4);
                        h += (pxQueue.Height / 4) * 2 + 1;
                    }

                    Rectangle mino = new Rectangle(new Point(0, h), pxQueue);
                    gfx.DrawImage((Image)Properties.Resources.ResourceManager.GetObject($"Queue_{queue[i++]}"), mino);
                }

                gfx.Flush();
            }

            canvasHold.Image = new Bitmap(canvasHold.Width, canvasHold.Height);

            using (Graphics gfx = Graphics.FromImage(canvasHold.Image)) {
                int h = (useHold)? current : hold;
                
                if (h != 255) {
                    Rectangle mino = new Rectangle(new Point(0, 0), pxHold);
                    gfx.DrawImage((Image)Properties.Resources.ResourceManager.GetObject($"Queue_{h}"), mino);
                }

                gfx.Flush();
            }
        }

        public void DrawForeground() {
            canvasBoard.Image = new Bitmap(canvasBoard.Width, canvasBoard.Height);

            using (Graphics gfx = Graphics.FromImage(canvasBoard.Image)) {
                int c = useHold ? ((hold == 255) ? queue[0] : hold) : current;

                int ghost = ghostY();

                foreach ((int, int) offset in pieces[c][r]) {
                    Rectangle mino = new Rectangle(new Point((x - 1 + offset.Item1) * pxBoard.Width, (ghost - 1 + offset.Item2) * pxBoard.Height), pxBoard);
                    gfx.DrawImage((Image)Properties.Resources.ResourceManager.GetObject($"Ghost_{c}"), mino);
                    gfx.DrawImage((Image)Properties.Resources.ResourceManager.GetObject($"Ghost_Deco"), mino);
                }

                foreach ((int, int) offset in pieces[c][r]) {
                    Rectangle mino = new Rectangle(new Point((x - 1 + offset.Item1) * pxBoard.Width, (y - 1 + offset.Item2) * pxBoard.Height), pxBoard);
                    gfx.DrawImage((Image)Properties.Resources.ResourceManager.GetObject($"Mino_{c}"), mino);
                }

                gfx.Flush();
            }
        }

        public Renderer(ref PictureBox _canvasBoard, ref PictureBox _canvasQueue, ref PictureBox _canvasHold) {
            canvasBoard = _canvasBoard;
            canvasQueue = _canvasQueue;
            canvasHold = _canvasHold;

            pxBoard = new Size() {
                Width = canvasBoard.Width / 10,
                Height = canvasBoard.Height / 24
            };

            pxQueue = new Size() {
                Width = canvasQueue.Width,
                Height = canvasQueue.Width / 2
            };

            pxHold = new Size() {
                Width = canvasHold.Width,
                Height = canvasHold.Height
            };
        }
    }
}
