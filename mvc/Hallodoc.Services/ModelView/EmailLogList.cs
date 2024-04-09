using DocumentFormat.OpenXml.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.ModelView
{
    public class EmailLogList
    {
        public string? Recipient { get; set; }

        public string? Action { get; set; }

        public string? EmailSent { get; set; }

        public int? EmailTries { get; set; }

        public string? roleName { get; set; }

        public DateOnly? CreatedDate { get; set; }

        public DateOnly? SendDate { get; set; }

        public string? Email { get; set; }
        
        public string? Confirmationnumber { get; set; }
    }
}
