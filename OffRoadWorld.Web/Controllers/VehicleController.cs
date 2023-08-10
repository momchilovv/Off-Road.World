using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OffRoadWorld.Services.Data.Contracts;
using OffRoadWorld.Web.ViewModels.Vehicle;
using System.Security.Claims;

namespace OffRoadWorld.Web.Controllers
{
    public class VehicleController : BaseController
    {
        private readonly IVehicleService vehicleService;

        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);

        public VehicleController(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new VehicleFormModel()
            {
                Categories = await vehicleService.GetAllCategoriesAsync()
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
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