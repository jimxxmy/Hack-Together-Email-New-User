using Microsoft.Graph;

namespace HackTogether.WebApp.Services
{
    public interface IGraphService
    {
        Task<GraphServiceClient> Authorize();
        //Task<HttpResponseMessage> CreateUserAsync();
    }
}
