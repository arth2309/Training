using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.ModelView
{
    public class AdminAccessRoleMV
    {
       public List<AdminRoleMenu>?  roleMenus {  get; set; }

        public int? Accountype { get; set; }

        public string? RoleName {get; set; }

        public int RoleId { get; set; }

        public List<int>? checkedBoxes { get; set; }

        public bool IsUpdate { get; set; }
    }
}
