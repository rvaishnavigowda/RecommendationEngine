using RecommendationEngineClientSide.DTO;
using RecommendationEngineClientSide.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RecommendationEngineClientSide.ConsoleHelper
{
    public class LoginConsoleHelper
    {
        private readonly ILoginService _loginService;

        public LoginConsoleHelper(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public async Task<string> PromptLoginAsync()
        {
            Console.WriteLine("Please enter your username:");
            string userName = Console.ReadLine();

            Console.WriteLine("Please enter your password:");
            string password = Console.ReadLine();

            var loginRequestDto = new LoginRequestDto
            {
                UserName = userName,
                Password = password
            };

            string loginResponse = await _loginService.HandleLoginAsync(loginRequestDto);
            SocketResponseDTO response = JsonSerializer.Deserialize<SocketResponseDTO>(loginResponse);
            if(response.Status=="Success")
            {
                LoginRequestDto user = JsonSerializer.Deserialize<LoginRequestDto>(loginResponse);
                return user.UserRole;
            }
            else
            {
                Console.WriteLine(response.Message.ToString());
                return response.Status;
            }
            
        }
    }
}
