using AutoMapper;
using RecommendationEngineServerSide.Common.DTO;
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

namespace RecommendationEngineServerSide.Service.EmplyoeeService
{
    public class EmplyoeeService : IEmplyoeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;   
        private readonly INotificationService _notificationService;
        private readonly IRecommendationService _recommendationService;

        public EmplyoeeService(IUnitOfWork unitOfWork, IMapper mapper, INotificationService notificationService, IRecommendationService recommendationService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _notificationService = notificationService;
            _recommendationService= recommendationService;
        }

        public async Task<List<string>> GetNotification(DailyMenuDTO userDetails)
        {
            var userDetail = (await _unitOfWork.User.GetAll()).FirstOrDefault(a => a.UserName.ToLower() == userDetails.UserName);
            List<string> notification = new List<string>();
            notification = await _notificationService.GetNotification(userDetail.UserId, userDetails.CurrentDate);
            return notification;
        }
        
        public async Task<NotificationDTO> GetMonthlyNotification(string userName)
        {
            var feedback = await _notificationService.GetMenuUpgradeFeedback(userName);
            if (feedback != null)
            {
                List<string> feedbackList = new List<string>();
                
                var feedbackNotification = (await _unitOfWork.MenuFeedbackQuestion.GetAll()).ToList();
                foreach(var item in feedbackNotification)
                {
                    feedbackList.Add(item.MenuFeedbackQuestionTitle);
                }
                return new NotificationDTO
                {
                    Status = "Success",
                    Message = feedback,
                    Notifications = feedbackList
                };
            }
            else
            {
                throw EmployeeException.HandleNoFeedback();
            }
        }

        public async Task AddMenuImprovementFeedback(UserMenuUpgradeDTO feedback)
        {
            var isUserPresent = (await _unitOfWork.User.GetAll()).FirstOrDefault(a => a.UserName.ToLower() == feedback.UserName);
            if(isUserPresent != null)
            {
                var menuDetails = (await _unitOfWork.Menu.GetAll()).FirstOrDefault(a => a.MenuName.ToLower() == feedback.MenuName);
                if(menuDetails != null)
                {
                    int i = 1;
                    foreach(var item in feedback.menuFeedback)
                    {
                        MenuFeedback userFeedback = new MenuFeedback()
                        {
                            UserId = isUserPresent.UserId,
                            MenuId = menuDetails.MenuId,
                            MenuFeedbackQuestionId = i,
                            MenuFeedbackAnswer = item
                        };
                        await _unitOfWork.MenuFeedback.Create(userFeedback);
                        await _unitOfWork.Save();
                        i++;
                    }
                }
                else
                {
                    throw MenuException.HandleNoMenuFound();
                }
            }
            else
            {
                throw LoginException.NoUserPresent();
            }

        }
        public async Task<EmployeeUpdateDTO> GetUserPreference(string userName)
        {
            var isUserPresent = (await _unitOfWork.User.GetAll()).FirstOrDefault(a => a.UserName == userName);
            if (isUserPresent != null)
            {
                EmployeeUpdateDTO employeeProfileDTO = new EmployeeUpdateDTO()
                {
                    ProfileQuestions = new List<ProfileQuestionDTO>()
                };

                var profileQuestions = (await _unitOfWork.ProfileQuestion.GetAll()).ToList();
                if (profileQuestions.Count > 0)
                {
                    foreach (var item in profileQuestions)
                    {
                        var profileQuestion = new ProfileQuestionDTO
                        {
                            Question = item.Question,
                            ProfileAnswers = new List<string>()
                        };

                        var profiles = (await _unitOfWork.ProfileAnswer.GetAll()).Where(a => a.ProfileQuestionId == item.PQId).ToList();
                        if (profiles != null)
                        {
                            foreach (var answer in profiles)
                            {
                                profileQuestion.ProfileAnswers.Add(answer.ProfileAnswerSolution);
                            }
                        }

                        employeeProfileDTO.ProfileQuestions.Add(profileQuestion);
                    }
                }

                return new EmployeeUpdateDTO
                {
                    Status = "Success",
                    ProfileQuestions = employeeProfileDTO.ProfileQuestions,
                    Message = "Please update your preferences."
                };
            }
            else
            {
                throw LoginException.NoUserPresent();
            }
        }

