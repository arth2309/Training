using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Runtime.Intrinsics.X86;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using HallodocServices.ModelView;
using HallodocServices.Interfaces;
using Azure;
using HallodocServices.Implementation;
using System.Text.Json;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using ClosedXML.Excel;
using System.Data;
using HalloDoc.Auth;
using HalloDoc.Repositories.PagedList;
using System.Reflection;
using System;
using HalloDoc.Repositories.DataModels;


namespace HalloDoc.Controllers
{
    public class ProviderController : Controller
    {
        private readonly IProviderDashBoardServices _dashBoardServices;
        private readonly IViewCaseServices _viewCaseServices;
        private readonly IViewNoteServices _viewNoteServices;
        private readonly ISchedulingServices _schedulingServices;
        private readonly ICreatePhysicianAccountServices _createPhysicianAccountServices;
        private readonly IEncounterFormServices _createEncounterFormServices;
        private readonly IAssignCaseServices _assignCaseServices;
        private readonly ISendOrderServices _sendOrderServices;
        private readonly IViewUploadsServices _viewUploadsServices;

        public ProviderController(IProviderDashBoardServices dashBoardServices, IViewCaseServices viewCaseServices, IViewNoteServices viewNoteServices, ISchedulingServices schedulingServices, ICreatePhysicianAccountServices createPhysicianAccountServices, IEncounterFormServices createEncounterFormServices, IAssignCaseServices assignCaseServices, ISendOrderServices sendOrderServices, IViewUploadsServices viewUploadsServices)
        {
            _dashBoardServices = dashBoardServices;
            _viewCaseServices = viewCaseServices;
            _viewNoteServices = viewNoteServices;
            _schedulingServices = schedulingServices;
            _createPhysicianAccountServices = createPhysicianAccountServices;
            _createEncounterFormServices = createEncounterFormServices;
            _assignCaseServices = assignCaseServices;
            _sendOrderServices = sendOrderServices;
            _viewUploadsServices = viewUploadsServices;
        }

        [CustomAuthorize("Provider")]
        public IActionResult ProviderDashBoard()
        {
            int id = Int32.Parse(Request.Cookies["lid"]);
            AdminDashBoard adminDashBoard = _dashBoardServices.newStates(1,1,0,id,null);
            ViewBag.Name = Request.Cookies["Name"];
            return View(adminDashBoard);
        }

        [HttpGet]
        public IActionResult CheckStatus(int statusI, int currentPage, int typeid, string name)
        {

            int id = Int32.Parse(Request.Cookies["lid"]);
            List<NewState> newStates = _dashBoardServices.getStates(name, typeid, id, statusI,currentPage);


            if (statusI == 1)
            {
                return PartialView("_ProviderNewState", newStates);
            }

            else if (statusI == 2)
            {
                return PartialView("_ProviderPendingState", newStates);
            }
            else if (statusI == 3)
            {
                return PartialView("_ProviderActiveState", newStates);
            }
            else
            {
                return PartialView("_ProviderConcludeState", newStates);
            }
           





        }

        [CustomAuthorize("Provider")]
        public IActionResult ProviderViewCase(int rcid)
        {
            
            AdminViewCase adminViewCase = _viewCaseServices.GetAdminViewCaseData(rcid);
            return View(adminViewCase);
        }

        [CustomAuthorize("Provider")]
        public IActionResult ProviderViewNotes(int reqid)
        {
            ViewBag.Name = Request.Cookies["Name"];
            ViewBag.ViewNotesReqid = reqid;
            AdminViewNote adminViewNote = _viewNoteServices.GetViewNote(reqid);
            return View(adminViewNote);
        }


        [HttpPost]
        public async Task<IActionResult> ProviderViewNotes(AdminViewNote adminViewNote)
        {
            if (ModelState.IsValid)
            {
                TempData["NUpdate"] = "Data Updated Successfully";
                AdminViewNote adminViewNote1 = await _viewNoteServices.EditPhysicianNote(adminViewNote);
                return RedirectToAction("ProviderViewNotes", new { reqid = adminViewNote.RequestId });
            }
            return View(null);
        }

