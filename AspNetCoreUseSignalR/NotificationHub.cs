using Microsoft.AspNetCore.SignalR;

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
            await base.Clients.Client(base.Context.ConnectionId).SendAsync("callClient", "hello", "world");
        }
    }

    /* 使用Redis的Set数据结构存储无序不重复的数据集合
    SADD	    添加元素（返回成功添加数）	SADD tags "java" "redis" 
    SREM	    删除指定元素	            SREM tags "mysql"
    SMEMBERS	获取所有元素	            SMEMBERS tags
    SCARD	    返回元素数量	            SCARD tags
    //*/
}
