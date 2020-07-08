using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using reg.Services;
using Scaffolds;

namespace reg.Extensions.IOC
{
    public static class ServiceInjection
    {
        public static IServiceCollection ServiceIOC(this IServiceCollection services)
        {
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<UserManager<ApplicationUser>>();
            services.AddScoped<IGrpcGreeterClient, GrpcGreeterClient>();
            return services;
        }
    }
}
