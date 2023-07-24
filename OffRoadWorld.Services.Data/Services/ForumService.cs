using Microsoft.EntityFrameworkCore;
using OffRoadWorld.Data;
using OffRoadWorld.Data.Models;
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
                    Forum = t.Forum.Title,
                    ForumId = t.ForumId
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

        public async Task<PostViewModel> GetAllPostsAsync(Guid topicId)
        {
            return await context.Topics
                .Where(t => t.Id == topicId)
                .Select(t => new PostViewModel
                {
                    Id = t.Id,
                    Topic = new Topic
                    {
                        Id = topicId,
                        Title = t.Title,
                        Content = t.Content
                    },
                    Posts = t.Posts
                })
                .FirstAsync();
        }

        public async Task CreateCategoryAsync(CategoryFormModel model)
        {
            var category = new Forum()
            {
                Title = model.Title,
                Description = model.Description
            };

            await context.Forums.AddAsync(category);
            await context.SaveChangesAsync();
        }

        public async Task AddTopicAsync(TopicFormModel model)
        {
            var topic = new Topic()
            {
                Title = model.Title,
                Content= model.Content,
                CreatedAt = DateTime.UtcNow,
                OwnerId = model.OwnerId,
                ForumId = model.ForumId
            };

            await context.Topics.AddAsync(topic);
            await context.SaveChangesAsync();
        }

        public async Task AddReplyAsync(Guid topicId, string userId, string reply)
        {
            var post = new Post()
            {
                Content = reply,
                OwnerId = userId,
                TopicId = topicId,
                CreatedAt = DateTime.UtcNow
            };

            await context.Posts.AddAsync(post);
            await context.SaveChangesAsync();
        }
    }
}