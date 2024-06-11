using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using RecommendationEngineServerSide.Common.DTO;
using RecommendationEngineServerSide.Controller.AdminControllers;
using RecommendationEngineServerSide.Controller.LoginControllers;
using RecommendationEngineServerSide.Service.RegisterService;

namespace RecommendationEngineServerSide.Controller
{
    public class ControllerRouter
    {
        private readonly LoginController _loginController;
        private readonly AdminController _adminController;

        public ControllerRouter(LoginController controller, AdminController adminController)
        {
            _loginController = controller;
            _adminController = adminController;
        }

        public async Task<string> RouteRequestAsync(string controllerName, string actionName, JsonElement data)
        {
            switch (controllerName)
            {
                case "LoginController":
                    return await RouteLoginControllerActions(actionName, data);
                    
                case "AdminController":
                    return await RouteAdminControllerActions(actionName, data);
                    
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
