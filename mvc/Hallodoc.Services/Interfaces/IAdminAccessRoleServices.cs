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
    }
}
