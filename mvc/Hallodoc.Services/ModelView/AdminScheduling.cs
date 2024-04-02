using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.ModelView
{
    public class AdminScheduling
    {
        public SchedulingList? SchedulingList { get; set; }

        public List<string>? RepeatedDays { get; set; }


        public int? NoRepeat { get; set; }

        [Required(ErrorMessage = "shiftdate is required")]
        public DateTime ShiftDate { get; set; }

        [Required(ErrorMessage = "starttime is required")]
        public TimeOnly Start { get; set; }

        [Required(ErrorMessage = "endtime is required")]
        public TimeOnly End { get; set; }

        [Required(ErrorMessage ="please select region")]
        public int RegionId {  get; set; }

        [Required(ErrorMessage="please select physician")]
        public int PhysicianId { get; set; }

    }
}
