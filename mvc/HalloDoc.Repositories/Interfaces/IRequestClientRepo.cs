﻿using HalloDoc.Repositories.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDoc.Repositories.Interfaces
{
    public interface IRequestClientRepo
    {

        RequestClient requestClient1(int reqid);
        List<RequestClient> GetNewStateData(int status,int typeid,int regionid,string name);
        int GetCount(int status);
        RequestClient GetViewCaseData(int id);
         Task<RequestClient> UpdateTable(RequestClient requestClient);
        Task<bool> AddTable(RequestClient requestClient);
        List<RequestClient> requestClient();

        List<RequestClient> BlockHistoryList(string name, string Mobile, DateOnly date, string email);

        List<RequestClient> SearchRecordList(string PatientName, string ProviderName, string Email, string PhoneNumber, int RequestTypeId, DateOnly ToService, DateOnly FromService);
    }
}
