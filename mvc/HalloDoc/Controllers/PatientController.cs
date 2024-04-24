using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Runtime.Intrinsics.X86;
using HalloDoc.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using HallodocServices.ModelView;
using HallodocServices.Interfaces;
using Microsoft.AspNetCore.Http;
using Azure;
using HalloDoc.Repositories.Interfaces;
using HalloDoc.Auth;

namespace HalloDoc.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientLoginServices _loginServices;

        private readonly IPatientDashBoardServices _dashBoardServices;
        private readonly IForgotPasswordServices _forgotPasswordServices;
        private readonly IJwtServices _jwtServices;
        private readonly IPasswordHashServices _passwordHashServices;
        private readonly IEncryptionDecryptionServices _encryptionDecryptionServices;
        private readonly IResetPasswordServices _resetPasswordServices;
        private readonly IPatientUserProfileServices _profileServices;
        private readonly IPatientShowDocumentsServices _showDocumentsServices;
        private readonly IPatientSendRequestServices _sendRequestServices;
        
        
       

        public PatientController(IPatientLoginServices loginServices, IPatientDashBoardServices dashBoardServices, IForgotPasswordServices forgotPasswordServices,IJwtServices jwtServices,IPasswordHashServices passwordHashServices,IPatientUserProfileServices patientUserProfileServices,IPatientShowDocumentsServices patientShowDocumentsServices,IPatientSendRequestServices sendRequestServices,IEncryptionDecryptionServices encryptionDecryptionServices,IResetPasswordServices resetPasswordServices)
        {
            
            _loginServices = loginServices;
            _dashBoardServices = dashBoardServices;
            _forgotPasswordServices = forgotPasswordServices;
            _jwtServices = jwtServices;
            _passwordHashServices = passwordHashServices;
            _profileServices = patientUserProfileServices;
            _showDocumentsServices = patientShowDocumentsServices;
            _sendRequestServices = sendRequestServices;
            _encryptionDecryptionServices = encryptionDecryptionServices;
            _resetPasswordServices = resetPasswordServices;
        }

      

        public IActionResult Index()
        {

            Response.Cookies.Delete("Name");
            Response.Cookies.Delete("lid");
            Response.Cookies.Delete("token");
            return View();
        }

        public IActionResult PatientLogin()
        {
            
            return View();
        }
        public IActionResult SubmitRequest()
        {
            return View();
        }
        public IActionResult PatientForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public  IActionResult PatientForgotPassword(ForgotPassword forgotPassword)
        {
            _forgotPasswordServices.SendEmail(forgotPassword.Email);
            return View();
        }

        public IActionResult PatientResetPassword(string Id)
        {
         
            ViewBag.Id = _encryptionDecryptionServices.Decrypt(Id);
            return View();
        }

        [HttpPost]
        public async  Task<IActionResult> PatientResetPassword(PatientResetPasswordVM patientResetPasswordVM)
        {
            bool id = await _resetPasswordServices.ResetPassword(patientResetPasswordVM);
            return RedirectToAction("PatientLogin");
        }
        public IActionResult CreatePatientRequest()
        {
            return View();
        }
        public IActionResult CreateFamilyFriendRequest()
        {
            return View();
        }
        public IActionResult CreateConciergeRequest()
        {
            
            return View();
            
        }
        public IActionResult CreateBusinessRequest()
        {
            
            return View();
        }

        [HttpGet]
        public IActionResult CheckEmailExists(string email)
        {
            bool emailId = _sendRequestServices.CheckEmail(email);
            return Json(emailId);
        }




        [CustomAuthorize("Patient")]
        public IActionResult PatientDashBoard()
        {
            if(Request.Cookies["id"] == null)
              {
                return RedirectToAction("PatientLogin");
              }
            int id = Int32.Parse(Request.Cookies["lid"]);
            string token = Request.Cookies["token"];
            List<PatientDashBoard> patientDashreq = _dashBoardServices.patientDashBoards(id);
            ViewBag.Name = Request.Cookies["Name"];

            return View("PatientDashboard", patientDashreq);


        }

        [CustomAuthorize("Patient")]
        public IActionResult PatientDashBoardDoc(int id)
        {
            ViewBag.Name = Request.Cookies["Name"];
            List<PatientShowDocument> patientShowDocuments = _showDocumentsServices.showDocuments(id);

            return View(patientShowDocuments);
        }

        [CustomAuthorize("Patient")]
        public IActionResult UserProfile()
        {
            ViewBag.Name = Request.Cookies["Name"];
            int id = Int32.Parse(Request.Cookies["lid"]);
            

            PatientUserProfile profile = _profileServices.GetUserProfile(id);


            return View("UserProfile", profile);
        }

        [HttpPost]
        public async Task <IActionResult> UserProfile(PatientUserProfile up)
        {

          await  _profileServices.EditUserProfile(up);
            return RedirectToAction("UserProfile");
        }

        [CustomAuthorize("Patient")]
        public IActionResult SubmitMe()
        {
            ViewBag.Name = Request.Cookies["Name"];
            int id = Int32.Parse(Request.Cookies["UserId"]);
            PatientSubmitMe showProfile = _sendRequestServices.SubmitMeData(id);
            return View(showProfile);
        }

        [CustomAuthorize("Patient")]
        public IActionResult SubmitSomeOne()
        {
            ViewBag.Name = Request.Cookies["Name"];
            return View();
        }

        public IActionResult AccessDenied() 
        {
           
            return View();
        }
       
        public IActionResult Logout()
        {

            Response.Cookies.Delete("Name");
            Response.Cookies.Delete("lid");
            Response.Cookies.Delete("token");
            return RedirectToAction("PatientLogin");
        }

        [HttpPost]
         public IActionResult PatientLogin(PatientLogin patientLogin)
        {
            if(!ModelState.IsValid)
            {
                return View(patientLogin);
            }
            int id = _loginServices.ValidateUser(patientLogin);
            string token = _jwtServices.GenerateJWTAuthetication(patientLogin.Email);

           if(id == 0)

            {
                TempData["invalid-user"] = true;
                return View(patientLogin);
            }
            else
            {
                CookieOptions options = new CookieOptions();
                options.Secure = true;
                options.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Append("Id", id.ToString(), options);

                UserVM userVM = _loginServices.Object(id);
                Response.Cookies.Append("Name", userVM.UserName);
                Response.Cookies.Append("lid", userVM.Id.ToString());

                Response.Cookies.Append("token", token);

                return RedirectToAction("PatientDashBoard", "Patient");
            }

           

        }


        [HttpPost]
        public async Task<IActionResult> CreatePatientRequest(PatientSendRequests patientSendRequests)
        {
           
               if(ModelState.IsValid)
            {

               await _sendRequestServices.SendPatientRequest(patientSendRequests);
                return RedirectToAction("PatientLogin");

            }
               else
            {
                TempData["PModalShow"] = false;
                return View(null);
            }
            
            


        }
        [HttpPost]
        public async Task<IActionResult> CreateConciergeRequest(ConciergeSendRequests conciergeSendRequests)
        {
            
                if(ModelState.IsValid)
            {

               await _sendRequestServices.SendConciergeRequest(conciergeSendRequests);
                return RedirectToAction("PatientLogin");
            }
                else
            {
                TempData["CModalShow"] = false;
                return View(null);
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateFamilyFriendRequest(FamilyFriendSendRequests familyFriendSendRequests)
        {

            if (ModelState.IsValid)
            {

               await  _sendRequestServices.SendFamilyFriendRequest(familyFriendSendRequests);
                return RedirectToAction("PatientLogin");

            }
            else
            {
                TempData["FModalShow"] = false;
                return View(null);
            }

        }
        [HttpPost]
        public async Task<IActionResult> SubmitMe(PatientSubmitMe user)
        {
            int id = Int32.Parse(Request.Cookies["UserId"]);
            if (ModelState.IsValid)
            {

               await _sendRequestServices.SubmitMeRequest(user,id);
                return RedirectToAction("PatientLogin");

            }
            else
            {
                return View(null);
            }

        }

        [HttpPost]
        public async Task<IActionResult> SubmitSomeOne(PatientSubmitMe user)
        {

           
            int id = Int32.Parse(Request.Cookies["UserId"]);
            if (ModelState.IsValid)
            {

                await _sendRequestServices.SubmitMeRequest(user,id);
                return RedirectToAction("PatientLogin");
            }
            else
            {
                return View(null);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBusinessRequest(BusinessSendRequests businessSendRequests)
        {

            if (ModelState.IsValid)
            {

               await _sendRequestServices.SendBusinessRequest(businessSendRequests);
                return RedirectToAction("PatientLogin");
            }
            else
            {
                TempData["BModalShow"] = false;
                return View(null);
            }

        }

    }
}


