using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OffRoadWorld.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {

    }
}