        public async Task UpdateUserProfile(UserProfileDetailDTO profile)
        {
            if (profile != null)
            {
                var isUserPresent = (await _unitOfWork.User.GetAll()).FirstOrDefault(a => a.UserName == profile.userName);
                if (isUserPresent != null)
                {
                    var userProfiles = (await _unitOfWork.UserProfile.GetAll()).Where(a => a.UserId == isUserPresent.UserId).ToList();
                    var profileQuestions = (await _unitOfWork.ProfileQuestion.GetAll()).ToList();
                    var profileAnswers = (await _unitOfWork.ProfileAnswer.GetAll()).ToList();

                    for (int i = 0; i < profile.userResponse.Count; i++)
                    {
                        if (i >= profileQuestions.Count)
                            break;

                        var profileQuestion = profileQuestions[i];
                        var answer = profile.userResponse[i];
                        var profileAnswer = profileAnswers.FirstOrDefault(a => a.ProfileQuestionId == profileQuestion.PQId && a.ProfileAnswerSolution == answer);

                        if (profileAnswer != null)
                        {
                            var existingUserProfile = userProfiles.FirstOrDefault(up => up.ProfileAnswer.ProfileQuestionId == profileAnswer.ProfileQuestionId);

                            if (existingUserProfile == null)
                            {
                                var newUserProfile = new UserProfile
                                {
                                    UserId = isUserPresent.UserId,
                                    ProfileAnswerId = profileAnswer.PAId
                                };
                                await _unitOfWork.UserProfile.Add(newUserProfile);
                            }
                            else
                            {
                                existingUserProfile.ProfileAnswerId = profileAnswer.PAId;
                                await _unitOfWork.UserProfile.Update(existingUserProfile);
                                
                            }
                        }
                    }

                    await _unitOfWork.Save();
                }
                else
                {
                    LoginException.NoUserPresent();
                }
            }
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
                    var dailyMenuList = await _recommendationService.GetPersonalizedDailyMenu(isUserPresent.UserId, dailyMenu.CurrentDate);
                    if (dailyMenuList.Count > 0)
                    {
                        foreach (var menuItem in dailyMenuList)
                        {
                            menuList.MenuList.Add(new DailyMenuList
                            {
                                MenuName = menuItem.MenuItemName,
                                Price = menuItem.Price,
                                MenuType=menuItem.MenuItemType
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

        public async Task<List<string>> GetOrderDetails(DateTime orderDate, string userName)
        {
            var userDetail=(await _unitOfWork.User.GetAll()).FirstOrDefault(a=>a.UserName == userName);
            if(userDetail != null)
            {
                var orderDetail=(await _unitOfWork.Order.GetAll()).Where(a=>a.UserId== userDetail.UserId && a.OrderDate==orderDate).ToList();
                if(orderDetail.Count>0)
                {
                    List<string> order = new List<string>();
                    foreach (var item in orderDetail)
                    {
                        var userOrder = (await _unitOfWork.UserOrder.GetAll()).Where(a => a.OrderId == item.OrderId).ToList();
                        foreach(var item2 in userOrder)
                        {
                            order.Add(item2.DailyMenu.Menu.MenuName);
                        }
                    }
                    return order;
                    
                }
                throw EmployeeException.HandleOrderNotPlaced();
            }
            else
            {
                throw LoginException.NoUserPresent();
            }
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
                        
                        var isFeedbackGiven = (await _unitOfWork.Feedback.GetAll()).Where(a => a.FeedbackDate == feedbackDTO.FeedbackDate && a.MenuId == isMenuItemPresent.MenuId && a.UserId==isUserPresent.UserId);
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