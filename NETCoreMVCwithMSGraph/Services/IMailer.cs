namespace HackTogether.WebApp.Services
{
    public interface IMailer
    {
        Task SendMail(string Email);
    }
}
