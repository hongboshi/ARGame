using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using friendserver.friend_message;
using msgserver.talk_message;

namespace Plates.Client.Net
{
    partial class FriendProtobufHandler : BaseProtoBufProtocolHandler
    {
        private static FriendProtobufHandler ins;
        public static FriendProtobufHandler getIns()
        {
            return ins;
            
        }

        public static void Connect(string ip, int port, System.Action<bool> result)
        {
            if (ins == null)
            {
                ins = new FriendProtobufHandler(new TcpAsyncClient(ip, port));
                ins.Initialize(result);
            }
        }
        private FriendProtobufHandler(TcpAsyncClient client) : base(client)
        {
            InitMsg();
        }
        public void RemoveFriendReq(long uid, long tuid,string session,Action<object> callback = null)
        {
            removeFriendHandler.callback = callback;
            friendserver.friend_message.CDelFriendC2S req = new CDelFriendC2S();
            req.fuid = uid;
            req.uid = tuid;
            req.session = session;
            SendMsg(MsgIDDefineDic.FriendServer.MSG_DEL_FRIEND_C2S, req);
        }
        public void AddFriendReq(long uid, long tuid, string session,Action<object> callback = null)
        {
            addFriendHandler.callback = callback;
            CBeFriendC2S req = new CBeFriendC2S();
            req.fuid = uid;
            req.uid = tuid;
            req.session = session;
            SendMsg(MsgIDDefineDic.FriendServer.MSG_BE_FRIEND_C2S, req);
        }
        public void PullFriendList(long uid,string session,Action<object> callback = null)
        {
            pullFriendlistHandler.callback = callback;
            CGetFriendListC2S req = new CGetFriendListC2S();
            req.uid = uid;
            req.session = session;
            SendMsg(MsgIDDefineDic.FriendServer.MSG_GET_FRIEND_LIST_C2S, req);
        }
    }
    partial class FriendProtobufHandler
    {
        private Handler removeFriendHandler = new Handler(null, typeof(CDelFriendS2C));// Action<object> removeFriendCallback;
        private Handler addFriendHandler = new Handler(null, typeof(CBeFriendS2C));// Action<object> addFriendCallback;
        private Handler pullFriendlistHandler = new Handler(null, typeof(CGetFriendListS2C));
        void InitMsg()
        {
            RegisterHandler(MsgIDDefineDic.FriendServer.MSG_BE_FRIEND_S2C, removeFriendHandler);
            RegisterHandler(MsgIDDefineDic.FriendServer.MSG_DEL_FRIEND_S2C, addFriendHandler);
            RegisterHandler(MsgIDDefineDic.FriendServer.MSG_GET_FRIEND_LIST_S2C, pullFriendlistHandler);
        }
    }
}
