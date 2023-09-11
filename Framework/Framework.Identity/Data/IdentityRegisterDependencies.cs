﻿using Framework.Core.DependencyManagement;
using Framework.Identity.Data.Entities;
using Framework.Identity.Data.Repositories;
using Framework.Identity.Data.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Framework.Identity.Data
{
    public static class IdentityRegisterDependencies
    {
        public static void IdentityConfigureServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(connectionString));

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

            //services.AddScoped<NotificationService>();

            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            //    options.Cookie.Name = "AspCoreIdentity";
            //    options.ExpireTimeSpan = TimeSpan.FromHours(24);
            //    options.LoginPath = "/ADIdentity/Account/Login";
            //    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            //    options.SlidingExpiration = true;
            //});

            services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                //options.Password.RequireDigit = true;
                //options.Password.RequireLowercase = true;
                //options.Password.RequireNonAlphanumeric = false;
                //options.Password.RequireUppercase = true;
                //options.Password.RequiredLength = 8;
                //options.Password.RequiredUniqueChars = 0;

                // Default SignIn settings.
                //options.SignIn.RequireConfirmedEmail = true;
                //options.SignIn.RequireConfirmedPhoneNumber = false;
                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.RequireUniqueEmail = true;

            });
        }

        public static void AddDataProtection(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DataKeysContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddDataProtection()
                .SetApplicationName("DSC-WebApp")
                .PersistKeysToDbContext<DataKeysContext>();

            services.Configure<DataProtectionTokenProviderOptions>(
                options =>
                    options.TokenLifespan = TimeSpan.FromHours(24)
            );
        }

        public static void UseIdentityDBMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
            .CreateScope())
            {
                serviceScope.ServiceProvider.GetService<AppIdentityDbContext>().Database.Migrate();
            }
        }

        public static void UseDataKeysMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
            .CreateScope())
            {
                serviceScope.ServiceProvider.GetService<DataKeysContext>().Database.Migrate();
            }
        }

        public static void ConfigureIdentityServices(this IServiceCollection services)
        {

            //Auto Register App services As Scoped
            //services.RegisterAssemblyPublicNonGenericClasses(Assembly.GetAssembly(typeof(UserAppService)))
            //    .Where(c => c.Name.EndsWith("AppService"))
            //    .AsConcreteTypesScoped();

            services.RegisterAssemblyPublicNonGenericClasses(Assembly.GetAssembly(typeof(IUserAppService)))
                .Where(c => c.Name.EndsWith("AppService"))
                .AsPublicImplementedInterfaces();
        }
    }
}
