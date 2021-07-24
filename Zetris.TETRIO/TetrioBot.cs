using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

using MisaMinoNET;
using PerfectClearNET;

namespace Zetris.TETRIO {
    class TetrioBot: Bot<UI, TetrioBot> {
        #region InputHelper
        static void AddGarbage(int[,] board, int[] garbage, int attack, out int[] leftover) {
            leftover = garbage.Take(garbage.Length - attack).ToArray();

            if (leftover.Length <= 0) return;

            int m = Math.Min(8, leftover.Length);

            for (int i = 0; i < m; i++)
                AddGarbageLine(board, leftover[i]);

            leftover = garbage.Skip(m).ToArray();
        }
        #endregion

        protected override int getPreviews() => (Preferences.Previews > 18)? int.MaxValue : Preferences.Previews;
        protected override bool getPerfectClear() => Preferences.PerfectClear;
        protected override bool getEnhancePerfect() => Preferences.EnhancePerfect;
        protected override bool HoldAllowed() => Preferences.HoldAllowed;
        protected override bool AllSpins() => Preferences.AllSpins;
        protected override MisaMinoParameters CurrentStyle() => Preferences.CurrentStyle.Clone().Parameters;
        protected override bool C4W() => Preferences.C4W;
        protected override bool TSDOnly() => Preferences.TSDOnly;
        protected override int Intelligence() => Preferences.Intelligence;
        protected override bool Allow180() => true;
        protected override bool SRSPlus() => true;
        protected override uint PCThreads() => Preferences.PCThreads;
        protected override bool GarbageBlocking() => true;
        protected override int RushTime() => 40;
        protected override bool Danger() => false; // there is no PUBLIC version due to TETR.IO not having local multiplayer

        static int ConvPiece(string p) {
            if (p == null) return 255;
            if (p == "GB") return 9;
            return Array.IndexOf(MisaMino.ToChar, p);
        }

        static readonly Dictionary<Instruction, string> ToTetrio = new Dictionary<Instruction, string>() {
            {Instruction.L, "Left"},
            {Instruction.R, "Right"},
            {Instruction.LL, "DASLeft"},
            {Instruction.RR, "DASRight"},
            {Instruction.D, "SonicDrop"},
            {Instruction.DD, "SonicDrop"},
            {Instruction.LSPIN, "Ccw"},
            {Instruction.RSPIN, "Cw"},
            {Instruction.SPIN2, "180"},
            {Instruction.HOLD, "Hold"},
        };

        int pieceCount = -1;
        Stopwatch timer;
        
        void startThinking(int garbage) {
            int[] q = getClippedQueue();

            LogHelper.LogText("QUEUE FOR AOT: " + string.Join(" ", q));

            MisaMinoAOT(current, q, hold, combo, garbage, 22 + Convert.ToInt32(!FitPieceWithConvert(misaboard, current, 4, 3, 0)));

            if (PerfectClear.Running && !pcbuffer) PerfectClear.Abort();

            PerfectClearAOT(current, q, hold, combo);
        }

        Dictionary<string, Func<JToken, object>> handlers;
        Dictionary<string, ChatCommandBase> chatCommands;

        public ushort Port = 47326;
        HttpListener server;

        protected override void LoopIteration() {
            HttpListenerContext e;

            try {
                e = server.GetContext();

            } catch (HttpListenerException ex) {
                LogHelper.LogText(ex.Message);
                return;
            }

            object response = null;

            // CORS
            if (e.Request.HttpMethod == "OPTIONS") {
                e.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept, X-Requested-With");
                e.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST");
                e.Response.AddHeader("Access-Control-Max-Age", "1728000");

            } else {
                string content = new StreamReader(e.Request.InputStream).ReadToEnd();
                LogHelper.LogText($"{e.Request.RawUrl} {content}");

                if (handlers.TryGetValue(e.Request.RawUrl, out var handler))
                    response = handler.Invoke(JToken.Parse(content));
            }

            e.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            e.Response.StatusCode = 200;

            if (response != null) {
                LogHelper.LogText(JToken.FromObject(response).ToString());

                byte[] data = Encoding.ASCII.GetBytes(JToken.FromObject(response).ToString());

                e.Response.ContentType = "application/json";
                e.Response.ContentLength64 = data.Length;

                e.Response.OutputStream.Write(data, 0, data.Length);
                e.Response.OutputStream.Flush();
            }

            e.Response.Close();
        }

