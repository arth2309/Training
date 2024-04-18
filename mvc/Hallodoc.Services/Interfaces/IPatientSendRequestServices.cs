using HallodocServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Interfaces
{
    public interface IPatientSendRequestServices
    {
        Task<bool> SendPatientRequest(PatientSendRequests user);
        Task<bool> SendFamilyFriendRequest(FamilyFriendSendRequests user);
        Task<bool> SendConciergeRequest(ConciergeSendRequests user);
        Task<bool> SendBusinessRequest(BusinessSendRequests user);

        PatientSubmitMe SubmitMeData(int userid);
        Task<bool> SubmitMeRequest(PatientSubmitMe user,int Userid);
        bool CheckEmail(string email);

        Task<bool> CreateRequest(CreateRequestVM user);

    }
}
