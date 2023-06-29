using OffRoadWorld.Web.ViewModels.Category;
using OffRoadWorld.Web.ViewModels.Vehicle;

namespace OffRoadWorld.Services.Data.Contracts
{
    public interface IVehicleService
    {
        Task AddVehicleAsync(VehicleFormModel model);

        Task<ICollection<CategoryModel>> GetAllCategoriesAsync();

        Task<ICollection<VehicleViewModel>> GetAllVehiclesAsync();

        Task<ICollection<VehicleViewModel>> GetMyVehiclesAsync(string userId);
    }
}