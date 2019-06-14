//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年06月14日-17:15
//MagicOnionTestService

using System.Threading.Tasks;
using MagicOnion.Server.Hubs;
using MagicOnionTestService.LobbyMessageTest.PlayerControlSyn;
using Shader.MessageObjects;

namespace MagicOnionTestService.LobbyMessageTest.Impls
{
    public class PlayerControlSynHub:StreamingHubBase<IPlayerControlSynHub,IPlayerControlSynReceiver>,IPlayerControlSynHub
    {
        private IGroup _room;
        
        public Task Init(PlayerControlInitMesg mesg)
        {
            _room = RoomManager.GetRoom(mesg.RoomName);
            
            return NilTask;
        }

        public Task Move(PlayerMoveControlMesg mesg)
        {
            this.BroadcastExceptSelf(_room).OnMove(mesg);
            
            return NilTask;
        }

        public Task Jump(string name)
        {
            this.BroadcastExceptSelf(_room).OnJump(name);
            
            return NilTask;
        }
    }
}