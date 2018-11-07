using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTMonitor {
    class GameHelper {
        public static bool OutsideMenu(VAMemory Game) {
            return Game.ReadInt32(new IntPtr(0x140573A78)) == 0x0;
        }

        public static int CurrentMode(VAMemory Game) => Game.ReadByte(new IntPtr(
            0x140573854
        ));

        public static bool InMultiplayer(VAMemory Game) => Game.ReadByte(new IntPtr(
            0x140573858
        )) != 0;

        public static int MenuHighlighted(VAMemory Game) => Game.ReadByte(new IntPtr(
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    0x140573A78
                )) + 0x98
            )) + 0x8C
        ));

        public static int PlayerCount(VAMemory Game) => Game.ReadInt32(new IntPtr(
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    0x140473760
                )) + 0x20
            )) + 0xB4
        ));

        public static int LocalSteam(VAMemory Game) => Game.ReadInt32(new IntPtr(
            0x1405A2010
        ));

        public static int PlayerSteam(VAMemory Game, int index) => Game.ReadInt32(new IntPtr(
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    0x140473760
                )) + 0x20
            )) + 0x118 + index * 0x50
        ));

        public static int FindPlayer(VAMemory Game) {
            if (PlayerCount(Game) < 2)
                return 0;

            int localSteam = LocalSteam(Game);

            for (int i = 0; i < 2; i++)
                if (localSteam == PlayerSteam(Game, i))
                    return i;

            return 0;
        }

        public static int scoreAddress(VAMemory Game) => Game.ReadInt32(new IntPtr(
            0x14057F048
        )) + 0x38;

        public static int getPlayerCount(VAMemory Game) {
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

        public static int leagueAddress(VAMemory Game) => Game.ReadInt32(new IntPtr(
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        0x140473760
                    )) + 0x68
                )) + 0x20
            )) + 0x970
        )) - 0x38;

        public static int prefAddress(VAMemory Game) => Game.ReadInt32(new IntPtr(
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        Game.ReadInt32(new IntPtr(
                                            Game.ReadInt32(new IntPtr(
                                                0x140573A78
                                            )) + 0x20
                                        )) + 0x20
                                    )) + 0x20
                                )) + 0xA8
                            )) + 0x68
                        )) + 0x90
                    )) + 0x28
                )) + 0x18
            )) + 0x08
        )) + 0xD4;

        public static int charAddress(VAMemory Game) => Game.ReadInt32(new IntPtr(
            0x140460690
        ));

        public static short getRating(VAMemory Game) => Game.ReadInt16(new IntPtr(
            0x140599FF0
        ));

        public static int boardAddress(VAMemory Game, int index) {
            switch (index) {
                case 0:
                    return Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    0x140598A20
                                )) + 0x38
                            )) + 0x3C0
                        )) + 0x18
                    ));


                case 1:
                    return Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    0x1405989D0
                                )) + 0x30
                            )) + 0x20
                        )) + 0x3C0
                    )) + 0x50;
            }

            return -1;
        }

        public static int piecesAddress(VAMemory Game, int index) {
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

            return -1;
        }

        public static int getCurrentPiece(VAMemory Game, int index) {
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

            return -1;
        }

        public static int getPiecePositionX(VAMemory Game, int index) {
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

            return -1;
        }

        public static int getPiecePositionY(VAMemory Game, int index) {
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

            return -1;
        }

        public static int getPieceRotation(VAMemory Game, int index) {
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

            return -1;
        }

        public static int getPieceDropped(VAMemory Game, int index) {
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

            return -1;
        }

        public static int getHoldPointer(VAMemory Game, int index) {
            switch (index) {
                case 0:
                    return Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        0x140460C08
                                    )) + 0x18
                                )) + 0x268
                            )) + 0x38
                        )) + 0x3C8
                    )) + 0x18;

                case 1:
                    return Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        0x1405989D0
                                    )) + 0x78
                                )) + 0x20
                            )) + 0xA8
                        )) + 0x3C8
                    )) + 0x18;
            }

            return -1;
        }

        public static int getGarbageOverhead(VAMemory Game, int index) {
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
            }

            return -1;
        }

        public static int getCombo(VAMemory Game, int index) {
            int ret = -1;

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

            return Math.Max(ret, 0);
        }

        public static int getFrameCount(VAMemory Game) => Game.ReadInt32(new IntPtr(
            Game.ReadInt32(new IntPtr(
                0x140461B20
            )) + 0x424
        ));

        public static int getBigFrameCount(VAMemory Game) => Game.ReadInt32(new IntPtr(
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            0x140598A20
                        )) + 0x138
                    )) + 0x18
                )) + 0x100
            )) + 0x58
        ));

        public static int getMenuFrameCount(VAMemory Game) => Game.ReadInt32(new IntPtr(
            0x140461B7C
        ));
    }
}
