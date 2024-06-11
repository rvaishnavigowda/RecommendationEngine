//using System;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading.Tasks;
//using Newtonsoft.Json;
//using RecommendationEngineClient.Dtos;

//namespace RecommendationEngineClient
//{
//    class Program
//    {
//        private const string ServerIp = "127.0.0.1";
//        private const int ServerPort = 5000;

//        static async Task Main(string[] args)
//        {
//            Console.WriteLine("Welcome to the Recommendation Engine Client!");

//            while (true)
//            {
//                Console.WriteLine("Please enter your employee ID:");
//                string employeeId = Console.ReadLine();

//                Console.WriteLine("Please enter your name:");
//                string name = Console.ReadLine();

//                var loginRequestDto = new LoginRequestDto
//                {
//                    EmployeeId = employeeId,
//                    Name = name
//                };

//                var loginRequest = new
//                {
//                    Controller = "login",
//                    Action = "handleloginrequest",
//                    Data = loginRequestDto
//                };

//                string requestJson = JsonConvert.SerializeObject(loginRequest);

//                string responseJson = await SendRequestAsync(requestJson);
//                var loginResponse = JsonConvert.DeserializeObject<string>(responseJson);

//                Console.WriteLine($"Server response: {loginResponse}");

//                if (loginResponse == "Login successful!")
//                {
//                    switch (loginResponse.ToLower())
//                    {
//                        case "admin":
//                            await HandleAdminRoleAsync();
//                            break;
//                        case "chef":
//                            await HandleChefRoleAsync();
//                            break;
//                        default:
//                            Console.WriteLine("Unknown role.");
//                            break;
//                    }
//                }
//            }
//        }

//        private static async Task<string> SendRequestAsync(string requestJson)
//        {
//            using TcpClient client = new TcpClient();
//            await client.ConnectAsync(ServerIp, ServerPort);

//            NetworkStream stream = client.GetStream();
//            byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
//            await stream.WriteAsync(requestBytes, 0, requestBytes.Length);

//            byte[] buffer = new byte[1024];
//            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

//            string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
//            return response;
//        }

//        private static async Task HandleAdminRoleAsync()
//        {
//            Console.WriteLine("Welcome, Admin! Choose an option:");
//            Console.WriteLine("1. Add Menu");
//            Console.WriteLine("2. Update Menu");
//            Console.WriteLine("3. Delete Menu");
//            string choice = Console.ReadLine();

//            // Handle admin choices here
//            switch (choice)
//            {
//                case "1":
//                    await AddMenuAsync();
//                    break;
//                case "2":
//                    await UpdateMenuAsync();
//                    break;
//                case "3":
//                    await DeleteMenuAsync();
//                    break;
//                default:
//                    Console.WriteLine("Invalid choice.");
//                    break;
//            }
//        }

//        private static async Task HandleChefRoleAsync()
//        {
//            Console.WriteLine("Welcome, Chef! Choose an option:");
//            Console.WriteLine("1. Set Daily Menu");
//            string choice = Console.ReadLine();

//            // Handle chef choices here
//            switch (choice)
//            {
//                case "1":
//                    await SetDailyMenuAsync();
//                    break;
//                default:
//                    Console.WriteLine("Invalid choice.");
//                    break;
//            }
//        }

//        private static async Task AddMenuAsync()
//        {
//            Console.WriteLine("Enter menu details to add:");
//            string menuDetails = Console.ReadLine();

//            var addMenuRequestDto = new AddMenuRequestDto
//            {
//                MenuDetails = menuDetails
//            };

//            var addMenuRequest = new
//            {
//                Controller = "menu",
//                Action = "add",
//                Data = addMenuRequestDto
//            };

//            string requestJson = JsonConvert.SerializeObject(addMenuRequest);
//            string responseJson = await SendRequestAsync(requestJson);
//            Console.WriteLine($"Server response: {responseJson}");
//        }

//        private static async Task UpdateMenuAsync()
//        {
//            Console.WriteLine("Enter menu details to update:");
//            string menuDetails = Console.ReadLine();

//            var updateMenuRequestDto = new UpdateMenuRequestDto
//            {
//                MenuDetails = menuDetails
//            };

//            var updateMenuRequest = new
//            {
//                Controller = "menu",
//                Action = "update",
//                Data = updateMenuRequestDto
//            };

//            string requestJson = JsonConvert.SerializeObject(updateMenuRequest);
//            string responseJson = await SendRequestAsync(requestJson);
//            Console.WriteLine($"Server response: {responseJson}");
//        }

//        private static async Task DeleteMenuAsync()
//        {
//            Console.WriteLine("Enter menu details to delete:");
//            string menuDetails = Console.ReadLine();

//            var deleteMenuRequestDto = new DeleteMenuRequestDto
//            {
//                MenuDetails = menuDetails
//            };

//            var deleteMenuRequest = new
//            {
//                Controller = "menu",
//                Action = "delete",
//                Data = deleteMenuRequestDto
//            };

//            string requestJson = JsonConvert.SerializeObject(deleteMenuRequest);
//            string responseJson = await SendRequestAsync(requestJson);
//            Console.WriteLine($"Server response: {responseJson}");
//        }

//        private static async Task SetDailyMenuAsync()
//        {
//            Console.WriteLine("Enter daily menu details:");
//            string dailyMenuDetails = Console.ReadLine();

//            var setDailyMenuRequestDto = new SetDailyMenuRequestDto
//            {
//                DailyMenuDetails = dailyMenuDetails
//            };

//            var setDailyMenuRequest = new
//            {
//                Controller = "menu",
//                Action = "setdaily",
//                Data = setDailyMenuRequestDto
//            };

//            string requestJson = JsonConvert.SerializeObject(setDailyMenuRequest);
//            string responseJson = await SendRequestAsync(requestJson);
//            Console.WriteLine($"Server response: {responseJson}");
//        }
//    }
//}

using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RecommendationEngineClient;
using RecommendationEngineClientSide.ConsoleHelper;
using RecommendationEngineClientSide.Services;
using RecommendationEngineClientSide;

namespace RecommendationEngineClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddClientServices();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var loginConsoleHelper = new LoginConsoleHelper(serviceProvider.GetService<ILoginService>());
            var adminConsoleHelper = new AdminConsoleHelper(serviceProvider.GetService<IAdminService>());

            Console.WriteLine("Welcome to the Recommendation Engine Client!");

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
                           
                            break;
                        case "employee":
                            await adminConsoleHelper.HandleAdminRoleAsync();
                        break;
                        default:
                            Console.WriteLine("Unknown role.");
                            break;
                    }
            }
        }
    }
}
