using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PPT_TAS {
    public partial class Dialog : Form {
        public Dialog(int[,] board, int current, int yPos, int hold, int[] queue, int cleared, int bagIndex) {
            InitializeComponent();

            px = new Size() {
                Width = canvas.Width / 11,
                Height = canvas.Height / 24
            };

            Draw();
        }

        readonly SolidBrush[] bg = new SolidBrush[] {
            new SolidBrush(Color.FromArgb(255, Color.FromArgb(0x1A1A1A))),
            new SolidBrush(Color.FromArgb(255, Color.FromArgb(0x272727))),
            new SolidBrush(Color.FromArgb(255, Color.FromArgb(0x343434))),
            new SolidBrush(Color.FromArgb(255, Color.FromArgb(0x414141)))
        };

        readonly Size px;

        void Draw() {
            canvas.Image = new Bitmap(canvas.Width, canvas.Height);

            using (Graphics gfx = Graphics.FromImage(canvas.Image)) {
                for (int i = 0; i < 10; i++) {
                    for (int j = 0; j < 24; j++) {
                        gfx.FillRectangle(bg[j % 4], new Rectangle(new Point(i * px.Width, (23 - j) * px.Height), px));
                    }
                }

                gfx.Flush();
            }
        }

        public int desiredX = 4, desiredR = 0;
        public bool desiredHold = false;

        private void valueX_ValueChanged(object sender, EventArgs e) {
            // desiredX
            Draw();
        }

        private void valueR_ValueChanged(object sender, EventArgs e) {
            if (valueR.Value == -1) {
                valueR.Value = 3;
                return;
            }

            if (valueR.Value == 4) {
                valueR.Value = 0;
                return;
            }
            //desiredR
            Draw();
        }

        private void valueHold_CheckedChanged(object sender, EventArgs e) {
            //desiredHold
            Draw();
        }

        bool[] keys = new bool[4];

        private void Dialog_KeyDown(object sender, KeyEventArgs e) {
            e.Handled = true;

            switch (e.KeyCode) {
                case Keys.Left:
                    valueX.Value--;
                    break;

                case Keys.Right:
                    valueX.Value++;
                    break;

                case Keys.Up:
                    if (!keys[0]) {
                        // Hard Drop
                    }
                    keys[0] = true;
                    break;

                case Keys.Down:
                    // placeholder for Soft Drop in the far future
                    break;

                case Keys.Z:
                    if (!keys[1]) {
                        // Rotate CCW
                    }
                    keys[1] = true;
                    break;

                case Keys.X:
                    if (!keys[2]) {
                        // Rotate CW
                    }
                    keys[2] = true;
                    break;

                case Keys.Space:
                    if (!keys[3]) {
                        // Toggle Hold
                    }
                    keys[3] = true;
                    break;

                default:
                    e.Handled = false;
                    break;
            }
        }

        private void Dialog_KeyUp(object sender, KeyEventArgs e) {
            e.Handled = true;

            switch (e.KeyCode) {
                case Keys.Left:
                case Keys.Right:
                case Keys.Down:  // placeholder
                    break;

                case Keys.Up:
                    keys[0] = false;
                    break;

                case Keys.Z:
                    keys[1] = false;
                    break;

                case Keys.X:
                    keys[2] = false;
                    break;

                case Keys.Space:
                    keys[3] = false;
                    break;

                default:
                    e.Handled = false;
                    break;
            }
        }
    }
}
