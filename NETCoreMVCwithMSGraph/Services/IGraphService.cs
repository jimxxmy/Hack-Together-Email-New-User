using Microsoft.Graph;
using NETCoreMVCwithMSGraph.Models;

namespace HackTogether.WebApp.Services
{
    public interface IGraphService
    {
        Task<HttpResponseMessage> CreateUserAsync(SubScribeModel data);
    }
}
