using HalloDoc.Repositories.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.ModelView
{
    public class TimeSheetListVM
    {
      public  int? ShiftCount { get; set; }
      public DateTime? ShiftDate { get; set; }

      public bool IsSubmit { get; set; }
      
     public double? callHours { get; set; }

        public double TotalHour { get; set; }
        public int? NumberOfHouseCall { get; set; }

        public int? NumberOfPhoneConsult { get; set; }

        public bool? IsWeekend { get; set; }

        public List<double>? TotalHours { get; set; }
        public List<int>? NumberOfHouseCalls { get; set; }

     public List<int>? NumberOfPhoneConsults { get; set; }

     public List<DateTime>? IsHoliday {get; set; }

    public int PhysicianId { get; set; }
     
    public  DateTime StartDate { get; set; }
        
    public DateTime EndDate { get; set;}
    }
}
