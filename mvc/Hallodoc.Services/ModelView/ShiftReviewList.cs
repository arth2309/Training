using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.ModelView
{
    public class ShiftReviewList
    {

        public int? ShiftDetailId { get; set; }
        public string? PhysicianName { get; set; }

        public DateOnly shiftDate { get; set; }

        public TimeOnly startTime { get; set; }

        public TimeOnly endTime { get; set; }

        public string ? RegionName { get; set; }
    }
}
