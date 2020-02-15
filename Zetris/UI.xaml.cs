using System;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Zetris {
    public partial class UI {
        static bool FreezeEvents = true;

        static string InactiveString, ActiveString, ConfidenceString, ThinkingTimeString;

        public UI() {
            InitializeComponent();

            FreezeEvents = false;

            Version.Text = $"Zetris-S3-Gatekeeper";

            switch (CultureInfo.CurrentCulture.TwoLetterISOLanguageName) {
                default:
                    InactiveString = "Inactive";
                    ActiveString = "Active";
                    ConfidenceString = "Confidence:";
                    ThinkingTimeString = "Thinking Time:";

                    LeagueText.Text = "League:";
                    League.Items.Add("S/A");
                    League.Items.Add("B/C");
                    League.Items.Add("D/E");
                    League.Items.Add("G");
                    League.SelectedIndex = 0;

                    Gamepad.Content = "Gamepad Connected";
                    break;
            }

            UpdateActive();
            Bot.Start(this, 4);
        }

        bool Active = false;
        public void SetActive(bool state) {
            if (Active != state) {
                Active = state;

                Dispatcher.InvokeAsync(UpdateActive);
            }
        }

        public void SetConfidence(string confidence) {
            Dispatcher.InvokeAsync(() => {
                Confidence.Text = $"{ConfidenceString} {confidence}";
                Info.MaxHeight = double.PositiveInfinity;
            });
        }

        public void SetThinkingTime(long time) {
            Dispatcher.InvokeAsync(() => {
                ThinkingTime.Text = $"{ThinkingTimeString} {time}ms";
                Info.MaxHeight = double.PositiveInfinity;
            });
        }

        void UpdateActive() {
            State.Text = Active? ActiveString : InactiveString;

            if (!Active) Info.MaxHeight = 0;
        }

        void GamepadChanged(object sender, RoutedEventArgs e) {
            if (!FreezeEvents) Bot.SetGamepad(Gamepad.IsChecked == true);
        }

        void LeagueChanged(object sender, SelectionChangedEventArgs e) {
            if (!FreezeEvents) SaltyController.League = League.SelectedIndex;
        }
    }
}
