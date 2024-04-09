using HalloDoc.Repositories.DataContext;
using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDoc.Repositories.Implementation
{
    public class EmailLogRepo : IEmailLogRepo
    {
        private readonly ApplicationDbContext _context;

        public EmailLogRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<EmailLog> GetLogs(int roleid, string name, string email, DateOnly cdate, DateOnly sdate)
        {
            DateTime dateTime = new DateTime(0001,01,01);
            DateOnly dateOnly = DateOnly.FromDateTime(dateTime);

            Func<EmailLog, bool> predicate = a => (name == null || a.Request.FirstName.Contains(name) || a.Request.LastName.Contains(name)) &&
                                                  (roleid == 0 || a.RoleId == roleid) &&
                                                  (email == null || a.EmailId.Contains(email))&&
                                                  (cdate == dateOnly || DateOnly.FromDateTime(a.CreateDate) == cdate) &&
                                                  (sdate == dateOnly || DateOnly.FromDateTime((DateTime)a.SentDate) == sdate);

            return _context.EmailLogs.Include(a=>a.Request).Include(a=>a.Physician).Where(predicate).ToList();
                                                   
        }
    }
}
