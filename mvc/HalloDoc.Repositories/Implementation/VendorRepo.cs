using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Interfaces;
using HalloDoc.Repositories.DataContext;
using Microsoft.EntityFrameworkCore;

namespace HalloDoc.Repositories.Implementation
{
    public class VendorRepo : IVendorRepo
    {
        private readonly ApplicationDbContext _DbContext;

        public VendorRepo(ApplicationDbContext context)
        {
            _DbContext = context;
        }

        public HealthProfessional GetVendor(int? vendorId)
        {
            return _DbContext.HealthProfessionals.FirstOrDefault(a=>a.VendorId == vendorId);
        }

        public List<HealthProfessional> GetVendorList(int ProfessionId)
        {
            return _DbContext.HealthProfessionals.Where(a => a.Profession == ProfessionId).ToList();
        }

        public List<HealthProfessional> GetVendorListForPartner(int ProfessionId,string name)
        {
            
            if( ProfessionId != 0 && name != null)
            {
                return _DbContext.HealthProfessionals.Include(a=>a.ProfessionNavigation).Where(a => a.Profession == ProfessionId && a.VendorName.Contains(name) && a.IsDeleted != new System.Collections.BitArray(1,true)).ToList();
            }
            else if( ProfessionId != 0)
            {
                return _DbContext.HealthProfessionals.Include(a => a.ProfessionNavigation).Where(a => a.Profession == ProfessionId && a.IsDeleted != new System.Collections.BitArray(1, true)).ToList();
            }
            else if( name != null) 
            {
                return _DbContext.HealthProfessionals.Include(a => a.ProfessionNavigation).Where(a => a.VendorName.Contains(name) && a.IsDeleted != new System.Collections.BitArray(1, true)).ToList();
            }

            else
            {
                return _DbContext.HealthProfessionals.Include(a => a.ProfessionNavigation).Where(a=> a.IsDeleted != new System.Collections.BitArray(1, true)).ToList();
            }
        }

        
        public async Task<HealthProfessional> AddData(HealthProfessional healthProfessional)
        {
            _DbContext.HealthProfessionals.Add(healthProfessional);
            await _DbContext.SaveChangesAsync();
            return healthProfessional;
        }

        public async Task<HealthProfessional> UpDateData(HealthProfessional healthProfessional)
        {
            _DbContext.HealthProfessionals.Update(healthProfessional);
            await _DbContext.SaveChangesAsync();
            return healthProfessional;
        }
    }
}
