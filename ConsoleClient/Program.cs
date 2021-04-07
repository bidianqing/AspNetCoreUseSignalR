using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace ConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/notificationhub", options =>
                {
                    options.AccessTokenProvider = async () =>
                    {
                        var httpClient = new HttpClient();
                        var httpResponseMessage = await httpClient.PostAsJsonAsync("http://localhost:5000/account/login", new LoginModel
                        {
                            Account = "sa",
                            Password = "sa"
                        });

                        var result = await httpResponseMessage.Content.ReadFromJsonAsync<LoginResult>();

                        // 只需要返回Token即可 会自动在请求头添加 Authorization = Bearer Token
                        return result.Token;
                    };
                })
                .AddMessagePackProtocol()
                .Build();

            // 服务端会呼叫这里定义的方法
            connection.On<string>("ReceiveMessage", (message) =>
            {
                Console.WriteLine(message);
            });

            await connection.StartAsync();

            Console.WriteLine("客户端已启动");

            await connection.InvokeAsync("SendMessage", "我是客户端");

            Console.ReadKey();
        }
    }
}
