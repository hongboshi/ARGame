using proto.scene_message;
using proto.sync_message;
using UnityEngine;

namespace Plates.Client.Net
{
    partial class SceneProtocolHandler : BaseProtoBufProtocolHandler
    {
        private static SceneProtocolHandler ins;
        public static SceneProtocolHandler getIns()
        {
            return ins;
        }

        public static void Connect(string ip, int port, System.Action<bool> result)
        {
            if (ins == null)
            {
                ins = new SceneProtocolHandler(new TcpAsyncClient(ip, port));
                ins.Initialize(result);
            }
        } 
        private SceneProtocolHandler(TcpAsyncClient client) : base(client)
        {
            InitMsg();
        }
        public void Login(long uid, int actorID, string session, bool isHost = false,System.Action<object> callback = null)
        {
            CLoginReqC2S rq = new CLoginReqC2S();
            rq.actorID = actorID;
            rq.uid = uid;
            rq.session = session;
            if (isHost) rq.anchor = 1;
            SendMsg(MsgIDDefineDic.SceneServer.MSG_LOGIN_REQ_C2S, rq);
            if (callback != null)
                loginCallback.callback = callback;
        }
        public void ChooseRoleSex(long uid,bool isMale,System.Action<object> callback = null)
        {
            CSetUsrInfoReqC2S rsp = new CSetUsrInfoReqC2S();
            rsp.uid = uid;
            rsp.actorID = isMale == true ? 1 : 2;
            SendMsg(MsgIDDefineDic.SceneServer.MSG_SET_USR_INFO_C2S, rsp);
        }
        public void GetAgoraAppidReq(long uid, System.Action<object> callback = null)
        {
            getAgoraAppidCallback.callback = callback;
            CGetAgoraAppidReq req = new proto.scene_message.CGetAgoraAppidReq();
            req.uid = uid;
            SendMsg(MsgIDDefineDic.SceneServer.MSG_GET_AGORA_APPID_REQ_C2S, req);
        }
        public void EnterRoomReq(long uid,int roomType, string pwd, int rid,System.Action<object> callback)
        {
            enterRoomCallback.callback = callback;
            CEnterRoomReqC2S rq = new CEnterRoomReqC2S();
            rq.rflag = roomType;
            rq.rid = rid;
            rq.pwd = pwd;
            rq.uid = uid;
            SendMsg(MsgIDDefineDic.SceneServer.MSG_ENTER_ROOM_REQ_C2S, rq);
        }
        public void CreateRoomReq(long uid, int confid, int usermax, string name, string pwd, string desc, string url = "",System.Action<object> callback = null)
        {
            createRoomCallback.callback = callback;
            CCreateRoomReqC2S req = new CCreateRoomReqC2S();
            req.confid = (int)confid;
            req.uid = uid;
            req.type = 2;
            req.usrmax = usermax;
            req.name = name;
            req.pwd = pwd;
            req.desc = desc;
            req.url = url;
            SendMsg(MsgIDDefineDic.SceneServer.MSG_CREATE_ROOM_REQ_C2S, req);
        }
        public void GetRoomListReq(long uid,System.Action<object> callback = null)
        {
            getRoomListCallback.callback = callback;
            CGetRoomListReqC2S req = new CGetRoomListReqC2S();
            req.uid = uid;
            SendMsg(MsgIDDefineDic.SceneServer.MSG_GET_ROOM_LIST_RES_S2C, req);
        }
        public void LeaveRoomReq(long uid,System.Action<object> callback = null)
        {
            leaveRoomCallback.callback = callback;
            CLeaveRoomReqC2S req = new CLeaveRoomReqC2S();
            req.uid = uid;
            SendMsg(MsgIDDefineDic.SceneServer.MSG_LEAVE_ROOM_REQ_C2S, req);
        }

