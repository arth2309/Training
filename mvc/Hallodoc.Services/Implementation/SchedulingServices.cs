using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Interfaces;
using HallodocServices.Interfaces;
using HallodocServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Implementation
{
    public class SchedulingServices : ISchedulingServices
    {
        private readonly IPhysicianRepo _physicianRepo;
        private readonly IShiftRepo _shiftRepo;

        public SchedulingServices(IPhysicianRepo physicianRepo, IShiftRepo shiftRepo)
        {
            _physicianRepo = physicianRepo;
            _shiftRepo = shiftRepo;
        }

       

        public AdminScheduling GetData(int regionid)
        {
            List<Physician> physicians = _physicianRepo.GetPhysiciansListForScheduling(regionid);
            SchedulingList schedulingList = new SchedulingList();
            schedulingList.GetPhysicians = physicians;

            AdminScheduling scheduling = new AdminScheduling();
            scheduling.SchedulingList = schedulingList;

            return scheduling;
        }

        public SchedulingList GetSchedulingList(int regionid) 
        {
            List<Physician> physicians = _physicianRepo.GetPhysiciansListForScheduling(regionid);
            SchedulingList list = new SchedulingList();
            list.GetPhysicians = physicians;
            return list;
        }

        public async Task<AdminScheduling> CreateShift(AdminScheduling scheduling)
        {
            Shift shift = new();
            shift.PhysicianId = scheduling.PhysicianId;
            shift.CreatedBy = 1;
            shift.StartDate = DateOnly.FromDateTime(scheduling.ShiftDate);

            string repeatdays = "";
            if (scheduling.RepeatedDays.Count() > 0)
            {
                repeatdays = String.Join("",scheduling.RepeatedDays[0]);

                for (int i = 1; i < scheduling.RepeatedDays.Count(); i++)
                {
                    repeatdays = String.Join(",", scheduling.RepeatedDays[i]);
                }
            }
            shift.WeekDays = repeatdays;

            shift.CreatedDate = DateTime.Now;
            shift.IsRepeat = new System.Collections.BitArray(1, true);
            shift.RepeatUpto = scheduling.NoRepeat;

            await _shiftRepo.AddDataInShift(shift);

            DateTime currentdate = scheduling.ShiftDate;
            DayOfWeek today = currentdate.DayOfWeek;
            

            for(int i = 0; i < scheduling.NoRepeat;i++)
            {
                for(int j = 0; j < scheduling.RepeatedDays.Count();j++)
                {
                    currentdate.AddDays(Int32.Parse(scheduling.RepeatedDays[j]));
                    ShiftDetail shiftDetail = new();
                    shiftDetail.ShiftDate = currentdate;
                    shiftDetail.ShiftId = shift.ShiftId;
                    shiftDetail.EndTime = scheduling.End;
                    shiftDetail.StartTime = scheduling.Start;
                    shiftDetail.Status = 1;
                    shiftDetail.EventId = "1";
                    shiftDetail.IsDeleted = new System.Collections.BitArray(1, false);
                    shiftDetail.RegionId = scheduling.RegionId;
                    await _shiftRepo.AddDataInShiftDetail(shiftDetail);
                }
            }
            

            return scheduling;
        }
    }
}
