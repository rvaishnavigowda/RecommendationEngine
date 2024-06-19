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

        public async Task HandleChefRoleAsync(string userName, DateTime loginDate)
        {
            _loginDate = loginDate;
            var notificationsResponse = await _chefService.FetchNotificationsAsync(userName);
            if (notificationsResponse.Status == "Success")
            {
                Console.WriteLine("Notifications:");
                foreach (var notification in notificationsResponse.Notifications)
                {
                    Console.WriteLine($"- {notification}\n");
                }
            }
            else
            {
                Console.WriteLine($" {notificationsResponse.Message}");
            }
            bool continueLoop = true;
            while (continueLoop)
            {
                Console.WriteLine("\nWelcome, Chef! Choose an option:");
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
                    var groupedMenuItems = menuList.Menu
                        .GroupBy(menuItem => menuItem.MenuItemType.ToLower())
                        .OrderBy(group => group.Key);

                    foreach (var group in groupedMenuItems)
                    {
                        Console.WriteLine($"\n{char.ToUpper(group.Key[0]) + group.Key.Substring(1)}:");
                        Console.WriteLine(new string('-', 57));                       
                        Console.WriteLine("{0,-20} {1,-10} {2,-10} {3,-15}", "Menu", "Price", "Rating", "Order Count");
                        foreach (var menuItem in group.OrderByDescending(menuItem => menuItem.Rating))
                        {
                            Console.WriteLine("{0,-20} {1,-10:F2} {2,-10} {3,-15}",
                                menuItem.MenuItemName.ToLower(),
                                menuItem.Price,
                                menuItem.Rating,
                                menuItem.OrderCount);
                        }
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
