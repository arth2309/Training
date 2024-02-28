using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Repositories.DataModels;

namespace HalloDoc.Repositories.Interfaces
{
    public interface IRequestStatusLogRepo
    {
        Task<RequestStatusLog> AddData(RequestStatusLog requestStatusLog);
    }
}
