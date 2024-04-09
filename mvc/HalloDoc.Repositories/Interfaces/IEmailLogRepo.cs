using HalloDoc.Repositories.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDoc.Repositories.Interfaces
{
    public interface IEmailLogRepo
    {
        List<EmailLog> GetLogs(int roleid, string name, string email, DateOnly cdate, DateOnly sdate);
    }
}
