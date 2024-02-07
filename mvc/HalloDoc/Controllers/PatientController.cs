using HalloDoc.DataContext;
using HalloDoc.DataModels;
using HalloDoc.ModelView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HalloDoc.Controllers
{
    public class PatientController : Controller
    {

        private readonly HalloDoc.DataContext.ApplicationDbContext _dbContext;

        public PatientController(HalloDoc.DataContext.ApplicationDbContext dbContext)
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
        public IActionResult PatientLogin(PatientLoginValidation user)
        {
            var userFromDb = _dbContext.AspNetUsers.FirstOrDefault(a => a.Email == user.Email);
            if (userFromDb != null && userFromDb.PasswordHash == user.PasswordHash)
            {
                return RedirectToAction("SubmitRequest");
            }
            else
            {
                return RedirectToAction("PatientLogin");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreatePatientRequest(User user)
        {
            User user1 = new()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                IntDate = user.IntDate,
                Email = user.Email,
                Mobile = user.Mobile,
                Street = user.Street,
                City = user.City,
                State = user.State,
                ZipCode = user.ZipCode,
                CreatedBy = "arth"
            };

            _dbContext.Users.Add(user1);
            await _dbContext.SaveChangesAsync();
            user1 = _dbContext.Users.FirstOrDefault(a => a.Email == user.Email);
            return RedirectToAction("Index", "Users");


        }
        
    }
}

