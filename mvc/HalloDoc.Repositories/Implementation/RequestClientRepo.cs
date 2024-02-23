using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Repositories.Interfaces;
using HalloDoc.Repositories.DataModels;
using Microsoft.EntityFrameworkCore;

namespace HalloDoc.Repositories.Implementation
{
    public class RequestClientRepo : IRequestClientRepo
    {
        private readonly HalloDoc.Repositories.DataContext.ApplicationDbContext _dbcontext;

        public RequestClientRepo(HalloDoc.Repositories.DataContext.ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public List<RequestClient> GetNewStateData(int status)
        {
              return  _dbcontext.RequestClients.Include(a=>a.Request).Where(a=>a.Request.Status == status).ToList();
              
        }
        public int GetCount()
        {
            return _dbcontext.RequestClients.Include(a=>a.Request).Where(a => a.Request.Status == 1).ToList().Count();
        }


    }
}
