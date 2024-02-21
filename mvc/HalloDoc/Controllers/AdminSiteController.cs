using Microsoft.AspNetCore.Mvc;

namespace HalloDoc.Controllers
{
    public class AdminSiteController : Controller
    {
        public IActionResult AdminDashBoard()
        {
            return View();
        }
    }
}
