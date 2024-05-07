
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
    public class InvoicingServices : IInvoicingServices
    {

        private readonly IShiftRepo _shiftrepo;
        private readonly IInvoiceRepo _invoicerepo;

        public InvoicingServices(IShiftRepo shiftrepo, IInvoiceRepo invoicerepo)
        {
            _shiftrepo = shiftrepo;
            _invoicerepo = invoicerepo;
        }

        public TimeSheetVM GetTimeSheet(int id)
        {
            TimeSheetVM vm = new();

            List<TimeSheetDateListVM> timeSheetDateListVMs = new();

            DateTime dateTime = DateTime.Now.AddMonths(-3);
            DateTime dateTime1 = new DateTime(dateTime.Year, dateTime.Month, 1);

            

            for (int i = 0; i < 3; i++)
            {

                TimeSheetDateListVM dateList = new();
                TimeSheetDateListVM dateList1 = new();

                DateTime dateTime2 = dateTime1.AddDays(13);
                DateTime dateTime3 = dateTime1.AddDays(14);
                DateTime dateTime4 = dateTime1.AddMonths(1).AddDays(-1);

                dateList.StartDate = dateTime1;
                dateList.EndDate = dateTime2;
                dateList.val = 1;
                dateList1.StartDate = dateTime3;
                dateList1.EndDate = dateTime4;
                dateList1.val = 2;

                timeSheetDateListVMs.Add(dateList);
                timeSheetDateListVMs.Add(dateList1);

                dateTime1 = dateTime1.AddMonths(1);

            }


            vm.dateList = timeSheetDateListVMs;
            DateTime dateTime5 = new DateTime(dateTime.Year, dateTime.Month,1);
            vm.timeSheetList = GetTimeSheetList(id, dateTime5);

            return vm;
        }

        public List<TimeSheetListVM> GetTimeSheetList(int id, DateTime startDate)
        {
            List<TimeSheetListVM> list = new();
            List<ShiftDetail> shiftDetails = _shiftrepo.GetShiftCount(id);

            DateTime dateTime1;
            DateTime dateTime2;

            List<Reimbursement> reimbursements;
            if (startDate.Day == 1)
            {
                 dateTime1 = startDate;
                 dateTime2 = startDate.AddDays(13);
                 reimbursements = _invoicerepo.GetReimbursements(id, dateTime1,dateTime2);
            }
            else
            {
                DateTime dateTime = new DateTime(startDate.Year, startDate.Month, 1);
                dateTime1 = startDate;
                dateTime2 = dateTime.AddMonths(1).AddDays(-1);
                reimbursements = _invoicerepo.GetReimbursements(id, dateTime1, dateTime2);

            }


            DateTime dateTime5 = dateTime1;
            List<InvoiceDetail> invoiceDetails = _invoicerepo.GetSubmitedDetail(id, dateTime1, dateTime2);

            if (invoiceDetails.Count > 0)
            {
                for (int i = 0; i < invoiceDetails.Count; i++)
                {
                   
                    TimeSheetListVM timeSheetListVM = new();
                    timeSheetListVM.ShiftDate = invoiceDetails[i].Date;
                    timeSheetListVM.ShiftCount = shiftDetails.Where(a => DateOnly.FromDateTime(a.ShiftDate) == DateOnly.FromDateTime(invoiceDetails[i].Date)).Count();
                    timeSheetListVM.IsSubmit = true;
                    timeSheetListVM.IsFinalize = invoiceDetails[i].Invoice.IsFinalize == null?false:invoiceDetails[i].Invoice.IsFinalize;
                    timeSheetListVM.TotalHour = invoiceDetails[i].TotalHours;
                    timeSheetListVM.NumberOfHouseCall = invoiceDetails[i].NumberOfHouseCalls;
                    timeSheetListVM.NumberOfPhoneConsult = invoiceDetails[i].NumberOfPhoneConsults;
                    timeSheetListVM.IsWeekend = invoiceDetails[i].IsHoliday;
                    timeSheetListVM.Reimbursements = reimbursements;
                    list.Add(timeSheetListVM);

                }

                return list;
            }
            else
            {


                for (int i = 0; i <= (dateTime2.Day - dateTime1.Day); i++)
                {
                    double diff = 0;
                    if (shiftDetails.Where(a => DateOnly.FromDateTime(a.ShiftDate) == DateOnly.FromDateTime(dateTime5)) != null)
                    {
                        List<ShiftDetail> shiftDetails1 = shiftDetails.Where(a => DateOnly.FromDateTime(a.ShiftDate) == DateOnly.FromDateTime(dateTime5)).ToList();
                        for (int j = 0; j < shiftDetails1.Count; j++)
                        {
                            double cal = (shiftDetails1[j].EndTime.Hour + (shiftDetails1[j].EndTime.Minute / (double)60)) - (shiftDetails1[j].StartTime.Hour + (shiftDetails1[j].StartTime.Minute / (double)60));
                            diff = diff + cal;

                        }
                    }
                    TimeSheetListVM timeSheetListVM = new();
                    timeSheetListVM.ShiftDate = dateTime5;
                    timeSheetListVM.ShiftCount = shiftDetails.Where(a => DateOnly.FromDateTime(a.ShiftDate) == DateOnly.FromDateTime(dateTime5)).Count();
                    timeSheetListVM.IsSubmit = false;
                    timeSheetListVM.callHours = diff;
                    dateTime5 = dateTime5.AddDays(1);
                    list.Add(timeSheetListVM);

                }

                return list;
            }




        }


        public TimeSheetVM GetBiWeeklySheet(int id,DateTime date)
        {

            TimeSheetVM timeSheetVM = new TimeSheetVM();

            List<TimeSheetListVM> list = new();
            List<ShiftDetail> shiftDetails = _shiftrepo.GetShiftCount(id);
            DateTime dateTime1;
            DateTime dateTime2;
            if (date.Day == 1)
            {
                dateTime1 = date;
                dateTime2 = date.AddDays(13);
            }
            else
            {
               DateTime dateTime = new DateTime(date.Year,date.Month, 1);
                dateTime1 = date;
                dateTime2 = dateTime.AddMonths(1).AddDays(-1);

            }
            List<InvoiceDetail> invoiceDetails = _invoicerepo.GetSubmitedDetail(id, dateTime1, dateTime2);
            DateTime dateTime5 = date;

            if (invoiceDetails.Count > 0)
            {
                for (int i = 0; i < invoiceDetails.Count; i++)
                {

                    TimeSheetListVM timeSheetListVM = new();
                    timeSheetListVM.ShiftDate = invoiceDetails[i].Date;
                    timeSheetListVM.ShiftCount = shiftDetails.Where(a => DateOnly.FromDateTime(a.ShiftDate) == DateOnly.FromDateTime(invoiceDetails[i].Date)).Count();
                    timeSheetListVM.IsSubmit = true;
                    timeSheetListVM.TotalHour = invoiceDetails[i].TotalHours;
                    timeSheetListVM.NumberOfHouseCall = invoiceDetails[i].NumberOfHouseCalls;
                    timeSheetListVM.NumberOfPhoneConsult = invoiceDetails[i].NumberOfPhoneConsults;
                    timeSheetListVM.IsWeekend = invoiceDetails[i].IsHoliday;

                    list.Add(timeSheetListVM);

                }


            }

            else
            {


                for (int i = 0; i <= (dateTime2.Day - dateTime1.Day); i++)
                {
                    double diff = 0;
                    if (shiftDetails.Where(a => DateOnly.FromDateTime(a.ShiftDate) == DateOnly.FromDateTime(dateTime5)) != null)
                    {
                        List<ShiftDetail> shiftDetails1 = shiftDetails.Where(a => DateOnly.FromDateTime(a.ShiftDate) == DateOnly.FromDateTime(dateTime5)).ToList();
                        for (int j = 0; j < shiftDetails1.Count; j++)
                        {
                            double cal = (shiftDetails1[j].EndTime.Hour + (shiftDetails1[j].EndTime.Minute / (double)60)) - (shiftDetails1[j].StartTime.Hour + (shiftDetails1[j].StartTime.Minute / (double)60));
                            diff = diff + cal;

                        }
                    }
                    TimeSheetListVM timeSheetListVM = new();
                    timeSheetListVM.ShiftDate = dateTime5;
                    timeSheetListVM.IsSubmit = false;
                    timeSheetListVM.callHours = diff;
                    dateTime5 = dateTime5.AddDays(1);
                    list.Add(timeSheetListVM);

                }

            }

           
            List<Reimbursement> reimbursement = _invoicerepo.GetReimbursements(id,dateTime1,dateTime2);

            timeSheetVM.timeSheetList = list;
            timeSheetVM.reimbursements = reimbursement;

            return timeSheetVM;
        }


        public async Task<bool> SubmitWeeklyList(TimeSheetListVM timeSheetListVM)
        {
            Invoice invoice = new Invoice();
            invoice.PhysicianId = timeSheetListVM.PhysicianId;
            invoice.StartDate = timeSheetListVM.StartDate;
            invoice.EndDate = timeSheetListVM.EndDate;
            invoice.CreatedDate = DateTime.Now;
            invoice.Status = 1;
            invoice.IsSubmit = true;

            await _invoicerepo.AddDataInInvoice(invoice);

            DateTime dateTime = timeSheetListVM.StartDate;
            for (int i = 0; i <= timeSheetListVM.EndDate.Day - timeSheetListVM.StartDate.Day; i++)
            {

                InvoiceDetail invoiceDetail = new InvoiceDetail();
                invoiceDetail.InvoiceId = invoice.InvoiceId;
                invoiceDetail.Date = dateTime;
                invoiceDetail.CreatedDate = DateTime.Now;
                invoiceDetail.TotalHours = timeSheetListVM.TotalHours[i];
                invoiceDetail.NumberOfPhoneConsults = timeSheetListVM.NumberOfPhoneConsults[i];
                invoiceDetail.NumberOfHouseCalls = timeSheetListVM.NumberOfHouseCalls[i];
                invoiceDetail.IsHoliday = timeSheetListVM.IsHoliday.Contains(dateTime) ? true : false;
                dateTime = dateTime.AddDays(1);

                await _invoicerepo.AddDataInInvoiceDetail(invoiceDetail);

            }

            return true;

        }

        public async Task<bool> GetReImbursementsSheet(ReImbursementVM reImbursementVM)
        {

            Reimbursement reimbursement = new Reimbursement();

            if (reImbursementVM.bill!=null)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");
                FileInfo fileInfo = new FileInfo(reImbursementVM.bill.FileName);
                string fileName = reImbursementVM.bill.FileName;



                string fileNameWithPath = Path.Combine(path, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    reImbursementVM.bill.CopyTo(stream);
                }

                reimbursement.File = fileName;
            }
            

            reimbursement.Date = reImbursementVM.ShiftDate;
            reimbursement.Item = reImbursementVM.Item;
            reimbursement.CreatedDate = DateTime.Now;
            reimbursement.Amount = reImbursementVM.Amount;
            reimbursement.PhysicianId = reImbursementVM.PhysicianId;
            
            
            bool result = await _invoicerepo.AddDataInReimburesment(reimbursement);
            return result;
            
        }

        public async Task<bool> DeleteReImbursementsSheet(int id)
        {
            Reimbursement reimbursement = _invoicerepo.GetReimbursement(id);
            bool result = await  _invoicerepo.RemoveDataInReimburesment(reimbursement);
            return result;
        }

        public async Task<bool> UpDateReImbursementsSheet(ReImbursementVM reImbursementVM)
        {
            Reimbursement reimbursement = _invoicerepo.GetReimbursement(reImbursementVM.ReimbursementId);
            reimbursement.Item = reImbursementVM.Item;
            reimbursement.Amount = reImbursementVM.Amount;
            bool result = await _invoicerepo.UpdateDataInReimburesment(reimbursement);
            return result;
        }

        public async Task<bool> Finalize(int id,DateTime startDate)
        {
            Invoice invoice = _invoicerepo.GetInvoice(id, startDate);
            if (invoice == null) 
            {
                return false;
            }
            else
            {
                invoice.IsFinalize = true;
                await _invoicerepo.UpdateInvoice(invoice);
                return true;
            }
        }
    }
}
