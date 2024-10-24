using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

using Fleck;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Zetris.TETRIO {
    class TetrioClientConnectionWS: TetrioClientConnection {
        WebSocketServer listener;

        TetrioClientConnectionWS(ushort port) {
            listener = new WebSocketServer($"ws://127.0.0.1:{port}");
        }

        BlockingCollection<Tuple<string, Func<string, Task>>> queue = new BlockingCollection<Tuple<string, Func<string, Task>>>();

        public static bool Start(ushort port, out TetrioClientConnection server) {
            server = null;
            TetrioClientConnectionWS wsserver;

            if (IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners().Select(p => p.Port).Contains(port)) {
                LogHelper.LogText($"TCP Port {port} is taken");
                return false;
            }

            try {
                wsserver = new TetrioClientConnectionWS(port);
                wsserver.listener.Start(client => {
                    client.OnMessage = e => {
                        wsserver.queue.Add(new Tuple<string, Func<string, Task>>(e, client.Send));
                    };
                });

            } catch (Exception ex) {
                LogHelper.LogText(ex.Message);
                return false;
            }

            server = wsserver;
            return true;
        }

        public override void LoopIteration() {
            Tuple<string, Func<string, Task>> e;

            try {
                e = queue.Take();

            } catch (InvalidOperationException) {
                // queue is complete
                return;
            }

            object response = null;

            LogHelper.LogText(e.Item1);

            string[] parts = e.Item1.Split(new char[] { ' ' }, 3);

            string id = parts.Length > 0? parts[0] : null;
            string path = parts.Length > 1? parts[1] : null;
            string content = parts.Length > 2? parts[2] : null;

            if (CheckHandler(path))
                response = InvokeHandler(path, JToken.Parse(content));

            if (response != null) {
                string res = JToken.FromObject(response).ToString(Formatting.None);
                res = $"{id} {res}";

                LogHelper.LogText(res);
                e.Item2(res);
            }
        }

        public override void Stop() {
            listener.Dispose();
            queue.CompleteAdding();
        }
    }
}
