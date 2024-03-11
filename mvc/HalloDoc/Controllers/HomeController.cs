using Microsoft.AspNetCore.Mvc;

namespace HalloDoc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
