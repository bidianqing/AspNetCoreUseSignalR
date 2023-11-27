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
                    loggingBuilder.AddConsole();
                    loggingBuilder.SetMinimumLevel(LogLevel.Debug);
                })
                .Build();

            // 服务端会呼叫这里定义的方法
            connection.On<string>("receiveMessage", (message) =>
            {
                Console.WriteLine($"{DateTime.Now} -- {message}");
            });
            connection.On<string>("logout", (message) =>
            {
                Console.WriteLine(message);
            });

            await connection.StartAsync();

            Console.WriteLine($"客户端已启动，{DateTime.Now}，ConnectionId = {connection.ConnectionId}");

            Console.ReadKey();
        }
    }
}
