using Commons.K2.Proxy;

using K2DemoWorkFlow.Application.IReprositary;

using K2DemoWorkFlow.Application.WorkFlowServices;
using K2DemoWorkFlow.Infrastructure.Implementation;
using K2DemoWorkFlow.Infrastructure.WorkFlowImplementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Framework.Core.DependencyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace K2DemoWorkFlow.Infrastructure
{
    public static class InfrastructureInjection
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection service, ConfigurationManager Configuration)
        {
            var baseTypeInterface = typeof(IWorkFlowProcess);
            var classTypes = Assembly.GetExecutingAssembly().GetTypes()
                .FirstOrDefault(type => type.IsClass && !type.IsInterface && type.GetInterfaces().Any(x => x.Name == baseTypeInterface.Name) && type.Name == Configuration.GetValue<string>("workFlowImplemention") );
            service.AddScoped(baseTypeInterface, classTypes);

            service.AddScoped<IleaveRequestReprositary, leaveRequestReprositary>();
            service.RegisterAssemblyPublicNonGenericClasses(Assembly.GetAssembly(typeof(K2Proxy)))
            .Where(item => item.Name.EndsWith("K2Proxy"))
            .AsConcreteTypesScoped();

            service.AddResponseCompression();
            service.AddHttpContextAccessor();

            return service;
        }
    }
}
