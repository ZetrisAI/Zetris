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

namespace Zetris {
    static class Bot {
        static UI Window = null;

        static int[,] board = new int[10, 40];

        static int current;
        static int? hold;
        static int combo;
        static List<int> queue = new List<int>();

        static int misa_lasty;
        static int b2b = 0;

        static int[,] misaboard = new int[10, 40];
        static bool misasolved = false;

        static int[,] pcboard = new int[10, 40];
        static bool pcsolved = false;
        static bool futurepcsolved = false;
        static bool pcbuffer = false;
        static List<Operation> cachedpc = new List<Operation>();
        static List<Operation> executingpc => pcbuffer? cachedpc : PerfectClear.LastSolution;
        static bool searchbufpc = false;

        static int getPreviews() => (Preferences.Previews > 18)? int.MaxValue : Preferences.Previews;

        static int[] getClippedQueue() => queue.Take(Math.Min(queue.Count, getPreviews())).ToArray();

        static int getPerfectType() => Convert.ToInt32(Preferences.EnhancePerfect) + Convert.ToInt32(Preferences.EnhancePerfect && Preferences.AllSpins) * 2;

        static bool isPCB2BEnding(int cleared, int piece, int r) => (cleared >= 4) || (Preferences.AllSpins && (
            piece == 0 ||
            piece == 1 ||
            (r != 2 && (
                piece == 2 ||
                piece == 3
            ))
        ));

        static void misaPrediction(int current, int[] q, int? hold, int combo, int cleared, int[] garbage, int attack) {
            if (cleared == 0 && garbage.Length > 0)
                InputHelper.AddGarbage(misaboard, garbage, attack, out garbage);

            if (MisaMino.Running) MisaMino.Abort();

            misasolved = false;

            MisaMino.FindMove(
                q,
                current,
                hold,
                misa_lasty = 23,
                misaboard,
                combo,
                Math.Max(
                    b2b,
                    Convert.ToInt32(InputHelper.FuckItJustDoB2B(misaboard, 25))
                ),
                garbage.Length
            );
        }

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
        
