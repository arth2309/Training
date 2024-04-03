using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.ModelView
{
    public class ShiftForReviewVM
    {
       public PaginatedList<ShiftReviewList>? shiftDetails { get; set; }
    }
}
