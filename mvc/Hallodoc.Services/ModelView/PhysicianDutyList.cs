using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Repositories.DataModels;

namespace HallodocServices.ModelView
{
    public class PhysicianDutyList
    {
        public List<Physician>?  PhysicianOnCall { get; set; }

        public List<Physician>? PhysicianOnDuty { get; set; }
    }
}
