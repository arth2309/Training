using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Repositories.DataContext;
using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
             return _context.Admins.Include(a=>a.AdminRegions).FirstOrDefault(a=>a.AdminId == adminid);
           
        }

        public async Task<bool> UpdateTable(Admin admin) 
        {
            _context.Admins.Update(admin);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<AdminRegion> AddDataInAdminRegion(AdminRegion adminRegion)
        {
            _context.AdminRegions.Add(adminRegion);
            await _context.SaveChangesAsync();
            return adminRegion;
        }

        public async Task<Admin> AddDataInAdmin(Admin admin)
        {
            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();
            return admin;
        }
    }
}
