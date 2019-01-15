//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//2019年01月15日01:39:32
//MagicOnionTestService

using System;
using System.Threading.Tasks;
using MagicOnion.Server.Hubs;

namespace MagicOnionTestService.LobbyMessageTest.Impls
{
    public class ChatHub:StreamingHubBase<IChatHub, IChat>, IChatHub
    {
        IGroup room;
        string me;
        //房间名字
        const string roomName = "Chat Room";
        public async Task JoinAsync(string userName)
        {
            //加入房间
            this.room = await this.Group.AddAsync(roomName);
            //保存名字
            me = userName;
            //广播消息:加入房间
            this.Broadcast(room).OnJoin(userName);
            Console.WriteLine($"{userName} Join Room");
        }

        public async Task LeaveAsync()
        {
            //离开房间
            await room.RemoveAsync(this.Context);
            //广播消息:退出房间
            this.Broadcast(room).OnLeave(me);
            Console.WriteLine($"{me} Leave Room");
        }


        public async Task SendMessageAsync(string message)
        {
            //广播消息:发送消息
            this.Broadcast(room).OnSendMessage(me, message);
            Console.WriteLine($"{me} Send Message : {message}");
        }

        protected override ValueTask OnDisconnected()
        {
            //nop
            return CompletedTask;
        }
    }
}