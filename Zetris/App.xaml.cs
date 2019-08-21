using System.Windows;

namespace Zetris {
    public partial class App {
        private void Main(object sender, StartupEventArgs e) => Bot.Args = e.Args;
    }
}
