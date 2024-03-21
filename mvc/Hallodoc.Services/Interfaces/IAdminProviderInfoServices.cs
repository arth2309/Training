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
        Physician GetPhysicianData(int id);

        void SendEmail(string email, string description);
    }
}
