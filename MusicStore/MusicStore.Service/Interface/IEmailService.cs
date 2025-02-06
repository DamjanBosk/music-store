namespace MusicStore.Service.Interface
{
    using MusicStore.Domain.Email;

    public interface IEmailService
    {
        Task SendEmailAsync(EmailMessage allMails);
    }

}
