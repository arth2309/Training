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
    private readonly IPasswordHashServices _passwordHashServices;
    private readonly IAspNetUserRepo _userRepo;

    public PatientLoginServices(IPatientLoginRepo patientLoginRepo,IPasswordHashServices passwordHashServices, IAspNetUserRepo userRepo)
    {
        _patientLoginRepo = patientLoginRepo;
        _passwordHashServices = passwordHashServices;
        _userRepo = userRepo;
    }
    public int ValidateUser( PatientLogin patientlogin )
    {
        int id = _patientLoginRepo.ValidateUser(patientlogin.Email,_passwordHashServices.PasswordHash(patientlogin.PasswordHash));
        return id;
    }

    public String GetUserName(int id)
    {
        string UserName = _patientLoginRepo.GetUserName(id);
        return UserName;
    }

    public string GetRole(string email) 
    {
        string role = _userRepo.role(email);
        return role;
    }

}

