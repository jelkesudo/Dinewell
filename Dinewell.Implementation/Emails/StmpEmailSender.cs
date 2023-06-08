using Dinewell.Application.Emails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Implementation.Emails
{
    public class SmtpEmailSender : IEmailSender
    {
        private string _fromEmail;
        private string _password;
        private int _port;
        private string _host;

        public SmtpEmailSender(string fromEmail, string password, int port, string host)
        {
            _fromEmail = fromEmail;
            _password = password;
            _port = port;
            _host = host;
        }

        public void Send(MessageDTO message)
        {
            var smtp = new SmtpClient
            {
                Host = _host,
                Port = _port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(_fromEmail, _password),
                UseDefaultCredentials = false
            };
            
            var messageSend = new MailMessage(_fromEmail, message.To);
            messageSend.Subject = message.Title;
            messageSend.Body = message.Body;
            messageSend.IsBodyHtml = true;
            smtp.Send(messageSend);
        }
    }
}
