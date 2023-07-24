using OffRoadWorld.Data.Models;

namespace OffRoadWorld.Web.ViewModels.Forum
{
    public class PostViewModel
    {
        public Guid Id { get; set; }

        public Topic Topic { get; set; } = null!;

        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}