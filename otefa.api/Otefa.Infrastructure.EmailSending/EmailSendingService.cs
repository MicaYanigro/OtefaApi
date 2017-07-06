using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Linq;
using System;
using Otefa.Infrastructure.IoC;
using Otefa.Domain.Model.Services;

namespace Otefa.Infrastructure.EmailSending
{
    public class EmailSendingService : IEmailSendingService
    {

        [Injectable]
        public ISmtpClientWrapper SmtpClientWrapper { get; set; }

        public void Send(string subject, string body, string to)
        {

            if (to == null)
            {
                throw new ArgumentException("To cannot be null");
            }

            if (!to.Any())
            {
                throw new ArgumentException("To cannot be empty");
            }

            var username = ConfigurationManager.AppSettings["Username"];
            var password = ConfigurationManager.AppSettings["Password"];

            SmtpClientWrapper.Host = ConfigurationManager.AppSettings["Host"];
            SmtpClientWrapper.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
            SmtpClientWrapper.UseDefaultCredentials = false;
            SmtpClientWrapper.Credentials = new NetworkCredential(username, password);

            SmtpClientWrapper.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["EnableSsl"]);
            SmtpClientWrapper.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpClientWrapper.Timeout = int.Parse(ConfigurationManager.AppSettings["Timeout"]);

            SmtpClientWrapper.Send(subject, body, to);

        }

        public void Send(string subject, string body, IEnumerable<string> to)
        {

            if (to == null)
            {

                throw new ArgumentException("To cannot be null");
            }

            if (!to.Any())
            {
                throw new ArgumentException("To cannot be empty");
            }

            var username = ConfigurationManager.AppSettings["Username"];
            var password = ConfigurationManager.AppSettings["Password"];

            SmtpClientWrapper.Host = ConfigurationManager.AppSettings["Host"];
            SmtpClientWrapper.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
            SmtpClientWrapper.UseDefaultCredentials = false;
            SmtpClientWrapper.Credentials = new NetworkCredential(username, password);

            SmtpClientWrapper.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["EnableSsl"]);
            SmtpClientWrapper.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpClientWrapper.Timeout = int.Parse(ConfigurationManager.AppSettings["Timeout"]);

            SmtpClientWrapper.Send(subject, body, to);

        }

    }
}