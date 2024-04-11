using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HallodocServices.Interfaces;
using HallodocServices.ModelView;
using Irony.Parsing;
using HalloDoc.Repositories.Interfaces;

namespace HallodocServices.Implementation
{
    public class ForgotPasswordServices : IForgotPasswordServices
    {

        private readonly IEncryptionDecryptionServices _encryptionDecryptionServices;
        private readonly IAspNetUserRepo _aspNetUserRepo;

        public ForgotPasswordServices(IEncryptionDecryptionServices encryptionDecryptionServices,IAspNetUserRepo aspNetUserRepo)
        {
            _encryptionDecryptionServices = encryptionDecryptionServices;
            _aspNetUserRepo = aspNetUserRepo;
        }
        public void SendEmail(string email)
        {
            MailMessage mm = new MailMessage("tatva.dotnet.arthgandhi@outlook.com", "arthgandhi7@gmail.com");
            mm.Subject = "Password Recovery";
            int id = _aspNetUserRepo.GetId(email);
            string encrypt = _encryptionDecryptionServices.Encrypt(id);
            string mainURL = "https://localhost:7091/Patient/PatientResetPassword" + "?Id=" + encrypt;
            mm.Body = string.Format("Hi {0}, Click on the link for a new password <p><a href=\"" + mainURL + "\">Link</a></p>", email);
            mm.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.office365.com";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(userName: "tatva.dotnet.arthgandhi@outlook.com", password: "Liony@2002");
            smtp.Port = 587;
            smtp.Send(mm);
        }

    }
}
