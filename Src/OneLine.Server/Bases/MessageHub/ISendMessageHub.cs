using System.Threading.Tasks;

namespace OneLine.Contracts
{
    /// <summary>
    /// Defines method to send real time messages to users connected to the hub
    /// </summary>
    public interface ISendMessageHub
    {
        Task SendMessageToUser<TMessage>(string userId, TMessage message);
        Task SendMessageToAllUsers<TMessage>(string senderUser, TMessage message);
        Task SendMessageToAllUsersAnonymously<TMessage>(TMessage message);
    }
}
