using HackTogether.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using NETCoreMVCwithMSGraph.Models;
using System.Diagnostics;

namespace NETCoreMVCwithMSGraph.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IGraphService _graphService;
        public HomeController(ILogger<HomeController> logger,IGraphService graphService)
        {
            _logger = logger;
            _graphService = graphService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(SubScribeModel cust)
        {
            
            
            //var graphClient = obj.Authorize();
            string customerId = cust.FirstName;
            string customerName = cust.lastName;
            string customerAddress = cust.Email;
            string customerPassword = cust.Password;
            string customerNickName = cust.NickName;

            _graphService.CreateUserAsync(cust);
        //    var body = new User
        //    {
        //        AccountEnabled = true,
        //        DisplayName = customerId,
        //        MailNickname = customerNickName,
        //        UserPrincipalName = $"{customerName}@w5w0b.onmicrosoft.com",
        //        PasswordProfile = new PasswordProfile
        //        {
        //            ForceChangePasswordNextSignIn = true,
        //            Password = customerPassword,
        //        },
        //};
        //    var result = await graphClient.;
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public GraphServiceClient Authorize()
        {
            throw new NotImplementedException();
        }

       
    }
}