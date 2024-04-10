using HalloDoc.Repositories.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDoc.Repositories.Interfaces
{
    public interface IUserRepo
    {
        User GetUserData(int UserId);
        Task<User> UpdateTable(User user);
        bool CheckUser(string Email);
        Task<bool> AddTable(User user);
        int GetUserId(string email);
        List<User> GetPatientHistory(string FirstName, string LastName, string Email, string Mobile);
    }
}
