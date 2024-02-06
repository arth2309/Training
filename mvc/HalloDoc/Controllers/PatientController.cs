using HalloDoc.DataContext;
using HalloDoc.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace HalloDoc.Controllers
{
    public class PatientController : Controller
    {

        private readonly ApplicationDBContext _dbContext;

        public PatientController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PatientLogin()
        {
            return View();
        }
        public IActionResult SubmitRequest()
        {
            return View();
        }
        public IActionResult PatientForgotPassword()
        {
            return View();
        }
        public IActionResult CreatePatientRequest()
        {
            return View();
        }
        public IActionResult CreateFamilyFriendRequest()
        {
            return View();
        }
        public IActionResult CreateConciergeRequest()
        {
            return View();
        }
        public IActionResult CreateBusinessRequest()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PatientLogin(AspNetUser user)
        {
            var userFromDb = _dbContext.AspNetUsers.FirstOrDefault(a => a.Username == user.Username);
            if (userFromDb != null && userFromDb.PasswordHash == user.PasswordHash)
            {
                return View(userFromDb);
            }
            else
            {
                return View(null);
            }
        }
    }
}

