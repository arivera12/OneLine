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

        public async Task SendMessageToAllUsers(string message)
        {
            await Clients.All.ReceiveMessageToAllUsers(message);
        }
        public async Task SendMessageToUser(string user, string message)
        {
            await Clients.Caller.ReceiveMessageToUser(user, message);
        }
    }
}