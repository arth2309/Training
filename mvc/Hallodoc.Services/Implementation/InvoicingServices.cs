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
            TimeSheetDateListVM dateList = new();
            TimeSheetDateListVM dateList1 = new();
            List<TimeSheetDateListVM> timeSheetDateListVMs = new();

            DateTime dateTime = DateTime.Now.AddMonths(-1);
            DateTime dateTime1 = new DateTime(dateTime.Year, dateTime.Month, 1);
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

            vm.dateList = timeSheetDateListVMs;
            vm.timeSheetList = GetTimeSheetList(id, 1);

            return vm;
        }

        public List<TimeSheetListVM> GetTimeSheetList(int id, int val)
        {
            List<TimeSheetListVM> list = new();
            List<ShiftDetail> shiftDetails = _shiftrepo.GetShiftCount(id);

            DateTime dateTime = DateTime.Now.AddMonths(-1);
            DateTime dateTime1 = new DateTime(dateTime.Year, dateTime.Month, 1);
            DateTime dateTime2 = dateTime1.AddDays(13);
            DateTime dateTime3 = dateTime1.AddDays(14);
            DateTime dateTime4 = dateTime1.AddMonths(1).AddDays(-1);


            if (val == 1)
            {
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
                        timeSheetListVM.TotalHour = invoiceDetails[i].TotalHours;
                        timeSheetListVM.NumberOfHouseCall = invoiceDetails[i].NumberOfHouseCalls;
                        timeSheetListVM.NumberOfPhoneConsult = invoiceDetails[i].NumberOfPhoneConsults;
                        timeSheetListVM.IsWeekend = invoiceDetails[i].IsHoliday;
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

            else
            {
                DateTime dateTime5 = dateTime3;
                List<InvoiceDetail> invoiceDetails = _invoicerepo.GetSubmitedDetail(id, dateTime3, dateTime4);

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

                    return list;
                }
                else
                {


                    for (int i = 0; i <= (dateTime4.Day - dateTime3.Day); i++)
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
            for (int i = 0; i<=timeSheetListVM.EndDate.Day - timeSheetListVM.StartDate.Day; i++) 
            {
               
                InvoiceDetail invoiceDetail = new InvoiceDetail();
                invoiceDetail.InvoiceId = invoice.InvoiceId;
                invoiceDetail.Date = dateTime;
                invoiceDetail.CreatedDate = DateTime.Now;
                invoiceDetail.TotalHours = timeSheetListVM.TotalHours[i];
                invoiceDetail.NumberOfPhoneConsults = timeSheetListVM.NumberOfPhoneConsults[i];
                invoiceDetail.NumberOfHouseCalls = timeSheetListVM.NumberOfHouseCalls[i];
                invoiceDetail.IsHoliday = timeSheetListVM.IsHoliday.Contains(dateTime)?true:false;
                dateTime = dateTime.AddDays(1);

                await _invoicerepo.AddDataInInvoiceDetail(invoiceDetail);

            }


            return true;

        }
    }
}
