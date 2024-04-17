using DocumentFormat.OpenXml.Bibliography;
using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Interfaces;
using HalloDoc.Repositories.PagedList;
using HallodocServices.Interfaces;
using HallodocServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

       

        public AdminScheduling GetData(int regionid,DateTime dateTime)
        {
            List<Physician> physicians = _physicianRepo.GetPhysiciansListForScheduling(regionid);
            List<SchedulingList> schedulingList = new List<SchedulingList>();

            for(int i = 0; i<physicians.Count; i++)
            {
                Physician physician = physicians[i];
                List<ShiftDetail> shiftDetails = _shiftRepo.GetShiftDetail(physicians[i].PhysicianId,dateTime,dateTime);
                SchedulingList schedulingList1 = new SchedulingList();

                schedulingList1.GetPhysicians = physician;
                schedulingList1.GetShiftDetail = shiftDetails;

                schedulingList.Add(schedulingList1);
            }
           
            AdminScheduling scheduling = new AdminScheduling();
            scheduling.SchedulingList = schedulingList;

            return scheduling;
        }

        public List<SchedulingList> GetSchedulingList(int regionid,DateTime dateTime1, DateTime dateTime2) 
        {
            List<Physician> physicians = _physicianRepo.GetPhysiciansListForScheduling(regionid);
            List<SchedulingList> schedulingList = new List<SchedulingList>();

            for (int i = 0; i < physicians.Count; i++)
            {
                Physician physician = physicians[i];
                List<ShiftDetail> shiftDetails = _shiftRepo.GetShiftDetail(physicians[i].PhysicianId,dateTime1,dateTime2);
                SchedulingList schedulingList1 = new SchedulingList();

                schedulingList1.GetPhysicians = physician;
                schedulingList1.GetShiftDetail = shiftDetails;

                schedulingList.Add(schedulingList1);
            }
            return schedulingList;
        }

       
        public MonthSchedulingVM GetMonthScheduling(int RegionId,DateTime dateTime1, DateTime dateTime2)
        {
            List<ShiftDetail> shiftDetails = _shiftRepo.GetShiftDetailForMonth(RegionId,dateTime1,dateTime2);
            int startDay = (int)dateTime1.DayOfWeek;

            MonthSchedulingVM monthSchedulingVM = new MonthSchedulingVM();
            monthSchedulingVM.startDay = startDay;
            monthSchedulingVM.GetDetail  = shiftDetails;

            return monthSchedulingVM;
        }

        public AdminScheduling GetProviderScheduling(int PhysicianId)
        {
            DateTime dateTime = DateTime.Now;
            DateTime dateTime1 = new DateTime(dateTime.Year, dateTime.Month, 1);
            DateTime dateTime2 = dateTime1.AddMonths(1).AddDays(-1);

            MonthSchedulingVM monthSchedulingVM = GetProviderSchedulingFilter(PhysicianId, dateTime1, dateTime2);
            AdminScheduling scheduling = new AdminScheduling();
            scheduling.MonthSchedulingVM = monthSchedulingVM;
            return scheduling;
        }

        public MonthSchedulingVM GetProviderSchedulingFilter(int PhysicianId, DateTime dateTime1, DateTime dateTime2)
        {
            List<ShiftDetail> shiftDetails = _shiftRepo.GetProviderScheduling(PhysicianId,dateTime1,dateTime2);
            int startDay = (int)dateTime1.DayOfWeek;

            MonthSchedulingVM monthSchedulingVM = new MonthSchedulingVM();
            monthSchedulingVM.startDay = startDay;
            monthSchedulingVM.GetDetail = shiftDetails;

            return monthSchedulingVM;
        }

        public async Task<AdminScheduling> CreateShift(AdminScheduling scheduling)
        {
            Shift shift = new();
            shift.PhysicianId = scheduling.PhysicianId;
            shift.CreatedBy = 1;
            shift.StartDate = DateOnly.FromDateTime(scheduling.ShiftDate);

            
            if (scheduling.RepeatedDays != null)
            {
                
            shift.WeekDays = System.String.Join("", scheduling.RepeatedDays);
                
            }
   

            shift.IsRepeat = new System.Collections.BitArray(1, scheduling.IsRepeat);
            shift.CreatedDate = DateTime.Now;
            shift.IsRepeat = new System.Collections.BitArray(1, true);
            shift.RepeatUpto = scheduling.NoRepeat;

            await _shiftRepo.AddDataInShift(shift);


            if (scheduling.IsRepeat)
            {

                for (int i = 0; i < scheduling.NoRepeat; i++)
                {
                    for (int j = 0; j < scheduling.RepeatedDays.Count(); j++)
                    {
                        DateTime currentdate = scheduling.ShiftDate;
                        var days = (int.Parse(scheduling.RepeatedDays[j]) - (int)currentdate.DayOfWeek + 7) % 7;
                        currentdate = currentdate.AddDays(days);

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

                    scheduling.ShiftDate = scheduling.ShiftDate.AddDays(7);
                }

            }

            else

            {
                ShiftDetail shiftDetail = new();
                shiftDetail.ShiftDate = scheduling.ShiftDate;
                shiftDetail.ShiftId = shift.ShiftId;
                shiftDetail.EndTime = scheduling.End;
                shiftDetail.StartTime = scheduling.Start;
                shiftDetail.Status = 1;
                shiftDetail.EventId = "1";
                shiftDetail.IsDeleted = new System.Collections.BitArray(1, false);
                shiftDetail.RegionId = scheduling.RegionId;
                await _shiftRepo.AddDataInShiftDetail(shiftDetail);
            }

            return scheduling;
        }

        public ShiftForReviewVM GetDataForReviewShift()
        {
          
           List<ShiftDetail> shiftList = _shiftRepo.GetShiftDetailByRegion(0,false);
           ShiftForReviewVM shiftForReview = new ShiftForReviewVM();
            List<ShiftReviewList> shiftReviewLists = new List<ShiftReviewList>();
            for(int i = 0; i < shiftList.Count;i++)
            {
                ShiftReviewList shiftReviewList = new ShiftReviewList();
                shiftReviewList.ShiftDetailId = shiftList[i].ShiftDetailId;
                shiftReviewList.PhysicianName = shiftList[i].Shift.Physician.FirstName + " " + shiftList[i].Shift.Physician.LastName;
                shiftReviewList.startTime = shiftList[i].StartTime;
                shiftReviewList.endTime = shiftList[i].EndTime;
                shiftReviewList.shiftDate = DateOnly.FromDateTime(shiftList[i].ShiftDate);

                shiftReviewLists.Add(shiftReviewList);
            }
           shiftForReview.shiftDetails = PaginatedList<ShiftReviewList>.ToPagedList(shiftReviewLists, 1, 5);
           return shiftForReview;
        }

        public PaginatedList<ShiftReviewList> FilterDataForReviewShift(int currentPage,int regionid,bool CurrentMonth)
        {

            List<ShiftDetail> shiftList = _shiftRepo.GetShiftDetailByRegion(regionid,CurrentMonth);
            List<ShiftReviewList> shiftReviewLists = new List<ShiftReviewList>();
            for (int i = 0; i < shiftList.Count; i++)
            {
                ShiftReviewList shiftReviewList = new ShiftReviewList();
                shiftReviewList.ShiftDetailId = shiftList[i].ShiftDetailId;
                shiftReviewList.PhysicianName = shiftList[i].Shift.Physician.FirstName + " " + shiftList[i].Shift.Physician.LastName;
                shiftReviewList.startTime = shiftList[i].StartTime;
                shiftReviewList.endTime = shiftList[i].EndTime;
                shiftReviewList.shiftDate = DateOnly.FromDateTime(shiftList[i].ShiftDate);

                shiftReviewLists.Add(shiftReviewList);
            }

            return PaginatedList<ShiftReviewList>.ToPagedList(shiftReviewLists, currentPage, 5);
        }

        public async Task<List<int>> ApproveShiftServices(List<int> List)
        {
            for(int i = 0; i < List.Count;i++)
            {
                ShiftDetail shiftDetail = _shiftRepo.GetShiftDetailData(List[i]);
                shiftDetail.Status = 2;
                await _shiftRepo.UpdateShiftDetailData(shiftDetail);
            }
            return List;
        }

        public async Task<List<int>> DeleteShiftServices(List<int> List)
        {
            for (int i = 0; i < List.Count; i++)
            {
                ShiftDetail shiftDetail = _shiftRepo.GetShiftDetailData(List[i]);
                shiftDetail.IsDeleted = new System.Collections.BitArray(1,true);
                await _shiftRepo.UpdateShiftDetailData(shiftDetail);
            }
            return List;
        }

        public SchedulingList ViewShiftDetail(int shiftDetailId) 
        {
            ShiftDetail shiftDetail = _shiftRepo.GetViewShiftData(shiftDetailId);
            SchedulingList schedulingList = new SchedulingList();
            schedulingList.Id = shiftDetail.ShiftDetailId;
            schedulingList.PhysicianName = "Dr " + shiftDetail.Shift.Physician.FirstName + " " + shiftDetail.Shift.Physician.LastName;  
            schedulingList.ShiftDate = DateOnly.FromDateTime(shiftDetail.ShiftDate);
            schedulingList.StartTime = shiftDetail.StartTime;
            schedulingList.EndTime = shiftDetail.EndTime;
            return schedulingList;
        }

        public async Task<SchedulingList> EditViewShift(SchedulingList schedulingList)
        {
            ShiftDetail shiftDetail = _shiftRepo.GetShiftDetailData(schedulingList.Id);
            DateOnly dateOnly = schedulingList.ShiftDate;
            shiftDetail.ShiftDate = dateOnly.ToDateTime(TimeOnly.Parse("12:00AM"));
            shiftDetail.StartTime = schedulingList.StartTime;
            shiftDetail.EndTime = schedulingList.EndTime;
            await _shiftRepo.UpdateShiftDetailData(shiftDetail);
            return schedulingList;
            
        }

        public async Task<ShiftDetail> DeleteViewShift(int id)
        {
            
            
                ShiftDetail shiftDetail = _shiftRepo.GetShiftDetailData(id);
                shiftDetail.IsDeleted = new System.Collections.BitArray(1, true);
                await _shiftRepo.UpdateShiftDetailData(shiftDetail);
            
            return shiftDetail;
        }

        public MdsOnCallVM GetMdsOnCallList()
        {
            List<Physician> physicians = _physicianRepo.GetPhysiciansListForScheduling(0);
            List<Physician> onDuty = new List<Physician>();
            List<Physician> offDuty = new List<Physician>();
            PhysicianDutyList physicianDutyList = new PhysicianDutyList();
            MdsOnCallVM mdsOnCallVM = new MdsOnCallVM();

            for(int i = 0; i < physicians.Count; i++)
            {
                if (physicians[i].Status == 1) 
                {
                    Physician physician = physicians[i];
                    offDuty.Add(physician);

                }
                else
                {
                    Physician physician = physicians[i];
                    onDuty.Add(physician);
                }
            }

            physicianDutyList.PhysicianOnCall = onDuty;
            physicianDutyList.PhysicianOnDuty = offDuty;
            mdsOnCallVM.PhysicianDutyList = physicianDutyList;
            return mdsOnCallVM;

        }

        public PhysicianDutyList GetMdsOnCallListFilter(int regionid)
        {
            List<Physician> physicians = _physicianRepo.GetPhysiciansListForScheduling(regionid);
            List<Physician> onDuty = new List<Physician>();
            List<Physician> offDuty = new List<Physician>();
            PhysicianDutyList physicianDutyList = new PhysicianDutyList();
            for (int i = 0; i < physicians.Count; i++)
            {
                if (physicians[i].Status == 1)
                {
                    Physician physician = physicians[i];
                    offDuty.Add(physician);

                }
                else
                {
                    Physician physician = physicians[i];
                    onDuty.Add(physician);
                }
            }

            physicianDutyList.PhysicianOnCall = onDuty;
            physicianDutyList.PhysicianOnDuty = offDuty;

            return physicianDutyList;

        }
    }
}
