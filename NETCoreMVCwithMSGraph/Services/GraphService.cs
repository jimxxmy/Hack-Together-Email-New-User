using Azure.Identity;
using Microsoft.Graph;
using NETCoreMVCwithMSGraph.Models;

namespace HackTogether.WebApp.Services
{
    public class GraphService : IGraphService
    {
        private readonly IConfiguration _configuration;
        public GraphService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public GraphServiceClient Authorize()
        {
            var scopes = new[] { "User.Read" };

            // Multi-tenant apps can use "common",
            // single-tenant apps must use the tenant ID from the Azure portal
            var tenantId = _configuration.GetSection("AzureAd")["TenantId"];

            // Values from app registration
            var clientId = _configuration.GetSection("AzureAd")["ClientId"];
            var clientSecret = _configuration.GetSection("AzureAd")["ClientSecret"];

            // For authorization code flow, the user signs into the Microsoft
            // identity platform, and the browser is redirected back to your app
            // with an authorization code in the query parameters
            var authorizationCode = _configuration.GetSection("AzureAd")["CallbackPath"];

            // using Azure.Identity;
            var options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            };

            // https://learn.microsoft.com/dotnet/api/azure.identity.authorizationcodecredential
            var authCodeCredential = new AuthorizationCodeCredential(
                tenantId, clientId, clientSecret, authorizationCode, options);

            var graphClient = new GraphServiceClient(authCodeCredential, scopes);
            return graphClient;
        }

        public async Task<HttpResponseMessage> CreateUserAsync(SubScribeModel data)
        {
            //var graphClient = Authorize();
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
            var authorizationCode = _configuration.GetSection("AzureAd")["CallbackPath"];

            // using Azure.Identity;
            var options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            };

            // https://learn.microsoft.com/dotnet/api/azure.identity.authorizationcodecredential
            var authCodeCredential = new AuthorizationCodeCredential(
                tenantId, clientId, clientSecret, authorizationCode, options);

            var graphClient = new GraphServiceClient(authCodeCredential, scopes);
            

            var requestBody = new User
            {
                AccountEnabled = true,
                DisplayName = data.FirstName,
                MailNickname = data.NickName,
                UserPrincipalName = $"{data.Email}@contoso.onmicrosoft.com",
                PasswordProfile = new PasswordProfile
                {
                    ForceChangePasswordNextSignIn = true,
                    Password = data.Password,
                },
            };
            try
            {
                var result = await graphClient.Users.Request().AddAsync(requestBody);
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
