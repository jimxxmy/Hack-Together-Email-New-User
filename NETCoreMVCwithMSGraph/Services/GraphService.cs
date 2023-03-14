using Azure.Identity;
using Microsoft.Graph;

namespace HackTogether.WebApp.Services
{
    public class GraphService : IGraphService
    {
        private readonly IConfiguration _configuration;
        public GraphService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<GraphServiceClient> Authorize()
        {
            var scopes = new[] { "User.Read" };

            // Multi-tenant apps can use "common",
            // single-tenant apps must use the tenant ID from the Azure portal
            var tenantId = _configuration.GetSection("AzureAd").GetConnectionString("TenantId");

            // Values from app registration
            var clientId = _configuration.GetSection("AzureAd").GetConnectionString("ClientId");
            var clientSecret = _configuration.GetSection("AzureAd").GetConnectionString("ClientSecret");

            // For authorization code flow, the user signs into the Microsoft
            // identity platform, and the browser is redirected back to your app
            // with an authorization code in the query parameters
            var authorizationCode = _configuration.GetSection("AzureAd").GetConnectionString("CallbackPath");

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

        //public async Task<HttpResponseMessage> CreateUserAsync()
        //{
        //    var graphClient = Authorize();

        //    var requestBody = new User
        //    {
        //        AccountEnabled = true,
        //        DisplayName = "Adele Vance",
        //        MailNickname = "AdeleV",
        //        UserPrincipalName = "AdeleV@contoso.onmicrosoft.com",
        //        PasswordProfile = new PasswordProfile
        //        {
        //            ForceChangePasswordNextSignIn = true,
        //            Password = "xWwvJ]6NMw+bWH-d",
        //        },
        //    };
        //    var result = await graphClient.User
        //}
    }
}
