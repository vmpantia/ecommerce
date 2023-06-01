using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MimeKit;

namespace ECommerce.Common.Utils
{
    public static class EmailUtil
    {
        private static IConfiguration _config { get; set; }
        static EmailUtil()
        {
            var builder = new HostBuilder()
            .ConfigureAppConfiguration((hostContext, configBuilder) =>
            {
                configBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                // Add any other configuration sources as needed
            })
            .Build();

            _config = builder.Services.GetRequiredService<IConfiguration>();
        }

        public static async Task SendEmail(string to, string subject, string body)
        {
            var email = new MimeMessage();
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };
            await Send(email);
        }

        public static async Task SendEmailToMany(IEnumerable<string> tos,
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

        private static async Task Send(MimeMessage email)
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
