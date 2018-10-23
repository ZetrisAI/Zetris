using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTMonitor {
    class GameHelper {
        public class Player {
            public string name;
            public int rating;
            public int league;
            public int playstyle;
            public int region;
            public int regional;
            public int worldwide;
            public int id;
            public string steam;
            public int pref;
            public int character = -1;
            public int gamemode = -1;
            public int voice = 0;

            public Player() {}
        }

        public static int scoreAddress(VAMemory Game) => Game.ReadInt32(new IntPtr(
            0x14057F048
        )) + 0x38;

        public static int playerAddress(VAMemory Game) => Game.ReadInt32(new IntPtr(
            Game.ReadInt32(new IntPtr(
                0x140473760
            )) + 0x20
        )) + 0xD8;

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

        public static int[] boardAddress(VAMemory Game) => new int[] {
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        0x1405989D0
                    )) + 0x3C0
                )) + 0x18
            )),
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            0x1405989D0
                        )) + 0x30
                    )) + 0x20
                )) + 0x3C0
            )) + 0x50
        };

        public static int piecesAddress(VAMemory Game) => Game.ReadInt32(new IntPtr(
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    0x140461B20
                )) + 0x378
            )) + 0xB8
        )) + 0x15C;

        public static int getCurrentPiece(VAMemory Game) => Game.ReadByte(new IntPtr(
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            0x140461B20
                        )) + 0x378
                    )) + 0xC0
                )) + 0x120
            )) + 0x110
        ));

        public static int getPiecePosition(VAMemory Game) => Game.ReadByte(new IntPtr(
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        0x140461B20
                    )) + 0x378
                )) + 0x40
            )) + 0x100
        ));

        public static int getPieceRotation(VAMemory Game) => Game.ReadByte(new IntPtr(
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            0x1405989D0
                        )) + 0x78
                    )) + 0xA8
                )) + 0x3C8
            )) + 0x18
        ));

        public static int getFrameCount(VAMemory Game) => Game.ReadInt32(new IntPtr(
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        0x140598A20
                    )) + 0x138
                )) + 0x30
            )) + 0x208
        ));
    }
}
