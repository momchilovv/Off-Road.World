#nullable disable

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static OffRoadWorld.Common.DataValidations.Post;

namespace OffRoadWorld.Data.Models
{
    [Comment("Post")]
    public class Post
    {
        [Key]
        [Comment("Post id.")]
        public Guid Id { get; set; }

        [Required]
        [Comment("Post content.")]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; }

        [Required]
        [Comment("Post created at.")]
        public DateTime CreatedAt { get; set; }

        [Required]
        [Comment("Topic id.")]
        public Guid TopicId { get; set; }

        [ForeignKey(nameof(TopicId))]
        public Topic Topics { get; set; }

        [Required]
        [Comment("Owner id.")]
        public string OwnerId { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public ApplicationUser Owner { get; set; }
    }
}