using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.ModelView
{
    public class CreateRequestVM
    {
        [Required] 
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? Mobile {get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public DateOnly DateOfBirth { get; set; }

        [Required]
        public string? Street { get; set;}

        [Required]
        public string? City { get; set; }

        [Required]
        public string? State { get; set; }

        [Required]
        public string? ZipCode { get; set; }

        [Required]
        public string? RoomOptional { get; set; }

        [Required]
        public string? Notes { get; set; }
    }
}
