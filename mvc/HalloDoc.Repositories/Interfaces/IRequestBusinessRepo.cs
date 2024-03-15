using HalloDoc.Repositories.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDoc.Repositories.Interfaces
{
    public interface IRequestBusinessRepo
    {
        Task<bool> AddRequestBusinessData(RequestBusiness requestBusiness);
        Task<bool> AddBusinessData(Business business);
    }
}
