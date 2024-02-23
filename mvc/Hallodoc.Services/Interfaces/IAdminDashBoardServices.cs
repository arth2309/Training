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
        List<NewState> getStates(int status);
        AdminDashBoard newStates(int status);
       
    }
}
