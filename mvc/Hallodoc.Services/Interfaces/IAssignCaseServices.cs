using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using HallodocServices.ModelView;

namespace HallodocServices.Interfaces
{
    public interface IAssignCaseServices
    {
        AdminAssignCase AdminAssignCase(AdminAssignCase adminAssignCase);
    }
}
