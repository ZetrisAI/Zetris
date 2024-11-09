using System;
using System.IO;

namespace Zetris.BotrisBattle {
    static class Preferences {
        static readonly string UserPath = Path.Combine(
            Environment.GetEnvironmentVariable("USERPROFILE"),
            ".zetris"
        );

        static readonly string FilePath = Path.Combine(UserPath, "Zetris-BotrisBattle.config");

        static bool Initialized = false;

        static uint _pcthreads = 5;
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
