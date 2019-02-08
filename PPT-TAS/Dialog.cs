using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace PPT_TAS {
    public partial class Dialog : Form {
        public Dialog(int[,] board, int current, int hold, int[] queue, int cleared) {
            


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
            UpdateButtons();
            Piece = new Tetromino(PieceQueue[PiecePointer]);

            InitializeComponent();
        }

        Tetromino Piece;

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
            PlacePiece.Enabled = PiecePointer >= PieceQueue.Count;
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

        List<byte> PieceQueue = new List<byte>();
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

        public byte[,] VisibleBoard = new byte[10, 20];
        int Blocksize = 20, Blocksize_ = 21;
        Bitmap image;
        SolidBrush brush;
        Pen pen;
        Graphics graphics;
        Color BackgroundColor = Color.DimGray;
        void DrawBoard()
        {
            //draw real block
            for (int y = 0; y < Size.Height; y++)
            {
                for (int x = 0; x < Size.Width; x++)
                {
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
                    }
                    graphics.FillRectangle(brush, x * Blocksize_ + 1, y * Blocksize_ + 1, Blocksize, Blocksize);

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
                    }
                    graphics.FillRectangle(brush, x * Blocksize_ + 1, y * Blocksize_ + 1, Blocksize, Blocksize);*/
                }
            }
        }
    }
}
