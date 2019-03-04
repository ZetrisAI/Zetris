using System;
using System.Windows.Forms;

namespace PPT_TAS {
    public partial class Dialog: Form {
        private Renderer gfx;

        public int desiredX = 4, desiredR = 0;
        public bool desiredHold = false;

        private int[,] board;
        private int current, y, hold;

        public Dialog(int[,] _board, int _current, int _yPos, int _hold, int[] _queue, int _cleared, int _bagIndex) {
            InitializeComponent();

            board = _board;
            current = _current;
            y = _yPos;
            hold = _hold;

            gfx = new Renderer(ref canvas) {
                board = _board,
                current = _current,
                x = desiredX,
                y = _yPos,
                r = desiredR,
                hold = _hold,
                queue = _queue,
                cleared = _cleared,
                bag = _bagIndex
            };

            labelHold.Text = y.ToString();

            gfx.Draw();
        }

        private bool movementCollision(int x) {
            foreach ((int, int) offset in Renderer.pieces[current][desiredR]) {
                try {
                    if (board[x - 1 + offset.Item1, 24 - y - offset.Item2] != 255) {
                        return false;
                    }
                } catch {
                    return false;
                }
            }

            return true;
        }

        private void valueX_ValueChanged(object sender, EventArgs e) {
            if (movementCollision((int)valueX.Value)) {
                desiredX = (int)valueX.Value;
                gfx.x = desiredX;
                gfx.Draw();
            } else {
                valueX.Value = desiredX;
            }
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

            if (true /* SRS check? */) {
                desiredR = (int)valueR.Value;
                gfx.r = desiredR;
                gfx.Draw();
            } else {
                valueR.Value = desiredR;
            }
        }

        private void valueHold_CheckedChanged(object sender, EventArgs e) {
            //desiredHold
            gfx.Draw();
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
                        valueR.Value--;
                    }
                    keys[1] = true;
                    break;

                case Keys.X:
                    if (!keys[2]) {
                        valueR.Value++;
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
