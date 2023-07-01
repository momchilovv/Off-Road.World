using Microsoft.AspNetCore.Mvc;
using OffRoadWorld.Services.Data.Contracts;
using OffRoadWorld.Services.Data.Services;
using OffRoadWorld.Web.ViewModels.Vehicle;
using System.Security.Claims;
using static OffRoadWorld.Common.NotificationMessages;

namespace OffRoadWorld.Web.Controllers
{
    public class VehicleController : BaseController
    {
        private readonly IVehicleService vehicleService;
        private readonly IMarketplaceService marketplaceService;

        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);

        public VehicleController(IVehicleService vehicleService, IMarketplaceService marketplaceService)
        {
            this.vehicleService = vehicleService;
            this.marketplaceService = marketplaceService;
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var model = await marketplaceService.GetVehicleByIdAsync(id);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new VehicleFormModel()
            {
                Categories = await vehicleService.GetAllCategoriesAsync()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(VehicleFormModel vehicle)
        {
            vehicle.OwnerId = GetUserId();
            await vehicleService.AddVehicleAsync(vehicle);

            return RedirectToAction("All", "Event");
        }

        public async Task<IActionResult> MyVehicles()
        {
            var model = await vehicleService.GetMyVehiclesAsync(GetUserId());

            return View(model);
        }
    }
}