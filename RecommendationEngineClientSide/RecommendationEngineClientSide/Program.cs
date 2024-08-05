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
            serviceCollection.AddSingleton<LoginDateRepository>(provider => new LoginDateRepository("loginDates.db"));
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
            var loginDateRepository = serviceProvider.GetService<LoginDateRepository>();

            while (true)
            {
                var loginResponse = await loginConsoleHelper.PromptLoginAsync();
                if(loginResponse.Status=="Success")
                {
                    string role = loginResponse.UserRole.ToLower();
                    DateTime loginDate = DateTime.MinValue;
                    if (role == "chef")
                    {
                        Console.WriteLine("Enter the date (YYYY-MM-DD) for login:");
                        if (!DateTime.TryParse(Console.ReadLine(), out loginDate))
                        {
                            Console.WriteLine("Invalid date format. Please try again.");
                            continue;
                        }

                        await loginDateRepository.SaveLoginDateAsync("chef", loginDate);
                    }
                    else
                    {
                        loginDate = (DateTime)await loginDateRepository.GetLastLoginDateAsync("chef");
                    }
                    switch (role)
                    {
                        case "admin":
                            await adminConsoleHelper.HandleAdminRoleAsync(loginDate);
                            break;
                        case "chef":
                            await chefConsoleHelper.HandleChefRoleAsync(loginResponse.UserName, loginDate);
                            break;
                        case "employee":
                            await employeeConsoleHelper.HandleEmployeeRoleAsync(loginResponse.UserName, loginDate);
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
                else if(loginResponse.Status=="Failure")
                {
                    Console.WriteLine(loginResponse.Message);
                }
            }
        }
    }
}