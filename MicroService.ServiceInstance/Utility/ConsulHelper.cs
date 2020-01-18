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

            //命令行传递启动参数
            //dotnet MicroService.ServiceInstance.dll --urls="http://*:5726"  --ip="127.0.0.1"  --port=5726 --weight=1
            //dotnet MicroService.ServiceInstance.dll --urls="http://*:5727"  --ip="127.0.0.1"  --port=5727 --weight=2
            //dotnet MicroService.ServiceInstance.dll --urls="http://*:5728"  --ip="127.0.0.1"  --port=5728 --weight=3
            //consul.exe agent -dev
            string ip = configuration["ip"] == null ? "127.0.01" : configuration["ip"];   //Ip地址
            int port = int.Parse(configuration["port"] == null ? "5726" : configuration["port"]);    //端口号
            int weight = int.Parse(configuration["weight"] == null ? "1" : configuration["weight"]);       //权重
            client.Agent.ServiceRegister(new AgentServiceRegistration()
            {
                ID = "Service_" + Guid.NewGuid(),
                Name = "HumoCloudServiceInstance",
                Address = ip,
                Port = port,
                Tags = new string[] { weight.ToString() },
                Check = new AgentServiceCheck()
                {
                    Interval = TimeSpan.FromSeconds(12),
                    HTTP = $"http://{ip}:{port}/api/health/index",
                    Timeout = TimeSpan.FromSeconds(5),
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5)
                }
            });
        }

    }
}
