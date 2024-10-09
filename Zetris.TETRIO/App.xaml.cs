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

            if (e.Args.Length == 1) {
                if (ushort.TryParse(e.Args[0], out ushort port)) {
                    TetrioBot.Instance.Port = port;
                
                } else if (e.Args[0] == "--zetrio") {
                    TetrioBot.Instance.IsZETRIO = true;
                }
            }
        }

        void Exiting(object sender, ExitEventArgs e) => TetrioBot.Instance.Dispose();
    }
}
