using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface IReceiveMessageHub
    {
        Task ReceiveMessage<TMessage>(TMessage message);
        Task ReceiveMessageToUser<TMessage>(string user, TMessage message);
        Task ReceiveMessageToAllUsers<TMessage>(string user, TMessage message);
        Task ReceiveMessageToAllUsersAnonymously<TMessage>(TMessage message);
    }
}
