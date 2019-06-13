//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年06月14日-02:22
//MagicOnionTestService

using MagicOnion;
using MagicOnion.Server;
using MagicOnionTestService.LobbyMessageTest.Room;

namespace MagicOnionTestService.LobbyMessageTest.Impls
{
    public class GetRoomService:ServiceBase<IGetRoomService>,IGetRoomService
    {
        public UnaryResult<string[]> GetRoomList()
        {
            return UnaryResult(RoomManager.Rooms.ToArray());
        }
    }
}