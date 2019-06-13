//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年06月14日-02:55
//Assembly-CSharp

using System;
using Shader.MessageObjects;
using UnityEngine;

namespace Chat.Component
{
    public static class ChatEvents
    {
        public static event Action<JoinOrCreateRoomMesg> JoinRoom;

        public static event Action<string> LeaveRoom;

        public static event Action<SendMesgResponses> SendMessage;

        public static void OnJoinRoom(JoinOrCreateRoomMesg mesg)
        {
            JoinRoom?.Invoke(mesg);
        }
        
        public static void OnLeaveRoom(string userName)
        {
            LeaveRoom?.Invoke(userName);
        }
        
        public static void OnSendMessage(SendMesgResponses mesg)
        {
            SendMessage?.Invoke(mesg);
        }
    }
}