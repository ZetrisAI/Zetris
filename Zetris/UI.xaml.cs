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
            InitializeComponent();

            Preferences.Freeze = false;

            Player.RawValue = Preferences.Player + 1;

#if PUBLIC
            ((StackPanel)Auto.Parent).Children.Remove(Auto);
#endif

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

            UpdateActive();

            Zetris.Style.Current = Preferences.Styles[0];
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

        void PlayerChanged(double NewValue) =>
            Preferences.Player = (int)Player.RawValue - 1;
        
        void AutoChanged(object sender, RoutedEventArgs e) =>
            Preferences.Auto = Auto.IsChecked == true;

        void GamepadChanged(object sender, RoutedEventArgs e) =>
            Bot.SetGamepad(Gamepad.IsChecked == true);
    }
}
