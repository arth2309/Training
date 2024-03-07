using HalloDoc.Repositories.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.ModelView
{
    public class AdminSendOrder
    {
        public List<HealthProfessional>? professionalList { get; set; }

        public List<HealthProfessionalType>? professionalTypeList { get; set; }

        public string? Prescription { get; set; }
        public int? refill {get; set; }
    }
}
