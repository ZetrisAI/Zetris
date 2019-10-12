using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zetris {
    static class LogHelper {
        public static void LogText(string text) {
            Console.WriteLine(text);
        }

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
