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

        public ShiftDetail GetShiftDetailData(int id)
        {
            return _context.ShiftDetails.FirstOrDefault(a => a.ShiftDetailId == id);
        }

        public async Task<ShiftDetail> UpdateShiftDetailData(ShiftDetail shiftDetail)
        {
            _context.ShiftDetails.Update(shiftDetail);
            await _context.SaveChangesAsync();
            return shiftDetail;
        }

        

        public List<ShiftDetail> GetShiftDetail(int physicianid, DateTime dateTime)
        {
            return _context.ShiftDetails.Include(a => a.Shift).Where(a => a.Shift.PhysicianId == physicianid && DateOnly.FromDateTime(a.ShiftDate) == DateOnly.FromDateTime(dateTime)).ToList();
        }

        public List<ShiftDetail> GetShiftDetailByRegion(int Regionid,bool CurrentMonth)
        {

            if(Regionid > 0 && CurrentMonth == true) 
            {
                return _context.ShiftDetails.Include(a => a.Shift).ThenInclude(a => a.Physician).Where(a => a.RegionId == Regionid && a.ShiftDate.Month == DateTime.Now.Month && a.Status == 1 && a.IsDeleted != new System.Collections.BitArray(1,true)).ToList();
            }
            else if(Regionid > 0 &&CurrentMonth == false) 
            {
                return _context.ShiftDetails.Include(a => a.Shift).ThenInclude(a => a.Physician).Where(a => a.RegionId == Regionid && a.Status == 1 && a.IsDeleted != new System.Collections.BitArray(1, true)).ToList();
            }
            else if(Regionid <= 0 && CurrentMonth == true)
            {
                return _context.ShiftDetails.Include(a => a.Shift).ThenInclude(a => a.Physician).Where(a => a.ShiftDate.Month == DateTime.Now.Month && a.Status == 1 && a.IsDeleted != new System.Collections.BitArray(1, true)).ToList();
            }
            else
            {
                return _context.ShiftDetails.Include(a => a.Shift).ThenInclude(a => a.Physician).Where(a => a.Status == 1 && a.IsDeleted != new System.Collections.BitArray(1, true)).ToList();
            }
            
        }

           
            
        }
    }
