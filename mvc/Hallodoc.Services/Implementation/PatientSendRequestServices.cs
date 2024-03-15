using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Interfaces;
using HallodocServices.ModelView;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Implementation
{
    public class PatientSendRequestServices
    {
        private readonly IAspNetUserRepo _aspNetUserRepo;
        private readonly IUserRepo _userRepo;
        private readonly IRequestBusinessRepo _requestBusinessRepo;
        private readonly IRequestClientRepo _requestClientRepo;
        private readonly IRequestConceirgeRepo _requestConceirgeRepo;
        private readonly IRequestFileRepo _requestFileRepo;
        private readonly IRequestRepo _requestRepo;

        public PatientSendRequestServices(IAspNetUserRepo aspNetUserRepo, IUserRepo userRepo, IRequestRepo requestRepo, IRequestFileRepo requestFileRepo, IRequestConceirgeRepo requestConceirgeRepo, IRequestClientRepo requestClientRepo, IRequestBusinessRepo requestBusinessRepo)
        {
            _aspNetUserRepo = aspNetUserRepo;
            _userRepo = userRepo;
            _requestRepo = requestRepo;
            _requestFileRepo = requestFileRepo;
            _requestClientRepo = requestClientRepo;
            _requestBusinessRepo = requestBusinessRepo;
            _requestConceirgeRepo = requestConceirgeRepo;
        }

        public async Task<bool> SendPatientRequest(PatientSendRequests user)
        {

            if (_aspNetUserRepo.CheckAspNetUser(user.Email))
            {
                AspNetUser aspNetUser = new()
                {

                    UserName = user.FirstName,
                    PasswordHash = user.PasswordHash,
                    Email = user.Email,
                    PhoneNumber = user.Mobile,
                    Ip = "192.168.0.2",
                    CreatedDate = DateTime.Now
                };
                await _aspNetUserRepo.AddTable(aspNetUser);
            }


            if (_userRepo.CheckUser(user.Email))
            {
                User user2 = new()
                {
                    AspNetUserId = 1,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Mobile = user.Mobile,
                    Street = user.Street,
                    City = user.City,
                    State = user.State,
                    ZipCode = user.ZipCode,
                    CreatedBy = 1,
                    CreatedDate = DateTime.Now


                };
                await _userRepo.AddTable(user2);
            }





            Request user3 = new()
            {
                RequestTypeId = 1,
                UserId = 1,
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
                ZipCode = user.ZipCode


            };

            await _requestClientRepo.AddTable(user4);
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
               await  _requestFileRepo.AddData(user5);
            }
            return true;
        }
    }
   
}
