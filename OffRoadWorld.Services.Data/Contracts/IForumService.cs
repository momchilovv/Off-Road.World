using OffRoadWorld.Web.ViewModels.Forum;

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
    }
}