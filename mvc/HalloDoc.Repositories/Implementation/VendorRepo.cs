using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Interfaces;
using HalloDoc.Repositories.DataContext;


namespace HalloDoc.Repositories.Implementation
{
    public class VendorRepo : IVendorRepo
    {
        private readonly ApplicationDbContext _DbContext;

        public VendorRepo(ApplicationDbContext context)
        {
            _DbContext = context;
        }

        public HealthProfessional GetVendor(int vendorId)
        {
            return _DbContext.HealthProfessionals.Find(vendorId);
        }

        public List<HealthProfessional> GetVendorList(int ProfessionId)
        {
            return _DbContext.HealthProfessionals.Where(a => a.Profession == ProfessionId).ToList();
        }
    }
}
