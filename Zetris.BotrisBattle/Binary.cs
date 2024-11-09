using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Zetris.BotrisBattle {
    static class Binary {
        static readonly int Version = 3;

        static byte[] CreateHeader() => Encoding.ASCII.GetBytes("ZEBB").Concat(BitConverter.GetBytes(Version)).ToArray();

        static int DecodeHeader(BinaryReader reader) {
            if (!reader.ReadChars(4).SequenceEqual(new char[] { 'Z', 'E', 'B', 'B' })) throw new InvalidDataException();
            int version = reader.ReadInt32();

            if (version > Version) throw new InvalidDataException();

            return version;
        }

        public static MemoryStream EncodePreferences() {
            MemoryStream output = new MemoryStream();

            using (BinaryWriter writer = new BinaryWriter(output)) {
                writer.Write(CreateHeader());

                writer.Write(Preferences.PCThreads);
            }

            return output;
        }

        public static void DecodePreferences(FileStream input) {
            using (BinaryReader reader = new BinaryReader(input)) {
                int version = DecodeHeader(reader);

                if (version < 3) return;

                Preferences.PCThreads = reader.ReadUInt32();
            }
        }
    }
}
