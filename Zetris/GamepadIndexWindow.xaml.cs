using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Zetris {
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
            ShowDialog();
            return result;
        }

        private void Resolve(object sender, RoutedEventArgs e) {
            result = Root.Children.IndexOf((Button)sender) + 1;
            Close();
        }
    }
}
