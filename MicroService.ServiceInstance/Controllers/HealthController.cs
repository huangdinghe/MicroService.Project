using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

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
            _logger.LogInformation(DateTime.Now+"：实例服务健康检查");
            return Ok();
        }

        [HttpGet]
        [Route("polly")]
        public async Task<string> polly()
        {
            _logger.LogInformation(DateTime.Now + "：实例服务熔断测试");
            await Task.Delay(6000);
            return "wait 6000 polly";
        }
    }
}