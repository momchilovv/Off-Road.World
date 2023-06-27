#nullable disable

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static OffRoadWorld.Common.DataValidations.Event;

namespace OffRoadWorld.Data.Models
{
    [Comment("Off-Road event.")]
    public class Event
    {
        [Key]
        [Comment("Id of event.")]
        public int Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        [Comment("Title of event.")]
        public string Title { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        [Comment("Description of event.")]
        public string Description { get; set; }

        [Required]
        [Comment("Date and time of event creation.")]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [Required]
        [DisplayFormat(DataFormatString = "dd-MM-yyyy HH:mm")]
        public DateTime Start { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; }

        [Required]
        public string OwnerId { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public ApplicationUser Owner { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

        public ICollection<EventParticipants> Participants { get; set; } = new HashSet<EventParticipants>();
    }
}