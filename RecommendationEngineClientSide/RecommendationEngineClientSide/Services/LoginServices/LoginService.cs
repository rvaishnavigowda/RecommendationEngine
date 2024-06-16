using Newtonsoft.Json;
using RecommendationEngineClientSide.DTO;
using RecommendationEngineClientSide.Services.RequestServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineClientSide.Services.LoginServices
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
            var serverResponse = await _requestService.SendRequestAsync(requestJson);
            var jsonResponse = JsonConvert.DeserializeObject<LoginRequestDto>(serverResponse);
            if (jsonResponse.Status != "failure")
            {
                return jsonResponse.UserRole;
            }
            else
                return "failure";
        }
    }
}
