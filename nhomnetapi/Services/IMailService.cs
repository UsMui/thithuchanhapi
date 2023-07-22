using nhomnetapi.Models;


namespace nhomnetapi.Services
{
    public interface IMailService
    {
        Task<bool> SendAsync(MailData mailData, CancellationToken ct);

    }
}
