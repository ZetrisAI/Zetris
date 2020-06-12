using System;
using System.Drawing;
using System.Windows.Forms;

namespace Zetris {
    public partial class Dialog: Form {
        private Renderer gfx;

        private static readonly (int, int)[][][] srs = new (int, int)[4][][] {
            new (int, int)[2][] { // 0 - 0
                new (int, int)[5] {( 0,  0), ( 1,  0), ( 1, -1), ( 0,  2), ( 1,  2)},
                new (int, int)[5] {( 0,  0), (-1,  0), (-1, -1), ( 0,  2), (-1,  2)}
            },
            new (int, int)[2][] { // 1 - CW
                new (int, int)[5] {( 0,  0), (-1,  0), (-1,  1), ( 0, -2), (-1, -2)},
                new (int, int)[5] {( 0,  0), (-1,  0), (-1,  1), ( 0, -2), (-1, -2)}
            },
            new (int, int)[2][] { // 2 - 180
                new (int, int)[5] {( 0,  0), (-1,  0), (-1, -1), ( 0,  2), (-1,  2)},
                new (int, int)[5] {( 0,  0), ( 1,  0), ( 1, -1), ( 0,  2), ( 1,  2)}
            },
            new (int, int)[2][] { // 3 - CCW
                new (int, int)[5] {( 0,  0), ( 1,  0), ( 1,  1), ( 0, -2), ( 1, -2)},
                new (int, int)[5] {( 0,  0), ( 1,  0), ( 1,  1), ( 0, -2), ( 1, -2)}
            }
        };

        private static readonly (int, int)[][][] srsI = new (int, int)[4][][] {
            new (int, int)[2][] { // 0 - 0
                new (int, int)[5] {( 0,  0), ( 2,  0), (-1,  0), ( 2,  1), (-1, -2)},
                new (int, int)[5] {( 0,  0), ( 1,  0), (-2,  0), ( 1, -2), (-2,  1)}
            },
            new (int, int)[2][] { // 1 - CW
                new (int, int)[5] {( 0,  0), ( 1,  0), (-2,  0), ( 1, -2), (-2,  1)},
                new (int, int)[5] {( 0,  0), (-2,  0), ( 1,  0), (-2, -1), ( 1,  2)}
            },
            new (int, int)[2][] { // 2 - 180
                new (int, int)[5] {( 0,  0), (-2,  0), ( 1,  0), (-2, -1), ( 1,  2)},
                new (int, int)[5] {( 0,  0), (-1,  0), ( 2,  0), (-1,  2), ( 2, -1)}
            },
            new (int, int)[2][] { // 3 - CCW
                new (int, int)[5] {( 0,  0), (-1,  0), ( 2,  0), (-1,  2), ( 2, -1)},
                new (int, int)[5] {( 0,  0), ( 2,  0), (-1,  0), ( 2,  1), (-1, -2)}
            }
        };

        private static readonly int initX = 4, initR = 0;
        private int initY;

        public int desiredX = initX, desiredY, desiredR = initR;
        public bool desiredHold = false;

        private int[,] board;
        private int[] queue;
        private int current, hold;

        private int offsetX = 0, offsetY = 0;

        private void ApplyOffset(int r) {
            int c = desiredHold ? ((hold == 255) ? queue[0] : hold) : current;

            desiredX -= offsetX;
            desiredY -= offsetY;

            if (c == 6) {
                switch (r) {
                    case 0:
                        offsetX = 0;
                        offsetY = 0;
                        break;

                    case 1:
                        offsetX = 1;
                        offsetY = 0;
                        break;

                    case 2:
                        offsetX = 1;
                        offsetY = 0;
                        break;

                    case 3:
                        offsetX = 0;
                        offsetY = 0;
                        break;
                }

            } else {
                offsetX = 0;
                offsetY = 0;
            }

            desiredX += offsetX;
            desiredY += offsetY;
        }

        private readonly static int DAS = 100;
        private readonly static int ARR = 16;

        private void ApplyARR(object o) {
            if (o != null && o.GetType() == typeof(Timer)) {
                ((Timer)o).Interval = ARR;
            }
        }

        private void StopDAS(object o) {
            if (o != null && o.GetType() == typeof(Timer)) {
                ((Timer)o).Enabled = false;
                ((Timer)o).Dispose();
                o = null;
            }
        }

