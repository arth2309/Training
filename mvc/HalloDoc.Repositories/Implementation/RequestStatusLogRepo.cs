using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Repositories.Interfaces;
using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.DataContext;

namespace HalloDoc.Repositories.Implementation
{
    public class RequestStatusLogRepo : IRequestStatusLogRepo
    {
        private readonly ApplicationDbContext _dbContext;
        public RequestStatusLogRepo (ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RequestStatusLog> AddData(RequestStatusLog requestStatusLog)
        {
            _dbContext.RequestStatusLogs.Add(requestStatusLog);
            await _dbContext.SaveChangesAsync();
            return new RequestStatusLog();
        }

    }
}
