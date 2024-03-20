using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        [Required]
        public string? State { get; set; }


        [StringLength(10)]
        [Required]
        public string? ZipCode { get; set; }

    }
}