        public IActionResult MySchedule()
        {
            ViewBag.AdminName = Request.Cookies["AdminName"];
            int id = Int32.Parse(Request.Cookies["lid"]);
      
            AdminScheduling adminScheduling = _schedulingServices.GetProviderScheduling(id);
            return View(adminScheduling);
        }

        public IActionResult ProviderScheduling(string StartDay, string EndDay)
        {
            int id = Int32.Parse(Request.Cookies["lid"]);
            MonthSchedulingVM monthSchedulingVM = _schedulingServices.GetProviderSchedulingFilter(id,DateTime.Parse(StartDay), DateTime.Parse(EndDay));
            return PartialView("_ProviderScheduling", monthSchedulingVM);
        }

        public IActionResult MyProfile()
        {
            int id = Int32.Parse(Request.Cookies["lid"]);
            AdminProfile adminProfile = _createPhysicianAccountServices.GetPhysician(id);
            return View(adminProfile);
        }

        [HttpGet]
        public async Task<JsonResult> ProviderResetPassword(int Id, string Password)
        {
            bool result = await _createPhysicianAccountServices.ResetPassword(Id, Password);
            return Json(new { redirect = Url.Action("MyProfile")});

        }

        [HttpPost]
        public async Task<IActionResult> EditProviderAccountInformation(AdminProfile adminProfile)
        {
            bool result = await _createPhysicianAccountServices.EditProviderAccountInformation(adminProfile);
            return RedirectToAction("MyProfile");
        }

        [HttpPost]

        public async Task<IActionResult> EditPhysicianInformation(AdminProfile adminProfile)
        {
            bool result = await _createPhysicianAccountServices.EditPhysicianInformation(adminProfile);
            return RedirectToAction("MyProfile");
        }

        [HttpPost]
        public async Task<IActionResult> ProviderMailingAndBillingInformation(AdminProfile adminProfile)
        {
            bool result = await _createPhysicianAccountServices.ProviderMailingAndBillingInformation(adminProfile);
            return RedirectToAction("MyProfile");
        }
        [HttpPost]
        public async Task<IActionResult> EditProviderProfile(AdminProfile adminProfile)
        {
            bool result = await _createPhysicianAccountServices.EditProviderProfile(adminProfile);
            return RedirectToAction("MyProfile");
        }

        [HttpGet]
        public async Task<JsonResult> HouseCall(int RequestId)
        {
            bool result = await _createEncounterFormServices.HouseCall(RequestId);
            return Json(new { result });
        }

        public IActionResult ProviderEncounterForm(int RequestId)
        {
            ViewBag.Name = Request.Cookies["Name"];
            ViewBag.RequestId = RequestId;
             AdminEncounterForm adminEncounterForm = _createEncounterFormServices.GetEncounterFormData(RequestId);
           return View(adminEncounterForm);
            

           
        }

        [HttpGet]
        public IActionResult downloadEncounterDocument(int requestId)
        {
            AdminEncounterForm encounterDetails = _createEncounterFormServices.GetEncounterFormData(requestId) ;

            // Generate PDF
            var pdfBytes =  _createEncounterFormServices.GeneratePDFServices(encounterDetails);

            // Return the PDF as a file
            return File(pdfBytes, "application/pdf", "Encounter.pdf");

        }

        [HttpPost]
        public async Task<IActionResult> EditEncounterForm(AdminEncounterForm adminEncounterForm)
        {
       
           
            bool result = await _createEncounterFormServices.UpdateaddEncounterFormData(adminEncounterForm);
            return RedirectToAction("ProviderEncounterForm", new {RequestId = adminEncounterForm.RequestId});



        }

        public async Task<IActionResult> AcceptRequest(int RequestId)
        {
            bool result = await _assignCaseServices.AcceptRequest(RequestId);
            return RedirectToAction("ProviderDashBoard");

        }

