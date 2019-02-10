///
/// todo:
/// add collision detection
/// add ghost blocks
/// make piece controllable
///
/// extra stuff:
/// scrollable piece queue
///

using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace PPT_TAS {
    public partial class Dialog : Form {
        public Dialog(int[,] board, int current, int yPos, int hold, int[] queue, int cleared, int bagIndex)
        {
            BagIndex = bagIndex;
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
            image = new Bitmap(11 * Blocksize_ + 1, 24 * Blocksize_ + 1);
            graphics = Graphics.FromImage(image);
            //UpdateButtons();
            //Piece = new Tetromino(PieceQueue[PiecePointer]);

            //draws background, separators and outline for the board (in that order)
            graphics.FillRectangle(brush, 0, 0, image.Width, image.Height);
            for (int x = 0; x * Blocksize_ < image.Width - Blocksize_ - 2; x++)
            {
                graphics.DrawLine(pen, x * Blocksize_, 0, x * Blocksize_, image.Height);
            }
            for (int y = 0; y < image.Height; y++)
            {
                graphics.DrawLine(pen, 0, y * Blocksize_, 210, y * Blocksize_);
            }
            graphics.DrawRectangle(pen, 0, 0, image.Width - 22, image.Height - 1);

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

        bool DrawSimple = false;
        void DrawBoard()
        {
            if (DrawSimple)
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
            }
            else
            {
                for (int y = 0; y < 24; y++)
                {
                    for (int x = 0; x < VisibleBoard.GetLength(0); x++)
                    {
                        bool IsPiece = true;
                        if ((byte)VisibleBoard[x, y] == 255)
                        {
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
                            graphics.FillRectangle(brush, x * Blocksize_ + 1, (23 - y) * Blocksize_ + 1, Blocksize, Blocksize);
                        }
                        else
                        {
                            Bitmap piece = GetBitmap((byte)VisibleBoard[x, y], PieceType.Normal);
                            graphics.DrawImage(piece, new Rectangle(x * Blocksize_ + 1, (23 - y) * Blocksize_ + 1, Blocksize, Blocksize));
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
                                    brush.Color = Color.FromArgb(85, 0, 0, 0);
                                    graphics.FillRectangle(brush, x * Blocksize_ + 1, (23 - y) * Blocksize_ + 1, Blocksize, Blocksize);
                                }
                            }
                        }
                    }
                }
            }
            Canvas.Image = image;
        }

        int BagIndex;

        void DrawHoldAndQueue()
        {
            int temp = BagIndex;
            HeldPiece.Image = GetBitmap((byte)Hold, PieceType.Mini);
            pen.Color = Color.Black;
            int Y = 2;
            foreach (int j in PieceQueue)
            {
                if ((temp++ % 7) == 5)
                {
                    Y += 8;
                    graphics.DrawLine(pen, 214, Y - 5, 228, Y - 5);
                }
                graphics.DrawImage(GetBitmap(((byte)j), PieceType.Mini), 212, Y);
                Y += 11;
                if (Y > image.Height) break;
            }
        }

        enum PieceType
        {
            Mini,
            Normal,
            Ghost
        }

        Bitmap GetBitmap(byte i, PieceType p)
        {
            switch (p)
            {
                case PieceType.Mini:
                    switch (i)
                    {
                        case (byte)Blocks.I:
                            return Properties.Resources.I_Mini;
                        case (byte)Blocks.J:
                            return Properties.Resources.J_Mini;
                        case (byte)Blocks.L:
                            return Properties.Resources.L_Mini;
                        case (byte)Blocks.O:
                            return Properties.Resources.O_Mini;
                        case (byte)Blocks.S:
                            return Properties.Resources.S_Mini;
                        case (byte)Blocks.T:
                            return Properties.Resources.T_Mini;
                        case (byte)Blocks.Z:
                            return Properties.Resources.Z_Mini;
                        default: return new Bitmap(1, 1);
                    }
                case PieceType.Normal:
                    switch (i)
                    {
                        case (byte)Blocks.I:
                            return Properties.Resources.I;
                        case (byte)Blocks.J:
                            return Properties.Resources.J;
                        case (byte)Blocks.L:
                            return Properties.Resources.L;
                        case (byte)Blocks.O:
                            return Properties.Resources.O;
                        case (byte)Blocks.S:
                            return Properties.Resources.S;
                        case (byte)Blocks.T:
                            return Properties.Resources.T;
                        case (byte)Blocks.Z:
                            return Properties.Resources.Z;
                        default: return new Bitmap(1, 1);
                    }
                case PieceType.Ghost:
                    switch (i)
                    {
                        case (byte)Blocks.I:
                            return Properties.Resources.I_Ghost;
                        case (byte)Blocks.J:
                            return Properties.Resources.J_Ghost;
                        case (byte)Blocks.L:
                            return Properties.Resources.L_Ghost;
                        case (byte)Blocks.O:
                            return Properties.Resources.O_Ghost;
                        case (byte)Blocks.S:
                            return Properties.Resources.S_Ghost;
                        case (byte)Blocks.T:
                            return Properties.Resources.T_Ghost;
                        case (byte)Blocks.Z:
                            return Properties.Resources.Z_Ghost;
                        default: return new Bitmap(1, 1);
                    }
            }
            return Properties.Resources.Garbage_Puyo;
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
