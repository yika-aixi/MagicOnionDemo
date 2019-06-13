//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年06月14日-01:50
//Assembly-CSharp

using Shader.MessageObjects;

namespace MagicOnionTestService.LobbyMessageTest
{
    /// <summary>
    /// 服务器调用
    /// </summary>
    public interface IChatHubReceiver
    {
        /// <summary>
        /// 玩家加入房间
        /// </summary>
        /// <param name="mesg"></param>
        /// <returns></returns>
        void OnJoinRoom(JoinOrCreateRoomMesg mesg);

        /// <summary>
        /// 玩家离开房间
        /// </summary>
        /// <returns></returns>
        void OnLeaveRoom(string playerName);
        
        /// <summary>
        /// 玩家发送消息
        /// </summary>
        /// <param name="mesg"></param>
        /// <returns></returns>
        void OnSendMessage(SendMesgResponses mesg);
    }
}