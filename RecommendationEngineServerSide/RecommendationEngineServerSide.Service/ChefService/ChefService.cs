﻿using RecommendationEngineServerSide.Common.ApplicationConst;
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

        public async Task<List<string>> GetNotification(string userName, DateTime date)
        {
            var userDetail=(await _unitOfWork.User.GetAll()).FirstOrDefault(a=>a.UserName.ToLower()==userName);
            List<string> notification = new List<string>();
            notification = await _notificationService.GetNotification(userDetail.UserId, date);
            return notification;
        }

        public async Task<MenuListDTO> GetMonthlyNotification(DateTime date)
        {
            var getDailyMenuDetails = (await _unitOfWork.DailyMenu.GetAll()).First();
            if(getDailyMenuDetails!=null)
            {
                double timeSpan = (date - getDailyMenuDetails.DailyMenuDate).TotalDays;
                if(timeSpan%30==0)
                {
                    MenuListDTO menuList = new MenuListDTO()
                    {
                        Menu = new List<ListMenuDTO>()
                    };
                    menuList.Menu = await _recommendationService.GetPoorRatedMenuList();
                    if(menuList.Menu != null)
                    {
                        return menuList;
                    }
                    else
                    {
                        throw MenuException.HandleNoMenuFound();
                    }
                }
                else
                {
                    throw MenuException.HandleNoNotification();
                }
            }
            else
            {
                throw DailyMenuException.MenuNotPresentException();
            }
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
                            await UpdateNotification(menuList.CurrentDate, ApplicationConstant.DailyMenuAddedNotification, (int)NotificationTypeEnum.NextDayMenuRecommendation);
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

        public async Task<OrderDTO> GetOrders(DateTime date)
        {
            var isDailyMenuRolled=(await _unitOfWork.DailyMenu.GetAll()).Where(a=>a.DailyMenuDate.Equals(date)).ToList();
            if(isDailyMenuRolled.Count>0)
            {
                OrderDTO orders = new OrderDTO()
                {
                    OrderList = new List<OrderDTO>()
                };
                foreach(var item in isDailyMenuRolled)
                {
                    var orderCount = (await _unitOfWork.UserOrder.GetAll()).Where(a => a.DailyMenuId == item.DailyMenuId && a.Order.OrderDate == date).ToList().Count();
                    OrderDTO order = new OrderDTO()
                    {
                        MenuName = item.Menu.MenuName,
                        OrderCount = orderCount
                    };
                    orders.OrderList.Add(order);
                }
                return orders;
            }
            else
            {
                throw MenuException.HandleMenuNOtRolled();
            }
        }
        public async Task DiscardMenuItem(string menuItem)
        {
            var isMenuItemPresent = (await _unitOfWork.Menu.Find(a => a.MenuName == menuItem)).FirstOrDefault();
            if (isMenuItemPresent != null && isMenuItemPresent.MenuStatus==1)
            {
                isMenuItemPresent.MenuStatus = 3;
                await _unitOfWork.Menu.Update(isMenuItemPresent);
                await _unitOfWork.Save();
            }
            else
            {
                throw AdminException.HandleMenuItemNotFound();
            }
        }
        public async Task ImproviseMenuItem(UpgradeMenuDto menuItem)
        {
            string notificationMessage = ApplicationConstant.ImproviseMenuItemNotification +" : "+ menuItem.MenuName;
            int notificationType = (int)NotificationTypeEnum.ImproveMenuItem;
            await UpdateNotification(menuItem.CurrentDate, notificationMessage, notificationType);
        }
        private async Task UpdateNotification(DateTime date, string notifiction, int notificationType)
        {
            await _notificationService.AddNotification(notifiction, notificationType, date);
        }
    }
}
