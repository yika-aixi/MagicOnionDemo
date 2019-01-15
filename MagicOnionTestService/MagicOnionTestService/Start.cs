using System;
using Grpc.Core;
using MagicOnion.Server;

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

            // localhost:12345でListen
            var server = new global::Grpc.Core.Server
            {
                Services = { service },
                Ports = { new ServerPort("localhost", 12345, ServerCredentials.Insecure) }
            };

            // MagicOnion起動
            server.Start();
            Console.WriteLine("启动完成...");
            // コンソールアプリが落ちないようにReadLineで待つ
            Console.ReadLine();
        }
    }
}