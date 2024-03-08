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
        public int ProfessionTypeId { get; set; }

        public int VendorId { get; set; }

        public int RequestId { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; } 

        public string FaxNumber { get; set; }

        public string? Prescription { get; set; }

        public int? Refill {get; set; }
    }
}
