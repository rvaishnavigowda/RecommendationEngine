using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RecommendationEngineServerSide.Common.DTO;
using RecommendationEngineServerSide.Common.Exceptions;
using RecommendationEngineServerSide.Controller.ChefControllers;
using RecommendationEngineServerSide.Service.ChefService;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RecommendationEngineServerSide.Controller.ChefControllers;
using RecommendationEngineServerSide.Common.DTO;
using RecommendationEngineServerSide.Common.Exceptions;
using RecommendationEngineServerSide.Service.ChefService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.UnitTest
{
    [TestClass]
    public class ChefControllerTests
    {
        private Mock<IChefService> _chefServiceMock;
        private Mock<IMapper> _mapperMock;
        private ChefController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _chefServiceMock = new Mock<IChefService>();
            _mapperMock = new Mock<IMapper>();
            _controller = new ChefController(_chefServiceMock.Object, _mapperMock.Object);
        }

        [TestMethod]
        public async Task HandleGetNotification_ValidUserName_ReturnsNotificationDTO()
        {
            // Arrange
            var userName = "testuser";
            var notifications = new List<string> { "Notification 1", "Notification 2" };
            _chefServiceMock.Setup(service => service.GetNotification(userName)).ReturnsAsync(notifications);

            // Act
            var result = await _controller.HandleGetNotification(userName);

            // Assert
            Assert.AreEqual("Success", result.Status);
            Assert.AreEqual("Notifications retrieved successfully.", result.Message);
            CollectionAssert.AreEqual(notifications, result.Notifications);
        }

        [TestMethod]
        public async Task HandleGetNotification_ExceptionThrown_ReturnsFailureResponse()
        {
            // Arrange
            var userName = "testuser";
            _chefServiceMock.Setup(service => service.GetNotification(userName)).ThrowsAsync(new Exception("Error retrieving notifications."));

            // Act
            var result = await _controller.HandleGetNotification(userName);

            // Assert
            Assert.AreEqual("Failure", result.Status);
            Assert.AreEqual("Error retrieving notifications.", result.Message);
        }

        [TestMethod]
        public async Task HandleMonthlyNotification_ValidDate_ReturnsMenuListDTO()
        {
            // Arrange
            var date = DateTime.Now;
            var menuListDto = new MenuListDTO
            {
                Status = "Success",
                Message = "New notification: Below is the list of items that performed poor this month.",
                Menu = new List<ListMenuDTO>()
            };
            _chefServiceMock.Setup(service => service.GetMonthlyNotification(date)).ReturnsAsync(menuListDto);

            // Act
            var result = await _controller.HandleMonthlyNotification(date);

            // Assert
            Assert.AreEqual("Success", result.Status);
            Assert.AreEqual("New notification: Below is the list of items that performed poor this month.", result.Message);
            // Additional assertions can be added for menu list comparison if needed
        }

        [TestMethod]
        public async Task HandleMonthlyNotification_MenuExceptionThrown_ReturnsFailureResponse()
        {
            // Arrange
            var date = DateTime.Now;
            _chefServiceMock.Setup(service => service.GetMonthlyNotification(date)).ThrowsAsync(new MenuException("No menu found."));

            // Act
            var result = await _controller.HandleMonthlyNotification(date);

            // Assert
            Assert.AreEqual("Failure", result.Status);
            Assert.AreEqual("No menu found.", result.Message);
        }

        [TestMethod]
        public async Task HandleGetMenuList_ValidDate_ReturnsMenuListDTO()
        {
            // Arrange
            var date = DateTime.Now;
            var menuListDto = new MenuListDTO
            {
                Status = "Success",
                Message = "Menu list retrieved successfully.",
                Menu = new List<ListMenuDTO>()
            };
            _chefServiceMock.Setup(service => service.GetMenuList(date)).ReturnsAsync(menuListDto);

            // Act
            var result = await _controller.HandleGetMenuList(date);

            // Assert
            Assert.AreEqual("Success", result.Status);
            Assert.AreEqual("Menu list retrieved successfully.", result.Message);
            // Additional assertions can be added for menu list comparison if needed
        }

        [TestMethod]
        public async Task HandleGetMenuList_DailyMenuExceptionThrown_ReturnsFailureResponse()
        {
            // Arrange
            var date = DateTime.Now;
            _chefServiceMock.Setup(service => service.GetMenuList(date)).ThrowsAsync(new DailyMenuException("Daily menu not present."));

            // Act
            var result = await _controller.HandleGetMenuList(date);

            // Assert
            Assert.AreEqual("Failure", result.Status);
            Assert.AreEqual("Daily menu not present.", result.Message);
        }

        [TestMethod]
        public async Task HandleAddDailyMenu_CommonExceptionThrown_ReturnsFailureResponse()
        {
            // Arrange
            var menuList = new NewDailyMenuDTO();
            _chefServiceMock.Setup(service => service.AddDailyMenu(menuList)).ThrowsAsync(new CommonException("Common exception occurred."));

            // Act
            var result = await _controller.HandleAddDailyMenu(menuList);

            // Assert
            Assert.AreEqual("Failure", result.Status);
            Assert.AreEqual("Common exception occurred.", result.Message);
        }

        [TestMethod]
        public async Task HandleDiscardFoodItem_ValidMenuItem_ReturnsSuccessResponse()
        {
            // Arrange
            var menuItem = "TestMenuItem";
            _chefServiceMock.Setup(service => service.DiscardMenuItem(menuItem)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.HandleDiscardFoodItem(menuItem);

            // Assert
            Assert.AreEqual("Success", result.Status);
            Assert.AreEqual("The menu item has been successfully discarded", result.Message);
        }

        [TestMethod]
        public async Task HandleDiscardFoodItem_ExceptionThrown_ReturnsFailureResponse()
        {
            // Arrange
            var menuItem = "TestMenuItem";
            _chefServiceMock.Setup(service => service.DiscardMenuItem(menuItem)).ThrowsAsync(new Exception("Error discarding menu item."));

            // Act
            var result = await _controller.HandleDiscardFoodItem(menuItem);

            // Assert
            Assert.AreEqual("Failure", result.Status);
            Assert.AreEqual("Error discarding menu item.", result.Message);
        }

        [TestMethod]
        public async Task HandleImproveFoodItem_ValidMenuItem_ReturnsSuccessResponse()
        {
            // Arrange
            var menuItem = new UpgradeMenuDto { MenuName = "TestMenuItem", CurrentDate = DateTime.Now };
            _chefServiceMock.Setup(service => service.ImproviseMenuItem(menuItem)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.HandleImproveFoodItem(menuItem);

            // Assert
            Assert.AreEqual("Success", result.Status);
            Assert.AreEqual("The menu item has been successfully discarded", result.Message);
        }

        [TestMethod]
        public async Task HandleImproveFoodItem_ExceptionThrown_ReturnsFailureResponse()
        {
            // Arrange
            var menuItem = new UpgradeMenuDto { MenuName = "TestMenuItem", CurrentDate = DateTime.Now };
            _chefServiceMock.Setup(service => service.ImproviseMenuItem(menuItem)).ThrowsAsync(new Exception("Error improving menu item."));

            // Act
            var result = await _controller.HandleImproveFoodItem(menuItem);

            // Assert
            Assert.AreEqual("Failure", result.Status);
            Assert.AreEqual("Error improving menu item.", result.Message);
        }

    }
}
