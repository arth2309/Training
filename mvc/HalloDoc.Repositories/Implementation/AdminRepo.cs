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
    public class AdminRepo : IAdminRepo
    {
        private readonly ApplicationDbContext _context;
        
        public AdminRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public Admin GetAdminData(int adminid)
        {
             return _context.Admins.FirstOrDefault(a=>a.AdminId == adminid);
           

        }

        public async Task<bool> UpdateTable(Admin admin) 
        {
            _context.Admins.Update(admin);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
