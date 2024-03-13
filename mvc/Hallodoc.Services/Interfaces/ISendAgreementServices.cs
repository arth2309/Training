using HalloDoc.Repositories.DataModels;
using HallodocServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Interfaces
{
    public interface ISendAgreementServices
    {
        Request LoadSendAgreementData(int requestid);
        void SendEmail(SendAgreement sendAgreement, string token);
        Task<int> CheckViewAgreement(string token);
        void CancelViewAgreement(int requestid, string description);
        void AcceptViewAgreement(int requestid);
    }
}
