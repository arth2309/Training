using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.ModelView
{
    public class NewState
    {
       

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string? LastName { get; set; }

        [StringLength(100)]
        public string? RFirstName { get; set; }

        [StringLength(100)]
        public string? RLastName { get; set; }

        public short? Status { get; set; }

        [Column(TypeName = "timestamp without time zone")]
        public DateTime? CreatedDate { get; set; }


        [StringLength(20)]
        public string? Mobile { get; set; }

        [StringLength(100)]
        public string? Street { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(100)]
        public string? State { get; set; }
        public int? id { get; set; }
        public string? Email { get; set; }
        public int? PageNumber { get; set; }
        public int? TotalPages { get; set; }
        public int Count { get; set; }
        public int RequestId { get; set;}
        public string? Notes { get; set; }
        public string? CaseTag { get; set; }
        public int RequestTypeId { get; set; }

        public string? StrMonth { get; set; }

        public int? year { get; set; }

        public int? day { get; set; }

        public AdminCancelCase? cancelCases { get; set; }
        public AdminAssignCase? assignCases { get; set; }
        public  AdminBlockCase? blockCases { get; set; }
        public SendAgreement? sendAgreement { get; set; }

        public string? physicianName { get; set; }
        public string? regionName { get; set; }

        public int? Calltype { get; set; }

        public bool? IsFinalize { get; set; }

        public int? SenderId { get; set; }

        public int? RecieverId { get; set; }

        public int? UserId { get; set; }

        public string? providerPhoto { get; set; }

       
    }
}
