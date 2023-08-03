using Microsoft.AspNetCore.Mvc;
using OffRoadWorld.Services.Data.Contracts;
using OffRoadWorld.Web.ViewModels.Forum;
using System.Security.Claims;

namespace OffRoadWorld.Web.Controllers
{
    public class ForumController : BaseController
    {
        private readonly IForumService forumService;

        private async Task<ICollection<TopicViewModel>> GetItemsForPage(int categoryId, int page, int itemsPerPage)
        {
            var allListings = await forumService.GetAllTopicsFromCategoryAsync(categoryId);

            int startIndex = (page - 1) * itemsPerPage;
            var listings = allListings
                .Skip(startIndex)
                .Take(itemsPerPage)
                .ToList();

            return listings;
        }

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

        public async Task<IActionResult> Topic(int categoryId, int page = 1, int pageSize = 6)
        {
            TempData["CategoryId"] = categoryId;

            var allTopics = await forumService.GetAllTopicsFromCategoryAsync(categoryId);
            
            int topics = allTopics.Count();

            var model = await GetItemsForPage(categoryId, page, pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)topics / pageSize);

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
        public async Task<IActionResult> UpdatePostContent(Guid postId, string content)
        {
            await forumService.UpdatePostContentAsync(postId, content);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(Guid postId)
        {
            await forumService.DeletePost(postId);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTopic(Guid topicId)
        {
            await forumService.DeleteTopic(topicId);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTopicContent(Guid topicId, string content)
        {
            await forumService.UpdateTopicAsync(topicId, content);

            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTopic(TopicFormModel model, int categoryId)
        {
            model.OwnerId = GetUserId();
            model.ForumId = categoryId;

            await forumService.AddTopicAsync(model);

            return Redirect("Topic?categoryId=" + categoryId);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReply()
        {
            var topicId = Guid.Parse(Request.Form["topicId"]);

            await forumService.AddReplyAsync(topicId, GetUserId(), Request.Form["reply"]);

            return RedirectToAction("Post", new { topicId = topicId });
        }
    }
}