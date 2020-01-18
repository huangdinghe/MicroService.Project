using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace MicroService.ServiceInstance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        private readonly ILogger<HealthController> _logger;
        public HealthController(ILogger<HealthController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            _logger.LogInformation(DateTime.Now+"：Consul执行对服务ServiceInstance的健康检查");
            return Ok();
        }
    }
}