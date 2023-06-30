using Microsoft.EntityFrameworkCore;
using OffRoadWorld.Data;
using OffRoadWorld.Services.Data.Contracts;
using OffRoadWorld.Web.ViewModels.Marketplace;
using OffRoadWorld.Web.ViewModels.Vehicle;

namespace OffRoadWorld.Services.Data.Services
{
    public class MarketplaceService : IMarketplaceService
    {
        private readonly OffRoadWorldDbContext dbContext;

        public MarketplaceService(OffRoadWorldDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<VehicleViewModel> GetVehicleByIdAsync(Guid vehicleId)
        {
            return await dbContext.Vehicles
                .Where(v => v.Id == vehicleId)
                .Select(v => new VehicleViewModel
                {
                    Id = v.Id,
                    Category = v.Category.Name,
                    Price = v.Price,
                    OwnerId = v.OwnerId
                })
                .FirstAsync();
        }

        public async Task BuyVehicleAsync(string userId, Guid vehicleId)
        {
            var user = await dbContext.Users.FindAsync(userId);

            var vehicle = await dbContext.Vehicles.FindAsync(vehicleId);

            var listing = await dbContext.Marketplace
                .Where(v => v.VehicleId == vehicleId)
                .FirstAsync();

            if (user?.Balance >= vehicle?.Price)
            {
                user.Balance -= vehicle.Price;

                if (vehicle.OwnerId != null)
                {
                    var currentOwner = await dbContext.Users.FindAsync(vehicle.OwnerId);

                    currentOwner!.Balance += vehicle.Price;
                }

                vehicle.OwnerId = userId;

                dbContext.Marketplace.Remove(listing);
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<MarketplaceViewModel>> GetAllListingsAsync()
        {
            return await dbContext.Marketplace
                .Select(m => new MarketplaceViewModel
                {
                    Id = m.Id,
                    VehicleId = m.VehicleId,
                    Make = m.Vehicle.Make,
                    Model = m.Vehicle.Model,
                    ProductionYear = m.Vehicle.ProductionYear,
                    Category = m.Category.Name,
                    Price = m.Vehicle.Price,
                    ImageUrl = m.Vehicle.ImageUrl,
                    Seller = m.Seller!.UserName
                })
                .ToListAsync();
        }
    }
}