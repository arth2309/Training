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
        public AdminCancelCase? cancelCases { get; set; }
        public AdminAssignCase? assignCases { get; set; }
        public  AdminBlockCase? blockCases { get; set; }


    }
}
