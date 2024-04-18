using HalloDoc.Repositories.DataModels;
using HallodocServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Interfaces
{
    public interface IEncounterFormServices
    {
        AdminEncounterForm GetEncounterFormData(int requestid);

        Task<bool> HouseCall(int RequestId);

        byte[] GeneratePDFServices(AdminEncounterForm encounterDetails);

        Task<bool> UpdateaddEncounterFormData(AdminEncounterForm encounter);



    }
}
