using System;
using System.Collections.Generic;
using System.Text;

namespace MsgIDDefineDic
{
    public struct LoginServer
    {
        //login
        public const int MSG_ACCOUNT_LOGIN_REQUEST_C2S = 10001;
        public const int MSG_ACCOUNT_LOGIN_RESPONSE_S2C = 10002;
        public const int MSG_ACCOUNT_REGIST_REQUEST_C2S = 10003;
        public const int MSG_ACCOUNT_REGIST_RESPONSE_S2C = 10004;
        public const int MSG_GUEST_LOGIN_REQUEST_C2S = 10005;
        public const int MSG_GUEST_LOGIN_RESPONSE_S2C = 10006;
        public const int MSG_ACCOUNT_REGIST_old_C2S = 10007;
        public const int MSG_ACCOUNT_REGIST_old_S2C = 10008;

        public const int MSG_PhoneAuthCode_C2S = 10009;
        public const int MSG_PhoneAuthCode_S2C = 10010;
        public const int MSG_PhoneBind_C2S = 10011;
        public const int MSG_PhoneBind_S2C = 10012;

        /// <summary>
        /// 请求各种服地址
        /// </summary>
        public const int MSG_SERVERADDR_REQUEST_C2S = 10201;
        public const int MSG_SERVERADDR_RESPONSE_S2C = 10202;

        // 角色信息
        public const int MSG_ACTOR_GETUSERINFO_REQUEST_C2S = 10101;
        public const int MSG_ACTOR_GETUSERINFO_RESPONSE_S2C = 10102;

        public const int MSG_SELECT_SEX_C2S = 10301;
        public const int MSG_SELECT_SEX_S2C = 10302;

        public const int MSG_ACTOR_PALYERPROP_REQUEST_C2S = 10105;
	    public const int MSG_ACTOR_PALYERPROP_RESPONSE_S2C= 10106;
    }

    public struct SceneServer
    {
        //选择角色性别
        public const int MSG_SET_USR_INFO_C2S = 20605;
        public const int MSG_SET_USR_INFO_S2C = 20606;

        /// <summary>
        /// 声网的
        /// </summary>
        public const int MSG_GET_AGORA_APPID_REQ_C2S = 20001;
        public const int MSG_GET_AGORA_APPID_RESP_S2C = 20002;

        /// <summary>
        /// 逻辑服登录房间
        /// </summary>
        public const int MSG_LOGIN_REQ_C2S = 20099;
        public const int MSG_LOGIN_RES_S2C = 20100;

        //enter room
        public const int MSG_ENTER_ROOM_REQ_C2S = 20101;
        public const int MSG_ENTER_ROOM_RES_S2C = 20102;
        public const int MSG_ENTER_ROOM_S2C = 20103;

        //create room
        public const int MSG_CREATE_ROOM_REQ_C2S = 20104;
        public const int MSG_CREATE_ROOM_RES_S2C = 20105;

        //get room list
        public const int MSG_GET_ROOM_LIST_REQ_C2S = 20106;
        public const int MSG_GET_ROOM_LIST_RES_S2C = 20107;

        //leave room
        public const int MSG_LEAVE_ROOM_REQ_C2S = 20108;
        public const int MSG_LEAVE_ROOM_RES_S2C = 20109;
        public const int MSG_NOTIFY_LEAVE_ROOM_S2C = 20110;

        // SYNC
        //sync moveinfo
        public const int MSG_SYNC_MOVE_C2S = 20201;
        public const int MSG_SYNC_MOVE_S2C = 20202;

        //sync url
        public const int MSG_SHARE_URL_C2S = 20301;
        public const int MSG_SHARE_URL_S2C = 20302;

        //sync playtoy
        public const int MSG_SYNC_PLAYTOY_C2S = 20401;
        public const int MSG_SYNC_PLAYTOY_S2C = 20402;

        public const int MSG_GET_UNIQUE_TOOL_C2S = 20113;
        public const int MSG_GET_UNIQUE_TOOL_RESP_S2C = 20114;
        public const int MSG_GET_UNIQUE_TOOL_S2C = 20115;

        public const int MSG_PUT_UNIQUE_TOOL_C2S = 20116;
        public const int MSG_PUT_UNIQUE_TOOL_RESP_S2C = 20117;
        public const int MSG_PUT_UNIQUE_TOOL_S2C = 20118;


        //sync expression
        public const int MSG_SYNC_EXPRESSION_C2S = 20501;
        public const int MSG_SYNC_EXPRESSION_S2C = 20502;

        //sync 同步拓展接口 1对多
        public const int MSG_SYNC_INFO_C2S = 20601;
        public const int MSG_SYNC_INFO_S2C = 20602;

        //心跳
        public const int MSG_PING_C2S = 20111;
        public const int MSG_PING_S2C = 20112;

        //notify 通知拓展接口  1对1
        public const int MSG_NOTIFY_INFO_C2S = 20603;
        public const int MSG_NOTIFY_INFO_S2C = 20604;

        //360 场景球
        public const int MSG_GET_360_IMAGE_LST_C2S = 20607;
        public const int MSG_GET_360_IMAGE_LST_S2C = 20608;

        //拓展接口消息
        //sync
        public const int sync_example1 = 100;
        public const int sync_sphere = 1011;
        //notify
        public const int notify_example1 = 200;
        public const int notify_ask_upstage = 201; //主播喊人上台消息
    }

    public struct MsgServer
    {
        public const int LoginRequest = 30001;
        public const int LoginResponse = 30002;

        public const int LogoutRequest = 30003;
        public const int LogoutResponse = 30004;

        public const int GetOfflineMsgC2S = 30005;
        public const int GetOfflineMsgS2C = 30006;
        public const int AckOfflineMsgC2S = 30007;

        public const int TalkC2S = 30008;
        public const int TalkRespS2C = 30009;
        public const int TalkS2C = 30010;
        public const int AckMsgC2S = 30011;

        //public const int ClientMsgRequest = 30012;
        //public const int ClientMsgResponse = 30013;

        public const int BroadcastC2S = 30014;
        public const int BroadcastRespS2C = 30015;
        public const int BroadcastS2C = 30016;

        public const int ClientMsgC2S = 30012;
        public const int ClientMsgRespS2C = 30013;
        public const int ClientMsgS2C = 30017;
    }

    public struct FriendServer
    {
        public const int MSG_BE_FRIEND_C2S = 21000;
        public const int MSG_BE_FRIEND_S2C = 21001;

        public const int MSG_DEL_FRIEND_C2S = 21002;
        public const int MSG_DEL_FRIEND_S2C = 21003;

        public const int MSG_GET_FRIEND_LIST_C2S = 21004;
        public const int MSG_GET_FRIEND_LIST_S2C = 21005;
    }


}

