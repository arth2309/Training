using HalloDoc.Repositories.PagedList;
using HallodocServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Interfaces
{
    public interface IProviderDashBoardServices
    {
        AdminDashBoard newStates(int status, int currentPage, int typeid, int physicianid, string name);

        PaginatedList<NewState> getStates(string Name, int TypeId, int Physicianid, int status, int currentPage);
    }
}
