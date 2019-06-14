//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年06月14日-17:35
//MagicOnionTestService

using System.Threading.Tasks;
using Shader.MessageObjects;

namespace MagicOnionTestService.LobbyMessageTest.Room
{
    public interface IRoomHubReceiver
    {
        /// <summary>
        /// 玩家加入房间
        /// </summary>
        /// <param name="playerName"></param>
        /// <returns></returns>
        void OnJoinRoom(string playerName);

        /// <summary>
        /// 玩家离开房间
        /// </summary>
        /// <returns></returns>
        void OnLeaveRoom(string playerName);
    }
}