using FakexiechengAPI.Database;
using FakexiechengAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FakexiechengAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();//ע��MVC Controller
            services.AddTransient<ITouristRouteRepository, TouristRouteRepository>();//����ע�룬ÿ�����󶼻�ע��һ���µ����ݲֿ�
            //services.AddSingleton //ֻ�ڳ������󴴽�һ�����ݲֿ⣬֮��ÿһ��ʹ����ͬ�ģ��ڴ�ռ���٣�Ч�ʸߣ����ǿ��ܻ����������Ⱦ��
            //services.AddScoped //����������ĸ����ʱ�����˽�

            services.AddDbContext<AppDbContext>(option =>//IOCע�����ݿ������Ķ������Ҫoption �����������ݿ��ַ��� 
            {
                //option.UseSqlServer("Data Source=(localdb)\\ProjectsV13;Initial Catalog=Fakexiecheng;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                option.UseSqlServer(Configuration["DbContext:ConnectionString"]);//ʹ��UseSqlServer��չ������Ҫnuget��װentityFramework sql server
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)//IApplicationBuilder����HTTP����ͨ��,MiddleWare�м�����崦��IWebHostEnvironment�����Ƿ�����������
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
