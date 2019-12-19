using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Zetris {
    public partial class GamepadIndexWindow {
        int? result = null;

        public GamepadIndexWindow() {
            InitializeComponent();

            switch (CultureInfo.CurrentCulture.TwoLetterISOLanguageName) {
                case "ko":
                    Title = "Zetris - ";
                    break;
                    
                case "ja":
                    Title = "Zetris - ";
                    break;
                   
                default:
                    Title = "Zetris - Select Gamepad Index";
                    break;
            }
        }

        public int? Ask() {
            ShowDialog();
            return result;
        }

        private void Resolve(object sender, RoutedEventArgs e) {
            result = Root.Children.IndexOf((Button)sender) + 1;
            Close();
        }
    }
}
