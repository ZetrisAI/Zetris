using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MahApps.Metro.Controls;

namespace Zetris {
    public partial class UI {
        static string InactiveString, ActiveString;

        public UI() {
            Preferences.Freeze = true;

            InitializeComponent();

            Preferences.Freeze = false;

            Style.SelectedIndex = Preferences.Style;
            Speed.RawValue = Preferences.Speed;
            Previews.RawValue = Preferences.Previews;
            PerfectClear.IsChecked = Preferences.PerfectClear;
            C4W.IsChecked = Preferences.C4W;
            Player.RawValue = Preferences.Player + 1;

#if PUBLIC
            ((StackPanel)Auto.Parent).Children.Remove(Auto);
#endif

            switch (CultureInfo.CurrentCulture.TwoLetterISOLanguageName) {
                case "ko":
                    InactiveString = "비활성화";
                    ActiveString = "활성화";
                    StyleText.Text = "스타일:";
                    StyleTspin.Content = "티스핀+";
                    StyleNoHold.Content = "노홀드";
                    Speed.Title = "플레이 속도:";
                    Previews.Title = "";
                    PerfectClear.Content = "퍼펙트 클리어 모드";
                    C4W.Content = "센터 포와이드";
                    Player.Title = "멀티아케이드:";
                    Gamepad.Content = "게임패드 연결";
                    break;
                    
                case "ja":
                    InactiveString = "停止";
                    ActiveString = "動作中";
                    StyleText.Text = "立ち回り:";
                    StyleTspin.Content = "Tスピン（強）";
                    StyleNoHold.Content = "ホールドなし";
                    Speed.Title = "速度:";
                    Previews.Title = "";
                    PerfectClear.Content = "パフェ発見機";
                    C4W.Content = "中開けREN";
                    Player.Title = "ドリームアーケード みんなで:";
                    Gamepad.Content = "コントローラー接続中";
                    break;
                    
                default:
                    InactiveString = "Inactive";
                    ActiveString = "Active";
                    StyleText.Text = "Style:";
                    StyleTspin.Content = "T-Spin+";
                    StyleNoHold.Content = "No Hold";
                    Speed.Title = "Speed:";
                    Previews.Title = "Previews:";
                    PerfectClear.Content = "Perfect Clear Finder";
                    C4W.Content = "Center 4-Wide";
                    Player.Title = "MP Arcade Player:";
                    Gamepad.Content = "Gamepad Connected";
                    break;
            }

            UpdateActive();
            Bot.Start(this);
        }

        bool Active = false;
        public void SetActive(bool state) {
            if (Active != state) {
                Active = state;

                Dispatcher.InvokeAsync(() => UpdateActive());
            }
        }

        void UpdateActive() {
            State.Text = Active? ActiveString : InactiveString;
            Style.IsEnabled = Speed.Enabled = Previews.Enabled = PerfectClear.IsEnabled = C4W.IsEnabled = Player.Enabled = !Active;
        }

        public void SetGamepadIndex(int index) =>
            Title = $"Zetris [{index}]";

        void StyleChanged(object sender, SelectionChangedEventArgs e) =>
            Preferences.Style = Style.SelectedIndex;

        void SpeedChanged(double NewValue) =>
            Preferences.Speed = (int)Speed.RawValue;

        void PreviewsChanged(double NewValue) =>
            Preferences.Previews = (int)Previews.RawValue;

        void PerfectClearChanged(object sender, RoutedEventArgs e) =>
            Preferences.PerfectClear = PerfectClear.IsChecked == true;

        void C4WChanged(object sender, RoutedEventArgs e) =>
            Preferences.C4W = C4W.IsChecked == true;

        void PlayerChanged(double NewValue) =>
            Preferences.Player = (int)Player.RawValue - 1;
        
        void AutoChanged(object sender, RoutedEventArgs e) =>
            Preferences.Auto = Auto.IsChecked == true;

        void GamepadChanged(object sender, RoutedEventArgs e) =>
            Bot.SetGamepad(Gamepad.IsChecked == true);
    }
}
