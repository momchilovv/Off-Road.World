#nullable disable

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static OffRoadWorld.Common.DataValidations.Forum;

namespace OffRoadWorld.Data.Models
{
    [Comment("Forum")]
    public class Forum
    {
        [Key]
        [Comment("Forum id.")]
        public int Id { get; set; }

        [Required]
        [Comment("Forum title.")]
        [MaxLength(TitleMaxLenght)]
        public string Title { get; set; }

        [Required]
        [Comment("Forum description.")]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public ICollection<Topic> TopicsCount { get; set; } = new List<Topic>();
    }
}