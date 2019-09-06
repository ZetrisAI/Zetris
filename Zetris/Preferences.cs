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

        static readonly int Version = 1;

        static bool Initialized = false;

        public static List<Style> Styles { get; private set; } = new List<Style>() {
            new Style("T-spin+")
        };

        static int _styleindex = 0;
        public static int StyleIndex {
            get => _styleindex;
            set {
                _styleindex = Math.Max(0, Math.Min(Styles.Count - 1, value));
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

        static int _previews = 18;
        public static int Previews {
            get => _previews;
            set {
                _previews = Math.Max(1, Math.Min(18, value));
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
                MemoryStream output = new MemoryStream();

                using (BinaryWriter writer = new BinaryWriter(output)) {
                    writer.Write(new char[] {'Z', 'E', 'T', 'R'});
                    writer.Write(Version);

                    writer.Write(Styles.Count);
                    foreach (Style style in Styles) {
                        writer.Write(style.Name);
                        writer.Write(style.Parameters.ToArray().SelectMany(BitConverter.GetBytes).ToArray());
                    }

                    writer.Write(StyleIndex);
                    writer.Write(Speed);
                    writer.Write(Previews);
                    writer.Write(HoldAllowed);
                    writer.Write(PerfectClear);
                    writer.Write(C4W);
                    writer.Write(Player);
                }

                File.WriteAllBytes(FilePath, output.ToArray());
            } catch (IOException) {}
        }

        static Preferences() {
            if (!File.Exists(FilePath)) return;

            using (FileStream file = File.Open(FilePath, FileMode.Open, FileAccess.Read))
                try {
                    using (BinaryReader reader = new BinaryReader(file)) {
                        if (!reader.ReadChars(4).SequenceEqual(new char[] {'Z', 'E', 'T', 'R'})) throw new InvalidDataException();

                        int version = reader.ReadInt32();

                        if (version > Version) throw new InvalidDataException();

                        if (version >= 1) {
                            Styles = new List<Style>();

                            int count = reader.ReadInt32();
                            for (int i = 0; i < count; i++) {
                                string name = reader.ReadString();

                                byte[] bytes = reader.ReadBytes(68);
                                int[] param = new int[17];

                                for (int j = 0; j < 17; j++)
                                    param[j] = BitConverter.ToInt32(bytes, j * 4);

                                Styles.Add(new Style(
                                    name,
                                    MisaMinoParameters.FromArray(param)
                                ));
                            }
                        } else reader.ReadInt32();

                        if (version >= 1)
                            StyleIndex = reader.ReadInt32();

                        Speed = reader.ReadInt32();

                        if (version >= 1) {
                            Previews = reader.ReadInt32();
                            HoldAllowed = reader.ReadBoolean();
                        }

                        PerfectClear = reader.ReadBoolean();
                        C4W = reader.ReadBoolean();
                        Player = reader.ReadInt32();
                    }
                } catch {}

            Initialized = true;
        }
    }
}
