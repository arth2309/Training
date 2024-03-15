using HalloDoc.Repositories.DataContext;
using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDoc.Repositories.Implementation
{
    public class RequestBusinessRepo :IRequestBusinessRepo
    {
        private readonly ApplicationDbContext _context;
        public RequestBusinessRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddRequestBusinessData(RequestBusiness requestBusiness)
        {
            _context.RequestBusinesses.Add(requestBusiness);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddBusinessData(Business business)
        {
            _context.Businesses.Add(business);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
