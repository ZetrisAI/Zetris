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
            Version.ToolTip = $"Based on Zetris-{Assembly.GetExecutingAssembly().GetName().Version.Minor}";

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

                    SaltyCopy.Content = "Copy to Clipboard";

                    Gamepad.Content = "Gamepad Connected";
                    break;
            }

            UpdateActive();
            SaltyController.Ready(this);

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

            League.IsEnabled = !Active && !SaltyController.Active;

            if (!Active) Info.MaxHeight = 0;
        }

        public void UpdateSaltyInfo() {
            Dispatcher.InvokeAsync(() => {
                League.IsEnabled = !Active && !SaltyController.Active;
                SaltyInfoPanel.MaxHeight = (SaltyController.Active || SaltyController.Time > 0)? double.PositiveInfinity : 0;

                SaltySettings.Text = $"{SaltyController.Speed}% / {SaltyController.Previews}prev / {SaltyController.Intelligence}iq";
                SaltyScore.Text = $"[{SaltyController.PlayerName}] {SaltyController.GetScore(1)} - {SaltyController.GetScore(0)} [Zetris]";

                SaltyTimeContainer.MaxHeight = SaltyController.TimerRunning? 0 : double.PositiveInfinity;
                SaltyTime.Text = $"{(SaltyController.Time / 60000).ToString("00")}:{(SaltyController.Time / 1000 % 60).ToString("00")}.{(SaltyController.Time % 1000).ToString("000")}";

                SaltyCopyContainer.MaxHeight = (SaltyCopy.IsEnabled = (SaltyController.Time > 0 && !SaltyController.Active && (SaltyController.GetScore(0) == 6 || SaltyController.GetScore(1) == 6)))? double.PositiveInfinity : 0;
            });
        }

        void LeagueChanged(object sender, SelectionChangedEventArgs e) {
            if (!FreezeEvents) SaltyController.League = League.SelectedIndex;
        }

        void CopyClicked(object sender, RoutedEventArgs e) 
            => Clipboard.SetText($"[{SaltyController.PlayerName}] {SaltyController.GetScore(1)} - {SaltyController.GetScore(0)} [{League.Items[SaltyController.LeagueUsed]} Gatekeeper {SaltyController.GatekeeperName}]\r\n{SaltyTime.Text}");

        void GamepadChanged(object sender, RoutedEventArgs e) {
            if (!FreezeEvents) Bot.SetGamepad(Gamepad.IsChecked == true);
        }
    }
}
