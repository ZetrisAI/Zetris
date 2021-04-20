using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Zetris.PPT.Memory;

namespace Zetris.PPT {
    public partial class UI: IUI {
        static bool FreezeEvents = true;

        static string InactiveString, ActiveString, ConfidenceString, ThinkingTimeString;

        Editor Editor = null;

        public UI() {
            InitializeComponent();

            PCThreads.Maximum = Preferences.PCThreadsMaximum;

            FreezeEvents = false;

            int? gamepadIndex = null;
            
            if (Keyboard.IsKeyDown(Key.LeftShift) && (gamepadIndex = new GamepadIndexWindow().Ask()).HasValue)
                Title = $"Zetris [{gamepadIndex}]";

            foreach (Style style in Preferences.Styles)
                Style.Items.Add(style);

            Style.SelectedIndex = Preferences.StyleIndex;

            Speed.RawValue = Preferences.Speed;
            Previews.RawValue = Preferences.Previews;
            Intelligence.RawValue = Preferences.Intelligence;
            HoldAllowed.IsChecked = Preferences.HoldAllowed;
            PerfectClear.IsChecked = Preferences.PerfectClear;
            EnhancePerfect.IsChecked = Preferences.EnhancePerfect;
            PCThreads.RawValue = Preferences.PCThreads;
            C4W.IsChecked = Preferences.C4W;
            AllSpins.IsChecked = Preferences.AllSpins;
            TSDOnly.IsChecked = Preferences.TSDOnly;
            Player.RawValue = Preferences.Player + 1;
            AccurateSync.IsChecked = Preferences.AccurateSync;
            Game.SelectedIndex = Preferences.Game;

            UpdateGame();

#if PUBLIC
            DevPanel.IsEnabled = false;
            ((StackPanel)DevPanel.Parent).Children.Remove(DevPanel);
#endif

            Version.Text = App.Version;

            switch (CultureInfo.CurrentCulture.TwoLetterISOLanguageName) {
                case "ko":
                    InactiveString = "비활성화";
                    ActiveString = "활성화";
                    ConfidenceString = "자신:";
                    ThinkingTimeString = "생각 하는시간:";
                    StyleText.Text = "스타일:";
                    Edit.Content = "플레이 스타일 변경";
                    Speed.Title = "플레이 속도:";
                    Previews.Title = "넥스트 보기 사이즈:";
                    Intelligence.Title = "지능:";
                    PerfectClear.Content = "퍼펙트 클리어 모드";
                    EnhancePerfect.Content = "강화 퍼펙트클리어 공격";
                    PCThreads.Title = "스레드:";
                    HoldAllowed.Content = "홀드 사용";
                    C4W.Content = "센터 포와이드";
                    AllSpins.Content = "올 스핀";
                    TSDOnly.Content = "TSD만 (20 TSD)";
                    Player.Title = "멀티아케이드:";
                    Gamepad.Content = "게임패드 연결";
                    AccurateSync.Content = "게임싱크보정";
                    AccurateSync.ToolTip = "이 항목을 활성화하면, 제트리스는 CPU 사용량을 증가시켜 프레임 스킵없이\n" +
                                           "계속 다음 프레임을 확인합니다.\n" +
                                           "이 항목은 제트리스를 실행할 때 성능문제가 발생할 경우에만 해제해주세요.";
                    GamepadPanel.RestoreText = "입력 복구";
                    Game.Items.Add("뿌요뿌요™테트리스®");
                    Game.Items.Add("뿌요뿌요™테트리스®２");
                    break;
                    
                case "ja":
                    InactiveString = "停止";
                    ActiveString = "動作中";
                    ConfidenceString = "自信:";
                    ThinkingTimeString = "思考時間:";
                    StyleText.Text = "立ち回り:";
                    Edit.Content = "詳細設定";
                    Speed.Title = "速度:";
                    Previews.Title = "ネクスト可視数:";
                    Intelligence.Title = "知能:";
                    PerfectClear.Content = "パフェ発見機";
                    EnhancePerfect.Content = "火力優先のパフェルートを選択";
                    PCThreads.Title = "スレッド:";
                    HoldAllowed.Content = "ホールド使用";
                    C4W.Content = "中開けREN";
                    AllSpins.Content = "特殊回転テトリス";
                    TSDOnly.Content = "TSDのみ (TSD20発用)";
                    Player.Title = "ドリームアーケード みんなで:";
                    Gamepad.Content = "コントローラー接続中";
                    AccurateSync.Content = "同期の最適化";
                    AccurateSync.ToolTip = "ここにチェックをいれると、ZetrisのCPU使用率をあげてフレームスキップを防ぎます。\n" +
                                           "チェックありの方が強いですが、パソコンが重い場合はチェックを外してください";
                    GamepadPanel.RestoreText = "操作権を返す";
                    Game.Items.Add("ぷよぷよ™テトリス®");
                    Game.Items.Add("ぷよぷよ™テトリス®２");
                    break;
                    
                default:
                    InactiveString = "Inactive";
                    ActiveString = "Active";
                    ConfidenceString = "Confidence:";
                    ThinkingTimeString = "Thinking Time:";
                    StyleText.Text = "Style:";
                    Edit.Content = "Edit Styles";
                    Speed.Title = "Speed:";
                    Previews.Title = "Previews:";
                    Intelligence.Title = "Intelligence:";
                    HoldAllowed.Content = "Hold Allowed";
                    PerfectClear.Content = "Perfect Clear Finder";
                    EnhancePerfect.Content = "Enhance Perfect Clear Attack";
                    PCThreads.Title = "Threads:";
                    C4W.Content = "Center 4-Wide";
                    AllSpins.Content = "All Spins";
                    TSDOnly.Content = "TSD Only (for 20 TSD)";
                    Player.Title = "MP Arcade Player:";
                    Gamepad.Content = "Gamepad Connected";
                    AccurateSync.Content = "Accurate Game Sync";
                    AccurateSync.ToolTip = "If this is enabled, Zetris will constantly scan for the next game frame to help\n" +
                                           "prevent frame skipping at the cost of increased CPU usage.\n" +
                                           "Uncheck this only if your computer has performance issues while running Zetris.";
                    GamepadPanel.RestoreText = "Restore Inputs";
                    Game.Items.Add("Puyo Puyo™ Tetris®");
                    Game.Items.Add("Puyo Puyo™ Tetris® 2");
                    break;
            }

            UpdateActive();

            PPTBot.Instance.Start(this, gamepadIndex?? 4);
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
            Edit.IsEnabled = Style.IsEnabled = Speed.Enabled = Previews.Enabled = Intelligence.Enabled = HoldAllowed.IsEnabled = PerfectClear.IsEnabled = EnhancePerfect.IsEnabled = PCThreads.Enabled = C4W.IsEnabled = AllSpins.IsEnabled = TSDOnly.IsEnabled = Player.Enabled = !Active;

            if (Active) Editor?.Close();
            else Info.MaxHeight = 0;
        }

