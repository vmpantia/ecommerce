using ECommerce.BAL.Contractors;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace ECommerce.BAL.Utilities
{
    public class EmailUtil : IEmailUtil
    {
        private readonly IConfiguration _config;
        public EmailUtil(IConfiguration config) => _config = config;

        public async Task SendEmail(string to, string subject, string body)
        {
            var email = new MimeMessage();
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };
            await Send(email);
        }

        public async Task SendEmailToMany(IEnumerable<string> tos,
                                          IEnumerable<string>? ccs,
                                          IEnumerable<string>? bccs,
                                          string subject,
                                          string body)
        {
            var email = new MimeMessage();

            foreach (var to in tos)
                email.To.Add(MailboxAddress.Parse(to));

            if (ccs != null)
                foreach (var to in ccs)
                    email.Cc.Add(MailboxAddress.Parse(to));

            if (bccs != null)
                foreach (var to in bccs)
                    email.Bcc.Add(MailboxAddress.Parse(to));

            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };
            await Send(email);
        }

        private async Task Send(MimeMessage email)
        {
            //Set From
            email.From.Add(MailboxAddress.Parse(_config["Email:Username"]));

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_config["Email:Smtp"], 587, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_config["Email:Username"], _config["Email:Password"]);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
