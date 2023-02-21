using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //配置EFCore读取Json中的连接字符串
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration["Data:SportStoreProducts:ConnectionString"]);
            });
            services.AddDbContext<AppIdentityDbContext>(option =>
            {
                option.UseSqlServer(Configuration["Data:SportStoreIdentity:ConnectionString"]);
            });
            //每次需要IProductRepository接口时会创建EFProductRepository实现类
            services.AddTransient<IProductRepository, EFProductRepository>();
            //注入Cart会执行SessionCart.GetCart()方法
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IOrderRepository, EFOrderRepository>();
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();
            services.AddRazorPages();
            services.AddMvc();
            // ???不知道是啥
            services.AddMemoryCache();
            //添加Session服务
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            //显示页面异常信息
            app.UseDeveloperExceptionPage();
            //启用wwwroot静态文件
            app.UseStaticFiles();
            //http响应种添加一条简单信息。如：404-页面找不到
            app.UseStatusCodePages();
            //允许使用Session
            app.UseSession();
            //路由中间件
            app.UseRouting();
            //向HTTP应用程序管道添加身份验证功能的扩展方法
            app.UseAuthentication();
            //启用身份中间件，身份验证功能
            app.UseAuthorization();
            //配置路由
            app.UseEndpoints(routs =>
            {
                routs.MapControllerRoute(
                    name: null,
                    pattern: "{category}/Page{productPage:int}",
                    defaults: new { controller = "Product", action = "List" }
                    );
                routs.MapControllerRoute(
                    name: null,
                    pattern: "Page{productPage:int}",
                    defaults: new { Controller = "Product", Action = "List", productPage = 1 }
                    );
                routs.MapControllerRoute(
                    name: null,
                    pattern: "{category}",
                    defaults: new { controller = "Product", action = "List", productPage = 1 }
                    );
                routs.MapControllerRoute(
                    name: null,
                    pattern: "",
                    defaults: new { Controller = "Product", Action = "List", productPage = 1 }
                    );
                routs.MapControllerRoute(
                    name: null,
                    pattern: "{controller=Product}/{action=List}/{id?}"
                    );
            });

            //SeedData.EnsurePopulated(app);
            //IdentitySeedData.EnsurePopulated(app);
        }
    }
}
