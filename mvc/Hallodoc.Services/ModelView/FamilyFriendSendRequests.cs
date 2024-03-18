using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.ModelView
{
    public class FamilyFriendSendRequests
    {
        [StringLength(100)]
        [Required(ErrorMessage = "this field is required")]
        public string FFirstName { get; set; } = null!;

        [StringLength(100)]
        [Required(ErrorMessage = "this field is required")]
        public string? FLastName { get; set; }

        [StringLength(20)]
        [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$", ErrorMessage = "please enter valid mobile number")]
        [Required(ErrorMessage = "this field is required")]
        public string? FMobile { get; set; }

        [StringLength(50)]
        [RegularExpression(@"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$", ErrorMessage = "please enter valid Email")]
        [Required(ErrorMessage = "this field is required")]
        public string FEmail { get; set; } = null!;

        [StringLength(100)]
        [Required(ErrorMessage = "this field is required")]
        public string? Relation { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "this field is required")]
        public string? Street { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "this field is required")]
        public string? City { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "this field is required")]
        public string? State { get; set; }


        [StringLength(10)]
        [Required(ErrorMessage = "this field is required")]
        public string? ZipCode { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "this field is required")]
        public string FirstName { get; set; } = null!;

        [StringLength(100)]
        [Required(ErrorMessage = "this field is required")]
        public string? LastName { get; set; }



        [StringLength(50)]
        [RegularExpression(@"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$", ErrorMessage = "please enter valid Email")]
        [Required(ErrorMessage = "this field is required")]
        public string Email { get; set; } = null!;



        [StringLength(20)]
        [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$", ErrorMessage = "please enter valid mobile number")]
        [Required(ErrorMessage = "this field is required")]
        public string? Mobile { get; set; }

        [StringLength(256, MinimumLength = 8, ErrorMessage = "please enter atleast 8 characters")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$", ErrorMessage = "please enter number and special symbol")]
        public string? PasswordHash { get; set; }


        [StringLength(256, MinimumLength = 8, ErrorMessage = "please enter atleast 8 characters")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$", ErrorMessage = "please enter number and special symbol")]
        public string? PasswordHashC { get; set; }



        [StringLength(512)]
        public string? symptoms { get; set; }

        [StringLength(128)]
        public string? roomsuite { get; set; }

        public IFormFile File { get; set; }
    }
}
