﻿#nullable disable

using OffRoadWorld.Data.Models;

namespace OffRoadWorld.Web.ViewModels.Event
{
    public class EventViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime Start { get; set; }

        public string Location { get; set; }

        public string Category { get; set; }

        public string Owner { get; set; }

        public ICollection<EventParticipants> Participants { get; set; } = new List<EventParticipants>();
    }
}