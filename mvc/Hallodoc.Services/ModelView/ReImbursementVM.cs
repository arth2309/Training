using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.ModelView
{
    public class ReImbursementVM
    {
        public DateTime? ShiftDate { get; set; }

        public string? Item { get; set; }

        public int? Amount { get; set; }

        public IFormFile? bill { get; set; }

        public int? PhysicianId { get; set; }

        public int? ReimbursementId { get; set; }   
    }
}
