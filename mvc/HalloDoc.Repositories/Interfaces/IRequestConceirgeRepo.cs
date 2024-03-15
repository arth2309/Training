using HalloDoc.Repositories.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDoc.Repositories.Interfaces
{
    public interface IRequestConceirgeRepo
    {
        Task<bool> AddRequestConciergeData(RequestConcierge requestConcierge);
        Task<bool> AddConceirgeData(Concierge concierge);
    }
}
