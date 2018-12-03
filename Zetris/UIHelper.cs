using System.Drawing;
using System.Windows.Forms;

namespace Zetris {
    class UIHelper {
        public static Color getTetrominoColor(int x) {
            switch (x) {
                case 0:
                    return Color.FromArgb(0, 255, 0);

                case 1:
                    return Color.FromArgb(255, 0, 0);

                case 2:
                    return Color.FromArgb(0, 0, 255);

                case 3:
                    return Color.FromArgb(255, 63, 0);

                case 4:
                    return Color.FromArgb(63, 0, 255);

                case 5:
                    return Color.FromArgb(255, 255, 0);

                case 6:
                    return Color.FromArgb(0, 255, 255);

                case 7:
                    return Color.FromArgb(239, 206, 26);

                case 9:
                    return Color.FromArgb(255, 255, 255);
            }

            return Color.Transparent;
        }

        public static void drawBoard(PictureBox canvas, int[,] board) {
            canvas.Image = new Bitmap(canvas.Width, canvas.Height);
            using (Graphics gfx = Graphics.FromImage(canvas.Image)) {
                for (int i = 0; i < 10; i++) {
                    for (int j = 0; j < 25; j++) {
                        Rectangle mino = new Rectangle(i * (canvas.Width / 10), (24 - j) * (canvas.Height / 25), canvas.Width / 10, canvas.Height / 25);
                        gfx.FillRectangle(new SolidBrush(UIHelper.getTetrominoColor(board[i, j])), mino);

                        mino.Width--;
                        mino.Height--;
                        gfx.DrawRectangle(new Pen(Color.Black), mino);
                    }
                }

                gfx.DrawLine(new Pen(Color.Red), 0, canvas.Height / 5, canvas.Width, canvas.Height / 5);
                gfx.Flush();
            }
        }
    }
}
