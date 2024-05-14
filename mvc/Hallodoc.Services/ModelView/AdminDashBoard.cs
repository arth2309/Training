using HalloDoc.Repositories.PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public int RId { get; set; }

        [Required]
        public string? Description { get; set; }

        public List<ChatVM>? ChatVMs { get; set; }
    }
}
