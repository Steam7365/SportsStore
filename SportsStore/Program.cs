using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Run()开始处理HTTP请求
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// 用于配置ASP.NET的方法
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //使用应用程序配置类，约定类的类名为Startup
                    webBuilder.UseStartup<Startup>();
                });
            //.UseDefaultServiceProvider(option => option.ValidateScopes = false);
    }
}
