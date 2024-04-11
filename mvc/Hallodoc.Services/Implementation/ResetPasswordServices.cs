using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Interfaces;
using HallodocServices.Interfaces;
using HallodocServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Implementation
{
    public class ResetPasswordServices : IResetPasswordServices
    {
        private readonly IAspNetUserRepo _aspNetUserRepo;
        private readonly IEncryptionDecryptionServices _encryptionDecryptionServices;
        private readonly IPasswordHashServices _passwordHashServices;

        public ResetPasswordServices(IAspNetUserRepo aspNetUserRepo, IEncryptionDecryptionServices encryptionDecryptionServices, IPasswordHashServices passwordHashServices)
        {
            _aspNetUserRepo = aspNetUserRepo;
            _encryptionDecryptionServices = encryptionDecryptionServices;
            _passwordHashServices = passwordHashServices;
        }

        public async Task<bool> ResetPassword(PatientResetPasswordVM patientResetPasswordVM)
        {
           
            AspNetUser aspNetUser = _aspNetUserRepo.GetData(patientResetPasswordVM.Id);
            aspNetUser.PasswordHash = _passwordHashServices.PasswordHash(patientResetPasswordVM.Password);
            await _aspNetUserRepo.UpdateTable(aspNetUser);
            return true;
        }
    }
}
