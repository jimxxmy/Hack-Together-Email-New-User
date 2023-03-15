using Microsoft.Graph;
using NETCoreMVCwithMSGraph.Models;

namespace HackTogether.WebApp.Services
{
    public interface IGraphService
    {
        GraphServiceClient Authorize();
        Task<HttpResponseMessage> CreateUserAsync(SubScribeModel data);
    }
}
