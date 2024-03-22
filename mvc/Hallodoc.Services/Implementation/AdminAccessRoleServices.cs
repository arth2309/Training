using HalloDoc.Repositories.Interfaces;
using HallodocServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Repositories.DataModels;
using HallodocServices.Interfaces;

namespace HallodocServices.Implementation
{
    public class AdminAccessRoleServices : IAdminAccessRoleServices
    {
        private readonly IMenuRepo _menuRepo;

        public  AdminAccessRoleServices (IMenuRepo menuRepo)
        {
            _menuRepo = menuRepo;
        }

        public AdminAccessRoleMV GetAccessRoleData()
        {
            AdminAccessRoleMV adminAccessRoleMV = new();
            List<Menu> menu = _menuRepo.getMenuByRole(0);
            List<AdminRoleMenu> adminRoleMenus = new();

            for(int i = 0; i < menu.Count; i++) 
            {
                AdminRoleMenu adminRoleMenu = new();
                adminRoleMenu.Name = menu[i].Name;
                adminRoleMenu.Roleid = menu[i].AccountType;
                adminRoleMenu.Menuid = menu[i].MenuId;
                adminRoleMenus.Add(adminRoleMenu);
            }
            adminAccessRoleMV.roleMenus = adminRoleMenus;
            return adminAccessRoleMV;
        }

        public  List<AdminRoleMenu> GetAccessRoleDataById(int id)
        {
            List<Menu> menu = _menuRepo.getMenuByRole(id);
            List<AdminRoleMenu> adminRoleMenus = new();

            if (menu.Count > 0)
            {
                for (int i = 0; i < menu.Count; i++)
                {
                    AdminRoleMenu adminRoleMenu = new();
                    adminRoleMenu.Name = menu[i].Name;
                    adminRoleMenu.Roleid = menu[i].AccountType;
                    adminRoleMenu.Menuid = menu[i].MenuId;
                    adminRoleMenus.Add(adminRoleMenu);
                }
                return adminRoleMenus;
            }
            else
            {
                return null;
            }
        }

    }
}
