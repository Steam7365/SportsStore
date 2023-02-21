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
            //����EFCore��ȡJson�е������ַ���
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration["Data:SportStoreProducts:ConnectionString"]);
            });
            services.AddDbContext<AppIdentityDbContext>(option =>
            {
                option.UseSqlServer(Configuration["Data:SportStoreIdentity:ConnectionString"]);
            });
            //ÿ����ҪIProductRepository�ӿ�ʱ�ᴴ��EFProductRepositoryʵ����
            services.AddTransient<IProductRepository, EFProductRepository>();
            //ע��Cart��ִ��SessionCart.GetCart()����
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IOrderRepository, EFOrderRepository>();
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();
            services.AddRazorPages();
            services.AddMvc();
            // ???��֪����ɶ
            services.AddMemoryCache();
            //���Session����
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
            //��ʾҳ���쳣��Ϣ
            app.UseDeveloperExceptionPage();
            //����wwwroot��̬�ļ�
            app.UseStaticFiles();
            //http��Ӧ�����һ������Ϣ���磺404-ҳ���Ҳ���
            app.UseStatusCodePages();
            //����ʹ��Session
            app.UseSession();
            //·���м��
            app.UseRouting();
            //��HTTPӦ�ó���ܵ���������֤���ܵ���չ����
            app.UseAuthentication();
            //��������м���������֤����
            app.UseAuthorization();
            //����·��
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
