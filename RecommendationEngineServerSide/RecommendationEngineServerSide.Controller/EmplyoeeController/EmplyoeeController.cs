﻿using AutoMapper;
using RecommendationEngineServerSide.Common.DTO;
using RecommendationEngineServerSide.Common.Exceptions;
using RecommendationEngineServerSide.Service.EmplyoeeService;
using System;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.Controller.EmployeeControllers
{
    public class EmployeeController
    {
        private readonly IEmplyoeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmplyoeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        public async Task<NotificationDTO> HandleGetNotification(DailyMenuDTO userDetails)
        {
            try
            {
                var notifications = await _employeeService.GetNotification(userDetails);

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
        public async Task<NotificationDTO> HandleGetMonthlyNotification(string userName)
        {
            try
            {
                var notifications = await _employeeService.GetMonthlyNotification(userName);

                return notifications;
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
        public async Task<SocketResponseDTO> HandleMenuUpgradeFeedback(UserMenuUpgradeDTO userMenuUpgrade)
        {
            try
            {
                await _employeeService.AddMenuImprovementFeedback(userMenuUpgrade);
                SocketResponseDTO updateResponse = new SocketResponseDTO()
                {
                    Message = "Your feedback to upgrade the menu has been sent to chef. Thankyou",
                    Status = "Success"
                };
                return updateResponse;
            }
            catch(Exception ex)
            {
                return new SocketResponseDTO
                {
                    Status = "Failure",
                    Message = ex.Message
                };
            }
        }
        public async Task<EmployeeUpdateDTO> HandleGetUserProfile(string userName)
        {
            try
            {
                var notifications = await _employeeService.GetUserPreference(userName);

                return notifications;
            }
            catch (Exception ex)
            {
                return new EmployeeUpdateDTO
                {
                    Status = "Failure",
                    Message = ex.Message
                };
            }
        }
        public async Task<SocketResponseDTO> HandleUpdateUserProfile(UserProfileDetailDTO userProfileDetail )
        {
            try
            {
                await _employeeService.UpdateUserProfile(userProfileDetail);
                SocketResponseDTO response = new SocketResponseDTO()
                {
                    Status = "Success",
                    Message = "Your profile is updated carefully."
                };
                return response;
            }
            catch (Exception ex)
            {
                return new EmployeeUpdateDTO
                {
                    Status = "Failure",
                    Message = ex.Message
                };
            }
        }
        public async Task<DailyMenuDTO> HandleGetDailyMenu(string userName, DateTime currentDate)
        {
            try
            {
                var dailyMenuDTO = new DailyMenuDTO
                {
                    UserName = userName,
                    CurrentDate = currentDate
                };

                var result = await _employeeService.GetDailyMenuList(dailyMenuDTO);
                if(result.Status==null)
                {
                    return new DailyMenuDTO
                    {
                        Status = "Success",
                        Message = "Daily menu retrieved successfully.",
                        MenuList = result.MenuList
                    };
                }
                return result;
            }
            catch (LoginException ex)
            {
                return new DailyMenuDTO
                {
                    Status = "Failure",
                    Message = ex.Message
                };
            }
            catch (DailyMenuException ex)
            {
                return new DailyMenuDTO
                {
                    Status = "Failure",
                    Message = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new DailyMenuDTO
                {
                    Status = "Failure",
                    Message =  ex.Message
                };
            }
        }
        public async Task<OrderDetailDTO> HandlePlaceOrder(OrderDetailDTO orderDetailDTO)
        {
            try
            {
                var result = await _employeeService.PlaceOrder(orderDetailDTO);
                if( result.Status==null)
                {
                    return new OrderDetailDTO
                    {
                        Status = "Success",
                        Message = "Order placed successfully.",
                        OrderDate = result.OrderDate,
                        Items = result.Items
                    };
                }
                return result;
            }
            catch (LoginException ex)
            {
                return new OrderDetailDTO
                {
                    Status = "Failure",
                    Message = ex.Message
                };
            }
            catch (DailyMenuException ex)
            {
                return new OrderDetailDTO
                {
                    Status = "Failure",
                    Message = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new OrderDetailDTO
                {
                    Status = "Failure",
                    Message = ex.Message
                };
            }
        }

        public async Task<DailyOrderDetailsDTO> HandleGetOrderList(DailyMenuDTO dailyMenu)
        {
            try
            {
                var orderDetail = await _employeeService.GetOrderDetails(dailyMenu.CurrentDate,dailyMenu.UserName);

                return new DailyOrderDetailsDTO
                {
                    Status = "Success",
                    Message = "Order retrieved successfully.",
                    OrderItem = orderDetail
                };
            }
            catch (Exception ex)
            {
                return new DailyOrderDetailsDTO
                {
                    Status = "Failure",
                    Message = ex.Message
                };
            }
        }
        public async Task<SocketResponseDTO> HandleGiveFeedback(FeedbackDTO feedbackDTO)
        {
            try
            {
                await _employeeService.GiveFeedback(feedbackDTO);

                return new SocketResponseDTO
                {
                    Status = "Success",
                    Message = "Feedback submitted successfully."
                };
            }
            catch (LoginException ex)
            {
                return new SocketResponseDTO
                {
                    Status = "Failure",
                    Message = ex.Message
                };
            }
            catch (DailyMenuException ex)
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
                    Message =  ex.Message
                };
            }
        }
    }
}