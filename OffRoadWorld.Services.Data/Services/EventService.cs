using Microsoft.EntityFrameworkCore;
using OffRoadWorld.Data;
using OffRoadWorld.Data.Models;
using OffRoadWorld.Services.Data.Contracts;
using OffRoadWorld.Web.ViewModels.Category;
using OffRoadWorld.Web.ViewModels.Event;
using Event = OffRoadWorld.Data.Models.Event;

namespace OffRoadWorld.Services.Data.Services
{
    public class EventService : IEventService
    {
        private readonly OffRoadWorldDbContext dbContext;

        public EventService(OffRoadWorldDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ICollection<EventViewModel>> GetAllEventsAsync()
        {
            return await dbContext.Events
                .Where(e => e.Start >= DateTime.UtcNow)
                .Select(e => new EventViewModel()
                {
                    Id = e.Id,
                    Title = e.Title,
                    Category = e.Category.Name,
                    Start = e.Start,
                    Location = $"{e.City}, {e.Country}",
                    Owner = e.Owner.UserName,
                    Participants = e.Participants
                })
                .ToListAsync();
        }

        public async Task CreateEventAsync(EventFormModel model)
        {
            var _event = new Event()
            {
                Title = model.Title,
                Description = model.Description,
                CreatedOn = DateTime.UtcNow,
                Start = model.Start,
                Country = model.Country,
                City = model.City,
                Address = model.Address,
                CategoryId = model.CategoryId,
                OwnerId = model.OwnerId
            };

            await dbContext.Events.AddAsync(_event);
            await dbContext.SaveChangesAsync();
        }

        public async Task EditEventAsync(int id, EventFormModel model)
        {
            var _event = await dbContext.Events.FindAsync(id);

            _event!.Title = model.Title;
            _event.Description = model.Description;
            _event.Start = model.Start;
            _event.Country = model.Country;
            _event.City = model.City;
            _event.Address = model.Address;
            _event.CategoryId = model.CategoryId;

            await dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<CategoryModel>> GetAllCategoriesAsync()
        {
            return await dbContext.Categories
                .Select(c => new CategoryModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }

        public EventDetailsViewModel GetDetailsById(int id)
        {
            return dbContext.Events
                .Where(e => e.Id == id)
                .Select(e => new EventDetailsViewModel()
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    CreatedOn = e.CreatedOn,
                    Start = e.Start,
                    Location = $"{e.City}, {e.Country}",
                    Address = e.Address,
                    Category = e.Category.Name,
                    CategoryId = e.CategoryId,
                    Owner = e.Owner.UserName,
                    TotalParticipants = e.Participants.Count()
                })
                .First();
        }

        public async Task<EventFormModel> GetEventFormModelAsync(int id)
        {
            return await dbContext.Events
                .Where(e => e.Id == id)
                .Select(e => new EventFormModel()
                {
                    Title = e.Title,
                    Description = e.Description,
                    Start = e.Start,
                    Country = e.Country,
                    City = e.City,
                    Address = e.Address,
                    OwnerId = e.Owner.Id,
                    CategoryId = e.CategoryId,
                    Categories = dbContext.Categories
                                    .Select(c => new CategoryModel()
                                    {
                                        Id = c.Id,
                                        Name = c.Name
                                    })
                                    .ToList()
                })
                .FirstAsync();
        }

        public async Task<ICollection<EventDetailsViewModel>> GetMyEventsAsync(string userId)
        {
            return await dbContext.Events
                .Where(e => e.OwnerId == userId)
                .Select(e => new EventDetailsViewModel()
                {
                    Id = e.Id,
                    Title = e.Title,
                    Start = e.Start,
                    Location = $"{e.City}, {e.Country}",
                    Address = e.Address,
                    Category = e.Category.Name,
                    Owner = e.Owner.UserName
                })
                .ToListAsync();
        }

        public async Task DeleteEventAsync(int id)
        {
            var _event = await dbContext.Events
                .Where(e => e.Id == id).FirstAsync();

            var participants = await dbContext.EventParticipants
                .Where(ep => ep.EventId == id)
                .ToListAsync();

            dbContext.EventParticipants.RemoveRange(participants);

            dbContext.Events.Remove(_event);

            await dbContext.SaveChangesAsync();
        }

        public async Task JoinEventAsync(string userId, EventViewModel model)
        {
            if (!await dbContext.EventParticipants
                .AnyAsync(e => e.ParticipantId == userId && e.EventId == model.Id))
            {
                await dbContext.EventParticipants.AddAsync(new EventParticipants
                {
                    ParticipantId = userId,
                    EventId = model.Id
                });

                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<Vehicle> GetVehicleAsync(string userId, int categoryId)
        {
#pragma warning disable CS8603 
// Possible null reference return.
            return await dbContext.Vehicles
                .Where(v => v.OwnerId == userId && v.CategoryId == categoryId)
                .FirstOrDefaultAsync();
#pragma warning restore CS8603
        }

        public async Task<ICollection<EventViewModel>> GetJoinedEventsAsync(string userId)
        {
            return await dbContext.EventParticipants
                .Where(e => e.ParticipantId == userId)
                .Select(p => new EventViewModel()
                {
                    Id = p.Event.Id,
                    Title = p.Event.Title,
                    Start = p.Event.Start,
                    Location = $"{p.Event.City}, {p.Event.Country}",
                    Category = p.Event.Category.Name
                })
                .ToListAsync();
        }

        public async Task LeaveEventAsync(string userId, EventViewModel model)
        {
            var _event = await dbContext.EventParticipants
                .FirstOrDefaultAsync(ep => ep.ParticipantId == userId && ep.EventId == model.Id);

            if (_event != null)
            {
                dbContext.EventParticipants.Remove(_event);

                await dbContext.SaveChangesAsync();
            }
        }
    }
}