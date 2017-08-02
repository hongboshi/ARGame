using UnityEngine;
using System;
using msgserver.broadcast_message;
using msgserver.client_message;
using msgserver.get_offline_message;
using msgserver.login_message;
using msgserver.logout_message;
using msgserver.talk_message;
using System.Collections.Generic;
using System.Collections;

namespace SimpleFramework.Net
{
    partial class MsgProtocolHandler : BaseProtoBufProtocolHandler
    {
        private static MsgProtocolHandler ins;
        public static MsgProtocolHandler getIns()
        {
            return ins;
        }

        public static void Connect(string ip, int port, System.Action<bool> result)
        {
            if (ins == null)
            {
                ins = new MsgProtocolHandler(new TcpAsyncClient(ip, port));
                ins.Initialize(result);
            }
        }
        private MsgProtocolHandler(TcpAsyncClient client) : base(client)
        {
            InitMsg();
        }
        public void Login(long uid, string session,Action<object> callback = null)
        {
            CMsgLoginRequest req = new CMsgLoginRequest();
            req.uid = uid;
            req.session = session;
            SendMsg(MsgIDDefineDic.MsgServer.LoginRequest, req);
        }
        public void Loginout(long uid,Action<object> callback = null)
        {
            logoutCallback.callback = callback;   
            CMsgLogoutRequest req = new CMsgLogoutRequest();
            req.uid = uid;
            SendMsg(MsgIDDefineDic.MsgServer.LogoutResponse, req);
        }
        public void PullOfflinemsgReq(long uid,int maxnumber = 0,Action<object> callback = null)
        {
            getOfflineMsgCallback.callback = callback;
            CGetOfflineMsg_C2S req = new CGetOfflineMsg_C2S();
            req.maxnum = maxnumber;
            req.uid = uid;
            SendMsg(MsgIDDefineDic.MsgServer.GetOfflineMsgC2S, req);
        }
        public void SendTalkmsgReq(CMsgTalk_C2S req, Action<object> callback = null)
        {
            talkMsgCallback.callback = callback;
            SendMsg(MsgIDDefineDic.MsgServer.TalkC2S, req);
        }
        public void ConfrimMsgid(CMsgAck_C2S req)
        {
            SendMsg(MsgIDDefineDic.MsgServer.AckMsgC2S, req);
        }
        public void SendClientmsgReq(long fuid,List<long> tuids,string msg,int type,Action<object> callback = null)
        {
            clientMsgCallback.callback = callback;
            CClientMsg_C2S req = new CClientMsg_C2S();
            req.fuid = fuid;
            foreach (var tem in tuids)
                req.tuids.Add(tem);
            req.msg = msg;
            req.type = type;
            SendMsg(MsgIDDefineDic.MsgServer.ClientMsgC2S, req);
        }
    }
    partial class MsgProtocolHandler
    {
        private Handler  loginCallback                             = new Handler(null, typeof(CMsgLoginResponse));
        private Handler  logoutCallback                            = new Handler(null, typeof(CMsgLogoutResponse));
        private Handler  getOfflineMsgCallback                     = new Handler(null, typeof(CGetOfflineMsg_S2C));
        private Handler  talkMsgCallback                           = new Handler(null, typeof(CMsgTalkResp_S2C));
        private Handler  broadcastCallback                         = new Handler(null, typeof(CMsgBroadcastResp_S2C));
        public  Handler clientMsgCallback                          = new Handler(null, typeof(CClientMsgResp_S2C));
        public  Handler talkMsgNotify                              = new Handler(null, typeof(CMsgTalk_S2C));
        public  Handler broadcastSync                              = new Handler(null, typeof(CMsgBroadcast_S2C));
        public  Handler clientMsgSync                              = new Handler(null, typeof(CClientMsg_S2C));

        void InitMsg()
        {
            RegisterHandler(MsgIDDefineDic.MsgServer.LoginResponse, loginCallback);
            RegisterHandler(MsgIDDefineDic.MsgServer.LogoutResponse, logoutCallback);
            RegisterHandler(MsgIDDefineDic.MsgServer.GetOfflineMsgS2C, getOfflineMsgCallback);
            RegisterHandler(MsgIDDefineDic.MsgServer.TalkRespS2C, talkMsgCallback);
            RegisterHandler(MsgIDDefineDic.MsgServer.BroadcastRespS2C, broadcastCallback);
            RegisterHandler(MsgIDDefineDic.MsgServer.ClientMsgRespS2C, clientMsgCallback);
            RegisterHandler(MsgIDDefineDic.MsgServer.TalkS2C, talkMsgNotify);
            RegisterHandler(MsgIDDefineDic.MsgServer.BroadcastS2C, broadcastSync);
            RegisterHandler(MsgIDDefineDic.MsgServer.ClientMsgS2C, clientMsgSync);
        }
    }
}