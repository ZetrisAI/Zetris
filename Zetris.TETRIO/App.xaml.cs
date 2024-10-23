using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;

namespace Zetris.TETRIO {
    public partial class App {
        public static readonly string Version = $"Zetris-{Assembly.GetExecutingAssembly().GetName().Version.Minor}";

#if !PUBLIC
        void OverrideLocale(string locale) =>
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo(locale);
#endif

        void Main(object sender, StartupEventArgs e) {
#if !PUBLIC
            OverrideLocale("en-US");
#endif

#if !DEBUG
            AppDomain.CurrentDomain.UnhandledException += (s, ex) => {
                new Error(ex.ExceptionObject.ToString()).ShowDialog();
                Current.Shutdown();
            };
#endif
            for (int i = 0; i < e.Args.Length; i++) {
                if (e.Args[i] == "--zetrio") {
                    TetrioBot.Instance.IsZETRIO = true;

                } else if (e.Args[i] == "--http") {
                    TetrioBot.Instance.UseLegacyHTTP = true;
                    TetrioBot.Instance.Port = 47326;

                    if (i + 1 < e.Args.Length && ushort.TryParse(e.Args[i + 1], out ushort port))
                        TetrioBot.Instance.Port = port;

                } else if (e.Args[i] == "--ws") {
                    if (i + 1 < e.Args.Length && ushort.TryParse(e.Args[i + 1], out ushort port))
                        TetrioBot.Instance.Port = port;

                } else if (i == 0 && ushort.TryParse(e.Args[0], out ushort port))
                    TetrioBot.Instance.Port = port;
            }
        }

        void Exiting(object sender, ExitEventArgs e) => TetrioBot.Instance.Dispose();
    }
}
