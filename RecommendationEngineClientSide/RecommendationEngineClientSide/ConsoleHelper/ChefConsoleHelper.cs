using RecommendationEngineClientSide.ChefDTO;
using RecommendationEngineClientSide.DTO;
using RecommendationEngineClientSide.Services.ChefServices;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace RecommendationEngineClientSide.ConsoleHelper
{
    public class ChefConsoleHelper
    {
        private readonly IChefService _chefService;
        public bool ShouldLogout { get; private set; }
        private DateTime _loginDate;
        private string _userName;
        public ChefConsoleHelper(IChefService chefService)
        {
            _chefService = chefService;
            ShouldLogout = false;
        }

        public async Task HandleChefRoleAsync(string userName, DateTime loginDate)
        {
            _loginDate = loginDate;
            _userName=userName;
            Console.WriteLine("\nWelcome, " + _userName+ "\nChoose an option:\n");
            await GetDailyNotification();
            await GetMonthlyNotification();
            bool continueLoop = true;
            while (continueLoop)
            {
                Console.WriteLine();
                Console.WriteLine("1. Get Menu List");
                Console.WriteLine("2. Add Daily Menu");
                Console.WriteLine("3. Get Order List");
                Console.WriteLine("4. Logout");
                Console.Write("Your Choice: ");
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
                        await GetOrderList();
                        break;
                    case "4":
                        continueLoop = false;
                        Console.WriteLine("Logging out...\n");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private async Task GetDailyNotification()
        {
            DailyMenuRequestDto userDetail = new DailyMenuRequestDto()
            {
                UserName = _userName,
                CurrentDate = _loginDate,
            };
            var notificationsResponse = await _chefService.FetchNotificationsAsync(userDetail);
            if (notificationsResponse.Status == "Success" )
            {
                if(notificationsResponse.Notifications.Count > 0)
                {
                    Console.WriteLine("Notifications:");
                    foreach (var notification in notificationsResponse.Notifications)
                    {
                        Console.WriteLine($"- {notification}\n");
                    }
                }
                else
                {
                    Console.WriteLine("No new notifications today.\n");
                }
                
            }
            else
            {
                Console.WriteLine($"{notificationsResponse.Message}\n");
            }
        }

        private async Task GetMonthlyNotification()
        {
            
            var notificationsResponse = await _chefService.FetchMonthlyNotificationASync(_loginDate);
            if (notificationsResponse.Status == "Success"  )
            {
                if(notificationsResponse.Menu.Count > 0)
                {
                    await GetChefChoice(notificationsResponse);
                }
                
            }
            else if(notificationsResponse.Message== "It hasn't been a month since the last deletion of items.")
            {

            }
            else
            {
                Console.WriteLine($" {notificationsResponse.Message}");
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
                        Console.WriteLine(new string('-', 57));
                        foreach (var menuItem in group.OrderByDescending(menuItem => menuItem.Rating))
                        {
                            Console.WriteLine("{0,-20} {1,-10:F2} {2,-10:F2} {3,-15}",
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
        private async Task GetOrderList()
        {
            try
            {
                var serverResponse = await _chefService.GetOrderDetails(_loginDate);
                if(serverResponse.Status == "Success")
                {
                    Console.WriteLine(new string('-', 40));
                    Console.WriteLine("{0,-20} {1,-10}", "Item", "Order Count");
                    Console.WriteLine(new string('-', 40));
                    foreach(var order in serverResponse.OrderList)
                    {
                        Console.WriteLine("{0,-20} {1,-10}", order.MenuName, order.OrderCount);
                    }
                }
                else
                {
                    Console.WriteLine(serverResponse.Message);
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }
        private async Task GetChefChoice(MenuListDto notification)
        {
            var menuItems = notification.Menu;

            if (menuItems.Count > 0)
            {
                Console.WriteLine("Menu Items for Review:");
                Console.WriteLine("{0,-20} {1,-10} {2,-20}", "Menu", "Rating", "Order Count");
                foreach (var item in menuItems)
                {
                    Console.WriteLine("{0,-20} {1,-10:F2} {2,-20}", item.MenuItemName.ToLower(), item.Rating, item.OrderCount);
                }

                for (int i = 0; i < menuItems.Count; i++)
                {
                    var item = menuItems[i];
                    bool continueLoop = true;
                    while (continueLoop)
                    {
                        Console.WriteLine($"\nChoose an option for {item.MenuItemName.ToLower()}:");
                        Console.WriteLine("1. Discard this Food Item");
                        Console.WriteLine("2. Upgrade this Food Item");
                        Console.WriteLine("3. Skip");
                        string choice = Console.ReadLine();

                        switch (choice)
                        {
                            case "1":
                                await DiscardFoodItemAsync(item.MenuItemName);
                                continueLoop = false;
                                break;
                            case "2":
                                await UpgradeFoodItemAsync(item.MenuItemName);
                                continueLoop = false;
                                break;
                            case "3":
                                continueLoop = false;
                                break;
                            default:
                                Console.WriteLine("Invalid choice. Please try again.");
                                break;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("No menu items for review.");
            }
        }

        private async Task DiscardFoodItemAsync(string itemName)
        {
            Console.WriteLine($"Discarding the food item: {itemName}");
            var response = await _chefService.RemoveFoodItemAsync(itemName);
            Console.WriteLine(response.Message);
        }

        private async Task UpgradeFoodItemAsync(string itemName)
        {
            Console.WriteLine($"Upgrading the food item: {itemName}");
            var menuDTO = new UpgradeMenuDto()
            {
                CurrentDate = _loginDate,
                MenuName = itemName,
            };
            var response = await _chefService.UpgradeFoodItemAsync(menuDTO);
            Console.WriteLine(response.Message);
        }

    }
}
