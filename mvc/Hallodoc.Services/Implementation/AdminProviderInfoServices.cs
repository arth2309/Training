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
            ProviderList providerList = new();
            providerList.physicianNotifications = _physicianRepo.GetListForNotifications(0);
            info.providerList = providerList;

            return info;
        }

        public ProviderList GetProviderList(int regionid)
        {
            ProviderList providerList = new();
            List<PhysicianNotification> physicians = _physicianRepo.GetListForNotifications(regionid);
            providerList.physicianNotifications = physicians;   
            return providerList;
        }

        public Physician GetPhysicianData(int id)
        {
            Physician physcian = _physicianRepo.GetPhysician(id);
            return physcian;
        }

        public async Task<bool> NotificationServices(int id,bool IsNotificationChecked)
        {
            PhysicianNotification physicianNotification = _physicianRepo.GetPhysicianNotificationData(id);
            physicianNotification.IsNotificationStopped = new System.Collections.BitArray(1, IsNotificationChecked);
            await _physicianRepo.UpdateDataInPhysicianNotification(physicianNotification);
           return true;

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
