using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HallodocServices.ModelView;
using HalloDoc.Repositories.Interfaces;
using HallodocServices.Interfaces;
using HalloDoc.Repositories.DataModels;

namespace HallodocServices.Implementation
{
    public class ClearCaseServices : IClearCaseServices
    {
        private readonly IRequestRepo _requestRepo;
       

        public ClearCaseServices(IRequestRepo requestRepo)
        {
            _requestRepo = requestRepo;
            
        }

        public async Task<bool> ClearCase(int RequestId)
        {
            Request request = _requestRepo.GetRequest(RequestId);
            request.RequestId = RequestId;
            request.Status = 12;
            _requestRepo.UpdateTable(request);

            return true;
        }
    }
}
