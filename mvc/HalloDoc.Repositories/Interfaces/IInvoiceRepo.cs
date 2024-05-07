﻿using HalloDoc.Repositories.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDoc.Repositories.Interfaces
{
    public interface IInvoiceRepo
    {
        Task<bool> AddDataInInvoice(Invoice invoice);

        Task<bool> AddDataInInvoiceDetail(InvoiceDetail invoiceDetail);

        List<InvoiceDetail>  GetSubmitedDetail(int physicianList, DateTime startDate, DateTime endDate);

        Task<bool> AddDataInReimburesment(Reimbursement reimbursement);

        Task<bool> UpdateDataInReimburesment(Reimbursement reimbursement);
        
        List<Reimbursement> GetReimbursements(int id, DateTime startDate, DateTime endDate);

        Reimbursement GetReimbursement(int? id);

        Task<bool> RemoveDataInReimburesment(Reimbursement reimbursement);

        Invoice GetInvoice(int id, DateTime StartDate);

        Task<bool> UpdateInvoice(Invoice invoice);


        }
}
