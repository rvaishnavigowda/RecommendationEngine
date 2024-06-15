using AutoMapper;
using RecommendationEngineServerSide.Common.DTO;
using RecommendationEngineServerSide.Common.Exceptions;
using RecommendationEngineServerSide.DAL.Model;
using RecommendationEngineServerSide.DAL.UnitfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.Service.EmplyoeeService
{
    public class EmplyoeeService : IEmplyoeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmplyoeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DailyMenuDTO> GetDailyMenuList(DailyMenuDTO dailyMenu)
        {
            var isUserPresent = (await _unitOfWork.User.GetAll()).FirstOrDefault(a => a.UserName == dailyMenu.UserName);
            if (isUserPresent != null)
            {
                var isOrderPlaced = (await _unitOfWork.Order.GetAll()).FirstOrDefault(a => a.OrderDate == dailyMenu.CurrentDate && a.User.UserName == dailyMenu.UserName);
                if (isOrderPlaced == null)
                {
                    var menuList = new DailyMenuDTO
                    {
                        UserName = dailyMenu.UserName,
                        CurrentDate = dailyMenu.CurrentDate,
                        MenuList = new List<DailyMenuList>()
                    };

                    var dailyMenuList = (await _unitOfWork.DailyMenu.GetAll()).Where(a => a.DailyMenuDate == dailyMenu.CurrentDate).ToList();
                    if (dailyMenuList.Count > 0)
                    {
                        foreach (var menuItem in dailyMenuList)
                        {
                            menuList.MenuList.Add(new DailyMenuList
                            {
                                MenuName = menuItem.Menu.MenuName,
                                Price = menuItem.Menu.Price,
                            });
                        }
                        CalculateMenuRating(menuList);
                        return menuList;
                    }
                    else
                    {
                         throw DailyMenuException.MenuNotRolledException();
                    }
                   
                }
                else
                {
                    throw DailyMenuException.OrderPlacedException();
                }
            }
            else
            {
                throw  LoginException.NoUserPresent();
            }
            
        }
        public async Task<OrderDetailDTO> PlaceOrder(OrderDetailDTO orderDetailDTO)
        {
            var isUserPresent = (await _unitOfWork.User.GetAll()).FirstOrDefault(a => a.UserName == orderDetailDTO.UserName);
            if (isUserPresent != null)
            {
                var isOrderPlaced = (await _unitOfWork.Order.GetAll()).FirstOrDefault(a => a.UserId == isUserPresent.UserId && a.OrderDate.Date == orderDetailDTO.OrderDate.Date);
                if (isOrderPlaced == null)
                {
                    var isMenuTypePresent = (await _unitOfWork.MenuType.GetAll()).FirstOrDefault(a => a.MenuTypeName == orderDetailDTO.OrderMenutype);
                    if (isMenuTypePresent != null)
                    {
                        var order = new Order
                        {
                            OrderDate = orderDetailDTO.OrderDate,
                            UserId = isUserPresent.UserId,
                            MenuTypeId= isMenuTypePresent.MenuTypeId,
                            IsDeleted = 0
                        };

                        await _unitOfWork.Order.Add(order);
                        await _unitOfWork.Save();

                        foreach (var orderItem in orderDetailDTO.Items)
                        {
                            var dailyMenu = (await _unitOfWork.DailyMenu.GetAll()).FirstOrDefault(a => a.DailyMenuName == orderItem.MenuName);
                            if (dailyMenu != null)
                            {
                                var userOrder = new UserOrder
                                {
                                    OrderId = order.OrderId,
                                    DailyMenuId = dailyMenu.DailyMenuId
                                };
                                await _unitOfWork.UserOrder.Add(userOrder);
                            }
                        }
                        await _unitOfWork.Save();
                        return new OrderDetailDTO
                        {
                            UserName = isUserPresent.UserName,
                            OrderDate = order.OrderDate,
                            Items = orderDetailDTO.Items
                        };
                    }
                    else
                    {
                        throw AdminException.HandleMenuTypeNotFound();
                    }
                }
                else
                {
                    throw DailyMenuException.OrderPlacedException();
                }
            }
            else
            {
                throw LoginException.NoUserPresent();
            }
        }

        public async Task GiveFeedback(FeedbackDTO feedbackDTO)
        {
            var isUserPresent=(await _unitOfWork.User.GetAll()).FirstOrDefault(a=>a.UserName == feedbackDTO.UserName);
            if (isUserPresent != null)
            {
                var isOrderPlaced = (await _unitOfWork.UserOrder.GetAll()).FirstOrDefault(a => a.DailyMenu.DailyMenuName == feedbackDTO.MenuName);
                if (isOrderPlaced != null)
                {
                    var isMenuItemPresent=(await _unitOfWork.DailyMenu.GetAll()).FirstOrDefault(a=>a.Menu.MenuName == feedbackDTO.MenuName);
                    if (isMenuItemPresent != null)
                    {
                        Feedback feedback = new Feedback()
                        {
                            MenuId = isMenuItemPresent.MenuId,
                            UserId = isUserPresent.UserId,
                            Rating = feedbackDTO.Rating,
                            Comment = feedbackDTO.Comment,
                            FeedbackDate = feedbackDTO.FeedbackDate,
                            ISDeleted = 0,
                        };
                        await _unitOfWork.Feedback.Add(feedback);
                        await _unitOfWork.Save();
                    }
                }
            }
        }
        private async Task CalculateMenuRating(DailyMenuDTO dailyMenu)
        {
            var menuItemRating = (await _unitOfWork.Feedback.GetAll());
            foreach(var menuItem in dailyMenu.MenuList)
            {
                menuItem.Rating = (await _unitOfWork.Feedback.GetAll()).FirstOrDefault(a => a.Menu.MenuName == menuItem.MenuName).Rating;

            }
            
        }
    }
}
