using BuildingBlocks.Exceptions.Handler;


namespace Ordering.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddCarter();
            services.AddExceptionHandler<CustomExceptionHandler>();
            services.AddHealthChecks(); 
            return services;
        }

        public static WebApplication UseApiService(this WebApplication webApplication)
        {

            webApplication.MapCarter();
            webApplication.UseExceptionHandler(OP => { });
            webApplication.MapHealthChecks("/health");
            return webApplication;
        }
    }
}
