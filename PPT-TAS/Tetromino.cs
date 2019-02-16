using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PPT_TAS
{
    class Tetromino
    {
        public byte Piece, previousRotation = (byte)Rotation.north, currentRotation = (byte)Rotation.north, SRSIteration = 0;
        public (int x, int y) Pos, TempPos;

        public byte[,] CurrentPiece = new byte[4, 4], TempPiece = new byte[4, 4];

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

        public Tetromino()
        {

        }

        public Tetromino(byte Piece)
        {
            this.Piece = Piece;
            TempPos = (4, 0);
            Pos = (4, 0);
            SetBlock(Piece);
        }

        public enum Blocks
        {
            S,
            Z,
            J,
            L,
            T,
            O,
            I
        } //all the used blocks

        public enum Rotation
        {
            north,
            east,
            south,
            west
        } //rotations

        public (short, short) SRSResult;

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
            switch (Piece)
            {
                case (byte)Blocks.I:
                    TempPos.x += SRSITable[row, SRSIteration].x;
                    TempPos.y -= SRSITable[row, SRSIteration].y;
                    SRSResult = SRSITable[row, SRSIteration];
                    break;
                case (byte)Blocks.O:
                    SRSIteration = 0;
                    previousRotation = currentRotation;
                    SRSResult = (0, 0);
                    return false;
                default:
                    TempPos.x += SRSJLSTZTable[row, SRSIteration].x;
                    TempPos.y -= SRSJLSTZTable[row, SRSIteration].y;
                    SRSResult = SRSJLSTZTable[row, SRSIteration];
                    break;
            }
            SRSIteration++;
            return true;
        }

        public byte[,] SetBlock(byte Tetromino)
        {
            switch (Tetromino)
            {
                case (byte)Blocks.I:
                    Piece = (byte)Blocks.I;
                    CurrentPiece = new byte[,] {
                        { 0, 0, 0, 0 },
                        { 1, 1, 1, 1 },
                        { 0, 0, 0, 0 },
                        { 0, 0, 0, 0 }
                    };
                    break;
                case (byte)Blocks.J:
                    Piece = (byte)Blocks.J;
                    CurrentPiece = new byte[,] {
                        { 1, 0, 0, 0 },
                        { 1, 1, 1, 0 },
                        { 0, 0, 0, 0 },
                        { 0, 0, 0, 0 }
                    };
                    break;
                case (byte)Blocks.L:
                    Piece = (byte)Blocks.L;
                    CurrentPiece = new byte[,] {
                        { 0, 0, 1, 0 },
                        { 1, 1, 1, 0 },
                        { 0, 0, 0, 0 },
                        { 0, 0, 0, 0 }
                    };
                    break;
                case (byte)Blocks.O:
                    Piece = (byte)Blocks.O;
                    CurrentPiece = new byte[,] {
                        { 1, 1, 0, 0 },
                        { 1, 1, 0, 0 },
                        { 0, 0, 0, 0 },
                        { 0, 0, 0, 0 }
                    };
                    break;
                case (byte)Blocks.S:
                    Piece = (byte)Blocks.S;
                    CurrentPiece = new byte[,] {
                        { 0, 1, 1, 0 },
                        { 1, 1, 0, 0 },
                        { 0, 0, 0, 0 },
                        { 0, 0, 0, 0 }
                    };
                    break;
                case (byte)Blocks.T:
                    Piece = (byte)Blocks.T;
                    CurrentPiece = new byte[,] {
                        { 0, 1, 0, 0 },
                        { 1, 1, 1, 0 },
                        { 0, 0, 0, 0 },
                        { 0, 0, 0, 0 }
                    };
                    break;
                case (byte)Blocks.Z:
                    Piece = (byte)Blocks.Z;
                    CurrentPiece = new byte[,] {
                        { 1, 1, 0, 0 },
                        { 0, 1, 1, 0 },
                        { 0, 0, 0, 0 },
                        { 0, 0, 0, 0 }
                    };
                    break;
                default:
                    Piece = 255;
                    CurrentPiece = new byte[,] {
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
                    temp[x, y] = CurrentPiece[y, x];
                }
            }
            TempPiece = temp;
            CurrentPiece = temp;
            currentRotation = 0;
            previousRotation = 0;
            return TempPiece;
        }

        static public byte[,] GetTetromino(byte Tetromino)
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

        public void RotateClockwise()
        {
            if (Piece == (byte)Blocks.I)
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
            else if (Piece != (byte)Blocks.O)
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
            if (Piece == (byte)Blocks.I)
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
            else if (Piece != (byte)Blocks.O)
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

        public void UpdatePiece(bool valid)
        {
            if (valid)
            {
                CurrentPiece = TempPiece;
                previousRotation = currentRotation;
                Pos = TempPos;
            }
            else
            {
                TempPiece = CurrentPiece;
                currentRotation = previousRotation;
                TempPos = Pos;
            }
            SRSIteration = 0;
        }
    }
}
