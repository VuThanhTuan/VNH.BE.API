using Microsoft.AspNetCore.Mvc;

namespace VNH.BE.API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return new RedirectResult("swagger");
        }
    }
}