        static readonly Dictionary<string, Func<JToken, object>> handlers = new Dictionary<string, Func<JToken, object>>() {
            {"/newGame", e => {
                handlers["/endGame"].Invoke(null);

                MisaMino.Reset(); // this will abort as well
                misasolved = false;
                b2b = 1; // Hack that makes MisaMino start like a normal person

                PerfectClear.Abort();
                pcsolved = false;
                futurepcsolved = false;
                pcbuffer = false;
                cachedpc = new List<Operation>();
                searchbufpc = false;

                board = new int[10, 40];
                for (int i = 0; i < 10; i++)
                    for (int j = 0; j < 40; j++)
                        board[i, j] = 255;

                misaboard = (int[,])board.Clone();
                pcboard = (int[,])board.Clone();

                IEnumerable<int> incoming = e.ToObject<string[]>().Select(ConvPiece);
                current = incoming.First();
                queue = incoming.Skip(1).ToList();
                hold = null;
                combo = 0;

                int[] q = getClippedQueue();

                MisaMino.FindMove(q, current, null, misa_lasty = 23, misaboard, 0, b2b, 0);

                if (Preferences.PerfectClear) {
                    PerfectClear.Find(
                        pcboard, q, current,
                        null, Preferences.HoldAllowed, 6, true, getPerfectType(), 0, false
                    );
                }

                Window?.SetActive(true);

                return null;
            }},

            {"/newPieces", e => {
                queue.AddRange(e.ToObject<string[]>().Select(ConvPiece));
                return null;
            }},

            {"/nextMove", e => {
                int[] garbage = ((dynamic)e).data.ToObject<int[]>();

                bool pathSuccess = false;
                int baseBoardHeight = 23;

                if (MisaMino.Running) MisaMino.Abort();
                if (PerfectClear.Running && !pcbuffer) PerfectClear.Abort();
        
                List<Instruction> movements = new List<Instruction>();
                int pieceUsed = -10, finalX = -10, finalY = -10, finalR = -10, atk = -10;

                if (Preferences.PerfectClear && pcsolved && InputHelper.BoardEquals(board, pcboard)) {
                    LogHelper.LogText("Detected PC");

                    pieceUsed = executingpc[0].Piece;
                    finalX = executingpc[0].X;
                    finalY = executingpc[0].Y + 2; // old baseboardheight was 21, now it's 23
                    finalR = executingpc[0].R;
                    
                    movements = MisaMino.FindPath(
                        board,
                        baseBoardHeight,
                        pieceUsed,
                        finalX,
                        finalY,
                        finalR,
                        current != pieceUsed,
                        out _,
                        out pathSuccess
                    );

                    if (!pathSuccess)
                        LogHelper.LogText($"PC PATHFINDER FAILED! piece={pieceUsed}, x={finalX}, y={finalY}, r={finalR}");
                }

                if (!pathSuccess) {
                    LogHelper.LogText("Using Misa!");

                    bool misaok = InputHelper.BoardEquals(misaboard, board) && misasolved;
                    bool misasaved = false;

                    if (misaok && misa_lasty != baseBoardHeight) { // oops we spawned on wrong y pos
                        LogHelper.LogText($"Tryna save Misa... {misa_lasty} {baseBoardHeight}");

                        movements = MisaMino.FindPath(
                            board,
                            baseBoardHeight,
                            MisaMino.LastSolution.PieceUsed,
                            MisaMino.LastSolution.FinalX,
                            MisaMino.LastSolution.FinalY,
                            MisaMino.LastSolution.FinalR,
                            current != MisaMino.LastSolution.PieceUsed,
                            out _,
                            out misaok
                        );

                        misasaved = misaok;

                        LogHelper.LogText($"misasaved {misasaved}");
                    }

                    if (!misaok) {
                        LogHelper.LogText("Rush");
                        LogHelper.LogBoard(misaboard, board);

                        MisaMino.FindMove(
                            queue.ToArray(),
                            current,
                            hold,
                            baseBoardHeight,
                            board,
                            combo,
                            Math.Max(
                                b2b, // can't read b2b, but yeah this value *might* be wrong idk
                                Convert.ToInt32(InputHelper.FuckItJustDoB2B(board, 40))
                            ),
                            garbage.Length
                        );

                        Stopwatch misasearching = new Stopwatch();
                        misasearching.Start();

                        while (misasearching.ElapsedMilliseconds < 40) {}

                        MisaMino.Abort();
                    }

                    if (!misasaved) movements = MisaMino.LastSolution.Instructions;
                    pieceUsed = MisaMino.LastSolution.PieceUsed;
                    b2b = MisaMino.LastSolution.B2B;
                    atk = MisaMino.LastSolution.Attack;
                    finalX = MisaMino.LastSolution.FinalX;
                    finalY = MisaMino.LastSolution.FinalY;
                    finalR = MisaMino.LastSolution.FinalR;

                    Window?.SetConfidence($"{MisaMino.LastSolution.Nodes} ({MisaMino.LastSolution.Depth})");
                    Window?.SetThinkingTime(MisaMino.LastSolution.Time);

                    pcsolved = false;
                    futurepcsolved = false;
                    pcbuffer = false;
                    searchbufpc = false;

                } else {
                    LogHelper.LogText("Using PC!");

                    cachedpc = executingpc.Skip(1).ToList();

                    bool prev = pcbuffer;
                    pcbuffer = cachedpc.Count != 0;

                    searchbufpc |= !prev && pcbuffer;

                    if (!pcbuffer) {
                        pcsolved = futurepcsolved;
                        searchbufpc = futurepcsolved = false;
                    }

                    Window?.SetConfidence($"[PC] {cachedpc.Count + 1}");
                    Window?.SetThinkingTime(PerfectClear.LastTime);
                }

                misasolved = false;

                bool wasHold = movements.Count > 0 && movements[0] == Instruction.HOLD;

                bool applied = InputHelper.ApplyPiece(board, pieceUsed, finalX, finalY, finalR, out int clear, out List<int[]> coords);
                LogHelper.LogText($"Piece applied? {applied}");

                misaboard = (int[,])board.Clone();

                if (applied) {
                    if (wasHold) {
                        if (hold == null) queue.RemoveAt(0);
                        hold = current;
                    }

                    current = queue[0];
                    queue.RemoveAt(0);
                    
                    int[] q = getClippedQueue();

                    if (clear > 0) combo++;
                    else combo = 0;

                    if (pathSuccess && !pcsolved) b2b = Convert.ToInt32(isPCB2BEnding(clear, pieceUsed, finalR));

                    LogHelper.LogText("AOT");
                    misaPrediction(current, q, hold, combo, clear, garbage, atk);

                    pcboard = (int[,])misaboard.Clone();

                    if (Preferences.PerfectClear && movements.Count > 0 && (!pcsolved || searchbufpc) && !PerfectClear.Running) {
                        bool cancel = false;

                        int[,] bufboard = pcboard;
                        int[] bufq = q;
                        int bufcurrent = current;
                        int? bufhold = hold;
                        int bufcombo = combo;
                        bool bufb2b = b2b > 0;
                        
                        if (searchbufpc) {
                            bufboard = new int[10, 40];

                            for (int i = 0; i < 10; i++)
                                for (int j = 0; j < 40; j++)
                                    bufboard[i, j] = 255;
                                    
                            int[,] tempboard = (int[,])pcboard.Clone();

                            for (int i = 0; i < cachedpc.Count; i++) {    // yes i copy pasted code, no i don't care, they're different enough to not generalize into a func
                                bool bufwasHold = bufcurrent != cachedpc[i].Piece;

                                if (cancel = !InputHelper.ApplyPiece(tempboard, cachedpc[i].Piece, cachedpc[i].X, cachedpc[i].Y + 2, cachedpc[i].R, out int bufclear, out _))
                                    break;

                                if (i == cachedpc.Count - 1) // last piece always clears a line, so don't have to track b2b all the time
                                    bufb2b = isPCB2BEnding(bufclear, cachedpc[i].Piece, cachedpc[i].R);

                                int bufstart = Convert.ToInt32(bufwasHold && bufhold == null);
                                            
                                bufhold = bufwasHold? bufcurrent : bufhold;
                                bufcurrent = bufq[bufstart];
                                bufq = bufq.Skip(bufstart + 1).ToArray();

                                bufcombo += Convert.ToInt32(bufclear > 0);
                            }

                            cancel |= !InputHelper.BoardEquals(bufboard, tempboard);
                        }
                                
                        if (!cancel) {
                            PerfectClear.Find(
                                bufboard, bufq.Take(Math.Min(bufq.Length, getPreviews())).ToArray(), bufcurrent,
                                bufhold, Preferences.HoldAllowed, 6, true, getPerfectType(), bufcombo, bufb2b
                            );

                            searchbufpc = false;
                        } else LogHelper.LogText("FUCK but less");
                    }
                } else LogHelper.LogText("FUCK");

                //LogHelper.LogText(string.Join(", ", movements));

                return new {
                    moves = movements.Where(i => ToTetrio.ContainsKey(i)).Select(i => ToTetrio[i]).ToArray(),
                    expected_cells = coords.ToArray()
                };
            }},
            {"/resetBoard", e => {
                string[,] reset = e.ToObject<string[,]>();

                for (int i = 0; i < 10; i++)
                    for (int j = 0; j < 40; j++)
                        board[i, j] = ConvPiece(reset[39 - j, i]?.ToUpper());

                return null;
            }},
            {"/endGame", e => {
                MisaMino.Abort();
                PerfectClear.Abort();
                
                Window?.SetActive(false);
                return null;
            }},
        };

