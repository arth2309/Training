using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Interfaces;
using HallodocServices.Interfaces;
using HallodocServices.ModelView;

namespace HallodocServices.Implementation
{
    public class CancelCaseServices : ICancelCaseServices
    {
        private readonly IRequestStatusLogRepo _statusLogRepo;
        private readonly IRequestRepo _requestRepo;
        public CancelCaseServices(IRequestStatusLogRepo statusLogRepo, IRequestRepo requestRepo)
        {
            _statusLogRepo = statusLogRepo;
            _requestRepo = requestRepo;
        }

        public AdminCancelCase CancelData(int reqID,AdminCancelCase newState)
        {
            Request request = _requestRepo.GetRequest(reqID);
            request.Status = 3;
            request.CaseTag = newState.CaseTag;
            _requestRepo.UpdateTable(request);

            RequestStatusLog requestStatusLog = new();
            requestStatusLog.RequestId = reqID;
            requestStatusLog.Status = 3;
            requestStatusLog.Notes = newState.Notes;
            _statusLogRepo.AddData(requestStatusLog);
            return newState;

        }


    }
}
