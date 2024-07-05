using AutoMapper;
using Moq;
using RecommendationEngineServerSide.Common.DTO;
using RecommendationEngineServerSide.Controller.EmployeeControllers;
using RecommendationEngineServerSide.Service.EmplyoeeService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using RecommendationEngineServerSide.Controller.EmployeeControllers;
using RecommendationEngineServerSide.Service.EmplyoeeService;
using RecommendationEngineServerSide.Common.DTO;
using RecommendationEngineServerSide.Common.Exceptions;

namespace RecommendationEngineServerSide.UnitTest
{
    [TestClass]
    public class EmployeeControllerTests
    {
        private Mock<IEmplyoeeService> _mockEmployeeService;
        private Mock<IMapper> _mockMapper;
        private EmployeeController _employeeController;

        [TestInitialize]
        public void Setup()
        {
            _mockEmployeeService = new Mock<IEmplyoeeService>();
            _mockMapper = new Mock<IMapper>();
            _employeeController = new EmployeeController(_mockEmployeeService.Object, _mockMapper.Object);
        }

        [TestMethod]
        public async Task HandleGetNotification_Success()
        {
            // Arrange
            string userName = "testUser";
            var notifications = new List<string> { "Notification1", "Notification2" };

            _mockEmployeeService.Setup(service => service.GetNotification(userName)).ReturnsAsync(notifications);

            // Act
            var result = await _employeeController.HandleGetNotification(userName);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Success", result.Status);
            Assert.AreEqual("Notifications retrieved successfully.", result.Message);
            CollectionAssert.AreEqual(notifications, result.Notifications);
        }

        [TestMethod]
        public async Task HandleGetNotification_Exception()
        {
            // Arrange
            string userName = "testUser";
            var exceptionMessage = "Test exception";

            _mockEmployeeService.Setup(service => service.GetNotification(userName)).ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _employeeController.HandleGetNotification(userName);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Failure", result.Status);
            Assert.AreEqual(exceptionMessage, result.Message);
        }

        [TestMethod]
        public async Task HandleGetMonthlyNotification_Success()
        {
            // Arrange
            string userName = "testUser";
            var notificationDTO = new NotificationDTO { Status = "Success", Message = "Monthly notification retrieved." };

            _mockEmployeeService.Setup(service => service.UpgradeMenuFeedback(userName)).ReturnsAsync(notificationDTO);

            // Act
            var result = await _employeeController.HandleGetMonthlyNotification(userName);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Success", result.Status);
            Assert.AreEqual("Monthly notification retrieved.", result.Message);
        }

        [TestMethod]
        public async Task HandleGetMonthlyNotification_Exception()
        {
            // Arrange
            string userName = "testUser";
            var exceptionMessage = "Test exception";

            _mockEmployeeService.Setup(service => service.UpgradeMenuFeedback(userName)).ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _employeeController.HandleGetMonthlyNotification(userName);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Failure", result.Status);
            Assert.AreEqual(exceptionMessage, result.Message);
        }

        [TestMethod]
        public async Task HandleGetUserProfile_Success()
        {
            // Arrange
            string userName = "testUser";
            var employeeUpdateDTO = new EmployeeUpdateDTO { Status = "Success", Message = "User profile retrieved." };

            _mockEmployeeService.Setup(service => service.GetUserPreference(userName)).ReturnsAsync(employeeUpdateDTO);

            // Act
            var result = await _employeeController.HandleGetUserProfile(userName);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Success", result.Status);
            Assert.AreEqual("User profile retrieved.", result.Message);
        }

        [TestMethod]
        public async Task HandleGetUserProfile_Exception()
        {
            // Arrange
            string userName = "testUser";
            var exceptionMessage = "Test exception";

            _mockEmployeeService.Setup(service => service.GetUserPreference(userName)).ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _employeeController.HandleGetUserProfile(userName);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Failure", result.Status);
            Assert.AreEqual(exceptionMessage, result.Message);
        }

