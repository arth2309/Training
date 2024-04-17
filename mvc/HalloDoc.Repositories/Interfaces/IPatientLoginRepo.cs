using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Repositories.DataModels;

namespace HalloDoc.Repositories.Interfaces
{
    public interface IPatientLoginRepo
    {
        int ValidateUser(string email, string password);
        string GetUserName(int id);

        AspNetUser aspNetUser(int id);

    }
}
