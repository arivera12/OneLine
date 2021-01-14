using System.Threading.Tasks;

namespace OneLine.Contracts
{
    public interface IReceiveMessageHub
    {
        Task ReceiveMessage<TMessage>(TMessage message);
        Task ReceiveMessageToUser<TMessage>(string senderUser, TMessage message);
        Task ReceiveMessageToAllUsers<TMessage>(string senderUser, TMessage message);
        Task ReceiveMessageToAllUsersAnonymously<TMessage>(TMessage message);
    }
}
