using SendGrid;
using SendGrid.Helpers.Mail;

namespace HackTogether.WebApp.Services
{
    public class Mailer : IMailer
    {
        private readonly IConfiguration _configuration;

        public Mailer(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendMailToUser(string ClientName, string ClientEmail)
        {
            var apiKey = _configuration.GetSection("SENDGRID_API_KEY").Value;
            var client = new SendGridClient(apiKey);
            string MyEmail = "jimxmy@w5w0b.onmicrosoft.com";
            string MyName = "Admin@MSFT";
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(MyEmail, MyName),
                Subject = "Sending with Twilio SendGrid is Fun",
                PlainTextContent = "and easy to do anywhere, especially with C#"
            };
            msg.AddTo(new EmailAddress(ClientEmail, ClientName));
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            var abc = response.Body;
        }
    }
}
