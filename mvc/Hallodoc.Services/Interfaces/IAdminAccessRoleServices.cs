using HallodocServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Interfaces
{
    public interface IAdminAccessRoleServices
    {
        AdminAccessRoleMV GetAccessRoleData();
        List<AdminRoleMenu> GetAccessRoleDataById(int id);

        Task<bool> CreateRole(AdminRoleMenu adminRoleMenu);

        List<AdminAccountAccess> GetAdminAccountAccessList();

        Task<bool> DeleteRole(int roleid);

        AdminAccessRoleMV EditAccessRoleData(int RoleId);

        Task<bool> CreateUpdateRole(AdminAccessRoleMV adminAccessRoleMV);
    }
}
