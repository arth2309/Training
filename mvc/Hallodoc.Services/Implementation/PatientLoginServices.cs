using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Repositories.Interfaces;
using HallodocServices.Interfaces;
using HallodocServices.ModelView;

namespace HallodocServices.Implementation;

    public class PatientLoginServices: IPatientLoginServices
{
    private readonly IPatientLoginRepo _patientLoginRepo;

    public PatientLoginServices(IPatientLoginRepo patientLoginRepo)
    {
        _patientLoginRepo = patientLoginRepo;
    }
    public int ValidateUser( PatientLogin patientlogin )
    {
        int id = _patientLoginRepo.ValidateUser(patientlogin.Email, patientlogin.PasswordHash);
        return id;
    }

}

