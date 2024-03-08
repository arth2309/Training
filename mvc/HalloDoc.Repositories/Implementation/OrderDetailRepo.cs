using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Repositories.Interfaces;
using HalloDoc.Repositories.DataContext;
using HalloDoc.Repositories.DataModels;

namespace HalloDoc.Repositories.Implementation
{
    public class OrderDetailRepo : IOrderDetailRepo
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddData(OrderDetail orderDetail)
        {
            _context.OrderDetails.Add(orderDetail);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
