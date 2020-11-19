using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface ISendMessageHub
    {
        Task SendMessageToUser<TMessage>(string senderUser, TMessage message);
        Task SendMessageToAllUsers<TMessage>(string senderUser, TMessage message);
        Task SendMessageToAllUsersAnonymously<TMessage>(TMessage message);
    }
}
