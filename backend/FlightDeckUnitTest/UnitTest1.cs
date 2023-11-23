using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using flight_data_server.Controllers;
using flight_data_server.Database;
using flight_data_server.Interface;
using flight_data_server.Models;
using flight_data_server.Models.Airliner;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace YourNamespace.Tests
    {
    [TestClass]
    public class AirlinerControllerTests
        {
        private readonly Mock<ILogger<AirlinerController>> _loggerMock = new Mock<ILogger<AirlinerController>>();
        private readonly Mock<AirlinerDBContext> _adbContextMock = new Mock<AirlinerDBContext>();
        private readonly Mock<IAirlinerDBFunctions> _dbAirlinerMock = new Mock<IAirlinerDBFunctions>();
        private readonly Mock<IHttpContextAccessor> _contextAccessorMock = new Mock<IHttpContextAccessor>();

        [TestMethod]
        public async Task GetAirliners_ReturnsOkResult()
            {
            // Arrange
            var controller = new AirlinerController(_loggerMock.Object, _adbContextMock.Object, _dbAirlinerMock.Object, _contextAccessorMock.Object);

            // Mock HttpContext
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Role, "user") }));
            _contextAccessorMock.Setup(x => x.HttpContext.User).Returns(user);

            _dbAirlinerMock.Setup(repo => repo.GetAllAsync(null)).ReturnsAsync(new List<Airliner>());

            // Act
            var result = await controller.GetAirliners();

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            //Assert.IsInstanceOfType(((OkObjectResult)OkResult).Value, typeof(APIResponse));
            //Assert.IsNotNull(apiResponse);
            //Assert.AreEqual(StatusCodes.Status200OK, ((OkObjectResult)okResult).StatusCode);
            }

        // Add similar tests for other actions like GetAirliner, CreateAirliner, etc.
        }
    }
