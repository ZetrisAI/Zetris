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

        Random r = new Random();

        public (int InitialDelay, int RepeatDelay) DAS = (400, 6); //not used yet

        public enum Blocks
        {
            empty,
            T,
            I,
            S,
            Z,
            O,
            L,
            J,
            garbage
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

        /*public Bitmap PlaceBlock((int x, int y) Pos, byte blockType, bool Real = true, bool Overwrite = false)
        {

            for (int x = 0; x < GhostBoard.GetLength(0); x++)
            {
                for (int y = 0; y < GhostBoard.GetLength(1); y++)
                {
                    if (GhostBoard[x, y] == (byte)Blocks.empty) Board[x, y] = (byte)Blocks.empty;
                    GhostBoard[x, y] = true;
                }
            }

            try
            {
                Pos.x = Pos.x / Blocksize_;
                Pos.y = Pos.y / Blocksize_;
                if (!Overwrite)
                {
                    if (Board[Pos.x, Pos.y] == ((byte)Blocks.empty))
                    {
                        Board[Pos.x, Pos.y] = blockType;
                        GhostBoard[Pos.x, Pos.y] = Real;
                    }
                }
                else
                {
                    Board[Pos.x, Pos.y] = blockType;
                    GhostBoard[Pos.x, Pos.y] = Real;
                }
            }
            catch
            {

            }
            return Draw();
        }*/

        /*public Bitmap PlaceBlocks(List<(int x, int y, byte)> BlockList, bool Real = true, bool Overwrite = false)
        {

            for (int x = 0; x < GhostBoard.GetLength(0); x++)
            {
                for (int y = 0; y < GhostBoard.GetLength(1); y++)
                {
                    if (GhostBoard[x, y] == false) Board[x, y] = (byte)Blocks.empty;
                    GhostBoard[x, y] = true;
                }
            }

            try
            {
                if (!Overwrite)
                {
                    foreach ((int x, int y, byte b) in BlockList)
                    {
                        int X = x / Blocksize_;
                        int Y = y / Blocksize_;
                        if (Board[X, Y] == (byte)Blocks.empty)
                        {
                            Board[X, Y] = b;
                            GhostBoard[X, Y] = Real;
                        }
                    }
                }
                else
                {
                    foreach ((int x, int y, byte b) in BlockList)
                    {
                        int X = x / Blocksize_;
                        int Y = y / Blocksize_;
                        Board[X, Y] = b;
                        GhostBoard[X, Y] = Real;
                    }
                }
            }
            catch
            {

            }
            return Draw();
        }*/

        public byte NextPiece()
        {
            if (BagPiece == 7)
            {
                BagPiece = 0;
                GenerateBag();
            }
            return Bag[BagPiece++];
        }

        void GenerateBag()
        {
            byte[] temp = new byte[7];
            byte newNumber;
            for (int i = 0; i < temp.Length; i++)
            {
                newNumber = (byte)(r.Next(1, 8));
                if (!temp.Contains(newNumber))
                {
                    temp[i] = newNumber;
                }
                else i--;
            }
            Bag = temp;
        }

        public void UpdateBlock((int x, int y) Pos, byte blockType, bool Real, bool Overwrite = true) //updates a single block on the board and then draws it to the image Bitmap, might edit the parameters
        {
            //clears the ghostblock
            GhostBoard[LastGhost.x, LastGhost.y] = (byte)Blocks.empty;
            DrawBlock(LastGhost);

            bool Drawn = false;

            try
            {
                //translates mouse position (origin point top left corner of the board) to the corresponding block on the board
                Pos.x = Pos.x / Blocksize_;
                Pos.y = Pos.y / Blocksize_;

                if (Overwrite) //can ovewrite already existing blocks
                {
                    if (Real) //real block
                    {
                        VisibleBoard[Pos.x, Pos.y] = blockType;
                        for (int i = 0; i < Size.Width; i++) //checks if a line is filled
                        {
                            if (VisibleBoard[i, Pos.y] == (byte)Blocks.empty)
                            {
                                break;
                            }
                            if (i == Size.Width - 1)
                            {
                                ClearLine(Pos.y); //clears the line if it's filled
                                //RedrawBoard(); //redraws the board
                                Drawn = true;
                            }
                        }
                    }
                    else //ghostblock
                    {
                        GhostBoard[Pos.x, Pos.y] = blockType;
                        LastGhost = (Pos.x, Pos.y);
                    }
                    if (!Drawn)
                    {
                        DrawBlock(Pos); //draws the changed block on the bitmap
                    }
                }
                else
                {
                    if (!Real || VisibleBoard[Pos.x, Pos.y] == ((byte)Blocks.empty)) //ghostblocks can be shown on top of real blocks but real blocks cannot be overwritten
                    {
                        if (Real) //real block
                        {
                            VisibleBoard[Pos.x, Pos.y] = blockType;
                            for (int i = 0; i < Size.Width; i++) //checks if a line is filled
                            {
                                if (VisibleBoard[i, Pos.y] == (byte)Blocks.empty)
                                {
                                    break;
                                }
                                if (i == Size.Width - 1)
                                {
                                    ClearLine(Pos.y); //clears the line if it's filled
                                    //RedrawBoard(); //redraws the board
                                    Drawn = true;
                                }
                            }
                        }
                        else //ghostblock
                        {
                            GhostBoard[Pos.x, Pos.y] = blockType;
                            LastGhost = (Pos.x, Pos.y);
                        }
                        if (!Drawn)
                        {
                            DrawBlock(Pos); //draws the changed block on the bitmap
                        }
                    }
                }
            }
            catch
            {

            }
        }

        public void ClearLine(int y, bool Redraw = true) //clears given line and moves the other lines down by one
        {
            for (int h = y; h >= 0; h--) //height
            {
                for (int w = 0; w < Size.Width; w++) //width
                {
                    if (h == 0) //this would be the top row
                    {
                        VisibleBoard[w, h] = (byte)Blocks.empty;
                    }
                    else
                    {
                        VisibleBoard[w, h] = VisibleBoard[w, h - 1]; //copies row on top
                    }
                }
            }
            if (Redraw)
            {

                //RedrawBoard(y); //redraws the board
            }
        }

        public void CheckLineClears() //checks for filled lines and clears them
        {
            bool Updated = false;
            for (int h = 0; h < Size.Height; h++) //height
            {
                for (int i = 0; i < Size.Width; i++) //checks if a line is filled
                {
                    if (VisibleBoard[i, h] == (byte)Blocks.empty)
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
                //RedrawBoard(Size.Height); //redraws the board
            }
        }

        /*public void UpdateTetromino(byte[,] Tetromino, (int x, int y) Pos, byte Piece)
        {
            try
            {
                for (int y = 0; y < 4; y++)
                {
                    for (int x = 0; x < 4; x++)
                    {
                        if (LastPiece.Piece[x, y] == 1)
                        {
                            UpdateTetrominoBlock((LastPiece.x + x, LastPiece.y + y), (byte)Blocks.empty);
                        }
                    }
                }
            }
            catch
            {

            }
            LastPiece = (Pos.x, Pos.y, Tetromino);
            try
            {
                for (int y = 0; y < 4; y++)
                {
                    for (int x = 0; x < 4; x++)
                    {
                        if (Tetromino[x, y] == 1)
                        {
                            UpdateTetrominoBlock((Pos.x + x, Pos.y + y), Piece);
                        }
                    }
                }
                RedrawBoard();
            }
            catch
            {
                
            }
        }*/

        public bool CheckCollision((byte x, byte y)[] Tetromino, (int x, int y) Pos, byte Piece)
        {
            try
            {
                for (int i = 0; i < 4; i++)
                {
                    UpdateTetrominoBlock((LastPiece.BlockPositions[i].x + LastPiece.Position.x, LastPiece.BlockPositions[i].y + LastPiece.Position.y), (byte)Blocks.empty);
                }
            }
            catch
            {

            }
            try
            {
                for (int i = 0; i < 4; i++)
                {
                    if (VisibleBoard[Tetromino[i].x + Pos.x, Tetromino[i].y + Pos.y] != (byte)Blocks.empty)
                    {
                        return false;
                    }
                }
                UpdateTetromino(Tetromino, Pos, Piece);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public void UpdateTetromino((byte x, byte y)[] Tetromino, (int x, int y) Pos, byte Piece)
        {
            /*try
            {
                for (int i = 0; i < 4; i++)
                {
                    UpdateTetrominoBlock((LastPiece.BlockPositions[i].x + LastPiece.Position.x, LastPiece.BlockPositions[i].y + LastPiece.Position.y), (byte)Blocks.empty);
                }
            }
            catch
            {

            }*/
            LastPiece = ((Pos.x, Pos.y), Tetromino);
            try
            {
                for (int i = 0; i < 4; i++)
                {
                    UpdateTetrominoBlock((Tetromino[i].x + Pos.x, Tetromino[i].y + Pos.y), Piece);
                }
                //RedrawBoard();
            }
            catch
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

        public void UpdateTetrominoBlock((int x, int y) Pos, byte blockType) //updates a single block on the board and then draws it to the image Bitmap, might edit the parameters
        {
            //clears the ghostblock
            GhostBoard[LastGhost.x, LastGhost.y] = (byte)Blocks.empty;
            DrawBlock(LastGhost);

            VisibleBoard[Pos.x, Pos.y] = blockType;
        }

        public void UpdateBlocks(List<(int x, int y, byte)> BlockList, bool Real, bool Overwrite = true) //this might get used once we actually draw tetrominoes, not commented yet because currently unused
        {
            //will get updated once it needs to be used
            for (int x = 0; x < GhostBoard.GetLength(0); x++)
            {
                for (int y = 0; y < GhostBoard.GetLength(1); y++)
                {
                    GhostBoard[x, y] = (byte)Blocks.empty;
                }
            }

            try
            {
                if (!Overwrite)
                {
                    foreach ((int x, int y, byte b) in BlockList)
                    {
                        if (Real)
                        {
                            VisibleBoard[x, y] = b;
                        }
                        else
                        {
                            GhostBoard[x, y] = b;
                        }
                        DrawBlock((x, y));
                    }
                }
                else
                {
                    foreach ((int x, int y, byte b) in BlockList)
                    {
                        if (!Real || VisibleBoard[x, y] == ((byte)Blocks.empty))
                        {
                            if (Real)
                            {
                                VisibleBoard[x, y] = b;
                            }
                            else
                            {
                                GhostBoard[x, y] = b;
                            }
                            DrawBlock((x, y));
                        }
                    }
                }
            }
            catch
            {

            }
        }

        public void DrawBlock((int x, int y) Pos)
        {
            //draw real block
            switch (VisibleBoard[Pos.x, Pos.y])
            {
                case (byte)Blocks.empty:
                    brush.Color = BackgroundColor;
                    break;
                case (byte)Blocks.garbage:
                    brush.Color = Color.LightGray;
                    break;
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
            graphics.FillRectangle(brush, Pos.x * Blocksize_ + 1, Pos.y * Blocksize_ + 1, Blocksize, Blocksize);

            //draw ghostblock with transparency
            switch (GhostBoard[Pos.x, Pos.y])
            {
                case (byte)Blocks.empty:
                    brush.Color = Color.FromArgb(0x40, BackgroundColor);
                    break;
                case (byte)Blocks.garbage:
                    brush.Color = Color.FromArgb(0x40, Color.LightGray);
                    break;
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
            graphics.FillRectangle(brush, Pos.x * Blocksize_ + 1, Pos.y * Blocksize_ + 1, Blocksize, Blocksize);
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
                        case (byte)Blocks.empty:
                            brush.Color = BackgroundColor;
                            break;
                        case (byte)Blocks.garbage:
                            brush.Color = Color.LightGray;
                            break;
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
                    switch (GhostBoard[x, y])
                    {
                        case (byte)Blocks.empty:
                            brush.Color = Color.FromArgb(0x40, BackgroundColor);
                            break;
                        case (byte)Blocks.garbage:
                            brush.Color = Color.FromArgb(0x40, Color.LightGray);
                            break;
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
                        case (byte)Blocks.empty:
                            brush.Color = BackgroundColor;
                            break;
                        case (byte)Blocks.garbage:
                            brush.Color = Color.LightGray;
                            break;
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
                    switch (GhostBoard[x, y])
                    {
                        case (byte)Blocks.empty:
                            brush.Color = Color.FromArgb(0x40, BackgroundColor);
                            break;
                        case (byte)Blocks.garbage:
                            brush.Color = Color.FromArgb(0x40, Color.LightGray);
                            break;
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
                    graphics.FillRectangle(brush, x * Blocksize_ + 1, y * Blocksize_ + 1, Blocksize, Blocksize);
                }
            }
        }

        /*public Bitmap Draw()
        {
            brush.Color = Color.Black;
            for (int y = 0; y < Board.GetLength(1); y++)
            {
                for (int x = 0; x < Board.GetLength(0); x++)
                {
                    if (Board[x, y] != (byte)Blocks.empty)
                    {
                        switch (Board[x, y])
                        {
                            case (byte)Blocks.garbage:
                                brush.Color = Color.LightGray;
                                break;
                            case (byte)Blocks.I:
                                brush.Color = Color.DeepSkyBlue;
                                break;
                            case (byte)Blocks.J:
                                brush.Color = Color.Blue;
                                break;
                            case (byte)Blocks.L:
                                brush.Color = Color.Orange;
                                break;
                            case (byte)Blocks.O:
                                brush.Color = Color.Yellow;
                                break;
                            case (byte)Blocks.S:
                                brush.Color = Color.LimeGreen;
                                break;
                            case (byte)Blocks.T:
                                brush.Color = Color.Purple;
                                break;
                            case (byte)Blocks.Z:
                                brush.Color = Color.Red;
                                break;
                        }
                        if (!GhostBoard[x, y]) brush.Color = Color.FromArgb(128, brush.Color);
                        graphics.FillRectangle(brush, x * Blocksize_ + 1, y * Blocksize_ + 1, Blocksize, Blocksize);
                    }
                }
            }
            return image;
        }*/

        #endregion

    }
}
