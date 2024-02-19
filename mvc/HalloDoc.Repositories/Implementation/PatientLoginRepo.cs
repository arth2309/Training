using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HalloDoc.Repositories.Implementation;

public class PatientLoginRepo : IPatientLoginRepo
{
    private readonly HalloDoc.Repositories.DataContext.ApplicationDbContext _dbcontext;

    public PatientLoginRepo(HalloDoc.Repositories.DataContext.ApplicationDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

   public int ValidateUser(string email, string password)
    {
        int id = _dbcontext.AspNetUsers.FirstOrDefault(user => user.Email == email && user.PasswordHash == password).Id;
        return id;
    }
}
