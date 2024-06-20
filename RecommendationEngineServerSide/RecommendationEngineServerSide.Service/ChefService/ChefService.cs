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

        public async Task<List<string>> GetNotification(string userName)
        {
            var userDetails=(await _unitOfWork.User.GetAll()).FirstOrDefault(a=>a.UserName.ToLower()==userName);
            List<string> notification = new List<string>();
            notification = await _notificationService.GetNotification(userDetails.UserId);
            return notification;
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
                    MenuItemName = menuItem.MenuItemName.ToLower(),
                    MenuItemType = menuItem.MenuItemType.ToLower(),
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
                //var menu = new DailyMenu()
                //{
                //    DailyMenuDate = menuList.CurrentDate,
                //};
                var isdailyMenuAdded=(await _unitOfWork.DailyMenu.GetAll()).Any(a=>a.DailyMenuDate==menuList.CurrentDate);
                if(!isdailyMenuAdded)
                {
                    foreach (var item in menuList.Menu)
                    {
                        var menuDetails = (await _unitOfWork.Menu.GetAll()).FirstOrDefault(a => a.MenuName.ToLower() == item.MenuItemName);
                        if (menuDetails != null)
                        {
                            var menu = new DailyMenu()
                            {
                                DailyMenuDate = menuList.CurrentDate,
                                MenuId = menuDetails.MenuId
                            };

                            await _unitOfWork.DailyMenu.Add(menu);
                            await _unitOfWork.Save();
                            await UpdateNotification(menuList.CurrentDate);
                        }
                        else
                        {
                            throw MenuException.HandleWrongMenuItem();
                        }
                    }
                }
                else
                {
                    throw MenuException.HandleDailyMenuAdded();
                }
            }
            else
            {
                throw CommonException.NullInputException();
            }
        }

        public async Task GetOrders(DateTime date)
        {
            var isDailyMenuRolled=(await _unitOfWork.DailyMenu.GetAll()).Where(a=>a.DailyMenuDate.Equals(date)).ToList();
            if(isDailyMenuRolled.Count>0)
            {

            }
        }
        private async Task UpdateNotification(DateTime date)
        {
            int notificationTypeId = (int)NotificationTypeEnum.NextDayMenuRecommendation;
            await _notificationService.AddNotification(ApplicationConstant.DailyMenuAddedNotification, notificationTypeId, date);
        }

    }
}
