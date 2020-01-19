using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MicroService.Client.Models;
using Consul;

namespace MicroService.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //发现服务实例
            //using (ConsulClient client = new ConsulClient(c =>
            //{
            //    c.Address = new Uri("http://localhost:8500");
            //    c.Datacenter = "dc1";
            //}))
            //{
            //    var dictionary = client.Agent.Services().Result.Response;
            //    foreach (var keyValuePair in dictionary)
            //    {
            //        AgentService agentService = keyValuePair.Value;
                    
            //    }
            //}

                return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
