using System.Globalization;
using System.Windows.Threading;

namespace Zetris.BotrisBattle {
    partial class UI: IUI {
        static bool FreezeEvents = true;

        static string InactiveString, ActiveString, ConfidenceString, ThinkingTimeString;

        public UI() {
            InitializeComponent();

            PCThreads.Maximum = BotrisBattleBot.PCThreadsMaximum;

            FreezeEvents = false;

            PCThreads.RawValue = Preferences.PCThreads;

            Version.Text = App.Version;

            switch (CultureInfo.CurrentCulture.TwoLetterISOLanguageName) {
                case "ko":
                    InactiveString = "비활성화";
                    ActiveString = "활성화";
                    ConfidenceString = "자신:";
                    ThinkingTimeString = "생각 하는시간:";
                    PCThreads.Title = "스레드:";
                    break;
                    
                case "ja":
                    InactiveString = "停止";
                    ActiveString = "動作中";
                    ConfidenceString = "自信:";
                    ThinkingTimeString = "思考時間:";
                    PCThreads.Title = "スレッド:";
                    break;
                    
                default:
                    InactiveString = "Inactive";
                    ActiveString = "Active";
                    ConfidenceString = "Confidence:";
                    ThinkingTimeString = "Thinking Time:";
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
            PCThreads.Enabled = !Active;

            if (!Active) Info.MaxHeight = 0;
        }

        void PCThreadsChanged(Dial sender, double NewValue) {
            if (!FreezeEvents) Preferences.PCThreads = (uint)PCThreads.RawValue;
        }
    }
}
