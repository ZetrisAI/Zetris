﻿using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Zetris.TETRIO {
    partial class UI: IUI {
        static bool FreezeEvents = true;

        static string InactiveString, ActiveString, ConfidenceString, ThinkingTimeString;

        Editor Editor = null;

        public UI() {
            InitializeComponent();

            Speed.CustomToValue = v => {     // https://www.desmos.com/calculator/srv6d67o19
                if (v < 1 / 15) return 45 * v * v;
                else if (v < 0.1) return (45 * v - 2) / 5;
                else if (v < 0.15) return (60 * v - 1) / 10;
                else return 1 - (Math.Pow(60 * (1 - v) / 51, 2) / 5);
            };
            Speed.CustomToRawValue = v => {  // https://www.desmos.com/calculator/iik6yqqmde
                if (v < 0.2) return Math.Sqrt(5 * v) / 15;
                else if (v < 0.5) return (5 * v + 2) / 45;
                else if (v < 0.8) return (10 * v + 1) / 60;
                else return 1 - (51 * Math.Sqrt(5 - 5 * v) / 60);
            };
            PCThreads.Maximum = TetrioBot.PCThreadsMaximum;

            FreezeEvents = false;

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
            AccurateSync.IsChecked = Preferences.AccurateSync;
            ChatCommands.IsChecked = Preferences.ChatCommands;

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
                    AllSpins.Content = "올스핀";
                    AccurateSync.Content = "게임싱크보정";
                    ChatCommands.Content = "채팅 명령 처리";
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
                    AccurateSync.Content = "同期の最適化";
                    ChatCommands.Content = "チャットコマンドの処理";
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
                    AccurateSync.Content = "Accurate Sync";
                    AccurateSync.ToolTip = "If this is enabled, Zetris will waste a CPU core trying to perfectly match the target PPS.\n" +
                                           "Uncheck this only if your computer has performance issues while running Zetris.";
                    ChatCommands.Content = "Process Chat Commands";
                    break;
            }

            UpdateTSDOnlyText();

            UpdateActive();
            TetrioBot.Instance.Start(this);
        }

        void UpdateTSDOnlyText() {
            switch (CultureInfo.CurrentCulture.TwoLetterISOLanguageName) {
                case "ko":
                    TSDOnly.Content = Preferences.AllSpins? "올스핀 싱글만" : "TSD만 (20 TSD)";
                    break;

                case "ja":
                    TSDOnly.Content = Preferences.AllSpins? "All-Spin Single のみ" : "TSDのみ (TSD20発用)";
                    break;

                default:
                    TSDOnly.Content = Preferences.AllSpins? "All-Spin Single Only" : "TSD Only (for 20 TSD)";
                    break;
            }
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

        public DispatcherOperation SetSpeed(double speed)
            => Dispatcher.InvokeAsync(() => {
                Speed.RawValue = speed;
                Preferences.Speed = Speed.RawValue;
            });

        public DispatcherOperation SetPerfectClear(bool pcf)
            => Dispatcher.InvokeAsync(() => {
                Preferences.PerfectClear = (PerfectClear.IsChecked = pcf).Value;
                Preferences.EnhancePerfect = (EnhancePerfect.IsChecked = pcf).Value;
            });
        
        public DispatcherOperation SetAllSpins(bool allspins)
            => Dispatcher.InvokeAsync(() => {
                Preferences.AllSpins = (AllSpins.IsChecked = allspins).Value;
                UpdateTSDOnlyText();
            });

        public void SetPortTitle(bool http, ushort port)
            => Dispatcher.InvokeAsync(() => Title = $"Zetris [{(http? "HTTP" : "WS")} {port}]");

        void UpdateActive() {
            State.Text = Active? ActiveString : InactiveString;
            Edit.IsEnabled = Style.IsEnabled = Speed.Enabled = Previews.Enabled = Intelligence.Enabled = HoldAllowed.IsEnabled = PerfectClear.IsEnabled = EnhancePerfect.IsEnabled = PCThreads.Enabled = C4W.IsEnabled = AllSpins.IsEnabled = TSDOnly.IsEnabled = !Active;

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

        void SpeedChanged(Dial sender, double NewValue) {
            if (!FreezeEvents) Preferences.Speed = Speed.RawValue;
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
            UpdateTSDOnlyText();
        }

        void AccurateSyncChanged(object sender, RoutedEventArgs e) {
            if (!FreezeEvents) Preferences.AccurateSync = AccurateSync.IsChecked == true;
        }

        void ChatCommandsChanged(object sender, RoutedEventArgs e) {
            if (!FreezeEvents) Preferences.ChatCommands = ChatCommands.IsChecked == true;
        }
    }
}
