using HalloDoc.Repositories.PagedList;
using HallodocServices.ModelView;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Interfaces
{
    public interface ISearchRecordServices
    {
        SearchRecordVM GetList();

        PaginatedList<SearchRecordList> GetListFilter(string PatientName, string ProviderName, int TypeId, string Email, string Mobile, DateOnly FDate, DateOnly TDate, int CurrentPage);

        DataTable getExportData(string PatientName, string ProviderName, int TypeId, string Email, string Mobile, DateOnly FDate, DateOnly TDate);

        Task<int> Delete(int RequestId);

        }
}
