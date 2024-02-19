using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Runtime.Intrinsics.X86;
using HalloDoc.DataContext;
using HalloDoc.DataModels;
using HalloDoc.ModelView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using HalloDoc.Repositories.ModelView;
using HallodocServices.Interfaces;


namespace HalloDoc.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientLoginServices _loginServices;
        
        private readonly HalloDoc.DataContext.ApplicationDbContext _dbContext;

        public PatientController(HalloDoc.DataContext.ApplicationDbContext dbContext, IPatientLoginServices loginServices)
        {
            _dbContext = dbContext;
            _loginServices = loginServices;
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

        [HttpGet]
        public IActionResult CheckEmailExists(string email)
        {
            bool emailId = _dbContext.AspNetUsers.ToList().Exists(a => a.Email == email);
            return Json(emailId);
        }





        public IActionResult PatientDashBoard()
        {
            int id = Int32.Parse(Request.Cookies["id"]);
            //User users = _dbContext.Users.FirstOrDefault(b => b.AspNetUserId == id);
            //Request req = _dbContext.Requests.FirstOrDefault(a => a.UserId == users.UserId);
            //var request = _dbContext.Requests.ToList();
            //var request1 = request.Where(r => r.UserId == users.UserId);
            //var list = _dbContext.RequestWiseFiles.ToList();
            //var doc = list.Count(a => a.RequestId == req.RequestId);

            //var requestview = request1.Select(p => new ShowDetails
            //{
            //    RequestId = p.RequestId,
            //    FirstName = p.FirstName,
            //    LastName = p.LastName,
            //    CreatedDate = p.CreatedDate,
            //    Status = p.Status,
            //    count = doc

            //}

            //);
            User user2 = _dbContext.Users.FirstOrDefault(a => a.AspNetUserId == id);
            List<Request> userrequest = _dbContext.Requests.Where(a => a.UserId == user2.UserId).ToList();
            List<ShowDetails> dashboard = new List<ShowDetails>();
            for (int i = 0; i < userrequest.Count; i++)
            {
                List<RequestWiseFile> requestWiseFiles = _dbContext.RequestWiseFiles.Where(a => a.RequestId == userrequest[i].RequestId).ToList();
                ShowDetails dashboard1 = new()
                {
                    RequestId = userrequest[i].RequestId,
                    FirstName = userrequest[i].FirstName,
                    LastName = userrequest[i].LastName,
                    Status = userrequest[i].Status,
                    CreatedDate = userrequest[i].CreatedDate,
                    count = requestWiseFiles.Count,
                };
                dashboard.Add(dashboard1);
            }


            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Secure = true;
            cookieOptions.Expires = DateTime.Now.AddMinutes(30);
            Response.Cookies.Append("UserId", user2.UserId.ToString(), cookieOptions);

            //CookieOptions cookieOptions1 = new CookieOptions();
            //cookieOptions1.Secure = true;
            //cookieOptions1.Expires = DateTime.Now.AddMinutes(30);
            //Response.Cookies.Append("RequestId",req.RequestId.ToString(), cookieOptions1);




            return View("PatientDashboard", dashboard);

        }
        public IActionResult PatientDashBoardDoc(int id)
        {
           //int id = Int32.Parse(Request.Cookies["MyCookie"]);
            List<RequestWiseFile> requestWiseFiles = _dbContext.RequestWiseFiles.Where(a => a.RequestId == id).ToList();
            
            List<ShowDocuments> showDocuments = new List<ShowDocuments>();
            for(int i=0;i<requestWiseFiles.Count;i++) 
            {
                String uploader;
                Request request = _dbContext.Requests.FirstOrDefault(a => a.RequestId == id);
                uploader = request.FirstName + request.LastName;
                //RequestWiseFile requestWiseFile = _dbContext.RequestWiseFiles.FirstOrDefault(a => a.RequestId == request.RequestId);
                RequestWiseFile requestWiseFile = requestWiseFiles[i];
                ShowDocuments showDocuments1 = new()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    FileName = Path.GetFileName(requestWiseFile.FileName),
                    uploader = uploader,
                    UploadDate = requestWiseFile.CreatedDate

                };
                showDocuments.Add( showDocuments1 );

            }

            return View(showDocuments);
        }

        public IActionResult UserProfile()
        {
            int id = Int32.Parse(Request.Cookies["UserId"]);
            
            
            User user = _dbContext.Users.FirstOrDefault(a => a.UserId == id);
            ShowProfile showProfile = new()
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Mobile = user.Mobile,
                Street = user.Street,   
                City = user.City,
                State = user.State,
                ZipCode = user.ZipCode
            };




            return View("UserProfile", showProfile);
        }

        [HttpPost]
        public async Task <IActionResult> UserProfile(ShowProfile up)
        {
            
            User user2 = _dbContext.Users.FirstOrDefault(a => a.UserId == up.UserId);
            user2.FirstName = up.FirstName;
            
            user2.LastName = up.LastName;
            

            user2.Mobile = up.Mobile;
            user2.Street = up.Street;
            user2.City = up.City;
            user2.State = up.State;
            user2.ZipCode = up.ZipCode;
            _dbContext.Users.Update(user2);
           await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index","Users");
        }

        public IActionResult SubmitMe()
        {
            int id = Int32.Parse(Request.Cookies["UserId"]);


            User user = _dbContext.Users.FirstOrDefault(a => a.UserId == id);
            SubmitMe showProfile = new()
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Mobile = user.Mobile,
                Street = user.Street,
                City = user.City,
                State = user.State,
                ZipCode = user.ZipCode
            };

            return View(showProfile);
        }

        public IActionResult SubmitSomeOne()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult PatientLogin(PatientLoginValidation user)
        //{
        //    var userFromDb = _dbContext.AspNetUsers.FirstOrDefault(a => a.Email == user.Email);
        //    if (userFromDb != null && userFromDb.PasswordHash == user.PasswordHash)
        //    {
        //        CookieOptions options = new CookieOptions();
        //        options.Secure = true;
        //        options.Expires = DateTime.Now.AddDays(1);
        //        Response.Cookies.Append("Id", userFromDb.Id.ToString(), options);
        //        return RedirectToAction("PatientDashBoard", "Patient");
        //    }
        //    else
        //    {
        //        return View(null);
        //    }
        //}

        [HttpPost]
         public IActionResult PatientLogin(PatientLogin patientLogin)
        {
            if(!ModelState.IsValid)
            {
                return View(patientLogin);
            }
            int id = _loginServices.ValidateUser(patientLogin);

           if(id == 0)
            {
                return View(patientLogin);
            }
            else
            {
                CookieOptions options = new CookieOptions();
                options.Secure = true;
                options.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Append("Id", id.ToString(), options);
                return RedirectToAction("PatientDashBoard", "Patient");
            }

        }


        [HttpPost]
        public async Task<IActionResult> CreatePatientRequest(AddPatientRequest user)
        {
           
               if(ModelState.IsValid)
            {
                AspNetUser aspNetUser = _dbContext.AspNetUsers.FirstOrDefault(a => a.Email == user.Email);
                if (aspNetUser == null)
                {
                    aspNetUser = new()
                    {

                        UserName = user.FirstName,
                        PasswordHash = user.PasswordHash,
                        Email = user.Email,
                        PhoneNumber = user.Mobile,
                        Ip = "192.168.0.2",
                        CreatedDate = DateTime.Now
                    };

                    _dbContext.AspNetUsers.Add(aspNetUser);
                    await _dbContext.SaveChangesAsync();
                }

                User user2 = _dbContext.Users.FirstOrDefault(a => a.Email == user.Email);
                if (user2 == null)
                {
                    user2 = new()
                    {
                        AspNetUserId = aspNetUser.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Mobile = user.Mobile,
                        Street = user.Street,
                        City = user.City,
                        State = user.State,
                        ZipCode = user.ZipCode,
                        CreatedBy = aspNetUser.Id,
                        CreatedDate = DateTime.Now


                    };
                    _dbContext.Users.Add(user2);
                    await _dbContext.SaveChangesAsync();
                }





                Request user3 = new()
                {
                    RequestTypeId = 1,
                    UserId = user2.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.Mobile,
                    CreatedDate = DateTime.Now,

                    Status = 1


                };
                _dbContext.Requests.Add(user3);
                await _dbContext.SaveChangesAsync();


                RequestClient user4 = new()
                {
                    RequestId = user3.RequestId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.Mobile,
                    Street = user.Street,
                    City = user.City,
                    State = user.State,
                    ZipCode = user.ZipCode


                };
                _dbContext.RequestClients.Add(user4);
                await _dbContext.SaveChangesAsync();
                if(user.File!=null)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");
                    FileInfo fileInfo = new FileInfo(user.File.FileName);
                    string fileName = user.File.FileName + fileInfo.Extension;

                    string fileNameWithPath = Path.Combine(path, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        user.File.CopyTo(stream);
                    }

                    RequestWiseFile user5 = new()
                    {
                        RequestId = user3.RequestId,
                        FileName = fileNameWithPath,
                        CreatedDate = DateTime.Now
                    };

                    _dbContext.RequestWiseFiles.Add(user5);
                    await _dbContext.SaveChangesAsync();
                }
                


                return RedirectToAction("Index", "AspNetUsers");

            }
               else
            {
                return View(null);
            }
            
            


        }
        [HttpPost]
        public async Task<IActionResult> CreateConciergeRequest(AddConciergeValidation user)
        {
            
                if(ModelState.IsValid)
            {
                AspNetUser aspNetUser = _dbContext.AspNetUsers.FirstOrDefault(a => a.Email == user.Email);
                if (aspNetUser == null)
                {
                    aspNetUser = new()
                    {

                        UserName = user.FirstName,
                        PasswordHash = user.PasswordHash,
                        Email = user.Email,
                        PhoneNumber = user.Mobile,
                        Ip = "192.168.0.2",
                        CreatedDate = DateTime.Now
                    };

                    _dbContext.AspNetUsers.Add(aspNetUser);
                    await _dbContext.SaveChangesAsync();
                }

                User user2 = _dbContext.Users.FirstOrDefault(a => a.Email == user.Email);
                if (user2 == null)
                {
                    user2 = new()
                    {
                        AspNetUserId = aspNetUser.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Mobile = user.Mobile,
                        Street = user.Street,
                        City = user.City,
                        State = user.State,
                        ZipCode = user.ZipCode,
                        CreatedBy = aspNetUser.Id,
                        CreatedDate = DateTime.Now


                    };
                    _dbContext.Users.Add(user2);
                    await _dbContext.SaveChangesAsync();
                }




                Request user3 = new()
                {
                    RequestTypeId = 3,
                    UserId = user2.UserId,
                    FirstName = user.CFirstName,
                    LastName = user.CLastName,
                    Email = user.CEmail,
                    PhoneNumber = user.CMobile,
                    CreatedDate = DateTime.Now,
                   
                    Status = 1


                };
                _dbContext.Requests.Add(user3);
                await _dbContext.SaveChangesAsync();
               


                RequestClient user4 = new()
                {
                    RequestId = user3.RequestId,
                    FirstName = user.FirstName,
                    LastName= user.LastName,
                    PhoneNumber= user.Mobile,
                    Street = user.Street,
                    City = user.City,
                    State = user.State,
                    ZipCode = user.ZipCode


                };
                _dbContext.RequestClients.Add(user4);
                await _dbContext.SaveChangesAsync();
                




                Concierge user5 = new()
                {
                    ConciergeName = user.CFirstName,
                    Street = user.Street,
                    City = user.City,
                    State = user.State,
                    ZipCode = user.ZipCode,
                    CreatedDate = DateTime.Now


                };
                _dbContext.Concierges.Add(user5);
                await _dbContext.SaveChangesAsync();
                

                RequestConcierge user6 = new()
                {

                    RequestId = user3.RequestId,
                    ConciergeId = user5.ConciergeId


                };
                _dbContext.RequestConcierges.Add(user6);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index", "AspNetUsers");



            }
                else
            {
                return View(null);
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateFamilyFriendRequest(AddFamilyFriendValidation user)
        {

            if (ModelState.IsValid)
            {
                AspNetUser aspNetUser = _dbContext.AspNetUsers.FirstOrDefault(a => a.Email == user.Email);
                if (aspNetUser == null)
                {
                    aspNetUser = new()
                    {

                        UserName = user.FirstName,
                        PasswordHash = user.PasswordHash,
                        Email = user.Email,
                        PhoneNumber = user.Mobile,
                        Ip = "192.168.0.2",
                        CreatedDate = DateTime.Now
                    };

                    _dbContext.AspNetUsers.Add(aspNetUser);
                    await _dbContext.SaveChangesAsync();
                }

                User user2 = _dbContext.Users.FirstOrDefault(a => a.Email == user.Email);
                if (user2 == null)
                {
                    user2 = new()
                    {
                        AspNetUserId = aspNetUser.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Mobile = user.Mobile,
                        Street = user.Street,
                        City = user.City,
                        State = user.State,
                        ZipCode = user.ZipCode,
                        CreatedBy = aspNetUser.Id,
                        CreatedDate = DateTime.Now


                    };
                    _dbContext.Users.Add(user2);
                    await _dbContext.SaveChangesAsync();
                }





                Request user3 = new()
                {
                    RequestTypeId = 2,
                    UserId = user2.UserId,
                    FirstName = user.FFirstName,
                    LastName = user.FLastName,
                    Email = user.FEmail,
                    PhoneNumber = user.FMobile,
                    CreatedDate = DateTime.Now,
                    
                    Status = 1


                };
                _dbContext.Requests.Add(user3);
                await _dbContext.SaveChangesAsync();



                RequestClient user4 = new()
                {
                    RequestId = user3.RequestId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.Mobile,
                    Street = user.Street,
                    City = user.City,
                    State = user.State,
                    ZipCode = user.ZipCode


                };
                _dbContext.RequestClients.Add(user4);
                await _dbContext.SaveChangesAsync();

                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");
                FileInfo fileInfo = new FileInfo(user.File.FileName);
                string fileName = user.File.FileName + fileInfo.Extension;

                string fileNameWithPath = Path.Combine(path, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    user.File.CopyTo(stream);
                }

                RequestWiseFile user5 = new()
                {
                    RequestId = user3.RequestId,
                    FileName = fileNameWithPath,
                    CreatedDate = DateTime.Now
                };

                _dbContext.RequestWiseFiles.Add(user5);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction("Index", "AspNetUsers");

            }
            else
            {
                return View(null);
            }

        }
        [HttpPost]
        public async Task<IActionResult> SubmitMe(SubmitMe user)
        {
            int id = Int32.Parse(Request.Cookies["UserId"]);
            if (ModelState.IsValid)
            {
                

                Request user3 = new()
                {
                    RequestTypeId = 1,
                    UserId = id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.Mobile,
                    CreatedDate = DateTime.Now,

                    Status = 1


                };
                _dbContext.Requests.Add(user3);
                await _dbContext.SaveChangesAsync();


                RequestClient user4 = new()
                {
                    RequestId = user3.RequestId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.Mobile,
                    Street = user.Street,
                    City = user.City,
                    State = user.State,
                    ZipCode = user.ZipCode


                };
                _dbContext.RequestClients.Add(user4);
                await _dbContext.SaveChangesAsync();
                if (user.File != null)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");
                    FileInfo fileInfo = new FileInfo(user.File.FileName);
                    string fileName = user.File.FileName + fileInfo.Extension;

                    string fileNameWithPath = Path.Combine(path, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        user.File.CopyTo(stream);
                    }

                    RequestWiseFile user5 = new()
                    {
                        RequestId = user3.RequestId,
                        FileName = fileNameWithPath,
                        CreatedDate = DateTime.Now
                    };

                    _dbContext.RequestWiseFiles.Add(user5);
                    await _dbContext.SaveChangesAsync();
                }



                return RedirectToAction("Index", "AspNetUsers");

            }
            else
            {
                return View(null);
            }




        }

        [HttpPost]
        public async Task<IActionResult> SubmitSomeOne(SubmitMe user)
        {
            int id = Int32.Parse(Request.Cookies["UserId"]);
            if (ModelState.IsValid)
            {


                Request user3 = new()
                {
                    RequestTypeId = 1,
                    UserId = id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.Mobile,
                    CreatedDate = DateTime.Now,

                    Status = 1


                };
                _dbContext.Requests.Add(user3);
                await _dbContext.SaveChangesAsync();


                RequestClient user4 = new()
                {
                    RequestId = user3.RequestId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.Mobile,
                    Street = user.Street,
                    City = user.City,
                    State = user.State,
                    ZipCode = user.ZipCode


                };
                _dbContext.RequestClients.Add(user4);
                await _dbContext.SaveChangesAsync();
                if (user.File != null)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");
                    FileInfo fileInfo = new FileInfo(user.File.FileName);
                    string fileName = user.File.FileName + fileInfo.Extension;

                    string fileNameWithPath = Path.Combine(path, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        user.File.CopyTo(stream);
                    }

                    RequestWiseFile user5 = new()
                    {
                        RequestId = user3.RequestId,
                        FileName = fileNameWithPath,
                        CreatedDate = DateTime.Now
                    };

                    _dbContext.RequestWiseFiles.Add(user5);
                    await _dbContext.SaveChangesAsync();
                }



                return RedirectToAction("Index", "AspNetUsers");

            }
            else
            {
                return View(null);
            }




        }



        [HttpPost]
        public async Task<IActionResult> CreateBusinessRequest(AddBusinessRequest user)
        {

            if (ModelState.IsValid)
            {
                AspNetUser aspNetUser = _dbContext.AspNetUsers.FirstOrDefault(a=>a.Email==user.Email);
               if(aspNetUser==null)
                {
                    aspNetUser = new()
                    {

                        UserName = user.BFirstName,
                        PasswordHash = user.PasswordHash,
                        Email = user.BEmail,
                        PhoneNumber = user.BMobile,
                        Ip = "192.168.0.2",
                        CreatedDate = DateTime.Now
                    };

                    _dbContext.AspNetUsers.Add(aspNetUser);
                    await _dbContext.SaveChangesAsync();
                }

                User user2 = _dbContext.Users.FirstOrDefault(a => a.Email == user.Email);
                if (user2 == null)
                {
                     user2 = new()
                    {
                        AspNetUserId = aspNetUser.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Mobile = user.Mobile,
                        Street = user.Street,
                        City = user.City,
                        State = user.State,
                        ZipCode = user.ZipCode,
                        CreatedBy = aspNetUser.Id,
                        CreatedDate = DateTime.Now


                    };
                    _dbContext.Users.Add(user2);
                    await _dbContext.SaveChangesAsync();
                }
                




                Request user3 = new()
                {
                    RequestTypeId = 4,
                    UserId = user2.UserId,
                    FirstName = user.BFirstName,
                    LastName = user.BLastName,
                    Email = user.BEmail,
                    PhoneNumber = user.BMobile,
                    CreatedDate = DateTime.Now,
                   
                    Status = 1


                };
                _dbContext.Requests.Add(user3);
                await _dbContext.SaveChangesAsync();



                RequestClient user4 = new()
                {
                    RequestId = user3.RequestId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.Mobile,
                    Street = user.Street,
                    City = user.City,
                    State = user.State,
                    ZipCode = user.ZipCode


                };
                _dbContext.RequestClients.Add(user4);
                await _dbContext.SaveChangesAsync();





                Business user5 = new()
                {
                    Name = user.FirstName,
                    Address1 = user.Street,
                    Address2 = user.Street,
                    City = user.City,
                    ZipCode = user.ZipCode,
                    PhoneNumber = user.BMobile,
                    CreatedDate = DateTime.Now


                };
                _dbContext.Businesses.Add(user5);
                await _dbContext.SaveChangesAsync();


                RequestBusiness user6 = new()
                {

                    RequestId = user3.RequestId,
                    BusinessId = user5.BusinessId


                };
                _dbContext.RequestBusinesses.Add(user6);
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


