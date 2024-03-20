﻿using System;
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

        public async Task<bool> AddData(AspNetUserRole userRole)
        {
            _context.AspNetUserRoles.Add(userRole);
            await _context.SaveChangesAsync();
            return true;
        }
        public bool CheckAspNetUser(string Email)
        {
            AspNetUser aspNetUser = _context.AspNetUsers.FirstOrDefault(u => u.Email == Email);
            return aspNetUser == null ? true : false;
        }

        public async Task<bool> AddTable(AspNetUser aspNetUser)
        {
            _context.AspNetUsers.Add(aspNetUser);
            await _context.SaveChangesAsync();
            return true;
        }

        public int GetId(string email)
        {
            AspNetUser aspNetUser = _context.AspNetUsers.FirstOrDefault(a=>a.Email == email);
            int id = aspNetUser != null ? aspNetUser.Id : 0;
            return id;
            
        }

        public AspNetUser GetData(int id)
        {
            return _context.AspNetUsers.FirstOrDefault(a => a.Id == id);
        }

        public async Task<bool> UpdateTable(AspNetUser aspNetUser)
        {
            _context.AspNetUsers.Update(aspNetUser);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
