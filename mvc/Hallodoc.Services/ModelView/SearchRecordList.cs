using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.ModelView
{
    public class SearchRecordList
    {
        public string? PatientName { get; set; }

        public string? Requestor { get; set; }

        public DateOnly? DateOfService { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public string? Zip { get; set; }

        public string? Physician { get; set; }

        public string? PhysicianNote { get; set; }

        public string? ProviderCancelledNote { get; set; }

        public string? AdminNote { get; set; }

        public string? PatientNote { get; set; }

        public int? RequestId { get; set; }

    }
}
