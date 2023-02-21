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
            //Run()��ʼ����HTTP����
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// ��������ASP.NET�ķ���
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //ʹ��Ӧ�ó��������࣬Լ���������ΪStartup
                    webBuilder.UseStartup<Startup>();
                });
            //.UseDefaultServiceProvider(option => option.ValidateScopes = false);
    }
}
