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
        public RequestClient requestClient1(int reqid)
        {
            RequestClient requestClient = _dbcontext.RequestClients.FirstOrDefault(a=>a.RequestId == reqid);
            return requestClient ?? null;
        }
        public List<RequestClient> GetNewStateData(int status,int typeid,int regionid)
        {
            if (typeid == 0 && regionid == 0)
            {
                return _dbcontext.RequestClients.Include(a => a.Request).Where(a => a.Request.Status == status).ToList();
            }
            else if(regionid == 0)
            {
                return _dbcontext.RequestClients.Include(a => a.Request).Where(a => a.Request.Status == status && a.Request.RequestTypeId == typeid).ToList();
            }
            else if(typeid == 0)
            {
                return _dbcontext.RequestClients.Include(a => a.Request).Where(a => a.Request.Status == status && a.RegionId == regionid).ToList();
            }
            return _dbcontext.RequestClients.Include(a => a.Request).Where(a => a.Request.Status == status && a.Request.RequestTypeId == typeid && a.RegionId == regionid).ToList();

        }
        public int GetCount(int status)
        {
            return _dbcontext.RequestClients.Include(a=>a.Request).Where(a => a.Request.Status == status).ToList().Count();
        }
        public RequestClient GetViewCaseData(int id)
        {
            RequestClient requestClient = _dbcontext.RequestClients.Include(a=>a.Request).FirstOrDefault(a=>a.RequestClientId == id);
            return requestClient;
        }

        public async Task <RequestClient> UpdateTable(RequestClient requestClient)
        {
            _dbcontext.RequestClients.Update(requestClient);
            await _dbcontext.SaveChangesAsync();
            return requestClient;
        }
        public async Task<bool> AddTable(RequestClient requestClient)
        {
            _dbcontext.RequestClients.Add(requestClient);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

    }
}
