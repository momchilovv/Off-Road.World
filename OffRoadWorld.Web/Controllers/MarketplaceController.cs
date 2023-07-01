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

        public async Task<IActionResult> Index(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                ViewBag.SearchQuery = search;

                var model = await marketplaceService.GetAllListingsBySearchAsync(search);

                return View(model);
            }
            else
            {
                var model = await marketplaceService.GetAllListingsAsync();
                return View(model);
            }
        }

        public async Task<IActionResult> Buy(Guid id)
        {
            var vehicle = await marketplaceService.GetVehicleByIdAsync(id);

            if (vehicle == null)
            {
                TempData[ErrorMessage] = "The vehicle you are trying to buy does not exist!";
                return RedirectToAction(nameof(Index));
            }

            if (vehicle.OwnerId == GetUserId())
            {
                TempData[WarningMessage] = "You already own this vehicle!";
                return RedirectToAction("MyVehicles", "Vehicle");
            }

            await marketplaceService.BuyVehicleAsync(GetUserId(), id);

            TempData[SuccessMessage] = $"Successfully bought {vehicle.Make} {vehicle.Model} for ${vehicle.Price}.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Sell(Guid id)
        {
            var model = await marketplaceService.GetVehicleByIdAsync(id);

            if (model == null)
            {
                TempData[ErrorMessage] = "The vehicle you are trying to sell does not exist!";
                return RedirectToAction(nameof(Index));
            }

            if (model!.OwnerId != GetUserId())
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
            var listing = await marketplaceService.GetVehicleByIdAsync(vehicle.Id);

            if (listing!.OwnerId != GetUserId())
            {
                TempData[ErrorMessage] = "You are not the owner of this vehicle.";
                return RedirectToAction(nameof(Index));
            }

            await marketplaceService.AddListingAsync(id, vehicle);

            TempData[SuccessMessage] = $"{listing.Make} {listing.Model} was successfully added for selling in Marketplace for ${listing.Price}.";
            return RedirectToAction(nameof(Index));
        }
    }
}