using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;

namespace Zetris {
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

            AppDomain.CurrentDomain.UnhandledException += (s, ex) => {
                new Error(ex.ExceptionObject.ToString()).ShowDialog();
                Current.Shutdown();
            };
        }

        void Exiting(object sender, ExitEventArgs e) => Bot.Dispose();
    }
}
