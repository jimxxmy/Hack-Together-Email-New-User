namespace HackTogether.WebApp.Services
{
    public interface IMailer
    {
        Task SendMailToUser(string ClientName, string ClientEmail);
    }
}
