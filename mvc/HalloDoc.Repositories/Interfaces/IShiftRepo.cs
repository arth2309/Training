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

        List<ShiftDetail> GetShiftDetail(int physicianid, DateTime dateTime);

        List<ShiftDetail> GetShiftDetailByRegion(int Regionid, bool CurrentMonth);

        ShiftDetail GetShiftDetailData(int id);

        Task<ShiftDetail> UpdateShiftDetailData(ShiftDetail shiftDetail);

        ShiftDetail GetViewShiftData(int shiftdetailid);
    }
}