        [HttpPost]
        public async Task<IActionResult> Transfer(AdminDashBoard newState)
        {
            bool result = await _assignCaseServices.Transfer(newState);
            return RedirectToAction("ProviderDashBoard");
        }


        [CustomAuthorize("Provider")]
        public IActionResult ProviderSendOrder(int reqID)

        {
            ViewBag.Name = Request.Cookies["Name"];
            ViewBag.reqID = reqID;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProviderSendOrder(AdminSendOrder adminSendOrder)
        {
            if (ModelState.IsValid)
            {
                await _sendOrderServices.AddDataServices(adminSendOrder);
                TempData["Order"] = "Placed Order Successfully";
                return RedirectToAction("ProviderDashBoard");
            }
            return View(null);
        }

        [CustomAuthorize("Provider")]
        public IActionResult ProviderViewUploads(int reqID)

        {
            ViewBag.Name = Request.Cookies["Name"];
            AdminViewUpoads adminViewUpoads = _viewUploadsServices.GetUpoads(reqID);
            return View(adminViewUpoads);
        }

        [HttpGet]
        public async Task<JsonResult> Delete(int id)
        {
            int reqId = _viewUploadsServices.GetReqIdService(id);
            await _viewUploadsServices.DeleteFileService(id);
            TempData["Delete"] = "File Deleted Successfully";
            return Json(new { redirect = Url.Action("ProviderViewUploads", new { reqID = reqId }) });
        }

        [HttpPost]

        public async Task<IActionResult> ProviderViewUploads(AdminViewUpoads adminViewUpoads)
        {
            if (adminViewUpoads.formFile != null)
            {
               await _viewUploadsServices.AddFileData(adminViewUpoads);
                TempData["Upload"] = "File Uploaded Successfully";
                return RedirectToAction("ProviderViewUploads", new { reqID = adminViewUpoads.requestId });
            }
            TempData["UploadF"] = "File is Required";
            return RedirectToAction("ProviderViewUploads", new { reqID = adminViewUpoads.requestId });
        }

        public ActionResult DownloadAll(int ReqId)
        {
            byte[] fileBytes = _viewUploadsServices.GetFilesAsZip(ReqId);
            return File(fileBytes, "application/zip", "files.zip");
        }

        [HttpPost]
        public ActionResult? DownloadSelected(List<string> filesChecked)
        {


            byte[] fileBytes = _viewUploadsServices.GetSelectedFilesAsZip(filesChecked);
            return File(fileBytes, "application/zip", "files.zip");


        }



        public IActionResult ViewUploadsSendMail(int reqID)
        {
            _viewUploadsServices.SendEmail(reqID);
            return RedirectToAction("ProviderViewUploads", new { reqID = reqID });
        }

        public IActionResult ConcludeCare(int RequestId)
        {
            ConcludeCareVM concludeCareVM = _createEncounterFormServices.GetConcludeCareFile(RequestId);
            return View(concludeCareVM);
        }


        public async  Task<IActionResult> Finalize(int RequestId)
        {
            bool result = await _createEncounterFormServices.Finalize(RequestId);
            return RedirectToAction("ProviderDashBoard");
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(ConcludeCareVM concludeCareVM)
        {
            bool result = await _createEncounterFormServices.AddFileData(concludeCareVM);
            return RedirectToAction("ConcludeCare", new { RequestId = concludeCareVM.reqid });
        }

        [HttpPost]
        public async Task<IActionResult> Conclude(ConcludeCareVM concludeCareVM)
        {
            bool result = await _createEncounterFormServices.Conclude(concludeCareVM);
            return RedirectToAction("ProviderDashBoard");
        }

        [HttpGet]

        public async Task<JsonResult> ToConclude(int RequestId)
        {
            bool result = await _createEncounterFormServices.ToConclude(RequestId);
            return Json(result);
        }

    }
}
