using RecommendationEngineClientSide.DTO;
using RecommendationEngineClientSide.Services;
using System;
using System.Threading.Tasks;

namespace RecommendationEngineClientSide.ConsoleHelper
{
    public class AdminConsoleHelper
    {
        private readonly IAdminService _adminService;

        public bool ShouldLogout { get; private set; }

        public AdminConsoleHelper(IAdminService adminService)
        {
            _adminService = adminService;
            ShouldLogout = false;
        }

        public async Task HandleAdminRoleAsync()
        {
            bool continueLoop = true;
            while (continueLoop)
            {
                Console.WriteLine("Welcome, Admin! Choose an option:");
                Console.WriteLine("1. Add Menu");
                Console.WriteLine("2. Update Menu");
                Console.WriteLine("3. Delete Menu");
                Console.WriteLine("4. Logout");
                string choice = Console.ReadLine();

                // Handle admin choices here
                switch (choice)
                {
                    case "1":
                        await AddMenuAsync();
                        break;
                    case "2":
                        await UpdateMenuAsync();
                        break;
                    case "3":
                        await DeleteMenuAsync();
                        break;
                    case "4":
                        continueLoop = false;
                        ShouldLogout = true;
                        Console.WriteLine("Logging out...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private async Task AddMenuAsync()
        {
            try
            {
                Console.WriteLine("Enter menu name:");
                string menuName = Console.ReadLine();

                Console.WriteLine("Enter menu type:");
                string menuType = Console.ReadLine();

                Console.WriteLine("Enter menu price:");
                if (!decimal.TryParse(Console.ReadLine(), out decimal menuPrice))
                {
                    Console.WriteLine("Invalid price format. Please try again.");
                    return;
                }

                var addMenuRequestDto = new AddMenuRequestDto
                {
                    MenuName = menuName,
                    MenuType = menuType,
                    MenuPrice = menuPrice
                };

                string responseJson = await _adminService.AddMenuAsync(addMenuRequestDto);
                Console.WriteLine($"Server response: {responseJson}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private async Task UpdateMenuAsync()
        {
            try
            {
                Console.WriteLine("Enter menu name to update:");
                string menuName = Console.ReadLine();

                Console.WriteLine("Enter new menu type:");
                string menuType = Console.ReadLine();

                Console.WriteLine("Enter new menu price:");
                if (!decimal.TryParse(Console.ReadLine(), out decimal menuPrice))
                {
                    Console.WriteLine("Invalid price format. Please try again.");
                    return;
                }

                var updateMenuRequestDto = new UpdateMenuRequestDto
                {
                    MenuName = menuName,
                    MenuType = menuType,
                    MenuPrice = menuPrice
                };

                string responseJson = await _adminService.UpdateMenuAsync(updateMenuRequestDto);
                Console.WriteLine($"Server response: {responseJson}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private async Task DeleteMenuAsync()
        {
            try
            {
                Console.WriteLine("Enter menu name to delete:");
                string menuName = Console.ReadLine();

                var deleteMenuRequestDto = new DeleteMenuRequestDto
                {
                    MenuName = menuName,
                };

                string responseJson = await _adminService.DeleteMenuAsync(deleteMenuRequestDto);
                Console.WriteLine($"Server response: {responseJson}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
