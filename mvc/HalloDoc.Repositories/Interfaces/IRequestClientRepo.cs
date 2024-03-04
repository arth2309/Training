using HalloDoc.Repositories.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDoc.Repositories.Interfaces
{
    public interface IRequestClientRepo
    {
        List<RequestClient> GetNewStateData(int status);
        int GetCount(int status);
        RequestClient GetViewCaseData(int id);
         Task<RequestClient> UpdateTable(RequestClient requestClient);
    }
}
