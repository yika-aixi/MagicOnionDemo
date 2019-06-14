//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年06月14日-18:08
//Assembly-CSharp

using MessagePack;

namespace Shader.MessageObjects
{
    [MessagePackObject()]
    public class PlayerControlInitMesg
    {
        [Key(0)]
        public string RoomName;

        [Key(1)]
        public string PlayerName;

        public PlayerControlInitMesg(string roomName, string playerName)
        {
            RoomName = roomName;
            PlayerName = playerName;
        }
    }
}