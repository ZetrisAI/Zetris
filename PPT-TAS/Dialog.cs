using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace PPT_TAS {
    public partial class Dialog : Form {
        public Dialog(int[,] board, int current, int yPos, int hold, int[] queue, int cleared)
        {

            VisibleBoard = board;
            Hold = hold;
            PieceQueue = queue;
            LinesCleared = cleared;
            byte[,] Piece = Tetromino.GetTetromino((byte)current);
            X = (current == (int)Blocks.O ? 4 : 3);
            Y = yPos;
            int i = 0;
            for (int y = 0; y < Piece.GetLength(1); y++)
            {
                for (int x = 0; x < Piece.GetLength(0); x++)
                {
                    if (Piece[x, y] == 1)
                    {
                        board[X + x, Y + y + 13] = current;
                        CurrentPiece[i++] = (X + x, Y + y + 13);
                    }
                }
            }

            brush = new SolidBrush(BackgroundColor);
            pen = new Pen(Color.Black);
            image = new Bitmap(Size.Width * Blocksize_ + 1, Size.Height * Blocksize_ + 1);
            graphics = Graphics.FromImage(image);
            graphics.FillRectangle(brush, 0, 0, image.Width, image.Height);
            for (int x = 0; x < Size.Width; x++)
            {
                graphics.DrawLine(pen, x * Blocksize_, 0, x * Blocksize_, image.Height);
            }
            for (int y = 0; y < Size.Height; y++)
            {
                graphics.DrawLine(pen, 0, y * Blocksize_, image.Width, y * Blocksize_);
            }
            graphics.DrawRectangle(pen, 0, 0, image.Width - 1, image.Height - 1);
            //UpdateButtons();
            //Piece = new Tetromino(PieceQueue[PiecePointer]);

            //draws background, separators and outline for the board (in that order)
            graphics.FillRectangle(brush, 0, 0, image.Width, image.Height);
            for (int x = 0; x < Size.Width; x++)
            {
                graphics.DrawLine(pen, x * Blocksize_, 0, x * Blocksize_, image.Height);
            }
            for (int y = 0; y < Size.Height; y++)
            {
                graphics.DrawLine(pen, 0, y * Blocksize_, image.Width, y * Blocksize_);
            }
            graphics.DrawRectangle(pen, 0, 0, image.Width - 1, image.Height - 1);

            InitializeComponent();

            DrawBoard();

            DrawHoldAndQueue();
        }

        (int x, int y)[] CurrentPiece = new (int x, int y)[4];
        int X = 4, Y;

        public int desiredX = 4, desiredR = 0;
        public bool desiredHold = false;

        private void valueX_ValueChanged(object sender, EventArgs e) {
            desiredX = Convert.ToInt32(valueX.Value);
        }

        private void valueR_ValueChanged(object sender, EventArgs e) {
            desiredR = Convert.ToInt32(valueR.Value);
        }

        private void valueHold_CheckedChanged(object sender, EventArgs e) {
            desiredHold = valueHold.Checked;
        }

        struct Instruction
        {
            public int desiredX, desiredR;
            public bool desiredHold;

            public Instruction(int desiredx, int desiredr, bool desiredhold)
            {
                desiredX = desiredx;
                desiredR = desiredr;
                desiredHold = desiredhold;
            }
        }

        void UpdateButtons()
        {
            Undo.Enabled = Instructions.Count > 0;
            PlacePiece.Enabled = PiecePointer >= PieceQueue.Length;
        }

        private void PlacePiece_Click(object sender, EventArgs e)
        {
            Instructions.Add(new Instruction(desiredX, desiredR, desiredHold));
            PiecePointer++;
            UpdateButtons();
        }

        private void Undo_Click(object sender, EventArgs e)
        {
            Instructions.RemoveAt(Instructions.Count - 1);
            PiecePointer--;
            UpdateButtons();
        }

        int PiecePointer = 0;

        int[] PieceQueue;
        List<Instruction> Instructions = new List<Instruction>();

        public enum Blocks
        {
            S,
            Z,
            J,
            L,
            T,
            O,
            I,
        } //all the used blocks

        public int[,] VisibleBoard;
        int Blocksize = 20, Blocksize_ = 21, Hold, LinesCleared;
        Bitmap image;
        SolidBrush brush;
        Pen pen;
        Graphics graphics;
        Color BackgroundColor = Color.DimGray;
        void DrawBoard()
        {
            //draw real block
            for (int y = 0; y < 24; y++)
            {
                for (int x = 0; x < VisibleBoard.GetLength(0); x++)
                {
                    bool IsPiece = true;
                    //draw real blocks
                    switch (VisibleBoard[x, y])
                    {
                        case (byte)Blocks.I:
                            brush.Color = Color.FromArgb(0x00, 0x9F, 0xDA);
                            break;
                        case (byte)Blocks.J:
                            brush.Color = Color.FromArgb(0x00, 0x65, 0xBD);
                            break;
                        case (byte)Blocks.L:
                            brush.Color = Color.FromArgb(0xFF, 0x79, 0x00);
                            break;
                        case (byte)Blocks.O:
                            brush.Color = Color.FromArgb(0xFE, 0xCB, 0x00);
                            break;
                        case (byte)Blocks.S:
                            brush.Color = Color.FromArgb(0x69, 0xBE, 0x28);
                            break;
                        case (byte)Blocks.T:
                            brush.Color = Color.FromArgb(0x95, 0x2D, 0x98);
                            break;
                        case (byte)Blocks.Z:
                            brush.Color = Color.FromArgb(0xED, 0x29, 0x39);
                            break;
                        default:
                            IsPiece = false;
                            if (y + LinesCleared >= 40)
                            {
                                brush.Color = Color.FromArgb(138, 69, 69);
                            }
                            else
                            {
                                int temp = (y + LinesCleared) % 4;
                                int color = 50 + (15 - (3 - temp) * 13);
                                brush.Color = Color.FromArgb(color, color, color);
                            }
                            break;
                    }
                    if (IsPiece)
                    {
                        bool current = false;
                        foreach ((int x, int y) i in CurrentPiece)
                        {
                            if (x == i.x && y == i.y)
                            {
                                current = true;
                            }
                        }
                        if (!current)
                        {
                            brush.Color = Color.FromArgb(170, brush.Color.R, brush.Color.G, brush.Color.B);
                        }
                    }
                    graphics.FillRectangle(brush, x * Blocksize_ + 1, (23 - y) * Blocksize_ + 1, Blocksize, Blocksize);

                    //draw ghostblock with transparency
                    //use simulated harddropped piece
                    /*switch (GhostBoard[x, y]) 
                    {
                        case (byte)Blocks.I:
                            brush.Color = Color.FromArgb(0x40, 0x00, 0x9F, 0xDA);
                            break;
                        case (byte)Blocks.J:
                            brush.Color = Color.FromArgb(0x40, 0x00, 0x65, 0xBD);
                            break;
                        case (byte)Blocks.L:
                            brush.Color = Color.FromArgb(0x40, 0xFF, 0x79, 0x00);
                            break;
                        case (byte)Blocks.O:
                            brush.Color = Color.FromArgb(0x40, 0xFE, 0xCB, 0x00);
                            break;
                        case (byte)Blocks.S:
                            brush.Color = Color.FromArgb(0x40, 0x69, 0xBE, 0x28);
                            break;
                        case (byte)Blocks.T:
                            brush.Color = Color.FromArgb(0x40, 0x95, 0x2D, 0x98);
                            break;
                        case (byte)Blocks.Z:
                            brush.Color = Color.FromArgb(0x40, 0xED, 0x29, 0x39);
                            break;
                        default:
                            brush.Color = Color.FromArgb(0x40, BackgroundColor);
                            break;
                    }
                    graphics.FillRectangle(brush, x * Blocksize_ + 1, y * Blocksize_ + 1, Blocksize, Blocksize);*/
                }
            }
            Canvas.Image = image;
        }

        void DrawHoldAndQueue()
        {
            string i = "Hold: " + GetPiece((byte)Hold) + "\nQueue:";
            foreach (int j in PieceQueue)
            {
                i += "\n" + GetPiece((byte)j);
            }
            HoldAndQueue.Text = i;
        }

        char GetPiece(byte i)
        {
            switch (i)
            {
                case (byte)Blocks.I:
                    return 'I';
                case (byte)Blocks.J:
                    return 'J';
                case (byte)Blocks.L:
                    return 'L';
                case (byte)Blocks.O:
                    return 'O';
                case (byte)Blocks.S:
                    return 'S';
                case (byte)Blocks.T:
                    return 'T';
                case (byte)Blocks.Z:
                    return 'Z';
                default:
                    return '-';
            }
        }
    }
}
