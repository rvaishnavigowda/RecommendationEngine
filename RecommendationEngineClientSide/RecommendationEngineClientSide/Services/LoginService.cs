using Newtonsoft.Json;
using RecommendationEngineClientSide.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineClientSide.Services
{
    public class LoginService : ILoginService
    {
        private readonly IRequestService _requestService;

        public LoginService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<string> HandleLoginAsync(LoginRequestDto loginRequestDto)
        {
            var loginRequest = new
            {
                Controller = "LoginController",
                Action = "HandleLoginRequest",
                Data = loginRequestDto
            };

            string requestJson = JsonConvert.SerializeObject(loginRequest);
            return await _requestService.SendRequestAsync(requestJson);
        }
    }
}
