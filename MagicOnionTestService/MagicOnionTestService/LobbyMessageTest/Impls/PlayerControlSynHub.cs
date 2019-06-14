//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年06月14日-17:15
//MagicOnionTestService

using System;
using System.Threading.Tasks;
using MagicOnion;
using MagicOnion.Server.Hubs;
using MagicOnionTestService.LobbyMessageTest.PlayerControlSyn;
using Shader.MessageObjects;

namespace MagicOnionTestService.LobbyMessageTest.Impls
{
    public class PlayerControlSynHub:StreamingHubBase<IPlayerControlSynHub,IPlayerControlSynReceiver>,IPlayerControlSynHub
    {
        private IGroup _room;
        
        public async Task Init(PlayerControlInitMesg mesg)
        {
            var result = RoomManager.GetRoom(mesg.RoomName,out _room);

            if (result)
            {
                await _room.AddAsync(Context);
            }
        }

        public Task Move(PlayerMoveControlMesg mesg)
        {
            this.BroadcastExceptSelf(_room).OnMove(mesg);
            
            return NilTask;
        }

//        async Task IStreamingHub<IPlayerControlSynHub, IPlayerControlSynReceiver>.DisposeAsync()
//        {
//            await _room.RemoveAsync(Context);
//        }
    }
}