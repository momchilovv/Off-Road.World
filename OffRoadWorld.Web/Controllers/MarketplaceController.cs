using Microsoft.AspNetCore.Mvc;
using OffRoadWorld.Services.Data.Contracts;
using OffRoadWorld.Web.ViewModels.Vehicle;
using System.Security.Claims;
using static OffRoadWorld.Common.NotificationMessages;

namespace OffRoadWorld.Web.Controllers
{
    public class MarketplaceController : BaseController
    {
        private readonly IMarketplaceService marketplaceService;

        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);

        public MarketplaceController(IMarketplaceService marketplaceService)
        {
            this.marketplaceService = marketplaceService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await marketplaceService.GetAllListingsAsync();

            return View(model);
        }

        public async Task<IActionResult> Buy(Guid id)
        {
            await marketplaceService.BuyVehicleAsync(GetUserId(), id);

            var vehicle = await marketplaceService.GetVehicleByIdAsync(id);

            TempData[SuccessMessage] = $"Successfully bought {vehicle.Make} {vehicle.Model} for ${vehicle.Price}.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Sell(Guid id)
        {
            var model = await marketplaceService.GetVehicleByIdAsync(id);

            if (model.OwnerId != GetUserId())
            {
                TempData[ErrorMessage] = "You are not the owner of this vehicle.";
                return RedirectToAction(nameof(Index));
            }

            if (marketplaceService.GetAllListingsAsync().Result.Any(l => l.VehicleId == model.Id))
            {
                TempData[ErrorMessage] = $"Your {model.Make} {model.Model} is already listed for sale.";
                return RedirectToAction("MyVehicles", "Vehicle");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sell(Guid id, VehicleViewModel vehicle)
        {
            await marketplaceService.AddListingAsync(id, vehicle);

            var listing = await marketplaceService.GetVehicleByIdAsync(vehicle.Id);

            TempData[SuccessMessage] = $"{listing.Make} {listing.Model} was successfully added for selling in Marketplace for ${listing.Price}.";

            return RedirectToAction(nameof(Index));
        }
    }
}