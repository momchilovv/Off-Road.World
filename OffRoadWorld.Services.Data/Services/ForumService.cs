using Microsoft.EntityFrameworkCore;
using OffRoadWorld.Data;
using OffRoadWorld.Services.Data.Contracts;
using OffRoadWorld.Web.ViewModels.Forum;

namespace OffRoadWorld.Services.Data.Services
{
    public class ForumService : IForumService
    {
        private readonly OffRoadWorldDbContext context;

        public ForumService(OffRoadWorldDbContext context)
        {
            this.context = context;
        }

        public async Task<List<TopicViewModel>> GetAllTopicsFromCategoryAsync(int categoryId)
        {
            return await context.Topics
                .Where(t => t.ForumId == categoryId)
                .Select(t => new TopicViewModel
                {
                    Id = t.Id,
                    Title = t.Title,
                    Content = t.Content,
                    CreatedAt = t.CreatedAt,
                    Owner = t.Owner,
                    Forum = t.Forum.Title
                })
                .ToListAsync();
        }

        public async Task<List<ForumViewModel>> GetAllForumCategoriesAsync()
        {
            return await context.Forums
                .Select(f => new ForumViewModel
                {
                    Id = f.Id,
                    Title = f.Title,
                    Description = f.Description,
                    TopicsCount = f.TopicsCount.Count()
                })
                .ToListAsync();
        }
    }
}