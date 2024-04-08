using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Interfaces;
using HalloDoc.Repositories.PagedList;
using HallodocServices.Interfaces;
using HallodocServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Implementation
{
    public class ProfessionMenuServices : IProfessionMenuServices
    {
        private readonly IProfessionRepo _professionRepo;

        private readonly IVendorRepo _vendorRepo;

        private readonly IRegionRepo _regionRepo;

        public ProfessionMenuServices(IProfessionRepo professionRepo, IVendorRepo vendorRepo, IRegionRepo regionRepo)
        {
            _professionRepo = professionRepo;
            _vendorRepo = vendorRepo;
            _regionRepo = regionRepo;
        }

        public ProfessionMenuVM GetVendorList()
        {
            List<HealthProfessionalType> healthProfessionalTypes = _professionRepo.ProfessionalTypes();
            List<HealthProfessional> healthProfessionals = _vendorRepo.GetVendorListForPartner(0, null);
            List<VendorsList> vendorsLists = new();
            ProfessionMenuVM professionMenuVM = new ProfessionMenuVM();

            for(int i = 0;i<healthProfessionals.Count;i++) 
            {
                VendorsList vendorsList = new VendorsList();
                vendorsList.professionName = healthProfessionals[i].ProfessionNavigation.ProfessionName;
                vendorsList.BusinessContact = healthProfessionals[i].BusinessContact;
                vendorsList.BusinessName = healthProfessionals[i].VendorName;
                vendorsList.FaxNumber = healthProfessionals[i].FaxNumber;
                vendorsList.Mobile = healthProfessionals[i].PhoneNumber;
                vendorsList.Email = healthProfessionals[i].Email;
                vendorsList.VendorId = healthProfessionals[i].VendorId;
                vendorsLists.Add(vendorsList);
            }

            professionMenuVM.Professions = healthProfessionalTypes;
            professionMenuVM.Vendors = PaginatedList<VendorsList>.ToPagedList(vendorsLists, 1, 5);

            return professionMenuVM;


        }

        public PaginatedList<VendorsList> GetVendorListFilter(int ProfessionId, string name, int currentPage)
        {
            List<HealthProfessional> healthProfessionals = _vendorRepo.GetVendorListForPartner(ProfessionId,name);
            List<VendorsList> vendorsLists = new();

            for (int i = 0; i < healthProfessionals.Count; i++)
            {
                VendorsList vendorsList = new VendorsList();
                vendorsList.professionName = healthProfessionals[i].ProfessionNavigation.ProfessionName;
                vendorsList.BusinessContact = healthProfessionals[i].BusinessContact;
                vendorsList.BusinessName = healthProfessionals[i].VendorName;
                vendorsList.FaxNumber = healthProfessionals[i].FaxNumber;
                vendorsList.Mobile = healthProfessionals[i].PhoneNumber;
                vendorsList.Email = healthProfessionals[i].Email;
                vendorsList.VendorId = healthProfessionals[i].VendorId;
                vendorsLists.Add(vendorsList);
            }

            return PaginatedList<VendorsList>.ToPagedList(vendorsLists, currentPage, 5);

        }

        public async Task<HealthProfessional> AddData(BusinessVM businessVM)
        {
            HealthProfessional healthProfessional = new HealthProfessional();
            healthProfessional.CreatedDate = DateTime.Now;
            healthProfessional.VendorName = businessVM.BusinessName;
            healthProfessional.FaxNumber = businessVM.FaxNumber;
            healthProfessional.BusinessContact = businessVM.BusinessContact;
            healthProfessional.City = businessVM.City;
            healthProfessional.State = _regionRepo.GetRegionName(businessVM.regionid);
            healthProfessional.RegionId = businessVM.regionid;
            healthProfessional.Email = businessVM.Email;
            healthProfessional.PhoneNumber = businessVM.Mobile;
            healthProfessional.Address = businessVM.Street;
            healthProfessional.Profession = businessVM.ProfessionId;
            healthProfessional.Zip = businessVM.ZipCode;

            await _vendorRepo.AddData(healthProfessional);
            return healthProfessional;

        }

        public BusinessVM ShowData(int VendorId) 
        {
            HealthProfessional healthProfessional = _vendorRepo.GetVendor(VendorId);
            BusinessVM businessVM = new BusinessVM();
            businessVM.Street = healthProfessional.Address;
            businessVM.BusinessName = healthProfessional.VendorName;
            businessVM.ProfessionId = healthProfessional.Profession;
            businessVM.regionid = healthProfessional.RegionId;
            businessVM.Email = healthProfessional.Email;
            businessVM.Mobile = healthProfessional.PhoneNumber;
            businessVM.FaxNumber = healthProfessional.FaxNumber;
            businessVM.BusinessContact = healthProfessional.BusinessContact;
            businessVM.City = healthProfessional.City;
            businessVM.ZipCode = healthProfessional.Zip;
            businessVM.VendorId = VendorId;
            return businessVM;  

        }

        public async Task<BusinessVM> UpdateData(BusinessVM businessVM)
        {
            HealthProfessional healthProfessional = _vendorRepo.GetVendor(businessVM.VendorId);
            healthProfessional.VendorName = businessVM.BusinessName;
            healthProfessional.FaxNumber = businessVM.FaxNumber;
            healthProfessional.BusinessContact = businessVM.BusinessContact;
            healthProfessional.City = businessVM.City;
            healthProfessional.State = _regionRepo.GetRegionName(businessVM.regionid);
            healthProfessional.RegionId = businessVM.regionid;
            healthProfessional.Email = businessVM.Email;
            healthProfessional.PhoneNumber = businessVM.Mobile;
            healthProfessional.Address = businessVM.Street;
            healthProfessional.Profession = businessVM.ProfessionId;
            healthProfessional.Zip = businessVM.ZipCode;
            healthProfessional.ModifiedDate = DateTime.Now;
            await _vendorRepo.UpDateData(healthProfessional);

            return businessVM;

        }

        public async Task<int> DeleteData(int Vendorid)
        {
            HealthProfessional healthProfessional = _vendorRepo.GetVendor(Vendorid);
            healthProfessional.IsDeleted = new System.Collections.BitArray(1, true);
            await _vendorRepo.UpDateData(healthProfessional);
            return Vendorid;
        }

       
    }
}
