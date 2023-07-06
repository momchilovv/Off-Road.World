using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsAPI;
using OffRoadWorld.Web.ViewModels.News;

namespace OffRoadWorld.Web.Controllers
{
    public class NewsController : BaseController
    {
        private const int NumbersOfArticlesToDisplay = 8;

        [AllowAnonymous]
        public IActionResult Index()
        {
            int displayedArticles = 0;

            var allNews = new NewsApiClient("Insert your newsapi.org API key here.");

            var response = allNews.GetEverything(new NewsAPI.Models.EverythingRequest
            {
                Q = "offroad enduro",
                Language = NewsAPI.Constants.Languages.EN
            });

            var listed = new List<NewsViewModel>();

            foreach (var news in response.Articles) 
            {
                if (displayedArticles == NumbersOfArticlesToDisplay)
                {
                    break;
                }

                listed.Add(new NewsViewModel()
                {
                    Title = news.Title,
                    Author = news.Author,
                    Description = news.Description,
                    Url = news.Url,
                    PublishedAt = news.PublishedAt
                });

                displayedArticles++;
            }

            return View(listed);
        }
    }
}