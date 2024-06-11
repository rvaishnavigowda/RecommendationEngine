using Microsoft.Extensions.DependencyInjection;
using RecommendationEngineClientSide.ConsoleHelper;
using RecommendationEngineClientSide.Services;

namespace RecommendationEngineClientSide
{
    public static class DI
    {
        public static IServiceCollection AddClientServices(this IServiceCollection services)
        {
            services.AddTransient<IRequestService, RequestService>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<LoginConsoleHelper>();
            services.AddTransient<AdminConsoleHelper>();
            return services;
        }
    }
}