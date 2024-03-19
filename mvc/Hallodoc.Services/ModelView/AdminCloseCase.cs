using HalloDoc.Repositories.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.ModelView
{
    public class AdminCloseCase
    {
        [StringLength(100)]
        [Required]
        public string? FirstName { get; set; }

        [StringLength(100)]
        [Required]
        public string? LastName { get; set; }


        [StringLength(20)]
        [Required]
        public string? Mobile { get; set; }

        [StringLength(20)]
        [Required]
        public string? Email { get; set; }

        public List<RequestWiseFile>? WiseFiles { get; set; }

        public int reqid { get; set; }
    }
}
