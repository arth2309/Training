using HalloDoc.Repositories.PagedList;
using HallodocServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Interfaces
{
    public interface IEmailLogServices
    {
        EmailLogVM GetLogs();
        PaginatedList<EmailLogList> GetLogsFilter(string Name, string Email, DateOnly Sdate, DateOnly Cdate, int RoleId, int CurrentPage);
    }
}
