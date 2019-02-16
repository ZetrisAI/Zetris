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
            byte[,] Block = Tetromino.GetTetromino((byte)current);
            X = (current == (int)Blocks.O ? 4 : 3);
            Y = yPos;
            int i = 0;
            for (int y = 0; y < Block.GetLength(1); y++)
            {
                for (int x = 0; x < Block.GetLength(0); x++)
                {
                    if (Block[x, y] == 1)
                    {
                        board[X + x, Y + y + 13] = current;
                        PiecePosition[i++] = (X + x, Y + y + 13);
                    }
                }
            }

            brush = new SolidBrush(BackgroundColor);
            pen = new Pen(Color.Black);
            image = new Bitmap(11 * Blocksize_ + 1, 24 * Blocksize_ + 1);
            graphics = Graphics.FromImage(image);
            //UpdateButtons();
            //Piece = new Tetromino(PieceQueue[PiecePointer]);

            InitializeComponent();

            Draw();

            TempPiece = Block;

            Piece = new Tetromino((byte)current);
        }

        Tetromino Piece = new Tetromino((byte)Blocks.T);
        Tetris Game = new Tetris();

        void Draw()
        {
            DrawSeparators();
            DrawBoard();
            DrawHoldAndQueue();
            if (!OutOfBounds())
            {
                DrawGhostblocks();
            }
        }

        void DrawSeparators()
        {
            //draws background, separators and outline for the board (in that order)
            brush.Color = BackgroundColor;
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
        }

        (int x, int y)[] PiecePosition = new (int x, int y)[4];
        int X = 4, Y;

        public int desiredX = 4, desiredR = 0;
        public bool desiredHold = false;

        private void valueX_ValueChanged(object sender, EventArgs e) {
            if (MovePiece((int)valueX.Value - desiredX))
            {
                desiredX = Convert.ToInt32(valueX.Value);
                Draw();
            }
            else
            {
                valueX.Value = desiredX;
            }
        }

        bool RotationSuccessful;

        private void valueR_ValueChanged(object sender, EventArgs e) {
            /*if (RotatePiece((int)valueR.Value - desiredR))
            {
                desiredR = (Convert.ToInt32(valueR.Value) + 4) % 4;
                valueR.Value = desiredR;
                CurrentPiece = TempPiece;
            }
            else
            {
                valueR.Value = desiredR;
            }
            Draw();*/
            RotatePiece(valueR.Value - desiredR == 1);

            if (RotationSuccessful)
            {
                desiredR = (Convert.ToInt32(valueR.Value) + 4) % 4;
                valueR.Value = desiredR;
            }
            else
            {
                valueR.Value = desiredR;
            }
            Draw();
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
            int time = Environment.TickCount;
            /*Instructions.Add(new Instruction(desiredX, desiredR, desiredHold));
            PiecePointer++;
            UpdateButtons();*/
            Draw();
            MessageBox.Show((Environment.TickCount - time) + "ms");
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

        bool OutOfBounds()
        {
            foreach ((int x, int y) pos in PiecePosition)
            {
                if (pos.x >= VisibleBoard.GetLength(0))
                {
                    return true;
                }
                if (pos.x < 0)
                {
                    return true;
                }
                if (pos.y >= VisibleBoard.GetLength(1))
                {
                    return true;
                }
                if (pos.y < 0)
                {
                    return true;
                }
            }
            return false;
        }

        bool CheckCollision(int MovedX = 0)
        {
            foreach ((int x, int y) pos in PiecePosition)
            {
                if (pos.x + MovedX >= VisibleBoard.GetLength(0))
                {
                    return false;
                }
                if (pos.x + MovedX < 0)
                {
                    return false;
                }
            }

            //remove current piece from board
            int current = VisibleBoard[PiecePosition[0].x, PiecePosition[0].y];
            int[] y =
            {
                PiecePosition[0].y,
                PiecePosition[1].y,
                PiecePosition[2].y,
                PiecePosition[3].y
            };
            for (int i = 0; i < PiecePosition.Length; i++)
            {
                VisibleBoard[PiecePosition[i].x, PiecePosition[i].y] = 255;
            }

            //check for collisions
            bool collision = false;
            foreach ((int x, int y) pos in PiecePosition)
            {
                if (VisibleBoard[pos.x + MovedX, pos.y] != 255)
                {
                    collision = true;
                    break;
                }
            }

            //readd current piece
            for (int i = 0; i < PiecePosition.Length; i++)
            {
                VisibleBoard[PiecePosition[i].x, PiecePosition[i].y] = current;
            }

            return !collision;
        }

        bool MovePiece(int MovedX)
        {
            //remove current piece from board
            int current = VisibleBoard[PiecePosition[0].x, PiecePosition[0].y];
            int[] y =
            {
                PiecePosition[0].y,
                PiecePosition[1].y,
                PiecePosition[2].y,
                PiecePosition[3].y
            };
            for (int i = 0; i < PiecePosition.Length; i++)
            {
                VisibleBoard[PiecePosition[i].x, PiecePosition[i].y] = 255;
            }

            //readd current piece
            for (int i = 0; i < PiecePosition.Length; i++)
            {
                VisibleBoard[PiecePosition[i].x, PiecePosition[i].y] = current;
            }

            if (CheckCollision(MovedX))
            {
                foreach ((int x, int y) pos in PiecePosition)
                {
                    VisibleBoard[pos.x, pos.y] = 255;
                }
                foreach ((int x, int y) pos in PiecePosition)
                {
                    VisibleBoard[pos.x + MovedX, pos.y] = current;
                }
                //move position
                for (int i = 0; i < PiecePosition.Length; i++)
                {
                    PiecePosition[i].x += MovedX;
                }
                return true;
            }
            return false;
        }

        void RotatePiece(bool CW)
        {
            (byte,byte)[] prev = Piece.FromAnchorPoint();
            if (CW) Piece.RotateClockwise();
            else Piece.RotateCounterClockwise();
            (byte, byte)[] curr = Piece.FromAnchorPoint();
            bool temp = false;
            while (!temp)
            {
                if (Piece.PerformSRS())
                {
                    /*if (Game.CheckCollision(Piece.FromAnchorPoint(), Piece.TempPos, Piece.Piece))
                    {
                        Piece.UpdatePiece(true);
                        temp = true;
                    }*/
                    //remove piece first
                    //change position by srsresult + prev - curr? something something
                    if (/*check collision*/true)
                    {
                        Piece.UpdatePiece(true);
                        temp = true;
                        RotationSuccessful = true;
                    }
                }
                else
                {
                    Piece.UpdatePiece(false);
                    temp = true;
                    RotationSuccessful = false;
                }
            }
            //readd piece
            //RotationSuccessful = Game.CheckCollision(Piece.FromAnchorPoint(), Piece.TempPos, Piece.Piece); //?
        }
        
        bool PreviousName_RotatePiece(int Rotation) //1 = CW, -1 = CCW
        {
            if (CheckCollision())
            {
                int current = VisibleBoard[PiecePosition[0].x, PiecePosition[0].y];
                Previousname_Piece = (byte)current;
                CurrentPiece = GetTetromino((byte)current);
                //currentRotation = (byte)((currentRotation + Rotation) % 4);
                (int x, int y)[] CurrentPosition = PiecePosition;
                bool workingRotation = false;
                int i = 0;

                //remove current piece from board
                for (int j = 0; j < PiecePosition.Length; j++)
                {
                    VisibleBoard[PiecePosition[j].x, PiecePosition[j].y] = 255;
                }

                (int x, int y) highest = (0, 0);
                for (int j = 0; j < PiecePosition.Length; j++)
                {
                    if (PiecePosition[j].x > highest.x)
                    {
                        highest.x = PiecePosition[j].x;
                    }
                    if (PiecePosition[j].y > highest.y)
                    {
                        highest.y = PiecePosition[j].y;
                    }
                }

                if (Rotation == 1)
                {
                    RotateClockwise();
                }
                else
                {
                    RotateCounterClockwise();
                }
                while (PerformSRS() && !workingRotation)
                {
                    foreach ((int x, int y) pos in FromAnchorPoint())
                    {
                        PiecePosition[i] = (highest.x - pos.x + SRSResult.x, highest.y - pos.y + SRSResult.y);
                        i++;
                    }
                    if (CheckCollision())
                    {
                        workingRotation = true;
                        i = 0;
                        foreach ((int x, int y) pos in FromAnchorPoint())
                        {
                            i++;
                            VisibleBoard[highest.x - pos.x + SRSResult.x, highest.y - pos.y + SRSResult.y] = current;
                        }
                    }
                    i = 0;
                }
                previousRotation = currentRotation;
                return workingRotation;
            }
            else
            {
                return false;
            }
        }

        public (byte, byte)[] FromAnchorPoint()
        {
            (byte, byte)[] temp = new(byte, byte)[4];
            int i = 0;
            for (byte y = 0; y < 4; y++)
            {
                for (byte x = 0; x < 4; x++)
                {
                    if (TempPiece[x, y] == 1) temp[i++] = (x, y);
                }
            }
            return temp;
        }

        bool CheckCollisionGhost(int MovedY)
        {
            //untested
            foreach ((int x, int y) pos in PiecePosition)
            {
                if (pos.y + MovedY >= VisibleBoard.GetLength(1)) return false;
                if (pos.y + MovedY < 0) return false;
                if (VisibleBoard[pos.x, pos.y + MovedY] != 255) return false;
            }
            return true;
        }

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
                            foreach ((int x, int y) i in PiecePosition)
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
                                foreach ((int x, int y) i in PiecePosition)
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

        void DrawGhostblocks()
        {
            /*bool oob = false;
            foreach ((int x, int y) pos in CurrentPiece)
            {
                if (pos.x + MovedX >= VisibleBoard.GetLength(0))
                {
                    oob = true;
                    break;
                }
                if (pos.x + MovedX < 0)
                {
                    oob = true;
                    break;
                }
            }

            if (!oob)
            {
                //stuff
            }*/

            //remove current piece from board
            int current = VisibleBoard[PiecePosition[0].x, PiecePosition[0].y];
            int[] y =
            {
                PiecePosition[0].y,
                PiecePosition[1].y,
                PiecePosition[2].y,
                PiecePosition[3].y
            };
            for (int i = 0; i < PiecePosition.Length; i++)
            {
                VisibleBoard[PiecePosition[i].x, PiecePosition[i].y] = 255;
            }

            while (CheckCollisionGhost(-1))
            {
                for (int i = 0; i < PiecePosition.Length; i++)
                {
                    PiecePosition[i].y--;
                }
            }
            for (int i = 0; i < PiecePosition.Length; i++)
            {
                DrawGhostBlock(PiecePosition[i].x, PiecePosition[i].y, current);
            }
            for (int i = 0; i < PiecePosition.Length; i++)
            {
                PiecePosition[i].y = y[i];
            }

            //readd current piece
            for (int i = 0; i < PiecePosition.Length; i++)
            {
                VisibleBoard[PiecePosition[i].x, PiecePosition[i].y] = current;
            }
        }

        void DrawGhostBlock(int x, int y, int current, bool DrawSimple = false)
        {
            if (DrawSimple)
            {
                //draw ghostblock with transparency
                //use simulated harddropped piece
                switch (VisibleBoard[PiecePosition[0].x, PiecePosition[0].y])
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
                graphics.FillRectangle(brush, x * Blocksize_ + 1, y * Blocksize_ + 1, Blocksize, Blocksize);
            }
            else
            {
                Bitmap piece = GetBitmap((byte)current, PieceType.Ghost);
                graphics.DrawImage(piece, new Rectangle(x * Blocksize_ + 1, (23 - y) * Blocksize_ + 1, Blocksize, Blocksize));
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

        byte[,] GetTetromino(byte Tetromino)
        {
            byte[,] Piece;
            switch (Tetromino)
            {
                case (byte)Blocks.I:
                    Piece = new byte[,] {
                        { 0, 0, 0, 0 },
                        { 1, 1, 1, 1 },
                        { 0, 0, 0, 0 },
                        { 0, 0, 0, 0 }
                    };
                    break;
                case (byte)Blocks.J:
                    Piece = new byte[,] {
                        { 1, 0, 0, 0 },
                        { 1, 1, 1, 0 },
                        { 0, 0, 0, 0 },
                        { 0, 0, 0, 0 }
                    };
                    break;
                case (byte)Blocks.L:
                    Piece = new byte[,] {
                        { 0, 0, 1, 0 },
                        { 1, 1, 1, 0 },
                        { 0, 0, 0, 0 },
                        { 0, 0, 0, 0 }
                    };
                    break;
                case (byte)Blocks.O:
                    Piece = new byte[,] {
                        { 1, 1, 0, 0 },
                        { 1, 1, 0, 0 },
                        { 0, 0, 0, 0 },
                        { 0, 0, 0, 0 }
                    };
                    break;
                case (byte)Blocks.S:
                    Piece = new byte[,] {
                        { 0, 1, 1, 0 },
                        { 1, 1, 0, 0 },
                        { 0, 0, 0, 0 },
                        { 0, 0, 0, 0 }
                    };
                    break;
                case (byte)Blocks.T:
                    Piece = new byte[,] {
                        { 0, 1, 0, 0 },
                        { 1, 1, 1, 0 },
                        { 0, 0, 0, 0 },
                        { 0, 0, 0, 0 }
                    };
                    break;
                case (byte)Blocks.Z:
                    Piece = new byte[,] {
                        { 1, 1, 0, 0 },
                        { 0, 1, 1, 0 },
                        { 0, 0, 0, 0 },
                        { 0, 0, 0, 0 }
                    };
                    break;
                default:
                    Piece = new byte[,] {
                        { 0, 0, 0, 0 },
                        { 0, 0, 0, 0 },
                        { 0, 0, 0, 0 },
                        { 0, 0, 0, 0 }
                    };
                    break;
            }
            byte[,] temp = new byte[4, 4];
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    temp[x, y] = Piece[3 - y, x];
                }
            }
            Piece = temp;
            return Piece;
        }

        public enum Rotation
        {
            north,
            east,
            south,
            west
        } //rotations

        public byte Previousname_Piece, previousRotation = (byte)Rotation.north, currentRotation = (byte)Rotation.north, SRSIteration = 0;

        bool[] keys = new bool[4];

        private void Dialog_KeyDown(object sender, KeyEventArgs e) { // todo: keybinds configurable?
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

        public (int x, int y) Pos, TempPos;

        (short x, short y)[,] SRSJLSTZTable = new(short, short)[,] {
            { ( 0, 0), (-1, 0), (-1, 1), ( 0,-2), (-1,-2) },
            { ( 0, 0), ( 1, 0), ( 1,-1), ( 0, 2), ( 1, 2) },
            { ( 0, 0), ( 1, 0), ( 1,-1), ( 0, 2), ( 1, 2) },
            { ( 0, 0), (-1, 0), (-1, 1), ( 0,-2), (-1,-2) },
            { ( 0, 0), ( 1, 0), ( 1, 1), ( 0,-2), ( 1,-2) },
            { ( 0, 0), (-1, 0), (-1,-1), ( 0, 2), (-1, 2) },
            { ( 0, 0), (-1, 0), (-1,-1), ( 0, 2), (-1, 2) },
            { ( 0, 0), ( 1, 0), ( 1, 1), ( 0,-2), ( 1,-2) }
        };

        (short x, short y)[,] SRSITable = new(short, short)[,] {
            { ( 0, 0), (-2, 0), ( 1, 0), (-2,-1), ( 1, 2) },
            { ( 0, 0), ( 2, 0), (-1, 0), ( 2, 1), (-1,-2) },
            { ( 0, 0), (-1, 0), ( 2, 0), (-1, 2), ( 2,-1) },
            { ( 0, 0), ( 1, 0), (-2, 0), ( 1,-2), (-2, 1) },
            { ( 0, 0), ( 2, 0), (-1, 0), ( 2, 1), (-1,-2) },
            { ( 0, 0), (-2, 0), ( 1, 0), (-2,-1), ( 1, 2) },
            { ( 0, 0), ( 1, 0), (-2, 0), ( 1,-2), (-2, 1) },
            { ( 0, 0), (-1, 0), ( 2, 0), (-1, 2), ( 2,-1) }
        };

        public bool PerformSRS()
        {
            if (SRSIteration > 4)
            {
                SRSIteration = 0;
                currentRotation = previousRotation;
                return false;
            }

            byte row = 0;
            if (previousRotation == 0)
            {
                if (currentRotation == 1)
                {
                    row = 0;
                }
                else if (currentRotation == 3)
                {
                    row = 7;
                }
            }
            else if (previousRotation == 1)
            {
                if (currentRotation == 0)
                {
                    row = 1;
                }
                else if (currentRotation == 2)
                {
                    row = 2;
                }
            }
            else if (previousRotation == 2)
            {
                if (currentRotation == 1)
                {
                    row = 3;
                }
                else if (currentRotation == 3)
                {
                    row = 4;
                }
            }
            else if (previousRotation == 3)
            {
                if (currentRotation == 2)
                {
                    row = 5;
                }
                else if (currentRotation == 0)
                {
                    row = 6;
                }
            }
            TempPos = Pos;
            switch (Previousname_Piece)
            {
                case (byte)Blocks.I:
                    SRSResult.x = SRSITable[row, SRSIteration].x;
                    SRSResult.y = SRSITable[row, SRSIteration].y;
                    break;
                case (byte)Blocks.O:
                    SRSIteration = 0;
                    previousRotation = currentRotation;
                    return false;
                default:
                    SRSResult.x = SRSITable[row, SRSIteration].x;
                    SRSResult.y = SRSITable[row, SRSIteration].y;
                    break;
            }
            SRSIteration++;
            return true;
        }

        (int x, int y) SRSResult;

        public byte[,] CurrentPiece = new byte[4, 4], TempPiece = new byte[4, 4];

        public void RotateClockwise()
        {
            if (Previousname_Piece == (byte)Blocks.I)
            {
                byte[,] temp = new byte[4, 4];
                for (int y = 0; y < 4; y++)
                {
                    for (int x = 0; x < 4; x++)
                    {
                        temp[x, y] = CurrentPiece[y, 3 - x];
                    }
                }
                TempPiece = temp;
            }
            else if (Previousname_Piece != (byte)Blocks.O)
            {
                byte[,] temp = new byte[4, 4];
                for (int y = 0; y < 3; y++)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        temp[x, y] = CurrentPiece[y, 2 - x];
                    }
                }
                TempPiece = temp;
            }

            //O rotation has no effect

            currentRotation = (byte)((currentRotation + 1) % 4);
        }

        public void RotateCounterClockwise()
        {
            if (Previousname_Piece == (byte)Blocks.I)
            {
                byte[,] temp = new byte[4, 4];
                for (int y = 0; y < 4; y++)
                {
                    for (int x = 0; x < 4; x++)
                    {
                        temp[x, y] = CurrentPiece[3 - y, x];
                    }
                }
                TempPiece = temp;
            }
            else if (Previousname_Piece != (byte)Blocks.O)
            {
                byte[,] temp = new byte[4, 4];
                for (int y = 0; y < 3; y++)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        temp[x, y] = CurrentPiece[2 - y, x];
                    }
                }
                TempPiece = temp;
            }

            //O rotation has no effect

            currentRotation = (byte)((currentRotation + 3) % 4);
        }
    }
}
