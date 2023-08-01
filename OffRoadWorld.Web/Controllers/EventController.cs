using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using OffRoadWorld.Data.Models;
using OffRoadWorld.Services.Data.Contracts;
using OffRoadWorld.Web.ViewModels.Event;
using System.Security.Claims;
using static OffRoadWorld.Common.NotificationMessages;

namespace OffRoadWorld.Web.Controllers
{
    public class EventController : BaseController
    {
        private readonly IEventService eventService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IStringLocalizer<EventController> localizer;

        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);

        public EventController(IEventService eventService, UserManager<ApplicationUser> userManager,
            IStringLocalizer<EventController> localizer)
        {
            this.eventService = eventService;
            this.userManager = userManager;
            this.localizer = localizer;
        }

        public async Task<IActionResult> All()
        {
            var model = await eventService.GetAllEventsAsync();
    
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new EventFormModel()
            {
                Categories = await eventService.GetAllCategoriesAsync()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(EventFormModel model)
        {
            var categories = await eventService.GetAllCategoriesAsync();

            model.OwnerId = GetUserId();

            await eventService.CreateEventAsync(model);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await eventService.GetEventFormModelAsync(id);

            if (!GetUserId().Equals(model.OwnerId))
            {
                TempData[ErrorMessage] = localizer["You cannot edit this event because you are not the owner!"].ToString();
                return RedirectToAction(nameof(All));
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EventFormModel model)
        {
            await eventService.EditEventAsync(id, model);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Details(int id)
        {
            var model = eventService.GetDetailsById(id);

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var model = eventService.GetDetailsById(id);

            if (!User.Identity!.Name!.Equals(model.Owner))
            {
                TempData[ErrorMessage] = localizer["You cannot delete this event because you are not the owner!"].ToString();
                return RedirectToAction(nameof(All));
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EventDetailsViewModel model)
        {
            var _event = eventService.GetDetailsById(model.Id);

            await eventService.DeleteEventAsync(model.Id);

            TempData[SuccessMessage] = localizer["You have successfully deleted {0} event!", _event.Title].ToString();

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Join(int id)
        {
            var _event = eventService.GetDetailsById(id);

            var user = await userManager.GetUserAsync(User);

            var model = await eventService.GetJoinedEventsAsync(GetUserId());

            var usersVehicle = await eventService.GetVehicleAsync(GetUserId(), _event.CategoryId);

            if (model.Any(m => m.Id == id))
            {
                TempData[WarningMessage] = localizer["You are already participating in this event!"].ToString();
                return RedirectToAction(nameof(All));
            }

            if (_event.Owner == User.Identity!.Name)
            {
                TempData[WarningMessage] = localizer["You cannot participate in this event because you are the owner!"].ToString();
                return RedirectToAction(nameof(All));
            }

            if (usersVehicle == null)
            {
                TempData[WarningMessage] = localizer["You need to own {0} vehicle to participate in {1}!", _event.Category, _event.Title].ToString();
                return RedirectToAction(nameof(All));
            }

            await eventService.JoinEventAsync(GetUserId(), _event);
            TempData[SuccessMessage] = localizer["You have successfully joined the {0} event!", _event.Title].ToString();

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> MyEvents()
        {
            var model = await eventService.GetMyEventsAsync(GetUserId());

            return View(model);
        }

        public async Task<IActionResult> LeaveEvent(int id)
        {
            string referrerUrl = Request.Headers["Referer"].ToString();

            var _event = eventService.GetDetailsById(id);

            await eventService.LeaveEventAsync(GetUserId(), _event);

            TempData[SuccessMessage] = localizer["You have left {0} event!", _event.Title].ToString();

            return Redirect(referrerUrl);
        }

        public async Task<IActionResult> Joined()
        {
            var model = await eventService.GetJoinedEventsAsync(GetUserId());

            return View(model);
        }
    }
}