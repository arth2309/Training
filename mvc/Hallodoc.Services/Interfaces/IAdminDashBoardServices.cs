using HalloDoc.Repositories.PagedList;
using HallodocServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Interfaces
{
    public interface IAdminDashBoardServices
    {
        PaginatedList<NewState> getStates(int status,int currentPage, int typeid, int regionid,string name);
        AdminDashBoard newStates(int status,int currentPage, int typeid, int regionid, string name);
       
    }
}
