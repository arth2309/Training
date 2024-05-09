using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using HalloDoc.Repositories.DataContext;

namespace HalloDoc.Repositories.Implementation;

public class PatientLoginRepo : IPatientLoginRepo
{
    private readonly ApplicationDbContext _dbcontext;

    public PatientLoginRepo(ApplicationDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

   public int ValidateUser(string email, string password)
    {
        AspNetUser userfromdb = _dbcontext.AspNetUsers.FirstOrDefault(user => user.Email == email && user.PasswordHash == password);
        return userfromdb?.Id??0;
    }

    public string GetUserName(int id)
    {
        string username = _dbcontext.AspNetUsers.FirstOrDefault(a => a.Id == id).UserName;
        return username;
    }

    public AspNetUser aspNetUser(int id)
    {
        return _dbcontext.AspNetUsers.Include(a => a.AspNetUserRole).Include(a => a.AdminAspNetUsers).Include(a => a.UserAspNetUsers).Include(a => a.PhysicianAspNetUsers).FirstOrDefault(a => a.Id == id);

    }
}
