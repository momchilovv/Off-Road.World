#nullable disable

namespace OffRoadWorld.Web.ViewModels.Event
{
    public class EventFilterViewModel
    {
        public string SortOrder { get; set; }

        public string Category { get; set; }

        public ICollection<EventViewModel> Events { get; set; }
    }
}