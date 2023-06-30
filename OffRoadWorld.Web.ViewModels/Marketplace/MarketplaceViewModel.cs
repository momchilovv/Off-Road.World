namespace OffRoadWorld.Web.ViewModels.Marketplace
{
    public class MarketplaceViewModel
    {
        public Guid Id { get; set; }

        public Guid VehicleId { get; set; }

        public string Make { get; set; } = null!;

        public string Model { get; set; } = null!;

        public int ProductionYear { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public string? Seller { get; set; }
    }
}