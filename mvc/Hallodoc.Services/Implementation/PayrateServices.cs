using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Interfaces;
using HallodocServices.ModelView;
using HallodocServices.Interfaces;

namespace HallodocServices.Implementation
{
    public class PayrateServices : IPayrateServices
    {
        private readonly IPayrateRepo _payrateRepo;

        public PayrateServices(IPayrateRepo payrateRepo)
        {
            _payrateRepo = payrateRepo;
        }

        public PayrateVM GetPayrate(int PhysicianId)
        {
            PhysicianPayrate physicianPayrate = _payrateRepo.GetData(PhysicianId);
            PayrateVM payrateVM = new();
            if (physicianPayrate != null)
            {


                payrateVM.PayrateId = physicianPayrate.PayrateId;
                payrateVM.PhysicianId = PhysicianId;
                payrateVM.Shift = (int)physicianPayrate.Shift;
                payrateVM.NightShiftWeekend = (int)physicianPayrate.NightShiftWeekend;
                payrateVM.HouseCalls = (int)physicianPayrate.HouseCalls;
                payrateVM.HouseCallsNightWeekend = (int)physicianPayrate.HouseCallsNightWeekend;
                payrateVM.PhoneConsults = (int)physicianPayrate.PhoneConsults;
                payrateVM.PhoneConsultsNightWeekend = (int)physicianPayrate.PhoneConsultsNightWeekend;
                payrateVM.BatchTesting = (int)physicianPayrate.BatchTesting;
                return payrateVM;
            }
            else
            {
                return null;
            }
           
        }

        public async Task<bool> AddPayrate(PayrateVM payrateVM)
        {
            PhysicianPayrate physicianPayrate = new();
            physicianPayrate.PhysicianId = payrateVM.PhysicianId;
            physicianPayrate.Shift = payrateVM.Shift;
            physicianPayrate.NightShiftWeekend = payrateVM.NightShiftWeekend;
            physicianPayrate.HouseCalls = payrateVM.HouseCalls;
            physicianPayrate.HouseCallsNightWeekend = payrateVM.HouseCallsNightWeekend;
            physicianPayrate.PhoneConsults = payrateVM.PhoneConsults;
            physicianPayrate.PhoneConsultsNightWeekend = payrateVM.PhoneConsultsNightWeekend;
            physicianPayrate.BatchTesting = payrateVM.BatchTesting;

            bool result = await _payrateRepo.AddData(physicianPayrate);
            return result;

        }

        public async Task<bool> UpdateNightShiftWeekend(int? price,int PhysicianId)
        {
            PhysicianPayrate physicianPayrate = _payrateRepo.GetData(PhysicianId);
            physicianPayrate.NightShiftWeekend = price;
            bool result = await _payrateRepo.UpdateData(physicianPayrate);
            return result;
        }

        public async Task<bool> UpdateShift(int? price, int PhysicianId)
        {
            PhysicianPayrate physicianPayrate = _payrateRepo.GetData(PhysicianId);
            physicianPayrate.Shift = price;
            bool result = await _payrateRepo.UpdateData(physicianPayrate);
            return result;
        }

        public async Task<bool> UpdateHouseCalls(int? price, int PhysicianId)
        {
            PhysicianPayrate physicianPayrate = _payrateRepo.GetData(PhysicianId);
            physicianPayrate.HouseCalls = price;
            bool result = await _payrateRepo.UpdateData(physicianPayrate);
            return result;
        }

        public async Task<bool> UpdateHouseCallsWeekend(int? price, int PhysicianId)
        {
            PhysicianPayrate physicianPayrate = _payrateRepo.GetData(PhysicianId);
            physicianPayrate.HouseCallsNightWeekend = price;
            bool result = await _payrateRepo.UpdateData(physicianPayrate);
            return result;
        }

        public async Task<bool> UpdatePhoneConsults(int? price, int PhysicianId)
        {
            PhysicianPayrate physicianPayrate = _payrateRepo.GetData(PhysicianId);
            physicianPayrate.PhoneConsults = price;
            bool result = await _payrateRepo.UpdateData(physicianPayrate);
            return result;
        }

        public async Task<bool> UpdatePhoneConsultsWeekend(int? price, int PhysicianId)
        {
            PhysicianPayrate physicianPayrate = _payrateRepo.GetData(PhysicianId);
            physicianPayrate.PhoneConsultsNightWeekend = price;
            bool result = await _payrateRepo.UpdateData(physicianPayrate);
            return result;
        }

        public async Task<bool> UpdateBatchTesting(int? price, int PhysicianId)
        {
            PhysicianPayrate physicianPayrate = _payrateRepo.GetData(PhysicianId);
            physicianPayrate.BatchTesting = price;
            bool result = await _payrateRepo.UpdateData(physicianPayrate);
            return result;
        }
    }
}
