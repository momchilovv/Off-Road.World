﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static OffRoadWorld.Common.DataValidations.Vehicle;

namespace OffRoadWorld.Data.Models
{
    [Comment("Vehicles of users.")]
    public class Vehicle
    {
        [Key]
        [Comment("Id of vehicle")]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(MakeMaxLength)]
        [Comment("Make of vehicle")]
        public string Make { get; set; } = null!;

        [Required]
        [MaxLength(ModelMaxLength)]
        [Comment("Model of vehicle")]
        public string Model { get; set; } = null!;

        [Required]
        [MaxLength(MaxProductionYear)]
        [Comment("Year of production of vehicle.")]
        public int ProductionYear { get; set; }

        [Required]
        [Comment("Category Id of vehicle.")]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        [Comment("Owner Id of vehicle.")]
        public string? OwnerId { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public ApplicationUser? Owner { get; set; }
    }
}