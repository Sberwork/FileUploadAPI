using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CommandDAL.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using AutoMapper;
using Newtonsoft.Json.Serialization;
using CommandAPI;
using Microsoft.Extensions.Logging;
using CommandDAL.Models;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using System.IO;
using Microsoft.OpenApi.Models;
using Hangfire;
using Hangfire.PostgreSql;
using CommandAPI.Infrastructure;
using CommandAPI.MiddleWares;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;
using CommandBLL.Services;

namespace CommandAPI
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
            var builder = new NpgsqlConnectionStringBuilder();
            builder.ConnectionString = Configuration.GetConnectionString("PostgreSqlConnection");
            builder.Username = Configuration["UserID"];
            builder.Password = Configuration["Password"];

            services.AddDbContext<ApplicationContext>(opt => opt.UseNpgsql(builder.ConnectionString, b => b.MigrationsAssembly("CommandAPI")));

            services.AddIdentity<User, IdentityRole>(options =>
                       {
                           options.Password.RequireDigit = false;
                           options.Password.RequiredLength = 4;
                           options.Password.RequireLowercase = false;
                           options.Password.RequireUppercase = false;
                           options.Password.RequireNonAlphanumeric = false;
                       }).AddEntityFrameworkStores<ApplicationContext>();

            services.AddControllers().AddNewtonsoftJson(s =>
            {
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                s.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            // services.ConfigureApplicationCookie(options =>
            // {
            //     options.Events.OnRedirectToLogin = context =>
            //     {
            //         context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //         return Task.CompletedTask;
            //     };
            // });

            services.AddHangfire(x => x.UsePostgreSqlStorage(builder.ConnectionString));
            services.AddHangfireServer();

            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            // services.AddScoped<ICommandAPIRepo, MockCommandAPIRepo>();
            services.AddScoped<ICommandAPIRepo, SqlCommandAPIRepo>();

            services.AddTransient<IMaterialService, MaterialService>();
            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = int.MaxValue;
            });

            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = 2147483648; // if don't set default value is: 128 MB
                x.MultipartHeadersLengthLimit = int.MaxValue;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = " Backend API",
                    Description = "Задание. API для работы с материалами",
                    TermsOfService = new Uri("https://github.com/Sberwork/FileUploadAPI/blob/master/README.md"),
                    Contact = new OpenApiContact
                    {
                        Name = "Halim Hamidov",
                        Email = string.Empty,
                        Url = new Uri("https://example.com"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under 8unicorns",
                        Url = new Uri("https://example.com"),
                    }
                });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseMiddleware<LogMiddleware>();

            // 1st v1
            // app.UseOpenApi();
            // app.UseSwaggerUi3();

            // v2
            app.UseSwagger();

            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "Halim's first API");
                x.RoutePrefix = string.Empty;
            });
            app.UseHangfireServer();
            app.UseHangfireDashboard();
            RecurringJob.AddOrUpdate(() => MailService.SendMail("You are the best version of yourself!"), Cron.Hourly);
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
