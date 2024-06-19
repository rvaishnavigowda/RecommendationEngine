using RecommendationEngineServerSide.Common.ApplicationConst;
using RecommendationEngineServerSide.Common.DTO;
using RecommendationEngineServerSide.Common.Enum;
using RecommendationEngineServerSide.Common.Exceptions;
using RecommendationEngineServerSide.DAL.Model;
using RecommendationEngineServerSide.DAL.UnitfWork;
using RecommendationEngineServerSide.Service.NotificationService;
using RecommendationEngineServerSide.Service.RecommendationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.Service.ChefService
{
    public class ChefService:IChefService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationService _notificationService;
        private readonly IRecommendationService _recommendationService;

        public ChefService(IUnitOfWork unitOfWork, INotificationService notificationService, IRecommendationService recommendationService)
        {
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
            _recommendationService = recommendationService;
        }

        public async Task<MenuListDTO> GetMenuList(DateTime date)
        {
            var recommendedMenuItems = await _recommendationService.GetRecommendedMenuItems();

            MenuListDTO dailyMenuList = new MenuListDTO()
            {
                Menu = new List<ListMenuDTO>()
            };

            foreach (var menuItem in recommendedMenuItems)
            {
                var item = new ListMenuDTO()
                {
                    MenuItemName = menuItem.MenuItemName,
                    MenuItemType = menuItem.MenuItemType,
                    Price = menuItem.Price,
                    Rating = menuItem.AverageRating,
                    OrderCount = menuItem.OrderCount 
                };
                dailyMenuList.Menu.Add(item);
            }

            return dailyMenuList;
        
    }

        public async Task AddDailyMenu(NewDailyMenuDTO menuList)
        {
            if(menuList != null)
            {
                var menu = new DailyMenu()
                {
                    DailyMenuDate = menuList.CurrentDate,
                };
                foreach(var item in menuList.Menu)
                {
                    menu.MenuId = (await _unitOfWork.Menu.GetAll()).FirstOrDefault(a => a.MenuName == item.MenuItemName).MenuId;
                    await _unitOfWork.DailyMenu.Add(menu);
                    await _unitOfWork.Save();
                    await UpdateNotification();
                }
            }
            else
            {
                throw CommonException.NullInputException();
            }
        }
        private async Task UpdateNotification()
        {
            int notificationTypeId = (int)NotificationTypeEnum.NewMenuItemAdded;
            await _notificationService.AddNotification(ApplicationConstant.DailyMenuAddedNotification, notificationTypeId);
        }

    }
}
