using HalloDoc.Repositories.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.ModelView
{
    public class AdminAssignCase
    {

        [StringLength(100)]
        public string RegionName { get; set; }

        [Required(ErrorMessage = "Please select an region.")]
        public int RegionId { get; set; }

        public List<Region>? regions { get; set; }

        public int RequestId { get; set; }

        public List<Physician>? physician { get; set; }

        [Required(ErrorMessage = "Please select an physician.")]
        public int PhysicianId { get; set; }

        public string? Description { get; set; }

      
    }
}
