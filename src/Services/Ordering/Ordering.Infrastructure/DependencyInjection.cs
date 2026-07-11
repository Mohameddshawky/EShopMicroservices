using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices
            (this IServiceCollection services,IConfiguration configuration )
        {

            var connectionString = configuration.GetConnectionString("Database");
            // Add infrastructure services here
            services.AddDbContext<AppDbContext>(op =>
            op.UseSqlServer(connectionString));
            //services.AddScoped<I>
            return services;
        }
    }
}
