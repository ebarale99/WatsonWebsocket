﻿using System;
using System.Net;
using System.Net.WebSockets;
using System.Threading;

namespace WatsonWebsocket
{
    /// <summary>
    /// Client metadata.
    /// </summary>
    public class ClientMetadata
    {
        #region Public-Members

        /// <summary>
        /// Globally-unique identifier of the client.
        /// </summary>
        public Guid Guid { get; } = Guid.NewGuid();

        /// <summary>
        /// IP:port of the client.
        /// </summary>
        public string IpPort
        {
            get
            {
                return Ip + ":" + Port;
            }
        }

        /// <summary>
        /// IP address of the client.
        /// </summary>
        public string Ip { get; } = null;

        /// <summary>
        /// Port for the client.
        /// </summary>
        public int Port { get; } = 0;

        /// <summary>
        /// Name for the client, managed by the developer (you).
        /// </summary>
        public string Name { get; set; } = null;

        /// <summary>
        /// Metadata for the client, managed by the developer (you).
        /// </summary>
        public object Metadata { get; set; } = null;

        #endregion

        #region Internal-Members

        internal HttpListenerContext HttpContext = null;
        internal WebSocket Ws = null;
        internal WebSocketContext WsContext = null;
        internal readonly CancellationTokenSource TokenSource = null;
        internal readonly SemaphoreSlim SendLock = new SemaphoreSlim(1);

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="httpContext">HTTP context.</param>
        /// <param name="ws">Websocket.</param>
        /// <param name="wsContext">Websocket context.</param>
        /// <param name="tokenSource">Token source.</param>
        public ClientMetadata(HttpListenerContext httpContext, WebSocket ws, WebSocketContext wsContext, CancellationTokenSource tokenSource)
        {
            HttpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
            Ws = ws ?? throw new ArgumentNullException(nameof(ws));
            WsContext = wsContext ?? throw new ArgumentNullException(nameof(wsContext));
            TokenSource = tokenSource ?? throw new ArgumentNullException(nameof(tokenSource));
            Ip = HttpContext.Request.RemoteEndPoint.Address.ToString();
            Port = HttpContext.Request.RemoteEndPoint.Port;
        }

        #endregion

        #region Public-Methods

        /// <summary>
        /// Human-readable representation of the object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string ret = "[";
            ret += Guid.ToString() + "|" + IpPort;
            if (!String.IsNullOrEmpty(Name)) ret += "|" + Name;
            ret += "]";
            return ret;
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}
