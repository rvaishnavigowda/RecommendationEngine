using RecommendationEngineClientSide.DTO;
using RecommendationEngineClientSide.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineClientSide.ConsoleHelper
{
    public class AdminConsoleHelper
    {
        private readonly IAdminService _adminService;

        public AdminConsoleHelper(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task HandleAdminRoleAsync()
        {
            Console.WriteLine("Welcome, Admin! Choose an option:");
            Console.WriteLine("1. Add Menu");
            Console.WriteLine("2. Update Menu");
            Console.WriteLine("3. Delete Menu");
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
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        private async Task AddMenuAsync()
        {
            Console.WriteLine("Enter menu name:");
            string menuName = Console.ReadLine();

            Console.WriteLine("Enter menu type:");
            string menuType = Console.ReadLine();

            Console.WriteLine("Enter menu price:");
            decimal menuPrice = decimal.Parse(Console.ReadLine());

            var addMenuRequestDto = new AddMenuRequestDto
            {
                MenuName = menuName,
                MenuType = menuType,
                MenuPrice = menuPrice
            };

            string responseJson = await _adminService.AddMenuAsync(addMenuRequestDto);
            Console.WriteLine($"Server response: {responseJson}");
        }

        private async Task UpdateMenuAsync()
        {
            Console.WriteLine("Enter menu name to update:");
            string menuName = Console.ReadLine();

            Console.WriteLine("Enter new menu type:");
            string menuType = Console.ReadLine();

            Console.WriteLine("Enter new menu price:");
            decimal menuPrice = decimal.Parse(Console.ReadLine());

            var updateMenuRequestDto = new UpdateMenuRequestDto
            {
                MenuName = menuName,
                MenuType = menuType,
                MenuPrice = menuPrice
            };

            string responseJson = await _adminService.UpdateMenuAsync(updateMenuRequestDto);
            Console.WriteLine($"Server response: {responseJson}");
        }

        private async Task DeleteMenuAsync()
        {
            Console.WriteLine("Enter menu name to delete:");
            string menuName = Console.ReadLine();

            Console.WriteLine("Enter menu type:");
            string menuType = Console.ReadLine();

            Console.WriteLine("Enter menu price:");
            decimal menuPrice = decimal.Parse(Console.ReadLine());

            var deleteMenuRequestDto = new DeleteMenuRequestDto
            {
                MenuName = menuName,
                MenuType = menuType,
                MenuPrice = menuPrice
            };

            string responseJson = await _adminService.DeleteMenuAsync(deleteMenuRequestDto);
            Console.WriteLine($"Server response: {responseJson}");
        }
    }
}
