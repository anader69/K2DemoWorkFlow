using AutoMapper;

using Framework.Core.AutoMapper;
using Framework.Core.SharedServices;
using Framework.Core.SharedServices.Services;
using Framework.Identity.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSwag.AspNetCore;
using System;

namespace DSC.K2.IntegationService
{
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="serviceProvider"></param>
        public Startup(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            this.Configuration = configuration;
            this.ServiceProvider = serviceProvider;
        }

        public IServiceProvider ServiceProvider { get; set; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.ConfigureSharedApplicationServices(connectionString);
            //services.ConfigureApplicationServices();
            //services.ConfigureInfrastructureServices(connectionString);
            services.IdentityConfigureServices(connectionString);
            services.AddDataProtection(connectionString);

            services.AddControllers();

            //services.AddTransient<AppSettingsService>();
            //services.AddTransient<K2Proxy>();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(Configuration.GetSection("Logging"));
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });


            #region SwaggerConfiguration
            // Register the Swagger services
            services.AddSwaggerDocument((op) =>
            {
                op.Title = "K2 Integration Endpoints";
                op.Version = "v1";
            });

            #endregion

            services.AddRouting();
            //services.AddMvc();
            //services.InitHangfire(connectionString);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app,IHostingEnvironment env,ILoggerFactory loggerFactory,IServiceProvider svp)
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IConfiguration configuration, IServiceProvider serviceProvider)
        {
            //app.ConfigureRequestPipeline(env, loggerFactory, this.Configuration, this.ServiceProvider);

            //var config = new MapperConfiguration(cfg =>
            //{
            //    //cfg.AddProfile(typeof(IdentityAutoMapperProfile));
            //    cfg.AddProfile(typeof(AppAutoMapperProfile));
            //    cfg.AddProfile(typeof(CommonsAutoMapperProfile));
            //});
            //app.UseHangfireDashboard();
            //AutoMapperConfiguration.Init(config);

            // Register the Swagger generator and the Swagger UI middlewares
            app.UseOpenApi();
            app.UseSwaggerUi3(config =>
            {
                config.SwaggerRoutes.Add(new SwaggerUi3Route("v1", "/swagger/v1/swagger.json"));
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.ConfigureCommonRequestPipeline(env, loggerFactory, configuration, serviceProvider);
        }
    }
}
