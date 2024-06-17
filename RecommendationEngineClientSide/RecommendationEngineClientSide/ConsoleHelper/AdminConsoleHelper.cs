using RecommendationEngineClientSide.DTO;
using RecommendationEngineClientSide.Services.AdminServices;
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
                    MenuName = menuName.ToLower(),
                    MenuType = menuType.ToLower(),
                    MenuPrice = menuPrice
                };

                var responseJson = await _adminService.AddMenuAsync(addMenuRequestDto);
                Console.WriteLine(responseJson.Message );

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
                var fetchMenuRequestDto = new FetchMenuRequestDTO { MenuName = menuName.ToLower() };
                var fetchResponse = await _adminService.FetchMenuDetailsAsync(fetchMenuRequestDto);

                if (fetchResponse.Status == "failure")
                {
                    Console.WriteLine(fetchResponse.Message);
                    return;
                }

                if (fetchResponse.MenuList.Count > 1)
                {
                    Console.WriteLine("Multiple menu items found. Please select one:");
                    for (int i = 0; i < fetchResponse.MenuList.Count; i++)
                    {
                        var menuItem = fetchResponse.MenuList[i];
                        Console.WriteLine($"{i + 1}. Name: {menuItem.MenuName}, Type: {menuItem.MenuType}, Price: {menuItem.MenuPrice:F2}");
                    }
                    Console.WriteLine("Enter the number of the menu item to update:");
                    if (!int.TryParse(Console.ReadLine(), out int selectedIndex) || selectedIndex < 1 || selectedIndex > fetchResponse.MenuList.Count)
                    {
                        Console.WriteLine("Invalid selection. Please try again.");
                        return;
                    }

                    var selectedMenuItem = fetchResponse.MenuList[selectedIndex - 1];

                    Console.WriteLine($"Current Menu Type: {selectedMenuItem.MenuType}");
                    Console.WriteLine($"Current Menu Price: {selectedMenuItem.MenuPrice:F2}");
                }
                else
                {
                    var menuItem = fetchResponse.MenuList.First();
                    Console.WriteLine($"Current Menu Type: {menuItem.MenuType}");
                    Console.WriteLine($"Current Menu Price: {menuItem.MenuPrice:F2}");
                }

                Console.WriteLine("Enter new menu type:");
                string newMenuType = Console.ReadLine();

                Console.WriteLine("Enter new menu price:");
                if (!decimal.TryParse(Console.ReadLine(), out decimal newMenuPrice))
                {
                    Console.WriteLine("Invalid price format. Please try again.");
                    return;
                }
                var updateMenuRequestDto = new UpdateMenuRequestDto
                {
                    MenuName = menuName.ToLower(),
                    MenuType = newMenuType.ToLower(),
                    MenuPrice = newMenuPrice
                };

                var responseJson = await _adminService.UpdateMenuAsync(updateMenuRequestDto);
                Console.WriteLine(responseJson.Message);
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
                    MenuName = menuName.ToLower(),
                };

                var responseJson = await _adminService.DeleteMenuAsync(deleteMenuRequestDto);
                Console.WriteLine(responseJson.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
