using UnityEngine;
using System.Collections;
using System;
using proto.loginmsg;
using proto.scene_message;
using proto.actormsg;

namespace SimpleFramework.Net
{
    partial class LoginProtobufHandler : BaseProtoBufProtocolHandler
    {
        private static LoginProtobufHandler ins;
        public static LoginProtobufHandler getIns()
        {
            return ins;
        }
        public static void Connect(string ip, int port, System.Action<bool> result)
        {
            if (ins == null)
            {
                ins = new LoginProtobufHandler(new TcpAsyncClient(ip, port));
                ins.Initialize(result);
            }
        }
        private LoginProtobufHandler(TcpAsyncClient client) : base(client)
        {
            InitMsg();
        }
        public void LoginReq(string accound, string pwd,Action<object> callback = null)
        {
            accountLoginHandler.callback = callback;
            ST_CMsgAccountLoginRequest req = new ST_CMsgAccountLoginRequest();
            req.account = accound;
            req.password = pwd;
            SendMsg(MsgIDDefineDic.LoginServer.MSG_ACCOUNT_LOGIN_REQUEST_C2S, req);
        }
        public void RegistReq(string account, string pwd)
        {

            ST_CMsgAccountRegistRequest req = new ST_CMsgAccountRegistRequest();
            req.account = account;
            req.password = pwd;
            SendMsg(MsgIDDefineDic.LoginServer.MSG_ACCOUNT_REGIST_REQUEST_C2S, req);
        }
        public void GuestLoginReq(string identify = "",Action<object> callback = null)
        {
            guestLoginHandler.callback = callback;
            ST_CMsgGuestLoginRequest req = new ST_CMsgGuestLoginRequest();
            if (identify != "")
            {
                req.identify = identify;
            }
            SendMsg(MsgIDDefineDic.LoginServer.MSG_GUEST_LOGIN_REQUEST_C2S, req);
        //    NetManagerEx.Instance.Send(NetManagerEx.serverType.login_Server, MsgIDDefineDic.LoginServer.MSG_GUEST_LOGIN_REQUEST_C2S, req);
        }
        public void PullRoleinfoReq(Action<object> callback)
        {
            getUserinfoHandler.callback = callback;
            ST_RoleInfoRequest req = new ST_RoleInfoRequest();
            SendMsg(MsgIDDefineDic.LoginServer.MSG_ACTOR_GETUSERINFO_REQUEST_C2S, req);
        }
    }
    partial class LoginProtobufHandler
    {
        private Handler accountLoginHandler = new Handler(null, typeof(ST_CMsgAccountLoginResponse));
        private Handler regist1Handler = new Handler(null, typeof(ST_CMsgAccountRegistResp));
        private Handler regist2Handler = new Handler(null, typeof(ST_CMsgAccountRegistResponse));
        private Handler guestLoginHandler = new Handler(null, typeof(ST_CMsgGuestLoginResponse));
        private Handler getServerAddrHandler = new Handler(null, typeof(ST_CMsgGetLogicAddrResponse));
        private Handler getUserinfoHandler = new Handler(null, typeof(ST_RoleInfoResponse));
        private Handler selectSexHandler = new Handler(null, typeof(CSetUsrInfoRespS2C));
        private Handler userDetailsHandler = new Handler(null, typeof(ST_PlayerPropResponse));
        private Handler phoneAuthCodeHandler = new Handler(null, typeof(ST_CMsgPhoneAuthCodeResp));
        private Handler phoneBindHandler = new Handler(null, typeof(ST_CMsgPhoneBindResp));

        void InitMsg()
        {

            RegisterHandler(MsgIDDefineDic.LoginServer.MSG_ACCOUNT_LOGIN_RESPONSE_S2C, accountLoginHandler);
            RegisterHandler(MsgIDDefineDic.LoginServer.MSG_GUEST_LOGIN_RESPONSE_S2C, guestLoginHandler);
            RegisterHandler(MsgIDDefineDic.LoginServer.MSG_SELECT_SEX_S2C, selectSexHandler);
            RegisterHandler(MsgIDDefineDic.LoginServer.MSG_SERVERADDR_RESPONSE_S2C, getServerAddrHandler);
            RegisterHandler(MsgIDDefineDic.LoginServer.MSG_ACTOR_PALYERPROP_RESPONSE_S2C, userDetailsHandler);
            RegisterHandler(MsgIDDefineDic.LoginServer.MSG_ACTOR_GETUSERINFO_RESPONSE_S2C, getUserinfoHandler);
            RegisterHandler(MsgIDDefineDic.LoginServer.MSG_ACCOUNT_REGIST_RESPONSE_S2C, regist1Handler);
            RegisterHandler(MsgIDDefineDic.LoginServer.MSG_ACCOUNT_REGIST_old_S2C, regist2Handler);
            RegisterHandler(MsgIDDefineDic.LoginServer.MSG_PhoneAuthCode_S2C, phoneAuthCodeHandler);
            RegisterHandler(MsgIDDefineDic.LoginServer.MSG_PhoneBind_S2C, phoneBindHandler);
        }
    }
}
