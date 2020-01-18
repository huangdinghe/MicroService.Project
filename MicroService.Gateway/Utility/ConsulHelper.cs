using Consul;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroService.Gateway.Utility
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
            //dotnet MicroService.Gateway.dll --urls="http://*:6300"  --ip="127.0.0.1"  --port=6300
            #endregion

            string ip = configuration["ip"] == null ? "127.0.01" : configuration["ip"];   //Ip地址
            int port = int.Parse(configuration["port"] == null ? "6300" : configuration["port"]);    //端口号
         
            client.Agent.ServiceRegister(new AgentServiceRegistration()
            {
                ID = Guid.NewGuid().ToString(),
                Name = "humocloud_service_gateway",
                Address = ip,
                Port = port,
                Tags = new string[] { "gateway" }
            });
        }
    }
}
