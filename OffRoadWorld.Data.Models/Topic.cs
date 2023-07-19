#nullable disable

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static OffRoadWorld.Common.DataValidations.Topic;

namespace OffRoadWorld.Data.Models
{
    [Comment("Topic")]
    public class Topic
    {
        [Key]
        [Comment("Topic id.")]
        public Guid Id { get; set; }

        [Required]
        [Comment("Topic title.")]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [Comment("Topic content.")]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; }

        [Required]
        [Comment("Topic owner")]
        public string OwnerId { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public ApplicationUser Owner { get; set; }

        [Required]
        [Comment("Topic created at.")]
        public DateTime CreatedAt { get; set; }

        [Required]
        [Comment("Forum id.")]
        public int ForumId { get; set; }

        [ForeignKey(nameof(ForumId))]
        public Forum Forum { get; set; }

        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}