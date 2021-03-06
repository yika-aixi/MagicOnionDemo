//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年06月14日-01:44
//Assembly-CSharp

using System.Collections.Generic;
using System.Threading.Tasks;
using MagicOnion;
using Shader.MessageObjects;

namespace MagicOnionTestService.LobbyMessageTest
{
    /// <summary>
    /// 客户端调用
    /// </summary>
    public interface IChatHub:IStreamingHub<IChatHub,IChatHubReceiver>
    {
        /// <summary>
        /// 加入聊天
        /// </summary>
        /// <param name="mesg"></param>
        /// <returns></returns>
        Task JoinChat(JoinOrCreateRoomMesg mesg);

        /// <summary>
        /// 离开聊天
        /// </summary>
        /// <returns></returns>
        Task LeaveChat();
        
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="mesg"></param>
        /// <returns></returns>
        Task SendMessage(SendMesg mesg);
    }
}