using System.Text.Json;
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

                case "EmplyoeeController":
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
                default:
                    throw new ArgumentException("Invalid action name");
            }
        }

        private async Task<string> RouteEmployeeControllerActions(string actionName, JsonElement data)
        {
            switch (actionName)
            {
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
                default:
                    throw new ArgumentException("Invalid action name");
            }
        }

        private async Task<string> RouteChefControllerActions(string actionName, JsonElement data)
        {
            switch (actionName)
            {
                case "HandleGetMenuList":
                    var date = await DeserializeJson<DateTime>(data);
                    var menuListResult = await _chefController.HandleGetMenuList(date);
                    return await SerializeJson(menuListResult);

                case "HandleAddDailyMenu":
                    var newDailyMenuDTO = await DeserializeJson<NewDailyMenuDTO>(data);
                    var addDailyMenuResult = await _chefController.HandleAddDailyMenu(newDailyMenuDTO);
                    return await SerializeJson(addDailyMenuResult);

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
