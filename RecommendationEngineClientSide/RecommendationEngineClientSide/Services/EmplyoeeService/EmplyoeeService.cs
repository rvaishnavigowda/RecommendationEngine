using Newtonsoft.Json;
using RecommendationEngineClientSide.DTO;
using RecommendationEngineClientSide.Services.RequestServices;
using System.Threading.Tasks;

namespace RecommendationEngineClientSide.Services.EmployeeServices
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRequestService _requestService;

        public EmployeeService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<NotificationDTO> FetchNotificationsAsync(string userName)
        {
            var fetchNotificationRequest = new
            {
                Controller = "EmployeeController",
                Action = "HandleGetNotification",
                Data = new { UserName = userName }
            };

            string requestJson = JsonConvert.SerializeObject(fetchNotificationRequest);
            var response = await _requestService.SendRequestAsync(requestJson);
            var notificationResponse = JsonConvert.DeserializeObject<NotificationDTO>(response);
            return notificationResponse;
        }
        public async Task<DailyMenuResponseDto> GetDailyMenuAsync(DailyMenuRequestDto dailyMenuRequestDto)
        {
            var getDailyMenuRequest = new
            {
                Controller = "EmployeeController",
                Action = "HandleGetDailyMenu",
                Data = dailyMenuRequestDto
            };

            string requestJson = JsonConvert.SerializeObject(getDailyMenuRequest);
            var response = await _requestService.SendRequestAsync(requestJson);
            var dailyMenuResponse = JsonConvert.DeserializeObject<DailyMenuResponseDto>(response);
            return dailyMenuResponse;
        }

        public async Task<OrderDetailRequestDto> PlaceOrderAsync(OrderDetailRequestDto orderDetailRequestDto)
        {
            var placeOrderRequest = new
            {
                Controller = "EmployeeController",
                Action = "HandlePlaceOrder",
                Data = orderDetailRequestDto
            };

            string requestJson = JsonConvert.SerializeObject(placeOrderRequest);
            var response = await _requestService.SendRequestAsync(requestJson);
            var socketResponse = JsonConvert.DeserializeObject<OrderDetailRequestDto>(response);
            return socketResponse;
        }

        public async Task<SocketResponseDTO> GiveFeedbackAsync(FeedbackDto feedbackDto)
        {
            var giveFeedbackRequest = new
            {
                Controller = "EmployeeController",
                Action = "HandleGiveFeedback",
                Data = feedbackDto
            };

            string requestJson = JsonConvert.SerializeObject(giveFeedbackRequest);
            var response = await _requestService.SendRequestAsync(requestJson);
            var socketResponse = JsonConvert.DeserializeObject<SocketResponseDTO>(response);
            return socketResponse;
        }
    }
}
