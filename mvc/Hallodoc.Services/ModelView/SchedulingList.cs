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

        public int Id { get; set; }
        public string? PhysicianName { get; set; }

        public DateOnly ShiftDate { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }
    }
}
