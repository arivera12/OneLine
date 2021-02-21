using System.Threading.Tasks;

namespace OneLine.Contracts
{
    public interface IReceiveMessageHub
    {
        Task ReceivePrivateMessage<TMessage>(TMessage message);
        Task ReceiveMessageForAllUsers<TMessage>(TMessage message);
    }
}
