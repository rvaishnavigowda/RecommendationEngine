using AutoMapper;
using RecommendationEngineServerSide.Common.ApplicationConst;
using RecommendationEngineServerSide.Common.DTO;
using RecommendationEngineServerSide.Common.Enum;
using RecommendationEngineServerSide.Common.Exceptions;
using RecommendationEngineServerSide.DAL.Model;
using RecommendationEngineServerSide.DAL.UnitfWork;
using RecommendationEngineServerSide.Service.NotificationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.Service.AdminService
{
    public class AdminService:IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        public AdminService(IUnitOfWork unitOfWork, IMapper mapper, INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _notificationService = notificationService;
        }

        public async Task AddMenu(MenuDTO menuDTO)
        {

            if(menuDTO!=null)
            {
                
                var isMenuTypePresent = await CheckMenuType(menuDTO.MenuType);
                if (isMenuTypePresent != null)
                {
                    var isMenuItemPresent = await CheckMenuItem(menuDTO.MenuName);
                    if (isMenuItemPresent == null )
                    {

                        Menu menu = new Menu
                        {
                            MenuName = menuDTO.MenuName,
                            MenuTypeId = isMenuTypePresent.MenuTypeId,
                            Price = menuDTO.MenuPrice,
                            FoodTypeId=menuDTO.FoodType,
                            CuisineTypeId=menuDTO.CuisineType,
                            SpiceLevel=menuDTO.SpiceLevel,
                            IsSweet=menuDTO.IsSweet,
                            MenuStatus=(int)MenuStatus.Active
                        };
                        await _unitOfWork.Menu.Add(menu);
                        await _unitOfWork.Save();
                        await UpdateNotification(menu.MenuName, menuDTO.MenuType,menuDTO.dateCreated);

                    }
                    else if(isMenuItemPresent!=null && isMenuItemPresent.MenuStatus==2)
                    {
                        isMenuItemPresent.MenuStatus = 1;
                        await _unitOfWork.Menu.Update(isMenuItemPresent);
                        await _unitOfWork.Save();
                    }
                    else if(isMenuItemPresent!=null && isMenuItemPresent.MenuStatus == 2 && isMenuItemPresent.MenuType.MenuTypeName!=menuDTO.MenuType)
                    {
                        Menu menu = new Menu
                        {
                            MenuName = menuDTO.MenuName,
                            MenuTypeId = isMenuTypePresent.MenuTypeId,
                            Price = menuDTO.MenuPrice
                        };
                        await _unitOfWork.Menu.Add(menu);
                        await _unitOfWork.Save();
                        await UpdateNotification(menu.MenuName, menuDTO.MenuType, menuDTO.dateCreated);
                    }
                    else
                    {
                        AdminException.HandleMenuItemAlreadyExists();
                    }
                }
                else
                {
                    AdminException.HandleMenuTypeNotFound();
                }
            }
            else
            {
                CommonException.NullInputException();
            }
        }
        public async Task<FetchMenuDTO> GetMenuDetailsByName(string menuName)
        {
            var isMenuNamePresent=(await _unitOfWork.Menu.GetAll()).Where(a=>a.MenuName.ToLower()==menuName && a.MenuStatus==1).ToList() ;
            if(isMenuNamePresent.Count!=0)
            {
                FetchMenuDTO menuDTO = new FetchMenuDTO()
                {
                    MenuList = new List<FetchMenuResponseDTO>()
                };
                foreach(var menuItem in isMenuNamePresent)
                {
                    var menuitem = new FetchMenuResponseDTO()
                    {
                        MenuName = menuItem.MenuName,
                        MenuType = menuItem.MenuType.MenuTypeName,
                        MenuPrice = menuItem.Price,
                    };
                    menuDTO.MenuList.Add(menuitem);
                }
                return menuDTO;
            }
            else
            {
                throw AdminException.HandleMenuItemNotFound();
            }
        }

        public async Task UpdateMenu(UpdateMenuDTO updateMenuDTO)
        {
            if (updateMenuDTO!=null)
            {
                var isMenuItemPresent = await CheckMenuItem(updateMenuDTO.MenuName);
                if (isMenuItemPresent != null )
                {
                    isMenuItemPresent.MenuName= updateMenuDTO.MenuName;
                    isMenuItemPresent.Price= updateMenuDTO.MenuPrice;
                    await _unitOfWork.Menu.Update(isMenuItemPresent);
                    await _unitOfWork.Save();
                }
                else if(isMenuItemPresent.MenuStatus==2)
                {
                    throw AdminException.HandleMenuItemDeleted();
                }
                else
                {
                   throw  AdminException.HandleMenuItemNotFound();
                }
            }
            else
            {
                throw CommonException.NullInputException();
            }
           
        }

        public async Task<MenuListDTO> GetAllMenu()
        {
            var menuList=(await _unitOfWork.Menu.GetAll()).Where(a=>a.MenuStatus==1).ToList();
            if(menuList!=null)
            {
                var menu = new MenuListDTO()
                {
                    Menu = new List<ListMenuDTO>()
                };
                foreach(var item in menuList)
                {
                    var menuItem = new ListMenuDTO()
                    {
                        MenuItemName = item.MenuName,
                        Price = item.Price,
                        MenuItemType = item.MenuType.MenuTypeName
                    };
                    menu.Menu.Add(menuItem);
                }
                return menu;
            }
            else
            {
                throw AdminException.HandleNoMenu();
            }
        }
        public async Task DeleteMenu(DeleteMenuDTO deleteMenuDTO)
        {
            if (deleteMenuDTO!=null)
            {
                var isMenuItemPresent = await CheckMenuItem(deleteMenuDTO.MenuName);
                if (isMenuItemPresent != null)
                {
                    if(isMenuItemPresent.MenuStatus==1)
                    {
                        isMenuItemPresent.MenuStatus = 2;
                        await _unitOfWork.Menu.Update(isMenuItemPresent);
                        await _unitOfWork.Save();
                    }
                    else
                    {
                        throw AdminException.HandleMenuItemAlreadyDeleted();
                    }
                }
                else
                {
                    throw AdminException.HandleMenuItemNotFound();
                }
            }
           else
            {
                throw CommonException.NullInputException ();
            }
        }

        private async Task<MenuType?> CheckMenuType(string menuTypeName)
        {
            var isMenuTypePresent = (await _unitOfWork.MenuType.GetAll()).FirstOrDefault(a => a.MenuTypeName.ToLower() == menuTypeName.ToLower());
            if (isMenuTypePresent!=null)
            {
                return isMenuTypePresent;
            }
            else
            {
                return null;
            }
        }
        
        private async Task<Menu?> CheckMenuItem(string menuName)
        {
            var isMenuItemPresent = (await _unitOfWork.Menu.GetAll()).FirstOrDefault(a => a.MenuName.ToLower() == menuName.ToLower());
            if (isMenuItemPresent!=null)
            {
                return isMenuItemPresent;
            }
            else
            {
                return null;
            }  
        }

        private async Task UpdateNotification(string menuName, string menuType, DateTime date)
        {
            int notificationTypeId = (int)NotificationTypeEnum.NewMenuItemAdded;
            string notification = ApplicationConstant.NewMenuItemNotification + " : " + menuName;
            await _notificationService.AddNotification(notification, notificationTypeId, date);
        }
    }
}


