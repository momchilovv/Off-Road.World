#nullable disable

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OffRoadWorld.Data.Models
{
    [Comment("Participants of event.")]
    public class EventParticipants
    {
        [Key]
        [Comment("Id of the participant.")]
        public string ParticipantId { get; set; }

        [ForeignKey(nameof(ParticipantId))]
        public ApplicationUser Participant { get; set; }

        [Key]
        [Comment("Id of the event.")]
        public int EventId { get; set; }

        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; }
    }
}