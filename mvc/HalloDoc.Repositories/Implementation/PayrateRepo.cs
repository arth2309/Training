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
    public class PayrateRepo : IPayrateRepo
    {
        private readonly ApplicationDbContext _context;

        public PayrateRepo(ApplicationDbContext context) 
        {
            _context = context;
        }

        public PhysicianPayrate  GetData(int PhysicianId)
        {
            return _context.PhysicianPayrates.FirstOrDefault(a => a.PhysicianId == PhysicianId);

        }

        public async Task<bool> AddData(PhysicianPayrate physicianPayrate)
        {
             _context.PhysicianPayrates.Add(physicianPayrate);
             await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateData(PhysicianPayrate physicianPayrate)
        {
            _context.PhysicianPayrates.Update(physicianPayrate);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
