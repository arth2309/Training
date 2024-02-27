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

namespace HallodocServices.Implementation
{
    public class ForgotPasswordServices : IForgotPasswordServices
    {
        

        public void SendEmail(string email)
        {
            MailMessage mm = new MailMessage("tatva.dotnet.arthgandhi@outlook.com", email);
            mm.Subject = "Password Recovery";
            mm.Body = string.Format("Hi {0}, Click on the link for a new password <p><a href=\""  + "\">Link</a></p>", email);
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
