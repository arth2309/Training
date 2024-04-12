using HalloDoc.Repositories.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.ModelView
{
    public class MonthSchedulingVM
    {
        public List<ShiftDetail>? GetDetail { get; set; }
        
        public int? startDay { get; set; }
    }
}