        //public void SyncMoveInfo(MoveInfo info)
        //{
        //    CMoveInfo minfo = info.ToProtobuf();
        //    SendMsg(MsgIDDefineDic.SceneServer.MSG_SYNC_MOVE_C2S, minfo);
        //}
        public void SyncUrlInfo(long uid, string url)
        {
            CShareUrlInfo info = new CShareUrlInfo();
            info.uid = uid;
            info.URL = url;
            SendMsg(MsgIDDefineDic.SceneServer.MSG_SHARE_URL_C2S, info);
        }
        public void SyncPlaytoyInfo(long uid, int toyID, Vector3 pos, string msg = "")
        {
            CPlaytoyInfo info = new CPlaytoyInfo();
            info.uid = uid;
            info.id = toyID;
            info.pos_x = pos.x;
            info.pos_y = pos.y;
            info.pos_z = pos.z;
            info.none = msg;
            SendMsg(MsgIDDefineDic.SceneServer.MSG_SYNC_PLAYTOY_C2S, info);
        }
        public void GetUniqueTool(long uid,int objectID,int roomid,System.Action<object> callback = null)
        {
            getUniqueToolCallback.callback = callback;
            CGetUniqueTool cc = new proto.sync_message.CGetUniqueTool();
            cc.rid = roomid;
            cc.toolid = objectID;
            cc.uid = uid;
            SendMsg(MsgIDDefineDic.SceneServer.MSG_GET_UNIQUE_TOOL_RESP_S2C, cc);
        }
        public void DropUniqueTool(long uid,int objectId,int roomid,System.Action<object> callback = null)
        {
            putUniqueToolCallback.callback = callback;
            CPutUniqueTool cc = new CPutUniqueTool();
            cc.rid = roomid;
            cc.uid = uid;
            cc.toolid = objectId;
            SendMsg(MsgIDDefineDic.SceneServer.MSG_PUT_UNIQUE_TOOL_RESP_S2C, cc);
        }
        public void SyncExpressionInfo(long uid,int expressionID)
        {
            CExpressionInfo info = new CExpressionInfo();
            info.uid = uid;
            info.id = expressionID;
            SendMsg(MsgIDDefineDic.SceneServer.MSG_SYNC_EXPRESSION_S2C, info);
        }
        public void ExtandSync(CSyncInfo obj)
        {
            SendMsg(MsgIDDefineDic.SceneServer.MSG_SYNC_INFO_C2S, obj);
        }
        public void PingServer(long uid,System.Action<object> callback)
        {
            pingCallback.callback = callback;
            CPingC2S rp = new CPingC2S();
            rp.uid = uid;
            SendMsg(MsgIDDefineDic.SceneServer.MSG_PING_C2S, rp);
        }
        public void ExtandNotify(CNotifyInfo cni)
        {
            SendMsg(MsgIDDefineDic.SceneServer.MSG_NOTIFY_INFO_C2S, cni);
        }
        public void Get360Texture(long uid,System.Action<object> callback = null)
        {
            pull360listCallback.callback = callback;
            CGet360ImageLstReq resqest = new CGet360ImageLstReq();
            resqest.uid = uid;
            SendMsg(MsgIDDefineDic.SceneServer.MSG_GET_360_IMAGE_LST_S2C, resqest);
        }
    }
    partial class SceneProtocolHandler
    {
        //callback(服务器端一问一答的消息) sync(1对多的消息) notify(1对1的消息)
        private Handler loginCallback = new Handler(null, typeof(CLoginRespS2C));
        private Handler enterRoomCallback  = new Handler(null, typeof(CEnterRoomRespS2C));
        private Handler createRoomCallback = new Handler(null, typeof(CCreateRoomRespS2C));
        private Handler getRoomListCallback = new Handler(null, typeof(CCreateRoomRespS2C));
        private Handler leaveRoomCallback = new Handler(null, typeof(CLeaveRoomRespS2C));
        private Handler getUniqueToolCallback = new Handler(null, typeof(CGetUniqueToolResp));
        private Handler putUniqueToolCallback = new Handler(null, typeof(CPutUniqueToolResp));
        private Handler pingCallback = new Handler(null, typeof(CPingS2C));
        private Handler pull360listCallback = new Handler(null, typeof(CGet360ImageLstResp));
        private Handler getAgoraAppidCallback = new Handler(null, typeof(CGetAgoraAppidResp));

