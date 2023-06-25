#nullable disable

namespace OffRoadWorld.Web.ViewModels.Event
{
    public class EventViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Category { get; set; }

        public string Owner { get; set; }
    }
}