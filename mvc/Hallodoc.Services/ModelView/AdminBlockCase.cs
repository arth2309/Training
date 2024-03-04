using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.ModelView
{
    public class AdminBlockCase
    {
        public string? blockNotes { get; set; }
        
        public int requestId { get; set; }

        [StringLength(20)]
        public string? Mobile { get; set; }
        public string? Email { get; set; }

    }
}
