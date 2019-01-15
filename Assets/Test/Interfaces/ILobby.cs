//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//2019年01月15日01:32:46
//MagicOnionTestService

namespace MagicOnionTestService.LobbyMessageTest
{
    public interface ILobby
    {
        /// <summary>
        /// 加入房间
        /// </summary>
        /// <param name="name">参加した人の名前</param>
        void OnJoin(string name);
        /// <summary>
        /// 离开房间
        /// </summary>
        /// <param name="name">退室した人の名前</param>
        void OnLeave(string name);
    }
}