using System;
using System.Collections.Generic;

namespace Zetris {
    class GameHelper {
        public static bool OutsideMenu(ProcessMemory Game) {
            return Game.ReadInt32(new IntPtr(0x140573A78)) == 0x0;
        }

        public static int CurrentMode(ProcessMemory Game) => Game.ReadByte(new IntPtr(
            0x140573854
        ));

        public static bool Online(ProcessMemory Game) => Game.ReadByte(new IntPtr(
            0x14059894C
        )) == 1;

        public static bool InMultiplayer(ProcessMemory Game) => Game.ReadByte(new IntPtr(
            0x140573858
        )) == 3;

        public static int MenuHighlighted(ProcessMemory Game) => Game.ReadByte(new IntPtr(
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    0x140573A78
                )) + 0x98
            )) + 0x8C
        ));

        public static int PlayerCount(ProcessMemory Game) => Game.ReadInt32(new IntPtr(
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    0x140473760
                )) + 0x20
            )) + 0xB4
        ));

        public static int LocalSteam(ProcessMemory Game) => Game.ReadInt32(new IntPtr(
            0x1405A2010
        ));

        public static int PlayerSteam(ProcessMemory Game, int index) => Game.ReadInt32(new IntPtr(
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    0x140473760
                )) + 0x20
            )) + 0x118 + index * 0x50
        ));

        public static int FindPlayer(ProcessMemory Game) {
            if (PlayerCount(Game) < 2)
                return 0;

            int localSteam = LocalSteam(Game);

            for (int i = 0; i < 4; i++)
                if (localSteam == PlayerSteam(Game, i))
                    return i;               

            return 0;
        }

        public static int scoreAddress(ProcessMemory Game) => Game.ReadInt32(new IntPtr(
            0x14057F048
        )) + 0x38;

        public static int getPlayerCount(ProcessMemory Game) {
            int ret = Game.ReadByte(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        0x140473760
                    )) + 0x20
                )) + 0xB4
            ));

            if (ret > 4) ret = 0;
            if (ret < 0) ret = 0;

            return ret;
        }

        public static int leagueAddress(ProcessMemory Game) => Game.ReadInt32(new IntPtr(
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        0x140473760
                    )) + 0x68
                )) + 0x20
            )) + 0x970
        )) - 0x38;

        public static bool InSwap(ProcessMemory Game) {
            //return false;
            if (Game.ReadBoolean(new IntPtr(0x14059894C))) {
                if (Game.ReadBoolean(new IntPtr(0x1404385C4))) {
                    return Game.ReadByte(new IntPtr(0x140438584)) == 3;
                } else {
                    return Game.ReadByte(new IntPtr(0x140573794)) == 2;
                }
            } else {
                return (Game.ReadByte(new IntPtr(0x140451C50)) & 0b11101111) == 4;
            }
        }

        public static int SwapType(ProcessMemory Game) => Game.ReadByte(new IntPtr(
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            0x140461B20
                        )) + 0x380
                    )) + 0x18
                )) + 0xD0
            )) + 0x50
        ));

        public static int charAddress(ProcessMemory Game) => Game.ReadInt32(new IntPtr(
            0x140460690
        ));

        public static short getRating(ProcessMemory Game) => Game.ReadInt16(new IntPtr(
            0x140599FF0
        ));

        public static int boardAddress(ProcessMemory Game, int index) {
            if (InSwap(Game)) {
                switch (index) {
                    case 0:
                        return Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        Game.ReadInt32(new IntPtr(
                                            Game.ReadInt32(new IntPtr(
                                                Game.ReadInt32(new IntPtr(
                                                    0x140461B20
                                                )) + 0x378
                                            )) + 0x18
                                        )) + 0xC0
                                    )) + 0x10
                                )) + 0x3C0
                            )) + 0x18
                        ));

                    case 1:
                        return Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        Game.ReadInt32(new IntPtr(
                                            0x140598A28
                                        )) + 0x140
                                    )) + 0x48
                                )) + 0x3C0
                            )) + 0x18
                        ));
                }
            } else {
                //switch (index) {
                //    case 0:
                    return Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        Game.ReadInt32(new IntPtr(
                                            0x140461B20
                                        )) + 0x378 + index * 0x04
                                    )) + 0xC0
                                )) + 0x10
                            )) + 0x3C0
                        )) + 0x18
                    ));

                  /*  case 1:
                        return Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    0x140461B20
                                )) + 0x380
                            )) + 0x5C8
                        )) + 0x50;
                        

                    case 2:
                        return Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    0x140461B20
                                )) + 0x388
                            )) + 0x5C8
                         )) + 0x50;

                    case 3:
                        return Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    0x1405989D8
                                )) + 0x28
                            )) + 0x3C0
                        )) + 0x50;*/
            }
            return -1;
        }

        public static int piecesAddress(ProcessMemory Game, int index) {
            if (InSwap(Game)) {
                switch (index) {
                    case 0:
                        return Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        0x140461B20
                                    )) + 0x380
                                )) + 0x18
                            )) + 0xB8
                        )) + 0x15C;

                    case 1:
                        return Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        0x1404611B8
                                    )) + 0x88
                                )) + 0x1E0
                            )) + 0xB8
                        )) + 0x15C;
                }
            } else {
                /*switch (index) {
                    case 0:
                        return Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    0x140461B20
                                )) + 0x378
                            )) + 0xB8
                        )) + 0x15C;

                    case 1:
                        return Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        0x1405989D0
                                    )) + 0x78
                                )) + 0x28
                            )) + 0xB8
                        )) + 0x15C;

                    case 2:
                        return Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    0x1404611B8
                                )) + 0x30
                            )) + 0x5C8
                        )) + 0xA6C;

                    case 3:
                        return Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    0x14057F050
                                )) + 0x18
                            )) + 0xB8
                        )) + 0x15C;
                }*/
                return Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            0x140461B20
                        )) + 0x378 + index * 0x4
                    )) + 0xB8
                )) + 0x15C;
            }

            return -1;
        }

        public static int getCurrentPiece(ProcessMemory Game, int index) {
            if (InSwap(Game)) {
                switch (index) {
                    case 0:
                        return Game.ReadByte(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        Game.ReadInt32(new IntPtr(
                                            Game.ReadInt32(new IntPtr(
                                                0x140461B20
                                            )) + 0x380
                                        )) + 0x18
                                    )) + 0x40
                                )) + 0x140
                            )) + 0x110
                        ));

                    case 1:
                        return Game.ReadByte(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        Game.ReadInt32(new IntPtr(
                                            0x1404611B8
                                        )) + 0x30
                                    )) + 0xC0
                                )) + 0x18
                            )) + 0x610
                        ));
                }
            } else {
                switch (index) {
                    case 0:
                        return Game.ReadByte(new IntPtr(
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

                    case 1:
                        return Game.ReadByte(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        Game.ReadInt32(new IntPtr(
                                            0x140461B28
                                        )) + 0x380
                                    )) + 0x40
                                )) + 0x140
                            )) + 0x110
                        ));

                    case 2:
                        return Game.ReadByte(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        Game.ReadInt32(new IntPtr(
                                            0x140461B20
                                        )) + 0x388
                                    )) + 0xC0
                                )) + 0x120
                            )) + 0x110
                        ));

                    case 3:
                        return Game.ReadByte(new IntPtr(
                            Game.ReadByte(new IntPtr(
                                Game.ReadByte(new IntPtr(
                                    Game.ReadByte(new IntPtr(
                                        Game.ReadByte(new IntPtr(
                                            0x140598A28
                                        )) + 0x28
                                    )) + 0x40
                                )) + 0x140
                            )) + 0x110
                        ));
                }
            }
            
            return -1;
        }

        public static int getPiecePositionX(ProcessMemory Game, int index) {
            if (InSwap(Game)) {
                switch (index) {
                    case 0:
                        return Game.ReadByte(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        Game.ReadInt32(new IntPtr(
                                            0x140461B20
                                        )) + 0x380
                                    )) + 0x18
                                )) + 0x40
                            )) + 0x100
                        ));

                    case 1:
                        return Game.ReadByte(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    0x1405989C8
                                )) + 0x40
                            )) + 0x100
                        ));
                }
            } else {
                switch (index) {
                    case 0:
                        return Game.ReadByte(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        0x140461B20
                                    )) + 0x378
                                )) + 0x40
                            )) + 0x100
                        ));

                    case 1:
                        return Game.ReadByte(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        Game.ReadInt32(new IntPtr(
                                            0x140461B20
                                        )) + 0x380
                                    )) + 0xC0
                                )) + 0x120
                            )) + 0x1E
                        ));

                    case 2:
                        return Game.ReadByte(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        Game.ReadInt32(new IntPtr(
                                            Game.ReadInt32(new IntPtr(
                                                0x140461B28
                                            )) + 0x380
                                        )) + 0xC0
                                    )) + 0x120
                                )) + 0x188
                            )) + 0x100
                        ));

                    case 3:
                        return Game.ReadByte(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        0x140461B20
                                    )) + 0x390
                                )) + 0xB8
                            )) + 0x5B0
                        ));
                }
            }

            return -1;
        }

        public static int getPiecePositionY(ProcessMemory Game, int index) {
            if (InSwap(Game)) {
                switch (index) {
                    case 0:
                        return Game.ReadByte(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        Game.ReadInt32(new IntPtr(
                                            0x140461B20
                                        )) + 0x380
                                    )) + 0x18
                                )) + 0x40
                            )) + 0x101
                        ));

                    case 1:
                        return Game.ReadByte(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    0x1405989C8
                                )) + 0x40
                            )) + 0x101
                        ));
                }
            } else {
                switch (index) {
                    case 0:
                        return Game.ReadByte(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        0x140461B20
                                    )) + 0x378
                                )) + 0x40
                            )) + 0x101
                        ));

                    case 1:
                        return Game.ReadByte(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        Game.ReadInt32(new IntPtr(
                                            0x140461B20
                                        )) + 0x380
                                    )) + 0xC0
                                )) + 0x120
                            )) + 0x1F
                        ));

                    case 2:
                        return Game.ReadByte(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        Game.ReadInt32(new IntPtr(
                                            Game.ReadInt32(new IntPtr(
                                                0x140461B28
                                            )) + 0x380
                                        )) + 0xC0
                                    )) + 0x120
                                )) + 0x188
                            )) + 0x101
                        ));

                    case 3:
                        return Game.ReadByte(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        0x140461B20
                                    )) + 0x390
                                )) + 0xB8
                            )) + 0x5B1
                        ));
                }
            }

            return -1;
        }

        public static int getPieceRotation(ProcessMemory Game, int index) {
            if (InSwap(Game)) {
                switch (index) {
                    case 0:
                        return Game.ReadByte(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        Game.ReadInt32(new IntPtr(
                                            Game.ReadInt32(new IntPtr(
                                                0x140461B20
                                            )) + 0x378
                                        )) + 0x30
                                    )) + 0x300
                                )) + 0x3C8
                            )) + 0x18
                        ));

                    case 1:
                        return Game.ReadByte(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        Game.ReadInt32(new IntPtr(
                                            0x140598A28
                                        )) + 0x140
                                    )) + 0x48
                                )) + 0x3C8
                            )) + 0x18
                        ));
                }
            } else {
                switch (index) {
                    case 0:
                        return Game.ReadByte(new IntPtr(
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

                    case 1:
                        return Game.ReadByte(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        Game.ReadInt32(new IntPtr(
                                            Game.ReadInt32(new IntPtr(
                                                0x1405989D0
                                            )) + 0x78
                                        )) + 0x20
                                    )) + 0xA8
                                )) + 0x3C8
                            )) + 0x18
                        ));

                    case 2:
                        return Game.ReadByte(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        0x140461B20
                                    )) + 0x388
                                )) + 0x5D0
                            )) + 0x18
                        ));

                    case 3:
                        return Game.ReadByte(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        0x1404611B8
                                    )) + 0x30
                                )) + 0x5D0
                            )) + 0x18
                        ));
                }
            }

            return -1;
        }

        public static int getPieceDropped(ProcessMemory Game, int index) {
            if (InSwap(Game)) {
                switch (index) {
                    case 0:
                        return Game.ReadByte(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        Game.ReadInt32(new IntPtr(
                                            Game.ReadInt32(new IntPtr(
                                                0x140461B20
                                            )) + 0x378
                                        )) + 0x30
                                    )) + 0x300
                                )) + 0x3C8
                            )) + 0x1C
                        ));

                    case 1:
                        return Game.ReadByte(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        Game.ReadInt32(new IntPtr(
                                            0x140598A28
                                        )) + 0x140
                                    )) + 0x48
                                )) + 0x3C8
                            )) + 0x1C
                        ));
                }
            } else {
                switch (index) {
                    case 0:
                        return Game.ReadByte(new IntPtr(
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

                    case 1:
                        return Game.ReadByte(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        Game.ReadInt32(new IntPtr(
                                            Game.ReadInt32(new IntPtr(
                                                0x1405989D0
                                            )) + 0x78
                                        )) + 0x20
                                    )) + 0xA8
                                )) + 0x3C8
                            )) + 0x1C
                        ));

                    case 2:
                        return Game.ReadByte(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        0x140461B20
                                    )) + 0x388
                                )) + 0x5D0
                            )) + 0x1C
                        ));

                    case 3:
                        return Game.ReadByte(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        0x1404611B8
                                    )) + 0x30
                                )) + 0x5D0
                            )) + 0x1C
                        ));
                }
            }

            return -1;
        }

        public static int getHoldPointer(ProcessMemory Game, int index) {
            if (InSwap(Game)) {
                switch (index) {
                    case 0:
                        return Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        Game.ReadInt32(new IntPtr(
                                            0x140461B20
                                        )) + 0x378
                                    )) + 0x30
                                )) + 0x300
                            )) + 0x3D0
                        )) + 0x8;

                    case 1:
                        return Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        0x140598A28
                                    )) + 0x140
                                )) + 0x48
                            )) + 0x3D0
                        )) + 0x8;
                }
            } else {
                switch (index) {
                    case 0:
                        return Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    0x140598A20
                                )) + 0x38
                            )) + 0x3D0
                        )) + 0x8;

                    case 1:
                        return Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        0x1405989D0
                                    )) + 0x270
                                )) + 0x20
                            )) + 0x3D0
                        )) + 0x8;

                    case 2:
                        return Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    0x140461B20
                                )) + 0x388
                            )) + 0x5D8
                        )) + 0x8;

                    case 3:
                        return Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    0x140598A28
                                )) + 0x38
                            )) + 0x3D0
                        )) + 0x8;
                }
            }

            return -1;
        }

        public static int? getHold(ProcessMemory Game, int index) {
            int ptr = getHoldPointer(Game, index);

            if (ptr < 0x0800000) return null;

            return Game.ReadInt32(new IntPtr(
                ptr
            ));
        }

        public static int getGarbageOverhead(ProcessMemory Game, int index) {
            if (InSwap(Game)) {
                switch (index) {
                    case 0:
                        return Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        Game.ReadInt32(new IntPtr(
                                            0x140461B98
                                        )) + 0x88
                                    )) + 0x18
                                )) + 0xD0
                            )) + 0x150
                        ));

                    case 1:
                        return Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        Game.ReadInt32(new IntPtr(
                                            0x140461B28
                                        )) + 0x380
                                    )) + 0x1F0
                                )) + 0xE8
                            )) + 0x308
                        ));
                }
            } else {
                switch (index) {
                    case 0:
                        return Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        Game.ReadInt32(new IntPtr(
                                            Game.ReadInt32(new IntPtr(
                                                0x140461B20
                                            )) + 0x378
                                        )) + 0x28
                                    )) + 0x18
                                )) + 0xD0
                            )) + 0x64
                        ));

                    case 1:
                        return Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        Game.ReadInt32(new IntPtr(
                                            0x140461B20
                                        )) + 0x378
                                    )) + 0x28
                                )) + 0xD0
                            )) + 0x3C
                        ));

                    case 2:
                        return Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        0x140598A28
                                    )) + 0x1F8
                                )) + 0x3C0
                            )) + 0xB04
                        ));

                    case 3:
                        return Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        0x1404611B8
                                    )) + 0x30
                                )) + 0x5C8
                            )) + 0xB04
                        ));
                }
            }

            return -1;
        }

        public static int getCombo(ProcessMemory Game, int index) {
            int ret = -1;

            if (InSwap(Game)) {
                switch (index) {
                    case 0:
                        ret = Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        Game.ReadInt32(new IntPtr(
                                            0x140461B20
                                        )) + 0x378
                                    )) + 0x30
                                )) + 0x300
                            )) + 0x3DC
                        ));
                        break;

                    case 1:
                        ret = Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        0x140598A28
                                    )) + 0x140
                                )) + 0x48
                            )) + 0x3DC
                        ));
                        break;
                }
            } else {
                switch (index) {
                    case 0:
                        ret = Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    0x140598A20
                                )) + 0x38
                            )) + 0x3DC
                        ));
                        break;

                    case 1:
                        ret = Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    0x140598A28
                                )) + 0x38
                            )) + 0x3DC
                        ));
                        break;

                    case 2:
                        ret = Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadByte(new IntPtr(
                                    0x140598A28
                                )) + 0x1F8
                            )) + 0x3DC
                        ));
                        break;

                    case 3:
                        ret = Game.ReadByte(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    0x1405989D8
                                )) + 0x28
                            )) + 0x3DC
                        ));
                        break;
                }
            }

            return Math.Max(ret & 255, 0);
        }

        public static bool getBackToBack(ProcessMemory Game, int index) {
            byte ret = 0;
            switch (index) {
                case 0:
                    ret = Game.ReadByte(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadByte(new IntPtr(
                                0x140598A20
                            )) + 0x38
                        )) + 0x3DD
                    ));
                    break;

                case 1:
                    ret = Game.ReadByte(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadByte(new IntPtr(
                                0x140598A28
                            )) + 0x38
                        )) + 0x3DD
                    ));
                    break;

                case 2:
                    ret = Game.ReadByte(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadByte(new IntPtr(
                                0x140598A28
                            )) + 0x1F8
                        )) + 0x3DD
                    ));
                    break;

                case 3:
                    ret = Game.ReadByte(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                0x1405989D8
                            )) + 0x28
                        )) + 0x3DD
                    ));
                    break;
            }
            return ret == 1;
        }

        public static int getFrameCount(ProcessMemory Game) => Game.ReadInt32(new IntPtr(
            Game.ReadInt32(new IntPtr(
                0x140461B20
            )) + 0x424
        ));

        public static int getBigFrameCount(ProcessMemory Game) {
            int addr;

            if (InSwap(Game)) {
                addr = Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            0x140598A20
                        )) + 0x20
                    )) + 0x40
                )) + 0xF8;
            } else {
                addr = Game.ReadInt32(new IntPtr(
                        //Game.ReadInt32(new IntPtr(
                        0x140598A20
                    )) + 0x208;
                //));
            }

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

        public static List<int> getNextFromBags(ProcessMemory Game, int index) {
            List<int> ret = new List<int>();
            int ptr = 0;

            if (InSwap(Game)) {
                switch (index) {
                    case 0:
                        ptr = Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        Game.ReadInt32(new IntPtr(
                                            0x140598A28
                                        )) + 0x140
                                    )) + 0x28
                                )) + 0x88
                            )) + 0x78
                        ));
                        break;

                    case 1:
                        ptr = Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        Game.ReadInt32(new IntPtr(
                                            0x140598A28
                                        )) + 0x138
                                    )) + 0x10
                                )) + 0x80
                            )) + 0x78
                        ));
                        break;
                }
            } else {

                switch (index) {
                    case 0:
                        ptr = Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                0x140598A20
                            )) + 0x1F8
                        ));
                        break;

                    case 1:
                        ptr = Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    0x1405989D0
                                )) + 0x30
                            )) + 0x20
                        ));
                        break;

                    case 2:
                        ptr = Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        0x140461B10
                                    )) + 0x2C0
                                )) + 0x158
                            )) + 0xE0
                        ));
                        break;

                    case 3:
                        ptr = Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    0x140598A28
                                )) + 0x140
                            )) + 0x48
                        ));
                        break;

                }
                for (int i = Game.ReadByte(new IntPtr(ptr + 0x3D8)); i < 14; i++)
                {
                    ret.Add(Game.ReadByte(new IntPtr(
                        ptr + 0x320 + 0x04 * i
                    )));
                }
            }
            return ret;
        }

        public static int CharSelectIndex(ProcessMemory Game, int index) => Game.ReadByte(new IntPtr(
            Game.ReadInt32(new IntPtr(
                0x140460690
            )) + 0x458 + 0x48 * index
        ));

        public static bool IsReplay(ProcessMemory Game) => Game.ReadByte(new IntPtr(
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    0x1405989D0
                )) + 0x40
            )) + 0x28
        )) != 0;

        public static uint RNG(ProcessMemory Game, int index) => Game.ReadUInt32(new IntPtr(
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    0x140598A20 + 8 * index
                                )) + 0x138
                            )) + 0x10
                        )) + 0x80
                    )) + 0x78
                )) + 0x78
            )) + 0x80
        ));
    }
}
