using OneLine.Models;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface IReceiveMessageHub
    {
        Task ReceivePrivateMessage(Notification<object> message);
        Task ReceiveMessageForAllUsers(Notification<object> message);
    }
}