        protected override void Starting() {
            for (int i = 0; i < 32; i++) {
                try {
                    server = new HttpListener() {
                        Prefixes = { $"http://127.0.0.1:{Port}/" },
                    };
                    server.Start();

                } catch (HttpListenerException) {
                    Port++;
                    continue;
                }

                break;
            }

            if (Port != 47326)
                Window?.SetPortTitle(Port);

            handlers = new Dictionary<string, Func<JToken, object>>() {
                {"/newGame", e => {
                    handlers["/endGame"].Invoke(null);

                    NewGame(() => {
                        board = new int[10, 40];
                            for (int i = 0; i < 10; i++)
                                for (int j = 0; j < 40; j++)
                                    board[i, j] = 255;

                        IEnumerable<int> incoming = e.ToObject<string[]>().Select(ConvPiece);
                        current = incoming.First();
                        queue = incoming.Skip(1).ToList();
                        hold = null;
                        combo = 0;
                    }, 22);

                    baseBoardHeight = 22;
                    pieceCount = 0;

                    Window?.SetActive(true);

                    return null;
                }},

                {"/newPieces", e => {
                    if (pieceCount != -1)
                        queue.AddRange(e.ToObject<string[]>().Select(ConvPiece));

                    return null;
                }},

                {"/nextMove", e => {
                    if (pieceCount == -1)
                        return new {
                            moves = new string[0],
                            expected_cells = new int[0][]
                        };

                    garbage = (int)e;

                    misaboard = (int[,])board.Clone();
                    pcboard = (int[,])board.Clone();

                    if (pieceCount == 0) {
                        timer?.Stop();
                        timer = Stopwatch.StartNew();

                    } else {
                        if (!MisaMino.Running)
                            startThinking(garbage);

                        while (timer.ElapsedMilliseconds < (Preferences.Speed >= 20? 0 : pieceCount * 1000.0 / Preferences.Speed));
                    }

                    pieceCount++;

                    // ideally this should take gravity into account...  i'm too lazy
                    // or make bot wait before harddropping, but then can't go under 2pps (lock is 500ms)
                    baseBoardHeight = 22 + Convert.ToInt32(!FitPieceWithConvert(misaboard, current, 4, 3, 0));

                    bool applied = MakeDecision(out bool wasHold, out int clear, out List<int[]> coords);

                    LogHelper.LogText($"Placed {pieceUsed}");

                    if (wasHold) {  // In TETR.IO, advance game state since we manage it internally, even if the piece didn't end up placing...
                        if (hold == null) queue.RemoveAt(0);
                        hold = current;
                    }

                    current = queue[0];
                    queue.RemoveAt(0);

                    if (clear > 0) combo++;
                    else combo = 0;

                    if (applied) {
                        LogHelper.LogText("Thinking...");
                        startThinking(clear > 0? garbage - atk : Math.Max(0, garbage - 8));
                    }

                    return new {
                        moves = movements.Where(i => ToTetrio.ContainsKey(i)).Select(i => ToTetrio[i]).ToArray(),
                        expected_cells = coords.ToArray()
                    };
                }},

                {"/resetBoard", _e => {
                    if (pieceCount == -1) return null;

                    dynamic e = (dynamic)_e;
                    string[,] reset = e.board.ToObject<string[,]>();

                    for (int i = 0; i < 10; i++)
                        for (int j = 0; j < 40; j++)
                            board[i, j] = ConvPiece(reset[39 - j, i]?.ToUpper());

                    misaboard = (int[,])board.Clone();
                    pcboard = (int[,])board.Clone();

                    LogHelper.LogText("ResetBoard thinking...");
                    startThinking((int)e.garbage);

                    return null;
                }},

                {"/endGame", e => {
                    EndGame();

                    pieceCount = -1;

                    Window?.SetActive(false);
                    return null;
                }},

                {"/speedDown", e => {
                    Window?.SetSpeed(Preferences.Speed * 10 / 11);
                    return null;
                }},

                {"/speedUp", e => {
                    Window?.SetSpeed(Preferences.Speed / 10 * 11);
                    return null;
                }},

                {"/chatCommand", e => {
                    IEnumerable<string> split = ((string)e).Trim().Split(' ').Select(i => i.Trim());

                    string command = split.First();
                    string[] args = split.Skip(1).ToArray();

                    foreach (var i in chatCommands)
                        if (i.Key == command)
                            return i.Value.Process(args);

                    return new { response = $"Unknown command." };
                }}
            };

            chatCommands = new Dictionary<string, ChatCommandBase>() {
                {"help", new ChatCommand(
                    "Displays all available commands.",
                    () => new { 
                        response = "Available commands:\n" + 
                            string.Join("\n", chatCommands.Select(i => $".{i.Key} {string.Join("", i.Value.Hints.Select(j => $"<{j}> "))}- {i.Value.HelpText}"))
                    }
                )}
            };
        }

        protected override void BeforeDispose() {
            server.Stop();
            server.Close();
        }
    }
}