        public Handler leaveRoomSync = new Handler(null, typeof(CNotifyLeaveRoomS2C));
        public Handler enterRoomSync = new Handler(null, typeof(CNotifyEnterRoomS2C));
        public Handler moveSync = new Handler(null, typeof(CMoveInfo));
        public Handler shareUrlSync = new Handler(null, typeof(CShareUrlInfo));
        public Handler playtoySync = new Handler(null, typeof(CPlaytoyInfo));
        public Handler expressionSync = new Handler(null, typeof(CExpressionInfo));
        public Handler extandSync = new Handler(null, typeof(CSyncInfo));
        public Handler extandNotify = new Handler(null, typeof(CNotifyInfo));

        void InitMsg()
        {
            RegisterHandler(MsgIDDefineDic.SceneServer.MSG_LOGIN_RES_S2C, loginCallback);
            RegisterHandler(MsgIDDefineDic.SceneServer.MSG_ENTER_ROOM_RES_S2C, enterRoomCallback);
            RegisterHandler(MsgIDDefineDic.SceneServer.MSG_CREATE_ROOM_RES_S2C, createRoomCallback);
            RegisterHandler(MsgIDDefineDic.SceneServer.MSG_GET_ROOM_LIST_RES_S2C, getRoomListCallback);
            RegisterHandler(MsgIDDefineDic.SceneServer.MSG_LEAVE_ROOM_RES_S2C, leaveRoomCallback);
            RegisterHandler(MsgIDDefineDic.SceneServer.MSG_GET_UNIQUE_TOOL_RESP_S2C, getUniqueToolCallback);
            RegisterHandler(MsgIDDefineDic.SceneServer.MSG_PUT_UNIQUE_TOOL_RESP_S2C, putUniqueToolCallback);
            RegisterHandler(MsgIDDefineDic.SceneServer.MSG_GET_360_IMAGE_LST_S2C, pull360listCallback);
            RegisterHandler(MsgIDDefineDic.SceneServer.MSG_PING_S2C, pingCallback);
            RegisterHandler(MsgIDDefineDic.SceneServer.MSG_GET_AGORA_APPID_RESP_S2C, getAgoraAppidCallback);

            RegisterHandler(MsgIDDefineDic.SceneServer.MSG_ENTER_ROOM_S2C, enterRoomSync);
            RegisterHandler(MsgIDDefineDic.SceneServer.MSG_NOTIFY_LEAVE_ROOM_S2C, leaveRoomSync);
            RegisterHandler(MsgIDDefineDic.SceneServer.MSG_SYNC_MOVE_S2C, moveSync);
            RegisterHandler(MsgIDDefineDic.SceneServer.MSG_SHARE_URL_S2C, shareUrlSync);
            RegisterHandler(MsgIDDefineDic.SceneServer.MSG_SYNC_PLAYTOY_S2C, playtoySync);
            RegisterHandler(MsgIDDefineDic.SceneServer.MSG_SYNC_EXPRESSION_S2C, expressionSync);
            RegisterHandler(MsgIDDefineDic.SceneServer.MSG_SYNC_INFO_S2C, extandSync);

            RegisterHandler(MsgIDDefineDic.SceneServer.MSG_NOTIFY_INFO_S2C, extandNotify);


            ////拓展接口消息
            ////sync
            //public const int sync_example1 = 100;
            //public const int sync_sphere = 1011;
            ////notify
            //public const int notify_example1 = 200;
            //public const int notify_ask_upstage = 201; //主播喊人上台消息


        }
    }
}