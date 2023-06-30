using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OffRoadWorld.Data.Models
{
    [Comment("Marketplace")]
    public class Marketplace
    {
        [Key]
        [Comment("Marketplace listing id.")]
        public Guid Id { get; set; }

        [Required]
        [Comment("Listing's vehicle id.")]
        public Guid VehicleId { get; set; }

        [ForeignKey(nameof(VehicleId))]
        public Vehicle Vehicle { get; set; } = null!;

        [Required]
        [Comment("Listing's category id.")]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        [Comment("Listing's seller id.")]
        public string? SellerId { get; set; }

        [ForeignKey(nameof(SellerId))]
        public ApplicationUser? Seller { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; } = new HashSet<Vehicle>();
    }
}