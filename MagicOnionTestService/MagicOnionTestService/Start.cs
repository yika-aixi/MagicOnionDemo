using System;
using Grpc.Core;
using MagicOnion.Server;
using StackExchange.Redis;

namespace MagicOnionTestService
{
    class Start
    {
        static void Main(string[] args)
        {
            Console.WriteLine("启动服务器...");
            //コンソールにログを表示させる
            GrpcEnvironment.SetLogger(new Grpc.Core.Logging.ConsoleLogger());

            var service = MagicOnionEngine.BuildServerServiceDefinition(isReturnExceptionStackTraceInErrorDetail: true);

            var serverPort = new ServerPort("localhost", 12345, ServerCredentials.Insecure);
            
            // localhost:12345でListen
            var server = new Server
            {
                Services = { service },
                Ports = { serverPort }
            };

            // MagicOnion起動
            server.Start();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"启动完成... {serverPort.Host}:{serverPort.Port}");
            Console.ForegroundColor = ConsoleColor.White;
            
            // コンソールアプリが落ちないようにReadLineで待つ
            Console.ReadLine();
        }
    }
}