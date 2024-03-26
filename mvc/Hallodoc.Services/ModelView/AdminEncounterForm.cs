using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.ModelView
{
    public class AdminEncounterForm
    {
        public int EncounterId { get; set; }

        public int RequestId { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Location { get; set; }
        
        public DateTime? Date { get; set; }
        
        public DateTime? BirthDate { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? MedicalHistory { get; set; }
        [Required]
        public string? IllnessOrInjury { get; set; }
        [Required]
        public string? Medications { get; set; }
        [Required]
        public string? Allergies { get; set; }
        [Required]
        public string? Temperature { get; set; }
        [Required]
        public string? HeartRate { get; set; }
        [Required]
        public string? RespiratoryRate { get; set; }
        [Required]
        public string? BloodPressure { get; set; }
        [Required]
        public string? O2 { get; set; }
        [Required]
        public string? Pain { get; set; }
        [Required]
        public string? Heent { get; set; }
        [Required]
        public string? Cv { get; set; }
        [Required]
        public string? Chest { get; set; }
        [Required]
        public string? Abd { get; set; }
        [Required]
        public string? Extr { get; set; }
        [Required]
        public string? Skin { get; set; }
        [Required]
        public string? Neuro { get; set; }
        [Required]
        public string? Other { get; set; }
        [Required]
        public string? Diagnosis { get; set; }
        [Required]
        public string? TreatmentPlan { get; set; }
        [Required]
        public string? MedicationsDispensed { get; set; }
        [Required]
        public string? Procedures { get; set; }
        [Required]
        public string? Followup { get; set; }
    }
}

