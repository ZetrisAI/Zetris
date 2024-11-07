using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Zetris.BotrisBattle {
    partial class UI: IUI {
        static bool FreezeEvents = true;

        static string InactiveString, ActiveString, ConfidenceString, ThinkingTimeString;

        Editor Editor = null;

        public UI() {
            InitializeComponent();

            PCThreads.Maximum = BotrisBattleBot.PCThreadsMaximum;

            FreezeEvents = false;

            foreach (Style style in Preferences.Styles)
                Style.Items.Add(style);

            Style.SelectedIndex = Preferences.StyleIndex;

            Intelligence.RawValue = Preferences.Intelligence;
            PerfectClear.IsChecked = Preferences.PerfectClear;
            PCThreads.RawValue = Preferences.PCThreads;

            Version.Text = App.Version;

            switch (CultureInfo.CurrentCulture.TwoLetterISOLanguageName) {
                case "ko":
                    InactiveString = "비활성화";
                    ActiveString = "활성화";
                    ConfidenceString = "자신:";
                    ThinkingTimeString = "생각 하는시간:";
                    StyleText.Text = "스타일:";
                    Edit.Content = "플레이 스타일 변경";
                    Intelligence.Title = "지능:";
                    PerfectClear.Content = "퍼펙트 클리어 모드";
                    PCThreads.Title = "스레드:";
                    break;
                    
                case "ja":
                    InactiveString = "停止";
                    ActiveString = "動作中";
                    ConfidenceString = "自信:";
                    ThinkingTimeString = "思考時間:";
                    StyleText.Text = "立ち回り:";
                    Edit.Content = "詳細設定";
                    Intelligence.Title = "知能:";
                    PerfectClear.Content = "パフェ発見機";
                    PCThreads.Title = "スレッド:";
                    break;
                    
                default:
                    InactiveString = "Inactive";
                    ActiveString = "Active";
                    ConfidenceString = "Confidence:";
                    ThinkingTimeString = "Thinking Time:";
                    StyleText.Text = "Style:";
                    Edit.Content = "Edit Styles";
                    Intelligence.Title = "Intelligence:";
                    PerfectClear.Content = "Perfect Clear Finder";
                    PCThreads.Title = "Threads:";
                    break;
            }

            UpdateActive();
            BotrisBattleBot.Instance.Start(this);
        }

        bool _active;
        public bool Active {
            get => _active;
            set {
                if (_active == value) return;

                _active = value;
                Dispatcher.InvokeAsync(UpdateActive);
            }
        }

        public void SetConfidence(string confidence)
            => Dispatcher.InvokeAsync(() => {
                Confidence.Text = $"{ConfidenceString} {confidence}";
                Info.MaxHeight = double.PositiveInfinity;
            });

        public void SetThinkingTime(long time)
            => Dispatcher.InvokeAsync(() => {
                ThinkingTime.Text = $"{ThinkingTimeString} {time}ms";
                Info.MaxHeight = double.PositiveInfinity;
            });

        void UpdateActive() {
            State.Text = Active? ActiveString : InactiveString;
            Edit.IsEnabled = Style.IsEnabled = Intelligence.Enabled = PerfectClear.IsEnabled = PCThreads.Enabled = !Active;

            if (Active) Editor?.Close();
            else Info.MaxHeight = 0;
        }
        
        void StyleChanged(object sender, SelectionChangedEventArgs e) {
            if (!FreezeEvents) Preferences.StyleIndex = Style.SelectedIndex;
        }

        void EditClicked(object sender, RoutedEventArgs e) {
            (Editor = new Editor(this)).ShowDialog();

            FreezeEvents = true;

            Style.Items.Clear();

            foreach (Style style in Preferences.Styles)
                Style.Items.Add(style);

            Style.SelectedIndex = Preferences.StyleIndex;
            
            FreezeEvents = false;
        }

        void IntelligenceChanged(Dial sender, double NewValue) {
            if (!FreezeEvents) Preferences.Intelligence = (int)Intelligence.RawValue;
        }

        void PerfectClearChanged(object sender, RoutedEventArgs e) {
            if (!FreezeEvents) Preferences.PerfectClear = PerfectClear.IsChecked == true;
        }

        void PCThreadsChanged(Dial sender, double NewValue) {
            if (!FreezeEvents) Preferences.PCThreads = (uint)PCThreads.RawValue;
        }
    }
}
