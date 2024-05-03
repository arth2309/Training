using HallodocServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Interfaces
{
    public interface IInvoicingServices
    {
        TimeSheetVM GetTimeSheet(int id);

        List<TimeSheetListVM> GetTimeSheetList(int id, DateTime startDate);

        Task<bool> SubmitWeeklyList(TimeSheetListVM timeSheetListVM);
    }
}
