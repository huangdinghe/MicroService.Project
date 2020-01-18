using Consul;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroService.Client.Utility
{
    public static class ConsulHelper
    {
        /// <summary>
        /// 服务注册
        /// </summary>
        /// <param name="configuration"></param>
        public static void ConsulRegist(this IConfiguration configuration)
        {
            ConsulClient client = new ConsulClient(c=> {
                c.Address = new Uri("http://localhost:8500");
                c.Datacenter = "dc1";
            });

            //命令行传递启动参数
            string ip = configuration["ip"];
            int port = int.Parse(configuration["port"]);
            client.Agent.ServiceRegister(new AgentServiceRegistration()
            {
                ID = "Service_" + Guid.NewGuid(),
                Name = "HumoCloudService",
                Address = ip,
                Port = port,
                Tags = null,
            });
        }



        
    }
}
