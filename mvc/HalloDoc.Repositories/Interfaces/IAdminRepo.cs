using HalloDoc.Repositories.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDoc.Repositories.Interfaces
{
    public interface IAdminRepo
    {
        Admin GetAdminData(int adminid);
        Task<bool> UpdateTable(Admin admin);

        Task<AdminRegion> AddDataInAdminRegion(AdminRegion adminRegion);

        Task<Admin> AddDataInAdmin(Admin admin);
    }
}
