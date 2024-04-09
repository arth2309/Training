using HalloDoc.Repositories.PagedList;
using HallodocServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Interfaces
{
    public interface IBlockHistoryServices
    {
        public BlockedHistoryVM GetBlockHistoryData();

        public PaginatedList<BlockedList> GetBlockHistoryDataFilter(string name, string mobile, string email, DateOnly date, int currentPage);

        Task<int> UnblockRequest(int RequestId);



    }
}
