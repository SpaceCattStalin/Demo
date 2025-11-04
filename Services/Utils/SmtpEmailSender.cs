using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Services.Utils
{
    public class SmtpEmailSender : Services.Interfaces.IEmailSender
    {
        private readonly IConfiguration _cfg;
        public SmtpEmailSender(IConfiguration cfg) => _cfg = cfg;

        public async Task SendAsync(string toEmail, string subject, string htmlBody)
        {
            var host = _cfg["Smtp:Host"]!;
            var port = int.Parse(_cfg["Smtp:Port"]!);
            var user = _cfg["Smtp:User"]!;
            var pass = _cfg["Smtp:Pass"]!;
            var from = _cfg["Smtp:From"] ?? user;
            var fromName = _cfg["Smtp:FromName"] ?? "No-Reply";
            var enableSsl = bool.TryParse(_cfg["Smtp:EnableSsl"], out var ssl) ? ssl : true;
            var timeoutMs = int.TryParse(_cfg["Smtp:TimeoutMs"], out var t) ? t : 10000;

            using var client = new SmtpClient(host, port)
            {
                EnableSsl = enableSsl,
                Credentials = new NetworkCredential(user, pass),
                Timeout = timeoutMs
            };

            using var msg = new MailMessage
            {
                From = new MailAddress(from, fromName),
                Subject = subject,
                Body = htmlBody,
                IsBodyHtml = true
            };
            msg.To.Add(toEmail);

            await client.SendMailAsync(msg);
        }
    }
}
