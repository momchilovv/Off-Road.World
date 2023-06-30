using Microsoft.AspNetCore.Mvc;
using OffRoadWorld.Services.Data.Contracts;
using System.Security.Claims;

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

            return RedirectToAction(nameof(Index));
        }
    }
}