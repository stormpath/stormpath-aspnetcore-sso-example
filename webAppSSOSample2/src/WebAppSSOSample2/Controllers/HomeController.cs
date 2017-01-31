using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Stormpath.Owin.Abstractions;
using Stormpath.SDK.Application;
using Stormpath.SDK.Client;

namespace WebAppSSOSample2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IClient _client;
        private readonly IApplication _application;

        public HomeController(IClient client, IApplication application)
        {
            _client = client;
            _application = application;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult SwitchApplication()
        {
            var stateToken = new StateTokenBuilder(_client, _client.Configuration.Client.ApiKey)
                .ToString();
            var uri = _application.NewIdSiteUrlBuilder()
                .SetCallbackUri("http://localhost:54919/stormpathCallback")
                .SetState(stateToken)
                .Build();
            return Redirect(uri);
        }
    }
}
