using System;
using System.Diagnostics;
using System.IO;

namespace Zetris {
    public static class LogHelper {
        static TextWriterTraceListener fileListener;

        [Conditional("VERBOSE")]
        public static void StartLoggingToFile(string id) {
            if (fileListener != null) return;

            var logDir = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "log"
            );
            Directory.CreateDirectory(logDir);

            var logFile = Path.Combine(
                logDir,
                $"{DateTime.Now:yyyyMMdd-HHmmss}{(string.IsNullOrWhiteSpace(id)? "" : $"-{id}")}.log"
            );
            fileListener = new TextWriterTraceListener(logFile);
            Trace.Listeners.Add(fileListener);
            Trace.AutoFlush = true;
        }

        [Conditional("VERBOSE")]
        public static void StopLoggingToFile() {
            if (fileListener == null) return;

            fileListener.Close();
            fileListener = null;
        }

        [Conditional("VERBOSE")]
        public static void LogText(string text) {
            Trace.WriteLine($"{DateTime.Now:yyyyMMdd-HHmmss.fff} {text}");
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

                LogText(string.Join("   ", o));
            }
        }
    }
}
