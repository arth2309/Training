using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HallodocServices.Interfaces;
using HallodocServices.ModelView;
using HalloDoc.Repositories.Interfaces;
using HalloDoc.Repositories.DataModels;

namespace HallodocServices.Implementation
{
    public class BlockCaseServices : IBlockCaseServices
    {
        private readonly IRequestRepo _requestRepo;
        private readonly IRequestStatusLogRepo _requestStatusLogRepo;
        private readonly IBlockedRequestRepo _blockedRequestRepo;

        public BlockCaseServices(IRequestRepo requestRepo, IRequestStatusLogRepo requestStatusLogRepo, IBlockedRequestRepo blockedRequestRepo)
        {
            _requestRepo = requestRepo;
            _requestStatusLogRepo = requestStatusLogRepo;
            _blockedRequestRepo = blockedRequestRepo;
        }

        public AdminBlockCase AdminBlockCase(AdminBlockCase adminBlockCase) 
        {
            Request request = _requestRepo.GetRequest(adminBlockCase.requestId);
            request.RequestId = adminBlockCase.requestId;
            request.Status = 10;
            _requestRepo.UpdateTable(request);

            RequestStatusLog requestStatusLog = new RequestStatusLog();
            requestStatusLog.RequestId = adminBlockCase.requestId;
            requestStatusLog.Status = 10;
            requestStatusLog.Notes = adminBlockCase.blockNotes;
            _requestStatusLogRepo.AddData(requestStatusLog);

            BlockRequest blockRequest = new BlockRequest();
            blockRequest.RequestId = adminBlockCase.requestId.ToString();
            blockRequest.Reason = adminBlockCase.blockNotes;
            blockRequest.CreatedDate = DateTime.Now;
            blockRequest.Email = adminBlockCase.Email;
            blockRequest.PhoneNumber = adminBlockCase.Mobile;
            _blockedRequestRepo.AddData(blockRequest);

            return adminBlockCase;
            
        }
    }
}
