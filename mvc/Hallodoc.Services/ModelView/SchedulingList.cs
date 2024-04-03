using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Repositories.DataModels;

namespace HallodocServices.ModelView
{
    public class SchedulingList
    {
        public Physician?  GetPhysicians { get; set; }

        public List<ShiftDetail>? GetShiftDetail { get; set; }
    }
}
