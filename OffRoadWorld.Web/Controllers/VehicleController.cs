using Microsoft.AspNetCore.Mvc;
using OffRoadWorld.Services.Data.Contracts;
using OffRoadWorld.Web.ViewModels.Vehicle;
using System.Security.Claims;
using static OffRoadWorld.Common.NotificationMessages;

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

        public async Task<IActionResult> All()
        {
            var model = await vehicleService.GetAllVehiclesAsync();

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