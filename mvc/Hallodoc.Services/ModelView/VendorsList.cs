using HalloDoc.Repositories.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.ModelView
{
    public class VendorsList
    {
        public int? VendorId { get; set; }
       public string? professionName { get; set; }

        public string? BusinessName { get; set; }

        public string? Email { get; set; }

        public string? FaxNumber { get; set; }  

        public string? Mobile { get;set; }

        public string? BusinessContact { get; set; }
    }

}
