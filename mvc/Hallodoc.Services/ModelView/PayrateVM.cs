using HalloDoc.Repositories.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.ModelView
{
    public class PayrateVM
    {
        
        public int? PayrateId { get; set; }

        public int PhysicianId { get; set; }

        [Required]
        public int NightShiftWeekend { get; set; }
        [Required]
        public int Shift { get; set; }
        [Required]
        public int HouseCallsNightWeekend { get; set; }
        [Required]
        public int PhoneConsults { get; set; }
        [Required]
        public int? PhoneConsultsNightWeekend { get; set; }
        [Required]
        public int? BatchTesting { get; set; }
        [Required]
        public int? HouseCalls { get; set; }

    }
}
