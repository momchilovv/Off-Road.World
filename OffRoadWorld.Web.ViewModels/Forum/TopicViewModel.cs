#nullable disable

using OffRoadWorld.Data.Models;

namespace OffRoadWorld.Web.ViewModels.Forum
{
    public class TopicViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public ApplicationUser Owner { get; set; }

        public string Forum { get; set; }
    }
}