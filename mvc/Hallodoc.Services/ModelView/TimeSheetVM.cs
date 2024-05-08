using HalloDoc.Repositories.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.ModelView
{
    public class TimeSheetVM
    {
        public List<TimeSheetDateListVM>? dateList { get; set; }

        public List<TimeSheetListVM>? timeSheetList { get; set; }

        public List<Reimbursement>? reimbursements { get; set; }

        public List<Physician>? physicians { get; set; }
    }
}
