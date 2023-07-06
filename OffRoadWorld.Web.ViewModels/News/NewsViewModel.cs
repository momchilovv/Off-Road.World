#nullable disable

namespace OffRoadWorld.Web.ViewModels.News
{
    public class NewsViewModel
    {
        public string Title { get; set; } 

        public string Author { get; set; }

        public string Description { get; set; } 

        public string Url { get; set; }

        public DateTime? PublishedAt { get; set; }
    }
}