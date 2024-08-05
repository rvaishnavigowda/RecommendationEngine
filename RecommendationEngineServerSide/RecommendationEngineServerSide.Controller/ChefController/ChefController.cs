using System;
using System.Threading.Tasks;
using AutoMapper;
using RecommendationEngineServerSide.Common.DTO;
using RecommendationEngineServerSide.Common.Exceptions;
using RecommendationEngineServerSide.Service.ChefService;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RecommendationEngineServerSide.Controller.ChefControllers
{
    public class ChefController
    {
        private readonly IChefService _chefService;
        private readonly IMapper _mapper;

        public ChefController(IChefService chefService, IMapper mapper)
        {
            _chefService = chefService;
            _mapper = mapper;
        }

        public async Task<NotificationDTO> HandleGetNotification(DailyMenuDTO userDetails )
        {
            try
            {
                var notifications = await _chefService.GetNotification(userDetails.UserName, userDetails.CurrentDate);

                return new NotificationDTO
                {
                    Status = "Success",
                    Message = "Notifications retrieved successfully.",
                    Notifications = notifications
                };
            }
            catch (Exception ex)
            {
                return new NotificationDTO
                {
                    Status = "Failure",
                    Message = ex.Message
                };
            }
        }
        public async Task<MenuListDTO> HandleMonthlyNotification(DateTime date)
        {
            try
            {
                var notificationList=await _chefService.GetMonthlyNotification(date);
                notificationList.Status = "Success";
                notificationList.Message = "New notification: Below is the list of items that performed poor this month.";
                return notificationList;
            }
            catch(Exception ex)
            {
                return new MenuListDTO()
                {
                    Status = "Failure",
                    Message = ex.Message
                };
            }       
        }
        public async Task<MenuListDTO> HandleGetMenuList(DateTime date)
        {
            try
            {
                var result = await _chefService.GetMenuList(date);

                return new MenuListDTO
                {
                    Status = "Success",
                    Message = "Menu list retrieved successfully.",
                    Menu = result.Menu
                };
            }
            catch (DailyMenuException ex)
            {
                return new MenuListDTO
                {
                    Status = "Failure",
                    Message = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new MenuListDTO
                {
                    Status = "Failure",
                    Message =   ex.Message
                };
            }
        }
        public async Task<SocketResponseDTO> HandleAddDailyMenu(NewDailyMenuDTO menuList)
        {
            try
            {
                await _chefService.AddDailyMenu(menuList);

                return new SocketResponseDTO
                {
                    Status = "Success",
                    Message = "Daily menu added successfully."
                };
            }
            catch (CommonException ex)
            {
                return new SocketResponseDTO
                {
                    Status = "Failure",
                    Message = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new SocketResponseDTO
                {
                    Status = "Failure",
                    Message = ex.Message
                };
            }
        }

        public async Task<OrderDTO> HandleOrderList(DateTime date)
        {
            try
            {
                var orderList=await _chefService.GetOrders(date);


                orderList.Status = "Success";
                orderList.Message = "The order list has been successfully fetched";
                return orderList;
            }
            catch (Exception ex)
            {
                return new OrderDTO()
                {
                    Status = "Failure",
                    Message = ex.Message
                };
            }
        }
        public async Task<SocketResponseDTO> HandleDiscardFoodItem(string menuItem)
        {
            try
            {
                await _chefService.DiscardMenuItem(menuItem);

                var notificationList = new SocketResponseDTO();
                notificationList.Status = "Success";
                notificationList.Message = "The menu item has been successfully discarded";
                return notificationList;
            }
            catch (Exception ex)
            {
                return new MenuListDTO()
                {
                    Status = "Failure",
                    Message = ex.Message
                };
            }
        }
        public async Task<SocketResponseDTO> HandleImproveFoodItem(UpgradeMenuDto menuItem)
        {
            try
            {
                await _chefService.ImproviseMenuItem(menuItem);
                var notificationList = new SocketResponseDTO();
                notificationList.Status = "Success";
                notificationList.Message = "The notification has successfully sent to employees to improvise the menu item.";
                return notificationList;
            }
            catch (Exception ex)
            {
                return new MenuListDTO()
                {
                    Status = "Failure",
                    Message = ex.Message
                };
            }
        }
    }
}
