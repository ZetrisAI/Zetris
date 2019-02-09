using System;
using System.Collections.Generic;
using System.Linq;

namespace PPT_TAS {
    class GameHelper {
        public static bool OutsideMenu(ProcessMemory Game) => Game.ReadInt32(new IntPtr(
            0x140573A78
        )) == 0x0;

        public static int CurrentMode(ProcessMemory Game) => Game.ReadByte(new IntPtr(
            0x140573854
        ));

        public static int boardAddress(ProcessMemory Game) => Game.ReadInt32(new IntPtr(
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        0x140461B20
                    )) + 0x378
                )) + 0xA8
            )) + 0x3C0
        )) + 0x50;
        

        public static int piecesAddress(ProcessMemory Game) => Game.ReadInt32(new IntPtr(
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    0x140461B20
                )) + 0x378
            )) + 0xB8
        )) + 0x15C;

        public static int getCurrentPiece(ProcessMemory Game) => Game.ReadByte(new IntPtr(
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

        public static int getPiecePositionX(ProcessMemory Game) => Game.ReadByte(new IntPtr(
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        0x140461B20
                    )) + 0x378
                )) + 0x40
            )) + 0x100
        ));

        public static int getPiecePositionY(ProcessMemory Game) => Game.ReadByte(new IntPtr(
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        0x140461B20
                    )) + 0x378
                )) + 0x40
            )) + 0x101
        ));

        public static int getPieceRotation(ProcessMemory Game) => Game.ReadByte(new IntPtr(
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

        public static int getPieceDropped(ProcessMemory Game) => Game.ReadByte(new IntPtr(
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

        public static List<int> getNextFromBags(ProcessMemory Game) {
            List<int> ret = new List<int>();

            int ptr = Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                0x140598A20
                            )) + 0x138
                        )) + 0x10
                    )) + 0x80
                )) + 0x78
            ));

            for (int i = Game.ReadByte(new IntPtr(ptr + 0x3D8)); i < 14; i++) {
                ret.Add(Game.ReadByte(new IntPtr(
                    ptr + 0x320 + 0x04 * i
                )));
            }
            
            return ret;
        }

        public static List<int> getNextFromRNG(ProcessMemory Game, int amount) {
            List<int> ret = new List<int>();

            int ptr = Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                0x140598A20
                            )) + 0x138
                        )) + 0x10
                    )) + 0x80
                )) + 0x78
            ));

            uint seed = Game.ReadUInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    ptr + 0x78
                )) + 0x80
            ));

            if (amount % 7 != 0) amount += 7 - amount % 7;

            for (int x = 0; x < amount / 7; x++) {
                List<int> bag = new List<int>() {0, 1, 2, 3, 4, 5, 6};

                for (int i = 0; i < 7; i++) {
                    seed *= 0x5D588B65;
                    seed += 0x269EC3;

                    int newIndex = (Convert.ToInt32((seed >> 16) * (7 - i)) >> 16) + i;

                    int newValue = bag[newIndex];
                    int oldValue = bag[i];
                    bag[i] = newValue;
                    bag[newIndex] = oldValue;
                }

                ret = ret.Concat(bag).ToList();
            }

            return ret;
        }

        public static int getHold(ProcessMemory Game) => Game.ReadByte(new IntPtr(
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        0x140598A20
                    )) + 0x38
                )) + 0x3D0
            )) + 0x8
        ));

        public static int getCleared(ProcessMemory Game) => Game.ReadByte(new IntPtr(
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    0x140598A20
                )) + 0x38
            )) + 0x3E8
        ));
    }
}
