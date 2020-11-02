using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Zetris.PPT {
    public partial class GamepadIndexWindow {
        int? result = null;

        public GamepadIndexWindow() {
            InitializeComponent();

            switch (CultureInfo.CurrentCulture.TwoLetterISOLanguageName) {
                case "ko":
                    Title = "Zetris - 게임패드 인덱스선택";
                    break;
                    
                case "ja":
                    Title = "Zetris - コントローラー番号を選択";
                    break;
                   
                default:
                    Title = "Zetris - Select Gamepad Index";
                    break;
            }
        }

        public int? Ask() {
            Dispatcher.Invoke(() => {
                Topmost = true;
                Activate();
                Focus();
            });
            ShowDialog();
            return result;
        }

        private void Resolve(object sender, RoutedEventArgs e) {
            result = Root.Children.IndexOf((Button)sender) + 1;
            Close();
        }
    }
}
