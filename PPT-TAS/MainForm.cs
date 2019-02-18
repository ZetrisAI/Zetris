using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

using ScpDriverInterface;

namespace PPT_TAS {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }

        ProcessMemory PPT = new ProcessMemory("puyopuyotetris", false);

        ScpBus scp = new ScpBus();
        bool gamepadPluggedIn = false;
        X360Controller gamepad = new X360Controller();

        private void buttonGamepad_Click(object sender, EventArgs e) {
            scp.UnplugAll();
            scp = new ScpBus();
            gamepad = new X360Controller();

            if (!gamepadPluggedIn) scp.PlugIn(4);

            gamepadPluggedIn = !gamepadPluggedIn;
        }

        int frames, globalFrames;

        int[,] board = new int[10, 40];

        int state = 0;
        int piece = 0;
        int[] queue = new int[5];
        bool register = false;

        private void runLogic() {
            int y = GameHelper.getPiecePositionY(PPT);

            int boardAddress = GameHelper.boardAddress(PPT);
            for (int i = 0; i < 10; i++) {
                int columnAddress = PPT.ReadInt32(new IntPtr(boardAddress + i * 0x08));
                for (int j = 0; j < 40; j++) {
                    board[i, j] = PPT.ReadByte(new IntPtr(columnAddress + j * 0x04));
                }
            }

            if (GameHelper.boardAddress(PPT) != 0x0 && GameHelper.OutsideMenu(PPT) && GameHelper.getBigFrameCount(PPT) != 0x0 && GameHelper.IsTetChallenge(PPT)) {
                int drop = GameHelper.getPieceDropped(PPT);

                int current = GameHelper.getCurrentPiece(PPT);

                int piecesAddress = GameHelper.piecesAddress(PPT);
                int i;

                int[] pieces = new int[5];
                for (i = 0; i < 5; i++) {
                    pieces[i] = PPT.ReadByte(new IntPtr(piecesAddress + i * 0x04));
                }

                if (GameHelper.getBigFrameCount(PPT) < 6) {
                    register = false;
                    inputStarted = false;
                }

                if (drop != state && drop == 1) {
                    register = true;
                }

                if (((register && !pieces.SequenceEqual(queue) && current == queue[0]) || (current != piece && piece == 255)) && y <= 5) {
                    PPT.Suspend();
                    ScanTimer.Enabled = false;

                    Dialog q = new Dialog(
                        board,
                        current,
                        y,
                        GameHelper.getHold(PPT),
                        pieces
                            .Concat(GameHelper.getNextFromBags(PPT))
                            .Concat(GameHelper.getNextFromRNG(PPT, 110))
                            .ToArray(),
                        GameHelper.getCleared(PPT),
                        GameHelper.getBagIndex(PPT)
                    );
                    q.ShowDialog();

                    ScanTimer.Enabled = true;
                    PPT.Resume();

                    desiredX = q.desiredX;
                    desiredR = q.desiredR;
                    desiredHold = q.desiredHold;

                    inputStarted = true;
                    register = false;
                }

                state = drop;
                piece = current;

                if (!register)
                    queue = (int[])pieces.Clone();
            }
        }

        bool inputStarted = false;
        int desiredX, desiredR;
        bool desiredHold;

        X360Buttons previousInputs = X360Buttons.None;

        private void applyInputs() {
            int nextFrame = GameHelper.getFrameCount(PPT);

            if (GameHelper.boardAddress(PPT) != 0x0 && GameHelper.OutsideMenu(PPT) && nextFrame > 0 && GameHelper.getBigFrameCount(PPT) != 0x0) {
                previousInputs = gamepad.Buttons;

                if (nextFrame != frames) {
                    gamepad.Buttons = X360Buttons.None;

                    if (inputStarted) {
                        int pieceX = GameHelper.getPiecePositionX(PPT);
                        int pieceR = GameHelper.getPieceRotation(PPT);

                        if (GameHelper.getPieceDropped(PPT) == 1) {
                            inputStarted = false;
                            return;
                        }

                        if (desiredHold) {
                            gamepad.Buttons |= X360Buttons.RightBumper;
                        }

                        if (desiredX == pieceX && desiredR == pieceR) {
                            gamepad.Buttons |= X360Buttons.Up;
                        } else {
                            if (desiredX != pieceX)
                                if (desiredX < pieceX) {
                                    gamepad.Buttons |= X360Buttons.Left;
                                } else {
                                    gamepad.Buttons |= X360Buttons.Right;
                                }

                            if (desiredR != pieceR)
                                if (desiredR == 3) {
                                    gamepad.Buttons |= X360Buttons.A;
                                } else {
                                    gamepad.Buttons |= X360Buttons.B;
                                }
                        }
                    }
                }

                gamepad.Buttons &= ~previousInputs;

                frames = nextFrame;

            } else {
                gamepad.Buttons = X360Buttons.None;
            }
            
            scp.Report(4, gamepad.GetReport());
        }

        private void updateUI() {
            buttonGamepad.Text = gamepadPluggedIn? "Disconnect" : "Connect";
            valueGamepadInputs.Text = gamepad.Buttons.ToString();
        }
                
        Stopwatch timer = new Stopwatch();
        int framesSkipped = 0;
        readonly double waitTime = Math.Round(0.001 * Stopwatch.Frequency);

        private void Loop(object sender, EventArgs e) {
            for (int _ = 0; _ < 20; _++) {
                timer = new Stopwatch();
                timer.Start();

                bool actualFrame = false;

                if (PPT.CheckProcess()) {
                    PPT.TrustProcess = true;

                    int prev = globalFrames;
                    globalFrames = GameHelper.getMenuFrameCount(PPT);

                    if (actualFrame = globalFrames > prev) {
                        runLogic();
                        applyInputs();

                        framesSkipped += globalFrames - prev - 1;
                        if (prev < 60) framesSkipped = 0;
                    }

                    PPT.TrustProcess = false;
                }

                updateUI();
                timer.Stop();

                if (actualFrame) {
                    valueFrametime.Text = $"{timer.ElapsedMilliseconds} ms";
                    valueSkipped.Text = framesSkipped.ToString();
                }

                Stopwatch wait = Stopwatch.StartNew();
                while (wait.ElapsedTicks < waitTime) {}
            }
        }

        private void buttonResume_Click(object sender, EventArgs e) {
            PPT.Resume();
        }

        void MainForm_Load(object sender, EventArgs e) {
            scp.UnplugAll();
            scp.PlugIn(4);
            gamepadPluggedIn = true;
        }

        void MainForm_Closing(object sender, EventArgs e) {
            scp.UnplugAll();
            gamepadPluggedIn = false;
        }
    }
}