using HalloDoc.Repositories.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HalloDoc.Repositories.Implementation
{
    public class InvoiceRepo : IInvoiceRepo
    {
        private readonly ApplicationDbContext _context;

        public InvoiceRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddDataInInvoice(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddDataInInvoiceDetail(InvoiceDetail invoiceDetail)
        {
            _context.InvoiceDetails.Add(invoiceDetail);
            await _context.SaveChangesAsync();
            return true;
        }

        public List<InvoiceDetail> GetSubmitedDetail(int physicianList,DateTime startDate,DateTime endDate) 
        {
            return _context.InvoiceDetails.Include(a=>a.Invoice).Where(a=>a.Invoice.PhysicianId == physicianList && DateOnly.FromDateTime(a.Date)>= DateOnly.FromDateTime(startDate) && DateOnly.FromDateTime(a.Date)<= DateOnly.FromDateTime(endDate)).ToList();
        }
    }
}
