using System;
using Grpc.Core;
using MagicOnion.Client;
using MagicOnionTestService.LobbyMessageTest;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //初始化
            var channel = new Channel("localhost:12345", ChannelCredentials.Insecure);
            var receiverChat = new ReceiverChat();
            var chatHub = StreamingHubClient.Connect<IChatHub, IChat>(channel, receiverChat);

            chatHub.JoinAsync("Console Player", "Console Room");

            Console.ReadLine();
        }

        class ReceiverChat:IChat
        {
            public void OnJoin(string name)
            {
                Console.WriteLine($"{name} Join.");
            }

            public void OnLeave(string name)
            {
                Console.WriteLine($"{name} Leave.");
            }

            public void OnSendMessage(string name, string message)
            {
                Console.WriteLine($"{name}: {message}");
            }
        }
    }
}