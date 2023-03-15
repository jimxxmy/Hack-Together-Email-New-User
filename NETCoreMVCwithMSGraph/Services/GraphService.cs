using Azure.Identity;
using Microsoft.Graph;
using NETCoreMVCwithMSGraph.Models;

namespace HackTogether.WebApp.Services
{
    public class GraphService : IGraphService
    {
        private readonly IConfiguration _configuration;
        private readonly IMailer _mailer;
        public GraphService(IConfiguration configuration, IMailer mailer)
        {
            _configuration = configuration;
            _mailer = mailer;
        }

        public async Task<HttpResponseMessage> CreateUserAsync(SubScribeModel data)
        {
            var scopes = new[] { "Directory.ReadWrite.All" };

            // Multi-tenant apps can use "common",
            // single-tenant apps must use the tenant ID from the Azure portal
            var tenantId = _configuration.GetSection("AzureAd")["TenantId"];

            // Values from app registration
            var clientId = _configuration.GetSection("AzureAd")["ClientId"];
            var clientSecret = _configuration.GetSection("AzureAd")["ClientSecret"];

            // For authorization code flow, the user signs into the Microsoft
            // identity platform, and the browser is redirected back to your app
            // with an authorization code in the query parameters
            var authorizationCode = "";

            // using Azure.Identity;
            //var options = new TokenCredentialOptions
            //{
            //    AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            //};

            // https://learn.microsoft.com/dotnet/api/azure.identity.authorizationcodecredential
            //var authCodeCredential = new AuthorizationCodeCredential(
            //    tenantId, clientId, clientSecret, authorizationCode, options);

            var interactiveBrowserCredentialOptions = new InteractiveBrowserCredentialOptions
            {
                ClientId = clientId,
                TenantId = tenantId,
                RedirectUri = new Uri("http://localhost"),
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
            };
            var tokenCredential = new InteractiveBrowserCredential(interactiveBrowserCredentialOptions);
            var graphClient = new GraphServiceClient(tokenCredential, scopes);
            

            var requestBody = new User
            {
                AccountEnabled = true,
                DisplayName = data.FirstName,
                MailNickname = data.NickName,
                UserPrincipalName = $"{data.FirstName}@w5w0b.onmicrosoft.com",
                PasswordProfile = new PasswordProfile
                {
                    ForceChangePasswordNextSignIn = true,
                    Password = data.Password,
                }
            };
            try
            {
                var result = await graphClient.Users.Request().AddAsync(requestBody);
                await _mailer.SendMailToUser(requestBody.UserPrincipalName, requestBody.DisplayName);

            }catch(Exception ex)
            {

            }
            
            //return result;
            return null;
        }

        public Task<HttpResponseMessage> CreateUserAsync()
        {
            throw new NotImplementedException();
        }
    }
}
