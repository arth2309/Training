using HalloDoc.Repositories.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDoc.Repositories.Interfaces
{
    public interface IShiftRepo
    {
        Task<Shift> AddDataInShift(Shift shift);

        Task<ShiftDetail> AddDataInShiftDetail(ShiftDetail shiftDetail);

        List<ShiftDetail> GetShiftDetail(int physicianid, DateTime dateTime1, DateTime dateTime2);

        List<ShiftDetail> GetShiftDetailByRegion(int Regionid, bool CurrentMonth);

        ShiftDetail GetShiftDetailData(int id);

        Task<ShiftDetail> UpdateShiftDetailData(ShiftDetail shiftDetail);

        ShiftDetail GetViewShiftData(int shiftdetailid);

        List<ShiftDetail> GetShiftDetailForMonth(int RegionId,DateTime dateTime1, DateTime dateTime2);

        List<ShiftDetail> GetProviderScheduling(int PhysicianId, DateTime dateTime1, DateTime dateTime2);
    }
}
