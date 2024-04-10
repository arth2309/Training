using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.ModelView
{
    public class PatientRecordList
    {
        public string? Name { get; set; }

        public DateOnly CreatedDate { get; set; }

        public string? ConfirmationNumber { get; set; }

        public string? ProviderName { get; set; }

        public DateOnly ConcludesdDate { get; set; }

        public short? status { get; set; }

        public int? RequestId { get; set; }

    }
}
