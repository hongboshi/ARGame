using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using LitJson;

namespace Plates.Client.Net
{
    class BaseJsonProtocolHandler : BaseProtocolHandler
    {
        internal BaseJsonProtocolHandler(TcpAsyncClient client) : base(client)
        {
        }

        public override void SendMsg(int msgno, object obj)
        {
            byte[] msgBody = System.Text.Encoding.UTF8.GetBytes(JsonMapper.ToJson(obj));
            ushort msgBodySize = (ushort)(msgBody.Length);
            byte[] msgByte = new byte[4 + msgBodySize];

            Array.Copy(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(msgno)), msgByte, 4);
            Buffer.BlockCopy(msgBody, 0, msgByte, 4, msgBody.Length);

            //Console.WriteLine("");

            Send(ref msgByte);
        }

        public override bool SignalInputBuffer(ref byte[] recvBuff)
        {
            int msgID = (int)(recvBuff[0] << 24 | recvBuff[1] << 16 | recvBuff[2] << 8 | recvBuff[3]);

            string js = System.Text.Encoding.UTF8.GetString(recvBuff, 4, recvBuff.Length - 4);
            JsonData stJson = JsonMapper.ToObject(js);

            NetObject stObj = new NetObject();
            stObj.msgID = msgID;
            stObj.obj = stJson;

            Console.WriteLine("sceneprotocol SignalInputBuffer msgid " + msgID.ToString());

            lock (_recvList)
            {
                _recvList.PushBack(stObj);
            }

            return true;
        }
    }
}