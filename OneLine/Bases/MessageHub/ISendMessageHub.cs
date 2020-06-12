using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface ISendMessageHub
    {
        Task SendMessageToUser(string user, string message);
        Task SendMessageToAllUsers(string message);
    }
}
