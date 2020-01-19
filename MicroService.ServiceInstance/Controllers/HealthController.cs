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

        /// <summary>
        /// 健康检测调用接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            _logger.LogInformation(DateTime.Now+"：实例服务健康检查");
            return Ok();
        }


        /// <summary>
        /// 熔断测试接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("polly")]
        public async Task<string> polly()
        {
            _logger.LogInformation(DateTime.Now + "：实例服务熔断测试");
            await Task.Delay(6000);
            return "等待6秒后返回的结果";
        }
    }
}