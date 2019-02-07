using System;

namespace PPT_TAS {
    class GameHelper {
        public static bool OutsideMenu(ProcessMemory Game) => Game.ReadInt32(new IntPtr(
            0x140573A78
        )) == 0x0;

        public static int CurrentMode(ProcessMemory Game) => Game.ReadByte(new IntPtr(
            0x140573854
        ));

        public static int boardAddress(ProcessMemory Game, int index) => Game.ReadInt32(new IntPtr(
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        0x140461B20
                    )) + 0x378
                )) + 0xA8
            )) + 0x3C0
        )) + 0x50;
        

        public static int piecesAddress(ProcessMemory Game, int index) => Game.ReadInt32(new IntPtr(
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    0x140461B20
                )) + 0x378
            )) + 0xB8
        )) + 0x15C;

        public static int getCurrentPiece(ProcessMemory Game, int index) => Game.ReadByte(new IntPtr(
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            0x140461B20
                        )) + 0x378
                    )) + 0x40
                )) + 0x140
            )) + 0x110
        ));

        public static int getPiecePositionX(ProcessMemory Game, int index) => Game.ReadByte(new IntPtr(
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        0x140461B20
                    )) + 0x378
                )) + 0x40
            )) + 0x100
        ));

        public static int getPiecePositionY(ProcessMemory Game, int index) => Game.ReadByte(new IntPtr(
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        0x140461B20
                    )) + 0x378
                )) + 0x40
            )) + 0x101
        ));

        public static int getPieceRotation(ProcessMemory Game, int index) => Game.ReadByte(new IntPtr(
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                0x140460C08
                            )) + 0x18
                        )) + 0x268
                    )) + 0x38
                )) + 0x3C8
            )) + 0x18
        ));

        public static int getPieceDropped(ProcessMemory Game, int index) => Game.ReadByte(new IntPtr(
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                0x140460C08
                            )) + 0x18
                        )) + 0x268
                    )) + 0x38
                )) + 0x3C8
            )) + 0x1C
        ));

        public static int getFrameCount(ProcessMemory Game) => Game.ReadInt32(new IntPtr(
            Game.ReadInt32(new IntPtr(
                0x140461B20
            )) + 0x424
        ));

        public static int getBigFrameCount(ProcessMemory Game) {
            int addr = Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            0x140598A20
                        )) + 0x138
                    )) + 0x18
                )) + 0x100
            )) + 0x58;

            int x = Game.ReadInt32(new IntPtr(
                addr
            ));

            if (x == 8) {
                return Game.ReadInt32(new IntPtr(
                    addr + 0x8
                ));
            }

            return x;
        }

        public static int getMenuFrameCount(ProcessMemory Game) => Game.ReadInt32(new IntPtr(
            0x140461B7C
        ));

        public static bool IsTetChallenge(ProcessMemory Game) {
            int a = (Game.ReadByte(new IntPtr(
                0x140451C50
            )) & 0b11101111) - 2;
            
            return 13 <= a && a <= 15;
        }
    }
}
