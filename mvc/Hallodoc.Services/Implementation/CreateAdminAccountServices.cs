using HalloDoc.Repositories.Interfaces;
using HallodocServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Repositories.DataModels;
using HallodocServices.Interfaces;
using System.Collections;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.EMMA;

namespace HallodocServices.Implementation
{
    public class CreateAdminAccountServices : ICreateAdminAccountServices
    {
        private readonly IRoleRepo _roleRepo;
        private readonly IRegionRepo _regionRepo;
        private readonly IAspNetUserRepo _userRepo;
        private readonly IAdminRepo _adminRepo;

        public CreateAdminAccountServices(IRoleRepo roleRepo, IRegionRepo regionRepo, IAspNetUserRepo aspNetUserRepo,IAdminRepo adminRepo)
        {
            _roleRepo = roleRepo;
            _regionRepo = regionRepo;
            _userRepo = aspNetUserRepo;
            _adminRepo = adminRepo;
        }

        public AdminProfile GetLists()
        {
            List<Region> regions = _regionRepo.GetRegions();
            List<Role> roles = _roleRepo.GetRoleDataForAdmin();

            AdminProfile adminProfile = new AdminProfile();
            adminProfile.roles = roles;
            adminProfile.regions = regions;

            return adminProfile;
        }

        public async Task<AdminProfile> AddData(AdminProfile adminProfile)
        {
            try
            {
                AspNetUser aspNetUser = new();
                aspNetUser.PhoneNumber = adminProfile.Mobile;
                aspNetUser.PasswordHash = adminProfile.Password;
                aspNetUser.CreatedDate = DateTime.Now;
                aspNetUser.Ip = "192.168.02";
                aspNetUser.Email = adminProfile.Email;
                aspNetUser.UserName = adminProfile.FirstName + " " + adminProfile.LastName;
                await _userRepo.AddTable(aspNetUser);
            }
              
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.ToString());
            }
            

           AspNetUserRole aspNetUserRole = new();
            aspNetUserRole.UserId = 1;
            aspNetUserRole.RoleId = 1;
            await _userRepo.AddData(aspNetUserRole);

            Admin admin = new();
            admin.RoleId = adminProfile.roleId;
            admin.Address1 = adminProfile.address1;
            admin.Address2 = adminProfile.address2;
            admin.AspNetUserId = 1;
            admin.CreatedBy = 1;
            admin.CreatedDate = DateTime.Now;
            admin.FirstName = adminProfile.FirstName;
            admin.LastName = adminProfile.LastName;
            admin.City = adminProfile.City;
            admin.Email = adminProfile.Email;
            admin.RegionId = adminProfile.regionId;

            

           
            
            await _adminRepo.AddDataInAdmin(admin);

            for(int i = 0; i< adminProfile.checkBoxes.Count();i++)
            {
                AdminRegion adminRegion = new();
                adminRegion.AdminId = admin.AdminId;
                adminRegion.RegionId = adminProfile.checkBoxes[i];
                await _adminRepo.AddDataInAdminRegion(adminRegion);
            }

            return adminProfile;
           
        }

        
    }
}
