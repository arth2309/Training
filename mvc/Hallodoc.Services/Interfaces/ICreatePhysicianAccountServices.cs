using HallodocServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Interfaces
{
    public interface ICreatePhysicianAccountServices
    {
        AdminProfile GetLists();
       Task<AdminProfile> CreatePhysicianAccount(AdminProfile adminProfile);

        AdminProfile GetPhysician(int PhysicianId);

        Task<bool> ResetPassword(int Id,string Password);

        Task <bool> EditProviderAccountInformation(AdminProfile adminProfile);

        Task<bool> EditPhysicianInformation(AdminProfile adminProfile);
    }
}
