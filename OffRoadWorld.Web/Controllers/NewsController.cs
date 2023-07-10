using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NewsAPI;
using OffRoadWorld.Web.ViewModels.News;

namespace OffRoadWorld.Web.Controllers
{
    public class NewsController : BaseController
    {
        private const int NumbersOfArticlesToDisplay = 0;

        private readonly IStringLocalizer<NewsController> localizer;

        public NewsController(IStringLocalizer<NewsController> localizer)
        {
            this.localizer = localizer;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            int displayedArticles = 0;

            var allNews = new NewsApiClient("");

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

        [AllowAnonymous]
        public IActionResult SetCulture(string id = "en")
        {
            string culture = id;
            Response.Cookies.Append(
               CookieRequestCultureProvider.DefaultCookieName,
               CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
               new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
           );

            ViewData["Message"] = "Culture set to " + culture;

            return View("About");
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}