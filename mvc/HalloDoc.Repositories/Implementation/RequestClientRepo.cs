﻿using System;
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

        public List<RequestClient> requestClient()
        {
            List<RequestClient> requestClient = _dbcontext.RequestClients.Include(a=>a.Request).ToList();
            return requestClient;
        }


        public List<RequestClient> GetNewStateData(int status,int typeid,int regionid,string name)
        {
           
            Func<RequestClient, bool> predicate = a =>
            (typeid == 0 || a.Request.RequestTypeId == typeid) &&
            (regionid == 0 || a.RegionId == regionid) &&
            (name == null || a.FirstName.Contains(name) || a.LastName.Contains(name)) &&
            ((status == 1 && a.Request.Status == 1 && a.Request.PhysicianId == null) || a.Request.Status == status);
            return _dbcontext.RequestClients.Include(r => r.Request).Include(p=>p.Request.Physician).Include(a=>a.Request.User).Where(predicate).ToList();


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

        public List<RequestClient> BlockHistoryList(string name, string Mobile, DateOnly date, string email)
        {
            DateTime dateTime = new DateTime(0001,01,01);
            DateOnly dateOnly = DateOnly.FromDateTime(dateTime);

            Func<RequestClient, bool> predicate = a => (name == null || a.FirstName.Contains(name) || a.LastName.Contains(name)) &&
                     (Mobile == null || a.PhoneNumber.Contains(Mobile)) &&
                     (email == null || a.Email.Contains(email)) &&
                     (date == dateOnly || DateOnly.FromDateTime(a.Request.CreatedDate) == date) &&
                     (a.Request.Status == 10);

            return _dbcontext.RequestClients.Include(a=>a.Request).Where(predicate).ToList(); 

          

        }

        public List<RequestClient> SearchRecordList(string PatientName, string ProviderName, string Email, string PhoneNumber, int RequestTypeId, DateOnly ToService, DateOnly FromService)
        {
            DateOnly dateOnly = new DateOnly(0001,01,01);

            Func<RequestClient, bool> predicate = a => (PatientName == null || a.FirstName.Contains(PatientName) || a.LastName.Contains(PatientName)) &&
                                                 (ProviderName == null || (ProviderName == null && a.Request.Physician == null) || ( a.Request.Physician !=null && a.Request.Physician.FirstName.Contains(ProviderName)) || (a.Request.Physician != null && a.Request.Physician.LastName.Contains(ProviderName))) &&
                                                 (Email == null || a.Email.Contains(Email)) &&
                                                 (PhoneNumber == null || a.PhoneNumber.Contains(PhoneNumber)) &&
                                                 (RequestTypeId == 0 || a.Request.RequestTypeId == RequestTypeId) &&
                                                 (FromService == dateOnly  || DateOnly.FromDateTime(a.Request.CreatedDate) >= FromService)&&
                                                 (ToService == dateOnly || DateOnly.FromDateTime(a.Request.CreatedDate) <= ToService) &&
                                                 (a.Request.IsDeleted == null || !a.Request.IsDeleted[0]);

                                 return _dbcontext.RequestClients.Include(a=>a.Request).Include(a=>a.Request.RequestNotes).Include(a=>a.Request.RequestStatusLogs).Include(a=>a.Request.Physician).Where(predicate).ToList();               
        }

        public List<RequestClient> ProviderDashBoardList(string Name,int typeId,List<int> statuses,int PhysicianId)
        {
            Func<RequestClient,bool> predicate = a=> (Name == null || a.FirstName.Contains(Name) || a.LastName.Contains(Name)) &&
                                                      (typeId == 0 || a.Request.RequestTypeId == typeId)  &&
                                                      (a.Request.PhysicianId == PhysicianId) &&
                                                       (statuses.Contains(a.Request.Status));

            return _dbcontext.RequestClients.Include(a=>a.Request).Where(predicate).ToList();
        }


    }
}
