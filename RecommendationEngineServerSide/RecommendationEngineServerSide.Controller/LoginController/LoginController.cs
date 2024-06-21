using AutoMapper;
using Castle.Core.Logging;
using RecommendationEngineServerSide.Common.DTO;
using RecommendationEngineServerSide.Common.Exceptions;
using RecommendationEngineServerSide.Service.RegisterService;
using Microsoft.Extensions.Logging;

namespace RecommendationEngineServerSide.Controller.LoginControllers
{
    public class LoginController
    {
        private readonly IRegisterService _register;
        private readonly IMapper _mapper;
        private readonly ILogger<LoginController> _logger;
        public LoginController(IRegisterService register, IMapper mapper, ILogger<LoginController> logger)
        {
            _register = register;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ResponseUserDTO> HandleLoginRequest(UserDTO userDTO)
        {
            try
            {
                var user = await _register.LoginValidation(userDTO);
                ResponseUserDTO responseUser = _mapper.Map<ResponseUserDTO>(userDTO);
                _logger.LogTrace(responseUser.Message);
                responseUser.Status = "Success"; 
                responseUser.Message = "Login successful";
                return responseUser;
            }
            catch (LoginException ex)
            {
                return new ResponseUserDTO
                {
                    Status = "Failure",
                    Message = ex.Message
                };
            }
            catch (CommonException ex)
            {
                return new ResponseUserDTO
                {
                    Status = "Failure",
                    Message = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new ResponseUserDTO
                {
                    Status = "Failure",
                    Message = "An unexpected error occurred: " + ex.Message
                };
            }
        }


        public string HandleSignupRequest(string request)
        {
            return "Handling signup request...";
        }

    }
}