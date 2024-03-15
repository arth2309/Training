﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HalloDoc.Repositories.Implementation
{
    

    public class UserRepo : IUserRepo
    {
        private readonly HalloDoc.Repositories.DataContext.ApplicationDbContext _dbcontext;

        public UserRepo(HalloDoc.Repositories.DataContext.ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public User GetUserData(int UserId)
        {
            return _dbcontext.Users.FirstOrDefault(a => a.UserId == UserId);
        }

        public async Task<User> UpdateTable(User user)
        {
            _dbcontext.Users.Update(user);
            await _dbcontext.SaveChangesAsync();
            return user;
        }

        public bool CheckUser(string Email)
        {
            User user = _dbcontext.Users.FirstOrDefault(u => u.Email == Email);
             return user == null?true:false;
        }

        public async Task<bool> AddTable(User user)
        {
            _dbcontext.Users.Add(user);
            await _dbcontext.SaveChangesAsync();
            return true;
        }
    }
}
