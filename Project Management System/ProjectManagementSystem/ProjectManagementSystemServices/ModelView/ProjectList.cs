using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystemServices.ModelView
{
    public class ProjectList
    {
        public int ProjectId { get; set; }

        [Required]
        public string? ProjectName { get; set; }

        [Required]
        public string? ProjectDescription { get; set; }

        [Required]
        public string? Assignee { get; set; }

        [Required]
        public DateTime? DueDate { get; set; }

        public string? DomainName { get; set; }

        [Required]
        public string? City { get; set; }
    }
}
