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
    public class AspNetUserRepo : IAspNetUserRepo
    {
        private readonly ApplicationDbContext _context;

        public AspNetUserRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public string role (string email)
        {
            int id = _context.AspNetUsers.FirstOrDefault(a => a.Email == email).Id;
            int Roleid = _context.AspNetUserRoles.FirstOrDefault(a => a.UserId == id).RoleId;

            if(Roleid == 1) 
            {
                return "Admin";
            }
            else if(Roleid == 2) 
            {
                return "Provider";
            }
            else if (Roleid == 3)
            {
                return "Patient";
            }
            else
            {
                return "others";
            }
        }
    }
}
