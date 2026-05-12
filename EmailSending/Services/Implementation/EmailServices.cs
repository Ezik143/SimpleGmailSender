using EmailSending.Configuration;
using EmailSending.Entities;
using MimeKit;
using MailKit.Net.Smtp;        // ← Replace System.Net.Mail with this
using MailKit.Security;
using EmailSending.Intefaces;        // ← For SecureSocketOptions

namespace EmailSending.Services
{
    public class EmailServices : IEmailService
    {
        private readonly EmailConfiguration _emailConfig;
        public EmailServices(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }
        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            send(emailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(MailboxAddress.Parse(_emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

            return emailMessage;
        }

        private void send(MimeMessage emailMessage)
        {
            using (var client = new SmtpClient())
            {
                client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, SecureSocketOptions.StartTls);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfig.Username, _emailConfig.Password);
                client.Send(emailMessage);
                client.Disconnect(true);
            }
        }

    }
}
