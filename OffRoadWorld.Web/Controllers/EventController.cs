﻿using Microsoft.AspNetCore.Mvc;
using OffRoadWorld.Services.Data.Contracts;
using OffRoadWorld.Web.ViewModels.Event;
using System.Security.Claims;
using static OffRoadWorld.Common.NotificationMessages;

namespace OffRoadWorld.Web.Controllers
{
    public class EventController : BaseController
    {
        private readonly IEventService eventService;

        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
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
                TempData[ErrorMessage] = "You cannot edit this event because you are not the owner!";
                return RedirectToAction(nameof(All));
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EventFormModel model)
        {
            if (!GetUserId().Equals(model.OwnerId))
            {
                return RedirectToAction(nameof(All));
            }

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
                TempData[ErrorMessage] = "You cannot delete this event because you are not the owner!";
                return RedirectToAction(nameof(All));
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EventDetailsViewModel model)
        {
            //TODO: Add validation.
            await eventService.DeleteEventAsync(model.Id);
            TempData[SuccessMessage] = $"You have successfully deleted this event!";

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Join(int id)
        {
            var _event = eventService.GetDetailsById(id);

            var model = await eventService.GetJoinedEventsAsync(GetUserId());

            if (model.Any(m => m.Id == id))
            {
                TempData[WarningMessage] = "You are already participating in this event!";
                return RedirectToAction(nameof(All));
            }

            if (_event.Owner == User.Identity!.Name)
            {
                TempData[WarningMessage] = "You cannot participate in this event because you are the owner!";
                return RedirectToAction(nameof(All));
            }

            await eventService.JoinEventAsync(GetUserId(), _event);
            TempData[SuccessMessage] = $"You have successfully joined the {_event.Title} event!";

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> MyEvents()
        {
            var model = await eventService.GetMyEventsAsync(GetUserId());

            return View(model);
        }
    }
}