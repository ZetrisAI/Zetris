﻿using System;
using System.Collections.Generic;
using System.IO;

using Zetris.PPT.Memory;

using MisaMinoNET;

namespace Zetris.PPT {
    public static class Preferences {
        static readonly string UserPath = Path.Combine(
            Environment.GetEnvironmentVariable("USERPROFILE"),
            ".zetris"
        );

        static readonly string FilePath = Path.Combine(UserPath, "Zetris.config");

        static bool Initialized = false;

        public static List<Style> Styles = new List<Style>() {
            new Style("T-Spin+ B2B"),
            new Style("T-Spin+", new MisaMinoParameters(13, 9, 17, 10, 29, 25, 39, 2, 12, 19, 7, 24, 21, 16, 14, 19, 0, 30, 0, 24, 0)),
            new Style("TST", new MisaMinoParameters(16, 9, 11, 17, 500, 25, 39, 2, 12, 19, 7, 1, 18, 7, 14, 19, 25, 30, 18, 19, 0)),
            new Style("Ultra", new MisaMinoParameters(16, 9, 11, 23, 20, 1, 39, 2, 12, 19, 7, 24, 32, 16, 1, 19, 500, 0, 63, 0, 0)),
            new Style("20 TSD", new MisaMinoParameters(0, 0, 0, 500, 0, 0, 0, 2, 12, 19, 7, 74, 0, 0, 0, 19, 500, 0, 0, 0, 0))
        };

        static int _styleindex = 0;
        public static int StyleIndex {
            get => _styleindex;
            set {
                _styleindex = Math.Max(0, Math.Min(Styles.Count - 1, value));
                PPTBot.Instance.UpdateConfig();
                Save();
            }
        }

        public static Style CurrentStyle => Styles[StyleIndex];

        static int _speed = 100;
        public static int Speed {
            get => _speed;
            set {
                _speed = Math.Max(10, Math.Min(100, value));
                Save();
            }
        }

        static int _previews = 19;
        public static int Previews {
            get => _previews;
            set {
                _previews = Math.Max(0, Math.Min(19, value));
                Save();
            }
        }

        static int _intelligence = 100;
        public static int Intelligence {
            get => _intelligence;
            set {
                _intelligence = Math.Max(10, Math.Min(150, value));
                PPTBot.Instance.UpdateConfig();
                Save();
            }
        }

        static bool _hold = true;
        public static bool HoldAllowed {
            get => _hold;
            set {
                _hold = value;
                PPTBot.Instance.UpdateConfig();
                Save();
            }
        }

        static bool _perfect = false;
        public static bool PerfectClear {
            get => _perfect;
            set {
                _perfect = value;
                Save();
            }
        }

        static bool _enhance = false;
        public static bool EnhancePerfect {
            get => _enhance;
            set {
                _enhance = value;
                Save();
            }
        }

        public static uint PCThreadsMaximum => (uint)Environment.ProcessorCount;

        static uint _pcthreads = 1;
        public static uint PCThreads {
            get => _pcthreads;
            set {
                _pcthreads = Math.Max(1, Math.Min(PCThreadsMaximum, value));
                PPTBot.Instance.UpdatePCThreads();
                Save();
            }
        }

        static bool _c4w = false;
        public static bool C4W {
            get => _c4w;
            set {
                _c4w = value;
                PPTBot.Instance.UpdateConfig();
                Save();
            }
        }

        static bool _allspins = false;
        public static bool AllSpins {
            get => _allspins;
            set {
                _allspins = value;
                PPTBot.Instance.UpdateConfig();
                Save();
            }
        }

        static bool _tsd = false;
        public static bool TSDOnly {
            get => _tsd;
            set {
                _tsd = value;
                PPTBot.Instance.UpdateConfig();
                Save();
            }
        }

        static bool _puzzleleague = false;
        public static bool PuzzleLeague {
#if !PUBLIC
            get => _puzzleleague;
#else
            get => false;
#endif
            set => _puzzleleague = value;
        }

        static bool _replay = false;
        public static bool SaveReplay {
#if !PUBLIC
            get => Game == 1? false : _replay;
#else
            get => false;
#endif
            set => _replay = value;
        }

        static bool _spam = false;
        public static bool SpamA {
#if !PUBLIC
            get => Game == 1? false : _spam;
#else
            get => false;
#endif
            set => _spam = value;
        }

        static int _player = 1;
        public static int Player {
            get => _player;
            set {
                _player = Math.Max(0, Math.Min(3, value));
                Save();
            }
        }

        static bool _accurate = true;
        public static bool AccurateSync {
            get => _accurate;
            set {
                _accurate = value;
                PPTBot.Instance.UpdatePriority();
                Save();
            }
        }

        static int _game = 0;
        public static int Game {
            get => _game;
            set {
                _game = value;
                
                if (_game == 0) GameHelper.UsePPT();
                else if (_game == 1) GameHelper.UsePPT2();

                Save();
            }
        }

        public static void Save() {
            if (!Initialized) return;

            if (!Directory.Exists(UserPath)) Directory.CreateDirectory(UserPath);

            try {
                File.WriteAllBytes(FilePath, Binary.EncodePreferences().ToArray());
            } catch (IOException) {}
        }

        static Preferences() {
            if (File.Exists(FilePath))
                using (FileStream file = File.Open(FilePath, FileMode.Open, FileAccess.Read))
                    try {
                        Binary.DecodePreferences(file);
                    } catch {}
            
            Initialized = true;
        }
    }
}
