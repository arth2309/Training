using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Repositories.DataModels;
using Microsoft.AspNetCore.Http;

namespace HallodocServices.ModelView
{
    public class AdminProfile
    {
        public int AdminId { get; set; }

        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string FirstName { get; set; } = null!;

        [StringLength(100)]
        [Required]
        public string? LastName { get; set; }

        [StringLength(50)]
        [Required]
        public string? Email { get; set; }

        [StringLength(50)]
        [Required]
        public string? Password { get; set; }



        [StringLength(20)]
        [Required]
        public string? Mobile { get; set; }

        [StringLength(100)]
        [Required]
        public string? address1 { get; set; }

        [StringLength(100)]
        [Required]
        public string? address2 { get; set; }

        [StringLength(100)]
        [Required]
        public string? City { get; set; }

        [StringLength(100)]
  
        public string? State { get; set; }


        [StringLength(10)]
        [Required]
        public string? ZipCode { get; set; }

        public List<Role>? roles { get;set; }

        public List<Region>? regions { get; set; }

        public List<int>? checkBoxes { get; set; }

        public int?regionId { get; set; }

        public int? roleId { get; set; }


        
        public IFormFile? Photo { get; set; }

        public string? MedicalLicense { get; set; }

        public string? NpiNumber { get; set; }

        public string? BusinessName { get; set; }

        public string? BusinessWebsite { get; set; }

        public List<int>? documents {get; set;} 

    }
}

