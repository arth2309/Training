using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.ModelView
{
    public class BusinessVM
    {
        [Required(ErrorMessage ="Business Name is required")]
        public string? BusinessName { get; set; }

        [Required(ErrorMessage = "please select profession")]
        public int? ProfessionId { get; set; }

        [Required(ErrorMessage = "Fax Number is required")]
        public string? FaxNumber { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        public string? Mobile { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Business Contact is required")]
        public string? BusinessContact { get; set; }

        [Required(ErrorMessage = "Street is required")]
        public string? Street { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string? City { get; set; }

        [Required(ErrorMessage = "please select state")]
        public int? regionid { get; set; }

        [Required(ErrorMessage = "ZipCode is required")]
        public string? ZipCode { get; set; }

        public int? VendorId { get; set; }
    }
}
