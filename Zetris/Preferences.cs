using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using MisaMinoNET;

namespace Zetris {
    public static class Preferences {
        static readonly string UserPath = Path.Combine(
            Environment.GetEnvironmentVariable("USERPROFILE"),
            ".zetris"
        );

        static readonly string FilePath = Path.Combine(UserPath, "Zetris.config");

        static bool Initialized = false;

        public static List<Style> Styles = new List<Style>() {
            new Style("T-Spin+ B2B"),
            new Style("T-Spin+", new MisaMinoParameters(13, 9, 17, 10, 29, 25, 39, 2, 12, 19, 7, 24, 21, 16, 14, 19, 6, 30, 0, 24)),
            new Style("TST", new MisaMinoParameters(13, 9, 17, 10, 500, 25, 39, 2, 12, 19, 7, 1, 21, 16, 14, 19, 35, 20, 20, 0, 0)),
            new Style("20 TSD", new MisaMinoParameters(0, 0, 0, 500, 0, 0, 0, 2, 12, 19, 7, 74, 0, 0, 0, 19, 500, 0, 0, 0, 0))
        };

        static int _styleindex = 0;
        public static int StyleIndex {
            get => _styleindex;
            set {
                _styleindex = Math.Max(0, Math.Min(Styles.Count - 1, value));
                Bot.UpdateConfig();
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

        static int _previews = 18;
        public static int Previews {
            get => _previews;
            set {
                _previews = Math.Max(0, Math.Min(18, value));
                Save();
            }
        }

        static bool _hold = true;
        public static bool HoldAllowed {
            get => _hold;
            set {
                _hold = value;
                Bot.UpdateConfig();
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

        static bool _c4w = false;
        public static bool C4W {
            get => _c4w;
            set {
                _c4w = value;
                Bot.UpdateConfig();
                Save();
            }
        }

        static int _player = 1;
        public static int Player {
            get => _player;
            set {
                _player = Math.Max(0, Math.Min(1, value));
                Save();
            }
        }

        static bool _auto = false;
        public static bool Auto {
#if !PUBLIC
            get => _auto;
#else
            get => false;
#endif
            set {
                _auto = value;
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
