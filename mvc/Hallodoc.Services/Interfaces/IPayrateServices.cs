using HallodocServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Interfaces
{
    public interface IPayrateServices
    {
        PayrateVM GetPayrate(int PhysicianId);
        Task<bool> AddPayrate(PayrateVM payrateVM);
        Task<bool> UpdateNightShiftWeekend(int? price, int PhysicianId);
        Task<bool> UpdateShift(int? price, int PhysicianId);
        Task<bool> UpdateHouseCalls(int? price, int PhysicianId);
        Task<bool> UpdateHouseCallsWeekend(int? price, int PhysicianId);
        Task<bool> UpdatePhoneConsults(int? price, int PhysicianId);
        Task<bool> UpdatePhoneConsultsWeekend(int? price, int PhysicianId);
        Task<bool> UpdateBatchTesting(int? price, int PhysicianId);

    }
}
