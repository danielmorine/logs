using Microsoft.Extensions.DependencyInjection;

namespace reg.Extensions.IOC
{
    public static class ServiceInjection
    {
        public static IServiceCollection ServiceIOC(this IServiceCollection services)
        {
            return services;
        }
    }
}
