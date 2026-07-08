namespace Ordering.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            // Add API services here
            return services;
        }

        public static WebApplication UseApiService(this WebApplication webApplication)
        {

            return webApplication;
        }
    }
}
