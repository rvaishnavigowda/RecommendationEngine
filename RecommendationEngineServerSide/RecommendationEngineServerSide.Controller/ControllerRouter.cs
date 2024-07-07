using System.Net.WebSockets;
using System.Text.Json;
using Microsoft.Identity.Client;
using RecommendationEngineServerSide.Common.DTO;
using RecommendationEngineServerSide.Controller.AdminControllers;
using RecommendationEngineServerSide.Controller.ChefControllers;
using RecommendationEngineServerSide.Controller.EmployeeControllers;
using RecommendationEngineServerSide.Controller.LoginControllers;

namespace RecommendationEngineServerSide.Controller
{
    public class ControllerRouter
    {
        private readonly LoginController _loginController;
        private readonly AdminController _adminController;
        private readonly EmployeeController _employeeController;
        private readonly ChefController _chefController;

        public ControllerRouter(LoginController controller, AdminController adminController, EmployeeController employeeController, ChefController chefController)
        {
            _loginController = controller;
            _adminController = adminController;
            _employeeController = employeeController;
            _chefController = chefController;
        }

        public async Task<string> RouteRequestAsync(string controllerName, string actionName, JsonElement data)
        {
            switch (controllerName)
            {
                case "LoginController":
                    return await RouteLoginControllerActions(actionName, data);
                    
                case "AdminController":
                    return await RouteAdminControllerActions(actionName, data);

                case "EmployeeController":
                    return await RouteEmployeeControllerActions(actionName, data);

                case "ChefController":
                    return await RouteChefControllerActions(actionName, data);

            }
            return "";
        }

        private async Task<string> RouteLoginControllerActions(string actionName, JsonElement data)
        {
            switch (actionName)
            {
                case "HandleLoginRequest":
                    var userDTO = await DeserializeJson<UserDTO>(data);
                    var loginResult = await _loginController.HandleLoginRequest(userDTO);
                    return await SerializeJson(loginResult);
                default:
                    throw new ArgumentException("Invalid action name");
            }
        }

        private async Task<string> RouteAdminControllerActions(string actionName, JsonElement data)
        {
            switch (actionName)
            {
                case "HandleAddMenu":
                    var menuDTO = await DeserializeJson<MenuDTO>(data);
                    var addMenuResult = await _adminController.HandleAddMenu(menuDTO);
                    return await SerializeJson(addMenuResult);

                case "HandleUpdateMenu":
                    var updateMenuDTO = await DeserializeJson<UpdateMenuDTO>(data);
                    var updateMenuResult = await _adminController.HandleUpdateMenu(updateMenuDTO);
                    return await SerializeJson(updateMenuResult);

                case "DeleteMenu":
                    var deleteMenuDTO = await DeserializeJson<DeleteMenuDTO>(data);
                    var deleteMenuResult = await _adminController.DeleteMenu(deleteMenuDTO);
                    return await SerializeJson(deleteMenuResult);
                case "FetchMenuDetails":
                    var fetchMenuRequestDto = await DeserializeJson<FetchMenuRequestDTO>(data);
                    var fetchMenuResult = await _adminController.FetchMenuDetails(fetchMenuRequestDto);
                    return await SerializeJson(fetchMenuResult);
                case "GetAllMenu":
                    var result = await _adminController.GetAllMenu();
                    return await SerializeJson(result);

                default:
                    throw new ArgumentException("Invalid action name");
            }
        }

