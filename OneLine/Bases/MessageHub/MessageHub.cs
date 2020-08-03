using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public class MessageHub : Hub<IReceiveMessageHub>, ISendMessageHub
    {
        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnDisconnectedAsync(exception);
        }
        public async Task SendMessageToAllUsers<TMessage>(string user, TMessage message)
        {
            await Clients.All.ReceiveMessageToAllUsers(user, message);
        }
        public async Task SendMessageToAllUsersAnonymously<TMessage>(TMessage message)
        {
            await Clients.All.ReceiveMessageToAllUsersAnonymously(message);
        }
        public async Task SendMessageToUser<TMessage>(string userId, TMessage message)
        {
            await Clients.User(userId).ReceiveMessage(message);
        }
    }
}