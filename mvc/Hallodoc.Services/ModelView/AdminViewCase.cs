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
    public class AdminViewCase
    {
        public AdminCancelCase? cancelCases { get; set; }

        [StringLength(100)]
        [Required]
        public string? FirstName { get; set; }

        [StringLength(100)]
        [Required]
        public string? LastName { get; set; }

       
        [StringLength(20)]
        [Required]
        public string? Mobile { get; set; }


        [StringLength(100)]
        [Required]
        public string? Street { get; set; }

        [StringLength(100)]
        [Required]
        public string? City { get; set; }

        [StringLength(100)]
        [Required]
        public string? State { get; set; }

        public int id { get; set; }

        [Required]
        public string? Email { get; set; }
        public short? status { get; set; }
        public int rid { get; set; }

        public int requesttypeid { get; set; }

        public AdminAssignCase? assignCases { get; set; }

    }
}
