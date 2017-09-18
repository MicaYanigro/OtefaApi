using System.Collections.Generic;

namespace Otefa.Domain.Model.Services
{
    public interface IEmailSendingService
    {
        void Send(string body, string replyTo);
        void Send(string subject, string body, IEnumerable<string> to, string replyTo);
    }
}