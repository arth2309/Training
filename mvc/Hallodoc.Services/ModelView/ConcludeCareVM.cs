using HalloDoc.Repositories.DataModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.ModelView
{
    public class ConcludeCareVM
    {

        [StringLength(100)]
        [Required]
        public string? ProviderNotes { get; set; }

        public List<RequestWiseFile>? WiseFiles { get; set; }

        public int reqid { get; set; }

        public IFormFile? file { get; set; }

       
    }
}
