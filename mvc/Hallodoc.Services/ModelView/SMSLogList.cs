using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.ModelView
{
    public class SMSLogList
    {
        public string? Recipient { get; set; }

        public string? Action { get; set; }

        public string? SmsSent { get; set; }

        public int? SmsTries { get; set; }

        public string? roleName { get; set; }

        public DateOnly? CreatedDate { get; set; }

        public DateOnly? SendDate { get; set; }

        public string? Mobile { get; set; }

        public string? Confirmationnumber { get; set; }
    }
}
