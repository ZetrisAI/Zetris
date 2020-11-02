using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;

namespace Zetris.PPT {
    public partial class App {
        public static readonly string Version = $"Zetris-{Assembly.GetExecutingAssembly().GetName().Version.Minor}";

        bool DetectMissingDriver(object ex) {
            if (ex == null || !(ex is Exception e)) return false;

            if (e is IOException && e.Message == "SCP Virtual Bus Device not found" && e.Source == "ScpDriverInterface") return true;
            return DetectMissingDriver(e.InnerException);
        }

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
                if (!e.Args.Contains("--nodriverinstall") && DetectMissingDriver(ex.ExceptionObject)) {

                    Process.Start(new ProcessStartInfo("ScpDriverInstaller.exe", "--quiet --install") {
                        WorkingDirectory = "ScpDriver"
                    }).WaitForExit();

                    Process.Start("Zetris.exe", "--nodriverinstall");

                } else new Error(ex.ExceptionObject.ToString()).ShowDialog();
                
                Current.Shutdown();
            };
#endif
        }

        void Exiting(object sender, ExitEventArgs e) => Bot.Dispose();
    }
}
