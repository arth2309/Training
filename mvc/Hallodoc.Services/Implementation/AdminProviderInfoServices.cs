using HalloDoc.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HallodocServices.ModelView;
using HallodocServices.Interfaces;
using HalloDoc.Repositories.DataModels;
using Irony.Parsing;
using System.Net.Mail;
using System.Net;

namespace HallodocServices.Implementation
{
    public class AdminProviderInfoServices : IAdminProviderInfoServices
    {
        private readonly IPhysicianRepo _physicianRepo;

        public AdminProviderInfoServices(IPhysicianRepo physicianRepo)
        {
            _physicianRepo = physicianRepo;
        }

        public AdminProviderInfo GetProviderInfo()
        {
            AdminProviderInfo info = new();
            info.physicians = _physicianRepo.GetPhysiciansList();
            return info;
        }

        public Physician GetPhysicianData(int id)
        {
            Physician physcian = _physicianRepo.GetPhysician(id);
            return physcian;
        }

        public void SendEmail(string email,string description) 
        {
            
            MailMessage mm = new MailMessage("tatva.dotnet.arthgandhi@outlook.com", "arthgandhi7@gmail.com");
            
            mm.Subject = "Agreement";
            mm.Body = string.Format(description);
            mm.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.office365.com";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(userName: "tatva.dotnet.arthgandhi@outlook.com", password: "Liony@2002");
            smtp.Port = 587;
            smtp.Send(mm);
        }
    }
}