        public static void UpdateConfig() {
            if (!Started) return;

            MisaMinoParameters param = Preferences.CurrentStyle.Clone().Parameters;
            param.Parameters.strategy_4w = 400 * Convert.ToInt32(Preferences.C4W);

            MisaMino.Configure(param, Preferences.HoldAllowed, Preferences.AllSpins, Preferences.TSDOnly, Preferences.Intelligence, true, true);
        }

        public static void UpdatePCThreads() {
            if (!Started) return;

            PerfectClear.SetThreads(Preferences.PCThreads);
        }

        public static ushort Port = 47326;
        static HttpListener server;

        static void Loop() {
            UpdateConfig();
            UpdatePCThreads();

            while (!Disposing) {
                HttpListenerContext e;

                try {
                    e = server.GetContext();

                } catch (HttpListenerException ex) {
                    LogHelper.LogText(ex.Message);
                    break;
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

            Disposed = true;
        }

        public static bool Started { get; private set; } = false;

        public static async void Start(UI window) {
            if (Started) return;

            Started = true;

            server = new HttpListener() {
                Prefixes = {$"http://127.0.0.1:{Port}/"},
            };

            MisaMino.Finished += success => misasolved = success;

            PerfectClear.Finished += success => {
                if (pcbuffer) futurepcsolved = success;
                else pcsolved = success;
            };

            Window = window;

            server.Start();

            await Task.Run(Loop);
        }

        static bool Disposing = false;
        public static bool Disposed { get; private set; } = false;

        public static void Dispose() {
            Disposing = true;

            server.Stop();
            server.Close();

            while (!Disposed && Started) {
                MisaMino.Abort();
                PerfectClear.Abort();
            }
        }
    }
}
