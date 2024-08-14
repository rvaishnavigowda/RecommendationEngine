using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RecommendationEngineServerSide.Controller.AdminControllers;
using RecommendationEngineServerSide.Common.DTO;
using RecommendationEngineServerSide.Common.Exceptions;
using RecommendationEngineServerSide.Service.AdminService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace RecommendationEngineServerSide.UnitTest
{
    [TestClass]
    public class AdminControllerTests
    {
        private Mock<IAdminService> _adminServiceMock;
        private Mock<IMapper> _mapperMock;
        private AdminController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _adminServiceMock = new Mock<IAdminService>();
            _mapperMock = new Mock<IMapper>();
            _controller = new AdminController(_adminServiceMock.Object, _mapperMock.Object);
        }

        [TestMethod]
        public async Task HandleAddMenu_ValidMenu_ReturnsSuccessResponse()
        {

            var menuDTO = new MenuDTO
            {
                MenuName = "Test Menu",
                MenuType = "Test Type",
                MenuPrice = 10
            };
            _adminServiceMock.Setup(service => service.AddMenu(menuDTO)).Returns(Task.CompletedTask);
            var result = await _controller.HandleAddMenu(menuDTO);
            Assert.AreEqual("Success", result.Status);
            Assert.AreEqual("The menu has been successfully added.", result.Message);
        }

        [TestMethod]
        public async Task HandleAddMenu_LoginException_ReturnsFailureResponse()
        {

            var menuDTO = new MenuDTO();
            _adminServiceMock.Setup(service => service.AddMenu(menuDTO)).ThrowsAsync(new LoginException("Login failed."));
            var result = await _controller.HandleAddMenu(menuDTO);
            Assert.AreEqual("Failure", result.Status);
            Assert.AreEqual("Login failed.", result.Message);
        }

        [TestMethod]
        public async Task HandleAddMenu_CommonException_ReturnsFailureResponse()
        {

            var menuDTO = new MenuDTO();
            _adminServiceMock.Setup(service => service.AddMenu(menuDTO)).ThrowsAsync(new CommonException("Common exception."));
            var result = await _controller.HandleAddMenu(menuDTO);
            Assert.AreEqual("Failure", result.Status);
            Assert.AreEqual("Common exception.", result.Message);
        }

        [TestMethod]
        public async Task FetchMenuDetails_ValidRequest_ReturnsMenuDetails()
        { 
            var fetchMenuRequestDto = new FetchMenuRequestDTO { MenuName = "TestMenu" };
            var fetchMenuDto = new FetchMenuDTO { Status = "Success" };
            _adminServiceMock.Setup(service => service.GetMenuDetailsByName(fetchMenuRequestDto.MenuName)).ReturnsAsync(fetchMenuDto);
            var result = await _controller.FetchMenuDetails(fetchMenuRequestDto);
            Assert.AreEqual("Success", result.Status);
        }

        [TestMethod]
        public async Task FetchMenuDetails_AdminException_ReturnsFailureResponse()
        {

            var fetchMenuRequestDto = new FetchMenuRequestDTO { MenuName = "NonexistentMenu" };
            _adminServiceMock.Setup(service => service.GetMenuDetailsByName(fetchMenuRequestDto.MenuName)).ThrowsAsync(new AdminException("Menu not found."));
            var result = await _controller.FetchMenuDetails(fetchMenuRequestDto);
            Assert.AreEqual("Failure", result.Status);
            Assert.AreEqual("Menu not found.", result.Message);
        }

        [TestMethod]
        public async Task HandleUpdateMenu_ValidUpdate_ReturnsSuccessResponse()
        {
            var updateMenuDTO = new UpdateMenuDTO
            {
                MenuName = "Updated Menu",
                MenuPrice = 15.99m
            };
            _adminServiceMock.Setup(service => service.UpdateMenu(updateMenuDTO)).Returns(Task.CompletedTask);
            var result = await _controller.HandleUpdateMenu(updateMenuDTO);
            Assert.AreEqual("Success", result.Status);
            Assert.AreEqual("Menu has been succesfully updated.", result.Message);
        }

        [TestMethod]
        public async Task HandleUpdateMenu_LoginException_ReturnsFailureResponse()
        {
            var updateMenuDTO = new UpdateMenuDTO();
            _adminServiceMock.Setup(service => service.UpdateMenu(updateMenuDTO)).ThrowsAsync(new LoginException("Login failed."));
            var result = await _controller.HandleUpdateMenu(updateMenuDTO);
            Assert.AreEqual("Failure", result.Status);
            Assert.AreEqual("Login failed.", result.Message);
        }

        [TestMethod]
        public async Task GetAllMenu_ReturnsAllMenuItems()
        {
            var menuListDto = new MenuListDTO
            {
                Status = "Success",
                Menu = new List<ListMenuDTO>
                {
                    new ListMenuDTO { MenuItemName = "Menu1", Price = 10.99m, MenuItemType = "Type1" },
                    new ListMenuDTO { MenuItemName = "Menu2", Price = 15.99m, MenuItemType = "Type2" }
                }
            };
            _adminServiceMock.Setup(service => service.GetAllMenu()).ReturnsAsync(menuListDto);
            var result = await _controller.GetAllMenu();
            Assert.AreEqual("Success", result.Status);
            Assert.IsNotNull(result.Menu);
            Assert.AreEqual(2, result.Menu.Count);
        }

        [TestMethod]
        public async Task GetAllMenu_AdminException_ReturnsFailureResponse()
        {
            _adminServiceMock.Setup(service => service.GetAllMenu()).ThrowsAsync(new AdminException("No menu found."));
            var result = await _controller.GetAllMenu();
            Assert.AreEqual("Failure", result.Status);
            Assert.AreEqual("No menu found.", result.Message);
        }

        [TestMethod]
        public async Task DeleteMenu_ValidDelete_ReturnsSuccessResponse()
        {
            var deleteMenuDTO = new DeleteMenuDTO { MenuName = "MenuToDelete" };
            _adminServiceMock.Setup(service => service.DeleteMenu(deleteMenuDTO)).Returns(Task.CompletedTask);
            var result = await _controller.DeleteMenu(deleteMenuDTO);
            Assert.AreEqual("Success", result.Status);
            Assert.AreEqual("Menu item has been succesfully deleted.", result.Message);
        }

        [TestMethod]
        public async Task DeleteMenu_LoginException_ReturnsFailureResponse()
        {
            var deleteMenuDTO = new DeleteMenuDTO();
            _adminServiceMock.Setup(service => service.DeleteMenu(deleteMenuDTO)).ThrowsAsync(new LoginException("Login failed."));
            var result = await _controller.DeleteMenu(deleteMenuDTO);
            Assert.AreEqual("Failure", result.Status);
            Assert.AreEqual("Login failed.", result.Message);
        }

        [TestMethod]
        public async Task DeleteMenu_CommonException_ReturnsFailureResponse()
        {
            var deleteMenuDTO = new DeleteMenuDTO();
            _adminServiceMock.Setup(service => service.DeleteMenu(deleteMenuDTO)).ThrowsAsync(new CommonException("Common exception."));
            var result = await _controller.DeleteMenu(deleteMenuDTO);
            Assert.AreEqual("Failure", result.Status);
            Assert.AreEqual("Common exception.", result.Message);
        }
    }
}

