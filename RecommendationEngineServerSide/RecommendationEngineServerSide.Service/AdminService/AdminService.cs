using AutoMapper;
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
                    if (isMenuItemPresent == null)
                    {
                        Menu menu = new Menu
                        {
                            MenuName = menuDTO.MenuName,
                            MenuTypeId = isMenuTypePresent.MenuTypeId,
                            Price = menuDTO.MenuPrice
                        };
                        await _unitOfWork.Menu.Add(menu);
                        await _unitOfWork.Save();

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

        public async Task UpdateMenu(UpdateMenuDTO updateMenuDTO)
        {
            if (updateMenuDTO!=null)
            {
                var isMenuItemPresent = await CheckMenuItem(updateMenuDTO.MenuName);
                if (isMenuItemPresent != null)
                {
                    Menu menu = new Menu
                    {
                        MenuName = updateMenuDTO.MenuName,
                        MenuTypeId = isMenuItemPresent.MenuType.MenuTypeId,
                        Price = updateMenuDTO.MenuPrice,
                    };
                    await _unitOfWork.Menu.Update(menu);
                    await _unitOfWork.Save();
                }
                else
                {
                    AdminException.HandleMenuItemNotFound();
                }
            }
            else
            {
                CommonException.NullInputException();
            }
           
        }

        public async Task DeleteMenu(DeleteMenuDTO deleteMenuDTO)
        {
            if (deleteMenuDTO!=null)
            {
                var isMenuItemPresent = await CheckMenuItem(deleteMenuDTO.MenuName);
                if (isMenuItemPresent != null)
                {
                    isMenuItemPresent.ISDeleted = true;
                    await _unitOfWork.Menu.Update(isMenuItemPresent);
                    await _unitOfWork.Save();
                }
                else
                {
                    AdminException.HandleMenuItemNotFound();
                }
            }
           else
            {
                CommonException.NullInputException ();
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

        private async Task UpdateNotification(string menuName)
        {
            string message = "A new item " + menuName + " has been to menu.";
            int notificationTypeId = (int)NotificationTypeEnum.NewMenuItemAdded;
            await _notificationService.AddNotification(message, notificationTypeId);
        }
    }
}


