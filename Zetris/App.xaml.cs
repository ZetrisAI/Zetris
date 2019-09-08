using System.Globalization;
using System.Threading;
using System.Windows;

namespace Zetris {
    public partial class App {
#if !PUBLIC
        void OverrideLocale(string locale) =>
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo(locale);
#endif

        void Main(object sender, StartupEventArgs e) {
            Bot.Args = e.Args;

#if !PUBLIC
            OverrideLocale("en-US");
#endif
        }

        void Exiting(object sender, ExitEventArgs e) => Bot.Dispose();
    }
}
