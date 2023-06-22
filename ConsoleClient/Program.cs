using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Collections;

namespace ConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // wss://bidianqing.natappvip.cc/chathub
            // ws://localhost:7282/chathub

            var connection = new HubConnectionBuilder()
                .WithUrl("ws://localhost:7282/chathub", options =>
                {
                    options.Headers = new Dictionary<string, string>
                    {
                        { "Platform","console" }
                    };
                    options.AccessTokenProvider = async () =>
                    {
                        var httpClient = new HttpClient();
                        var httpResponseMessage = await httpClient.PostAsJsonAsync("https://localhost:5001/login", new LoginModel
                        {
                            Phone = "18515278856",
                            Code = "0000"
                        });

                        if (!httpResponseMessage.IsSuccessStatusCode) return string.Empty;

                        var json = await httpResponseMessage.Content.ReadAsStringAsync();
                        JObject obj = JObject.Parse(json);

                        // 只需要返回Token即可 会自动在请求头添加 Authorization = Bearer Token
                        return obj["data"]["token"].ToString();
                    };
                })
                .ConfigureLogging((loggingBuilder) =>
                {

                })
                .Build();

            // 服务端会呼叫这里定义的方法
            connection.On<string>("receiveMessage", (message) =>
            {
                Console.WriteLine(message);
            });

            await connection.StartAsync();

            Console.WriteLine($"客户端已启动，{DateTime.Now}，ConnectionId = {connection.ConnectionId}");

            await connection.InvokeAsync("SendMessage", "1117825663852642304", "我是控制台客户端");

            Console.ReadKey();
        }
    }
}
