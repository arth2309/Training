using HalloDoc.Repositories.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.ModelView
{
    public class AdminDashBoard
    {
        public PaginatedList<NewState> AdminNewState { get; set; }

        public int? NewCount { get; set; }
        public int? PendingCount { get; set; }
        public int? ActiveCount { get; set; }
        public int? ConcludeCount { get; set; }
        public int? ToCloseCount { get; set; }
        public int? UnPaidCount { get; set; }
    }
}
