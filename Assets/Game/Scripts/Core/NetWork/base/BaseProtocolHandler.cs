using System;
using System.Collections.Generic;

namespace Plates.Client.Net
{
    struct NetObject
    {
        public int msgID;
        public Object obj;
    }

    class Handler
    {
        public Type t;
        public System.Action<object> callback;

        public Handler(System.Action<object> handler, Type t = null)
        {
            this.t = t;
            this.callback = handler;
        }
    }

    class Common
    {
        public static void Swap<T>(ref T t1, ref T t2)
        {
            T t = t2;
            t2 = t1;
            t1 = t;
        }
    }

    abstract class BaseProtocolHandler
    {
        protected TcpProtocol _tcpProtocol;
        protected CircularBuffer<NetObject> _recvList = new CircularBuffer<NetObject>(1000);
        private CircularBuffer<NetObject> _doList = new CircularBuffer<NetObject>(1000);
        public Dictionary<int,Handler> _packetHandleDic = new Dictionary<int, Handler>();

        internal BaseProtocolHandler(TcpAsyncClient client)
        {
            _tcpProtocol = new TcpProtocol(client);
            _tcpProtocol.SetProtocolHandler(this);
            HandlerManager.RegisterProtocolHandler(this);
        }

        protected void Initialize(System.Action<bool> callback)
        {
            _tcpProtocol.Connect(callback);
        }

        protected void Send(ref byte[] msgByte)
        {
            _tcpProtocol.Send(ref msgByte);
        }

        //protected void RegisterHandler(int msgid, System.Action<object> handler, Type t = null)
        //{
        //    _packetHandleDic.Add(msgid, new Handler(handler, t));
        //}
        protected void RegisterHandler(int msgid, Handler handler)
        {
            _packetHandleDic.Add(msgid,handler);
        }

        public void DoRecvMsg()
        {
            lock(_recvList)
            {
                Common.Swap<CircularBuffer<NetObject>>(ref _recvList, ref _doList);
            }

            NetObject netObj;
            while (!_doList.IsEmpty)
            {
                netObj = _doList.Front();
                _doList.PopFront();

                Handler handler = null;
                _packetHandleDic.TryGetValue(netObj.msgID, out handler);
                if (handler == null || handler.callback == null)
                {
                    continue;
                }
                handler.callback(netObj.obj);
            }
        }

        public void DisConnect()
        {
            _tcpProtocol.DisConnect();
        }

        public bool Connected { get { return _tcpProtocol.Connected; } }

        public abstract void SendMsg(int msgno, object obj);
        public abstract bool SignalInputBuffer(ref byte[] recvBuff);
    }
}