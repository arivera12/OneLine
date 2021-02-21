using System.Threading.Tasks;

namespace OneLine.Contracts
{
    /// <summary>
    /// Defines method to send real time messages to users connected to the hub
    /// </summary>
    public interface ISendMessageHub
    {
        Task SendMessageToAllUsers<TMessage>(TMessage message);
        Task SendMessageToUser<TMessage>(string receiverUserIdentifier, TMessage message);
        //Task SendMessageToConnection<TMessage>(string connectionId, TMessage message);
        //Task ConnectionExists(string connectionId);
        //Task CloseConnection(string connectionId);
        //Task SendMessageToAllUsersWithinGroup<TMessage>(string groupName, string senderUser, TMessage message);
        //Task AnyConnectionExistsInGroup(string groupName);
        //Task IsUserConnected(string userId);
        //Task AddConnectionToGroup(string connectionId, string groupName);
        //Task RemoveConnectionFromGroup(string connectionId, string groupName);
        //Task UserExistsInGroup(string userId, string groupName);
        //Task AddUserToGroup(string userId, string groupName);
        //Task RemoveUserFromGroup(string userId, string groupName);
        //Task RemoveUserFromAllGroups(string userId);
    }
}
