using HalloDoc.Repositories.PagedList;
using HallodocServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Interfaces
{
    public interface IPatientHistoryServices
    {
        PatientHistoryVM GetList();

        PaginatedList<PatientHistoryList> GetListFilter(string FirstName, string LastName, string Email, string Mobile, int CurrentPage);
    }
}
