using System;
using System.Collections.Generic;
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
        public UI() {
            InitializeComponent();

            Style.SelectedIndex = Preferences.Style;
            Speed.RawValue = Preferences.Speed;
            PerfectClear.IsChecked = Preferences.PerfectClear;
            C4W.IsChecked = Preferences.C4W;
            Player.RawValue = Preferences.Player + 1;
        }

        void StyleChanged(object sender, SelectionChangedEventArgs e)
            => Preferences.Style = Style.SelectedIndex;

        void SpeedChanged(double NewValue)
            => Preferences.Speed = (int)Speed.RawValue;

        void PerfectClearChanged(object sender, RoutedEventArgs e)
            => Preferences.PerfectClear = PerfectClear.IsChecked == true;

        void C4WChanged(object sender, RoutedEventArgs e)
            => Preferences.C4W = C4W.IsChecked == true;

        void PlayerChanged(double NewValue)
            => Preferences.Player = (int)Player.RawValue - 1;
    }
}
