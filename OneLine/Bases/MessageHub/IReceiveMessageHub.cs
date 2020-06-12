using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface IReceiveMessageHub
    {
        Task ReceiveMessageToUser(string user, string message);
        Task ReceiveMessageToAllUsers(string message);
    }
}
