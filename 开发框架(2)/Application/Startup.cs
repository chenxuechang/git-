using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cathy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Application
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor, Microsoft.AspNetCore.Http.HttpContextAccessor>();

            //上传大文件 
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue;
                x.MultipartHeadersLengthLimit = int.MaxValue;
            });

            //跨域
            services.AddCors(options =>
            {
                //全跨域、不限制域名
                //options.AddPolicy(MyAllowSpecificOrigins,
                //   builder => builder.SetIsOriginAllowed(origin => true).AllowAnyHeader().AllowCredentials().AllowAnyMethod()
                //   );

                options.AddPolicy(MyAllowSpecificOrigins,
                  builder => builder
                       .WithOrigins(
                           // App:CorsOrigins in appsettings.json can contain more than one address separated by comma.
                           ConfigHelper.GetSection("CorsHosts").Value
                               .Split(",", StringSplitOptions.RemoveEmptyEntries)
                       )
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials()
                   );
            });

            //添加异常过滤器
            services.AddMvc(options =>
            {
                options.Filters.Add<CustomExceptionFilter>();
            });


            //配置swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Crypto Exchange",
                    Description = "人才大数据系统",
                    Contact = new OpenApiContact
                    {
                        Name = "Cathy",
                        Email = "",
                        Url = new Uri("http://hf.com"),
                    },
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "在下框中输入请求头中需要添加Authorization授权Token：Bearer Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "",
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });


                // 加载程序集的xml描述文档
                var baseDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
                var xmlFile = System.AppDomain.CurrentDomain.FriendlyName + ".xml";
                var xmlPath = Path.Combine(baseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            //Microsoft.AspNetCore.Mvc.MvcOptions.EnableEndpointRouting=false;

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            //跨域
            app.UseCors(MyAllowSpecificOrigins);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "myArea",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });

            Application.HttpContext.Configure(app.ApplicationServices.GetRequiredService<Microsoft.AspNetCore.Http.IHttpContextAccessor>());
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //      name: "default",
            //      template: "{controller=Login}/{action=Index}/{id?}"
            //    );
            //    routes.MapRoute(
            //      name: "areas",
            //      template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            //    );
            //});

            //配置swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Crypto Exchange");
                // 访问Swagger的路由后缀
                c.RoutePrefix = "swagger";
            });
        }
    }
}
