using HallodocServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Interfaces
{
    public interface IViewCaseServices
    {
        AdminViewCase GetAdminViewCaseData(int id);
        AdminViewCase EditViewData(AdminViewCase adminViewCase);
        AdminViewCase CancelViewData(int rid);
    }
}
