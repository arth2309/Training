using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Repositories.DataModels;
using HallodocServices.ModelView;

namespace HallodocServices.Interfaces
{
    public interface IAssignCaseServices
    {
        Task<AdminAssignCase> AdminAssignCase(AdminAssignCase adminAssignCase);
        List<Region> GetRegions();
        List<Physician> GetPhysciansByRegions(int regionid);
    }
}
