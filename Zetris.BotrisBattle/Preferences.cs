using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

using MisaMinoNET;

namespace Zetris.BotrisBattle {
    static class Preferences {
        static readonly string UserPath = Path.Combine(
            Environment.GetEnvironmentVariable("USERPROFILE"),
            ".zetris"
        );

        static readonly string FilePath = Path.Combine(UserPath, "Zetris-BotrisBattle.config");

        static bool Initialized = false;

        public static List<Style> Styles = new List<Style>() {
            new Style("T-Spin+ B2B"),
            new Style("T-Spin+", new MisaMinoParameters(13, 9, 17, 10, 29, 25, 39, 2, 12, 19, 7, 24, 21, 16, 14, 19, 0, 30, 0, 24)),
            new Style("TST", new MisaMinoParameters(16, 9, 11, 17, 500, 25, 39, 2, 12, 19, 7, 1, 18, 7, 14, 19, 25, 30, 18, 19)),
            new Style("Ultra", new MisaMinoParameters(16, 9, 11, 23, 20, 1, 39, 2, 12, 19, 7, 24, 32, 16, 1, 19, 500, 0, 63, 0)),
            new Style("20 TSD", new MisaMinoParameters(0, 0, 0, 500, 0, 0, 0, 2, 12, 19, 7, 74, 0, 0, 0, 19, 500, 0, 0, 0))
        };

        static int _styleindex = 0;
        public static int StyleIndex {
            get => _styleindex;
            set {
                _styleindex = Math.Max(0, Math.Min(Styles.Count - 1, value));
                BotrisBattleBot.Instance.UpdateConfig();
                Save();
            }
        }

        public static Style CurrentStyle => Styles[StyleIndex];

        static int _intelligence = 100;
        public static int Intelligence {
            get => _intelligence;
            set {
                _intelligence = Math.Max(10, Math.Min(150, value));
                BotrisBattleBot.Instance.UpdateConfig();
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

        static uint _pcthreads = 1;
        public static uint PCThreads {
            get => _pcthreads;
            set {
                _pcthreads = Math.Max(1, Math.Min(BotrisBattleBot.PCThreadsMaximum, value));
                BotrisBattleBot.Instance.UpdatePCThreads();
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
