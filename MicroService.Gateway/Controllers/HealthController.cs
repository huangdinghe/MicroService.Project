using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace MicroService.Gateway.Controllers
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
            _logger.LogInformation(DateTime.Now + "：网关服务健康检查");
            return Ok();
        }
    }
}