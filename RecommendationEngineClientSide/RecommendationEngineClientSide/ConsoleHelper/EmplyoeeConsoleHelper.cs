using RecommendationEngineClientSide.DTO;
using RecommendationEngineClientSide.Services.EmployeeServices;
using System;
using System.Threading.Tasks;

namespace RecommendationEngineClientSide.ConsoleHelper
{
    public class EmployeeConsoleHelper
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeConsoleHelper(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task HandleEmployeeRoleAsync()
        {
            bool continueLoop = true;
            while (continueLoop)
            {
                Console.WriteLine("Welcome, Employee! Choose an option:");
                Console.WriteLine("1. Get Daily Menu");
                Console.WriteLine("2. Place Order");
                Console.WriteLine("3. Give Feedback");
                Console.WriteLine("4. Logout");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await GetDailyMenuAsync();
                        break;
                    case "2":
                        await PlaceOrderAsync();
                        break;
                    case "3":
                        await GiveFeedbackAsync();
                        break;
                    case "4":
                        continueLoop = false;
                        Console.WriteLine("Logging out...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private async Task GetDailyMenuAsync()
        {
            try
            {
                Console.WriteLine("Enter your username:");
                string userName = Console.ReadLine();

                var dailyMenuRequestDto = new DailyMenuRequestDto
                {
                    UserName = userName,
                    CurrentDate = DateTime.Now
                };

                var response = await _employeeService.GetDailyMenuAsync(dailyMenuRequestDto);
                if (response.MenuList != null && response.MenuList.Count > 0)
                {
                    Console.WriteLine("Daily Menu:");
                    foreach (var menuItem in response.MenuList)
                    {
                        Console.WriteLine($"{menuItem.MenuName} - {menuItem.Price} - {menuItem.Rating}");
                    }
                }
                else
                {
                    Console.WriteLine("No menu items found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private async Task PlaceOrderAsync()
        {
            try
            {
                Console.WriteLine("Enter your username:");
                string userName = Console.ReadLine();

                Console.WriteLine("Enter order date (YYYY-MM-DD):");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime orderDate))
                {
                    Console.WriteLine("Invalid date format. Please try again.");
                    return;
                }

                var orderItems = new List<OrderItemDto>();
                bool addingItems = true;
                while (addingItems)
                {
                    Console.WriteLine("Enter menu item name:");
                    string menuItemName = Console.ReadLine();

                    Console.WriteLine("Enter quantity:");
                    if (!int.TryParse(Console.ReadLine(), out int quantity))
                    {
                        Console.WriteLine("Invalid quantity. Please try again.");
                        continue;
                    }

                    orderItems.Add(new OrderItemDto
                    {
                        MenuName = menuItemName,
                        Quantity = quantity
                    });

                    Console.WriteLine("Do you want to add more items? (yes/no)");
                    string addMore = Console.ReadLine();
                    addingItems = addMore.Equals("yes", StringComparison.OrdinalIgnoreCase);
                }

                var orderDetailRequestDto = new OrderDetailRequestDto
                {
                    UserName = userName,
                    OrderDate = orderDate,
                    Items = orderItems
                };

                var response = await _employeeService.PlaceOrderAsync(orderDetailRequestDto);
                Console.WriteLine(response.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private async Task GiveFeedbackAsync()
        {
            try
            {
                Console.WriteLine("Enter your username:");
                string userName = Console.ReadLine();

                Console.WriteLine("Enter menu item name:");
                string menuItemName = Console.ReadLine();

                Console.WriteLine("Enter rating (1-5):");
                if (!int.TryParse(Console.ReadLine(), out int rating))
                {
                    Console.WriteLine("Invalid rating. Please try again.");
                    return;
                }

                Console.WriteLine("Enter your comments:");
                string comment = Console.ReadLine();

                var feedbackDto = new FeedbackDto
                {
                    UserName = userName,
                    MenuName = menuItemName,
                    Rating = rating,
                    Comment = comment,
                    FeedbackDate = DateTime.Now
                };

                var response = await _employeeService.GiveFeedbackAsync(feedbackDto);
                Console.WriteLine(response.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
