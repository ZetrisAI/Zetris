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
            if (MisaMino.Running) MisaMino.Abort();
            if (PerfectClear.Running && !pcbuffer) PerfectClear.Abort();

            misasolved = false;

            int[] q = getClippedQueue();

            LogHelper.LogText("QUEUE FOR AOT: " + string.Join(" ", q));

            MisaMino.FindMove(
                q,
                current,
                hold,
                misa_lasty = 23,
                misaboard,
                combo,
                Math.Max(
                    b2b,
                    Convert.ToInt32(FuckItJustDoB2B(misaboard, 25))
                ),
                garbage
            );

            if (Preferences.PerfectClear && (!pcsolved || searchbufpc) && !PerfectClear.Running) {
                bool cancel = false;

                int[,] bufboard = pcboard;
                int[] bufq = q;
                int bufcurrent = current;
                int? bufhold = hold;
                int bufcombo = combo;
                bool bufb2b = b2b > 0;

                if (searchbufpc) {
                    LogHelper.LogText("searchbufpc start");

                    bufboard = new int[10, 40];

                    for (int i = 0; i < 10; i++)
                        for (int j = 0; j < 40; j++)
                            bufboard[i, j] = 255;

                    int[,] tempboard = (int[,])pcboard.Clone();

                    for (int i = 0; i < cachedpc.Count; i++) {    // yes i copy pasted code, no i don't care, they're different enough to not generalize into a func
                        bool bufwasHold = bufcurrent != cachedpc[i].Piece;

                        if (cancel = !ApplyPiece(tempboard, cachedpc[i].Piece, cachedpc[i].X, cachedpc[i].Y + 2, cachedpc[i].R, 23, out int bufclear, out _))
                            break;

                        if (i == cachedpc.Count - 1) // last piece always clears a line, so don't have to track b2b all the time
                            bufb2b = isPCB2BEnding(bufclear, cachedpc[i].Piece, cachedpc[i].R);

                        int bufstart = Convert.ToInt32(bufwasHold && bufhold == null);

                        bufhold = bufwasHold ? bufcurrent : bufhold;
                        bufcurrent = bufq[bufstart];
                        bufq = bufq.Skip(bufstart + 1).ToArray();

                        bufcombo += Convert.ToInt32(bufclear > 0);
                    }

                    cancel |= !BoardEquals(bufboard, tempboard);
                }

                if (!cancel) {
                    PerfectClear.Find(
                        bufboard, bufq.Take(Math.Min(bufq.Length, getPreviews())).ToArray(), bufcurrent,
                        bufhold, Preferences.HoldAllowed, 6, true, getPerfectType(), bufcombo, bufb2b
                    );

                    searchbufpc = false;

                } else LogHelper.LogText("FUCK but less");
            }
        }

        Dictionary<string, Func<JToken, object>> handlers;

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
                    }, 23);

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

                    int garbage = (int)e;

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

                    bool pathSuccess = false;
                    int baseBoardHeight = 23;

                    if (MisaMino.Running) MisaMino.Abort();
                    if (PerfectClear.Running && !pcbuffer) PerfectClear.Abort();

                    List<Instruction> movements = new List<Instruction>();
                    int pieceUsed = -10, finalX = -10, finalY = -10, finalR = -10, atk = -10;

                    if (Preferences.PerfectClear && pcsolved && BoardEquals(board, pcboard)) {
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

                        bool misaok = BoardEquals(misaboard, board) && misasolved;
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
                            LogHelper.LogText("Rush (SOMETHING JUST WENT REALLY WRONG)");
                            LogHelper.LogBoard(misaboard, board);

                            int[] rushq = getClippedQueue();

                            LogHelper.LogText("QUEUE FOR RUSH: " + string.Join(" ", rushq));

                            MisaMino.FindMove(
                                rushq,
                                current,
                                hold,
                                baseBoardHeight,
                                board,
                                combo,
                                Math.Max(
                                    b2b, // can't read b2b, but yeah this value *might* be wrong idk
                                    Convert.ToInt32(FuckItJustDoB2B(board, 40))
                                ),
                                garbage
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

                    bool applied = ApplyPiece(board, pieceUsed, finalX, finalY, finalR, 23, out int clear, out List<int[]> coords);
                    LogHelper.LogText($"Piece applied? {applied}");

                    misaboard = (int[,])board.Clone();
                    pcboard = (int[,])board.Clone();

                    if (wasHold) {  // In TETR.IO, advance game state since we manage it internally, even if the piece didn't end up placing...
                        if (hold == null) queue.RemoveAt(0);
                        hold = current;
                    }

                    current = queue[0];
                    queue.RemoveAt(0);

                    if (clear > 0) combo++;
                    else combo = 0;

                    if (pathSuccess && !pcsolved) b2b = Convert.ToInt32(isPCB2BEnding(clear, pieceUsed, finalR));

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
                    MisaMino.Abort();
                    PerfectClear.Abort();

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
                }}
            };
        }

        protected override void BeforeDispose() {
            server.Stop();
            server.Close();
        }
    }
}
