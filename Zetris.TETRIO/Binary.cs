using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using MisaMinoNET;

namespace Zetris.TETRIO {
    static class Binary {
        static readonly int Version = 2;

        static byte[] CreateHeader() => Encoding.ASCII.GetBytes("ZEIO").Concat(BitConverter.GetBytes(Version)).ToArray();

        static int DecodeHeader(BinaryReader reader) {
            if (!reader.ReadChars(4).SequenceEqual(new char[] { 'Z', 'E', 'I', 'O' })) throw new InvalidDataException();
            int version = reader.ReadInt32();

            if (version > Version) throw new InvalidDataException();

            return version;
        }

        public static MemoryStream EncodePreferences() {
            MemoryStream output = new MemoryStream();

            using (BinaryWriter writer = new BinaryWriter(output)) {
                writer.Write(CreateHeader());

                writer.Write(Preferences.Styles.Count);
                foreach (Style style in Preferences.Styles)
                    WriteStyle(writer, style);

                writer.Write(Preferences.StyleIndex);
                writer.Write(Preferences.Speed);
                writer.Write(Preferences.Previews);
                writer.Write(Preferences.Intelligence);
                writer.Write(Preferences.HoldAllowed);
                writer.Write(Preferences.PerfectClear);
                writer.Write(Preferences.EnhancePerfect);
                writer.Write(Preferences.PCThreads);
                writer.Write(Preferences.C4W);
                writer.Write(Preferences.AllSpins);
                writer.Write(Preferences.TSDOnly);
                writer.Write(Preferences.AccurateSync);
                writer.Write(Preferences.ChatCommands);
            }

            return output;
        }

        public static void DecodePreferences(FileStream input) {
            using (BinaryReader reader = new BinaryReader(input)) {
                int version = DecodeHeader(reader);

                if (version >= 2) // Set this to latest version if changing default styles
                    Preferences.Styles = new List<Style>();

                int count = reader.ReadInt32();
                for (int i = 0; i < count; i++) {
                    Style style = ReadStyle(reader, version);

                    if (Preferences.Styles.Select(x => x.Name).Contains(style.Name))
                        continue;

                    Preferences.Styles.Add(style);
                }

                Preferences.StyleIndex = reader.ReadInt32();
                Preferences.Speed = reader.ReadDouble();
                Preferences.Previews = reader.ReadInt32();
                Preferences.Intelligence = reader.ReadInt32();
                Preferences.HoldAllowed = reader.ReadBoolean();
                Preferences.PerfectClear = reader.ReadBoolean();
                Preferences.EnhancePerfect = reader.ReadBoolean();
                Preferences.PCThreads = reader.ReadUInt32();
                Preferences.C4W = reader.ReadBoolean();
                Preferences.AllSpins = reader.ReadBoolean();
                Preferences.TSDOnly = reader.ReadBoolean();

                if (version >= 2)
                    Preferences.AccurateSync = reader.ReadBoolean();

                Preferences.ChatCommands = reader.ReadBoolean();
            }
        }

        static void WriteStyle(BinaryWriter writer, Style style) {
            writer.Write(style.Name);
            writer.Write(style.Parameters.ToArray().Take(21).SelectMany(BitConverter.GetBytes).ToArray());
        }

        static Style ReadStyle(BinaryReader reader, int version) {
            string name = reader.ReadString();

            int size = 21;
            if (version <= 1) {
                size = 20;
            }

            byte[] bytes = reader.ReadBytes(size * 4);
            int[] param = new int[22];

            for (int j = 0; j < size; j++)
                param[j] = BitConverter.ToInt32(bytes, j * 4);

            if (version <= 1) {
                param[20] = 0;
            }

            return new Style(name, MisaMinoParameters.FromArray(param));
        }

        public static MemoryStream EncodeStyle(Style style) {
            MemoryStream output = new MemoryStream();

            using (BinaryWriter writer = new BinaryWriter(output)) {
                writer.Write(CreateHeader());

                WriteStyle(writer, style);
            }

            return output;
        }

        public static Style DecodeStyle(FileStream input) {
            using (BinaryReader reader = new BinaryReader(input)) {
                int version = DecodeHeader(reader);

                return ReadStyle(reader, version);
            }
        }
    }
}
