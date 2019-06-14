//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年06月14日-17:42
//MagicOnionTestService

using System;
using System.Threading.Tasks;
using MagicOnion.Client;
using MagicOnion.Server.Hubs;
using MagicOnionTestService.LobbyMessageTest.Room;

namespace MagicOnionTestService.LobbyMessageTest.Impls
{
    [GroupConfiguration(typeof(ConcurrentDictionaryGroupRepositoryFactory))]
    public class RoomHub:StreamingHubBase<IRoomHub,IRoomHubReceiver>,IRoomHub
    {
        private IGroup _room;
        private string _playerName;
        
#pragma warning disable 1998
        public async Task CreateRoom(string roomName)
#pragma warning restore 1998
        {
            if (RoomManager.HasRoom(roomName))
            {
                return;
            }
            
            var room = this.Group.RawGroupRepository.GetOrAdd(roomName);
            
            RoomManager.AddRoom(room);
        }

        public async Task JoinOrCreateRoom(string roomName, string playerName)
        {
            #region 因为Context的不一样,导致广播调用不到函数,所以其他hub必须加入Context才行,所以先只是创建而已,等作者添加支持

            var result = RoomManager.GetRoom(roomName,out _room);

            if (result)
            {
                await RoomManager.JoinRoom(_room, Context);
            }
            else
            {
                _room = await Group.AddAsync(roomName);
            
                RoomManager.AddRoom(_room); 
            }

            _playerName = playerName;
            
            #endregion
            
            Broadcast(_room).OnJoinRoom(playerName);

            Console.WriteLine($"{playerName} Join {roomName} Room.");
        }

        public async Task LeaveRoom()
        {
            if (_room == null)
            {
//                return ReturnStatusCode<int>(-1,"Leave Room failure.");
                return;
            }
            
            Broadcast(_room).OnLeaveRoom(_playerName);

            #region 因为Context的不一样,导致广播调用不到函数,所以其他hub必须加入Context才行,所以先只是创建而已,所以不离开房间

            await RoomManager.LeaveRoom(_room, Context);

            #endregion

            Console.WriteLine($"{_playerName} Leave {_room.GroupName} Room.");
        }
    }
}