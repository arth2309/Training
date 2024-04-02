using HallodocServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Interfaces
{
    public interface ISchedulingServices
    {
        AdminScheduling GetData(int regionid);
        SchedulingList GetSchedulingList(int regionid);

        Task<AdminScheduling> CreateShift(AdminScheduling scheduling);
    }
}
