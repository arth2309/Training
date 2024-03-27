using HalloDoc.Repositories.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDoc.Repositories.Interfaces
{
    public interface IRoleRepo
    {
        Task<bool> AddDataInRoleTable(Role role);
        Task<bool> AddDataInRoleMenuTable(RoleMenu roleMenu);

        List<Role> GetRoleData();

        Task<bool> RemoveDataInRoleMenuTable(RoleMenu roleMenu);

        List<RoleMenu> GetRoleMenuDataByroleid(int roleid);

        Task<bool> RemoveDataInRoleTable(Role role);

        Role GetRoleById(int roleid);

        List<Role> GetRoleDataForAdmin();


    }
}
