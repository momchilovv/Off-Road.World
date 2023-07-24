using Microsoft.AspNetCore.Mvc;
using OffRoadWorld.Services.Data.Contracts;
using OffRoadWorld.Web.ViewModels.Forum;
using System.Security.Claims;

namespace OffRoadWorld.Web.Controllers
{
    public class ForumController : BaseController
    {
        private readonly IForumService forumService;

        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);

        public ForumController(IForumService forumService)
        {
            this.forumService = forumService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await forumService.GetAllForumCategoriesAsync();

            return View(model);
        }

        public async Task<IActionResult> Topic(int categoryId)
        {
            TempData["CategoryId"] = categoryId;

            var model = await forumService.GetAllTopicsFromCategoryAsync(categoryId);

            return View(model);
        }

        public async Task<IActionResult> Post(Guid topicId)
        {
            var model = await forumService.GetAllPostsAsync(topicId);

            return View(model);
        }

        //ADMIN ONLY
        [HttpGet]
        public IActionResult AddCategory()
        {
            var model = new CategoryFormModel();

            return View(model);
        }

        //ADMIN ONLY
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCategory(CategoryFormModel model)
        {
            await forumService.CreateCategoryAsync(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult AddTopic(int categoryId)
        {
            var model = new TopicFormModel()
            {
                ForumId = categoryId
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTopic(TopicFormModel model, int categoryId)
        {
            model.OwnerId = GetUserId();
            model.ForumId = categoryId;

            await forumService.AddTopicAsync(model);

            return RedirectToAction(nameof(Index));
        }
    }
}