using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace HomeLoanApplication.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpHost = "smtp.gmail.com";  // For Gmail SMTP
        private readonly int _smtpPort = 587;  // For Gmail (use port 465 for SSL)
        private readonly string _smtpUser = "rutujas.pimpalgaonkar@gmail.com";  // Your Gmail email address
        private readonly string _smtpPass = "ggrj mbfu qmvf jyty";  // Gmail password or App Password

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Home Loan Application", _smtpUser));  // "Home Loan Application" is the sender's display name
            message.To.Add(new MailboxAddress("", toEmail));  // The recipient's email address
            message.Subject = subject;  // Subject of the email

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = body  // The body content of the email (HTML format)
            };

            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                try
                {
                    // Connecting to the Gmail SMTP server
                    await client.ConnectAsync(_smtpHost, _smtpPort, false);  // 'false' means no SSL encryption (port 587 uses STARTTLS)
                    await client.AuthenticateAsync(_smtpUser, _smtpPass);  // Authenticating with your email and password
                    await client.SendAsync(message);  // Sending the email
                    await client.DisconnectAsync(true);  // Disconnecting after sending
                }
                catch (Exception ex)
                {
                    throw new Exception("Error sending email: " + ex.Message);  // Error handling
                }
            }
        }
    }

    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string body);
    }
}
