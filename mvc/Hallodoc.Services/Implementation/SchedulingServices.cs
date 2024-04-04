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
                List<ShiftDetail> shiftDetails = _shiftRepo.GetShiftDetail(physicians[i].PhysicianId,dateTime);
                SchedulingList schedulingList1 = new SchedulingList();

                schedulingList1.GetPhysicians = physician;
                schedulingList1.GetShiftDetail = shiftDetails;

                schedulingList.Add(schedulingList1);
            }
           
            AdminScheduling scheduling = new AdminScheduling();
            scheduling.SchedulingList = schedulingList;

            return scheduling;
        }

        public List<SchedulingList> GetSchedulingList(int regionid,DateTime dateTime) 
        {
            List<Physician> physicians = _physicianRepo.GetPhysiciansListForScheduling(regionid);
            List<SchedulingList> schedulingList = new List<SchedulingList>();

            for (int i = 0; i < physicians.Count; i++)
            {
                Physician physician = physicians[i];
                List<ShiftDetail> shiftDetails = _shiftRepo.GetShiftDetail(physicians[i].PhysicianId,dateTime);
                SchedulingList schedulingList1 = new SchedulingList();

                schedulingList1.GetPhysicians = physician;
                schedulingList1.GetShiftDetail = shiftDetails;

                schedulingList.Add(schedulingList1);
            }
            return schedulingList;
        }

        public async Task<AdminScheduling> CreateShift(AdminScheduling scheduling)
        {
            Shift shift = new();
            shift.PhysicianId = scheduling.PhysicianId;
            shift.CreatedBy = 1;
            shift.StartDate = DateOnly.FromDateTime(scheduling.ShiftDate);

            
            if (scheduling.RepeatedDays.Count() > 0)
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
    }
}
