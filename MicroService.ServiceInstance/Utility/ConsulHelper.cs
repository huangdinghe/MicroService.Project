using Consul;
using Microsoft.Extensions.Configuration;
using System;

namespace MicroService.ServiceInstance.Utility
{
    public static class ConsulHelper
    {
        /// <summary>
        /// 服务注册
        /// </summary>
        /// <param name="configuration"></param>
        public static void ConsulRegist(this IConfiguration configuration)
        {
            ConsulClient client = new ConsulClient(c =>
            {
                c.Address = new Uri("http://localhost:8500");
                c.Datacenter = "dc1";
            });

            #region 命令行传递启动参数记录文本
            //consul.exe agent -dev
            //dotnet MicroService.ServiceInstance.dll --urls="http://*:5726"  --ip="127.0.0.1"  --port=5726
            //dotnet MicroService.ServiceInstance.dll --urls="http://*:5727"  --ip="127.0.0.1"  --port=5727
            //dotnet MicroService.ServiceInstance.dll --urls="http://*:5728"  --ip="127.0.0.1"  --port=5728
            #endregion

            string ip = configuration["ip"] == null ? "127.0.0.1" : configuration["ip"];   //Ip地址
            int port = int.Parse(configuration["port"] == null ? "5726" : configuration["port"]);    //端口号
            client.Agent.ServiceRegister(new AgentServiceRegistration()
            {
                ID = Guid.NewGuid().ToString(),
                Name = "humocloud_service_instance",
                Address = ip,
                Port = port,
                Tags = new string[] { "instance" },
                Check = new AgentServiceCheck()
                {
                    Interval = TimeSpan.FromSeconds(60),
                    HTTP = $"http://{ip}:{port}/api/health/index",
                    Timeout = TimeSpan.FromSeconds(5),
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5)
                }
            });
        }

    }
}
