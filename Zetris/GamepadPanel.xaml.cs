using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using ScpDriverInterface;

namespace Zetris {
    public partial class GamepadPanel: UserControl {
        public string RestoreText {
            set => Restore.Content = value;
        }

        Dictionary<string, X360Buttons> map = new Dictionary<string, X360Buttons>() {
            {"Up", X360Buttons.Up},
            {"Down", X360Buttons.Down},
            {"Left", X360Buttons.Left},
            {"Right", X360Buttons.Right},
            {"Y", X360Buttons.Y},
            {"A", X360Buttons.A},
            {"X", X360Buttons.X},
            {"B", X360Buttons.B},
            {"L", X360Buttons.LeftBumper},
            {"S", X360Buttons.Start},
            {"R", X360Buttons.RightBumper}
        };

        public GamepadPanel() {
            InitializeComponent();
        }

        void ClickButton(object sender, RoutedEventArgs e) {
            if (!(sender is Button btn)) return;
            Bot.ManualInput(map[btn.Name], WPFISGAYANDICANTHIDELEMENTS);
        }

        void RestoreClicked(object sender, RoutedEventArgs e) => Bot.RestoreManual();
    }
}
