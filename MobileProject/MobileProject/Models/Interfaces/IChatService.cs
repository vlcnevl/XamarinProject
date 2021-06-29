using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileProject.Models.Interfaces
{
    public interface IChatService
    {
        Task Connect(string userEmail);
        Task Disconnect(string userEmail);
        Task SendMessage(string userId, string message, bool isPrivate = false);
        void ReceiveMessage(Action<string, string> GetMessageAndUser, bool isPrivate = false);
    }
}
