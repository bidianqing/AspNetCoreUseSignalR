using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace AspNetCoreUseSignalR
{
    public class NotificationHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            string connectionId = base.Context.ConnectionId;

            await Console.Out.WriteLineAsync($"客户端连接，{connectionId}");
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string connectionId = base.Context.ConnectionId;

            await Console.Out.WriteLineAsync($"客户端断开，{connectionId}");
        }

        // 客户端会Invoke这里的方法
        public async Task SendMessage(string userId, string message)
        {
            await Console.Out.WriteLineAsync("收到客户端发来的消息:" + message);
        }
    }
}
