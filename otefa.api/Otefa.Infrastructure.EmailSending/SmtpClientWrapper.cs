using System.Net.Mail;
using System.Collections.Generic;
using System.Configuration;
using Otefa.Domain.Model.Services;

namespace Otefa.Infrastructure.EmailSending
{
    public class SmtpClientWrapper : SmtpClient, ISmtpClientWrapper
    {

        public void Send(string subject, string body, string to, string replyTo)
        {

            var from = ConfigurationManager.AppSettings["fromAddress"];

            using (var message = new MailMessage()
            {
                From = new MailAddress(from),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
               message.To.Add(to);

               base.Send(message);
            }

        }

        public void Send(string subject, string body, IEnumerable<string> to, string replyTo)
        {

            var from = ConfigurationManager.AppSettings["fromAddress"];

            using (var message = new MailMessage()
            {
                From = new MailAddress(from),
                ReplyTo = new MailAddress(replyTo),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {

                foreach (var email in to)
                {
                    message.To.Add(email);
                }

                base.Send(message);

            }

        }

    }
}