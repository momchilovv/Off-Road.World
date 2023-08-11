using Microsoft.EntityFrameworkCore;
using OffRoadWorld.Data;
using OffRoadWorld.Data.Models;
using OffRoadWorld.Services.Data.Contracts;
using OffRoadWorld.Services.Data.Services;
using OffRoadWorld.Web.ViewModels.Vehicle;

namespace OffRoadWorld.Services.Data.Tests
{
    [TestFixture]
    public class VehicleTest
    {
        private OffRoadWorldDbContext dbContext;

        private IVehicleService service;

        [SetUp]
        public async Task SetUp()
        {
            var ownerId = "ID1337";

            var options = new DbContextOptionsBuilder<OffRoadWorldDbContext>()
                .UseInMemoryDatabase("OffRoadWorldTestDB")
                .Options;

            dbContext = new OffRoadWorldDbContext(options);

            await dbContext.Database.EnsureCreatedAsync();

            var vehicle = new Vehicle()
            {
                Id = Guid.NewGuid(),
                Make = "KTM",
                Model = "EXC 450F",
                ProductionYear = 2022,
                Price = 13500,
                ImageUrl = "TestURL",
                CategoryId = 1,
                HorsePower = 76,
                EngineCapacity = 450,
                OwnerId = ownerId
            };

            await dbContext.Vehicles.AddAsync(vehicle);

            await dbContext.SaveChangesAsync();

            service = new VehicleService(dbContext);
        }

        [TearDown]
        public async Task TearDown()
        {
            await dbContext.Database.EnsureDeletedAsync();
        }

        [Test]
        public async Task AddVehicleAsync_CreatesVehicleSuccessfully()
        {
            // Arrange
            var vehicle = new VehicleFormModel()
            {
                Make = "Yamaha",
                Model = "YZ 250",
                ProductionYear = 2021,
                Price = 13600,
                ImageUrl = "TestURL",
                CategoryId = 1,
                HorsePower = 55,
                EngineCapacity = 250,
                OwnerId = "ID1337"
            };

            int expectedCount = dbContext.Vehicles.Count() + 1;

            // Act
            await service.AddVehicleAsync(vehicle);
            await dbContext.SaveChangesAsync();

            int actualCount = dbContext.Vehicles.Count();

            // Assert
            Assert.That(actualCount, Is.EqualTo(expectedCount));
        }
    }
}