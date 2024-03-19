using HallodocServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Interfaces
{
    public interface ICloseCaseServices
    {
        AdminCloseCase GetCloseCaseData(int requestid);
        Task<bool> UpdateCloseData(AdminCloseCase adminClose);

        Task<bool> UpdateStatus(int requestid);
    }
}
