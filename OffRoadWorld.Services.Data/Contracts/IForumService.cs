﻿using OffRoadWorld.Web.ViewModels.Forum;

namespace OffRoadWorld.Services.Data.Contracts
{
    public interface IForumService
    {
        Task<List<ForumViewModel>> GetAllForumCategoriesAsync();

        Task<List<TopicViewModel>> GetAllTopicsFromCategoryAsync(int categoryId);

        Task<PostViewModel> GetAllPostsAsync(Guid topicId);

        Task CreateCategoryAsync(CategoryFormModel model);
        
        Task AddTopicAsync(TopicFormModel model);

        Task AddReplyAsync(Guid topicId, string userId, string reply);

        Task UpdatePostContentAsync(Guid id, string content);

        Task UpdateTopicAsync(Guid id, string content);

        Task DeletePost(Guid id);

        Task DeleteTopic(Guid id);
    }
}