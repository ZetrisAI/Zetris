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

        static readonly int Version = 2;

        public static bool Freeze = true;

        public static List<Style> Styles { get; private set; } = new List<Style>() {
            new Style("T-spin+")
        };

        static int _player = 1;
        public static int Player {
            get => _player;
            set {
                if (Freeze) return;

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
                if (Freeze) return;

                _auto = value;
            }
        }

        public static void Save() {
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
                        writer.Write(style.Speed);
                        writer.Write(style.Previews);
                        writer.Write(style.HoldAllowed);
                        writer.Write(style.PerfectClear);
                    }

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

                            if (version <= 1) return;

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
                                    MisaMinoParameters.FromArray(param),
                                    reader.ReadInt32(),
                                    reader.ReadInt32(),
                                    reader.ReadBoolean(),
                                    reader.ReadBoolean()
                                ));
                            }

                            Player = reader.ReadInt32();
                        }
                    } catch {}

            if (!success) Save();
        }
    }
}
