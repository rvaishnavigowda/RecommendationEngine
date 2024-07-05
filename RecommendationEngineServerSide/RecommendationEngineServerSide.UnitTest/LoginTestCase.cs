using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using RecommendationEngineServerSide.Controller.LoginControllers;
using RecommendationEngineServerSide.Service.RegisterService;
using RecommendationEngineServerSide.Common.DTO;
using RecommendationEngineServerSide.Common.Exceptions;

namespace RecommendationEngineServerSide.UnitTest
{
    [TestClass]
    public class LoginControllerTests
    {
        private Mock<IRegisterService> _mockRegisterService;
        private Mock<IMapper> _mockMapper;
        private Mock<ILogger<LoginController>> _mockLogger;
        private LoginController _loginController;

        [TestInitialize]
        public void Setup()
        {
            _mockRegisterService = new Mock<IRegisterService>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<LoginController>>();
            _loginController = new LoginController(_mockRegisterService.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [TestMethod]
        public async Task HandleLoginRequest_ValidUser_ReturnsSuccessResponse()
        {
            var userDTO = new UserDTO { UserName = "testuser", Password = "password" };
            var responseUserDTO = new ResponseUserDTO { UserName = "testuser", Status = "Success", Message = "Login successful" };

            _mockRegisterService.Setup(s => s.LoginValidation(It.IsAny<UserDTO>()))
                .ReturnsAsync(userDTO);

            _mockMapper.Setup(m => m.Map<ResponseUserDTO>(It.IsAny<UserDTO>()))
                .Returns(responseUserDTO);

            var result = await _loginController.HandleLoginRequest(userDTO);

            Assert.IsNotNull(result);
            Assert.AreEqual("Success", result.Status);
            Assert.AreEqual("Login successful", result.Message);
        }

        [TestMethod]
        public async Task HandleLoginRequest_InvalidUser_ReturnsFailureResponse()
        {
            var userDTO = new UserDTO { UserName = "invaliduser", Password = "wrongpassword" };

            _mockRegisterService.Setup(s => s.LoginValidation(It.IsAny<UserDTO>()))
                .ThrowsAsync(new LoginException("Invalid credentials"));

            var result = await _loginController.HandleLoginRequest(userDTO);
            Assert.IsNotNull(result);
            Assert.AreEqual("Failure", result.Status);
            Assert.AreEqual("Invalid credentials", result.Message);
        }

        [TestMethod]
        public async Task HandleLoginRequest_ThrowsCommonException_ReturnsFailureResponse()
        {
            var userDTO = new UserDTO { UserName = "testuser", Password = "password" };

            _mockRegisterService.Setup(s => s.LoginValidation(It.IsAny<UserDTO>()))
                .ThrowsAsync(new CommonException("Common error occurred"));
            var result = await _loginController.HandleLoginRequest(userDTO);
            Assert.IsNotNull(result);
            Assert.AreEqual("Failure", result.Status);
            Assert.AreEqual("Common error occurred", result.Message);
        }

        [TestMethod]
        public async Task HandleLoginRequest_ThrowsException_ReturnsFailureResponse()
        {
            var userDTO = new UserDTO { UserName = "testuser", Password = "password" };

            _mockRegisterService.Setup(s => s.LoginValidation(It.IsAny<UserDTO>()))
                .ThrowsAsync(new Exception("Unexpected error"));
            var result = await _loginController.HandleLoginRequest(userDTO);
            Assert.IsNotNull(result);
            Assert.AreEqual("Failure", result.Status);
            Assert.AreEqual("An unexpected error occurred: Unexpected error", result.Message);
        }
    }

}
