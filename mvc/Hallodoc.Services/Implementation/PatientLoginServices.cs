using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Interfaces;
using HallodocServices.Interfaces;
using HallodocServices.ModelView;

namespace HallodocServices.Implementation;

    public class PatientLoginServices: IPatientLoginServices
{
    private readonly IPatientLoginRepo _patientLoginRepo;
    private readonly IPasswordHashServices _passwordHashServices;
    private readonly IAspNetUserRepo _userRepo;
    private readonly IRoleRepo _roleRepo;


    public PatientLoginServices(IPatientLoginRepo patientLoginRepo,IPasswordHashServices passwordHashServices, IAspNetUserRepo userRepo,IRoleRepo roleRepo)
    {
        _patientLoginRepo = patientLoginRepo;
        _passwordHashServices = passwordHashServices;
        _userRepo = userRepo;
        _roleRepo = roleRepo;
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

    public UserVM Object(int id) 
    {
        UserVM userVM = new UserVM();
        AspNetUser aspNetUser = _patientLoginRepo.aspNetUser(id);
        userVM.RoleId = aspNetUser.AspNetUserRole.RoleId;
        int role = aspNetUser.AspNetUserRole.RoleId;
        if(role == 1)
        {
            userVM.Id = aspNetUser.AdminAspNetUsers!=null?aspNetUser.AdminAspNetUsers.FirstOrDefault(a => a.AspNetUserId == id).AdminId:0;
            userVM.UserName = aspNetUser.UserName;
        }

        else if(role == 2) 
        {
            userVM.Id = aspNetUser.PhysicianAspNetUsers!=null?aspNetUser.PhysicianAspNetUsers.FirstOrDefault(a => a.AspNetUserId == id).PhysicianId:0;
            userVM.UserName = aspNetUser.UserName;
            int roleId1 = (int)aspNetUser.PhysicianAspNetUsers.FirstOrDefault(a => a.AspNetUserId == id).RoleId;
            List<RoleMenu> roleMenu1 = _roleRepo.GetRoleMenuDataByroleid(roleId1);
            List<int> menulist1 = new List<int>();
            for (int i = 0; i < roleMenu1.Count; i++)
            {
                int menu = roleMenu1[i].MenuId;
                menulist1.Add(menu);
            }
            userVM.MenuLists = menulist1;
        }

        else
        {
            userVM.Id = aspNetUser.UserAspNetUsers != null ? aspNetUser.UserAspNetUsers.FirstOrDefault(a => a.AspNetUserId == id).UserId : 0;
            userVM.UserName = aspNetUser.UserName;
        }

        return userVM;
    }

}

