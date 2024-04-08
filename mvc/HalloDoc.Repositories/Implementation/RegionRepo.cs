using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.DataContext;
using HalloDoc.Repositories.Interfaces;

namespace HalloDoc.Repositories.Implementation
{
    public class RegionRepo : IRegionRepo
    {
        private readonly ApplicationDbContext _Dbcontext;
        public RegionRepo(ApplicationDbContext dbcontext)
        {
            _Dbcontext = dbcontext;
        }
        public List<Region> GetRegions() 
        {
            var region =  _Dbcontext.Regions.ToList();
            return region;
        }
        public Region GetRegion(int? regionid)
        {
            return _Dbcontext.Regions.FirstOrDefault(a => a.RegionId == regionid);
        }

        public string GetRegionName(int? regionid) 
        {
            string name = _Dbcontext.Regions.FirstOrDefault(a => a.RegionId == regionid).Name;
            return name;
        }
    }
}
