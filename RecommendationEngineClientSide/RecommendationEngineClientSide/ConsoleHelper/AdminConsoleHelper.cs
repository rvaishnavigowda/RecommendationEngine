using RecommendationEngineClientSide.ChefDTO;
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
        private  DateTime _loginDate;

        public AdminConsoleHelper(IAdminService adminService)
        {
            _adminService = adminService;
            ShouldLogout = false;
        }

        public async Task HandleAdminRoleAsync(DateTime loginDate)
        {
            _loginDate=loginDate;
            bool continueLoop = true;

            Console.WriteLine("Welcome, Admin! \nChoose an option:");
            while (continueLoop)
            {
                Console.WriteLine("1. Add Menu");
                Console.WriteLine("2. Update Menu");
                Console.WriteLine("3. Delete Menu");
                Console.WriteLine("4. Display Menu List");
                Console.WriteLine("5. Logout");
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
                        await GetAllMenuAsync();
                        break;
                    case "5":
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

                // Show options and get input for FoodType
                Console.WriteLine("Select food type:");
                Console.WriteLine("1 - Veg");
                Console.WriteLine("2 - NonVeg");
                Console.WriteLine("3 - Egg");
                if (!int.TryParse(Console.ReadLine(), out int foodType) || foodType < 1 || foodType > 3)
                {
                    Console.WriteLine("Invalid food type. Please try again.");
                    return;
                }

                Console.WriteLine("Select cuisine type:");
                Console.WriteLine("1 - NorthIndian");
                Console.WriteLine("2 - SouthIndian");
                Console.WriteLine("3 - Chinese");
                if (!int.TryParse(Console.ReadLine(), out int cuisineType) || cuisineType < 1 || cuisineType > 3)
                {
                    Console.WriteLine("Invalid cuisine type. Please try again.");
                    return;
                }

                // Show options and get input for SpiceLevel
                Console.WriteLine("Select spice level:");
                Console.WriteLine("1 - Low");
                Console.WriteLine("2 - Medium");
                Console.WriteLine("3 - High");
                if (!int.TryParse(Console.ReadLine(), out int spiceLevel) || spiceLevel < 1 || spiceLevel > 3)
                {
                    Console.WriteLine("Invalid spice level. Please try again.");
                    return;
                }

                // Show options and get input for SweetLevel
                Console.WriteLine("Is it sweet? (yes/no)");
                string sweetInput = Console.ReadLine().ToLower();
                bool isSweet;
                if (sweetInput == "yes")
                {
                    isSweet = true;
                }
                else if (sweetInput == "no")
                {
                    isSweet = false;
                }
                else
                {
                    Console.WriteLine("Invalid input for sweetness. Please enter 'yes' or 'no'.");
                    return;
                }

                var addMenuRequestDto = new AddMenuRequestDto
                {
                    MenuName = menuName.ToLower(),
                    MenuType = menuType.ToLower(),
                    MenuPrice = menuPrice,
                    FoodType = foodType,
                    CuisineType = cuisineType,
                    SpiceLevel = spiceLevel,
                    IsSweet = isSweet,
                    dateCreated = _loginDate
                };

                var responseJson = await _adminService.AddMenuAsync(addMenuRequestDto);
                Console.WriteLine(responseJson.Message);
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

                if (fetchResponse.Status == "Failure")
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

        private async Task GetAllMenuAsync()
        {
            try
            {
                var response = await _adminService.GetAllMenuAsync();
                if (response is MenuListDto menuList)
                {
                    var groupedMenuItems = menuList.Menu
                        .GroupBy(menuItem => menuItem.MenuItemType.ToLower())
                        .OrderBy(group => group.Key);

                    foreach (var group in groupedMenuItems)
                    {
                        Console.WriteLine($"\n{char.ToUpper(group.Key[0]) + group.Key.Substring(1)}:");
                        Console.WriteLine(new string('-', 37));
                        Console.WriteLine("{0,-20} {1,-10}", "Menu", "Price");
                        Console.WriteLine(new string('-', 37));
                        foreach (var menuItem in group.OrderByDescending(menuItem => menuItem.Rating))
                        {
                            Console.WriteLine("{0,-20} {1,-10:F2}",
                                menuItem.MenuItemName.ToLower(),
                                menuItem.Price
                                );
                        }
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
    }
}
