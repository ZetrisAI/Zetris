using System;
using System.Collections.Generic;

using Newtonsoft.Json.Linq;

namespace Zetris.TETRIO {
    abstract class TetrioClientConnection {
        protected Dictionary<string, Func<JToken, object>> handlers = new Dictionary<string, Func<JToken, object>>();

        public void RegisterHandler(string path, Func<JToken, object> handler)
            => handlers.Add(path, handler);

        public bool CheckHandler(string path)
            => path == null? false : handlers.ContainsKey(path);

        public object InvokeHandler(string path, JToken e)
            => handlers[path].Invoke(e);

        public abstract void LoopIteration();
        public abstract void Stop();
    }
}
