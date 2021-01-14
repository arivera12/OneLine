using Microsoft.AspNetCore.SignalR;
using OneLine.Contracts;
using System;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public class MessageHub : Hub<IReceiveMessageHub>, ISendMessageHub
    {
        public override async Task OnConnectedAsync()
        {
            //TODO: Poner online el usuario
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            //TODO: Poner offline el usuario
            await base.OnDisconnectedAsync(exception);
        }
        public async Task SendMessageToAllUsers<TMessage>(string senderUser, TMessage message)
        {
            await Clients.All.ReceiveMessageToAllUsers(senderUser, message);
        }
        public async Task SendMessageToAllUsersAnonymously<TMessage>(TMessage message)
        {
            await Clients.All.ReceiveMessageToAllUsersAnonymously(message);
        }
        public async Task SendMessageToUser<TMessage>(string senderUser, TMessage message)
        {
            await Clients.User(senderUser).ReceiveMessage(message);
        }
    }
}