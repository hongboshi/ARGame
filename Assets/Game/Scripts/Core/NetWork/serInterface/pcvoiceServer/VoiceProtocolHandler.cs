using LitJson;

namespace Plates.Client.Net
{
    class VoiceProtocolHandler : BaseJsonProtocolHandler
    {
        private static VoiceProtocolHandler ins;
        public static VoiceProtocolHandler getIns()
        {
            return ins;
        }

        public static void Connect(string ip, int port,System.Action<bool> result)
        {
            if (ins == null)
            {
                ins = new VoiceProtocolHandler(new TcpAsyncClient(ip, port));
                ins.Initialize(result);
            }
        }

        public const int MSG_AGORA_VOICE_SERVICE = 60000;

        private VoiceProtocolHandler(TcpAsyncClient client) : base(client)
        {

        }

        public void Initialize(string appId,System.Action<object> callback)
        {
     //       RegisterHandler(MSG_AGORA_VOICE_SERVICE, callback);

            JsonData stJson = new JsonData();

            stJson["id"] = 2;
            stJson["appId"] = appId;

            SendMsg(MSG_AGORA_VOICE_SERVICE, stJson);
        }
        public void JoinChannel(string channelId,long selfUid)
        {
            JsonData stJson = new JsonData();
            stJson["id"] = 0;
            stJson["uid"] = selfUid;
            stJson["channelId"] = channelId;
            SendMsg(MSG_AGORA_VOICE_SERVICE, stJson);
        }
        public void LeaveChannel()
        {
            JsonData stJson = new JsonData();
            stJson["id"] = 1;
            SendMsg(MSG_AGORA_VOICE_SERVICE, stJson);
        }
    }

}