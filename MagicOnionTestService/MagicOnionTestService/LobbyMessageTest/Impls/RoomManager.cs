//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年06月14日-02:23
//MagicOnionTestService

using System.Collections.Generic;

namespace MagicOnionTestService.LobbyMessageTest.Impls
{
    public static class RoomManager
    {
        public static List<string> Rooms { get; }
        
        static RoomManager()
        {
            Rooms = new List<string>();
        }
    }
}