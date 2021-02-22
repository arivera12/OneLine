using OneLine.Models;
using System.Threading.Tasks;

namespace OneLine.Contracts
{
    public interface IReceiveMessageHub
    {
        Task ReceivePrivateMessage(Notification<object> message);
        Task ReceiveMessageForAllUsers(Notification<object> message);
    }
}
