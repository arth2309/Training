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
    public class ViewCaseServices : IViewCaseServices
    {
        private readonly IRequestClientRepo _repo;
        private readonly IRequestRepo _repo1;

        public ViewCaseServices(IRequestClientRepo repo,IRequestRepo repo1) 
        {
            _repo = repo;
            _repo1 = repo1;
        }

        public AdminViewCase GetAdminViewCaseData(int id)
        {
            var r1 = _repo.GetViewCaseData(id);
            AdminCancelCase adminCancelCase = new AdminCancelCase();
            adminCancelCase.requestId = r1.RequestId;

            AdminViewCase adminViewCase = new AdminViewCase();

            {
               
                adminViewCase.id = id;
                adminViewCase.FirstName = r1.FirstName;
                adminViewCase.LastName = r1.LastName;
                adminViewCase.Email = r1.Email;
                adminViewCase.Mobile = r1.PhoneNumber;
                adminViewCase.State = r1.State;
                adminViewCase.City = r1.City;  
                adminViewCase.Street = r1.Street;
                adminViewCase.id = r1.RequestClientId;
                adminViewCase.status = r1.Request.Status;
                adminViewCase.rid = r1.RequestId;
                adminViewCase.cancelCases = adminCancelCase;
                adminViewCase.requesttypeid = r1.Request.RequestTypeId;
                
            }

            return adminViewCase;
        }

        public AdminViewCase EditViewData(AdminViewCase adminViewCase) 
        {
            RequestClient requestClient = _repo.GetViewCaseData(adminViewCase.id);
           requestClient.Email = adminViewCase.Email;
           requestClient.FirstName = adminViewCase.FirstName;
            requestClient.LastName = adminViewCase.LastName;
            requestClient.PhoneNumber = adminViewCase.Mobile;
            requestClient.Street = adminViewCase.Street;
            _repo.UpdateTable(requestClient);
            return adminViewCase;
        }

        public  AdminViewCase CancelViewData(int rid)
        {
            Request request = _repo1.GetRequest(rid);
            request.Status = 3;
            _repo1.UpdateTable(request);
            return null;
        }
    }
}
