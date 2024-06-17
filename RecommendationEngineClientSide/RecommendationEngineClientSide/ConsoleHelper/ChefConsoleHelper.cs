using RecommendationEngineClientSide.ChefDTO;
using RecommendationEngineClientSide.DTO;
using RecommendationEngineClientSide.Services.ChefServices;
using System;
using System.Threading.Tasks;

namespace RecommendationEngineClientSide.ConsoleHelper
{
    public class ChefConsoleHelper
    {
        private readonly IChefService _chefService;
        public bool ShouldLogout { get; private set; }
        public ChefConsoleHelper(IChefService chefService)
        {
            _chefService = chefService;
            ShouldLogout = false;
        }

        public async Task HandleChefRoleAsync()
        {
            bool continueLoop = true;
            while (continueLoop)
            {
                Console.WriteLine("Welcome, Chef! Choose an option:");
                Console.WriteLine("1. Get Menu List");
                Console.WriteLine("2. Add Daily Menu");
                Console.WriteLine("3. Logout");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await GetMenuListAsync();
                        break;
                    case "2":
                        await AddDailyMenuAsync();
                        break;
                    case "3":
                        continueLoop = false;
                        Console.WriteLine("Logging out...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private async Task GetMenuListAsync()
        {
            try
            {
                Console.WriteLine("Enter the date (YYYY-MM-DD):");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
                {
                    Console.WriteLine("Invalid date format. Please try again.");
                    return;
                }

                var responseJson = await _chefService.GetMenuListAsync(date);
                // Assuming the response contains the menu list
                if (responseJson is MenuListDto menuList)
                {
                    foreach (var menuItem in menuList.Menu)
                    {
                        Console.WriteLine($"Item: {menuItem.MenuItemName}, Type: {menuItem.MenuItemType}, Price: {menuItem.Price}, Rating: {menuItem.Rating}");
                    }
                }
                else
                {
                    Console.WriteLine(responseJson.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private async Task AddDailyMenuAsync()
        {
            try
            {
                Console.WriteLine("Enter the date for the daily menu (YYYY-MM-DD):");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
                {
                    Console.WriteLine("Invalid date format. Please try again.");
                    return;
                }

                var menuItems = new List<MenuListItemDto>();
                while (true)
                {
                    Console.WriteLine("Enter menu item name (or 'done' to finish):");
                    string menuItemName = Console.ReadLine();
                    if (menuItemName.ToLower() == "done")
                        break;

                    menuItems.Add(new MenuListItemDto { MenuItemName = menuItemName });
                }

                var newDailyMenuDto = new NewDailyMenuDto
                {
                    CurrentDate = date,
                    Menu = menuItems
                };

                var responseJson = await _chefService.AddDailyMenuAsync(newDailyMenuDto);
                Console.WriteLine(responseJson.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
