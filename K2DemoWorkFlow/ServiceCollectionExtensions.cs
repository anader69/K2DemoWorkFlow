


using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity;

using K2DemoWorkFlow.Application;
using K2DemoWorkFlow.Infrastructure;
using System.Reflection;


namespace K2DemoWorkFlow
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureApplicationServices(this IServiceCollection services,string ConnectionString, ConfigurationManager Configuration)
        {


            DependencyInjection.AddApplication(services);
            InfrastructureInjection.AddInfrastructureService(services, Configuration);



            services.AddDbContext<WorkFlowContext>
             (options => options.UseSqlServer(ConnectionString));

         
          


        }


    }
}