        void UpdateGame() {
            NoPPT2Panel.Height = (NoPPT2Panel.IsEnabled = (Preferences.Game != 1))? double.NaN : 0;
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

        void SpeedChanged(Dial sender, double NewValue) {
            if (!FreezeEvents) Preferences.Speed = (int)Speed.RawValue;
        }

        void PreviewsChanged(Dial sender, double NewValue) {
            if (!FreezeEvents) Preferences.Previews = (int)Previews.RawValue;
        }

        void IntelligenceChanged(Dial sender, double NewValue) {
            if (!FreezeEvents) Preferences.Intelligence = (int)Intelligence.RawValue;
        }

        void HoldAllowedChanged(object sender, RoutedEventArgs e) {
            if (!FreezeEvents) Preferences.HoldAllowed = HoldAllowed.IsChecked == true;
        }

        void PerfectClearChanged(object sender, RoutedEventArgs e) {
            if (!FreezeEvents) Preferences.PerfectClear = PerfectClear.IsChecked == true;
        }

        void EnhancePerfectChanged(object sender, RoutedEventArgs e) {
            if (!FreezeEvents) Preferences.EnhancePerfect = EnhancePerfect.IsChecked == true;
        }

        void PCThreadsChanged(Dial sender, double NewValue) {
            if (!FreezeEvents) Preferences.PCThreads = (uint)PCThreads.RawValue;
        }

        void C4WChanged(object sender, RoutedEventArgs e) {
            if (!FreezeEvents) Preferences.C4W = C4W.IsChecked == true;
        }

        void TSDOnlyChanged(object sender, RoutedEventArgs e) {
            if (!FreezeEvents) Preferences.TSDOnly = TSDOnly.IsChecked == true;
        }

        void AllSpinsChanged(object sender, RoutedEventArgs e) {
            if (!FreezeEvents) Preferences.AllSpins = AllSpins.IsChecked == true;
        }

        void PuzzleLeagueChanged(object sender, RoutedEventArgs e) {
            if (!FreezeEvents) Preferences.PuzzleLeague = PuzzleLeague.IsChecked == true;
        }

        void SaveReplayChanged(object sender, RoutedEventArgs e) {
            if (!FreezeEvents) Preferences.SaveReplay = SaveReplay.IsChecked == true;
        }

        void KickExploit(object sender, RoutedEventArgs e) {
            #if !PUBLIC
                if (GameHelper.CheckProcess() && GameHelper.Instance is Memory.PPT && GameHelper.Instance.GetMenu.Call() == 27)
                    Process.Start($"steam://joinlobby/{GameHelper.GameID}/{GameHelper.Instance.LobbyID.Call()}/");  // patched for ppt2 :(
            #endif
        }

        void SpamAChanged(object sender, RoutedEventArgs e) {
            if (!FreezeEvents) Preferences.SpamA = SpamA.IsChecked == true;
        }

        void PlayerChanged(Dial sender, double NewValue) {
            if (!FreezeEvents) Preferences.Player = (int)Player.RawValue - 1;
        }

        void GamepadChanged(object sender, RoutedEventArgs e) {
            if (!FreezeEvents) {
                FreezeEvents = true;
                Gamepad.IsChecked = PPTBot.Instance.SetGamepad(Gamepad.IsChecked == true);
                FreezeEvents = false;
            }
        }

        void AccurateSyncChanged(object sender, RoutedEventArgs e) {
            if (!FreezeEvents) Preferences.AccurateSync = AccurateSync.IsChecked == true;
        }

        void GameChanged(object sender, SelectionChangedEventArgs e) {
            if (!FreezeEvents) Preferences.Game = Game.SelectedIndex;
            UpdateGame();
        }
    }
}
