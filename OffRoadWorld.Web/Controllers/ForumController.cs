using Microsoft.AspNetCore.Mvc;
using OffRoadWorld.Services.Data.Contracts;

namespace OffRoadWorld.Web.Controllers
{
    public class ForumController : BaseController
    {
        private readonly IForumService forumService;

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
            var model = await forumService.GetAllTopicsFromCategoryAsync(categoryId);

            return View(model);
        }
    }
}