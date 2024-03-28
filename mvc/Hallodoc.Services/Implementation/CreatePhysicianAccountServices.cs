using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Implementation;
using HalloDoc.Repositories.Interfaces;
using HallodocServices.Interfaces;
using HallodocServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Implementation
{
    public class CreatePhysicianAccountServices : ICreatePhysicianAccountServices
    {
        private readonly IRoleRepo _roleRepo;
        private readonly IRegionRepo _regionRepo;
        private readonly IAspNetUserRepo _userRepo;
        private readonly IPhysicianRepo _physicianRepo;

        public CreatePhysicianAccountServices(IRoleRepo roleRepo, IRegionRepo regionRepo, IAspNetUserRepo userRepo, IPhysicianRepo physicianRepo)
        {
            _roleRepo = roleRepo;
            _regionRepo = regionRepo;
            _userRepo = userRepo;
            _physicianRepo = physicianRepo;
        }

        public AdminProfile GetLists()
        {
            List<Region> regions = _regionRepo.GetRegions();
            List<Role> roles = _roleRepo.GetRoleDataForPhysician();

            AdminProfile adminProfile = new AdminProfile();
            adminProfile.roles = roles;
            adminProfile.regions = regions;

            return adminProfile;
        }

        public async Task<AdminProfile>  CreatePhysicianAccount(AdminProfile adminProfile)
        {
            
           
                AspNetUser aspNetUser = new();
                aspNetUser.PhoneNumber = adminProfile.Mobile;
                aspNetUser.PasswordHash = adminProfile.Password;
                aspNetUser.CreatedDate = DateTime.Now;
                aspNetUser.Ip = "192.168.02";
                aspNetUser.Email = adminProfile.Email;
                aspNetUser.UserName = adminProfile.FirstName + " " + adminProfile.LastName;
                await _userRepo.AddTable(aspNetUser);
            

            


            AspNetUserRole aspNetUserRole = new();
            aspNetUserRole.UserId = aspNetUser.Id;
            aspNetUserRole.RoleId = 2;
            await _userRepo.AddData(aspNetUserRole);

           Physician physician = new Physician();
            
            physician.Address1 = adminProfile.address1;
            physician.Address2 = adminProfile.address2;
            physician.RoleId = adminProfile.roleId;
            physician.AspNetUserId = aspNetUser.Id;
            physician.BusinessName = adminProfile.BusinessName;
            physician.BusinessWebsite = adminProfile.BusinessWebsite;
            physician.City = adminProfile.City;
            physician.Npinumber = adminProfile.NpiNumber;
            physician.MedicalLicense = adminProfile.MedicalLicense;
            physician.Email = adminProfile.Email;
            physician.FirstName = adminProfile.FirstName;
            physician.RegionId = adminProfile.regionId;
            physician.LastName = adminProfile.LastName;
            physician.Mobile = adminProfile.Mobile;
            physician.CreatedBy = aspNetUser.Id;
            physician.CreatedDate = DateTime.Now;
            physician.AltPhone = adminProfile.Mobile;

            for(int i = 0; i<adminProfile.documents.Count(); i++)
            {
                if (adminProfile.documents[i] == 1)
                {
                    physician.IsAgreementDoc = new System.Collections.BitArray(1, true);
                }

                if (adminProfile.documents[i] == 2)
                {
                    physician.IsBackgroundDoc = new System.Collections.BitArray(1, true);
                }

                if (adminProfile.documents[i] == 3)
                {
                    physician.IsTrainingDoc = new System.Collections.BitArray(1, true);
                }

                if (adminProfile.documents[i] == 4)
                {
                    physician.IsNonDisclosureDoc = new System.Collections.BitArray(1, true);
                }
            }

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");
            string fileName = adminProfile.Photo.FileName;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                adminProfile.Photo.CopyTo(stream);
            }

            physician.Photo = fileName;

            await _physicianRepo.AddDatainPhysician(physician);


            for (int i = 0; i < adminProfile.checkBoxes.Count(); i++)
            {
                PhysicianRegion physicianRegion = new();
                physicianRegion.PhysicianId = physician.PhysicianId;
                physicianRegion.RegionId = adminProfile.checkBoxes[i];
                await _physicianRepo.AddDataInPhysicianRegion(physicianRegion);
            }

            PhysicianNotification physicianNotification = new PhysicianNotification();
            physicianNotification.PhysicianId = physician.PhysicianId;
            physicianNotification.IsNotificationStopped = new System.Collections.BitArray(1, false);
            await _physicianRepo.AddDataInPhysicianNotification(physicianNotification);

            return adminProfile;
        }
    }
}
