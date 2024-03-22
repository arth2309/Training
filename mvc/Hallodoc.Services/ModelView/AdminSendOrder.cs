using HalloDoc.Repositories.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.ModelView
{
    public class AdminSendOrder
    {
        [Required(ErrorMessage ="Please Select Profession")]
        public int ProfessionTypeId { get; set; }

        [Required(ErrorMessage = "Please Select Vendor")]
        public int VendorId { get; set; }

        public int RequestId { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string FaxNumber { get; set; }

        [Required]
        public string? Prescription { get; set; }

        public int? Refill {get; set; }
    }
}
