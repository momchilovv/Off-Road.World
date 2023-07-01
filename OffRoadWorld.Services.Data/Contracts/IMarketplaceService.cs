using OffRoadWorld.Web.ViewModels.Marketplace;
using OffRoadWorld.Web.ViewModels.Vehicle;

namespace OffRoadWorld.Services.Data.Contracts
{
    public interface IMarketplaceService
    {
        Task<ICollection<MarketplaceViewModel>> GetAllListingsAsync();

        Task BuyVehicleAsync(string userId, Guid vehicleId);

        Task<VehicleViewModel> GetVehicleByIdAsync(Guid vehicleId);

        Task<ICollection<MarketplaceViewModel>> GetAllListingsBySearchAsync(string search);

        Task AddListingAsync(Guid id, VehicleViewModel model);
    }
}