        private async Task<string> RouteEmployeeControllerActions(string actionName, JsonElement data)
        {
            switch (actionName)
            {
                case "HandleGetNotification":
                    var userName= await DeserializeJson<userDto>(data);
                    var notificationresult= await _employeeController.HandleGetNotification(userName.UserName);
                    return await SerializeJson(notificationresult);
                case "HandleGetDailyMenu":
                    var dailyMenuRequest = await DeserializeJson<DailyMenuDTO>(data);
                    var dailyMenuResult = await _employeeController.HandleGetDailyMenu(dailyMenuRequest.UserName, dailyMenuRequest.CurrentDate);
                    return await SerializeJson(dailyMenuResult);
                case "HandlePlaceOrder":
                    var orderDetailDTO = await DeserializeJson<OrderDetailDTO>(data);
                    var placeOrderResult = await _employeeController.HandlePlaceOrder(orderDetailDTO);
                    return await SerializeJson(placeOrderResult);
                case "HandleGiveFeedback":
                    var feedbackDTO = await DeserializeJson<FeedbackDTO>(data);
                    var giveFeedbackResult = await _employeeController.HandleGiveFeedback(feedbackDTO);
                    return await SerializeJson(giveFeedbackResult);
                case "HandleMonthlyFeedback":
                    var feedbacks=await DeserializeJson<userDto > (data);
                    var monthlyFeedbacks = await _employeeController.HandleGetMonthlyNotification(feedbacks.UserName);
                    return await SerializeJson(monthlyFeedbacks);
                case "HandleMenuUpgradeFeedback":
                    var userMenuUpgrade= await DeserializeJson<UserMenuUpgradeDTO> (data);
                    var menuUpgradeResponse=await _employeeController.HandleMenuUpgradeFeedback(userMenuUpgrade);
                    return await SerializeJson(menuUpgradeResponse);
                case "HandleGetUserProfile":
                    var userDetail=await DeserializeJson<userDto> (data);
                    var userProfile=await _employeeController.HandleGetUserProfile(userDetail.UserName);
                    return await SerializeJson(userProfile);
                case "HandleUpdateUserProfile":
                    var userProfileDetails=await DeserializeJson<UserProfileDetailDTO> (data);
                    var updateResponse= await _employeeController.HandleUpdateUserProfile(userProfileDetails);
                    return await SerializeJson(updateResponse);
                default:
                    throw new ArgumentException("Invalid action name");
            }
        }

        private async Task<string> RouteChefControllerActions(string actionName, JsonElement data)
        {
            switch (actionName)
            {
                case "HandleGetNotification":
                    var requestObject = JsonSerializer.Deserialize<userDto>(data);
                    var notificationresult = await _chefController.HandleGetNotification(requestObject.UserName);
                    return await SerializeJson(notificationresult);
                case "HandleMontlyNotification":
                    var currentDate = JsonSerializer.Deserialize<DateTime> (data);
                    var notificationResult = await _chefController.HandleMonthlyNotification(currentDate);
                    return await SerializeJson(notificationResult);
                case "HandleGetMenuList":
                    var date = await DeserializeJson<DateTime>(data);
                    var menuListResult = await _chefController.HandleGetMenuList(date);
                    return await SerializeJson(menuListResult);
                case "HandleAddDailyMenu":
                    var newDailyMenuDTO = await DeserializeJson<NewDailyMenuDTO>(data);
                    var addDailyMenuResult = await _chefController.HandleAddDailyMenu(newDailyMenuDTO);
                    return await SerializeJson(addDailyMenuResult);
                case "HandleDiscardFoodItem":
                    var itemName= await DeserializeJson<string>(data);
                    var result=await _chefController.HandleDiscardFoodItem(itemName);
                    return await SerializeJson(result);
                case "HandleImproveFoodItem":
                    var itemDetails=await DeserializeJson<UpgradeMenuDto>(data);
                    var improveItemresult = await _chefController.HandleImproveFoodItem(itemDetails);
                    return await SerializeJson(improveItemresult);
                default:
                    throw new ArgumentException("Invalid action name");
            }
        }
        private Task<T?> DeserializeJson<T>(JsonElement jsonData)
        {
            return Task.FromResult(JsonSerializer.Deserialize<T>(jsonData.GetRawText()));
        }

        private Task<string> SerializeJson<T>(T data)
        {
            return Task.FromResult(JsonSerializer.Serialize(data));
        }
    }
}
