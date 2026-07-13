using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Data.interceptors;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices
            (this IServiceCollection services,IConfiguration configuration )
        {

            var connectionString = configuration.GetConnectionString("Database");
            // Add infrastructure services here
            services.AddDbContext<AppDbContext>(op => {
                op.AddInterceptors(new AuditableEntityInterceptor());
                op.UseSqlServer(connectionString);

                });
            //services.AddScoped<I>
            return services;
        }
    }
}
