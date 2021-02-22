using Microsoft.AspNetCore.SignalR;
using OneLine.Contracts;
using OneLine.Models;
using System;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public class MessageHub : Hub<IReceiveMessageHub>, ISendMessageHub
    {
        public MessageHub()
        {

        }
        //public Task AddConnectionToGroup(string connectionId, string groupName)
        //{
        //    return Groups.AddToGroupAsync(connectionId, groupName);
        //}
        //public Task AddUserToGroup(string connectionId, string groupName)
        //{
        //    return Groups.AddToGroupAsync(connectionId, groupName);
        //}
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
        //public Task RemoveConnectionFromGroup(string connectionId, string userId)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task RemoveUserFromAllGroups(string userId)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task RemoveUserFromGroup(string userId, string groupName)
        //{
        //    throw new NotImplementedException();
        //}

        public Task SendMessageToAllUsers(Notification<object> message)
        {
            return Clients.All.ReceiveMessageForAllUsers(message);
        }

        //public Task SendMessageToAllUsersWithinGroup<TMessage>(string groupName, string senderUser, TMessage message)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task SendMessageToConnection<TMessage>(string connectionId, TMessage message)
        //{
        //    throw new NotImplementedException();
        //}

        public Task SendMessageToUser(string receiverUserIdentifier, Notification<object> message)
        {
            return Clients.User(receiverUserIdentifier).ReceivePrivateMessage(message);
        }

        //public Task UserExistsInGroup(string userId, string groupName)
        //{
        //    throw new NotImplementedException();
        //}
    }
}