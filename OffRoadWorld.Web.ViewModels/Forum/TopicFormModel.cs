#nullable disable

using System.ComponentModel.DataAnnotations;
using static OffRoadWorld.Common.DataValidations.Topic;

namespace OffRoadWorld.Web.ViewModels.Forum
{
    public class TopicFormModel
    {
        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(ContentMaxLength, MinimumLength = ContentMinLength)]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public string OwnerId { get; set; }
        
        public int ForumId { get; set; }
    }
}