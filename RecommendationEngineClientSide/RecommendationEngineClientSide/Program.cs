using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RecommendationEngineClientSide;
using RecommendationEngineClientSide.ConsoleHelper;
using RecommendationEngineClientSide.Services.AdminServices;
using RecommendationEngineClientSide.Services.ChefServices;
using RecommendationEngineClientSide.Services.EmployeeServices;
using RecommendationEngineClientSide.Services.LoginServices;

namespace RecommendationEngineClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddClientServices();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            Console.WriteLine("Welcome to the Recommendation Engine");

            while (true)
            {
                await RunMainAsync(serviceProvider);
                Console.WriteLine("You have been logged out. Returning to login page...");
            }
        }

        private static async Task RunMainAsync(IServiceProvider serviceProvider)
        {
            var loginConsoleHelper = new LoginConsoleHelper(serviceProvider.GetService<ILoginService>());
            var adminConsoleHelper = new AdminConsoleHelper(serviceProvider.GetService<IAdminService>());
            var chefConsoleHelper = new ChefConsoleHelper(serviceProvider.GetService<IChefService>());
            var employeeConsoleHelper = new EmployeeConsoleHelper(serviceProvider.GetService<IEmployeeService>());

            while (true)
            {
                string loginResponse = await loginConsoleHelper.PromptLoginAsync();
                string role = loginResponse.ToLower();

                switch (role)
                {
                    case "admin":
                        await adminConsoleHelper.HandleAdminRoleAsync();
                        break;
                    case "chef":
                        await chefConsoleHelper.HandleChefRoleAsync();
                        break;
                    case "employee":
                        await employeeConsoleHelper.HandleEmployeeRoleAsync();
                        break;
                    default:
                        Console.WriteLine("Unknown role.");
                        break;
                }

                if (adminConsoleHelper.ShouldLogout || chefConsoleHelper.ShouldLogout || employeeConsoleHelper.ShouldLogout)
                {
                    break;
                }
            }
        }
    }
}