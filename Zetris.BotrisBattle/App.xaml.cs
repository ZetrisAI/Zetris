using System;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows;

namespace Zetris.BotrisBattle {
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
                if (e.Args[i].StartsWith("--token=")) {
                    BotrisBattleBot.Instance.Token = e.Args[i].Substring(8);

                } else if (e.Args[i].StartsWith("--roomKey=")) {
                    BotrisBattleBot.Instance.RoomKey = e.Args[i].Substring(10);
                }
            }
        }

        void Exiting(object sender, ExitEventArgs e) => BotrisBattleBot.Instance.Dispose();
    }
}
