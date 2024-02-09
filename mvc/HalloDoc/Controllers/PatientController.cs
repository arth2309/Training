﻿using HalloDoc.DataContext;
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

        public IActionResult PatientDashBoard()
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



            return RedirectToAction("Index", "AspNetUsers");
            
            


        }
        [HttpPost]
        public async Task<IActionResult> CreateConciergeRequest(AddConciergeValidation user)
        {
            
                if(ModelState.IsValid)
            {
                AspNetUser user1 = new()
                {
                    UserName = user.CFirstName,
                    PasswordHash = "abc@123",
                    Email = user.CEmail,
                    PhoneNumber = user.CMobile,
                    Ip = "192.168.0.2",
                    CreatedDate = DateTime.Now


                };

                _dbContext.AspNetUsers.Add(user1);
                await _dbContext.SaveChangesAsync();

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
                



                Request user3 = new()
                {
                    RequestTypeId = 3,
                    UserId = user2.UserId,
                    FirstName = user.CFirstName,
                    LastName = user.CLastName,
                    Email = user.CEmail,
                    PhoneNumber = user.CMobile,
                    CreatedDate = DateTime.Now,
                    Symptoms = user.symptoms,
                    Roomsuite = user.roomsuite,
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
                AspNetUser user1 = new()
                {
                    UserName = user.FFirstName,
                    PasswordHash = "abc@123",
                    Email = user.FEmail,
                    PhoneNumber = user.FMobile,
                    Ip = "192.168.0.2",
                    CreatedDate = DateTime.Now


                };

                _dbContext.AspNetUsers.Add(user1);
                await _dbContext.SaveChangesAsync();

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




                Request user3 = new()
                {
                    RequestTypeId = 2,
                    UserId = user2.UserId,
                    FirstName = user.FFirstName,
                    LastName = user.FLastName,
                    Email = user.FEmail,
                    PhoneNumber = user.FMobile,
                    CreatedDate = DateTime.Now,
                    Symptoms = user.symptoms,
                    Roomsuite = user.roomsuite,
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
                AspNetUser user1 = new()
                {
                    UserName = user.BFirstName,
                    PasswordHash = "abc@123",
                    Email = user.BEmail,
                    PhoneNumber = user.BMobile,
                    Ip = "192.168.0.2",
                    CreatedDate = DateTime.Now


                };

                _dbContext.AspNetUsers.Add(user1);
                await _dbContext.SaveChangesAsync();

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




                Request user3 = new()
                {
                    RequestTypeId = 4,
                    UserId = user2.UserId,
                    FirstName = user.BFirstName,
                    LastName = user.BLastName,
                    Email = user.BEmail,
                    PhoneNumber = user.BMobile,
                    CreatedDate = DateTime.Now,
                    Symptoms = user.symptoms,
                    Roomsuite = user.roomsuite,
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

