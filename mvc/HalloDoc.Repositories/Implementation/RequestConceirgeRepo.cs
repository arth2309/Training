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
    public class RequestConceirgeRepo : IRequestConceirgeRepo
    {
        private readonly ApplicationDbContext _context;
        public RequestConceirgeRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddRequestConciergeData(RequestConcierge requestConcierge)
        {
            _context.RequestConcierges.Add(requestConcierge);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddConceirgeData(Concierge concierge)
        {
            _context.Concierges.Add(concierge);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
