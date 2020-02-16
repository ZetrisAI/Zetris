using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Zetris {
    static class SaltyController {
        static UI Window = null;

        public static bool Active { get; private set; } = false;

        public static string PlayerName = "";
        public static string GatekeeperName = "";

        public static string Output = "";
        public static void CopyOutput() => Window?.Dispatcher.InvokeAsync(() => Clipboard.SetText(Output));

        public static string Current => $"G{Games} {score[1]}-{score[0]} {SettingsString}";

        static int[] score = new int[2] {0, 0};
        public static int GetScore(int index) => score[index];
        public static int Games => score.Sum();
        public static bool Finished => Games >= 6;

        static Stopwatch timer;
        public static long Time { get; private set; } = 0;
        public static string TimeString => $"{(Time / 60000).ToString("00")}:{(Time / 1000 % 60).ToString("00")}.{(Time % 1000).ToString("000")}";
        public static bool TimerRunning => timer?.IsRunning ?? false;
        
        static int[] SpeedMap = new int[4] {33, 25, 18, 12};
        static int spd;
        public static int Speed {
            get => Math.Max(10, spd);
            private set {
                spd = value;

                if (spd >= 29) Previews = 4;
                else if (spd >= 22) Previews = 2;
                else if (spd >= 10) Previews = 1;
                else Previews = 0;
                
                Intelligence = 20 * (spd - 4);
            }
        }

        public static int Previews { get; private set; }

        static int iq;
        public static int Intelligence {
            get => Math.Max(20, iq);
            private set {
                iq = Math.Min(100, value);
                Bot.UpdateConfig();
            }
        }

        public static string SettingsString => $"{Speed}% {Previews}Prev {Intelligence}Int";

        public static string[] Leagues = {"S/A", "B/C", "D/E", "G"};

        public static int LeagueUsed { get; private set; } = 0;
        public static int League;

        public static void GameStarted(bool match) {
            if (!match && Finished) return;

            if (!Active) {
                score = new int[2] {0, 0};
                Time = 0;

                Speed = SpeedMap[LeagueUsed = League];

                Output = $"Player: {PlayerName}\r\nGatekeeper: {GatekeeperName}\r\nLeague: {Leagues[LeagueUsed]}\r\n\r\n";

                Active = true;
            }

            timer = new Stopwatch();
            timer.Start();

            Window?.UpdateSaltyInfo();
        }

        public static void GameFinished(int winner) {
            if (Finished) return;

            timer?.Stop();
            Time += timer?.ElapsedMilliseconds ?? 0;

            if (Active) {
                score[winner]++;

                Output += $"{Current} {TimeString}\r\n";

                if (Finished) {
                    Active = false;
                    CopyOutput();

                } else if (winner == 0) Speed = spd - 1;
                else Speed = spd + 1;
            }

            Window?.UpdateSaltyInfo();
        }

        public static void Ready(UI window) {
            Window = window;
            Window?.UpdateSaltyInfo();
        }
    }
}
