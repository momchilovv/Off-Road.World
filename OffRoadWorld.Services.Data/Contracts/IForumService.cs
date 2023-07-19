using OffRoadWorld.Web.ViewModels.Forum;

namespace OffRoadWorld.Services.Data.Contracts
{
    public interface IForumService
    {
        Task<List<ForumViewModel>> GetAllForumCategoriesAsync();

        Task<List<TopicViewModel>> GetAllTopicsFromCategoryAsync(int categoryId);
    }
}