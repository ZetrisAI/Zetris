using System.Drawing;
using System.Windows.Forms;

namespace PPT_TAS {
    public class Renderer {
        PictureBox canvas;

        public int[,] board;
        public int[] queue;
        public int current, y, hold, cleared, bag;

        readonly SolidBrush[] bg = new SolidBrush[] {
            new SolidBrush(Color.FromArgb(255, Color.FromArgb(0x1A1A1A))),
            new SolidBrush(Color.FromArgb(255, Color.FromArgb(0x272727))),
            new SolidBrush(Color.FromArgb(255, Color.FromArgb(0x343434))),
            new SolidBrush(Color.FromArgb(255, Color.FromArgb(0x414141)))
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

                        gfx.FillRectangle(bg[j % 4], mino);

                        if (board[i, j] != 255)
                            gfx.DrawImage((Image)Properties.Resources.ResourceManager.GetObject($"Mino_{board[i, j]}"), mino);
                    }
                }

                gfx.Flush();
            }
        }

        public Renderer(ref PictureBox _canvas) {
            canvas = _canvas;
        }
    }
}
