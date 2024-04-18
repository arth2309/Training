using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Implementation;
using HalloDoc.Repositories.Interfaces;
using HallodocServices.Interfaces;
using HallodocServices.ModelView;
using NpgsqlTypes;
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
        private readonly IPasswordHashServices _passwordHashServices;

        public CreatePhysicianAccountServices(IRoleRepo roleRepo, IRegionRepo regionRepo, IAspNetUserRepo userRepo, IPhysicianRepo physicianRepo, IPasswordHashServices passwordHashServices)
        {
            _roleRepo = roleRepo;
            _regionRepo = regionRepo;
            _userRepo = userRepo;
            _physicianRepo = physicianRepo;
            _passwordHashServices = passwordHashServices;
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

        public AdminProfile GetPhysician(int PhysicianId)
        {
            Physician physician = _physicianRepo.GetPhysician(PhysicianId);
            List<Region> regions = _regionRepo.GetRegions();
            List<Role> roles = _roleRepo.GetRoleDataForPhysician();
            List<PhysicianRegion> physicianRegions = physician.PhysicianRegions.Where(a=>a.PhysicianId ==  PhysicianId).ToList();

          List<int> ints = new List<int>();

            for(int i=0;i<physicianRegions.Count;i++)
            {
                int region = physicianRegions[i].RegionId;
                ints.Add(region);
            }



           
            AdminProfile adminProfile = new AdminProfile();
            adminProfile.UserName = physician.AspNetUser.UserName;
            adminProfile.Id = physician.AspNetUser.Id;
            adminProfile.AdminId = physician.PhysicianId;
            adminProfile.roles = roles;
            adminProfile.regions = regions;
            adminProfile.roleId = physician.RoleId;
            adminProfile.FirstName = physician.FirstName;
            adminProfile.LastName = physician.LastName;
            adminProfile.NpiNumber = physician.Npinumber;
            adminProfile.MedicalLicense = physician.MedicalLicense;
            adminProfile.Mobile = physician.Mobile;
            adminProfile.address1 = physician.Address1;
            adminProfile.address2 = physician.Address2;
            adminProfile.regionId = physician.RegionId;
            adminProfile.City = physician.City;
            adminProfile.Email  = physician.Email;
            adminProfile.ZipCode = physician.Zip;
            adminProfile.BusinessName = physician.BusinessName;
            adminProfile.BusinessWebsite = physician.BusinessWebsite;
            adminProfile.WorkingRegions = ints;
            adminProfile.IsAgreementDoc = (physician.IsAgreementDoc!=null && physician.IsAgreementDoc[0])?true:false;
            adminProfile.IsBackGroundDoc =(physician.IsBackgroundDoc != null && physician.IsBackgroundDoc[0]) ? true : false;
            adminProfile.IsTrainingDoc =  (physician.IsTrainingDoc != null && physician.IsTrainingDoc[0]) ? true : false;
            adminProfile.IsNonDisclosureDoc = (physician.IsNonDisclosureDoc != null && physician.IsNonDisclosureDoc[0]) ? true : false;


            return adminProfile;

        }

        public async Task<bool> ResetPassword(int Id, string Password)
        {
            AspNetUser aspNetUser = _userRepo.GetData(Id);
            aspNetUser.PasswordHash = _passwordHashServices.PasswordHash(Password);
            await _userRepo.UpdateTable(aspNetUser);
            return true;

        }

        public async Task<bool> EditProviderAccountInformation(AdminProfile adminProfile)
        {
            AspNetUser aspNetUser = _userRepo.GetData(adminProfile.Id);
            aspNetUser.UserName = adminProfile.UserName;
            await _userRepo.UpdateTable(aspNetUser);

            Physician physician = _physicianRepo.GetPhysician(adminProfile.AdminId);
            physician.RoleId = adminProfile.roleId;
            await _physicianRepo.UpdateDatainPhysician(physician);

            return true;

        }

        public async Task<bool> EditPhysicianInformation(AdminProfile adminProfile)
        {
            AspNetUser aspNetUser = _userRepo.GetData(adminProfile.Id);
            aspNetUser.Email = adminProfile.Email;
            await _userRepo.UpdateTable(aspNetUser);

            Physician physician = _physicianRepo.GetPhysician(adminProfile.AdminId);
            physician.FirstName = adminProfile.FirstName;
            physician.LastName =adminProfile.LastName;
            physician.Email = adminProfile.Email;
            physician.Mobile = adminProfile.Mobile;
            physician.MedicalLicense = adminProfile.MedicalLicense;
            physician.Npinumber = adminProfile.NpiNumber;

            await _physicianRepo.UpdateDatainPhysician(physician);

            List<PhysicianRegion> physicianRegions = physician.PhysicianRegions.ToList();

            for(int i = 0; i < physicianRegions.Count; i++) 
            {
                bool IsAbsent = adminProfile.checkBoxes.Contains(physicianRegions[i].RegionId)?false:true;
                if(IsAbsent) 
                {
                    await _physicianRepo.RemoveDataInPhysicianRegion(physicianRegions[i]);
                }
            }

            for(int i = 0;i<adminProfile.checkBoxes.Count;i++) 
            {
                bool IsAbsent = true;
                for (int j = 0;j<physicianRegions.Count;j++) 
                {
                    
                    if (adminProfile.checkBoxes[i] == physicianRegions[j].RegionId)
                    {
                        IsAbsent = false;
                        break;
                       
                    }
                }

                if(IsAbsent) 
                {
                    PhysicianRegion physicianRegion = new();
                    physicianRegion.PhysicianId = adminProfile.AdminId;
                    physicianRegion.RegionId = adminProfile.checkBoxes[i];
                    await _physicianRepo.AddDataInPhysicianRegion(physicianRegion);
                }
            }

            return true;
        }

        public async Task<bool> ProviderMailingAndBillingInformation(AdminProfile adminProfile)
        {
            Physician physician = _physicianRepo.GetPhysician(adminProfile.AdminId);
            physician.Address1 = adminProfile.address1;
            physician.Address2 = adminProfile.address2;
            physician.City = adminProfile.City;
            physician.RegionId = adminProfile.regionId;
            physician.Zip = adminProfile.ZipCode;

            await _physicianRepo.UpdateDatainPhysician(physician);
            return true;
        }

        public async Task<bool> EditProviderProfile(AdminProfile adminProfile)
        {
            Physician physician = _physicianRepo.GetPhysician(adminProfile.AdminId);
            physician.BusinessName = adminProfile.BusinessName;
            physician.BusinessWebsite = adminProfile.BusinessWebsite;


            if (adminProfile.Photo != null)
            {


                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");
                string fileName = adminProfile.Photo.FileName;

                string fileNameWithPath = Path.Combine(path, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    adminProfile.Photo.CopyTo(stream);
                }

                physician.Photo = fileName;
            }

            await _physicianRepo.UpdateDatainPhysician(physician);
            return true;
        }
    }
}
