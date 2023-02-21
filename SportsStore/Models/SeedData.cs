using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace SportsStore.Models
{
    public class SeedData
    {
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="app"></param>
        public static async void EnsurePopulated(IServiceProvider services)
        {
            ApplicationDbContext context = services.GetRequiredService<ApplicationDbContext>();

            //不推荐使用，此作用域是全局的，若使用这行数据，需要禁用全局的验证机制
            //var context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            //IServiceProvider objManager = app.ApplicationServices;
            //var scope=objManager.CreateScope();
            //using (var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
            //{
            //给我权限，让我能在C#代码中操
            //控数据库（挂起迁移），如果数据库不存在将创建数据库
            //Database：提供上下文的数据库信息和操作的访问权限
            context.Database.Migrate();
            //判断数据库是否包含数据
            if (!context.Products.Any())
            {
                 context.Products.AddRange(new[]
               {
                    new Product { Name = "小米",Price =7261m,
                        Description ="好用，恒牛逼", Category ="电子产品"},

                    new Product { Name = "三只老鼠",Price =11m,
                        Description ="好吃，量很多。", Category ="零食"},

                    new Product { Name = "Asp.Net MVC5",Price =101m,
                        Description ="入门到入院", Category ="书籍"},

                    new Product { Name = "联想电脑",Price =27261m,
                        Description ="7090ti,8k200赫兹", Category ="电子产品"},

                    new Product { Name = "2b水笔",Price =1m,
                        Description ="不会断水的水笔", Category ="文具"},

                    new Product { Name = "卫龙辣条",Price =5m,
                        Description ="够辣够得劲", Category ="零食"},

                    new Product { Name = "索尼PS8",Price =9991m,
                        Description ="索尼独占，不买滚蛋", Category ="电子产品"},

                    new Product { Name = "Asp.Net WebApi",Price =161m,
                        Description ="入院到入土", Category ="书籍"},

                    new Product { Name = "雷蛇鼠标",Price =211m,
                        Description ="人体工学，除了人都觉得工学", Category ="电子产品"}
                });
                 context.SaveChanges();
            }            //} 

        }
    }
}
