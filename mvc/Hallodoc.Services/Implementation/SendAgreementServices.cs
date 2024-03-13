using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Interfaces;
using HallodocServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HallodocServices.ModelView;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HallodocServices.Implementation
{
    public class SendAgreementServices : ISendAgreementServices
    {
        private readonly IRequestRepo _requestRepo;
        private readonly IJwtServices _jwtServices;
        private readonly IRequestStatusLogRepo _requestStatusLogRepo;

        public SendAgreementServices(IRequestRepo requestRepo,IJwtServices jwtServices, IRequestStatusLogRepo requestStatusLogRepo)
        {
            _requestRepo = requestRepo;
            _jwtServices = jwtServices;
            _requestStatusLogRepo = requestStatusLogRepo;
        }

        public Request LoadSendAgreementData(int requestid)
        {
            Request request = _requestRepo.GetRequest(requestid);
            return request;
        }

        public void SendEmail(SendAgreement sendAgreement, string token)
        {
            string email = sendAgreement.Email;
            MailMessage mm = new MailMessage("tatva.dotnet.arthgandhi@outlook.com", email);
            string mainURL = "https://localhost:7091/adminSite/ViewAgreement" + "?token=" + token;
            mm.Subject = "Agreement";
            mm.Body = string.Format("Hi {0}, Click on the link to view agreement <p><a href=\"" + mainURL + "\">Link</a></p>", email);
            mm.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.office365.com";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(userName: "tatva.dotnet.arthgandhi@outlook.com", password: "Liony@2002");
            smtp.Port = 587;
            smtp.Send(mm);
        }

        public async Task<int> CheckViewAgreement(string token)
           {
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(token);
            if (_jwtServices.ValidateToken(token, out jwtSecurityToken))
            {
                return Int32.Parse(jwtSecurityToken.Claims.FirstOrDefault(a => a.Type == "RequestId").Value);
            }
            else
            {
                return 0;
            }
        }

        public void CancelViewAgreement(int requestid,string description)
        {
            Request request = _requestRepo.GetRequest(requestid);
            request.Status = 10;
            _requestRepo.UpdateTable(request);

            RequestStatusLog requestStatusLog = new RequestStatusLog();
            requestStatusLog.Status = 10;
            requestStatusLog.Notes = description;
            requestStatusLog.RequestId = requestid;
            _requestStatusLogRepo.AddData(requestStatusLog);
            
        }

        public void AcceptViewAgreement(int requestid)
        {
            Request request = _requestRepo.GetRequest(requestid);
            request.Status = 4;
            _requestRepo.UpdateTable(request);

        }

    }
}
