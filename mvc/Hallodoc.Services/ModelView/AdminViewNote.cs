using HalloDoc.Repositories.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.ModelView
{
    public class AdminViewNote
    {
        public int RequestNotesId { get; set; }

        public int RequestId { get; set; }

        [StringLength(500)]
        public string? PhysicianNotes { get; set; }

        [StringLength(500)]
       
        public string? AdminNotes { get; set; }

        [Required]
        public string? AdditionalNotes { get; set; }

        public List<RequestStatusLog>? TransferNotes { get; set; }
    }
}
