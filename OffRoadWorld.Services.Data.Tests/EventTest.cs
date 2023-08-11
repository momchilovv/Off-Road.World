using Microsoft.EntityFrameworkCore;
using OffRoadWorld.Data;
using OffRoadWorld.Data.Models;
using OffRoadWorld.Services.Data.Contracts;
using OffRoadWorld.Services.Data.Services;
using OffRoadWorld.Web.ViewModels.Event;

namespace OffRoadWorld.Services.Data.Tests
{
    [TestFixture]
    public class EventTest
    {
        private OffRoadWorldDbContext context;

        private IEventService service;

        [SetUp]
        public async Task Setup()
        {
            var options = new DbContextOptionsBuilder<OffRoadWorldDbContext>()
                .UseInMemoryDatabase("OffRoadWorldTestDB")
                .Options;

            context = new OffRoadWorldDbContext(options);

            await context.Database.EnsureCreatedAsync();

            await context.Events.AddAsync(new Event
            {
                Id = 1,
                Title = "Test Event",
                Description = "This is a test event",
                Start = DateTime.UtcNow.AddDays(22),
                Country = "Bulgaria",
                City = "Montana",
                Address = "bul. Treti Mart 45",
                CreatedOn = DateTime.UtcNow,
                CategoryId = 1,
                OwnerId = Guid.NewGuid().ToString()
            });

            await context.SaveChangesAsync();

            service = new EventService(context);
        }

        [TearDown]
        public async Task TearDown()
        {
            await context.Database.EnsureDeletedAsync();
        }

        [Test]
        public async Task CreateEventAsync_CreatesEventSuccessfully()
        {
            // Arrange
            int expectedCount = context.Events.Count() + 1;

            var model = new EventFormModel()
            {
                Title = "New Event",
                Description = "This is the newest event.",
                Start = DateTime.UtcNow.AddMonths(1),
                CategoryId = 1,
                OwnerId = Guid.NewGuid().ToString(),
                Country = "Bulgaria",
                City = "Chiprovtsi",
                Address = "ul. Petar Bogdan 4"
            };

            // Act
            await service.CreateEventAsync(model);

            int actualCount = context.Events.Count();

            // Assert 
            Assert.That(actualCount, Is.EqualTo(expectedCount));
        }

        [Test]
        public async Task EditEventAsync_FindsTheCorrectEventToEdit()
        {
            // Arrange
            var expectedName = "Test Event";

            // Act
            var _event = await context.Events.FindAsync(1);

            var actualName = _event!.Title;

            // Assert
            Assert.That(actualName, Is.EqualTo(expectedName));
        }

        [Test]
        public async Task EditEventAsync_EditsEventSuccessfully()
        {
            // Arrange
            var expectedTitle = "New Edited Title";
            var expectedDescription = "This is the edited description event.";
            var expectedAddress = "bul. Petar Parchevich 1";

            var model = new EventFormModel()
            {
                Title = "New Edited Title",
                Description = "This is the edited description event.",
                Start = DateTime.UtcNow.AddMonths(1),
                CategoryId = 1,
                OwnerId = Guid.NewGuid().ToString(),
                Country = "Bulgaria",
                City = "Chiprovtsi",
                Address = "bul. Petar Parchevich 1"
            };

            // Act
            await service.EditEventAsync(1, model);

            var _event = await context.Events.FindAsync(1);

            var actualTitle = _event!.Title;
            var actualDescription = _event!.Description;
            var actualAddress = _event!.Address;

            // Assert
            Assert.That(actualTitle, Is.EqualTo(expectedTitle));
            Assert.That(actualDescription, Is.EqualTo(expectedDescription));
            Assert.That(actualAddress, Is.EqualTo(expectedAddress));
        }

        [Test]
        public async Task GetAllCategoriesAsync_ReturnsAllEventCategories()
        {
            // Arrange
            await context.Categories.AddAsync(new Category { Id = 1, Name = "Enduro" });
            await context.Categories.AddAsync(new Category { Id = 2, Name = "ATV" });
            await context.Categories.AddAsync(new Category { Id = 3, Name = "Jeep" });

            await context.SaveChangesAsync();

            var expectedCategories = new List<Category>()
            {
                new Category { Id = 1, Name = "Enduro" },
                new Category { Id = 2, Name = "ATV" },
                new Category { Id = 3, Name = "Jeep" }
            };

            // Act
            var categories = await service.GetAllCategoriesAsync();

            // Assert
            Assert.That(context.Categories.Count(), Is.EqualTo(categories.Count()));

            foreach (var category in expectedCategories)
            {
                Assert.That(categories.Any(c => c.Id == category.Id));
            }
        }
    }
}