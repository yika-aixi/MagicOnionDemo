//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//2019年01月15日01:39:32
//MagicOnionTestService

using System;
using System.Threading.Tasks;
using MagicOnion.Server.Hubs;
using Shader.MessageObjects;

namespace MagicOnionTestService.LobbyMessageTest.Impls
{
    public class ChatHub:StreamingHubBase<IChatHub, IChatHubReceiver>, IChatHub
    {
        IGroup room;
        string me;
        public async Task JoinOrCreateRoom(JoinOrCreateRoomMesg mesg)
        {
            //加入房间
            this.room = await this.Group.AddAsync(mesg.RoomName);

            var count = await room.GetMemberCountAsync();
            
            Console.WriteLine("Current Room Player Count: " + count);
            
            //保存名字
            me = mesg.UserName;
            
            //广播消息:加入房间
            this.Broadcast(room).OnJoinRoom(mesg);
            
            Console.WriteLine($"{mesg.UserName} Join {mesg.RoomName} Room. {mesg.RoomName} Room Player Count : {count}");
        }

        public async Task LeaveRoom()
        {
            //离开房间
            await room.RemoveAsync(this.Context);
            
            //广播消息:退出房间
            this.Broadcast(room).OnLeaveRoom(me);
            
            Console.WriteLine($"{me} Leave {room.GroupName} Room");
        }

#pragma warning disable 1998
        public async Task SendMessage(SendMesg mesg)
#pragma warning restore 1998
        {
            Console.WriteLine($"{me} Send Message : {mesg.Message}");

            //广播消息:发送消息
            this.Broadcast(room).OnSendMessage(new SendMesgResponses(me,mesg));
        }
    }
}