using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Repositories.DataContext;
using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Interfaces;

namespace HalloDoc.Repositories.Implementation
{
    public class BlockedRequestRepo : IBlockedRequestRepo
    {
        private readonly ApplicationDbContext _DbContext;

        public BlockedRequestRepo(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public async Task<BlockRequest> AddData(BlockRequest blockRequest)
        {
            _DbContext.BlockRequests.Add(blockRequest);
            await _DbContext.SaveChangesAsync();
            return null;
        }
    }
}
