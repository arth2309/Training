using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HallodocServices.Interfaces;
using HallodocServices.ModelView;
using HalloDoc.Repositories.Interfaces;

namespace HallodocServices.Implementation
{
    public class SendOrderServices : ISendOrderServices
    {
        private readonly IProfessionRepo _professionRepo;
        private readonly IVendorRepo _vendorRepo;

        public SendOrderServices(IProfessionRepo professionRepo, IVendorRepo vendorRepo)
        {
            _professionRepo = professionRepo;
            _vendorRepo = vendorRepo;
        }

        public AdminSendOrder GetList(int id) 
        { 
            AdminSendOrder adminSendOrder = new AdminSendOrder();
            adminSendOrder.professionalTypeList = _professionRepo.ProfessionalTypes();
            adminSendOrder.professionalList = _vendorRepo.GetVendorData(id);
            return adminSendOrder;
        }
    }
}
