using OffRoadWorld.Web.ViewModels.Category;

namespace OffRoadWorld.Web.ViewModels.Event
{
    public class EventDetailsViewModel : EventViewModel
    {
        public string Description { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public int TotalParticipants { get; set; }

        public int CategoryId { get; set; }

        public ICollection<CategoryModel> Categories { get; set; } = new List<CategoryModel>();
    }
}