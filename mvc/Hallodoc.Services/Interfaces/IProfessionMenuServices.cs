using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.PagedList;
using HallodocServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Interfaces
{
    public interface IProfessionMenuServices
    {
        public ProfessionMenuVM GetVendorList();

        public PaginatedList<VendorsList> GetVendorListFilter(int ProfessionId, string name, int currentPage);

        Task<HealthProfessional> AddData(BusinessVM businessVM);

        BusinessVM ShowData(int VendorId);

        Task<BusinessVM> UpdateData(BusinessVM businessVM);

        Task<int> DeleteData(int Vendorid);
    }
}
