using HalloDoc.Repositories.DataContext;
using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDoc.Repositories.Implementation
{
    public class ShiftRepo : IShiftRepo
    {
        private readonly ApplicationDbContext _context;

        public ShiftRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Shift> AddDataInShift(Shift shift)
        {
            _context.Shifts.Add(shift);
            await _context.SaveChangesAsync();
            return shift;
        }

        public async Task<ShiftDetail> AddDataInShiftDetail(ShiftDetail shiftDetail)
        {
            _context.ShiftDetails.Add(shiftDetail);
            await _context.SaveChangesAsync();
            return shiftDetail;
        }

        public List<ShiftDetail> GetShiftDetail(int physicianid,DateTime dateTime)
        {
            return _context.ShiftDetails.Include(a=>a.Shift).Where(a=>a.Shift.PhysicianId == physicianid && DateOnly.FromDateTime(a.ShiftDate) == DateOnly.FromDateTime(dateTime)).ToList();
        }

        public List<ShiftDetail> GetShiftDetailByRegion(int Regionid)
        {
            if (Regionid > 0)
            {
                return _context.ShiftDetails.Include(a => a.Shift.Physician).Where(a => a.RegionId == Regionid).ToList();
            }
            else
            {
                return _context.ShiftDetails.Include(a => a.Shift).ToList();
            }
        }
    }
}
