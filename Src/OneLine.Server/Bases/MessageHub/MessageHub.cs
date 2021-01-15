using Microsoft.AspNetCore.SignalR;
using OneLine.Contracts;
using System;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public class MessageHub : Hub<IReceiveMessageHub>, ISendMessageHub
    {
        public Task AddConnectionToGroup(string connectionId, string groupName)
        {
            return Groups.AddToGroupAsync(connectionId, groupName);
        }
        public Task AddUserToGroup(string connectionId, string groupName)
        {
            return Groups.AddToGroupAsync(connectionId, groupName);
        }
        public override Task OnConnectedAsync()
        {
            //TODO: Poner online el usuario
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            //TODO: Poner offline el usuario
            return base.OnDisconnectedAsync(exception);
        }
        public Task RemoveConnectionFromGroup(string connectionId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveUserFromAllGroups(string userId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveUserFromGroup(string userId, string groupName)
        {
            throw new NotImplementedException();
        }

        public Task SendMessageToAllUsers<TMessage>(string senderUser, TMessage message)
        {
            return Clients.All.ReceiveMessageToAllUsers(senderUser, message);
        }

        public Task SendMessageToAllUsersWithinGroup<TMessage>(string groupName, string senderUser, TMessage message)
        {
            throw new NotImplementedException();
        }

        public Task SendMessageToConnection<TMessage>(string connectionId, TMessage message)
        {
            throw new NotImplementedException();
        }

        public Task SendMessageToUser<TMessage>(string senderUser, TMessage message)
        {
            return Clients.User(senderUser).ReceiveMessage(message);
        }

        public Task UserExistsInGroup(string userId, string groupName)
        {
            throw new NotImplementedException();
        }
    }
}