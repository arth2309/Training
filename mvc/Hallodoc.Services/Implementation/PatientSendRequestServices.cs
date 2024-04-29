using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Interfaces;
using HallodocServices.Interfaces;
using HallodocServices.ModelView;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Implementation
{
    public class PatientSendRequestServices : IPatientSendRequestServices
    {
        private readonly IAspNetUserRepo _aspNetUserRepo;
        private readonly IUserRepo _userRepo;
        private readonly IRequestBusinessRepo _requestBusinessRepo;
        private readonly IRequestClientRepo _requestClientRepo;
        private readonly IRequestConceirgeRepo _requestConceirgeRepo;
        private readonly IRequestFileRepo _requestFileRepo;
        private readonly IRequestRepo _requestRepo;
        private readonly IPasswordHashServices _passwordHashServices;

        public PatientSendRequestServices(IAspNetUserRepo aspNetUserRepo, IUserRepo userRepo, IRequestRepo requestRepo, IRequestFileRepo requestFileRepo, IRequestConceirgeRepo requestConceirgeRepo, IRequestClientRepo requestClientRepo, IRequestBusinessRepo requestBusinessRepo,IPasswordHashServices passwordHashServices)
        {
            _aspNetUserRepo = aspNetUserRepo;
            _userRepo = userRepo;
            _requestRepo = requestRepo;
            _requestFileRepo = requestFileRepo;
            _requestClientRepo = requestClientRepo;
            _requestBusinessRepo = requestBusinessRepo;
            _requestConceirgeRepo = requestConceirgeRepo;
            _passwordHashServices = passwordHashServices;
        }

        

        public async Task<bool> SendPatientRequest(PatientSendRequests user)
        {

            if (_aspNetUserRepo.CheckAspNetUser(user.Email))
            {
                AspNetUser aspNetUser = new()
                {

                    UserName = user.FirstName,
                    PasswordHash =  _passwordHashServices.PasswordHash(user.PasswordHash),
                    Email = user.Email,
                    PhoneNumber = user.Mobile,
                    Ip = "192.168.0.2",
                    CreatedDate = DateTime.Now
                };
                await _aspNetUserRepo.AddTable(aspNetUser);

                AspNetUserRole aspNetUserRole = new()
                {
                    UserId = aspNetUser.Id,
                    RoleId = 3
                };

                await _aspNetUserRepo.AddData(aspNetUserRole);
            }


            if (_userRepo.CheckUser(user.Email))
            {
                User user2 = new()
                {
                    AspNetUserId = _aspNetUserRepo.GetId(user.Email),
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Mobile = user.Mobile,
                    Street = user.Street,
                    City = user.City,
                    State = user.State,
                    ZipCode = user.ZipCode,
                    CreatedBy = _aspNetUserRepo.GetId(user.Email),
                    CreatedDate = DateTime.Now


                };
                await _userRepo.AddTable(user2);
            }





            Request user3 = new()
            {
                RequestTypeId = 1,
                UserId = _userRepo.GetUserId(user.Email),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.Mobile,
                CreatedDate = DateTime.Now,
                ConfirmationNumber = GenerateConfirmationNumber(DateTime.Now, user.FirstName, user.LastName),
                Status = 1


            };

            await _requestRepo.AddTable(user3);


            RequestClient user4 = new()
            {
                RequestId = user3.RequestId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.Mobile,
                Street = user.Street,
                City = user.City,
                State = user.State,
                ZipCode = user.ZipCode,
                Email = user.Email,
                StrMonth = user.BirthDate.ToString("MMM"),
                IntYear = user.BirthDate.Year,
                IntDate = user.BirthDate.Day


            };

            await _requestClientRepo.AddTable(user4);
            if (user.File != null)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");
                FileInfo fileInfo = new FileInfo(user.File.FileName);
                string fileName = user.File.FileName;

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
               await  _requestFileRepo.AddData(user5);
            }
            return true;
        }
        public async Task<bool> SendBusinessRequest(BusinessSendRequests user)
        {
            if (_aspNetUserRepo.CheckAspNetUser(user.Email))
            {
                AspNetUser aspNetUser = new()
                {

                    UserName = user.FirstName,
                    PasswordHash = _passwordHashServices.PasswordHash(user.PasswordHash),
                    Email = user.Email,
                    PhoneNumber = user.Mobile,
                    Ip = "192.168.0.2",
                    CreatedDate = DateTime.Now
                };

                await _aspNetUserRepo.AddTable(aspNetUser);

                AspNetUserRole aspNetUserRole = new()
                {
                    UserId = aspNetUser.Id,
                    RoleId = 3
                };

                await _aspNetUserRepo.AddData(aspNetUserRole);
            }


            if (_userRepo.CheckUser(user.Email))
            {
                User user2 = new()
                {
                    AspNetUserId = _aspNetUserRepo.GetId(user.Email),
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Mobile = user.Mobile,
                    Street = user.Street,
                    City = user.City,
                    State = user.State,
                    ZipCode = user.ZipCode,
                    CreatedBy = _aspNetUserRepo.GetId(user.Email),
                    CreatedDate = DateTime.Now


                };

                await _userRepo.AddTable(user2);
            }




            Request user3 = new()
            {
                RequestTypeId = 3,
                UserId = _userRepo.GetUserId(user.Email),
                FirstName = user.BFirstName,
                LastName = user.BLastName,
                Email = user.BEmail,
                PhoneNumber = user.BMobile,
                CreatedDate = DateTime.Now,
                ConfirmationNumber = GenerateConfirmationNumber(DateTime.Now, user.FirstName, user.LastName),
                Status = 1


            };

            await _requestRepo.AddTable(user3);



            RequestClient user4 = new()
            {
                RequestId = user3.RequestId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.Mobile,
                Street = user.Street,
                City = user.City,
                State = user.State,
                ZipCode = user.ZipCode,
                Email = user.Email,
                StrMonth = user.BirthDate.ToString("MMM"),
                IntYear = user.BirthDate.Year,
                IntDate = user.BirthDate.Day


            };

            await _requestClientRepo.AddTable(user4);





            Business user5 = new()
            {
                Name = user.FirstName,
                Address1 = user.Street,
                Address2 = user.Street,
                City = user.City,
                ZipCode = user.ZipCode,
                PhoneNumber = user.BMobile,
                CreatedDate = DateTime.Now,


            };

            await _requestBusinessRepo.AddBusinessData(user5);


            RequestBusiness user6 = new()
            {

                RequestId = user3.RequestId,
                BusinessId = user5.BusinessId


            };
            await _requestBusinessRepo.AddRequestBusinessData(user6);

            return true;
        }

        public async Task<bool> SendConciergeRequest(ConciergeSendRequests user)
        {
           
            if (_aspNetUserRepo.CheckAspNetUser(user.Email))
            {
               AspNetUser aspNetUser = new()
                {

                    UserName = user.FirstName,
                    PasswordHash =_passwordHashServices.PasswordHash(user.PasswordHash),
                    Email = user.Email,
                    PhoneNumber = user.Mobile,
                    Ip = "192.168.0.2",
                    CreatedDate = DateTime.Now
                };

                await _aspNetUserRepo.AddTable(aspNetUser);

                AspNetUserRole aspNetUserRole = new()
                {
                    UserId = aspNetUser.Id,
                    RoleId = 3
                };

                await _aspNetUserRepo.AddData(aspNetUserRole);
            }

            
            if (_userRepo.CheckUser(user.Email))
            {
                User user2 = new()
                {
                    AspNetUserId = _aspNetUserRepo.GetId(user.Email),
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Mobile = user.Mobile,
                    Street = user.Street,
                    City = user.City,
                    State = user.State,
                    ZipCode = user.ZipCode,
                    CreatedBy = _aspNetUserRepo.GetId(user.Email),
                    CreatedDate = DateTime.Now


                };
                
                await _userRepo.AddTable(user2);
            }




            Request user3 = new()
            {
                RequestTypeId = 3,
                UserId = _userRepo.GetUserId(user.Email),
                FirstName = user.CFirstName,
                LastName = user.CLastName,
                Email = user.CEmail,
                PhoneNumber = user.CMobile,
                CreatedDate = DateTime.Now,

                Status = 1


            };
            
            await _requestRepo.AddTable(user3);



            RequestClient user4 = new()
            {
                RequestId = user3.RequestId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.Mobile,
                Street = user.Street,
                City = user.City,
                State = user.State,
                ZipCode = user.ZipCode,
                Email = user.Email,
                StrMonth = user.BirthDate.ToString("MMM"),
                IntYear = user.BirthDate.Year,
                IntDate = user.BirthDate.Day


            };
            
            await _requestClientRepo.AddTable(user4);





            Concierge user5 = new()
            {
                ConciergeName = user.CFirstName,
                Street = user.Street,
                City = user.City,
                State = user.State,
                ZipCode = user.ZipCode,
                CreatedDate = DateTime.Now


            };
            
            await _requestConceirgeRepo.AddConceirgeData(user5);


            RequestConcierge user6 = new()
            {

                RequestId = user3.RequestId,
                ConciergeId = user5.ConciergeId


            };
            await _requestConceirgeRepo.AddRequestConciergeData(user6);

            return true;
        }

        public async Task<bool> SendFamilyFriendRequest(FamilyFriendSendRequests user)
        {
            if (_aspNetUserRepo.CheckAspNetUser(user.Email))
            {
                AspNetUser aspNetUser = new()
                {

                    UserName = user.FirstName,
                    PasswordHash = _passwordHashServices.PasswordHash(user.PasswordHash),
                    Email = user.Email,
                    PhoneNumber = user.Mobile,
                    Ip = "192.168.0.2",
                    CreatedDate = DateTime.Now
                };
                await _aspNetUserRepo.AddTable(aspNetUser);

                AspNetUserRole aspNetUserRole = new()
                {
                    UserId = aspNetUser.Id,
                    RoleId = 3
                };

                await _aspNetUserRepo.AddData(aspNetUserRole);
            }


            if (_userRepo.CheckUser(user.Email))
            {
                User user2 = new()
                {
                    AspNetUserId = _aspNetUserRepo.GetId(user.Email),
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Mobile = user.Mobile,
                    Street = user.Street,
                    City = user.City,
                    State = user.State,
                    ZipCode = user.ZipCode,
                    CreatedBy = _aspNetUserRepo.GetId(user.Email),
                    CreatedDate = DateTime.Now


                };
                await _userRepo.AddTable(user2);
            }





            Request user3 = new()
            {
                RequestTypeId = 2,
                UserId = _userRepo.GetUserId(user.Email),
                FirstName = user.FFirstName,
                LastName = user.FLastName,
                Email = user.FEmail,
                PhoneNumber = user.FMobile,
                CreatedDate = DateTime.Now,
                Status = 1


            };

            await _requestRepo.AddTable(user3);


            RequestClient user4 = new()
            {
                RequestId = user3.RequestId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.Mobile,
                Street = user.Street,
                City = user.City,
                State = user.State,
                ZipCode = user.ZipCode,
                Email = user.Email,
                StrMonth = user.BirthDate.ToString("MMM"),
                IntYear = user.BirthDate.Year,
                IntDate = user.BirthDate.Day


            };

            await _requestClientRepo.AddTable(user4);
            if (user.File != null)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");
                FileInfo fileInfo = new FileInfo(user.File.FileName);
                string fileName = user.File.FileName;

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
                await _requestFileRepo.AddData(user5);
            }
            return true;
        }

        public PatientSubmitMe SubmitMeData(int userid)
        {
            User user = _userRepo.GetUserData(userid);
            PatientSubmitMe showProfile = new()
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
            return showProfile;
        }

        public async Task<bool> SubmitMeRequest(PatientSubmitMe user, int Userid)
        {
            Request user3 = new()
            {
                RequestTypeId = 1,
                UserId = Userid,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.Mobile,
                CreatedDate = DateTime.Now,
                Status = 1


            };

            await _requestRepo.AddTable(user3);


            RequestClient user4 = new()
            {
                RequestId = user3.RequestId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.Mobile,
                Street = user.Street,
                City = user.City,
                State = user.State,
                ZipCode = user.ZipCode,
                Email = user.Email,
                StrMonth = user.BirthDate.ToString("MMM"),
                IntYear = user.BirthDate.Year,
                IntDate = user.BirthDate.Day


            };

            await _requestClientRepo.AddTable(user4);
            if (user.File != null)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");
                FileInfo fileInfo = new FileInfo(user.File.FileName);
                string fileName = user.File.FileName;

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
                await _requestFileRepo.AddData(user5);
            }

            return true;
        }

        public bool CheckEmail(string email)
        {
            return _aspNetUserRepo.CheckAspNetUser(email) == true? false : true;
        }

        public async Task<bool> CreateRequest(CreateRequestVM user)
        {

            if (_aspNetUserRepo.CheckAspNetUser(user.Email))
            {
                AspNetUser aspNetUser = new()
                {

                    UserName = user.FirstName,
                    Email = user.Email,
                    PhoneNumber = user.Mobile,
                    Ip = "192.168.0.2",
                    CreatedDate = DateTime.Now
                };
                await _aspNetUserRepo.AddTable(aspNetUser);

                AspNetUserRole aspNetUserRole = new()
                {
                    UserId = aspNetUser.Id,
                    RoleId = 3
                };

                await _aspNetUserRepo.AddData(aspNetUserRole);
            }


            if (_userRepo.CheckUser(user.Email))
            {
                User user2 = new()
                {
                    AspNetUserId = _aspNetUserRepo.GetId(user.Email),
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Mobile = user.Mobile,
                    Street = user.Street,
                    City = user.City,
                    State = user.State,
                    ZipCode = user.ZipCode,
                    CreatedBy = _aspNetUserRepo.GetId(user.Email),
                    CreatedDate = DateTime.Now


                };
                await _userRepo.AddTable(user2);
            }





            Request user3 = new()
            {
                RequestTypeId = 1,
                UserId = _userRepo.GetUserId(user.Email),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.Mobile,
                CreatedDate = DateTime.Now,
                Status = 1


            };

            await _requestRepo.AddTable(user3);


            RequestClient user4 = new()
            {
                RequestId = user3.RequestId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.Mobile,
                Street = user.Street,
                City = user.City,
                State = user.State,
                ZipCode = user.ZipCode,
                Email = user.Email,
                StrMonth = user.DateOfBirth.ToString("MMM"),
                IntYear = user.DateOfBirth.Year,
                IntDate = user.DateOfBirth.Day


            };

            await _requestClientRepo.AddTable(user4);
            return true;
        }

        public string GenerateConfirmationNumber(DateTime dateTime,string FirstName,string LastName)
        {
            string number1 = dateTime.ToString("yyyyMMdd") + FirstName.Substring(0,2).ToUpper() + LastName.Substring(0,2).ToUpper();
            string count1 = _requestRepo.NoOfRequestToday(dateTime).ToString("0000");
            number1 = number1 + count1;
            return number1;
        }
    }
   
}
