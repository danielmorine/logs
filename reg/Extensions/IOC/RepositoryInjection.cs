using Microsoft.Extensions.DependencyInjection;

namespace reg.Extensions.IOC
{
    public static class RepositoryInjection
    {
        public static IServiceCollection RepositoryIOC(this IServiceCollection services)
        {
            return services;
        }
    }
}
