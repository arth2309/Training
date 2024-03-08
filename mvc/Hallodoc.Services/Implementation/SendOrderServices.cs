using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HallodocServices.Interfaces;
using HallodocServices.ModelView;
using HalloDoc.Repositories.Interfaces;
using HalloDoc.Repositories.DataModels;

namespace HallodocServices.Implementation
{
    public class SendOrderServices : ISendOrderServices
    {
        private readonly IProfessionRepo _professionRepo;
        private readonly IVendorRepo _vendorRepo;
        private readonly IOrderDetailRepo _orderDetailRepo;

        public SendOrderServices(IProfessionRepo professionRepo, IVendorRepo vendorRepo, IOrderDetailRepo orderDetailRepo)
        {
            _professionRepo = professionRepo;
            _vendorRepo = vendorRepo;
            _orderDetailRepo = orderDetailRepo;
        }

        public List<HealthProfessional> GetHealthProfessionalByType(int professionalTypeId)
        {
            return _vendorRepo.GetVendorList(professionalTypeId);
        }

        public HealthProfessional GetProfessionalById(int vendorId)
        {
            return _vendorRepo.GetVendor(vendorId);
        }

        public List<HealthProfessionalType> GetProfessionalTypes()
        {
            return _professionRepo.ProfessionalTypes();
        }

        public async Task<bool> AddDataServices(AdminSendOrder adminSendOrder)
        {
            OrderDetail orderDetail = new OrderDetail();
            orderDetail.CreatedDate = DateTime.Now;
            orderDetail.VendorId = adminSendOrder.VendorId;
            orderDetail.RequestId = adminSendOrder.RequestId;
            orderDetail.FaxNumber = adminSendOrder.FaxNumber;
            orderDetail.BusinessContact = adminSendOrder.PhoneNumber;
            orderDetail.Email = adminSendOrder.Email;
            orderDetail.NoOfRefill = adminSendOrder.Refill;
            orderDetail.Prescription = adminSendOrder.Prescription;
            _orderDetailRepo.AddData(orderDetail);
            return true;
        }
    }
}
