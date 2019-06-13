//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年06月14日-02:11
//Assembly-CSharp

using MessagePack;

namespace Shader.MessageObjects
{
    /// <summary>
    /// 加入或创建房间
    /// </summary>
    [MessagePackObject]
    public class JoinOrCreateRoomMesg
    {
        /// <summary>
        /// 房间名
        /// </summary>
        [Key(0)]
        public string RoomName;

        /// <summary>
        /// 用户名
        /// </summary>
        [Key(1)]
        public string UserName;

        public JoinOrCreateRoomMesg(string roomName, string userName)
        {
            RoomName = roomName;
            UserName = userName;
        }
    }
}