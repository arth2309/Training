using HalloDoc.Repositories.DataModels;
using HallodocServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Interfaces
{
    public interface ISendOrderServices
    {
        //AdminSendOrder GetList(int id);
        List<HealthProfessionalType> GetProfessionalTypes();

        List<HealthProfessional> GetHealthProfessionalByType(int professionalTypeId);

        HealthProfessional GetProfessionalById(int vendorId);
         Task<bool> AddDataServices(AdminSendOrder adminSendOrder);
    }
}
