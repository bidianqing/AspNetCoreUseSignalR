using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ConsoleClient
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            var connection = new HubConnectionBuilder()
                .WithUrl("ws://localhost:5112/chathub", options =>
                {
                    
                })
                .ConfigureLogging((loggingBuilder) =>
                {
                    loggingBuilder.SetMinimumLevel(LogLevel.Debug);
                })
                .Build();

            connection.KeepAliveInterval = new TimeSpan(0, 0, 3);

            // 服务端会呼叫这里定义的方法
            connection.On<string, string>("receiveMessage", (message) =>
            {
                Console.WriteLine(message);

                return "back";
            });
            connection.On<string>("logout", (message) =>
            {
                Console.WriteLine(message);
            });

            await connection.StartAsync();

            Console.WriteLine($"客户端已启动，{DateTime.Now}，ConnectionId = {connection.ConnectionId}");

            //await connection.SendAsync("SendMessage", "1117825663852642304", "我是控制台客户端");

            Console.ReadKey();
        }
    }
}
