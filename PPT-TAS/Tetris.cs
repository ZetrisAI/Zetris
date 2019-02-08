using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PPT_TAS
{
    class Tetris
    {
        
        #region variable declarations

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

        public byte[,] VisibleBoard, Fullboard, GhostBoard; //Values in board determine the block type, one board for regular blocks, one for ghostblocks

        (int x, int y) LastGhost; //keeps track of last ghostblock

        public (int Width, int Height) Size; //determines board size

        public int Blocksize, Blocksize_; //blocksize and blocksize + 1 (because I needed it surprisingly often)

        public byte HoldSlot;

        ((int x, int y) Position, (byte x, byte y)[] BlockPositions) LastPiece = ((-1, -1), new(byte, byte)[4]);

        byte[] Bag = new byte[7];
        int BagPiece = 7;

        //needed for drawing
        public Bitmap image;
        Graphics graphics;
        SolidBrush brush;
        Pen pen;
        Color BackgroundColor = Color.DimGray;

        #endregion

        public Tetris(int Width = 10, int Height = 20, int Blocksize = 20) //constructor
        {
            //initializing custom board settings and boards
            Size = (Width, Height);
            VisibleBoard = new byte[Size.Width, Size.Height];
            GhostBoard = new byte[Size.Width, Size.Height];
            Fullboard = new byte[Size.Width, Size.Height * 2];
            this.Blocksize = Blocksize;
            Blocksize_ = Blocksize + 1;

            //initializing stuff needed for drawing the board
            image = new Bitmap(Size.Width * Blocksize_ + 1, Size.Height * Blocksize_ + 1); //additional space for the lines inbetween and the border lines
            graphics = Graphics.FromImage(image);
            pen = new Pen(Color.Black);
            brush = new SolidBrush(BackgroundColor);

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
        }

        #region functions
        
        public void ClearLine(int y, bool Redraw = true) //clears given line and moves the other lines down by one
        {
            for (int h = y; h >= 0; h--) //height
            {
                for (int w = 0; w < Size.Width; w++) //width
                {
                    if (h == 0) //this would be the top row
                    {
                        VisibleBoard[w, h] = 255;
                    }
                    else
                    {
                        VisibleBoard[w, h] = VisibleBoard[w, h - 1]; //copies row on top
                    }
                }
            }
            if (Redraw)
            {
            }
        }

        public void CheckLineClears() //checks for filled lines and clears them
        {
            bool Updated = false;
            for (int h = 0; h < Size.Height; h++) //height
            {
                for (int i = 0; i < Size.Width; i++) //checks if a line is filled
                {
                    if (VisibleBoard[i, h] == 255)
                    {
                        break;
                    }
                    if (i == Size.Width - 1)
                    {
                        ClearLine(h); //clears the line if it's filled
                        Updated = true;
                    }
                }
            }
            if (Updated)
            {
            }
        }

        public void Harddrop((byte x, byte y)[] Tetromino, (int x, int y) Pos, byte Piece)
        {
            try
            {
                for (int i = 0; i < 4; i++)
                {
                    try
                    {
                        VisibleBoard[Pos.x + Tetromino[i].x, Pos.y + Tetromino[i].y] = 0;
                    }
                    catch
                    {

                    }
                }
                bool possible = true;
                int count = 0;
                for (; possible; count++)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        try
                        {
                            if (VisibleBoard[Pos.x + Tetromino[i].x, Pos.y + Tetromino[i].y + count] != 0) possible = false;
                        }
                        catch
                        {
                            possible = false;
                        }
                    }
                }
                if (count != 0)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        VisibleBoard[Pos.x + Tetromino[i].x, Pos.y + Tetromino[i].y + count - 2] = Piece;
                    }
                    LastPiece = ((-1, -1), new(byte, byte)[4]);
                    CheckLineClears();
                }
            }
            catch
            {

            }
        }

        public void RedrawBoard()
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
                }
            }
        }

        public void RedrawBoard(int Y) //redraws the board from a vertical line upwards
        {
            for (int y = Y; y >= 0; y--)
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
                }
            }
        }

        #endregion

    }
}
