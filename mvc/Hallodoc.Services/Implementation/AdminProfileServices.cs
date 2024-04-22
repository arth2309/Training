using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Interfaces;
using HallodocServices.ModelView;
using HallodocServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Repositories.Implementation;

namespace HallodocServices.Implementation
{
    public class AdminProfileServices : IAdminProfileServices
    {
        private readonly IAdminRepo _adminRepo;
        private readonly IAspNetUserRepo _userRepo;
        private readonly IRegionRepo _regionRepo;
        private readonly IRoleRepo _roleRepo;

        public AdminProfileServices(IAdminRepo adminRepo, IAspNetUserRepo userRepo, IRegionRepo regionRepo, IRoleRepo roleRepo)
        {
            _adminRepo = adminRepo;
            _userRepo = userRepo;
            _regionRepo = regionRepo;
            _roleRepo = roleRepo;
        }

        public AdminProfile GetAdminData(int adminid)
        {

            List<Region> regions = _regionRepo.GetRegions();
            List<Role> roles = _roleRepo.GetRoleDataForAdmin();

          

            Admin admin = _adminRepo.GetAdminData(adminid);
            List<AdminRegion> adminRegions = admin.AdminRegions.ToList();

            List<int> ints = new List<int>();

            for (int i = 0; i < adminRegions.Count; i++)
            {
                int region = adminRegions[i].RegionId;
                ints.Add(region);
            }

            AspNetUser aspNetUser = _userRepo.GetData(admin.AspNetUserId);
            AdminProfile profile = new AdminProfile();
            profile.AdminId = adminid;
            profile.FirstName = admin.FirstName;
            profile.LastName = admin.LastName;
            profile.Email = admin.Email;
            profile.address1 = admin.Address1;
            profile.address2 = admin.Address2;
            profile.City = admin.City;
            profile.State = "Maryland";
            profile.Id = admin.AspNetUserId;
            profile.ZipCode = admin.Zip;
            profile.roles = roles;
            profile.WorkingRegions = ints;
            profile.regions = regions;
            profile.roleId = admin.RoleId;
            profile.Mobile = admin.Mobile;
            




            return profile;
        } 

        public async Task<bool> ResetPassword(int id,string password)
        {
            AspNetUser aspNetUser = _userRepo.GetData(id);
            aspNetUser.PasswordHash = password;
            await _userRepo.UpdateTable(aspNetUser);
            return true;
        }

        public async Task<bool> UpdateInformation(int id,int adminid,string firstname,string lastname,string email,string Mobile)
        {
            Admin admin = _adminRepo.GetAdminData(adminid);
                 admin.FirstName = firstname;
                 admin.LastName = lastname;
                 admin.Email = email;
              admin.Mobile = Mobile;
             await _adminRepo.UpdateTable(admin);

            AspNetUser aspNetUser = _userRepo.GetData(id);
            aspNetUser.Email = email;
            await _userRepo.UpdateTable(aspNetUser);

            return true;
                
        }

        public async Task<bool> UpDateBillingInformation(int adminid,string address1,string address2,string city,string zip,string altphone)
        {
            Admin admin = _adminRepo.GetAdminData(adminid) ;
            admin.Address1 = address1;
            admin.Address2 = address2;  
            admin.City = city;
            admin.Zip = zip;
            admin.AltPhone = altphone;
            await _adminRepo.UpdateTable(admin);
            return true;
        }

    }
}
