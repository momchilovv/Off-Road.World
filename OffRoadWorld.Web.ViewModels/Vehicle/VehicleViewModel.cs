namespace OffRoadWorld.Web.ViewModels.Vehicle
{
    public class VehicleViewModel
    {
        public Guid Id { get; set; }

        public string Make { get; set; } = null!;

        public string Model { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public int ProductionYear { get; set; }

        public int HorsePower { get; set; }

        public int? EngineCapacity { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; } = null!;

        public string? Owner { get; set; }

        public string? OwnerId { get; set; }
    }
}