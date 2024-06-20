using AutoMapper;
using RecommendationEngineServerSide.Common.DTO;
using RecommendationEngineServerSide.Common.Exceptions;
using RecommendationEngineServerSide.DAL.Model;
using RecommendationEngineServerSide.DAL.UnitfWork;
using RecommendationEngineServerSide.Service.NotificationService;
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
        private readonly INotificationService _notificationService;

        public EmplyoeeService(IUnitOfWork unitOfWork, IMapper mapper, INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _notificationService = notificationService;
        }


        public async Task<List<string>> GetNotification(string userName)
        {
            var userDetails = (await _unitOfWork.User.GetAll()).FirstOrDefault(a => a.UserName.ToLower() == userName);
            List<string> notification = new List<string>();
            notification = await _notificationService.GetNotification(userDetails.UserId);
            return notification;
        }
        public async Task<DailyMenuDTO> GetDailyMenuList(DailyMenuDTO dailyMenu)
        {
            var isUserPresent = (await _unitOfWork.User.GetAll()).FirstOrDefault(a => a.UserName.ToLower() == dailyMenu.UserName);
            if (isUserPresent != null)
            {
                var isOrderPlaced = (await _unitOfWork.Order.GetAll()).Where(a => a.OrderDate == dailyMenu.CurrentDate && a.User.UserName.ToLower() == dailyMenu.UserName).ToList();
                if (isOrderPlaced.Count==0)
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
                                MenuType=menuItem.Menu.MenuType.MenuTypeName
                            });
                        }
                        await CalculateMenuRating(menuList);
                        return menuList;
                    }
                    else
                    {
                         throw DailyMenuException.MenuNotRolledException();
                    }
                   
                }
                else
                {
                    DailyMenuDTO dailyMenuDTO = new DailyMenuDTO()
                    {
                        Status="Void",
                        Message="You have already placed the order for today.",
                        MenuList = new List<DailyMenuList>()
                    };
                    foreach(var order in isOrderPlaced)
                    {
                        var userOrder = (await _unitOfWork.UserOrder.GetAll()).Where(a => a.OrderId == order.OrderId).ToList();
                        foreach (var item in userOrder)
                        {
                            DailyMenuList dailyMenuList = new DailyMenuList()
                            {
                                MenuName = item.DailyMenu.Menu.MenuName,
                                Price = item.DailyMenu.Menu.Price,
                            };
                            dailyMenuDTO.MenuList.Add(dailyMenuList);
                        }
                        
                    }
                    return dailyMenuDTO;

                    //throw DailyMenuException.OrderPlacedException();
                }
            }
            else
            {
                throw  LoginException.NoUserPresent();
            }
            
        }
        
        public async Task<OrderDetailDTO> PlaceOrder(OrderDetailDTO orderDetailDTO)
        {
            var isUserPresent = (await _unitOfWork.User.GetAll()).FirstOrDefault(a => a.UserName.ToLower() == orderDetailDTO.UserName.ToLower());
            if (isUserPresent == null)
            {
                throw LoginException.NoUserPresent();
            }
            var isOrderPlaced = (await _unitOfWork.Order.GetAll())
                .Where(a => a.UserId == isUserPresent.UserId && a.OrderDate.Date == orderDetailDTO.OrderDate.Date).ToList();

            if (isOrderPlaced.Count>0)
            {
                //throw DailyMenuException.OrderPlacedException();
                OrderDetailDTO dailyMenuDTO = new OrderDetailDTO()
                {
                    Status = "Void",
                    Message = "You have already placed the order for today.",
                    Items = new List<OrderItemDTO>()
                };
                foreach (var order in isOrderPlaced)
                {
                    var userOrder = (await _unitOfWork.UserOrder.GetAll()).Where(a => a.OrderId == order.OrderId).ToList();
                    foreach (var item in userOrder)
                    {
                        OrderItemDTO dailyMenuList = new OrderItemDTO()
                        {
                            MenuName = item.DailyMenu.Menu.MenuName,
                            OrderMenutype=item.Order.MenuType.MenuTypeName
                        };
                        dailyMenuDTO.Items.Add(dailyMenuList);
                    }

                }
                return dailyMenuDTO;
            }

            var createdOrders = new List<Order>();

            var menuTypes = orderDetailDTO.Items.GroupBy(i => i.OrderMenutype.ToLower());

            foreach (var menuTypeGroup in menuTypes)
            {
                var menuTypeName = menuTypeGroup.Key;
                var isMenuTypePresent = (await _unitOfWork.MenuType.GetAll())
                    .FirstOrDefault(a => a.MenuTypeName?.ToLower() == menuTypeName);

                if (isMenuTypePresent == null)
                {
                    throw AdminException.HandleMenuTypeNotFound();
                }

                var order = new Order
                {
                    OrderDate = orderDetailDTO.OrderDate,
                    UserId = isUserPresent.UserId,
                    MenuTypeId = isMenuTypePresent.MenuTypeId,
                    IsDeleted = false
                };

                await _unitOfWork.Order.Add(order);
                await _unitOfWork.Save();

                createdOrders.Add(order);
                foreach (var orderItem in menuTypeGroup)
                {
                    var dailyMenu = (await _unitOfWork.DailyMenu.GetAll())
                        .FirstOrDefault(a => a.Menu.MenuName.ToLower() == orderItem.MenuName.ToLower());

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
            }

            return new OrderDetailDTO
            {
                UserName = isUserPresent.UserName,
                OrderDate = orderDetailDTO.OrderDate,
                Items = orderDetailDTO.Items
            };
        }


        public async Task GiveFeedback(FeedbackDTO feedbackDTO)
        {
            var isUserPresent=(await _unitOfWork.User.GetAll()).FirstOrDefault(a=>a.UserName.ToLower() == feedbackDTO.UserName);
            if (isUserPresent != null)
            {
                var orderDetails = (await _unitOfWork.UserOrder.GetAll()).FirstOrDefault(a => a.DailyMenu.Menu.MenuName.ToLower() == feedbackDTO.MenuName && a.Order.OrderDate==feedbackDTO.FeedbackDate && a.Order.UserId==isUserPresent.UserId);
                if (orderDetails != null)
                {
                    var isMenuItemPresent=(await _unitOfWork.DailyMenu.GetAll()).FirstOrDefault(a=>a.Menu.MenuName.ToLower() == feedbackDTO.MenuName);
                    if (isMenuItemPresent != null)
                    {
                        var isFeedbackGiven = (await _unitOfWork.Feedback.GetAll()).Where(a => a.FeedbackDate == feedbackDTO.FeedbackDate && a.MenuId == isMenuItemPresent.MenuId);
                        if(!isFeedbackGiven.Any())
                        {
                            Feedback feedback = new Feedback()
                            {
                                MenuId = isMenuItemPresent.MenuId,
                                UserId = isUserPresent.UserId,
                                Rating = feedbackDTO.Rating,
                                Comment = feedbackDTO.Comment,
                                FeedbackDate = feedbackDTO.FeedbackDate,
                                ISDeleted = false,
                            };
                            await _unitOfWork.Feedback.Add(feedback);
                            await _unitOfWork.Save();
                        }
                        else
                        {
                            throw EmployeeException.HandleFeedbackGiven();
                        }
                    }
                    else
                    {
                        throw AdminException.HandleMenuItemNotFound();
                    }
                }
                else
                {
                    throw EmployeeException.HandleOrderNotPlaced();
                }
            }
            else
            {
                throw LoginException.NoUserPresent();
            }
        }

        private async Task CalculateMenuRating(DailyMenuDTO dailyMenu)
        {
            var feedbackList = await _unitOfWork.Feedback.GetAll();

            foreach (var menuItem in dailyMenu.MenuList)
            {
                var ratings = feedbackList.Where(f => f.Menu.MenuName == menuItem.MenuName)
                                          .Select(f => f.Rating);

                if (ratings.Any())
                {
                    menuItem.Rating = (ratings.Average());
                }
                else
                {
                    menuItem.Rating = 0; 
                }
            }
        }

    }
}
