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

        public List<Physician> GetPhysiciansData()
        {
            var physiciianList = _DbContext.Physicians.ToList();
            return physiciianList;
        }
    }
}
