using System;
using System.Collections.Generic;
using System.Linq;

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

        public static CachedMethod<bool> OutsideMenu = new CachedMethod<bool>(() =>
            Game.ReadInt32(new IntPtr(0x140573A78)) == 0x0
        );

        public static CachedMethod<int> GameEnd = new CachedMethod<int>(() =>
            Game.ReadByte(new IntPtr(    //16 if in post game menu
                Game.ReadInt32(new IntPtr(      //36 if in post game pre-menu area (I.E. league results)
                    0x140460690
                )) + 0x78
            ))
        );

        public static CachedMethod<int, byte> MenuNavigation = new CachedMethod<int, byte>((type) => {
            int addr = Game.ReadInt32(new IntPtr(0x140461B38));

            switch (type) {
                case 0:
                    addr += 250;
                    return (byte)addr;  //used for when pointer is dead, 250 is arbitrary

                case 1:
                    return Game.ReadByte(new IntPtr(addr + 0x95)); //paused

                case 2:
                    return Game.ReadByte(new IntPtr(addr + 0x98)); //menu selection
            }
            return 0;
        });

        public static CachedMethod<byte> CanSaveReplay = new CachedMethod<byte>(() =>
            Game.ReadByte(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    0x140461B48
                )) + 0x41
            ))
        );

        public static CachedMethod<byte> ConfirmingReplay = new CachedMethod<byte>(() =>
            Game.ReadByte(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    0x140461B48
                )) + 0x40
            ))
        );


        public static CachedMethod<int> ReplayMenuSelection = new CachedMethod<int>(() =>
            Game.ReadByte(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                0x140461B40
                            )) + 0xB8
                        )) + 0x140
                    )) + 0x18
                )) + 0x8
            ))
        );

        public static CachedMethod<int> CurrentMode = new CachedMethod<int>(() =>
            Game.ReadByte(new IntPtr(
                0x140573854
            ))
        );

        public static CachedMethod<bool> Online = new CachedMethod<bool>(() =>
            Game.ReadByte(new IntPtr(
                0x14059894C
            )) == 1
        );

        public static CachedMethod<int> LobbyPtr = new CachedMethod<int>(() =>
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    0x140473760
                )) + 0x20
            ))
        );

        public static CachedMethod<bool> InMultiplayer = new CachedMethod<bool>(() =>
            Game.ReadByte(new IntPtr(
                0x140573858
            )) == 3
        );

        public static CachedMethod<int> MenuHighlighted = new CachedMethod<int>(() =>
            Game.ReadByte(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        0x140573A78
                    )) + 0x98
                )) + 0x8C
            ))  
        );

        public static CachedMethod<int> PlayerCount = new CachedMethod<int>(() =>
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        0x140473760
                    )) + 0x20
                )) + 0xB4
            ))
        );

        public static CachedMethod<int> LocalSteam = new CachedMethod<int>(() =>
            Game.ReadInt32(new IntPtr(
                0x1405A2010
#if PUBLIC
                + LobbyPtr()
#endif
            ))
        );

        public static CachedMethod<int, int> PlayerSteam = new CachedMethod<int, int>((index) =>
            Game.ReadInt32(new IntPtr(
                LobbyPtr.Call() + 0x118 + index * 0x50
            ))
        );

        public static CachedMethod<int> FindPlayer = new CachedMethod<int>(() => {
            if (PlayerCount.Call() < 2)
                return 0;

            int localSteam = LocalSteam.Call();

            for (int i = 0; i < 4; i++)
                if (localSteam == PlayerSteam.Call(i))
                    return i;

            return 0;
        });

        public static CachedMethod<int> getPlayerCount = new CachedMethod<int>(() => {
            int ret = Game.ReadByte(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        0x140473760
                    )) + 0x20
                )) + 0xB4
            ));

            if (ret < 0 || ret > 4) ret = 0;

            return ret;
        });

        public static CachedMethod<bool> InSwap = new CachedMethod<bool>(() => {
            if (Game.ReadBoolean(new IntPtr(0x14059894C))) {
                if (Game.ReadBoolean(new IntPtr(0x1404385C4))) {
                    return Game.ReadByte(new IntPtr(0x140438584)) == 3;
                } else {
                    return Game.ReadByte(new IntPtr(0x140573794)) == 2;
                }
            } else {
                return (Game.ReadByte(new IntPtr(0x140451C50)) & 0b11101111) == 4;
            }
        });

        public static CachedMethod<int> SwapType = new CachedMethod<int>(() =>
            Game.ReadByte(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                0x140461B20
                            )) + 0x380
                        )) + 0x18
                    )) + 0xD0
                )) + 0x50
            ))
        );

        public static CachedMethod<ushort> getRating = new CachedMethod<ushort>(() =>
            Game.ReadUInt16(new IntPtr(
                0x140599FF0
            ))
        );

        public static CachedMethod<int, bool> getPlayerIsTetris = new CachedMethod<int, bool>((index) =>
            (Game.ReadByte(new IntPtr(
                0x140598C27 + 0x68 * index
            )) & 64) == 1
        );

        public static CachedMethod<int, int> boardAddress = new CachedMethod<int, int>((index) => {
            if (InSwap.Call()) {
                return Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        0x140461B20
                                    )) + 0x378 + index * 0x8
                                )) + 0xA8
                            )) + 0x300
                        )) + 0x3C0
                    )) + 0x18
                ));
            } else {
                return Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        0x140461B20
                                    )) + 0x378 + index * 0x8
                                )) + 0xC0
                            )) + 0x10
                        )) + 0x3C0
                    )) + 0x18
                ));
            }
        });

        public static CachedMethod<int, int[,]> getBoard = new CachedMethod<int, int[,]>((index) => {
            int[,] ret = new int[10, 40];

            int boardaddr = boardAddress.Call(index);
#if PUBLIC
            boardaddr += LobbyPtr();
#endif

            for (int i = 0; i < 10; i++) {
                int columnAddress = Game.ReadInt32(new IntPtr(boardaddr + i * 0x08));
                for (int j = 0; j < 28; j++) {
                    ret[i, j] = Game.ReadByte(new IntPtr(columnAddress + j * 0x04));
                }
            }

            return ret;
        });

        public static CachedMethod<int, int> piecesAddress = new CachedMethod<int, int>((index) => {
            if (InSwap.Call()) {
                if (index == 0) {
                    return Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    0x140461B20
                                )) + 0x380
                            )) + 0x18
                        )) + 0xB8
                    )) + 0x15C;
                } else {
                    return Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    0x140461B20
                                )) + 0x378 + index * 0x8
                            )) + 0x1E0
                        )) + 0xB8
                    )) + 0x15C;
                }
            } else {
                return Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            0x140461B20
                        )) + 0x378 + index * 0x8
                    )) + 0xB8
                )) + 0x15C;
            }
        });

        public static CachedMethod<int, int[]> getPieces = new CachedMethod<int, int[]>((index) => {
            int[] ret = new int[5];

            int pieceaddr = piecesAddress.Call(index);
#if PUBLIC
            pieceaddr += LobbyPtr();
#endif

            for (int i = 0; i < 5; i++) {
                ret[i] = Game.ReadByte(new IntPtr(pieceaddr + i * 0x04));
            }

            return ret;
        });

        public static CachedMethod<int, int> getCurrentPiece = new CachedMethod<int, int>((index) => {
            if (InSwap.Call()) {
                return Game.ReadByte(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        0x140461B20
                                    )) + 0x378 + index * 0x8
                                )) + 0x1E0
                            )) + 0x40
                        )) + 0x140
                    )) + 0x110
                ));
            } else {
                return Game.ReadByte(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    0x140461B20
                                )) + 0x378 + index * 0x8
                            )) + 0xC0
                        )) + 0x120
                    )) + 0x110
                ));
            }
        });

        public static CachedMethod<int, int> getPiecePositionX = new CachedMethod<int, int>((index) => {
            if (InSwap.Call()) {
                return Game.ReadByte(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    0x140461B20
                                )) + 0x378 + index * 0x8
                            )) + 0x1E0
                        )) + 0x40
                    )) + 0x100
                ));
            } else {
                return Game.ReadByte(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    0x140461B20
                                )) + 0x378 + index * 0x8
                            )) + 0xC0
                        )) + 0x120
                    )) + 0x1E
                ));
            }
        });

        public static CachedMethod<int, int> getPiecePositionY = new CachedMethod<int, int>((index) => {
            if (InSwap.Call()) {
                return Game.ReadByte(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    0x140461B20
                                )) + 0x378 + index * 0x8
                            )) + 0x1E0
                        )) + 0x40
                    )) + 0x101
                ));
            } else {
                return Game.ReadByte(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    0x140461B20
                                )) + 0x378 + index * 0x8
                            )) + 0xC0
                        )) + 0x120
                    )) + 0x1F
                ));
            }
        });

        public static CachedMethod<int, int> getPieceRotation = new CachedMethod<int, int>((index) => {
            if (InSwap.Call()) {
                return Game.ReadByte(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        0x140461B20
                                    )) + 0x378 + index * 0x8
                                )) + 0xA8
                            )) + 0x300
                        )) + 0x3C8
                    )) + 0x18
                ));
            } else {
                return Game.ReadByte(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    0x140461B20
                                )) + 0x378 + index * 0x8
                            )) + 0xA8
                        )) + 0x3C8
                    )) + 0x18
                ));
            }
        });

        public static CachedMethod<int, int> getPieceDropped = new CachedMethod<int, int>((index) => {
#if PUBLIC
            index += LobbyPtr();
#endif

            if (InSwap.Call()) {
                return Game.ReadByte(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        0x140461B20
                                    )) + 0x378 + index * 0x8
                                )) + 0xA8
                            )) + 0x300
                        )) + 0x3C8
                    )) + 0x1C
                ));
            } else {
                return Game.ReadByte(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    0x140461B20
                                )) + 0x378 + index * 0x8
                            )) + 0xA8
                        )) + 0x3C8
                    )) + 0x1C
                ));
            }
        });

        public static CachedMethod<int, int> getHoldPointer = new CachedMethod<int, int>((index) => {
            if (InSwap.Call()) {
                return Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    0x140461B20
                                )) + 0x378 + index * 0x8
                            )) + 0x30
                        )) + 0x300
                    )) + 0x3D0
                )) + 0x8;
            } else {
                return Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                0x140461B20
                            )) + 0x378 + index * 0x8
                        )) + 0xA8
                    )) + 0x3D0
                )) + 0x8;
            }
        });

        public static CachedMethod<int, int?> getHold = new CachedMethod<int, int?>((index) => {
            int ptr = getHoldPointer.Call(index);

            if (ptr < 0x0800000) return null;

            return Game.ReadInt32(new IntPtr(
                ptr
            ));
        });

        public static CachedMethod<int, int> getGarbageDropping = new CachedMethod<int, int>((index) => {
            if (InSwap.Call()) {
                return Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    Game.ReadInt32(new IntPtr(
                                        0x140461B20
                                    )) + 0x378 + index * 0x8
                                )) + 0xA8
                            )) + 0x300
                        )) + 0x3C0
                    )) + 0x18
                ));
            } else {
                return Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                0x140461B20
                            )) + 0x378 + index * 0x8
                        )) + 0xD0
                    )) + 0x3C
                ));
            }
        });

        public static CachedMethod<int, int> getCombo = new CachedMethod<int, int>((index) => {
            if (InSwap.Call()) {
                return Game.ReadByte(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                Game.ReadInt32(new IntPtr(
                                    0x140461B20
                                )) + 0x378 + index * 0x8
                            )) + 0x1E0
                        )) + 0xA8
                    )) + 0x3DC
                ));
            } else {
                return Game.ReadByte(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                0x140598A28
                            )) + 0x378 + index * 0x8
                        )) + 0xA8
                    )) + 0x3DC
                ));
            }
        });

        public static CachedMethod<int> getFrameCount = new CachedMethod<int>(() =>
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    0x140461B20
                )) + 0x424
            ))
        );

        public static CachedMethod<int> getPlayer1Base = new CachedMethod<int>(() =>
            Game.ReadInt32(new IntPtr(
                0x140598A20
            ))
        );
        
        public static CachedMethod<int> getStartAnimation = new CachedMethod<int>(() =>
            Game.ReadInt32(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        0x140461B20
                    )) + 0x3A8
                )) + 0xB8
            ))
        );

        public static int getMenuFrameCount() => Game.ReadInt32(new IntPtr(
            0x140461B7C
        ));

        public static CachedMethod<int, List<int>> getNextFromBags = new CachedMethod<int, List<int>>((index) => {
            List<int> ret = new List<int>();
            int ptr;

            if (InSwap.Call()) {
                ptr = Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            Game.ReadInt32(new IntPtr(
                                0x140461B20
                            )) + 0x378 + index * 0x8
                        )) + 0x1D8
                    )) + 0x2B0
                ));
            } else {
                ptr = Game.ReadInt32(new IntPtr(
                    Game.ReadInt32(new IntPtr(
                        Game.ReadInt32(new IntPtr(
                            0x140461B20
                        )) + 0x378 + index * 8
                    )) + 0xA8
                ));
            }

            for (int i = Game.ReadByte(new IntPtr(ptr + 0x3D8)); i < 14; i++) {
                ret.Add(Game.ReadByte(new IntPtr(
                    ptr + 0x320 + 0x04 * i
                )));
            }

            return ret;
        });

        public static List<int> getNextFromRNG(int index, int amount, int atk) {
            List<int> ret = new List<int>();

            uint seed = RNG.Call(index);

            int garbage_drop = CalculateGarbage(index, atk, out int garbage_left);

            while (garbage_drop > 0) {
                seed = InputHelper.NextRNG(seed);

                for (int i = 0; i < garbage_drop; i++) {
                    if (70 < InputHelper.RandomInt(ref seed, 99))
                        seed = InputHelper.NextRNG(seed);
                }

                garbage_drop = 0;

                if (garbage_left > 0)
                    garbage_drop = CalculateGarbage(index, 0, out garbage_left, garbage_left);
            }

            if (amount % 7 != 0) amount += 7 - amount % 7;

            for (int x = 0; x < amount / 7; x++) {
                List<int> bag = new List<int>() { 0, 1, 2, 3, 4, 5, 6 };

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

        public static CachedMethod<int, int> CharSelectIndex = new CachedMethod<int, int>((index) =>
            Game.ReadByte(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    0x140460690
                )) + 0x458 + 0x48 * index
            ))
        );

        public static CachedMethod<int, uint> RNG = new CachedMethod<int, uint>((index) =>
            Game.ReadUInt32(new IntPtr(
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
            ))
        );

        public static CachedMethod<bool> IsCharacterSelect = new CachedMethod<bool>(() => {
            int P1State = Game.ReadByte(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    0x140460690
                )) + 0x274
            ));

            return P1State > 0 && P1State < 16;
        });

        public static CachedMethod<int, byte> CharacterSelectState = new CachedMethod<int, byte>((index) =>
            Game.ReadByte(new IntPtr(
                Game.ReadInt32(new IntPtr(
                    0x140460690
                )) + 0x1B8 + 0x30 * index
            ))
        );

        public static int CalculateGarbage(int index, int atk, out int garbage_left, int garbage_drop = -1) {
            if (garbage_drop == -1)
                garbage_drop = Math.Max(0, getGarbageDropping.Call(index) - atk);

            garbage_left = 0;

            if ((InSwap.Call() || !getPlayerIsTetris.Call(1 - index)) && garbage_drop > 7) {
                garbage_left = garbage_drop - 7;
                garbage_drop = 7;
            }

            return garbage_drop;
        }
    }
}