        [TestMethod]
        public async Task HandleGetDailyMenu_Success()
        {
            // Arrange
            string userName = "testUser";
            DateTime currentDate = DateTime.Now;
            var dailyMenuDTO = new DailyMenuDTO
            {
                Status = "Success",
                Message = "Daily menu retrieved successfully.",
                MenuList = new List<DailyMenuList>()
            };

            _mockEmployeeService.Setup(service => service.GetDailyMenuList(It.Is<DailyMenuDTO>(dto => dto.UserName == userName && dto.CurrentDate == currentDate)))
                .ReturnsAsync(dailyMenuDTO);

            // Act
            var result = await _employeeController.HandleGetDailyMenu(userName, currentDate);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Success", result.Status);
            Assert.AreEqual("Daily menu retrieved successfully.", result.Message);
        }

        [TestMethod]
        public async Task HandleGetDailyMenu_Exception()
        {
            // Arrange
            string userName = "testUser";
            DateTime currentDate = DateTime.Now;
            var exceptionMessage = "Test exception";

            _mockEmployeeService.Setup(service => service.GetDailyMenuList(It.IsAny<DailyMenuDTO>())).ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _employeeController.HandleGetDailyMenu(userName, currentDate);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Failure", result.Status);
            Assert.AreEqual(exceptionMessage, result.Message);
        }

        [TestMethod]
        public async Task HandlePlaceOrder_Success()
        {
            // Arrange
            var orderDetailDTO = new OrderDetailDTO
            {
                UserName = "testUser",
                OrderDate = DateTime.Now,
                Items = new List<OrderItemDTO>()
            };

            var placedOrderDetailDTO = new OrderDetailDTO
            {
                Status = "Success",
                Message = "Order placed successfully.",
                OrderDate = DateTime.Now,
                Items = new List<OrderItemDTO>()
            };

            _mockEmployeeService.Setup(service => service.PlaceOrder(orderDetailDTO)).ReturnsAsync(placedOrderDetailDTO);

            // Act
            var result = await _employeeController.HandlePlaceOrder(orderDetailDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Success", result.Status);
            Assert.AreEqual("Order placed successfully.", result.Message);
        }

        [TestMethod]
        public async Task HandlePlaceOrder_Exception()
        {
            // Arrange
            var orderDetailDTO = new OrderDetailDTO
            {
                UserName = "testUser",
                OrderDate = DateTime.Now,
                Items = new List<OrderItemDTO>()
            };

            var exceptionMessage = "Test exception";

            _mockEmployeeService.Setup(service => service.PlaceOrder(orderDetailDTO)).ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _employeeController.HandlePlaceOrder(orderDetailDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Failure", result.Status);
            Assert.AreEqual(exceptionMessage, result.Message);
        }

        [TestMethod]
        public async Task HandleGiveFeedback_Success()
        {
            // Arrange
            var feedbackDTO = new FeedbackDTO
            {
                UserName = "testUser",
                MenuName = "TestMenu",
                Rating = 5,
                Comment = "Great!",
                FeedbackDate = DateTime.Now
            };

            _mockEmployeeService.Setup(service => service.GiveFeedback(feedbackDTO)).Returns(Task.CompletedTask);

            // Act
            var result = await _employeeController.HandleGiveFeedback(feedbackDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Success", result.Status);
            Assert.AreEqual("Feedback submitted successfully.", result.Message);
        }

        [TestMethod]
        public async Task HandleGiveFeedback_Exception()
        {
            // Arrange
            var feedbackDTO = new FeedbackDTO
            {
                UserName = "testUser",
                MenuName = "TestMenu",
                Rating = 5,
                Comment = "Great!",
                FeedbackDate = DateTime.Now
            };

            var exceptionMessage = "Test exception";

            _mockEmployeeService.Setup(service => service.GiveFeedback(feedbackDTO)).ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _employeeController.HandleGiveFeedback(feedbackDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Failure", result.Status);
            Assert.AreEqual(exceptionMessage, result.Message);
        }
    }
}
