using System.Collections.Generic;

namespace Otefa.Domain.Model.Services
{
    public interface IEmailSendingService
    {
        void Send(string subject, string body, string to);
        void Send(string subject, string body, IEnumerable<string> to);
    }
}