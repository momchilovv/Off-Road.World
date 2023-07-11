using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using OffRoadWorld.Services.Data.Contracts;
using OffRoadWorld.Web.ViewModels.Marketplace;
using OffRoadWorld.Web.ViewModels.Vehicle;
using System.Security.Claims;
using static OffRoadWorld.Common.NotificationMessages;

namespace OffRoadWorld.Web.Controllers
{
    public class MarketplaceController : BaseController
    {
        private readonly IMarketplaceService marketplaceService;
        private readonly IStringLocalizer<MarketplaceController> localizer;

        private async Task<ICollection<MarketplaceViewModel>> GetItemsForPage(int page, int itemsPerPage)
        {
            var allListings = await marketplaceService.GetAllListingsAsync();

            int startIndex = (page - 1) * itemsPerPage;
            var listings = allListings
                .Skip(startIndex)
                .Take(itemsPerPage)
                .ToList();

            return listings;
        }

        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);

        public MarketplaceController(IMarketplaceService marketplaceService,
            IStringLocalizer<MarketplaceController> localizer)
        {
            this.marketplaceService = marketplaceService;
            this.localizer = localizer;
        }

        public async Task<IActionResult> Index(string search, int page = 1, int pageSize = 8)
        {
            if (!string.IsNullOrEmpty(search))
            {
                ViewBag.SearchQuery = search;

                var model = await marketplaceService.GetAllListingsBySearchAsync(search);

                return View(model);
            }
            else
            {
                var vehicles = await marketplaceService.GetAllListingsAsync();

                int listings = vehicles.Count();

                var model = await GetItemsForPage(page, pageSize);

                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = (int)Math.Ceiling((double)listings / pageSize);

                return View(model);
            }
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var model = await marketplaceService.GetVehicleByIdAsync(id);

            return View(model);
        }

        public async Task<IActionResult> Buy(Guid id)
        {
            try
            {
                var vehicle = await marketplaceService.GetVehicleByIdAsync(id);

                if (vehicle == null)
                {
                    TempData[ErrorMessage] = localizer["The vehicle you are trying to buy does not exist!"].ToString();
                    return RedirectToAction(nameof(Index));
                }

                if (vehicle.OwnerId == GetUserId())
                {
                    TempData[WarningMessage] = localizer["You already own this vehicle!"].ToString();
                    return RedirectToAction("MyVehicles", "Vehicle");
                }

                await marketplaceService.BuyVehicleAsync(GetUserId(), id);

                TempData[SuccessMessage] = localizer["Successfully bought {0} {1} for ${2}.",
                                                    vehicle.Make, vehicle.Model, vehicle.Price.ToString("f2")].ToString();

                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException)
            {
                TempData[WarningMessage] = localizer["You don't have enough money to buy this vehicle!"].ToString();
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Sell(Guid id)
        {
            var model = await marketplaceService.GetVehicleByIdAsync(id);

            if (model == null)
            {
                TempData[ErrorMessage] = localizer["The vehicle you are trying to sell does not exist!"].ToString();
                return RedirectToAction(nameof(Index));
            }

            if (model!.OwnerId != GetUserId())
            {
                TempData[ErrorMessage] = localizer["You are not the owner of this vehicle."].ToString();
                return RedirectToAction(nameof(Index));
            }

            if (marketplaceService.GetAllListingsAsync().Result.Any(l => l.VehicleId == model.Id))
            {
                TempData[ErrorMessage] = localizer["Your {0} {1} is already listed for sale.",
                                                    model.Make, model.Model].ToString();

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
                TempData[ErrorMessage] = localizer["You are not the owner of this vehicle."].ToString();

                return RedirectToAction(nameof(Index));
            }

            await marketplaceService.AddListingAsync(id, vehicle);

            TempData[SuccessMessage] = localizer["{0} {1} was successfully added for selling in " +
                "                                   Marketplace for ${2}.", listing.Make, listing.Model, listing.Price.ToString("f2")].ToString();
            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Remove(Guid id)
        {
            var model = await marketplaceService.GetVehicleByIdAsync(id);

            if (model.OwnerId != GetUserId())
            {
                TempData[ErrorMessage] = localizer["You are not the owner of this vehicle."].ToString();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(Guid id, VehicleViewModel vehicle)
        {
            var model = await marketplaceService.GetVehicleByIdAsync(id);

            if (model.OwnerId != GetUserId())
            {
                TempData[ErrorMessage] = localizer["You are not the owner of this vehicle."].ToString();
                return RedirectToAction(nameof(Index));
            }

            TempData[SuccessMessage] = localizer["You have successfully removed your {0} {1} from the Marketplace!", 
                                                        model.Make, model.Model].ToString();
            await marketplaceService.RemoveListingAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}