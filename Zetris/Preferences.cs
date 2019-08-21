using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Zetris {
    public static class Preferences {
        static readonly string UserPath = Path.Combine(
            Environment.GetEnvironmentVariable("USERPROFILE"),
            ".zetris"
        );

        static readonly string FilePath = Path.Combine(UserPath, "Zetris.config");

        static readonly int Version = 0;
        
        static int _style = 0;
        public static int Style {
            get => _style;
            set {
                _style = Math.Max(0, Math.Min(2, value));
                Save();
            }
        }

        static int _speed = 100;
        public static int Speed {
            get => _speed;
            set {
                _speed = Math.Max(10, Math.Min(100, value));
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
                Save();
            }
        }

        static int _player = 1;
        public static int Player {
            get => _player;
            set {
                _player = Math.Max(0, Math.Min(3, value));
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

        static void Save() {
            if (!Directory.Exists(UserPath)) Directory.CreateDirectory(UserPath);

            try {
                MemoryStream output = new MemoryStream();

                using (BinaryWriter writer = new BinaryWriter(output)) {
                    writer.Write(new char[] {'Z', 'E', 'T', 'R'});
                    writer.Write(Version);

                    writer.Write(Style);
                    writer.Write(Speed);
                    writer.Write(PerfectClear);
                    writer.Write(C4W);
                    writer.Write(Player);
                }

                File.WriteAllBytes(FilePath, output.ToArray());
            } catch (IOException) {}
        }

        static Preferences() {
            bool success = false;

            if (File.Exists(FilePath))
                using (FileStream file = File.Open(FilePath, FileMode.Open, FileAccess.Read))
                    try {
                        using (BinaryReader reader = new BinaryReader(file)) {
                            if (!reader.ReadChars(4).SequenceEqual(new char[] {'Z', 'E', 'T', 'R'})) throw new InvalidDataException();

                            int version = reader.ReadInt32();

                            if (version > Version) throw new InvalidDataException();

                            Style = reader.ReadInt32();
                            Speed = reader.ReadInt32();
                            PerfectClear = reader.ReadBoolean();
                            C4W = reader.ReadBoolean();
                            Player = reader.ReadInt32();
                        }
                    } catch {}

            if (!success) Save();
        }
    }
}
