using Commons.K2.Proxy;
using Framework.Core.BackgroundJobs;
using Framework.Core.SharedServices.Services;
using Framework.Identity.Data.Services;
using Framework.Identity;
using System.Reflection;
using Framework.Core.DependencyManagement;
using Framework.Identity.Data.Services.Interfaces;
using Framework.Identity.Data.Repositories;
using Framework.Core.Data.Repositories;
using Framework.Core.Data.Uow;
using Framework.Core.Notifications;
using Framework.Core.SharedServices;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Framework.Core.Caching;
using Microsoft.EntityFrameworkCore;
using Framework.Identity.Data;
using Framework.Identity.Data.Entities;
using Microsoft.AspNetCore.Identity;
using K2DemoWorkFlow.Infrastructure;

namespace K2DemoWorkFlow
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureApplicationServices(this IServiceCollection services,string ConnectionString)
        {
            services.AddDbContext<WorkFlowContext>
             (options => options.UseSqlServer(ConnectionString));

            services.AddDbContext<CommonsDbContext>(options => options.UseSqlServer(ConnectionString));
         
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.GetAssembly(typeof(K2Proxy)))
            .Where(c => c.Name.EndsWith("K2Proxy"))
           .AsConcreteTypesScoped();
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.GetAssembly(typeof(IUserAppService)))
              .Where(c => c.Name.EndsWith("AppService"))
              .AsPublicImplementedInterfaces();
            services.TryAddScoped<ICacheManager, MemoryCacheManager>();
            services.AddTransient<AppSettingsService>();
            services.AddScoped<UserRepository>();
            services.AddScoped<RoleRepository>();
            services.AddScoped<UserRolesRepository>();
            services.AddScoped<UserTokensRepository>();
            services.AddTransient<AppSettingsService>();
            services.AddTransient<UserAppService>();
            services.AddTransient<UserRoleAppService>();
            services.AddTransient<RoleAppService>();
            services.AddTransient<UserTokensAppService>();

            services.AddTransient<ActiveDirectoryHelperAppService>();

            services.TryAddScoped(typeof(IUnitOfWorkBase<>), typeof(UnitOfWorkBase<>));
            services.TryAddScoped(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));
            services.TryAddScoped<ICommonsDbContext, CommonsDbContext>();

            services.AddScoped<INotificationsManager, NotificationsManager>();
            services.AddScoped<NotificationTemplateService>();
            services.AddScoped<AppSettingsService>();
            services.AddScoped<LogAppService>();

            services.TryAddScoped<IEmailService, SmtpEmailService>();
            //services.TryAddScoped<AttachmentService, AttachmentService>();
            //services.TryAddScoped<AttachmentHelperAppService, AttachmentHelperAppService>();

            // Caching
            services.AddMemoryCache();
            services.AddResponseCompression();
            services.AddDbContext<AppIdentityDbContext>(options =>
              options.UseSqlServer(ConnectionString));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<UserRepository>();
            services.AddScoped<RoleRepository>();
            services.AddScoped<UserRolesRepository>();
            services.AddScoped<UserTokensRepository>();
            //services.AddScoped<UserAppService>();
            //services.AddScoped<RoleAppService>();
            services.ConfigureIdentityServices();

            services.AddHttpContextAccessor();


        }


    }
}
