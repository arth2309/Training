using HalloDoc.Repositories.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.ModelView
{
    public class SearchRecordVM
    {
        public PaginatedList<SearchRecordList>? lists { get; set; }
    }
}
