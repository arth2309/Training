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

        public ProviderController(IProviderDashBoardServices dashBoardServices, IViewCaseServices viewCaseServices, IViewNoteServices viewNoteServices, ISchedulingServices schedulingServices, ICreatePhysicianAccountServices createPhysicianAccountServices)
        {
            _dashBoardServices = dashBoardServices;
            _viewCaseServices = viewCaseServices;
            _viewNoteServices = viewNoteServices;
            _schedulingServices = schedulingServices;
            _createPhysicianAccountServices = createPhysicianAccountServices;
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


    }
}
