using System;
using System.Diagnostics;

namespace Zetris.PPT {
    static class LogHelper {
        [Conditional("VERBOSE")]
        public static void LogText(string text) {
            Console.WriteLine(text);
        }

        [Conditional("VERBOSE")]
        public static void LogBoard(params int[][,] boards) {
            for (int y = 30; y >= 0; y--) {
                string[] o = new string[boards.Length];

                for (int i = 0; i < boards.Length; i++) {
                    for (int x = 0; x < 10; x++) {
                        o[i] += boards[i][x, y] >= 255 ? "." : boards[i][x, y].ToString();
                    }
                }

                Console.WriteLine(String.Join("   ", o));
            }
        }
    }
}
