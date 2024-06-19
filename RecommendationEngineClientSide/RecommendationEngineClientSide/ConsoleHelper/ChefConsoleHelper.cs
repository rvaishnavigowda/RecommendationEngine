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
        private DateTime _loginDate;
        public ChefConsoleHelper(IChefService chefService)
        {
            _chefService = chefService;
            ShouldLogout = false;
        }

        public async Task HandleChefRoleAsync(DateTime loginDate)
        {
            _loginDate = loginDate;
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
                var responseJson = await _chefService.GetMenuListAsync(_loginDate);

                if (responseJson is MenuListDto menuList)
                {
                    Console.WriteLine("{0,-20} {1,-15} {2,-10} {3,-10} {4,-15}", "Menu", "MenuType", "Price", "Rating", "Order Count");
                    foreach (var menuItem in menuList.Menu)
                    {
                        Console.WriteLine("{0,-20} {1,-15} {2,-10:F2} {3,-10} {4,-15}", menuItem.MenuItemName, menuItem.MenuItemType, menuItem.Price, menuItem.Rating, menuItem.OrderCount);
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
                    CurrentDate = _loginDate,
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
