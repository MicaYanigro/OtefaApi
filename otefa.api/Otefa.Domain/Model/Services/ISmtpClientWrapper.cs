using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace Otefa.Domain.Model.Services
{
    public interface ISmtpClientWrapper
    {
        string Host { get; set; }
        int Port { get; set; }
        bool UseDefaultCredentials { get; set; }
        ICredentialsByHost Credentials { get; set; }
        bool EnableSsl { get; set; }
        SmtpDeliveryMethod DeliveryMethod { get; set; }
        int Timeout { get; set; }
        void Send(string subject, string body, string to, IEnumerable<string> replyTo);
        void Send(string subject, string body, IEnumerable<string> to, IEnumerable<string> replyTo);
    }
}