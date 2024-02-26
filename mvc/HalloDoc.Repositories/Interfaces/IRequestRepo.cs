using HalloDoc.Repositories.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDoc.Repositories.Interfaces
{
    public interface IRequestRepo
    {
        int GetUid(int id);
        List<Request> GetAllRequests(int uid);
        Task<Request> UpdateTable(Request request);
        Request GetRequest(int id);


    }
}
