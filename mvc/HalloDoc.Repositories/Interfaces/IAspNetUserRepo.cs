using HalloDoc.Repositories.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDoc.Repositories.Interfaces
{
    public interface IAspNetUserRepo
    {
        string role(string email);
        Task<bool> AddData(AspNetUserRole userRole);
        bool CheckAspNetUser(string Email);
        Task<bool> AddTable(AspNetUser aspNetUser);
    }
}
