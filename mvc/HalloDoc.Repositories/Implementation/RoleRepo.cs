using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Repositories.DataContext;
using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Interfaces;

namespace HalloDoc.Repositories.Implementation
{
    public class RoleRepo : IRoleRepo
    {
        private readonly ApplicationDbContext _context;

        public RoleRepo(ApplicationDbContext context)
        {
            _context = context;

        }

        public async Task<bool> AddDataInRoleTable(Role role) 
        {
            
                _context.Roles.Add(role);
                await _context.SaveChangesAsync();
                return true;
            
            
        }

        public async Task<bool> AddDataInRoleMenuTable(RoleMenu roleMenu)
        {
            
 
                _context.RoleMenus.Add(roleMenu);
                await _context.SaveChangesAsync();
                return true;
            
        }

        public List<Role> GetRoleData()
        {
            return _context.Roles.Where(a=>a.IsDeleted == new System.Collections.BitArray(1,false)).ToList();
        }

        public Role GetRoleById(int roleid)
        {
            return _context.Roles.FirstOrDefault(a => a.RoleId == roleid);
        }
        public List<RoleMenu> GetRoleMenuDataByroleid(int roleid)
        {
            return _context.RoleMenus.Where(a=>a.RoleId == roleid).ToList();
        }

        public async Task<bool> RemoveDataInRoleTable(Role role)
        {


            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> RemoveDataInRoleMenuTable(RoleMenu roleMenu)
        {

            _context.RoleMenus.Remove(roleMenu);
            await _context.SaveChangesAsync();
            return true;

        }

        public List<Role> GetRoleDataForAdmin()
        {
            return _context.Roles.Where(a=>a.AccountType == 1 && a.IsDeleted != new System.Collections.BitArray(1,true)).ToList();
        }
        public List<Role> GetRoleDataForPhysician()
        {
            return _context.Roles.Where(a => a.AccountType == 2 && a.IsDeleted != new System.Collections.BitArray(1, true)).ToList();
        }
    }
}
