//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年06月14日-17:25
//Assembly-CSharp

using MessagePack;

namespace MagicOnionTestService.LobbyMessageTest.Room
{
    [MessagePackObject()]
    public class RoomInfo
    {
        /// <summary>
        /// 房间名
        /// </summary>
        [Key(0)]
        public string RoomName { get; }
        
        /// <summary>
        /// 玩家数量
        /// </summary>
        [Key(1)]
        public int PlayerCount;

        public RoomInfo(string roomName)
        {
            RoomName = roomName;
        }
    }
}