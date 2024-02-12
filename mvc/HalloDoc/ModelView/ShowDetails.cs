using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HalloDoc.ModelView
{
    public class ShowDetails
    {
        public int AspNetUserId { get; set; }
        public int UserId { get; set; }

        [StringLength(256, MinimumLength = 8, ErrorMessage = "please enter atleast 8 characters")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$", ErrorMessage = "please enter number and special symbol")]
        [Required(ErrorMessage = "please enter password")]
        public string? PasswordHash { get; set; }

        [StringLength(256)]
        [RegularExpression(@"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$", ErrorMessage = "please enter valid Email")]
        [Required(ErrorMessage = "please enter email")]
        public string? Email { get; set; }

    }
}
