using BuildingBlocks.Behaviors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddMediatR(cfg =>
            {
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            });
            //services.AddFeatureManagement();

            //services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
