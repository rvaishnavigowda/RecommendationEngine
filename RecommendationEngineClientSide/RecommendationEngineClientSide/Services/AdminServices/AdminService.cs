using Newtonsoft.Json;
using RecommendationEngineClientSide.ChefDTO;
using RecommendationEngineClientSide.DTO;
using RecommendationEngineClientSide.Services.RequestServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineClientSide.Services.AdminServices
{
    public class AdminService : IAdminService
    {
        private readonly IRequestService _requestService;

        public AdminService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<SocketResponseDTO> AddMenuAsync(AddMenuRequestDto addMenuRequestDto)
        {
            var addMenuRequest = new
            {
                Controller = "AdminController",
                Action = "HandleAddMenu",
                Data = addMenuRequestDto
            };

            string requestJson = JsonConvert.SerializeObject(addMenuRequest);
            var response = await _requestService.SendRequestAsync(requestJson);
            var Jsonresponse=JsonConvert.DeserializeObject<SocketResponseDTO>(response);
            return Jsonresponse;
        }

        public async Task<FetchMenuDTO> FetchMenuDetailsAsync(FetchMenuRequestDTO fetchMenuRequestDto)
        {
            var fetchMenuRequest = new
            {
                Controller = "AdminController",
                Action = "FetchMenuDetails",
                Data = fetchMenuRequestDto
            };

            string requestJson = JsonConvert.SerializeObject(fetchMenuRequest);
            var response = await _requestService.SendRequestAsync(requestJson);
            var fetchResponse = JsonConvert.DeserializeObject<FetchMenuDTO>(response);
            return fetchResponse;
        }

        public async Task<MenuListDto> GetAllMenuAsync()
        {
            var fetchMenuRequest = new
            {
                Controller = "AdminController",
                Action = "GetAllMenu",
            };

            string requestJson = JsonConvert.SerializeObject(fetchMenuRequest);
            var response = await _requestService.SendRequestAsync(requestJson);
            var fetchResponse = JsonConvert.DeserializeObject<MenuListDto>(response);
            return fetchResponse;
        }
        public async Task<SocketResponseDTO> UpdateMenuAsync(UpdateMenuRequestDto updateMenuRequestDto)
        {
            var updateMenuRequest = new
            {
                Controller = "AdminController",
                Action = "HandleUpdateMenu",
                Data = updateMenuRequestDto
            };

            string requestJson = JsonConvert.SerializeObject(updateMenuRequest);
            var response = await _requestService.SendRequestAsync(requestJson);
            var Jsonresponse = JsonConvert.DeserializeObject<SocketResponseDTO>(response);
            return Jsonresponse;
        }

        public async Task<SocketResponseDTO> DeleteMenuAsync(DeleteMenuRequestDto deleteMenuRequestDto)
        {
            var deleteMenuRequest = new
            {
                Controller = "AdminController",
                Action = "DeleteMenu",
                Data = deleteMenuRequestDto
            };

            string requestJson = JsonConvert.SerializeObject(deleteMenuRequest);
            var response = await _requestService.SendRequestAsync(requestJson);
            var Jsonresponse = JsonConvert.DeserializeObject<SocketResponseDTO>(response);
            return Jsonresponse;
        }
    }
}
