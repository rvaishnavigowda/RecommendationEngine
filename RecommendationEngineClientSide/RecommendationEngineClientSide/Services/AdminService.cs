using Newtonsoft.Json;
using RecommendationEngineClientSide.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineClientSide.Services
{
    public class AdminService : IAdminService
    {
        private readonly IRequestService _requestService;

        public AdminService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<string> AddMenuAsync(AddMenuRequestDto addMenuRequestDto)
        {
            var addMenuRequest = new
            {
                Controller = "AdminController",
                Action = "HandleAddMenu",
                Data = addMenuRequestDto
            };

            string requestJson = JsonConvert.SerializeObject(addMenuRequest);
            return await _requestService.SendRequestAsync(requestJson);
        }

        public async Task<string> UpdateMenuAsync(UpdateMenuRequestDto updateMenuRequestDto)
        {
            var updateMenuRequest = new
            {
                Controller = "AdminController",
                Action = "HandleUpdateMenu",
                Data = updateMenuRequestDto
            };

            string requestJson = JsonConvert.SerializeObject(updateMenuRequest);
            return await _requestService.SendRequestAsync(requestJson);
        }

        public async Task<string> DeleteMenuAsync(DeleteMenuRequestDto deleteMenuRequestDto)
        {
            var deleteMenuRequest = new
            {
                Controller = "AdminController",
                Action = "DeleteMenu",
                Data = deleteMenuRequestDto
            };

            string requestJson = JsonConvert.SerializeObject(deleteMenuRequest);
            return await _requestService.SendRequestAsync(requestJson);
        }
    }
}
