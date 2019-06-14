//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年06月14日-02:22
//MagicOnionTestService

using System.Linq;
using MagicOnion;
using MagicOnion.Server;
using MagicOnionTestService.LobbyMessageTest.Room;

namespace MagicOnionTestService.LobbyMessageTest.Impls
{
    public class GetRoomService:ServiceBase<IGetRoomService>,IGetRoomService
    {
        public async UnaryResult<RoomInfo[]> GetRoomList()
        {
            var result = await RoomManager.GetRoomInfos();

            return result.ToArray();
        }

        public async UnaryResult<int> GetRoomPlayerCount(string roomName)
        {
            var room = RoomManager.GetRoom(roomName);

            if (room != null)
            {
                var count = await room.GetMemberCountAsync();

                return count;
            }

            return -1;
        }
    }
}