using System;
using System.Collections.Generic;

namespace Zetris {
    static class GameHelper {
        static ProcessMemory Game = new ProcessMemory("puyopuyotetris", false);

        public static bool CheckProcess() => Game.CheckProcess();

        public static bool TrustProcess {
            get => Game.TrustProcess;
            set {
                Game.TrustProcess = value;
            }
        }

        public static bool OutsideMenu() {
            return Game.ReadInt32(new IntPtr(0x140573A78)) == 0x0;
        }

        public static int CurrentMode() => Game.ReadByte(new IntPtr(
            0x140573854
        ));

        public static bool Online() => Game.ReadByte(new IntPtr(
            0x14059894C
        )) == 1;

        public static bool InMultiplayer() => Game.ReadByte(new IntPtr(
            0x140573858
        )) == 3;

        public static int MenuHighlighted() => Game.ReadByte(new IntPtr(
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    0x140573A78
                )) + 0x98
            )) + 0x8C
        ));

        public static int PlayerCount() => Game.ReadInt32(new IntPtr(
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    0x140473760
                )) + 0x20
            )) + 0xB4
        ));

        public static int LocalSteam() => Game.ReadInt32(new IntPtr(
            0x1405A2010
        ));

        public static int PlayerSteam(int index) => Game.ReadInt32(new IntPtr(
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    0x140473760
                )) + 0x20
            )) + 0x118 + index * 0x50
        ));

        public static int FindPlayer() {
            if (PlayerCount() < 2)
                return 0;

            int localSteam = LocalSteam();

            for (int i = 0; i < 2; i++)
                if (localSteam == PlayerSteam(i))
                    return i;

            return 0;
        }

        public static int scoreAddress() => Game.ReadInt32(new IntPtr(
            0x14057F048
        )) + 0x38;

        public static int getPlayerCount() {
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

        public static int leagueAddress() => Game.ReadInt32(new IntPtr(
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        0x140473760
                    )) + 0x68
                )) + 0x20
            )) + 0x970
        )) - 0x38;

        public static bool InSwap() {
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

        public static int SwapType() => Game.ReadByte(new IntPtr(
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

        public static int charAddress() => Game.ReadInt32(new IntPtr(
            0x140460690
        ));

        public static short getRating() => Game.ReadInt16(new IntPtr(
            0x140599FF0
        ));

        public static bool getPlayerIsTetris(int index) => (Game.ReadByte(new IntPtr(
            0x140598C27 + 0x68 * index
        )) & 64) == 1;

        public static int boardAddress(int index) {
            if (InSwap()) {
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
                                                )) + 0x380
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
                                            0x1404611B8
                                        )) + 0x30
                                    )) + 0xA8
                                )) + 0x3C0
                            )) + 0x18
                        ));
                }
            }

            return -1;
        }

        public static int[,] getBoard(int index) {
            int[,] ret = new int[10, 40];

            int boardaddr = boardAddress(index);
            for (int i = 0; i < 10; i++) {
                int columnAddress = Game.ReadInt32(new IntPtr(boardaddr + i * 0x08));
                for (int j = 0; j < 28; j++) {
                    ret[i, j] = Game.ReadByte(new IntPtr(columnAddress + j * 0x04));
                }
            }

            return ret;
        }

        public static int piecesAddress(int index) {
            if (InSwap()) {
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
                switch (index) {
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
                }
            }

            return -1;
        }

        public static int[] getPieces(int index) {
            int[] ret = new int[5];

            int pieceaddr = piecesAddress(index);

            for (int i = 0; i < 5; i++) {
                ret[i] = Game.ReadByte(new IntPtr(pieceaddr + i * 0x04));
            }

            return ret;
        }

        public static int getCurrentPiece(int index) {
            if (InSwap()) {
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
                }
            }

            return -1;
        }

        public static int getPiecePositionX(int index) {
            if (InSwap()) {
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
                }
            }

            return -1;
        }

        public static int getPiecePositionY(int index) {
            if (InSwap()) {
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
                }
            }

            return -1;
        }

        public static int getPieceRotation(int index) {
            if (InSwap()) {
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
                }
            }

            return -1;
        }

        public static int getPieceDropped(int index) {
            if (InSwap()) {
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
                }
            }

            return -1;
        }

        public static int getHoldPointer(int index) {
            if (InSwap()) {
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
                }
            }

            return -1;
        }

        public static int? getHold(int index) {
            int ptr = getHoldPointer(index);

            if (ptr < 0x0800000) return null;

            return Game.ReadInt32(new IntPtr(
                ptr
            ));
        }

        public static int getGarbagePointer(int index) {
            if (InSwap()) {
                return 0; // todo implement

            } else {
                switch (index) {
                    case 0:
                        return Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    0x1405989D0
                                )) + 0x78
                            )) + 0xB8
                        )) + 0x210;

                    case 1:
                        return Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    0x1404611B8
                                )) + 0x30
                            )) + 0xB8
                        )) + 0x1EC;
                }
            }

            return 0;
        }

        public static int getGarbageDropping(int index) => Game.ReadInt32(new IntPtr(
            getGarbagePointer(index) + 0x8
        ));

        public static int getCombo(int index) {
            int ret = -1;

            if (InSwap()) {
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
                }
            }

            return Math.Max(ret & 255, 0);
        }

        public static int getFrameCount() => Game.ReadInt32(new IntPtr(
            Game.ReadInt32(new IntPtr(
                0x140461B20
            )) + 0x424
        ));

        public static int getBigFrameCount() {
            int addr;

            if (InSwap()) {
                addr = Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            0x140598A20
                        )) + 0x20
                    )) + 0x40
                )) + 0xF8;
            } else {
                addr = Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                0x140598A20
                            )) + 0x138
                        )) + 0x18
                    )) + 0x100
                )) + 0x58;
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

        public static int getMenuFrameCount() => Game.ReadInt32(new IntPtr(
            0x140461B7C
        ));

        public static List<int> getNextFromBags(int index) {
            List<int> ret = new List<int>();
            int ptr = 0;

            if (InSwap()) {
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
                ptr = Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    0x140598A20 + index * 8
                                )) + 0x138
                            )) + 0x10
                        )) + 0x80
                    )) + 0x78
                ));
            }

            for (int i = Game.ReadByte(new IntPtr(ptr + 0x3D8)); i < 14; i++) {
                ret.Add(Game.ReadByte(new IntPtr(
                    ptr + 0x320 + 0x04 * i
                )));
            }

            return ret;
        }

        public static int CharSelectIndex(int index) => Game.ReadByte(new IntPtr(
            Game.ReadInt32(new IntPtr(
                0x140460690
            )) + 0x458 + 0x48 * index
        ));

        public static uint RNG(int index) => Game.ReadUInt32(new IntPtr(
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

        public static bool IsCharacterSelect() {
            int P1State = Game.ReadByte(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    0x140460690
                )) + 0x274
            ));

            return P1State > 0 && P1State < 16;
        }
    }
}