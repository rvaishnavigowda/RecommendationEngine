using RecommendationEngineServerSide.Common.DTO;
using RecommendationEngineServerSide.Common.Exceptions;
using RecommendationEngineServerSide.DAL.Model;
using RecommendationEngineServerSide.DAL.UnitfWork;
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

        public ChefService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MenuListDTO> GetMenuList(DateTime date)
        {
            var menuList = (await _unitOfWork.Menu.GetAll());
            if (menuList != null)
            {
                MenuListDTO dailyMenuList = new MenuListDTO()
                {
                    Menu = new List<ListMenuDTO>()
                };
                foreach(var menuItem in menuList)
                {
                    var item = new ListMenuDTO()
                    {
                        MenuItemName = menuItem.MenuName,
                        MenuItemType = menuItem.MenuType.MenuTypeName,
                        Price = menuItem.Price,
                    };
                    dailyMenuList.Menu.Add(item);
                }
                return dailyMenuList;
            }
            else
            {
                throw DailyMenuException.MenuNotPresentException();
            }
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
                }
            }
            else
            {
                throw CommonException.NullInputException();
            }
        }

    }
}
