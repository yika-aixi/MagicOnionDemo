//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//2019年01月15日01:38:09
//MagicOnionTestService

using System.Threading.Tasks;
using MagicOnion;

namespace MagicOnionTestService.LobbyMessageTest
{
    public interface IChatHub:IStreamingHub<IChatHub,IChat>
    {
        /// <summary>
        /// 加入房间
        /// </summary>
        /// <param name="userName">名字</param>
        /// <param name="roomName">房间名</param>
        /// <returns></returns>
        Task JoinAsync(string userName,string roomName);
        /// <summary>
        /// 退出房间
        /// </summary>
        /// <returns></returns>
        Task LeaveAsync();
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendMessageAsync(string message);
    }
}