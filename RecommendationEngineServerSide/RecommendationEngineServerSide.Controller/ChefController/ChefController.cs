using System;
using System.Threading.Tasks;
using AutoMapper;
using RecommendationEngineServerSide.Common.DTO;
using RecommendationEngineServerSide.Common.Exceptions;
using RecommendationEngineServerSide.Service.ChefService;

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

        public async Task<NotificationDTO> HandleGetNotification(string userName)
        {
            try
            {
                var notifications = await _chefService.GetNotification(userName);

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
    }
}
