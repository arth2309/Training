using HallodocServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Interfaces
{
     public interface IAdminProfileServices
    {
        AdminProfile GetAdminData(int adminid);
        Task<bool> ResetPassword(int id, string password);
        Task<bool> UpdateInformation(int id, int adminid, string firstname, string lastname, string email, string Mobile);
        Task<bool> UpDateBillingInformation(int adminid, string address1, string address2, string city, string zip, string altphone);
        
        }
}
