using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Portfoliowebsite.Services
{
    public sealed class SmtpOptions
    {
        public string Host { get; set; } = default!;
        public int Port { get; set; } = 587;
        public bool EnableSsl { get; set; } = true;
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string From { get; set; } = default!;
    }

    public class SmtpEmailSender : IEmailSender
    {
        private readonly SmtpOptions _opt;
        public SmtpEmailSender(IOptions<SmtpOptions> options) => _opt = options.Value;

        public async Task SendAsync(string Name, string Email, string Subject, string Message)
        {
            var smtp = new SmtpClient(_opt.Host, _opt.Port)
            {
                EnableSsl = _opt.EnableSsl,
                Credentials = new NetworkCredential(_opt.UserName, _opt.Password)
            };

            var mail = new MailMessage();
            mail.From = new MailAddress(_opt.From, "Website");

            mail.To.Add("contact@example.com");

            mail.Subject = $"Contact: {Subject}";
            mail.Body = $"Naam: {Name}\nEmail: {Email}\nBericht:\n{Message}";
            mail.IsBodyHtml = false; // Make sure body is in HTML, to avoid XSS

            await smtp.SendMailAsync(mail);
        }
    }
}
