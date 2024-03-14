using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Repositories.DataContext;
using HalloDoc.Repositories.Interfaces;
using HalloDoc.Repositories.DataModels;

namespace HalloDoc.Repositories.Implementation
{
    public class PhysicianRepo : IPhysicianRepo
    {
        private readonly ApplicationDbContext _DbContext;
        public PhysicianRepo(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public List<Physician> GetPhysiciansData(int regionId)
        {
            return  _DbContext.Physicians.Where(a=>a.RegionId == regionId).ToList();

        }

        public Physician GetPhysician(int? physicianid)
        {
            return _DbContext.Physicians.FirstOrDefault(a => a.PhysicianId == physicianid);
        }
    }
}
