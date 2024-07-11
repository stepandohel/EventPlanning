using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace EventPlanning.Email
{
    public class EmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "admin@test.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using var client = new SmtpClient();
            //TODO вынести в апсетинги
            await client.ConnectAsync("localhost", 1025, false);
            await client.AuthenticateAsync(string.Empty, string.Empty);
            await client.SendAsync(emailMessage);

            await client.DisconnectAsync(true);
        }
    }
}
