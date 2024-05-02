using HalloDoc.Repositories.DataModels;
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

    }
}
