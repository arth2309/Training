using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Interfaces;

namespace HalloDoc.Repositories.Implementation
{
    public class RequestRepo : IRequestRepo
    {
        private readonly HalloDoc.Repositories.DataContext.ApplicationDbContext _dbcontext;

        public RequestRepo(HalloDoc.Repositories.DataContext.ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public int GetUid(int id)
        {
            User user = _dbcontext.Users.FirstOrDefault(a=>a.AspNetUserId == id);
            return user?.UserId??0;
        }
        
        public List<Request> GetAllRequests(int uid)
        {
            List<Request> requests = _dbcontext.Requests.Where(a=>a.UserId == uid).ToList();
            return requests;
        } 
        
        //public List<Request> GetNewStateName(int page, int pageSize, int status)
        //{
        //    List<Request> requests = _dbcontext.Requests.Where(a=>a.Status == status).ToList();
        //    return requests.Skip((page-1)*pageSize) .Take(pageSize).ToList();
        //}

        //public int GetCount()
        //{
        //    return _dbcontext.Requests.Where(a=>a.Status == 1).ToList().Count();
        //}

    }
}
