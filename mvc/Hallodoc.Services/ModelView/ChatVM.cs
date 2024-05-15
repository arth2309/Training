using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.ModelView
{
    public class ChatVM
    {
        public int SenderId { get; set; }

        public int RecieverId { get; set; }

        public DateTime CreatedDate { get; set; }

        public string? Chat { get; set; } 

        public int RequestId { get; set; }
    }
}
