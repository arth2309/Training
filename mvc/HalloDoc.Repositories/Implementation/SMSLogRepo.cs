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
    public class SMSLogRepo : ISMSLogRepo
    {
        private readonly ApplicationDbContext _context;

        public SMSLogRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Smslog> GetLogs(int roleid, string name, string mobile, DateOnly cdate, DateOnly sdate)
        {
            DateTime dateTime = new DateTime(0001, 01, 01);
            DateOnly dateOnly = DateOnly.FromDateTime(dateTime);

            Func<Smslog,bool> predicate = a => (name == null || a.Request.FirstName.Contains(name) || a.Request.LastName.Contains(name)) &&
                                                  (roleid == 0 || a.RoleId == roleid) &&
                                                  (mobile == null || a.MobileNumber.Contains(mobile)) &&
                                                  (cdate == dateOnly || DateOnly.FromDateTime(a.CreateDate) == cdate) &&
                                                  (sdate == dateOnly || DateOnly.FromDateTime((DateTime)a.SentDate) == sdate);

            return _context.Smslogs.Include(a=>a.Request).Include(a=>a.Physician).Where(predicate).ToList();

        }
    }
}
