using HalloDoc.Repositories.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDoc.Repositories.Interfaces
{
    public interface ISMSLogRepo
    {
        List<Smslog> GetLogs(int roleid, string name, string mobile, DateOnly cdate, DateOnly sdate);
    }
}
