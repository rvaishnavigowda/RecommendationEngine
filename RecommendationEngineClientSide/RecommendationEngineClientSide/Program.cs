using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RecommendationEngineClientSide;
using RecommendationEngineClientSide.ConsoleHelper;
using RecommendationEngineClientSide.Services;

namespace RecommendationEngineClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddClientServices();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            Console.WriteLine("Welcome to the Recommendation Engine Client!");

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
                        // Add chef role handling here
                        break;
                    case "employee":
                        await adminConsoleHelper.HandleAdminRoleAsync();
                        break;
                    default:
                        Console.WriteLine("Unknown role.");
                        break;
                }

                //if (role == "admin" || role == "employee")
                //{
                    if (adminConsoleHelper.ShouldLogout)
                    {
                        break;
                    }
                //}
            }
        }
    }
}
