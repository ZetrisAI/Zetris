﻿using System.IO;
using System.Net;
using System.Text;

using Newtonsoft.Json.Linq;

namespace Zetris.TETRIO {
    class TetrioClientConnectionHTTP: TetrioClientConnection {
        HttpListener listener;

        TetrioClientConnectionHTTP(ushort port) {
            listener = new HttpListener() {
                Prefixes = { $"http://127.0.0.1:{port}/" },
            };
        }

        public static bool Start(ushort port, out TetrioClientConnection server) {
            server = null;
            TetrioClientConnectionHTTP httpserver;

            try {
                httpserver = new TetrioClientConnectionHTTP(port);
                httpserver.listener.Start();

            } catch (HttpListenerException) {
                return false;
            }

            server = httpserver;
            return true;
        }

        public override void LoopIteration() {
            HttpListenerContext e;

            try {
                e = listener.GetContext();

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

                if (CheckHandler(e.Request.RawUrl))
                    response = InvokeHandler(e.Request.RawUrl, JToken.Parse(content));
            }

            e.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            e.Response.StatusCode = 200;

            if (response != null) {
                string res = JToken.FromObject(response).ToString();

                LogHelper.LogText(res);
                byte[] data = Encoding.ASCII.GetBytes(res);

                e.Response.ContentType = "application/json";
                e.Response.ContentLength64 = data.Length;

                e.Response.OutputStream.Write(data, 0, data.Length);
                e.Response.OutputStream.Flush();
            }

            e.Response.Close();
        }

        public override void Stop() {
            listener.Stop();
            listener.Close();
        }
    }
}
