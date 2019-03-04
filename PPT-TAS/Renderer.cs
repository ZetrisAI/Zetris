using System.Drawing;
using System.Windows.Forms;

namespace PPT_TAS {
    public class Renderer {
        static readonly (int, int)[][][] pieces = new (int, int)[7][][] {
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
            new (int, int)[4][] { // L
                new (int, int)[4] {(2, 0), (0, 1), (1, 1), (2, 1)},
                new (int, int)[4] {(1, 0), (1, 1), (1, 2), (2, 2)},
                new (int, int)[4] {(0, 1), (1, 1), (2, 1), (0, 2)},
                new (int, int)[4] {(0, 0), (1, 0), (1, 1), (1, 2)}
            },
            new (int, int)[4][] { // J
                new (int, int)[4] {(0, 0), (0, 1), (1, 1), (2, 1)},
                new (int, int)[4] {(1, 0), (2, 0), (1, 1), (1, 2)},
                new (int, int)[4] {(0, 1), (1, 1), (2, 1), (2, 2)},
                new (int, int)[4] {(1, 0), (1, 1), (0, 2), (1, 2)}
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
        
        PictureBox canvas;

        public int[,] board;
        public int[] queue;
        public int x, r, current, y, hold, cleared, bag;

        readonly SolidBrush[] bg = new SolidBrush[] {
            new SolidBrush(Color.FromArgb(255, Color.FromArgb(0x1A1A1A))),
            new SolidBrush(Color.FromArgb(255, Color.FromArgb(0x272727))),
            new SolidBrush(Color.FromArgb(255, Color.FromArgb(0x343434))),
            new SolidBrush(Color.FromArgb(255, Color.FromArgb(0x414141))),
            new SolidBrush(Color.FromArgb(255, Color.FromArgb(0x1A090A))),
            new SolidBrush(Color.FromArgb(255, Color.FromArgb(0x270D0E))),
            new SolidBrush(Color.FromArgb(255, Color.FromArgb(0x341314))),
            new SolidBrush(Color.FromArgb(255, Color.FromArgb(0x41181A)))
        };
        
        public void Draw() {
            Size px = new Size() {
                Width = canvas.Width / 10,
                Height = canvas.Height / 24
            };

            canvas.Image = new Bitmap(canvas.Width, canvas.Height);

            using (Graphics gfx = Graphics.FromImage(canvas.Image)) {
                for (int i = 0; i < 10; i++) {
                    for (int j = 0; j < 24; j++) {
                        Rectangle mino = new Rectangle(new Point(i * px.Width, (23 - j) * px.Height), px);

                        gfx.FillRectangle(bg[(j + cleared) % 4 + ((cleared + j >= 40)? 4 : 0)], mino);
                        gfx.DrawImage(Properties.Resources.Grid, mino);

                        if (board[i, j] != 255)
                            gfx.DrawImage((Image)Properties.Resources.ResourceManager.GetObject($"Mino_{board[i, j]}"), mino);
                    }
                }

                foreach ((int, int) offset in pieces[current][r]) {
                    Rectangle mino = new Rectangle(new Point((x - 1 + offset.Item1) * px.Width, (y - 1 + offset.Item2) * px.Height), px);
                    gfx.DrawImage((Image)Properties.Resources.ResourceManager.GetObject($"Mino_{current}"), mino);
                }

                gfx.Flush();
            }
        }

        public Renderer(ref PictureBox _canvas) {
            canvas = _canvas;
        }
    }
}
