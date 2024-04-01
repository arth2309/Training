using HalloDoc.Repositories.DataModels;
using HallodocServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Interfaces
{
    public interface IAdminProviderInfoServices
    {
        AdminProviderInfo GetProviderInfo();

        ProviderList GetProviderList(int regionid);
        Physician GetPhysicianData(int id);

        Task<bool> NotificationServices(int id, bool IsNotificationChecked);

        void SendEmail(string email, string description);
    }
}
