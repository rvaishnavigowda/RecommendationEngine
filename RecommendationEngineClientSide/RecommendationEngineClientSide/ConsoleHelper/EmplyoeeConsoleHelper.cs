using Newtonsoft.Json;
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
            await HandleDailyNotification();
            await HandleImproveMenuItem();
            bool continueLoop = true;
            while (continueLoop)
            {
                Console.WriteLine("\nWelcome, Employee! Choose an option:");
                Console.WriteLine("1. Get Daily Menu");
                Console.WriteLine("2. Place Order");
                Console.WriteLine("3. Give Feedback");
                Console.WriteLine("4. Profile Update");
                Console.WriteLine("5. Logout");
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
                        await UserProfileUpdateAsync();
                        break;
                    case "5":
                        continueLoop = false;
                        Console.WriteLine("Logging out...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        #region Private Methods
        private async Task HandleDailyNotification()
        {
            var notificationsResponse = await _employeeService.FetchNotificationsAsync(_username);
            if (notificationsResponse.Status == "Success")
            {
                if(notificationsResponse.Notifications.Count>0)
                {
                    foreach (var notification in notificationsResponse.Notifications)
                    {
                        Console.WriteLine("Notifications:");
                        Console.WriteLine($"- {notification}\n");
                    }
                }
                else
                {
                    Console.WriteLine("No new notifications today.\n");
                }
            }
            else if(notificationsResponse.Message== "There are no notifications to show") { }
            else 
            {
                Console.WriteLine($" {notificationsResponse.Message}");
            }
        }
        private async Task HandleImproveMenuItem()
        {
            var notification = await _employeeService.FetchFeedbackQuestion(_username);
            if (notification.Status == "Success")
            { 
                Console.WriteLine(notification.Message);
                UserMenuUpgradeListDTO userMenuUpgradeList = new UserMenuUpgradeListDTO()
                {
                    UserName = _username,
                    MenuName = notification.Message.Split(':')[1].Replace(" ", "").ToLower(),

                    menuFeedback = new List<string>()
                };
                foreach (var item in notification.Notifications)
                {
                    Console.WriteLine(item);
                    string userInput = Console.ReadLine();
                    userMenuUpgradeList.menuFeedback.Add(userInput);
                }
                var serverResponse = await _employeeService.UpdateMenuUpgradeFeedback(userMenuUpgradeList);
                if (serverResponse.Status == "Success")
                {
                    Console.WriteLine(serverResponse.Message);
                }
                else
                {
                    Console.WriteLine(serverResponse.Message);
                }
            }
            else if(notification.Message== "There are no monthly notifications.")
            {

            }
            else
            {
                Console.WriteLine(notification.Message);
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

                if (response.Status == "Success")
                {
                    if (response.MenuList != null && response.MenuList.Count > 0)
                    {
                        var groupedMenuItems = response.MenuList
                            .GroupBy(menuItem => menuItem.MenuType)
                            .OrderBy(group => group.Key);

                        foreach (var group in groupedMenuItems)
                        {
                            Console.WriteLine($"\n{char.ToUpper(group.Key[0]) + group.Key.Substring(1)}:");
                            Console.WriteLine("{0,-20} {1,10} {2,10}", "Menu", "Price", "Rating");
                            Console.WriteLine(new string('-', 42));

                            foreach (var menuItem in group)
                            {
                                Console.WriteLine("{0,-20} {1,10:F2} {2,10:F2}", menuItem.MenuName, menuItem.Price, menuItem.Rating);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("No menu items found.");
                    }
                }
                else if (response.Status == "Void")
                {
                    Console.WriteLine($"\n{response.Message}");
                    Console.WriteLine("Your order is:\n");
                    Console.WriteLine("{0,-20} {1,10}", "Menu", "Price");
                    Console.WriteLine(new string('-', 32));
                    foreach (var menuItem in response.MenuList)
                    {
                        Console.WriteLine("{0,-20} {1,10:F2}", menuItem.MenuName, menuItem.Price);
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
                
                var orderItems = new List<OrderItemDto>();
                bool addingItems = true;
                while (addingItems)
                {
                    Console.WriteLine("Enter the type of menu you are ordering:");
                    string orderMenutype = Console.ReadLine();
                    Console.WriteLine("Enter menu item name:");
                    string menuItemName = Console.ReadLine();

                    orderItems.Add(new OrderItemDto
                    {
                        MenuName = menuItemName.ToLower(),
                        OrderMenutype=orderMenutype.ToLower()
                    }) ;

                    Console.WriteLine("Do you want to add more items? (yes/no)");
                    string addMore = Console.ReadLine();
                    addingItems = addMore.Equals("yes", StringComparison.OrdinalIgnoreCase);
                }

                var orderDetailRequestDto = new OrderDetailRequestDto
                {
                    UserName = _username.ToLower(),
                    OrderDate = _date,
                    Items = orderItems
                };

                var response = await _employeeService.PlaceOrderAsync(orderDetailRequestDto);
                Console.WriteLine(response.Message);
                //if(response.Status=="Success")
                //{
                    if (response.Items != null && response.Items.Count > 0)
                    {
                        Console.WriteLine("\nOrder Details:");
                        Console.WriteLine("{0,-20} {1,-10}", "Menu Name", "Menu Type");
                        Console.WriteLine(new string('-', 30));
                        foreach (var item in response.Items)
                        {
                            Console.WriteLine("{0,-20} {1,-10}", item.MenuName, item.OrderMenutype);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No order items found.");
                    }
               // }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}\n");
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
        private async Task UserProfileUpdateAsync()
        {
            try
            {
                var userProfileDetails = await _employeeService.FetchUserProfileQuestion(_username);
                if (userProfileDetails.Status == "Success")
                {
                    var profileData = userProfileDetails.ProfileQuestions;
                    var questionAnswerPairs = new List<string>();

                    foreach (var question in profileData)
                    {
                        Console.WriteLine($"Question: {question.Question}");
                        for (int i = 0; i < question.ProfileAnswers.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {question.ProfileAnswers[i]}");
                        }
                        int selectedOption;
                        while (true)
                        {
                            Console.Write("Select your answer by entering the corresponding number: ");
                            string input = Console.ReadLine();
                            if (int.TryParse(input, out selectedOption) && selectedOption > 0 && selectedOption <= question.ProfileAnswers.Count)
                            {
                                break; 
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter a valid number corresponding to an answer option.");
                            }
                        }
                        string selectedAnswer = question.ProfileAnswers[selectedOption - 1];

                        questionAnswerPairs.Add(selectedAnswer);
                    }
                    UserProfileDetailDTO userProfileDetail = new UserProfileDetailDTO()
                    {
                        userName = _username,
                        userResponse = questionAnswerPairs
                    };
                    var submitResponse = await _employeeService.SubmitUserProfileAnswers(userProfileDetail);
                    Console.WriteLine(submitResponse.Message, "\n");
                }
                else
                {
                    Console.WriteLine($"Failed to fetch user profile questions: {userProfileDetails.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        #endregion
    }
}