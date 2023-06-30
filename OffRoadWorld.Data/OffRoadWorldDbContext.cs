﻿#nullable disable

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OffRoadWorld.Data.Models;

namespace OffRoadWorld.Data
{
    public class OffRoadWorldDbContext : IdentityDbContext<ApplicationUser>
    {
        public OffRoadWorldDbContext(DbContextOptions<OffRoadWorldDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<EventParticipants> EventParticipants { get; set; }

        public DbSet<Marketplace> Marketplace { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Vehicle>()
                .Property(v => v.Price)
                .HasPrecision(18, 2);

            builder.Entity<ApplicationUser>()
                .Property(u => u.Balance)
                .HasPrecision(18, 2);

            builder.Entity<EventParticipants>()
                .HasKey(k => new { k.EventId, k.ParticipantId });

            builder.Entity<EventParticipants>()
                .HasOne(e => e.Event)
                .WithMany(p => p.Participants)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Event>()
                .HasOne(e => e.Category)
                .WithMany(e => e.Events)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Marketplace>()
                .HasOne(x => x.Vehicle)
                .WithMany()
                .HasForeignKey(x => x.VehicleId);

            base.OnModelCreating(builder);
        }
    }
}