using HalloDoc.Repositories.Interfaces;
using HallodocServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Repositories.DataModels;
using HallodocServices.Interfaces;

namespace HallodocServices.Implementation
{
    public class EncounterFormServices : IEncounterFormServices
    {
        private readonly IEncounterRepo _encounterRepo;

        public EncounterFormServices(IEncounterRepo encounterRepo)
        {
            _encounterRepo = encounterRepo;
        }

        public AdminEncounterForm GetEncounterFormData(int requestid)
        {
            Encounter encounter = _encounterRepo.GetData(requestid);
            if (encounter != null)
            {
                AdminEncounterForm adminEncounterForm = new AdminEncounterForm();
                adminEncounterForm.FirstName = encounter.FirstName;
                adminEncounterForm.LastName = encounter.LastName;
                adminEncounterForm.PhoneNumber = encounter.PhoneNumber;
                adminEncounterForm.Email = encounter.Email;
                adminEncounterForm.Cv = encounter.Cv;
                adminEncounterForm.Chest = encounter.Chest;
                adminEncounterForm.Extr = encounter.Extr;
                adminEncounterForm.Skin = encounter.Skin;
                adminEncounterForm.Pain = encounter.Pain;
                adminEncounterForm.Neuro = encounter.Neuro;
                adminEncounterForm.Procedures = encounter.Procedures;
                adminEncounterForm.Abd = encounter.Abd;
                adminEncounterForm.Allergies = encounter.Allergies;
                adminEncounterForm.BloodPressure = encounter.BloodPressure;
                adminEncounterForm.Diagnosis = encounter.Diagnosis;
                adminEncounterForm.Followup = encounter.Followup;
                adminEncounterForm.Diagnosis = encounter.Diagnosis;
                adminEncounterForm.Location = encounter.Location;
                adminEncounterForm.HeartRate = encounter.HeartRate;
                adminEncounterForm.Temperature = encounter.Temperature;
                adminEncounterForm.RespiratoryRate = encounter.RespiratoryRate;
                adminEncounterForm.MedicalHistory = encounter.MedicalHistory;
                adminEncounterForm.MedicationsDispensed = encounter.MedicationsDispensed;
                adminEncounterForm.O2 = encounter.O2;
                adminEncounterForm.TreatmentPlan = encounter.TreatmentPlan;
                adminEncounterForm.IllnessOrInjury = encounter.IllnessOrInjury;


                return adminEncounterForm;
            }
            else
            {
                return null;
            }
        }
    }
}
