using Newtonsoft.Json;
using RecommendationEngineClientSide.ChefDTO;
using RecommendationEngineClientSide.DTO;
using RecommendationEngineClientSide.Services.RequestServices;
using System;
using System.Threading.Tasks;

namespace RecommendationEngineClientSide.Services.ChefServices
{
    public class ChefService : IChefService
    {
        private readonly IRequestService _requestService;

        public ChefService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<NotificationDTO> FetchNotificationsAsync(string userName)
        {
            var fetchNotificationRequest = new
            {
                Controller = "ChefController",
                Action = "HandleGetNotification",
                Data = new { UserName = userName }
            };

            string requestJson = JsonConvert.SerializeObject(fetchNotificationRequest);
            var response = await _requestService.SendRequestAsync(requestJson);
            var notificationResponse = JsonConvert.DeserializeObject<NotificationDTO>(response);
            return notificationResponse;
        }

        public async Task<MenuListDto> GetMenuListAsync(DateTime date)
        {
            var getMenuListRequest = new
            {
                Controller = "ChefController",
                Action = "HandleGetMenuList",
                Data = date
            };

            string requestJson = JsonConvert.SerializeObject(getMenuListRequest);
            var response = await _requestService.SendRequestAsync(requestJson);
            return JsonConvert.DeserializeObject<MenuListDto>(response);
        }

        public async Task<SocketResponseDTO> AddDailyMenuAsync(NewDailyMenuDto newDailyMenuDto)
        {
            var addDailyMenuRequest = new
            {
                Controller = "ChefController",
                Action = "HandleAddDailyMenu",
                Data = newDailyMenuDto
            };

            string requestJson = JsonConvert.SerializeObject(addDailyMenuRequest);
            var response = await _requestService.SendRequestAsync(requestJson);
            return JsonConvert.DeserializeObject<SocketResponseDTO>(response);
        }
    }
}
