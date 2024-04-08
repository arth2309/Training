using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.ModelView
{
    public class ProfessionMenuVM
    {
        public List<HealthProfessionalType>? Professions { get; set; }

        public PaginatedList<VendorsList>? Vendors { get; set; }

        
    }
}
