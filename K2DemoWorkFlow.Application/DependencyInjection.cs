using K2DemoWorkFlow.Application.Services;
using K2DemoWorkFlow.Application.ServicesImplementation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2DemoWorkFlow.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection service)
        {

            service.AddScoped<ILeaveRequest, LeaveRequest>();

           
            return service;
        }
    }
}
