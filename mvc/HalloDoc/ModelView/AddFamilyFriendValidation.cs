using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HalloDoc.ModelView
{
    public class AddFamilyFriendValidation
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



        [StringLength(512)]
        public string? symptoms { get; set; }

        [StringLength(128)]
        public string? roomsuite { get; set; }
    }
}
