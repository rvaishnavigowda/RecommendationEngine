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

        public async Task<SocketResponseDTO> GetMenuListAsync(DateTime date)
        {
            var getMenuListRequest = new
            {
                Controller = "ChefController",
                Action = "HandleGetMenuList",
                Data = date
            };

            string requestJson = JsonConvert.SerializeObject(getMenuListRequest);
            var response = await _requestService.SendRequestAsync(requestJson);
            return JsonConvert.DeserializeObject<SocketResponseDTO>(response);
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
