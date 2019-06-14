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
        public async Task JoinChat(JoinOrCreateRoomMesg mesg)
        {
            //获取房间
            this.room = RoomManager.GetRoom(mesg.RoomName);

            if (room == null)
            {
                Console.WriteLine($"no Room {mesg.RoomName}");
                return;
            }
            
            var count = await room.GetMemberCountAsync();
            
            Console.WriteLine("Current Chat Player Count: " + count);
            
            //保存名字
            me = mesg.UserName;

            //广播消息:加入房间
            Broadcast(room).OnJoinChat(mesg);
            
            Console.WriteLine($"{mesg.UserName} Join {mesg.RoomName} Chat. {mesg.RoomName} Chat Player Count : {count}");
        }

        public async Task LeaveChat()
        {
            //离开房间
            var result = await RoomManager.LeaveRoom(room, Context);

            if (!result)
            {
                return;
            }
            
            //广播消息:离开聊天
            this.Broadcast(room).OnLeaveChat(me);
            
            Console.WriteLine($"{me} Leave {room.GroupName} Chat");
        }

#pragma warning disable 1998
        public async Task SendMessage(SendMesg mesg)
#pragma warning restore 1998
        {
            Console.WriteLine($"{me} Send Message : {mesg.Message}");

            //广播消息:发送消息
            this.BroadcastExceptSelf(room).OnSendMessage(new SendMesgResponses(me,mesg));
        }
    }
}