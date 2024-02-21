using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HallodocServices.Interfaces;
using HallodocServices.ModelView;
using HalloDoc.Repositories.Interfaces;
using HalloDoc.Repositories.DataModels;
using Microsoft.EntityFrameworkCore;

namespace HallodocServices.Implementation
{
     public class PatientDashBoardServices : IPatientDashBoardServices
    {
        private readonly IUserRepo _userRepo;
        private readonly IRequestRepo _requestRepo;
        private readonly IRequestFileRepo _requestFileRepo;

        public PatientDashBoardServices(IUserRepo userRepo, IRequestRepo requestRepo, IRequestFileRepo requestFileRepo)
        {
            _userRepo = userRepo;
            _requestRepo = requestRepo;
            _requestFileRepo = requestFileRepo;
        }
        public int GetId(int id)
        {
            return _requestRepo.GetUid(id);
        }

       public List<PatientDashBoard> patientDashBoards(int id)
        {
            int uid = _requestRepo.GetUid(id);
            List<Request> userrequest = _requestRepo.GetAllRequests(uid);
            List<PatientDashBoard> dashboard = new List<PatientDashBoard>();
            for (int i = 0; i < userrequest.Count; i++)
            {
                List<RequestWiseFile> requestWiseFiles = _requestFileRepo.GetAllFiles(userrequest[i].RequestId).ToList();
                PatientDashBoard dashboard1 = new()
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
            return dashboard;
        }


        

    }
}
