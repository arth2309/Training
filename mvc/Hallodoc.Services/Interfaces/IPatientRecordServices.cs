using HalloDoc.Repositories.PagedList;
using HallodocServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Interfaces
{
    public interface IPatientRecordServices
    {
        PatientRecordVM GetPatientRecord(int UserId);
        PaginatedList<PatientRecordList> GetRecordFilter(int UserId, int CurrentPage);
    }
}
