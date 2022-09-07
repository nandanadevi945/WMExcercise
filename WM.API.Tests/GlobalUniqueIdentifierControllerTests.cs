using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using WM.Api.Controllers;
using WM.Api.DB;
using WM.Api.Models;

namespace WM.API.Tests
{
    [TestClass]
    public class GlobalUniqueIdentifierControllerTests
    {
        private GlobalUniqueIdentifierController controller;
        private CoreDbContext context;
        private ILogger<GlobalUniqueIdentifierController> logger;

        [TestInitialize]
        public void TestInit()
        {
            var data = new List<GlobalUniqueIdentifier>
            {
                new GlobalUniqueIdentifier { Guid = "47FA41F90E45452085CA34CBA33FA67C", Expire = System.DateTime.Today.AddDays(10), Id = 1, Usr = "TestUser" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<GlobalUniqueIdentifier>>();
            mockSet.As<IQueryable<GlobalUniqueIdentifier>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<GlobalUniqueIdentifier>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<GlobalUniqueIdentifier>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<GlobalUniqueIdentifier>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<CoreDbContext>();
            mockContext.Setup(c => c.GlobalUniqueIdentifiers).Returns(mockSet.Object);

            context = Substitute.For<CoreDbContext>();
            logger = Substitute.For<ILogger<GlobalUniqueIdentifierController>>();
            controller = new GlobalUniqueIdentifierController(mockContext.Object, logger);
        }
        [TestMethod]
        public void ShouldReturnBadRequest_WhenInvalidGuid()
        {
            IActionResult result = controller.Get("3422342");
            var contentResult = result as BadRequestObjectResult;
            Assert.IsNotNull(contentResult);
        }
        [TestMethod]
        public void ShouldReturnBadRequest_WhenNullGuid()
        {
            IActionResult result = controller.Get(null);
            var contentResult = result as BadRequestObjectResult;
            Assert.IsNotNull(contentResult);
        }
        [TestMethod]
        public void ShouldReturnGlobalUniqueIdentifier_WhenValidGuid()
        {
            var existingGuid = new GlobalUniqueIdentifier { Guid = "47FA41F90E45452085CA34CBA33FA67C", Expire = System.DateTime.Today.AddDays(10), Id = 1, Usr = "TestUser" };
            IActionResult result = controller.Get("47FA41F90E45452085CA34CBA33FA67C");
            var contentResult = result as OkObjectResult;
            Assert.IsNotNull(contentResult);
        }
        [TestMethod]
        public void ShouldAddNewGlobalUniqueIdentifier_WhenValidGuid()
        {
            var newGuid = new NewGlobalUniqueIdentifierModel { Guid = "DA52C3AF099A4707A853E0195907B37A", ExpiryDays = 5, Usr = "TestUser" };
            IActionResult result = controller.Post(newGuid);
            var contentResult = result as OkObjectResult;
            Assert.IsNotNull(contentResult);
        }
        [TestMethod]
        public void ShouldUpdateExistingGlobalUniqueIdentifier_WhenValidGuid()
        {
            var updateModel = new UpdateGlobalUniqueIdentifierModel { ExpiryDays = 15};
            IActionResult result = controller.Put("47FA41F90E45452085CA34CBA33FA67C", updateModel);
            var contentResult = result as OkObjectResult;
            Assert.IsNotNull(contentResult);
        }
        [TestMethod]
        public void ShouldDeleteExistingGlobalUniqueIdentifier_WhenValidGuid()
        {
            IActionResult result = controller.Delete("47FA41F90E45452085CA34CBA33FA67C");
            var contentResult = result as OkResult;
            Assert.IsNotNull(contentResult);
        }
    }
}