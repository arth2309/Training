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
                return View(null);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreatePatientRequest(AddPatientRequest user)
        {
           
                AspNetUser user1 = new()
                {
                    UserName = user.FirstName,
                    PasswordHash = "abc@123",
                    Email = user.Email,
                    PhoneNumber = user.Mobile,
                    Ip = "192.168.0.2",
                    CreatedDate = DateTime.Now


                };

                _dbContext.AspNetUsers.Add(user1);
                await _dbContext.SaveChangesAsync();
                user1 = _dbContext.AspNetUsers.FirstOrDefault(a => a.Email == user.Email);

                User user2 = new()
                {
                    AspNetUserId = user1.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Mobile = user.Mobile,
                    Street = user.Street,
                    City = user.City,
                    State = user.State,
                    ZipCode = user.ZipCode,
                    CreatedBy = user1.Id,
                    CreatedDate = DateTime.Now


                };
                _dbContext.Users.Add(user2);
                await _dbContext.SaveChangesAsync();
                user2 = _dbContext.Users.FirstOrDefault(a => a.Email == user.Email);

               

                Request user3 = new()
                {
                    RequestTypeId = 1,
                    UserId = user2.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.Mobile,
                    CreatedDate = DateTime.Now,
                    Symptoms = user.symptoms,
                    Roomsuite=user.roomsuite,
                    Status=1
                    

                };
                _dbContext.Requests.Add(user3);
                await _dbContext.SaveChangesAsync();
                user3 = _dbContext.Requests.FirstOrDefault(a => a.Email == user.Email);



                return RedirectToAction("Index", "AspNetUsers");
            
            


        }
        [HttpPost]
        public async Task<IActionResult> CreateConciergeRequest(AddConciergeValidation user)
        {
            
                if(ModelState.IsValid)
            {
                AspNetUser user1 = new()
                {
                    UserName = user.FirstName,
                    PasswordHash = "abc@123",
                    Email = user.Email,
                    PhoneNumber = user.Mobile,
                    Ip = "192.168.0.2",
                    CreatedDate = DateTime.Now


                };

                _dbContext.AspNetUsers.Add(user1);
                await _dbContext.SaveChangesAsync();
                user1 = _dbContext.AspNetUsers.FirstOrDefault(a => a.Email == user.Email);

                User user2 = new()
                {
                    AspNetUserId = user1.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Mobile = user.Mobile,
                    Street = user.Street,
                    City = user.City,
                    State = user.State,
                    ZipCode = user.ZipCode,
                    CreatedBy = user1.Id,
                    CreatedDate = DateTime.Now


                };
                _dbContext.Users.Add(user2);
                await _dbContext.SaveChangesAsync();
                user2 = _dbContext.Users.FirstOrDefault(a => a.Email == user.Email);



                Request user3 = new()
                {
                    RequestTypeId = 3,
                    UserId = user2.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.Mobile,
                    CreatedDate = DateTime.Now,
                    Symptoms = user.symptoms,
                    Roomsuite = user.roomsuite,
                    Status = 1


                };
                _dbContext.Requests.Add(user3);
                await _dbContext.SaveChangesAsync();
                user3 = _dbContext.Requests.FirstOrDefault(a => a.Email == user.CEmail);

                Concierge user4 = new()
                {
                    ConciergeName = user.CFirstName + " " + user.CLastName,
                    Street = user.Street,
                    City = user.City,
                    State = user.State,
                    ZipCode = user.ZipCode,
                    CreatedDate = DateTime.Now
                    

                };
                _dbContext.Concierges.Add(user4);
                await _dbContext.SaveChangesAsync();


                RequestConcierge user5 = new()
                {

                    RequestId = user3.RequestId,
                    ConciergeId = user4.ConciergeId
                    

                };
                _dbContext.RequestConcierges.Add(user5);
                await _dbContext.SaveChangesAsync();
               

                return RedirectToAction("Index", "AspNetUsers");



            }
                else
            {
                return View(null);
            }

        }

    }
}

