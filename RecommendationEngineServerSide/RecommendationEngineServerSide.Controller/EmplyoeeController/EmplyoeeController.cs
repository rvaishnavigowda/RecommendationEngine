using AutoMapper;
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

                return new DailyMenuDTO
                {
                    Status = "Success",
                    Message = "Daily menu retrieved successfully.",
                    MenuList = result.MenuList
                };
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
                    Message = "An unexpected error occurred: " + ex.Message
                };
            }
        }

        public async Task<OrderDetailDTO> HandlePlaceOrder(OrderDetailDTO orderDetailDTO)
        {
            try
            {
                var result = await _employeeService.PlaceOrder(orderDetailDTO);

                return new OrderDetailDTO
                {
                    Status = "Success",
                    Message = "Order placed successfully.",
                    OrderDate = result.OrderDate,
                    Items = result.Items
                };
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
                    Message = "An unexpected error occurred: " + ex.Message
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
                    Message = "An unexpected error occurred: " + ex.Message
                };
            }
        }
    }
}