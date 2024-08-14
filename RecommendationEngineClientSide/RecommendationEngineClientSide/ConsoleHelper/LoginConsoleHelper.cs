using RecommendationEngineClientSide.DTO;
using RecommendationEngineClientSide.Services.LoginServices;
using System;
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

        public async Task<LoginRequestDto> PromptLoginAsync()
        {
            Console.WriteLine("Enter username:");
            string username = Console.ReadLine();

            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();

            var loginRequest = new LoginRequestDto
            {
                UserName = username.ToLower(),
                Password = password
            };

            var role = await _loginService.HandleLoginAsync(loginRequest);
            return role;
        }
    }
}
