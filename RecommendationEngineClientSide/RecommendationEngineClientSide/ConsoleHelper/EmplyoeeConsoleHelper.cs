using RecommendationEngineClientSide.DTO;
using RecommendationEngineClientSide.Services.EmployeeServices;
using System;
using System.Threading.Tasks;

namespace RecommendationEngineClientSide.ConsoleHelper
{
    public class EmployeeConsoleHelper
    {
        private readonly IEmployeeService _employeeService;
        public bool ShouldLogout { get; private set; }
        private string _username;
        private DateTime _date;

        public EmployeeConsoleHelper(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
            ShouldLogout = false;
        }

        public async Task HandleEmployeeRoleAsync(string userName, DateTime date)
        {
            _username = userName.ToLower();
            _date = date;
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
                

                var dailyMenuRequestDto = new DailyMenuRequestDto
                {
                    UserName = _username.ToLower(),
                    CurrentDate = _date
                };

                var response = await _employeeService.GetDailyMenuAsync(dailyMenuRequestDto);
                if(response.Status=="Success")
                {
                    if (response.MenuList != null && response.MenuList.Count > 0)
                    {

                        Console.WriteLine("Daily Menu:");
                        Console.WriteLine("Menu\t Price\t Rating \t");
                        foreach (var menuItem in response.MenuList)
                        {
                            Console.WriteLine($"{menuItem.MenuName}\t {menuItem.Price:F2}\t {menuItem.Rating}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No menu items found.");
                    }
                }
                else
                {
                    Console.WriteLine(response.Message);
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
                // Ask for the type of menu
                Console.WriteLine("Enter the type of menu you are ordering:");
                string orderMenutype = Console.ReadLine();

                // Initialize order items list
                var orderItems = new List<OrderItemDto>();
                bool addingItems = true;
                while (addingItems)
                {
                    Console.WriteLine("Enter menu item name:");
                    string menuItemName = Console.ReadLine();

                    orderItems.Add(new OrderItemDto
                    {
                        MenuName = menuItemName.ToLower()
                    });

                    Console.WriteLine("Do you want to add more items? (yes/no)");
                    string addMore = Console.ReadLine();
                    addingItems = addMore.Equals("yes", StringComparison.OrdinalIgnoreCase);
                }

                // Create the order detail request DTO
                var orderDetailRequestDto = new OrderDetailRequestDto
                {
                    UserName = _username.ToLower(),
                    OrderDate = _date,
                    OrderMenutype = orderMenutype.ToLower(),
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
                    UserName = _username.ToLower(),
                    MenuName = menuItemName.ToLower(),
                    Rating = rating,
                    Comment = comment.ToLower(),
                    FeedbackDate = _date
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
