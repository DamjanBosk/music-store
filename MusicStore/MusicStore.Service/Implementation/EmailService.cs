namespace MusicStore.Service.Implementation
{
    using Microsoft.Extensions.Options;
    using MusicStore.Domain.Email;
    using MusicStore.Service.Interface;
    using MailKit.Security;
    using MimeKit;
    using System.Net.Mail;

    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;

        public EmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(EmailMessage allMails)
        {
            var emailMessage = new MimeMessage
            {
                Sender = new MailboxAddress("Music Store Application", "some_email@outlook.com"),
                Subject = allMails.Subject
            };

            emailMessage.From.Add(new MailboxAddress("Music Store Application", "some_email@outlook.com"));

            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Plain) { Text = allMails.Content };

            emailMessage.To.Add(new MailboxAddress(allMails.MailTo, allMails.MailTo));

            try
            {
                using (var smtp = new MailKit.Net.Smtp.SmtpClient())
                {
                    var socketOptions = SecureSocketOptions.Auto;

                    await smtp.ConnectAsync(_mailSettings.SmtpServer, 587, socketOptions);

                    if (!string.IsNullOrEmpty(_mailSettings.SmtpUserName))
                    {
                        await smtp.AuthenticateAsync(_mailSettings.SmtpUserName, _mailSettings.SmtpPassword);
                    }
                    await smtp.SendAsync(emailMessage);


                    await smtp.DisconnectAsync(true);
                }
            }
            catch (SmtpException ex)
            {
                throw ex;
            }
        }
    }
}

