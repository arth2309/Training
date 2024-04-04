using HalloDoc.Repositories.PagedList;
using HallodocServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Interfaces
{
    public interface ISchedulingServices
    {
        AdminScheduling GetData(int regionid, DateTime dateTime);

        List<SchedulingList> GetSchedulingList(int regionid, DateTime dateTime);

        Task<AdminScheduling> CreateShift(AdminScheduling scheduling);

        ShiftForReviewVM GetDataForReviewShift();

        PaginatedList<ShiftReviewList> FilterDataForReviewShift(int currentPage, int regionid, bool CurrentMonth);

        Task<List<int>> ApproveShiftServices(List<int> List);

        Task<List<int>> DeleteShiftServices(List<int> List);
    }
}