        public Dialog(int[,] _board, int _current, int _yPos, int _hold, int[] _queue, int? _cleared, int _bagIndex, int _garbage) {
            InitializeComponent();

            actions = new Action<object, EventArgs>[8] {
                (object sender, EventArgs e) => {
                    valueX.Value--;
                    StopDAS(timers[1]);
                    ApplyARR(sender);
                },
                (object sender, EventArgs e) => {
                    valueX.Value++;
                    StopDAS(timers[0]);
                    ApplyARR(sender);
                },
                (object sender, EventArgs e) => {
                    if (sender != null) {
                        StopDAS(sender);
                    } else {
                        for (int i = 0; i < 8; i++) {
                            StopDAS(timers[i]);
                        }

                        this.Close();
                    }
                },
                (object sender, EventArgs e) => {
                    Softdrop();
                    ApplyARR(sender);
                },
                (object sender, EventArgs e) => {
                    if (sender != null) {
                        StopDAS(sender);
                    } else {
                        valueR.Value--;
                    }
                },
                (object sender, EventArgs e) => {
                    if (sender != null) {
                        StopDAS(sender);
                    } else {
                        valueR.Value++;
                    }
                },
                (object sender, EventArgs e) => {
                    if (sender != null) {
                        StopDAS(sender);
                    } else {
                        valueHold.Checked = !valueHold.Checked;
                    }
                },
                (object sender, EventArgs e) => {
                    for (int i = 0; i < 8; i++) {
                        StopDAS(timers[i]);
                    }
                    this.Close();
                    desiredX = -100;
                }
            };

            board = _board;
            current = _current;
            desiredY = initY = _yPos;
            hold = _hold;
            queue = _queue;

            gfx = new Renderer(ref canvasBoard, ref canvasQueue, ref canvasHold) {
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

            gfx.DrawBackground();
            gfx.DrawForeground();
            gfx.DrawExtras();

            garbage.Location = new Point(
                garbage.Location.X + canvasBoard.Width / 10 * _garbage,
                garbage.Location.Y
            );
        }

        private bool TestCollision(int x, int y, int r) {
            int c = desiredHold? ((hold == 255)? queue[0] : hold) : current;

            foreach ((int, int) offset in Renderer.pieces[c][r]) {
                int i = x - 1 + offset.Item1, j = 24 - y - offset.Item2;
                
                if (i < 0 || i > 9 || j < 0 || j > 39) return false;

                if (board[x - 1 + offset.Item1, 24 - y - offset.Item2] != 255) {
                    return false;
                }
            }

            return true;
        }

        private bool TestRotation(int r, int d) {
            int c = desiredHold ? ((hold == 255) ? queue[0] : hold) : current;

            ApplyOffset(0);

            foreach ((int, int) transl in (c == 6)? srsI[r][d] : srs[r][d]) {
                if (TestCollision(desiredX + transl.Item1, desiredY - transl.Item2, r)) {
                    desiredX += transl.Item1;
                    desiredY -= transl.Item2;

                    ApplyOffset(r);
                    return true;
                }
            }

            ApplyOffset(desiredR);
            return false;
        }

        private void valueX_ValueChanged(object sender, EventArgs e) {
            if (TestCollision((int)valueX.Value - offsetX, desiredY - offsetY, desiredR)) {
                desiredX = (int)valueX.Value;

                gfx.x = desiredX - offsetX;
                gfx.DrawForeground();

            } else {
                valueX.Value = desiredX;
            }
        }

        private bool Softdrop() {
            bool ret = TestCollision(desiredX - offsetX, desiredY + 1, desiredR);

            if (ret) {
                desiredY++;

                gfx.y = desiredY - offsetY;
                gfx.DrawForeground();
            }

            return ret;
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

            if (TestRotation((int)valueR.Value, (((desiredR - (int)valueR.Value + 4) % 4) & 2) >> 1)) {
                desiredR = (int)valueR.Value;

                valueX.Value = desiredX;

                gfx.x = desiredX - offsetX;
                gfx.y = desiredY - offsetY;
                gfx.r = desiredR;
                gfx.DrawForeground();

            } else {
                valueR.Value = desiredR;
            }
        }

        private void valueHold_CheckedChanged(object sender, EventArgs e) {
            desiredHold = valueHold.Checked;

            offsetX = 0;
            offsetY = 0;

            gfx.y = (desiredY = initY);

            valueX.Value = initX;
            valueR.Value = initR;

            gfx.useHold = desiredHold;
            gfx.DrawForeground();
            gfx.DrawExtras();
        }

        protected override void OnShown(EventArgs e) {
            Focus();
            Activate();
        }

        private readonly Keys[] keycodes = new Keys[8] {
            /*Keys.J,
            Keys.L,
            Keys.I,
            Keys.K,
            Keys.A,
            Keys.S,
            Keys.C,
            Keys.Escape*/
            Keys.Left,
            Keys.Right,
            Keys.Up,
            Keys.Down,
            Keys.X,
            Keys.W,
            Keys.Q,
            Keys.Escape
        };

        private bool[] keys = new bool[8];

        private Action<object, EventArgs>[] actions;
        private Timer[] timers = new Timer[8];

        private void Dialog_KeyDown(object sender, KeyEventArgs e) {
            for (int i = 0; i < 8; i++) {
                if (e.KeyCode == keycodes[i]) {
                    if (!keys[i]) {
                        actions[i].Invoke(null, EventArgs.Empty);

                        timers[i] = new Timer();
                        timers[i].Tick += new EventHandler(actions[i]);
                        timers[i].Interval = DAS;
                        timers[i].Enabled = true;
                    }

                    keys[i] = true;

                    e.Handled = true;
                    return;
                }
            }
        }

        private void Dialog_KeyUp(object sender, KeyEventArgs e) {
            for (int i = 0; i < 8; i++) {
                if (e.KeyCode == keycodes[i]) {
                    if (timers[i] != null) {
                        timers[i].Enabled = false;
                        timers[i].Dispose();
                        timers[i] = null;
                    }

                    keys[i] = false;

                    e.Handled = true;
                    return;
                }
            }
        }

        private void Dialog_Closing(object sender, FormClosingEventArgs e) {
            if ((desiredHold ? ((hold == 255) ? queue[0] : hold) : current) == 5) desiredR = 0;

            while (Softdrop());
            desiredX -= offsetX;
            desiredY -= offsetY;
        }
    }
}
