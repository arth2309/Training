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
        private readonly IRoleRepo _roleRepo;

        public  AdminAccessRoleServices (IMenuRepo menuRepo,IRoleRepo roleRepo)
        {
            _menuRepo = menuRepo;
            _roleRepo = roleRepo;
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
            adminAccessRoleMV.IsUpdate = false;
            adminAccessRoleMV.RoleId = 0;
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

        public async Task<bool> CreateRole(AdminRoleMenu adminRoleMenu)
        {
            
            Role role = new();
            role.Name = adminRoleMenu.Name;
            role.CreatedDate = DateTime.Now;
            role.AccountType = short.Parse(adminRoleMenu.Roleid.ToString());
            role.CreatedBy = "1";
            role.IsDeleted = new System.Collections.BitArray(1, false);
            await _roleRepo.AddDataInRoleTable(role);


            for (int i = 0;i <adminRoleMenu.MenuLists.Count();i++)
            {
                RoleMenu roleMenu = new();
                roleMenu.MenuId = Int32.Parse(adminRoleMenu.MenuLists[i]);
                roleMenu.RoleId = role.RoleId;
                
               await  _roleRepo.AddDataInRoleMenuTable(roleMenu);
                
            }

            

            return true;
        }

        public async Task<bool> CreateUpdateRole(AdminAccessRoleMV adminAccessRoleMV)
        {
            if(adminAccessRoleMV.IsUpdate)
            {
                List<RoleMenu> roleMenus = _roleRepo.GetRoleMenuDataByroleid(adminAccessRoleMV.RoleId);

                for(int i = 0;i<roleMenus.Count;i++)
                {
                    bool IsAbsent = adminAccessRoleMV.checkedBoxes.Contains(roleMenus[i].MenuId)?false: true;
                    if (IsAbsent)
                    {
                        await _roleRepo.RemoveDataInRoleMenuTable(roleMenus[i]);
                    }
                }

                for(int i = 0;i<adminAccessRoleMV.checkedBoxes.Count;i++)
                {
                    bool IsAbsent = true;
                    for(int j = 0;j<roleMenus.Count;j++)
                    {
                        if (adminAccessRoleMV.checkedBoxes[i] == roleMenus[j].MenuId)
                        {
                            IsAbsent = false;
                            break;
                        }

                        if (IsAbsent)
                        {
                            RoleMenu roleMenu = new();
                            roleMenu.RoleId = adminAccessRoleMV.RoleId;
                            roleMenu.MenuId = adminAccessRoleMV.checkedBoxes[i];
                            await _roleRepo.AddDataInRoleMenuTable(roleMenu);
                        }
                    }

                    
                }


            }
            else
            {
                Role role = new();
                role.Name = adminAccessRoleMV.RoleName;
                role.CreatedDate = DateTime.Now;
                role.AccountType = short.Parse(adminAccessRoleMV.Accountype.ToString());
                role.CreatedBy = "1";
                role.IsDeleted = new System.Collections.BitArray(1, false);
                await _roleRepo.AddDataInRoleTable(role);


                for (int i = 0; i < adminAccessRoleMV.checkedBoxes.Count; i++)
                {
                    RoleMenu roleMenu = new();
                    roleMenu.MenuId = Int32.Parse(adminAccessRoleMV.checkedBoxes[i].ToString());
                    roleMenu.RoleId = role.RoleId;

                    await _roleRepo.AddDataInRoleMenuTable(roleMenu);

                }

            }
            return true;
        }

        public List<AdminAccountAccess> GetAdminAccountAccessList() 
        {
            List<AdminAccountAccess> adminAccountAccesses = new List<AdminAccountAccess>();
            List<Role> roles = _roleRepo.GetRoleData();

            if(roles!= null)
            {
                for(int i = 0;i < roles.Count;i++) 
                {
                    AdminAccountAccess adminAccountAccess = new AdminAccountAccess();
                    adminAccountAccess.accounttype = roles[i].AccountType;
                    adminAccountAccess.Name = roles[i].Name;
                    adminAccountAccess.roleid = roles[i].RoleId;

                    adminAccountAccesses.Add(adminAccountAccess);
                }
                return adminAccountAccesses;
            }
            return null;
        }

        public async Task<bool>  DeleteRole (int roleid)

        {
            List<RoleMenu> roleMenus = _roleRepo.GetRoleMenuDataByroleid(roleid);
            for(int i = 0; i < roleMenus.Count;i++) 
            {
                RoleMenu roleMenu = roleMenus[i];
                await _roleRepo.RemoveDataInRoleMenuTable(roleMenu);
            }

            Role role = _roleRepo.GetRoleById(roleid);
            role.IsDeleted = new System.Collections.BitArray(1, true);
            await _roleRepo.RemoveDataInRoleTable(role);
            return true;

        }

        public AdminAccessRoleMV EditAccessRoleData(int RoleId)
        {
           Role role = _roleRepo.GetRoleById (RoleId);
            int AccountType = role!=null ? role.AccountType : 0;
            AdminAccessRoleMV adminAccessRoleMV = new();
            List<Menu> menu = _menuRepo.getMenuByRole(AccountType);
            List<AdminRoleMenu> adminRoleMenus = new();
            List<int?> Menus = new List<int?>();

            List<RoleMenu> roleMenu = _roleRepo.GetRoleMenuDataByroleid(RoleId);

            for (int i = 0; i < roleMenu.Count; i++)
            {
                int Menu = roleMenu[i].MenuId;
                Menus.Add(Menu);
            }

            for (int i = 0; i < menu.Count; i++)
            {
                AdminRoleMenu adminRoleMenu = new();
                adminRoleMenu.Name = menu[i].Name;
                adminRoleMenu.Roleid = menu[i].AccountType;
                adminRoleMenu.Menuid = menu[i].MenuId;
                adminRoleMenu.SelectedMenu = Menus;
                adminRoleMenus.Add(adminRoleMenu);
            }

            

            
            adminAccessRoleMV.roleMenus = adminRoleMenus;
            adminAccessRoleMV.RoleName = role.Name;
            adminAccessRoleMV.Accountype = role.AccountType;
            adminAccessRoleMV.IsUpdate = true;
            return adminAccessRoleMV;
        }

        //public async Task<bool> UpdateTable(Admin)
    }
}
