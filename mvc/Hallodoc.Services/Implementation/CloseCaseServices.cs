using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Interfaces;
using HallodocServices.Interfaces;
using HallodocServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Implementation
{

    public class CloseCaseServices : ICloseCaseServices
    {
        private readonly IRequestClientRepo _requestClientRepo;
        private readonly IRequestFileRepo _requestFileRepo;
        private readonly IRequestRepo _requestRepo;

        public CloseCaseServices(IRequestClientRepo requestClientRepo, IRequestFileRepo requestFileRepo, IRequestRepo requestRepo)
        {
            _requestClientRepo = requestClientRepo;
            _requestFileRepo = requestFileRepo;
            _requestRepo = requestRepo;
        }

        public AdminCloseCase GetCloseCaseData(int requestid)
        {
            RequestClient requestClient = _requestClientRepo.requestClient1(requestid);

            List<RequestWiseFile> requestWiseFile = _requestFileRepo.GetAllFiles(requestid);

            AdminCloseCase adminCloseCase = new();
            adminCloseCase.FirstName = requestClient.FirstName;
            adminCloseCase.LastName = requestClient.LastName;
            adminCloseCase.Mobile = requestClient.PhoneNumber;
            adminCloseCase.Email = requestClient.Email;
            adminCloseCase.WiseFiles = requestWiseFile;
            adminCloseCase.reqid = requestid;

            return adminCloseCase;
            
        }

        public async Task<bool> UpdateCloseData(AdminCloseCase adminClose)
        {
            RequestClient requestClient = _requestClientRepo.requestClient1(adminClose.reqid);
            requestClient.RequestId = adminClose.reqid;
            requestClient.FirstName = adminClose.FirstName;
            requestClient.LastName = adminClose.LastName;
            requestClient.PhoneNumber = adminClose.Mobile;
            requestClient.Email = adminClose.Email;
            await _requestClientRepo.UpdateTable(requestClient);
            return true;
        }

        public async Task<bool> UpdateStatus(int requestid)
        {
            Request request = _requestRepo.GetRequest(requestid);
            request.Status = 9;
            await _requestRepo.UpdateTable(request);
            return true;
        }
        
    }
}
