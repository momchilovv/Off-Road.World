using OffRoadWorld.Web.ViewModels.Category;
using OffRoadWorld.Web.ViewModels.Event;

namespace OffRoadWorld.Services.Data.Contracts
{
    public interface IEventService
    {
        Task<ICollection<EventViewModel>> GetAllEventsAsync();

        Task<ICollection<CategoryModel>> GetAllCategoriesAsync();

        Task CreateEventAsync(EventFormModel model);

        EventDetailsViewModel GetDetailsById(int id);

        Task<EventFormModel> GetEventFormModelAsync(int id);

        Task JoinEventAsync(string userId, EventViewModel model);

        Task<ICollection<EventViewModel>> GetJoinedEventsAsync(string userId);

        Task DeleteEventAsync(int id);

        Task EditEventAsync(int id, EventFormModel model);

        Task<ICollection<EventDetailsViewModel>> GetMyEventsAsync(string userId);
    }
}