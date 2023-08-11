using OffRoadWorld.Data.Models;
using OffRoadWorld.Web.ViewModels.Category;
using OffRoadWorld.Web.ViewModels.Event;

namespace OffRoadWorld.Services.Data.Contracts
{
    public interface IEventService
    {
        Task<ICollection<EventViewModel>> GetAllEventsAsync();

        Task<ICollection<CategoryModel>> GetAllCategoriesAsync();

        Task<ICollection<EventViewModel>> GetJoinedEventsAsync(string userId);

        Task CreateEventAsync(EventFormModel model);

        EventDetailsViewModel GetDetailsById(int id);

        Task<EventFormModel> GetEventFormModelAsync(int id);

        Task JoinEventAsync(string userId, EventViewModel model);

        Task LeaveEventAsync(string userId, EventViewModel model);

        Task DeleteEventAsync(int id);

        Task EditEventAsync(int id, EventFormModel model);

        Task<ICollection<EventDetailsViewModel>> GetMyEventsAsync(string userId);

        Task<Vehicle> GetVehicleAsync(string userId, int categoryId); 
    }
}